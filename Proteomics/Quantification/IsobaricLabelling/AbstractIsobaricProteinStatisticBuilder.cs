using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RCPA.Proteomics.Quantification;
using RCPA.Proteomics.Summary;
using RCPA.Proteomics.Sequest;
using MathNet.Numerics.Statistics;
using System.IO;
using RCPA.Utils;
using RCPA.Proteomics.Utils;
using RCPA.Proteomics.Mascot;
using MathNet.Numerics.Distributions;
using RCPA.Numerics;
using RCPA.R;

namespace RCPA.Proteomics.Quantification.IsobaricLabelling
{
  public abstract class AbstractIsobaricProteinStatisticBuilder : AbstractThreadFileProcessor
  {
    private IsobaricProteinStatisticBuilderOptions options;

    public AbstractIsobaricProteinStatisticBuilder(IsobaricProteinStatisticBuilderOptions options)
    {
      this.options = options;
    }

    protected abstract IIdentifiedResult GetIdentifiedResult(string fileName);

    protected abstract MascotResultTextFormat GetFormat(IIdentifiedResult ir);

    private string GetSequence(IIdentifiedSpectrum sepc)
    {
      var pure = sepc.GetMatchSequence();
      var result = new StringBuilder();
      var dic = new Dictionary<char, int>();
      foreach (var c in pure)
      {
        if (Char.IsLetter(c))
        {
          result.Append(c);
        }
        else
        {
          if (dic.ContainsKey(c))
          {
            dic[c] = dic[c] + 1;
          }
          else
          {
            dic[c] = 1;
          }
        }
      }

      return string.Format("{0}_{1}", result.ToString(), (from k in dic.Keys
                                                          orderby k
                                                          select string.Format("{0}{1}", k, dic[k])).Merge("_"));
    }

    public override IEnumerable<string> Process(string fileName)
    {
      options.ProteinFileName = fileName;

      var refNames = (from refF in options.References select refF.Name).Merge("");
      string resultFileName = GetResultFilePrefix(fileName, refNames);

      string paramFileName = Path.ChangeExtension(resultFileName, ".param");
      options.SaveToFile(paramFileName);

      Progress.SetMessage("Reading proteins...");

      IIdentifiedResult ir = GetIdentifiedResult(fileName);

      List<IIdentifiedSpectrum> spectra = ir.GetSpectra();

      IsobaricScanUtils.Load(spectra, options.IsobaricFileName, false, this.Progress);

      var isoSpectra = (from s in spectra
                        where s.FindIsobaricItem() != null
                        select s).ToList();

      if (isoSpectra.Count == 0)
      {
        throw new Exception(string.Format("No isobaric labelling information between {0} and {1}", fileName, options.IsobaricFileName));
      }

      if (options.PerformNormalizition)
      {
        Progress.SetMessage("Normalizing channels using cyclic loess algorithm...");

        var isoGroup = isoSpectra.GroupBy(m => m.Query.FileScan.Experimental).ToList();
        foreach (var isoFile in isoGroup)
        {
          var datafile = string.Format("{0}.{1}.tsv", resultFileName, isoFile.Key);
          using (var sw = new StreamWriter(datafile))
          {
            sw.WriteLine("FileScan\t{0}", (from cha in options.PlexType.Channels select cha.Name).Merge("\t"));

            foreach (var isoSpec in isoFile)
            {
              sw.Write("{0}", isoSpec.Query.FileScan.LongFileName);
              var item = isoSpec.FindIsobaricItem();
              for (int i = 0; i < options.PlexType.Channels.Count; i++)
              {
                sw.Write("\t{0:0.0}", item[i].Intensity);
              }
              sw.WriteLine();
            }
          }

          var roptions = new RTemplateProcessorOptions();
          roptions.InputFile = datafile;
          roptions.OutputFile = Path.ChangeExtension(datafile, ".norm.tsv");
          roptions.RTemplate = FileUtils.GetTemplateDir() + "/CyclicLoessNormalization.r";

          new RTemplateProcessor(roptions).Process();

          var specMap = isoFile.ToDictionary(m => m.Query.FileScan.LongFileName);

          //read R result to replace the intensity of each spectrum
          using (var sr = new StreamReader(roptions.OutputFile))
          {
            //ignore header
            string line = sr.ReadLine();
            IIdentifiedSpectrum spec;
            while ((line = sr.ReadLine()) != null)
            {
              if (string.IsNullOrWhiteSpace(line))
              {
                break;
              }

              var parts = line.Split('\t');
              if (!specMap.TryGetValue(parts[0], out spec))
              {
                throw new Exception(string.Format("{0} can not be found! The first column of normalization result file {1} must be FileScan!", parts[0], roptions.OutputFile));
              }

              var item = spec.FindIsobaricItem();
              for (int i = 1; i < parts.Length; i++)
              {
                item[i - 1].Intensity = double.Parse(parts[i]);
              }
            }
          }
        }
      }

      Progress.SetMessage("Quantifying peptide with outlier detection ...");

      var refFuncs = options.References;
      var samFuncs = options.GetSamples();

      var pepfile = string.Format("{0}.peptides.tsv", resultFileName);
      using (var sw = new StreamWriter(pepfile))
      {
        sw.WriteLine("Subject\tDataset\tFileScan\tSequence\tREF\t{0}",
          samFuncs.ConvertAll(m => m.Name).Merge("\t"));

        var peptides = isoSpectra.ToGroupDictionary(m => m.Peptide.PureSequence).OrderBy(m => m.Key).ToList();
        foreach (var pep in peptides)
        {
          foreach (var dsName in options.DatasetMap.Keys)
          {
            var dsSet = new HashSet<string>(options.DatasetMap[dsName]);
            var dsSpectra = (from s in pep.Value
                             where dsSet.Contains(s.Query.FileScan.Experimental)
                             orderby s.Peptide.Sequence
                             select s).ToList();

            foreach (var spec in dsSpectra)
            {
              var isoitem = spec.FindIsobaricItem();
              sw.WriteLine("{0}\t{1}\t{2}\t{3}\t{4:0.0}\t{5}",
                  pep.Key,
                  dsName,
                  spec.Query.FileScan.ShortFileName,
                  spec.Peptide.Sequence,
                  refFuncs.ConvertAll(m => m.GetValue(isoitem)).Sum(),
                  samFuncs.ConvertAll(m => string.Format("{0:0.0}", m.GetValue(isoitem))).Merge("\t"));
            }
          }
        }
      }

      var proteinpeptidefile = string.Format("{0}.proteins.tsv", resultFileName);
      using (var sw = new StreamWriter(proteinpeptidefile))
      {
        sw.WriteLine("Index\tPeptide\tProteins\tDescription\tPepCount\tUniquePepCount");
        foreach (var g in ir)
        {
          var peps = g.GetPeptides();
          var seqs = (from p in peps
                      select p.Peptide.PureSequence).Distinct().OrderBy(m => m).ToArray();
          var proname = (from p in g select p.Name).Merge(" ! ");
          var description = (from p in g select p.Description).Merge(" ! ");
          foreach (var seq in seqs)
          {
            sw.WriteLine("{0}\t{1}\t{2}\t{3}\t{4}\t{5}",
              g.Index,
              seq,
              proname,
              description,
              g[0].PeptideCount,
              g[0].UniquePeptideCount);
          }
        }
      }

      var qoptions = new RTemplateProcessorOptions();
      qoptions.InputFile = pepfile;
      qoptions.OutputFile = resultFileName + ".proteins.quan." + options.PeptideToProteinMethod + ".tsv";

      qoptions.RTemplate = string.Format("{0}/Quantification{1}.r", FileUtils.GetTemplateDir(), options.PeptideToProteinMethod);
      qoptions.Parameters.Add(string.Format("proteinfile<-\"{0}\"", proteinpeptidefile.Replace("\\", "/")));
      qoptions.Parameters.Add(string.Format("peptidequanfile<-\"{0}\"", FileUtils.ChangeExtension(pepfile, ".quan.tsv").Replace("\\", "/")));
      qoptions.Parameters.Add(string.Format("missingvalue<-{0}", IsobaricConsts.NULL_INTENSITY));
      qoptions.Parameters.Add("pvalue<-0.01");
      qoptions.Parameters.Add("minFinalCount<-3");

      new RTemplateProcessor(qoptions).Process();

      //var irFormat = GetFormat(ir);

      //Progress.SetMessage("Writing result ...");

      //irFormat.WriteToFile(resultFileName, ir);

      Progress.SetMessage("Finished.");

      return new[] { qoptions.OutputFile };
    }

