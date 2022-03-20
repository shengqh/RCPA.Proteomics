using RCPA.Proteomics.Spectrum;
using System.Collections.Generic;

namespace RCPA.Proteomics.Fragmentation
{
  public class ETDPeptideZSeriesBuilder<T> : AbstractETDPeptideBuilder<T> where T : IIonTypePeak, new()
  {
    public ETDPeptideZSeriesBuilder(double maxMz, int precursorCharge)
      : base(maxMz, precursorCharge)
    { }

    public override IonType SeriesType
    {
      get { return IonType.Z; }
    }

    protected override double GetTerminalMass()
    {
      return CtermMass + 3 * MassH - MassNH3;
    }

    protected override List<AminoacidInfo> GetAminoacidInfos(string sequence)
    {
      return GetReverseAminoacidInfos(sequence);
    }
  }
}
