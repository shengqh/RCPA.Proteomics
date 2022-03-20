namespace RCPA.Proteomics.Quantification.IsobaricLabelling
{
  public class IsobaricResultMultipleFileDistillerOptions : AbstractIsobaricResultFileDistillerOptions
  {
    public string[] RawFiles { get; set; }

    public bool Individual { get; set; }
  }
}
