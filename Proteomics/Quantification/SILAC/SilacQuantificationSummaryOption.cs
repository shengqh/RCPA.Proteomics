using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RCPA.Proteomics.Summary;
using System.IO;

namespace RCPA.Proteomics.Quantification.SILAC
{
  public class SilacQuantificationSummaryOption : IQuantificationSummaryOption
  {
    private SilacGetRatioIntensity func = new SilacGetRatioIntensity();

    private SilacQuantificationSummaryItemXmlFormat format = new SilacQuantificationSummaryItemXmlFormat();

    public double HighlightR2 { get; set; }

    public SilacQuantificationSummaryOption()
    {
      HighlightR2 = 0.9;
    }

    private bool IsOutlier(IAnnotation ann)
    {
      var item = ann.GetQuantificationItem();

      if (null == item || !item.HasRatio)
      {
        return false;
      }

      return item.Correlation < this.HighlightR2;
    }

    #region IQuantificationSummaryOption Members

    public IProteinRatioCalculator GetProteinRatioCalculator()
    {
      return new SilacProteinRatioCalculator();
    }

    public object ReadRatioFile(string file)
    {
      return format.ReadFromFile(file);
    }

    public bool IsPeptideRatioValid(IIdentifiedSpectrum ann)
    {
      return ann.GetRatioEnabled();
    }

    public bool IsProteinRatioValid(IIdentifiedProtein ann)
    {
      return ann.GetRatioEnabled();
    }

    public bool IsPeptideOutlier(IIdentifiedSpectrum ann)
    {
      return IsOutlier(ann);
    }

    public bool IsProteinOutlier(IIdentifiedProtein ann)
    {
      return IsOutlier(ann);
    }

    public IQuantificationPeptideForm CreateForm()
    {
      return new SilacQuantificationSummaryItemForm();
    }

    public IGetRatioIntensity Func
    {
      get { return func; }
    }

    public string RatioFileKey
    {
      get { return QuantificationItem.KEY_FILE; }
    }

    public double GetPeptideRatio(IIdentifiedSpectrum ann)
    {
      return func.GetRatio(ann);
    }

    public double GetProteinRatio(IIdentifiedProtein ann)
    {
      return func.GetRatio(ann);
    }

    public string GetProteinRatioDescription(IIdentifiedProtein ann)
    {
      var corr = ann.GetQuantificationItem().Correlation;
      return MyConvert.Format("Ratio = {0:0.0000}; Correlation = {1:0.0000}", GetProteinRatio(ann), corr);
    }

    #endregion

    #region IQuantificationSummaryOption Members


    public bool HasPeptideRatio(IIdentifiedSpectrum ann)
    {
      return ann.HasRatio();
    }

    public bool HasProteinRatio(IIdentifiedProtein ann)
    {
      return ann.HasRatio();
    }

    #endregion

    #region IQuantificationSummaryOption Members


    public void SetPeptideRatioValid(IIdentifiedSpectrum ann, bool value)
    {
      ann.SetRatioEnabled(value);
    }

    public void SetProteinRatioValid(IIdentifiedProtein ann, bool value)
    {
      ann.SetRatioEnabled(value);
    }

    #endregion
  }
}
