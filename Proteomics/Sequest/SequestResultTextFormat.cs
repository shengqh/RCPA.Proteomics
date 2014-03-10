using RCPA.Proteomics.Summary;

namespace RCPA.Proteomics.Sequest
{
  public class SequestResultTextFormat : AbstractIdentifiedResultSingleFileTextFormat
  {
    public SequestResultTextFormat()
    {
    }

    public SequestResultTextFormat(string proteinHeader, string peptideHeader) : base(proteinHeader, peptideHeader)
    {
    }

    protected override IIdentifiedResult Allocate()
    {
      return new IdentifiedResult();
    }

    protected override string GetDefaultProteinHeader()
    {
      return SequestHeader.SEQUEST_PROTEIN_HEADER;
    }

    protected override string GetDefaultPeptideHeader()
    {
      return SequestHeader.SEQUEST_PEPTIDE_HEADER;
    }

    protected override string GetEngineName()
    {
      return "sequest";
    }
  }
}