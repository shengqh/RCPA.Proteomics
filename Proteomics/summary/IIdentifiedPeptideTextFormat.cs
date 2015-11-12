using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RCPA.Utils;

namespace RCPA.Proteomics.Summary
{
  public delegate IIdentifiedSpectrum AllocateSpectrum();

  public interface IIdentifiedPeptideTextFormat : IFileFormat<List<IIdentifiedSpectrum>>
  {
    void Initialize(List<IIdentifiedSpectrum> spectra);

    bool NotExportSummary { get; set; }
  }

  public abstract class AbstractPeptideTextFormat : IIdentifiedPeptideTextFormat
  {
    public LineFormat<IIdentifiedSpectrum> PeptideFormat { get; set; }

    public Func<IIdentifiedSpectrum, bool> ValidSpectrum { get; set; }

    public AllocateSpectrum Allocate { get; set; }

    public bool NotExportSummary { get; set; }

    public AbstractPeptideTextFormat()
    {
      this.ValidSpectrum = (m => true);
    }

    public AbstractPeptideTextFormat(string peptideHeaders)
      : this()
    {
      this.PeptideFormat = new PeptideLineFormat(peptideHeaders, GetEngineName());
    }

    public AbstractPeptideTextFormat(string peptideHeaders, List<IIdentifiedSpectrum> spectra)
      : this()
    {
      this.PeptideFormat = new PeptideLineFormat(peptideHeaders, GetEngineName(), spectra);
    }

    protected abstract string GetDefaultPeptideHeader();

    protected abstract string GetEngineName();

    protected virtual bool IsValidSpectrum(IIdentifiedSpectrum spectrum)
    {
      return ValidSpectrum == null ? true : ValidSpectrum(spectrum);
    }

    protected void CheckFormat(List<IIdentifiedSpectrum> spectra)
    {
      if (PeptideFormat == null)
      {
        Initialize(spectra);
      }
    }

    #region IIdentifiedPeptideTextFormat Members

    public void Initialize(List<IIdentifiedSpectrum> spectra)
    {
      string oldPeptideHeader = PeptideFormat == null ? GetDefaultPeptideHeader() : PeptideFormat.GetHeader();
      List<string> pepAnnotations = AnnotationUtils.GetAnnotationKeys(spectra);
      string newPeptideHeader = StringUtils.GetMergedHeader(oldPeptideHeader, pepAnnotations, '\t');
      PeptideFormat = new PeptideLineFormat(newPeptideHeader, GetEngineName(), spectra);
    }

    #endregion

    #region IFileReader<List<IIdentifiedSpectrum>> Members

    public abstract List<IIdentifiedSpectrum> ReadFromFile(string fileName);

    #endregion

    #region IFileWriter<List<IIdentifiedSpectrum>> Members

    public abstract void WriteToFile(string fileName, List<IIdentifiedSpectrum> t);

    #endregion
  }
}
