using RCPA.Proteomics.Summary;

namespace RCPA.Proteomics.PropertyConverter
{
  public class IdentifiedSpectrumObservedMzConverter<T> : AbstractPropertyConverter<T> where T : IIdentifiedSpectrum
  {
    public override string Name
    {
      get { return "Obs"; }
    }

    public override string GetProperty(T t)
    {
      if (t.Query.ObservedMz == 0.0)
      {
        double massH = t.IsPrecursorMonoisotopic ? Atom.H.MonoMass : Atom.H.AverageMass;
        t.Query.ObservedMz = (t.ExperimentalMass + massH * t.Query.Charge) / t.Query.Charge;
      }

      return MyConvert.Format("{0:0.00000}", t.Query.ObservedMz);
    }

    public override void SetProperty(T t, string value)
    {
      t.Query.ObservedMz = MyConvert.ToDouble(value);
      if (t.Query.Charge != 0)
      {
        double massH = t.IsPrecursorMonoisotopic ? Atom.H.MonoMass : Atom.H.AverageMass;
        t.ExperimentalMass = t.Query.ObservedMz * t.Query.Charge - massH * t.Query.Charge;
      }
    }
  }
}