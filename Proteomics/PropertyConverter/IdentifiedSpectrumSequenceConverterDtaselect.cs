using RCPA.Proteomics.Summary;

namespace RCPA.Proteomics.PropertyConverter
{
  public class IdentifiedSpectrumSequenceConverterDtaselect<T> : AbstractPropertyConverter<T>
    where T : IIdentifiedSpectrum
  {
    public override string Version
    {
      get { return "Dtaselect"; }
    }

    public override string Name
    {
      get { return "Sequence"; }
    }

    public override string GetProperty(T t)
    {
      return t.Peptides[0].Sequence;
    }

    public override void SetProperty(T t, string value)
    {
      t.ClearPeptides();
      IIdentifiedPeptide mp = t.NewPeptide();
      mp.Sequence = value;
    }
  }
}