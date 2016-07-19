using System.Collections.Generic;
using RCPA.Proteomics.Spectrum;
using RCPA.emass;
using System.Windows.Forms;
using System.IO;
using System.Linq;
using RCPA.Utils;

namespace RCPA.Proteomics.Isotopic
{
  public interface IIsotopicProfileBuilder2
  {
    List<Peak> GetProfile(AtomComposition ac, int charge, int profileLength);

    List<Peak> GetProfile(AtomComposition ac, int charge, double minPercentage);
  }

  public class EmassProfileBuilder : IIsotopicProfileBuilder2
  {
    private EmassCalculator calculator;

    public EmassProfileBuilder() : this(FileUtils.AppPath() + "\\ISOTOPE.DAT") { }

    public EmassProfileBuilder(string isotopeDat)
    {
      this.calculator = new EmassCalculator(isotopeDat);
    }

    #region IIsotopicProfileBuilder2 Members

    public List<Peak> GetProfile(AtomComposition ac, int charge, double minPercentage)
    {
      var result = DoGetProfile(ac, charge);

      while (result.Last().Intensity < minPercentage)
      {
        result.RemoveAt(result.Count - 1);
      }

      return result;
    }

    public List<Peak> GetProfile(AtomComposition ac, int charge, int profileLength)
    {
      var result = DoGetProfile(ac, charge);

      while (result.Count > profileLength)
      {
        result.RemoveAt(result.Count - 1);
      }

      return result;
    }

    private Pattern DoGetProfile(AtomComposition ac, int charge)
    {
      AtomComposition ac2;
      if (charge == 0)
      {
        ac2 = ac;
      }
      else
      {
        ac2 = new AtomComposition(ac.GetFormula());
        ac2[Atom.H] = ac2[Atom.H] + charge;
      }

      var result = calculator.Calculate(ac2, 0, charge);
      return result;
    }

    #endregion
  }
}