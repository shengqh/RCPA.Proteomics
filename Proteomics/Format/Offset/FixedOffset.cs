using RCPA.Gui;

namespace RCPA.Proteomics.Format.Offset
{
  public class FixedOffset : ProgressClass, IOffset
  {
    private double precursorPPM;

    private double productIonPPM;

    public FixedOffset(double precursorPPM, double productIonPPM)
    {
      this.precursorPPM = precursorPPM;
      this.productIonPPM = productIonPPM;
    }

    #region IOffset Members

    public double GetPrecursorOffset(string experimental, int scan)
    {
      return precursorPPM;
    }

    public double GetProductIonOffset(string experimental, int scan)
    {
      return productIonPPM;
    }

    #endregion

    public override string ToString()
    {
      if (precursorPPM != 0.0 && productIonPPM != 0.0)
      {
        return string.Format("PrecursorPPM({0:0.00}),ProductIonPPM({1:0.00})", precursorPPM, productIonPPM);
      }
      else if (precursorPPM != 0.0)
      {
        return string.Format("PrecursorPPM({0:0.00})", precursorPPM);
      }
      else if (productIonPPM != 0.0)
      {
        return string.Format("ProductIonPPM({1:0.00})", productIonPPM);
      }
      else
      {
        return "NONE";
      }
    }
  }
}
