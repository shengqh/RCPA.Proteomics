using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RCPA.Proteomics.Quantification.Srm
{
  public class CompoundItem : MzItem
  {
    public CompoundItem()
    {
      this.TransitionItems = new List<TransitionItem>();
    }

    public string FileName { get; set; }

    private bool _enabledAssigned = false;
    private bool _enabled = true;

    public bool Enabled
    {
      get
      {
        if (string.IsNullOrEmpty(FileName))
        {
          if (_enabledAssigned)
          {
            return _enabled;
          }
          else
          {
            var totalCount = (from v in this.TransitionItems
                              from vl in v.FileItems
                              select vl).Count();

            var goodCount = (from v in this.TransitionItems
                             from vl in v.FileItems
                             where vl.PairedProductIon.Enabled
                             select vl).Count();

            return goodCount >= totalCount / 2;
          }
        }
        else
        {
          var pep = GetPeptideItem(FileName);
          if (null == pep)
          {
            return false;
          }
          return pep.Enabled;
        }
      }
      set
      {
        if (string.IsNullOrEmpty(FileName))
        {
          _enabledAssigned = true;
          _enabled = value;
        }
        else
        {
          var pep = GetPeptideItem(FileName);
          if (null == pep)
          {
            return;
          }
          pep.Enabled = value;
        }
      }
    }

    public bool IsDecoy
    {
      get { return TransitionItems.Any(m => m != null && m.FileItems.Any(n => n != null && n.PairedProductIon.IsDecoy)); }
    }

    public List<FileItems> FileItems { get; private set; }

    private string GetString(TransitionItem transition)
    {
      return MyConvert.Format("{0:0.0000}-{1:0.0000}", transition.LightMz, transition.HeavyMz);
    }

    public void InitializeFileItems(IEnumerable<string> fileNames)
    {
      FileItems = new List<FileItems>();
      foreach (var titem in TransitionItems)
      {
        var item = new FileItems();
        item.Precursor = GetString(titem);
        item.Items = new List<SrmFileItem>();

        foreach (var filename in fileNames)
        {
          var fileitem = titem.FileItems.Find(m => m.PairedResult.PureFileName.Equals(filename));
          item.Items.Add(fileitem);
        }

        FileItems.Add(item);
      }
    }

    public List<TransitionItem> TransitionItems { get; private set; }

    public SrmPairedPeptideItem GetPeptideItem(string fileName)
    {
      return (from t in TransitionItems
              from p in t.FileItems
              where p.PairedResult.PureFileName == fileName
              select p.PairedPeptide).FirstOrDefault();

    }

    public List<SrmPairedProductIon> GetProductIonPairs(string fileName)
    {
      var pep = GetPeptideItem(fileName);

      if (pep == null)
      {
        return new List<SrmPairedProductIon>();
      }

      return pep.ProductIonPairs;
    }

    public bool HasPeptideItem(SrmPairedPeptideItem item)
    {
      return TransitionItems.Any(m => m.FileItems.Any(n => n.PairedPeptide == item));
    }
  }

  public static class CompoundItemUtils
  {
    public static bool IsDecoy(CompoundItem item)
    {
      return item.IsDecoy;
    }

    public static bool IsInvalid(CompoundItem item)
    {
      return !item.Enabled;
    }

    public static bool AlwaysFalse(CompoundItem item)
    {
      return false;
    }
  }
}
