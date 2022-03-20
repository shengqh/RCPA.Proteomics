using RCPA.Proteomics.Spectrum;
using System.Collections.Generic;

namespace RCPA.Proteomics.Fragmentation
{
  public class ETDPeptideCSeriesBuilder<T> : AbstractETDPeptideBuilder<T> where T : IIonTypePeak, new()
  {
    public ETDPeptideCSeriesBuilder(double maxMz, int precursorCharge)
      : base(maxMz, precursorCharge)
    { }

    public override IonType SeriesType
    {
      get { return IonType.C; }
    }

    protected override double GetTerminalMass()
    {
      return NtermMass + MassNH3;
    }

    protected override List<AminoacidInfo> GetAminoacidInfos(string sequence)
    {
      return GetForwardAminoacidInfos(sequence);
    }
  }
}