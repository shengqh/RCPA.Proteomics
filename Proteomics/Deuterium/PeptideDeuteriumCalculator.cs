using MathNet.Numerics.Statistics;
using RCPA.Proteomics.Mascot;
using RCPA.Proteomics.Quantification.Labelfree;
using RCPA.Proteomics.Summary;
using RCPA.R;
using RCPA.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace RCPA.Proteomics.Deuterium
{
  public class PeptideDeuteriumCalculator : AbstractThreadProcessor
  {
    private static string DeuteriumR = Path.Combine(FileUtils.GetTemplateDir().FullName, "ProfileChroDeuterium.r");

    private DeuteriumCalculatorOptions options;
    public PeptideDeuteriumCalculator(DeuteriumCalculatorOptions options)
    {
      this.options = options;
    }

    public override IEnumerable<string> Process()
    {
      //Extract chromotagraph information
      var chroOptions = new ChromatographProfileBuilderOptions();
      options.CopyProperties(chroOptions);
      chroOptions.InputFile = options.InputFile;
      chroOptions.OutputFile = options.BoundaryOutputFile;
      chroOptions.DrawImage = false;
      var builder = new ChromatographProfileBuilder(chroOptions);
      if (!File.Exists(options.BoundaryOutputFile) || options.Overwrite)
      {
        Progress.SetMessage("Finding envelope ...");
        builder.Progress = this.Progress;
        builder.Process();
      }

      //Calculate deuterium enrichment for peptide
      if (!File.Exists(options.DeuteriumOutputFile) || options.Overwrite)
      {
        Progress.SetMessage("Calculating deuterium ...");
        var deuteriumOptions = new RTemplateProcessorOptions()
        {
          InputFile = options.BoundaryOutputFile,
          OutputFile = options.DeuteriumOutputFile,
          RTemplate = DeuteriumR,
          RExecute = SystemUtils.GetRExecuteLocation(),
          CreateNoWindow = true
        };

        deuteriumOptions.Parameters.Add("outputImage<-" + (options.DrawImage ? "1" : "0"));
        deuteriumOptions.Parameters.Add("excludeIsotopic0<-" + (options.ExcludeIsotopic0 ? "1" : "0"));

        new RTemplateProcessor(deuteriumOptions) { Progress = this.Progress }.Process();
      }

      var deuteriumMap = new AnnotationFormat().ReadFromFile(options.DeuteriumOutputFile).ToDictionary(m => m.Annotations["ChroFile"].ToString());

      //Read old spectra information
      var format = new MascotPeptideTextFormat();
      var spectra = format.ReadFromFile(options.InputFile);

      foreach (var spec in spectra)
      {
        spec.Annotations.Remove("RetentionTime");
        spec.Annotations.Remove("TheoreticalDeuterium");
        spec.Annotations.Remove("ObservedDeuterium");
        spec.Annotations.Remove("NumDeuteriumIncorporated");
        spec.Annotations.Remove("NumExchangableHydrogen");
        spec.Annotations.Remove("DeuteriumEnrichmentPercent");
      }

      var calcSpectra = new List<IIdentifiedSpectrum>();
      var aas = new Aminoacids();
      foreach (var pep in spectra)
      {
        var filename = Path.GetFileNameWithoutExtension(builder.GetTargetFile(pep));
        if (deuteriumMap.ContainsKey(filename))
        {
          var numExchangeableHydrogens = aas.ExchangableHAtom(pep.Peptide.PureSequence);
          var numDeuteriumIncorporated = double.Parse(deuteriumMap[filename].Annotations["NumDeuteriumIncorporated"] as string);

          pep.Annotations["PeakRetentionTime"] = deuteriumMap[filename].Annotations["RetentionTime"];
          pep.Annotations["TheoreticalDeuterium"] = deuteriumMap[filename].Annotations["TheoreticalDeuterium"];
          pep.Annotations["ObservedDeuterium"] = deuteriumMap[filename].Annotations["ObservedDeuterium"];
          pep.Annotations["NumDeuteriumIncorporated"] = deuteriumMap[filename].Annotations["NumDeuteriumIncorporated"];
          pep.Annotations["NumExchangableHydrogen"] = numExchangeableHydrogens;
          pep.Annotations["DeuteriumEnrichmentPercent"] = numDeuteriumIncorporated / numExchangeableHydrogens;

          calcSpectra.Add(pep);
        }
      }
      format.PeptideFormat.Headers = format.PeptideFormat.Headers + "\tPeakRetentionTime\tTheoreticalDeuterium\tObservedDeuterium\tNumDeuteriumIncorporated\tNumExchangableHydrogen\tDeuteriumEnrichmentPercent";
      format.NotExportSummary = true;
      format.WriteToFile(GetPeptideDeteriumFile(), calcSpectra);

      var specGroup = calcSpectra.GroupBy(m => m.Peptide.PureSequence).OrderBy(l => l.Key).ToList();

      var times = options.ExperimentalTimeMap.Values.Distinct().OrderBy(m => m).ToArray();
      using (var sw = new StreamWriter(options.OutputFile))
      {
        sw.WriteLine("Peptide\t{0}", (from t in times select t.ToString()).Merge("\t"));

        foreach (var peptide in specGroup)
        {
          var curSpectra = peptide.GroupBy(m => options.ExperimentalTimeMap[m.Query.FileScan.Experimental]).ToDictionary(l => l.Key, l => l.ToArray());
          if (options.PeptideInAllTimePointOnly && times.Any(l => !curSpectra.ContainsKey(l)))
          {
            continue;
          }

          sw.Write(peptide.Key);

          foreach (var time in times)
          {
            if (curSpectra.ContainsKey(time))
            {
              var deps = (from spec in curSpectra[time] select double.Parse(spec.Annotations["DeuteriumEnrichmentPercent"].ToString())).ToArray();
              var depMedian = Statistics.Median(deps);
              sw.Write("\t{0:0.######}", depMedian);
            }
            else
            {
              sw.Write("\tNA");
            }
          }
          sw.WriteLine();
        }
      }

      Progress.SetMessage("Peptide deuterium enrichment calculation finished ...");

      return new string[] { options.OutputFile };
    }

    public string GetPeptideDeteriumFile()
    {
      return Path.ChangeExtension(options.OutputFile, ".individual.tsv");
    }
  }
}
