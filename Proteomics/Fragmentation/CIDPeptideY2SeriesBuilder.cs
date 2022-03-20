using RCPA.Proteomics.Spectrum;

namespace RCPA.Proteomics.Fragmentation
{
  public class CIDPeptideY2SeriesBuilder<T> : AbstractPeptideYSeriesBuilder<T> where T : IIonTypePeak, new()
  {
    protected override double GetTerminalMass()
    {
      return CtermMass + 3 * MassH;
    }

    protected override int IonCharge
    {
      get
      {
        return 2;
      }
    }

    public override IonType SeriesType { get { return IonType.Y2; } }
  }
}
