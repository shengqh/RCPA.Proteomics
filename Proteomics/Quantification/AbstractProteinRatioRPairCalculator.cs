using RCPA.Proteomics.Summary;
using RCPA.R;
using RCPA.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace RCPA.Proteomics.Quantification
{
  public abstract class AbstractProteinRatioRPairCalculator : IProteinRatioCalculator
  {
    class WaitingEntry
    {
      public IIdentifiedProteinGroup Group { get; set; }
      public string IntensityFile { get; set; }
    }

    protected IGetRatioIntensity intensityFunc;

    public IPairQuantificationOptions Option { get; set; }

    public AbstractProteinRatioRPairCalculator(IGetRatioIntensity intensityFunc, IPairQuantificationOptions option)
    {
      this.intensityFunc = intensityFunc;
      this.Option = option;
    }

    public IGetRatioIntensity IntensityFunc
    {
      get { return this.intensityFunc; }
    }

    private static double ParseDouble(string value, double valueForNA)
    {
      if (value.Equals("NA"))
      {
        return valueForNA;
      }
      return double.Parse(value);
    }

    public void Calculate(IIdentifiedResult mr, Func<IIdentifiedSpectrum, bool> validFunc)
    {
      var proteinFiles = new List<WaitingEntry>();
      foreach (var mpg in mr)
      {
        var pf = DoCalculate(mpg, validFunc, false);
        if (pf != null)
        {
          proteinFiles.Add(pf);
        }
      }

      if (proteinFiles.Count > 0)
      {
        var listfile = (this.DetailDirectory + "/rlm_file.csv").Replace("\\", "/");
        using (var sw = new StreamWriter(listfile))
        {
          sw.WriteLine("Protein,IntensityFile");
          foreach (var we in proteinFiles)
          {
            sw.WriteLine("\"{0}\",\"{1}\"", we.Group[0].Name, we.IntensityFile);
          }
        }

        var linearfile = new FileInfo(this.DetailDirectory + "/rlm.linear").FullName.Replace("\\", "/");

        var roptions = new RTemplateProcessorOptions();

        roptions.InputFile = listfile;
        roptions.OutputFile = linearfile;
        roptions.RTemplate = FileUtils.GetTemplateDir() + "/MultiplePairQuantification.r";

        new RTemplateProcessor(roptions).Process();

        var results = (from line in File.ReadAllLines(linearfile).Skip(1)
                       let parts = line.Split('\t')
                       select new
                       {
                         ProteinName = parts[0].StringAfter("\"").StringBefore("\""),
                         LinearRegressionResult = ParseLinearRegressionRatioResult(parts, 2)
                       }).ToDictionary(m => m.ProteinName);

        foreach (var pg in mr)
        {
          if (results.ContainsKey(pg[0].Name))
          {
            var res = results[pg[0].Name];
            var lrrr = res.LinearRegressionResult;
            foreach (IIdentifiedProtein protein in pg)
            {
              this.intensityFunc.SaveToAnnotation(protein, lrrr);
            }
          }
        }
      }
    }

    public void Calculate(IIdentifiedProteinGroup proteinGroup, Func<IIdentifiedSpectrum, bool> validFunc)
    {
      DoCalculate(proteinGroup, validFunc, true);
    }

    private WaitingEntry DoCalculate(IIdentifiedProteinGroup proteinGroup, Func<IIdentifiedSpectrum, bool> validFunc, bool runRImmediately)
    {
      List<IIdentifiedSpectrum> spectra = (from s in proteinGroup[0].GetSpectra()
                                           where validFunc(s) && s.IsEnabled(true) && HasPeptideRatio(s)
                                           select s).ToList();

      if (spectra.Count == 1)
      {
        var lrrr = new LinearRegressionRatioResult(CalculatePeptideRatio(spectra[0]), 0.0)
        {
          PointCount = 1,
          TValue = 0,
          PValue = 1,
          ReferenceIntensity = this.intensityFunc.GetReferenceIntensity(spectra[0]),
          SampleIntensity = this.intensityFunc.GetSampleIntensity(spectra[0])
        };

        var r = CalculatePeptideRatio(spectra[0]);
        foreach (var protein in proteinGroup)
        {
          this.intensityFunc.SaveToAnnotation(protein, lrrr);
        }
        return null;
      }
      else if (spectra.Count > 1)
      {
        var intensities = this.intensityFunc.ConvertToArray(spectra);

        double sumSam = intensities[0].Max();
        double sumRef = intensities[1].Max();

        LinearRegressionRatioResult lrrr;

        if (sumSam == 0.0)
        {
          lrrr = new LinearRegressionRatioResult(20, 0.0)
          {
            PointCount = intensities.Count(),
            TValue = 0,
            PValue = 0,
            ReferenceIntensity = sumRef,
          };
          lrrr.SampleIntensity = sumRef / lrrr.Ratio;
        }
        else
        {
          if (sumRef == 0.0)
          {
            lrrr = new LinearRegressionRatioResult(0.05, 0.0)
            {
              PointCount = intensities.Count(),
              TValue = 0,
              PValue = 0,
              SampleIntensity = sumSam
            };
            lrrr.ReferenceIntensity = sumSam * lrrr.Ratio;
          }
          else
          {
            var filename = (this.DetailDirectory + "/" + proteinGroup[0].Name.Replace("|", "_") + ".csv").Replace("\\", "/");

            PrepareIntensityFile(spectra, filename);

            if (!runRImmediately)
            {
              return new WaitingEntry()
              {
                Group = proteinGroup,
                IntensityFile = filename
              };
            }

            var linearfile = filename + ".linear";

            var roptions = new RTemplateProcessorOptions();

            roptions.InputFile = filename;
            roptions.OutputFile = linearfile;
            roptions.RTemplate = FileUtils.GetTemplateDir() + "/PairQuantification.r";

            new RTemplateProcessor(roptions).Process();

            var parts = File.ReadAllLines(linearfile).Skip(1).First().Split('\t');

            lrrr = ParseLinearRegressionRatioResult(parts, 0);
          }
        }

        foreach (IIdentifiedProtein protein in proteinGroup)
        {
          this.intensityFunc.SaveToAnnotation(protein, lrrr);
        }
      }
      else
      {
        foreach (IIdentifiedProtein protein in proteinGroup)
        {
          this.intensityFunc.RemoveFromAnnotation(protein);
        }
      }
      return null;
    }

    private static LinearRegressionRatioResult ParseLinearRegressionRatioResult(string[] parts, int startIndex)
    {
      LinearRegressionRatioResult result;

      result = new LinearRegressionRatioResult();
      result.ReferenceIntensity = double.Parse(parts[startIndex]);
      result.SampleIntensity = double.Parse(parts[startIndex + 1]);
      result.Ratio = double.Parse(parts[startIndex + 2]);
      result.Stdev = parts[startIndex + 3].Equals("NA") ? 0 : double.Parse(parts[startIndex + 3]);
      //lrrr.RSquare = double.Parse(parts[2]);
      result.TValue = parts[startIndex + 4].Equals("NA") ? 0 : ParseDouble(parts[startIndex + 4], 0);
      result.PValue = parts[startIndex + 5].Equals("NA") ? 1 : ParseDouble(parts[startIndex + 5], 0);
      result.PointCount = int.Parse(parts[startIndex + 6]);
      if (result.ReferenceIntensity > result.SampleIntensity)
      {
        result.SampleIntensity = result.ReferenceIntensity * result.Ratio;
      }
      else
      {
        result.ReferenceIntensity = result.SampleIntensity / result.Ratio;
      }

      return result;
    }

    protected abstract void PrepareIntensityFile(List<IIdentifiedSpectrum> spectra, string filename);

    public double CalculatePeptideRatio(IIdentifiedSpectrum mph)
    {
      double refIntensity = this.intensityFunc.GetReferenceIntensity(mph);
      double samIntensity = this.intensityFunc.GetSampleIntensity(mph);

      double result = 0;
      if (refIntensity == 0)
      {
        result = 20;
      }
      else if (samIntensity == 0)
      {
        result = 0.05;
      }
      else
      {
        result = samIntensity / refIntensity;
      }

      result = Math.Min(20, Math.Max(result, 0.05));

      return result;
    }

    public bool HasPeptideRatio(IIdentifiedSpectrum spectrum)
    {
      return intensityFunc.HasRatio(spectrum);
    }

    public bool HasProteinRatio(IIdentifiedProtein protein)
    {
      return intensityFunc.HasRatio(protein);
      //if (!protein.Annotations.ContainsKey(this.intensityFunc.RatioKey))
      //{
      //  return false;
      //}

      //return protein.Annotations[this.intensityFunc.RatioKey] is LinearRegressionRatioResult;
    }

    public double GetProteinRatio(IIdentifiedProtein protein)
    {
      return intensityFunc.GetRatio(protein);
      //if (HasProteinRatio(protein))
      //{
      //  return (protein.Annotations[this.intensityFunc.RatioKey] as LinearRegressionRatioResult).Ratio;
      //}
      //else
      //{
      //  return double.NaN;
      //}
    }

    public string SummaryFileDirectory { get; set; }

    public string DetailDirectory { get; set; }
  }
}