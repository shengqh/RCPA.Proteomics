using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RCPA.Proteomics.Quantification.Srm
{
  /// <summary>
  /// 多个MRM结果的集合，用于多文件之间比较。
  /// </summary>
  public class SrmFileItemList
  {
    public double MzTolerance { get; private set; }

    public List<SrmFileItem> Items { get; set; }

    public SrmFileItemList(List<SrmFileItem> fileitems, double mzTolerance)
    {
      this.MzTolerance = mzTolerance;

      this.Items = fileitems;

      BuildMap();
    }

    public SrmFileItemList(List<SrmPairedResult> fileitems, double mzTolerance)
    {
      this.MzTolerance = mzTolerance;

      this.Items = (from fi in fileitems
                    from pep in fi
                    from product in pep.ProductIonPairs
                    select new SrmFileItem(fi, pep, product)).ToList();

      BuildList();
    }

    public List<string> GetFileNames()
    {
      return (from pr in this.Items
              orderby pr.PairedResult.PureFileName
              select pr.PairedResult.PureFileName).Distinct().ToList();
    }

    private List<CompoundItem> _compounds;
    public List<CompoundItem> Compounds { get { return _compounds; } }

    private bool IsSameCompound(MzItem a, SrmFileItem b)
    {
      if (a.HasName())
      {
        return a.NameChargeEquals(b);
      }
      else
      {
        return a.MzEquals(b.PrecursorMz, MzTolerance);
      }
    }

    public void BuildList()
    {
      _compounds = new List<CompoundItem>();

      foreach (var m in Items)
      {
        CompoundItem citem = null;
        foreach (var c in _compounds)
        {
          if (IsSameCompound(c, m))
          {
            citem = c;
            break;
          }
        }

        if (citem == null)
        {
          citem = new CompoundItem();
          AssignSrmFileItemTo(m, citem);
          _compounds.Add(citem);
        }

        TransitionItem titem = null;
        foreach (var t in citem.TransitionItems)
        {
          if (t.TransitionEquals(m, this.MzTolerance))
          {
            titem = t;
            break;
          }
        }

        if (titem == null)
        {
          titem = new TransitionItem();
          AssignSrmFileItemTo(m, titem);
          citem.TransitionItems.Add(titem);
        }

        titem.FileItems.Add(m);
      }

      var fileNames = GetFileNames();

      _compounds.ForEach(m => m.InitializeFileItems(fileNames));
      _compounds.Sort((m1, m2) => m1.ToString().CompareTo(m2.ToString()));
      _compounds.ForEach(m => m.TransitionItems.Sort((n1, n2) => n1.LightMz.CompareTo(n2.LightMz)));
    }

    private static void AssignSrmFileItemTo(SrmFileItem source, MzItem target)
    {
      target.ObjectName = source.PairedProductIon.ObjectName;
      target.PrecursurFormula = source.PairedProductIon.PrecursorFormula;
      target.PrecursurCharge = source.PairedProductIon.PrecursorCharge;
      target.LightMz = source.PairedPeptide.LightPrecursorMZ;
      target.HeavyMz = source.PairedPeptide.HeavyPrecursorMZ;
    }

    public List<CompoundItem> GetCompounds(CompoundItemFilter reject)
    {
      return (from c in _compounds
              where !reject(c)
              select c).ToList();
    }

    private Dictionary<Pair<double, double>, Dictionary<Pair<double, double>, List<SrmFileItem>>> _map;
    public Dictionary<Pair<double, double>, Dictionary<Pair<double, double>, List<SrmFileItem>>> PeptideProductIonMap { get { return _map; } }

    private void BuildMap()
    {
      _map = new Dictionary<Pair<double, double>, Dictionary<Pair<double, double>, List<SrmFileItem>>>();

      foreach (var m in Items)
      {
        Dictionary<Pair<double, double>, List<SrmFileItem>> pepmap = null;
        foreach (var d in _map)
        {
          if (m.PrecursorEquals(d.Key, this.MzTolerance))
          {
            pepmap = d.Value;
            break;
          }
        }

        if (pepmap == null)
        {
          pepmap = new Dictionary<Pair<double, double>, List<SrmFileItem>>();
          _map[m.PrecursorMz] = pepmap;
        }

        List<SrmFileItem> productlist = null;
        foreach (var p in pepmap)
        {
          if (m.ProductEquals(p.Key, MzTolerance))
          {
            productlist = p.Value;
            break;
          }
        }

        if (productlist == null)
        {
          productlist = new List<SrmFileItem>();
          pepmap[m.ProductIonMz] = productlist;
        }
        productlist.Add(m);
      }
    }

    public SrmFileItem FindFileItem(SrmPairedProductIon ion)
    {
      return (from s in Items
              where s.PairedProductIon == ion
              select s).FirstOrDefault();
    }
  }
}
