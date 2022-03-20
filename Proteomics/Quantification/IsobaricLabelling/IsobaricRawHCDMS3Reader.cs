using RCPA.Proteomics.Raw;

namespace RCPA.Proteomics.Quantification.IsobaricLabelling
{
  /// <summary>
  /// MS1->MS2->MS3->MS2->MS3
  /// </summary>
  public class IsobaricRawHCDMS3Reader : AbstractIsobaricRawHCDMS3Reader
  {
    public IsobaricRawHCDMS3Reader() : base("HCD-MS3") { }

    protected override IScanLevelBuilder GetScanLevelBuilder()
    {
      return new ScanLevelBuilder();
    }

    public override string ToString()
    {
      return Name + " : MS1->MS2->MS3->MS2->MS3";
    }
  }
}
