using RCPA.Proteomics.Spectrum;

namespace RCPA.Proteomics.Fragmentation
{
  public class CIDPeptideYSeriesBuilder<T> : AbstractPeptideYSeriesBuilder<T> where T : IIonTypePeak, new()
  {
    protected override double GetTerminalMass()
    {
      return CtermMass + 2 * MassH;
    }

    protected override int IonCharge
    {
      get
      {
        return 1;
      }
    }

    public override IonType SeriesType { get { return IonType.Y; } }
  }
}
