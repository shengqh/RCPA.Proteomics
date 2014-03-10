using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RCPA.Proteomics.Summary;
using RCPA.Utils;
using System.IO;

namespace RCPA.Proteomics.Quantification.O18
{
  public class AbstractO18QuantificationOption : IO18QuantificationOption
  {
    private IProteinRatioCalculator calc;

    protected IGetRatioIntensity func;
    protected O18QuantificationSummaryItemXmlFormat format = new O18QuantificationSummaryItemXmlFormat();

    public AbstractO18QuantificationOption()
    {
      this.func = new O18GetRatioIntensity();
      this.calc = new O18ProteinRatioRPeptideCalculator(this.func, this);
    }

    public virtual IFileFormat<O18QuantificationSummaryItem> GetIndividualFileFormat()
    {
      return format;
    }

    public IGetRatioIntensity Func
    {
      get { return func; }
    }

    public virtual IProteinRatioCalculator GetProteinRatioCalculator()
    {
      return calc;
    }

    public bool HasPeptideRatio(IIdentifiedSpectrum ann)
    {
      return GetProteinRatioCalculator().HasPeptideRatio(ann);
    }

    public bool IsPeptideRatioValid(IIdentifiedSpectrum ann)
    {
      return HasPeptideRatio(ann) && AnnotationUtils.IsEnabled(ann, true);
    }

    private bool IsRelativeDir(string ratioFile)
    {
      return ratioFile.Contains("\\") || ratioFile.Contains("/");
    }

    public string GetRatioFile(Summary.IIdentifiedSpectrum mph, string summaryFileDirectory, string defaultDetailDirectory)
    {
      string ratioFile = (string)mph.Annotations[O18QuantificationConstants.O18_RATIO_FILE];
      if (ratioFile.Equals("-"))
      {
        return null;
      }

      string result;
      if (IsRelativeDir(ratioFile))
      {
        result = summaryFileDirectory + "/" + ratioFile;
      }
      else
      {
        result = summaryFileDirectory + "/" + defaultDetailDirectory + "/" + ratioFile;
      }

      result = new FileInfo(result).FullName;

      if (!File.Exists(result))
      {
        return null;
      }

      return result;
    }
  }
}
