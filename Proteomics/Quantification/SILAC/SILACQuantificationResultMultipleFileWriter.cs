using RCPA.Proteomics.Summary;

namespace RCPA.Proteomics.Quantification.SILAC
{
  public class SilacQuantificationResultMultipleFileWriter : AbstractIdentifiedResultMultipleFileWriter
  {
    public SilacQuantificationResultMultipleFileWriter() : base() { }

    public SilacQuantificationResultMultipleFileWriter(string[] perferAccessNumberRegexString) : base(perferAccessNumberRegexString) { }

    protected override string GetProteinHeader()
    {
      return "\tPepCount\tUniquePepCount\tCoverPercent\tMW\tINT_REF\tINT_SAM\tS_CORR\tS_RATIO";
    }

    protected override string GetPeptideHeader()
    {
      return "\tReference\t\"File, Scan(s)\"\tSequence\tINT_REF\tINT_SAM\tS_CORR\tS_FILE\tS_RATIO\tS_SCANS";
    }

    protected override string GetPeptideFileName(string proteinFileName)
    {
      return FileUtils.ChangeExtension(proteinFileName, ".PepSILACQuant");
    }
  }
}