using RCPA.Proteomics.Mascot;
using RCPA.Proteomics.Summary;
using System.IO;

namespace RCPA.Proteomics.Quantification
{
  public class QuantificationResultMultipleFileFormat : AbstractIdentifiedResultMuitlpleFileTextFormat, IScanWriter
  {
    public QuantificationResultMultipleFileFormat()
    { }

    private string defaultPeptideExtension = string.Empty;

    public QuantificationResultMultipleFileFormat(string defaultPeptideExtension)
    {
      this.defaultPeptideExtension = defaultPeptideExtension;
    }

    protected override IIdentifiedResult Allocate()
    {
      return new IdentifiedResult();
    }

    protected override string GetDefaultProteinHeader()
    {
      return MascotHeader.MASCOT_PROTEIN_HEADER;
    }

    protected override string GetDefaultPeptideHeader()
    {
      return "Id" + MascotHeader.MASCOT_PEPTIDE_HEADER;
    }

    protected override string GetEngineName()
    {
      return "mascot";
    }

    #region IScanWriter Members

    public IIdentifiedSpectrumWriter ScanWriter { get; set; }

    #endregion

    protected override void WritePeptide(StreamWriter sw, IIdentifiedSpectrum mph)
    {
      base.WritePeptide(sw, mph);
      if (ScanWriter != null)
      {
        ScanWriter.WriteToStream(sw, mph);
      }
    }

    protected override void WritePeptideHeader(StreamWriter sw)
    {
      base.WritePeptideHeader(sw);
      if (ScanWriter != null)
      {
        sw.WriteLine(ScanWriter.Header);
      }
    }

    protected override string GetPeptideFileName(string fileName)
    {
      if (this.defaultPeptideExtension == string.Empty)
      {
        return base.GetPeptideFileName(fileName);
      }

      return FileUtils.ChangeExtension(fileName, defaultPeptideExtension);
    }
  }
}