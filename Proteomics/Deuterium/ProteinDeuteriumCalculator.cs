using MathNet.Numerics.Statistics;
using RCPA.Proteomics.Mascot;
using RCPA.R;
using RCPA.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace RCPA.Proteomics.Deuterium
{
  public class ProteinDeuteriumCalculator : AbstractThreadProcessor
  {
    private DeuteriumCalculatorOptions options;
    public ProteinDeuteriumCalculator(DeuteriumCalculatorOptions options)
    {
      this.options = options;
    }

    private static string RatioR = Path.Combine(FileUtils.GetTemplateDir().FullName, "DeuteriumKinetic.r");

    public override IEnumerable<string> Process()
    {
      //Prepare unique peptide file
      var format = new MascotResultTextFormat();
      var proteins = format.ReadFromFile(options.InputFile);
      proteins.RemoveAmbiguousSpectra();

      var spectra = proteins.GetSpectra();

      foreach (var spec in spectra)
      {
        spec.Annotations.Remove("TheoreticalDeuterium");
        spec.Annotations.Remove("ObservedDeuterium");
        spec.Annotations.Remove("NumDeuteriumIncorporated");
        spec.Annotations.Remove("NumExchangableHydrogen");
        spec.Annotations.Remove("DeuteriumEnrichmentPercent");
      }

      var peptideFile = Path.ChangeExtension(options.InputFile, ".unique.peptides");
      var peptideFormat = new MascotPeptideTextFormat(format.PeptideFormat.Headers);
      peptideFormat.WriteToFile(peptideFile, spectra);

      //Calculate deterium enrichment at peptide level
      var pepOptions = new DeuteriumCalculatorOptions();
      options.CopyProperties(pepOptions);
      pepOptions.InputFile = peptideFile;
      pepOptions.OutputFile = peptideFile + ".tsv";

      var pepCalc = new PeptideDeuteriumCalculator(pepOptions);
      pepCalc.Progress = this.Progress;
      pepCalc.Process();

      //Copy annotation from calculated peptide to original peptide
      var calcSpectra = peptideFormat.ReadFromFile(pepCalc.GetPeptideDeteriumFile());
      var oldSpectraMap = spectra.ToDictionary(m => m.Query.FileScan.LongFileName);
      foreach (var calcSpec in calcSpectra)
      {
        var oldSpec = oldSpectraMap[calcSpec.Query.FileScan.LongFileName];
        foreach (var ann in calcSpec.Annotations)
        {
          oldSpec.Annotations[ann.Key] = ann.Value;
        }
      }

      //Remove the peptide not contain calculation result
      for (int i = proteins.Count - 1; i >= 0; i--)
      {
        foreach (var protein in proteins[i])
        {
          protein.Peptides.RemoveAll(l => !l.Spectrum.Annotations.ContainsKey("DeuteriumEnrichmentPercent"));
        }

        if (proteins[i][0].Peptides.Count == 0)
        {
          proteins.RemoveAt(i);
        }
      }

      format.PeptideFormat = peptideFormat.PeptideFormat;

      var noredundantFile = Path.ChangeExtension(options.OutputFile, ".individual.tsv");
      format.WriteToFile(noredundantFile, proteins);

      var times = options.ExperimentalTimeMap.Values.Distinct().OrderBy(m => m).ToArray();
      var timeFile = Path.ChangeExtension(options.OutputFile, ".times.tsv");
      using (var sw = new StreamWriter(timeFile))
      {
        sw.WriteLine("Protein\t{0}", (from t in times select t.ToString()).Merge("\t"));

        foreach (var protein in proteins)
        {
          var curSpectra = protein[0].GetSpectra();
          if (options.PeptideInAllTimePointOnly)
          {
            var curMap = curSpectra.ToGroupDictionary(l => l.Peptide.PureSequence);
            curSpectra.Clear();
            foreach (var peps in curMap.Values)
            {
              var pepMap = peps.ToGroupDictionary(m => options.ExperimentalTimeMap[m.Query.FileScan.Experimental]);
              if (times.All(time => pepMap.ContainsKey(time)))
              {
                curSpectra.AddRange(peps);
              }
            }
          }

          if (curSpectra.Count == 0)
          {
            continue;
          }

          sw.Write((from p in protein select p.Name).Merge("/"));
          var curTimeMap = curSpectra.ToGroupDictionary(m => options.ExperimentalTimeMap[m.Query.FileScan.Experimental]);

          foreach (var time in times)
          {
            if (curTimeMap.ContainsKey(time))
            {
              var deps = (from spec in curTimeMap[time] select double.Parse(spec.Annotations["DeuteriumEnrichmentPercent"].ToString())).ToArray();
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

      Progress.SetMessage("Calculating ratio consistant ...");
      var deuteriumOptions = new RTemplateProcessorOptions()
      {
        InputFile = timeFile,
        OutputFile = options.OutputFile,
        RTemplate = RatioR,
        RExecute = SystemUtils.GetRExecuteLocation(),
        CreateNoWindow = true
      };

      new RTemplateProcessor(deuteriumOptions) { Progress = this.Progress }.Process();

      Progress.SetMessage("Finished ...");

      return new string[] { options.OutputFile };
    }
  }
}