    private static void WriteForGLM(IIdentifiedResult ir, List<IsobaricIndex> refFuncs, List<IsobaricIndex> samFuncs, HashSet<IIdentifiedSpectrum> dsSpectra, string datafile)
    {
      using (var sw = new StreamWriter(datafile))
      {
        sw.WriteLine("GroupIndex\tPeptide\tCategory\tFile\tScan\tChannel\tIntensity");
        foreach (var group in ir)
        {
          var specs = group.GetPeptides();
          foreach (var spec in specs)
          {
            if (!dsSpectra.Contains(spec))
            {
              continue;
            }

            var isoitem = spec.FindIsobaricItem();
            foreach (var refFunc in refFuncs)
            {
              sw.WriteLine("{0}\t{1}\t{2}\t{3}\t{4}\t{5}\t{6}",
                group.Index,
                group[0].Name,
                "REF",
                spec.Query.FileScan.Experimental,
                spec.Query.FileScan.Scan,
                refFunc.Name,
                refFunc.GetValue(isoitem));
            }

            foreach (var samFunc in samFuncs)
            {
              sw.WriteLine("{0}\t{1}\t{2}\t{3}\t{4}\t{5}\t{6}",
                group.Index,
                group[0].Name,
                samFunc.Name,
                spec.Query.FileScan.Experimental,
                spec.Query.FileScan.Scan,
                samFunc.Name,
                samFunc.GetValue(isoitem));
            }
          }
        }
      }
    }

    private void WriteForLinearModel(IIdentifiedResult ir, List<IsobaricIndex> refFuncs, List<IsobaricIndex> samFuncs, HashSet<IIdentifiedSpectrum> dsSpectra, string datafile)
    {
      using (var sw = new StreamWriter(datafile))
      {
        sw.WriteLine("GroupIndex\tProtein\tPeptide\t{0}\t{1}",
          refFuncs.ConvertAll(m => m.Name).Merge("\t"),
          samFuncs.ConvertAll(m => m.Name).Merge("\t"));
        foreach (var group in ir)
        {
          var specs = group.GetPeptides();
          foreach (var spec in specs)
          {
            if (!dsSpectra.Contains(spec))
            {
              continue;
            }

            var isoitem = spec.FindIsobaricItem();
            sw.WriteLine("{0}\t{1}\t{2}\t{3}\t{4}",
                group.Index,
                group[0].Name,
                spec.Sequence,
                refFuncs.ConvertAll(m => m.GetValue(isoitem).ToString()).Merge("\t"),
                samFuncs.ConvertAll(m => m.GetValue(isoitem).ToString()).Merge("\t"));
          }
        }
      }
    }

    protected virtual string GetResultFilePrefix(string fileName, string refNames)
    {
      return fileName + "." + refNames;
    }
  }
}
