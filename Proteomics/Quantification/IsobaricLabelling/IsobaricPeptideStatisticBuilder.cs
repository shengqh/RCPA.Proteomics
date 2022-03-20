﻿using RCPA.Proteomics.Mascot;
using RCPA.Proteomics.Modification;
using RCPA.Proteomics.Summary;
using RCPA.R;
using RCPA.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace RCPA.Proteomics.Quantification.IsobaricLabelling
{
  public class IsobaricPeptideStatisticBuilder : AbstractThreadProcessor
  {
    private IsobaricPeptideStatisticBuilderOptions options;

    public IsobaricPeptideStatisticBuilder(IsobaricPeptideStatisticBuilderOptions options)
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

    public override IEnumerable<string> Process()
    {
      var design = new IsobaricLabelingExperimentalDesign();
      design.LoadFromFile(options.DesignFile);

      string resultFileName = GetResultFilePrefix(design);

      string paramFileName = Path.ChangeExtension(resultFileName, ".param");
      options.SaveToFile(paramFileName);

      Progress.SetMessage("Reading peptides...");

      List<IIdentifiedSpectrum> spectra = new MascotPeptideTextFormat().ReadFromFile(options.PeptideFile);
      IsobaricScanUtils.Load(spectra, design.IsobaricFile, false, this.Progress);

      var isoSpectra = (from s in spectra
                        where s.FindIsobaricItem() != null
                        select s).ToList();

      if (isoSpectra.Count == 0)
      {
        throw new Exception(string.Format("No isobaric labelling information between {0} and {1}", options.PeptideFile, options.DesignFile));
      }

      if (options.PerformNormalizition)
      {
        var msg = "Normalizing channels using loess algorithm";

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
              sw.WriteLine("FileScan\t{0}", (from cha in design.PlexType.Channels select cha.Name).Merge("\t"));

              foreach (var isoSpec in isoFile)
              {
                sw.Write("{0}", isoSpec.Query.FileScan.LongFileName);
                var item = isoSpec.FindIsobaricItem();
                for (int i = 0; i < design.PlexType.Channels.Count; i++)
                {
                  sw.Write("\t{0:0.0}", item[i].Intensity);
                }
                sw.WriteLine();
              }
            }

            var roptions = new RTemplateProcessorOptions();
            roptions.InputFile = datafile;
            roptions.OutputFile = rresultfile;
            roptions.Parameters.Add(string.Format("missingvalue<-{0}", IsobaricConsts.NULL_INTENSITY));
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
      FilterSpectraByQuantifyMode(isoSpectra);

      var refFuncs = design.References;
      var samFuncs = design.GetSamples();

      var pepfile = resultFileName + ".tsv";
      using (var sw = new StreamWriter(pepfile))
      {
        sw.WriteLine("Subject\tDataset\tFileScan\tSequence\tREF\t{0}",
          samFuncs.ConvertAll(m => m.Name).Merge("\t"));
        Func<IIdentifiedSpectrum, string> keyFunc;
        if (options.Mode == QuantifyMode.qmModificationSite)
        {
          keyFunc = m => m.GetMatchSequence();
        }
        else
        {
          keyFunc = m => m.Peptide.PureSequence;
        }

        var peptides = isoSpectra.ToGroupDictionary(m => keyFunc(m)).OrderBy(m => m.Key).ToList();
        foreach (var pep in peptides)
        {
          foreach (var dsName in design.DatasetMap.Keys)
          {
            var dsSet = new HashSet<string>(design.DatasetMap[dsName]);
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

    /// <summary>
    /// Filter spectra by quantify mode
    /// </summary>
    /// <param name="isoSpectra"></param>
    public void FilterSpectraByQuantifyMode(List<IIdentifiedSpectrum> isoSpectra)
    {
      if (QuantifyMode.qmModificationSite == options.Mode)
      {
        isoSpectra.RemoveAll(m => !ModificationUtils.IsModifiedSequence(m.GetMatchSequence(), options.ModifiedAminoacids));

        if (isoSpectra.Any(m => m.Peptide.GetSiteProbabilities().Any(l => l.Probability > 0.0)))
        {
          isoSpectra.RemoveAll(m => m.Peptide.GetSiteProbabilities().Any(l => l.Probability < options.MinimumSiteProbability));
        }
        else
        {
          Progress.SetMessage("There is no site probability in spectra, cannot filter spectra by minimum site probability, filter ignored.");
        }
      }
      else if (QuantifyMode.qmUnmodifiedPeptide == options.Mode)
      {
        isoSpectra.RemoveAll(m =>
        {
          return ModificationUtils.IsModifiedSequence(m.GetMatchSequence(), options.ModifiedAminoacids);
        });
      }
    }

    protected virtual string GetResultFilePrefix(IsobaricLabelingExperimentalDesign design)
    {
      return string.Format("{0}.{1}.{2}", options.PeptideFile, options.Mode, design.GetReferenceNames(""));
    }
  }
}
