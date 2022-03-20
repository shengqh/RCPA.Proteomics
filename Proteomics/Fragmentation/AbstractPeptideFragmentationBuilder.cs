using RCPA.Proteomics.Spectrum;
using System.Collections.Generic;

namespace RCPA.Proteomics.Fragmentation
{
  public abstract class AbstractPeptideFragmentationBuilder<T> : IPeptideFragmentationBuilder<T> where T : IIonTypePeak, new()
  {
    protected static readonly double MassH = Atom.H.MonoMass;

    protected static readonly double MassNH3 = Atom.N.MonoMass + Atom.H.MonoMass * 3;

    protected static readonly double MassN = Atom.N.MonoMass;

    protected static readonly double MassOH = Atom.O.MonoMass + Atom.H.MonoMass;

    public Aminoacids CurAminoacids { get; set; }

    public double CtermMass { get; set; }

    public double NtermMass { get; set; }

    public AbstractPeptideFragmentationBuilder()
    {
      CurAminoacids = new Aminoacids();
      NtermMass = MassH;
      CtermMass = MassOH;
    }

    #region IPeptideFragmentationBuilder Members

    public virtual List<T> Build(string sequence)
    {
      var aais = GetAminoacidInfos(sequence);

      var result = new List<T>();

      double ionMass = GetTerminalMass();

      for (int i = 0; i < GetIonCount(aais.Count); i++)
      {
        ionMass += aais[i].Mass;

        var p = new T()
        {
          Mz = ionMass / IonCharge,
          Intensity = 10.0,
          Charge = IonCharge,
          PeakType = SeriesType,
          PeakIndex = i + 1,
        };
        result.Add(p);
      }

      return result;
    }

    public abstract IonType SeriesType { get; }

    #endregion

    protected abstract int GetIonCount(int aminoacidCount);

    protected abstract double GetTerminalMass();

    protected abstract List<AminoacidInfo> GetAminoacidInfos(string sequence);

    protected abstract int IonCharge { get; }

    protected List<AminoacidInfo> GetForwardAminoacidInfos(string sequence)
    {
      return CurAminoacids.BuildInfo(sequence);
    }

    protected List<AminoacidInfo> GetReverseAminoacidInfos(string sequence)
    {
      var result = GetForwardAminoacidInfos(sequence);

      result.Reverse();

      return result;
    }
  }
}