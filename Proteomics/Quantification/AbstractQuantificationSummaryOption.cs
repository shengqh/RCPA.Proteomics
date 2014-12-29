using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RCPA.Proteomics.Summary;
using System.IO;

namespace RCPA.Proteomics.Quantification
{
  public abstract class AbstractQuantificationSummaryOption : IQuantificationSummaryOption
  {
    public double MinimumRSquare { get; set; }

    public AbstractQuantificationSummaryOption()
    {
      MinimumRSquare = 0.9;
    }

    private bool IsOutlier(IAnnotation ann)
    {
      var item = ann.GetQuantificationItem();

      if (null == item || !item.HasRatio)
      {
        return false;
      }

      return item.Correlation < this.MinimumRSquare;
    }

    #region IQuantificationSummaryOption Members

    public abstract IProteinRatioCalculator GetProteinRatioCalculator();

    public abstract object ReadRatioFile(string file);

    public virtual bool IsPeptideRatioValid(IIdentifiedSpectrum ann)
    {
      return ann.GetRatioEnabled();
    }

    public virtual bool IsProteinRatioValid(IIdentifiedProtein ann)
    {
      return ann.GetRatioEnabled();
    }

    public virtual bool IsPeptideOutlier(IIdentifiedSpectrum ann)
    {
      return IsOutlier(ann);
    }

    public virtual bool IsProteinOutlier(IIdentifiedProtein ann)
    {
      return IsOutlier(ann);
    }

    public abstract IQuantificationPeptideForm CreateForm();

    public abstract IGetRatioIntensity Func { get; }

    public virtual string RatioFileKey
    {
      get { return QuantificationItem.KEY_FILE; }
    }

    public virtual double GetPeptideRatio(IIdentifiedSpectrum ann)
    {
      return Func.GetRatio(ann);
    }

    public virtual double GetProteinRatio(IIdentifiedProtein ann)
    {
      return Func.GetRatio(ann);
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
