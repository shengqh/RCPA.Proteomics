using RCPA.Proteomics.Summary;

namespace RCPA.Proteomics.PropertyConverter
{
  //Before using SetProperty of TheoreticalMinusExperimentMassIO, the theoretical or experimental mass
  //of MascotPeptideHit should already be assigned
  public class IdentifiedSpectrumExperimentalMinusTheoreticalMassConverter<T> : AbstractPropertyConverter<T>
    where T : IIdentifiedSpectrum
  {
    public override string Name
    {
      get { return "DeltaMH"; }
    }

    public override string GetProperty(T t)
    {
      return MyConvert.Format("{0:0.00000}", -t.TheoreticalMinusExperimentalMass);
    }

    public override void SetProperty(T t, string value)
    {
      double diff = MyConvert.ToDouble(value);
      if (t.TheoreticalMass != 0.0)
      {
        t.ExperimentalMass = t.TheoreticalMass + diff;
      }
      else if (t.ExperimentalMass != 0.0)
      {
        t.TheoreticalMass = t.ExperimentalMass - diff;
      }
    }
  }
}