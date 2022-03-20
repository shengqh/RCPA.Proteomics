using RCPA.Proteomics.Spectrum;
using System.Collections.Generic;

namespace RCPA.Proteomics.Fragmentation
{
  public abstract class AbstractETDPeptideBuilder<T> : AbstractPeptideFragmentationBuilder<T> where T : IIonTypePeak, new()
  {
    public double MaxMz { get; set; }

    public int PrecursorCharge { get; set; }

    public AbstractETDPeptideBuilder(double maxMz, int precursorCharge)
    {
      this.MaxMz = maxMz;
      this.PrecursorCharge = precursorCharge;
    }

    public override List<T> Build(string sequence)
    {
      var aais = GetAminoacidInfos(sequence);

      var ionMass = GetTerminalMass();

      var result = new List<T>();

      for (int i = 0; i < GetIonCount(aais.Count); i++)
      {
        ionMass += aais[i].Mass;

        if (ionMass < this.MaxMz)
        {
          var p = new T()
          {
            Mz = ionMass,
            Intensity = 10.0,
            Charge = 1,
            PeakType = IonType.C,
            PeakIndex = i + 1
          };
          result.Add(p);
          continue;
        }

        for (int charge = 2; charge < PrecursorCharge; charge++)
        {
          double mz = (ionMass + MassH * (charge - 1)) / charge;
          if (mz < this.MaxMz)
          {
            var multipleChargePeak = new T()
            {
              Mz = mz,
              Intensity = 10.0,
              Charge = charge,
              PeakType = IonType.C,
              PeakIndex = i + 1
            };
            result.Add(multipleChargePeak);
          }
        }
      }

      result.Sort((m1, m2) => m1.Mz.CompareTo(m2.Mz));

      return result;
    }

    protected override int GetIonCount(int aminoacidCount)
    {
      return aminoacidCount - 1;
    }

    protected override int IonCharge
    {
      get { return 1; }
    }
  }
}