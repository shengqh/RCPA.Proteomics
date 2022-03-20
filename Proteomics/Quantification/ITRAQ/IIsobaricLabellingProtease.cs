using System;

namespace RCPA.Proteomics.Quantification.ITraq
{
  public interface IIsobaricLabellingProtease
  {
    void InitializeByTag(double tagMH);

    double GetHighBYFreeWindow();

    double GetLowestBYFreeWindow();

    string GetHighBYFreeWindowDescription();
  }

  public abstract class AbstractIsobaricLabellingProtease : IIsobaricLabellingProtease
  {
    protected double lowBYFreeWindow;

    protected double highBYFreeWindow;

    protected string highBYFreeWindowDescription;

    public abstract void InitializeByTag(double tagMH);

    public override string ToString()
    {
      return "Trypsin";
    }

    #region IIsobaricLabellingProtease Members

    public string GetHighBYFreeWindowDescription()
    {
      return highBYFreeWindowDescription;
    }

    public double GetHighBYFreeWindow()
    {
      return highBYFreeWindow;
    }

    public double GetLowestBYFreeWindow()
    {
      return lowBYFreeWindow;
    }

    #endregion
  }

  public class IsobaricLabellingTrypsin : AbstractIsobaricLabellingProtease
  {
    public override void InitializeByTag(double tagMH)
    {
      var aas = new Aminoacids();
      var minb = aas['G'].MonoMass + tagMH;
      var h2o = Atom.H.MonoMass * 2 + Atom.O.MonoMass;
      var miny = Math.Min(aas['K'].MonoMass + tagMH + h2o, aas['R'].MonoMass + h2o + Atom.H.MonoMass);
      this.lowBYFreeWindow = Math.Min(minb, miny);

      var massR = aas['R'].MonoMass + h2o;
      var massK = aas['K'].MonoMass + tagMH + Atom.H.MonoMass + Atom.O.MonoMass;
      this.highBYFreeWindow = Math.Min(massR, massK);
      if (massR < massK)
      {
        this.highBYFreeWindowDescription = "PrecursorMass-Arg-H2O";
      }
      else
      {
        this.highBYFreeWindowDescription = "PrecursorMass-Lys-Label-OH";
      }
    }

    public override string ToString()
    {
      return "Trypsin";
    }
  }

  public class IsobaricLabellingLysC : AbstractIsobaricLabellingProtease
  {
    public override string ToString()
    {
      return "LysC";
    }

    public override void InitializeByTag(double tagMH)
    {
      var aas = new Aminoacids();
      var minb = aas['G'].MonoMass + tagMH;
      var h2o = Atom.H.MonoMass * 2 + Atom.O.MonoMass;
      this.lowBYFreeWindow = aas['K'].MonoMass + tagMH + h2o;

      this.highBYFreeWindow = aas['K'].MonoMass + tagMH + Atom.H.MonoMass + Atom.O.MonoMass;
      this.highBYFreeWindowDescription = "PrecursorMass-Lys-Label-OH";
    }
  }

  public class IsobaricLabellingOther : AbstractIsobaricLabellingProtease
  {
    public override string ToString()
    {
      return "Other";
    }

    public override void InitializeByTag(double tagMH)
    {
      var aas = new Aminoacids();
      var minb = aas['G'].MonoMass + tagMH;
      var h2o = Atom.H.MonoMass * 2 + Atom.O.MonoMass;
      this.lowBYFreeWindow = aas['G'].MonoMass + tagMH;

      this.highBYFreeWindow = aas['G'].MonoMass + tagMH + Atom.H.MonoMass + Atom.O.MonoMass;
      this.highBYFreeWindowDescription = "PrecursorMass-Gly-Label-OH";
    }
  }

  public static class IsobaricLabellingProteaseFactory
  {
    private static IIsobaricLabellingProtease[] proteases = null;

    public static IIsobaricLabellingProtease[] Proteases
    {
      get
      {
        if (proteases == null)
        {
          proteases = new IIsobaricLabellingProtease[] { new IsobaricLabellingTrypsin(), new IsobaricLabellingLysC(), new IsobaricLabellingOther() };
        }

        return proteases;
      }
    }

    public static IIsobaricLabellingProtease GetProtease(string name)
    {
      foreach (var protease in Proteases)
      {
        if (protease.ToString().Equals(name))
        {
          return protease;
        }
      }

      return new IsobaricLabellingOther();
    }
  }
}
