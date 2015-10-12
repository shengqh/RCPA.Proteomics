using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RCPA.Proteomics.Summary;
using RCPA.Utils;
using System.IO;

namespace RCPA.Proteomics.Quantification.O18
{
  public class O18QuantificationSummaryViewerOptions : AbstractO18QuantificationOptions, IQuantificationSummaryOption
  {
    public double MinimumPeptideRSquare { get; set; }
    public double MinimumProteinRSquare { get; set; }

    private string _summaryFile;
    public override string SummaryFile
    {
      get
      {
        return _summaryFile;
      }
    }

    public O18QuantificationSummaryViewerOptions(string summaryFile, double minimumRSquare = 0.9 )
    {
      this._summaryFile = summaryFile;
      this.MinimumPeptideRSquare = minimumRSquare;
      this.MinimumProteinRSquare = minimumRSquare;
    }

    public object ReadRatioFile(string file)
    {
      return format.ReadFromFile(file);
    }

    public bool IsProteinRatioValid(IIdentifiedProtein ann)
    {
      return HasProteinRatio(ann) && AnnotationUtils.IsEnabled(ann, true);
    }

    public bool IsPeptideOutlier(IIdentifiedSpectrum ann)
    {
      return false;
    }

    public bool IsProteinOutlier(IIdentifiedProtein ann)
    {
      if(GetProteinRatioCalculator().HasProteinRatio(ann))
      {
        return false;
      }

      if (!ann.Annotations.ContainsKey(O18QuantificationConstants.O18_RATIO))
      {
        return false;
      }

      LinearRegressionRatioResult lrrr = ann.Annotations[O18QuantificationConstants.O18_RATIO] as LinearRegressionRatioResult;

      return lrrr.RSquare < this.MinimumProteinRSquare;
    }

    public IQuantificationPeptideForm CreateForm()
    {
      return new O18QuantificationSummaryItemForm();
    }

    public string RatioFileKey
    {
      get { return O18QuantificationConstants.O18_RATIO_FILE; }
    }

    public double GetPeptideRatio(IIdentifiedSpectrum ann)
    {
      return func.GetRatio(ann);
    }

    public double GetProteinRatio(IIdentifiedProtein ann)
    {
      return GetProteinRatioCalculator().GetProteinRatio(ann);
    }

    public string GetProteinRatioDescription(IIdentifiedProtein ann)
    {
      LinearRegressionRatioResult lrrr = ann.Annotations[O18QuantificationConstants.O18_RATIO] as LinearRegressionRatioResult;
      return MyConvert.Format("Ratio={0:0.00}; StdErr={1:0.0000}; pValue={2:0.##E-000}", lrrr.Ratio, lrrr.RSquare, lrrr.PValue);
    }

    public string GetPeptideClassification(IIdentifiedSpectrum ann)
    {
      return "O18/O16";
    }

    public double GetProteinRatio(IIdentifiedProtein ann, string key)
    {
      return GetProteinRatio(ann);
    }

    public string GetProteinRatioDescription(IIdentifiedProtein ann, string key)
    {
      return GetProteinRatioDescription(ann);
    }

    public bool HasProteinRatio(IIdentifiedProtein ann)
    {
      return GetProteinRatioCalculator().HasProteinRatio(ann);
    }

    public void SetPeptideRatioValid(IIdentifiedSpectrum ann, bool value)
    {
      ann.SetEnabled(value);
    }

    public void SetProteinRatioValid(IIdentifiedProtein ann, bool value)
    {
      ann.SetEnabled(value);
    }
  }
}
