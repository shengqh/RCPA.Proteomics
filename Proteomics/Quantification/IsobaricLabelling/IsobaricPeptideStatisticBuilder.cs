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
  public class IsobaricPeptideStatisticBuilder : AbstractThreadFileProcessor
  {
    private IsobaricPeptideStatisticBuilderOption options;

    public IsobaricPeptideStatisticBuilder(IsobaricPeptideStatisticBuilderOption options)
    {
      this.options = options;
    }

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
      options.PeptideFileName = fileName;

      var refNames = (from refF in options.References select refF.Name).Merge("");
      string resultFileName = GetResultFilePrefix(fileName, refNames);

      string paramFileName = Path.ChangeExtension(resultFileName, ".param");
      options.SaveToFile(paramFileName);

      Progress.SetMessage("Reading peptides...");

      List<IIdentifiedSpectrum> spectra = new MascotPeptideTextFormat().ReadFromFile(fileName);

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
        var msg = "Normalizing channels using cyclic loess algorithm ";

        var detailsDir = resultFileName + ".details";
        if (!Directory.Exists(detailsDir))
        {
          Directory.CreateDirectory(detailsDir);
        }

        var isoGroup = isoSpectra.GroupBy(m => m.Query.FileScan.Experimental).ToList();
        Progress.SetRange(0, isoGroup.Count);
        Progress.SetPosition(0);
        var fileIndex = 0;
        foreach (var isoFile in isoGroup)
        {
          if (Progress.IsCancellationPending())
          {
            throw new UserTerminatedException();
          }
          fileIndex++;

          Progress.SetMessage("{0} {1}/{2} ...", msg, fileIndex, isoGroup.Count);

          var datafile = string.Format("{0}\\{1}.{2}.tsv", detailsDir, Path.GetFileNameWithoutExtension(resultFileName), isoFile.Key);
          var rresultfile = Path.ChangeExtension(datafile, ".norm.tsv");
          //if (!File.Exists(rresultfile))
          {
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
            roptions.OutputFile = rresultfile;
            roptions.RTemplate = FileUtils.GetTemplateDir() + "/CyclicLoessNormalization.r";

            new RTemplateProcessor(roptions).Process();

            Progress.SetPosition(fileIndex);
          }

          var specMap = isoFile.ToDictionary(m => m.Query.FileScan.LongFileName);

          //read R result to replace the intensity of each spectrum
          using (var sr = new StreamReader(rresultfile))
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
                throw new Exception(string.Format("{0} can not be found! The first column of normalization result file {1} must be FileScan!", parts[0], rresultfile));
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

      var pepfile = resultFileName + ".tsv";
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

      var qoptions = new RTemplateProcessorOptions();
      qoptions.InputFile = pepfile;
      qoptions.OutputFile = resultFileName + ".quan.tsv";

      qoptions.RTemplate = string.Format("{0}/PeptideQuantification.r", FileUtils.GetTemplateDir());
      qoptions.Parameters.Add(string.Format("missingvalue<-{0}", IsobaricConsts.NULL_INTENSITY));
      qoptions.Parameters.Add("pvalue<-0.01");
      qoptions.Parameters.Add("minFinalCount<-3");

      new RTemplateProcessor(qoptions).Process();

      Progress.SetMessage("Finished.");

      return new[] { qoptions.OutputFile };
    }

    protected virtual string GetResultFilePrefix(string fileName, string refNames)
    {
      return fileName + "." + refNames;
    }
  }
}
