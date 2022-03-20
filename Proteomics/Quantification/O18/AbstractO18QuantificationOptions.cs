using RCPA.Proteomics.Summary;
using System;
using System.IO;

namespace RCPA.Proteomics.Quantification.O18
{
  public abstract class AbstractO18QuantificationOptions : IO18QuantificationOptions
  {
    private IProteinRatioCalculator calc;

    protected IGetRatioIntensity func;
    protected O18QuantificationSummaryItemXmlFormat format = new O18QuantificationSummaryItemXmlFormat();

    public AbstractO18QuantificationOptions()
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
      calc.DetailDirectory = GetDetailDirectory();
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

    public string GetRatioFile(Summary.IIdentifiedSpectrum mph)
    {
      string ratioFile = (string)mph.Annotations[O18QuantificationConstants.O18_RATIO_FILE];
      if (ratioFile.Equals("-"))
      {
        return null;
      }

      string result;
      if (IsRelativeDir(ratioFile))
      {
        result = GetSummaryDirectory() + "/" + ratioFile;
      }
      else
      {
        result = GetDetailDirectory() + "/" + ratioFile;
      }

      result = new FileInfo(result).FullName;

      if (!File.Exists(result))
      {
        return null;
      }

      return result;
    }

    public string GetSummaryDirectory()
    {
      return new FileInfo(this.SummaryFile).DirectoryName;
    }

    public string GetDetailDirectory()
    {
      return FileUtils.ChangeExtension(this.SummaryFile, ".details");
    }

    public abstract String SummaryFile { get; }
  }
}
