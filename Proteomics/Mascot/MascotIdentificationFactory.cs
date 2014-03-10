using RCPA.Proteomics.Summary;

namespace RCPA.Proteomics.Mascot
{
  public class MascotIdentificationFactory : IdentificationFactory
  {
    public override IIdentifiedResult AllocateResult()
    {
      return new MascotResult();
    }
  }
}