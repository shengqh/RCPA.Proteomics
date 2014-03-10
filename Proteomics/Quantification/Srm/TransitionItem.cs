using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RCPA.Proteomics.Quantification.Srm
{
  public class TransitionItem : MzItem
  {
    public TransitionItem()
    {
      this.FileItems = new List<SrmFileItem>();
    }

    public bool Enabled { get; set; }

    public List<SrmFileItem> FileItems { get; private set; }

    public bool TransitionEquals(SrmFileItem lh, double mzTolerance)
    {
      return (Math.Abs(this.LightProductIon - lh.PairedProductIon.LightProductIon) <= mzTolerance) && (Math.Abs(this.HeavyProductIon - lh.PairedProductIon.HeavyProductIon) <= mzTolerance);
    }

    public double LightProductIon
    {
      get
      {
        var fi = FileItems.FirstOrDefault(m => m != null && m.PairedProductIon != null && m.PairedProductIon.Light != null);
        if (fi == null)
        {
          return 0.0;
        }
        else
        {
          return fi.PairedProductIon.LightProductIon;
        }
      }
    }

    public double HeavyProductIon
    {
      get
      {
        var fi = FileItems.FirstOrDefault(m => m != null && m.PairedProductIon != null && m.PairedProductIon.Heavy != null);
        if (fi == null)
        {
          return 0.0;
        }
        else
        {
          return fi.PairedProductIon.HeavyProductIon;
        }
      }
    }

    public SrmFileItem this[string fileName]
    {
      get
      {
        return (from s in FileItems
                where s.PairedResult.PureFileName == fileName
                select s).FirstOrDefault();
      }
    }

    public bool IsDecoy
    {
      get
      {
        var s = FileItems.FirstOrDefault(m => m != null && m.PairedPeptide != null);
        if (s == null)
        {
          return false;
        }
        return s.PairedPeptide.IsDecoy;
      }
    }
  }

  public static class TransitionItemUtils
  {
    public static bool IsDecoy(TransitionItem item)
    {
      return item.IsDecoy;
    }

    public static bool IsInvalid(TransitionItem item)
    {
      return !item.Enabled;
    }

    public static bool AlwaysFalse(TransitionItem item)
    {
      return false;
    }
  }


}
