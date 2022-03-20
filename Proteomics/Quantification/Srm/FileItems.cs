using System.Collections.Generic;
using System.Linq;

namespace RCPA.Proteomics.Quantification.Srm
{
  public class FileItems
  {
    private static double EnabledPercentage = 0.5;

    public string Precursor { get; set; }

    public List<SrmFileItem> Items { get; set; }

    public double LightProductIon
    {
      get
      {
        var fi = Items.FirstOrDefault(m => m != null && m.PairedProductIon != null && m.PairedProductIon.Light != null);
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
        var fi = Items.FirstOrDefault(m => m != null && m.PairedProductIon != null && m.PairedProductIon.Heavy != null);
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

    private bool _enabled;
    private bool _enabledAssigned = false;

    public bool Enabled
    {
      get
      {
        if (_enabledAssigned)
        {
          return _enabled;
        }
        else
        {
          return (from p in Items
                  where p != null && p.PairedProductIon.Enabled
                  select p).Count() >= Items.Count * EnabledPercentage;
        }
      }
      set
      {
        _enabled = value;
        _enabledAssigned = true;
      }
    }
  }
}
