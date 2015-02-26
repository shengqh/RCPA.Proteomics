using RCPA.Proteomics.Summary;

namespace RCPA.Proteomics.Mascot
{
  public class MascotResultTextFormat : AbstractIdentifiedResultSingleFileTextFormat
  {
    public MascotResultTextFormat() { }

    public MascotResultTextFormat(string proteinHeaders, string peptideHeaders)
      : base(proteinHeaders, peptideHeaders)
    { }

    protected override IIdentifiedResult Allocate()
    {
      return new MascotResult();
    }

    protected override string GetDefaultProteinHeader()
    {
      return MascotHeader.MASCOT_PROTEIN_HEADER;
    }

    protected override string GetDefaultPeptideHeader()
    {
      return MascotHeader.MASCOT_PEPTIDE_HEADER;
    }

    protected override string GetEngineName()
    {
      return "mascot";
    }
  }
}