using RCPA.Proteomics.Quantification.ITraq;
using System.Collections.Generic;

namespace RCPA.Tools.Quantification
{
  public class ITraqPhosphoPeptideStatisticBuilder : AbstractITraqPeptideStatisticBuilder
  {
    private IITraqRatioCalculator calc;

    private string modifiedAminoacids;

    public ITraqPhosphoPeptideStatisticBuilder(string rawFileName, string modifiedAminoacids, IITraqRatioCalculator calc)
      : base(rawFileName)
    {
      this.calc = calc;
      this.modifiedAminoacids = modifiedAminoacids;
    }

    public override IEnumerable<string> Process(string fileName)
    {
      //ModificationSpectrumFilter filter = new ModificationSpectrumFilter(modifiedAminoacids);

      //var find = GetSpectrumITraqPairList(fileName);

      //if (find.Count == 0)
      //{
      //  throw new Exception("No pair found between peptide and itraq!");
      //}

      //var plexType = find.First().Value.PlexType;

      //List<ITraqFuncClass> funcs = plexType.GetITraqFuncs();

      //var bin = find.GroupBy(f => PeptideUtils.GetPureSequence(f.Key.Sequence));

      //string resultFileName = fileName + ".modified_" + modifiedAminoacids + "_ratio.itraq";
      //using (StreamWriter sw = new StreamWriter(resultFileName))
      //{
      //  sw.WriteLine("PureSequence\tSequence\tFileScan\t{0}\t{1}", funcs.GetITraqNames(), calc.GetRatioHeader());

      //  foreach (var b in bin)
      //  {
      //    bool hasModified = false;
      //    bool hasNormal = false;
      //    foreach (var s in b)
      //    {
      //      if (filter.Accept(s.Key))
      //      {
      //        hasModified = true;
      //      }
      //      else
      //      {
      //        hasNormal = true;
      //      }
      //    }

      //    if (hasModified && hasNormal)
      //    {
      //      sw.WriteLine(b.Key);

      //      var items =
      //        from i in b
      //        orderby i.Key.Sequence
      //        select i;

      //      foreach (var v in items)
      //      {
      //        if (!filter.Accept(v.Key) && calc.Valid(v.Value))
      //        {
      //          sw.WriteLine("\t{0}\t{1}\t{2}\t{3}",
      //            v.Key.Sequence,
      //            v.Key.Query.FileScan.ShortFileName,
      //            funcs.GetITraqValues(v.Value),
      //            calc.GetRatioValue(v.Value));
      //        }
      //        else
      //        {
      //          sw.WriteLine("\t{0}\t{1}\t{2}",
      //            v.Key.Sequence,
      //            v.Key.Query.FileScan.ShortFileName,
      //            funcs.GetITraqValues(v.Value));
      //        }
      //      }
      //    }
      //  }
      //}

      //Progress.SetMessage("Finished.");

      //return new[] { resultFileName };
      return null;
    }
  }
}
