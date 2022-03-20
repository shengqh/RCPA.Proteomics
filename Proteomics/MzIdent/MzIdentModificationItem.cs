namespace RCPA.Proteomics.MzIdent
{
  public class MzIdentModificationItem
  {
    public int Location { get; set; }

    public MzIdentModificationDefinitionItem Item { get; set; }

    public override string ToString()
    {
      return string.Format("{0} at {1}", Item.Name, Location);
    }
  }
}
