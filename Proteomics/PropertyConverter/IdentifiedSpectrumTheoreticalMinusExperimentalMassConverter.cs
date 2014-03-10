using RCPA.Proteomics.Summary;

namespace RCPA.Proteomics.PropertyConverter
{
  //Before using SetProperty of TheoreticalMinusExperimentMassIO, the theoretical mass
  //of MascotPeptideHit should already be assigned
  public class IdentifiedSpectrumTheoreticalMinusExperimentalMassConverter<T> : AbstractPropertyConverter<T>
    where T : IIdentifiedSpectrum
  {
    public IdentifiedSpectrumTheoreticalMinusExperimentalMassConverter(string format = "{0:0.00000}")
    {
      this._format = format;
    }

    private string _format;

    public override string Name
    {
      get { return "Diff(MH+)"; }
    }

    public override string GetProperty(T t)
    {
      return MyConvert.Format(this._format, t.TheoreticalMinusExperimentalMass);
    }

    public override void SetProperty(T t, string value)
    {
      double diff = MyConvert.ToDouble(value);
      if (t.TheoreticalMass != 0.0)
      {
        t.ExperimentalMass = t.TheoreticalMass - diff;
      }
      else if (t.ExperimentalMass != 0.0)
      {
        t.TheoreticalMass = t.ExperimentalMass + diff;
      }
    }
  }

  public class IdentifiedSpectrumTheoreticalMinusExperimentalMassPPMConverter<T> : AbstractPropertyConverter<T>
    where T : IIdentifiedSpectrum
  {
    public override string Name
    {
      get { return "DiffPPM"; }
    }

    public override string GetProperty(T t)
    {
      return MyConvert.Format("{0:0.00}", PrecursorUtils.mz2ppm(t.TheoreticalMass, t.TheoreticalMinusExperimentalMass));
    }

    public override void SetProperty(T t, string value)
    { }
  }
}