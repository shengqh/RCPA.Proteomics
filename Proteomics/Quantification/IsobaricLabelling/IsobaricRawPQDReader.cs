namespace RCPA.Proteomics.Quantification.IsobaricLabelling
{
  public class IsobaricRawPQDReader : AbstractIsobaricRawReader
  {
    public IsobaricRawPQDReader() : base(2, "PQD") { }

    protected override string[] GetScanMode()
    {
      return new string[] { "PQD" };
    }

    public override string ToString()
    {
      return this.Name + " : MS1->PQD->PQD";
    }
  }
}
