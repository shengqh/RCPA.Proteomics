using System.Collections.Generic;

namespace RCPA.Proteomics
{
  public class SiliconePolymer
  {
    public SiliconePolymer(int number, double precursor)
    {
      this.PolymerNumber = number;
      this.PrecursorMinusCH4 = precursor - Atom.C.MonoMass - Atom.H.MonoMass * 4;
      this.Precursor = precursor;
      this.PrecursorNH3 = precursor + Atom.N.MonoMass + Atom.H.MonoMass * 3;
    }

    public int PolymerNumber { get; set; }

    public double PrecursorMinusCH4 { get; set; }

    public bool PrecursorMinusCH4Checked { get; set; }

    public double Precursor { get; set; }

    public bool PrecursorChecked { get; set; }

    public double PrecursorNH3 { get; set; }

    public bool PrecursorNH3Checked { get; set; }

    public void SetCheckedAll(bool value)
    {
      PrecursorMinusCH4Checked = value;
      PrecursorChecked = value;
      PrecursorNH3Checked = value;
    }

    public void SetChecked(int mass)
    {
      if ((int)PrecursorMinusCH4 == mass)
      {
        PrecursorMinusCH4Checked = true;
      }

      if ((int)Precursor == mass)
      {
        PrecursorChecked = true;
      }

      if ((int)PrecursorNH3 == mass)
      {
        PrecursorNH3Checked = true;
      }
    }

    public List<double> GetSelectedPloymers()
    {
      var result = new List<double>();

      if (PrecursorMinusCH4Checked)
      {
        result.Add(PrecursorMinusCH4);
      }

      if (PrecursorChecked)
      {
        result.Add(Precursor);
      }

      if (PrecursorNH3Checked)
      {
        result.Add(PrecursorNH3);
      }

      return result;
    }
  }
}
