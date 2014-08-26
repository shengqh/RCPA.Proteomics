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
using RCPA.Normalization;
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

    public override IEnumerable<string> Process(string fileName)
    {
      options.ProteinFileName = fileName;

      var refNames = (from refF in options.References select refF.Name).Merge("");
      string resultFileName = GetResultFileName(fileName, refNames);

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
        throw new Exception("No isobaric labelling information matched from " + options.IsobaricFileName);
      }

      Progress.SetMessage("Normalizing ...");

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
              sw.Write("\t{0:0.0}", item[i]);
            }
            sw.WriteLine();
          }
        }

        var roptions = new NormalizationRCalculatorOptions();
        roptions.InputFile = datafile;
        roptions.OutputFile = Path.ChangeExtension(datafile, ".norm.tsv");
        roptions.RTemplate = FileUtils.GetTemplateDir() + "/CyclicLoessNormalization.r";

        new NormalizationRCalculator(roptions).Process();

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
              item[i - 1] = double.Parse(parts[i]);
            }
          }
        }
      }

      Progress.SetMessage("Calculating ...");

      var refFuncs = options.References;
      var samFuncs = options.GetSamples();

      foreach (var dsName in options.DatasetMap.Keys)
      {
        var dsSet = new HashSet<string>(options.DatasetMap[dsName]);
        var dsSpectra = new HashSet<IIdentifiedSpectrum>(from s in isoSpectra
                                                         where dsSet.Contains(s.Query.FileScan.Experimental)
                                                         select s);

        //for each dataset, calculate ratio
        var datafile = string.Format("{0}.{1}.tsv", resultFileName, dsName);
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

          //call R to calculate the ratio of each group
          //read R result to fill the ratio
        }
      }

      var irFormat = GetFormat(ir);

      irFormat.WriteToFile(resultFileName, ir);

      Progress.SetMessage("Finished.");

      return new[] { resultFileName };
    }

    protected virtual string GetResultFileName(string fileName, string refNames)
    {
      return fileName + "." + refNames + ".isobaric";
    }
  }
}
