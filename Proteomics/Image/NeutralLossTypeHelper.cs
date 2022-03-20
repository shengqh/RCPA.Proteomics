namespace RCPA.Proteomics.Image
{
  public static class NeutralLossTypeHelper
  {
    public static bool IsPhosphoNeutralLossType(this INeutralLossType aType)
    {
      return aType.Name.Contains("PO");
    }
  }
}
