using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RCPA.Proteomics.Quantification.Srm
{
  public class SrmFileItem
  {
    public SrmPairedResult PairedResult { get; private set; }

    public SrmPairedPeptideItem PairedPeptide { get; private set; }

    public SrmPairedProductIon PairedProductIon { get; private set; }

    public SrmFileItem(SrmPairedResult result, SrmPairedPeptideItem peptide, SrmPairedProductIon production)
    {
      this.PairedResult = result;
      this.PairedPeptide = peptide;
      this.PairedProductIon = production;
      _precursorMz = new Pair<double, double>(peptide.LightPrecursorMZ, peptide.HeavyPrecursorMZ);
      _productIonMz = new Pair<double, double>(production.LightProductIon, production.HeavyProductIon);
    }

    private Pair<double, double> _precursorMz;
    public Pair<double, double> PrecursorMz
    {
      get
      {
        return _precursorMz;
      }
    }

    private Pair<double, double> _productIonMz;
    public Pair<double, double> ProductIonMz
    {
      get
      {
        return _productIonMz;
      }
    }

    private bool MzEquals(Pair<double, double> a, Pair<double, double> b, double mzTolerance)
    {
      return (Math.Abs(a.First - b.First) <= mzTolerance) && (Math.Abs(a.Second - b.Second) <= mzTolerance);
    }

    public bool PrecursorEquals(SrmFileItem another, double mzTolerance)
    {
      return MzEquals(this.PrecursorMz, another.PrecursorMz, mzTolerance);
    }

    public bool ProductEquals(SrmFileItem another, double mzTolerance)
    {
      return MzEquals(this.ProductIonMz, another.ProductIonMz, mzTolerance);
    }

    public bool PrecursorEquals(Pair<double, double> another, double mzTolerance)
    {
      return MzEquals(this.PrecursorMz, another, mzTolerance);
    }

    public bool ProductEquals(Pair<double, double> another, double mzTolerance)
    {
      return MzEquals(this.ProductIonMz, another, mzTolerance);
    }
  }
}
