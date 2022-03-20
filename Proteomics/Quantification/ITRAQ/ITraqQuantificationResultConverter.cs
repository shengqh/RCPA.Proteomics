using System.Collections.Generic;
using System.Linq;

namespace RCPA.Proteomics.Quantification.ITraq
{
  public class ITraqQuantificationResultConverter<T> : AbstractPropertyConverter<T> where T : IAnnotation
  {
    private string itraqKey;

    private string prefix = "IR";

    public ITraqQuantificationResultConverter()
    {
      this.itraqKey = ITraqConsts.ITRAQ_KEY;
    }

    public override string Name
    {
      get { return itraqKey; }
    }

    public override string GetProperty(T t)
    {
      if (t.Annotations.ContainsKey(itraqKey))
      {
        var lrrr = t.Annotations[itraqKey] as ITraqQuantificationResult;
        return lrrr.DatasetMap.Count.ToString();
      }

      return "0";
    }

    public override void SetProperty(T t, string value)
    {
      if (!t.Annotations.ContainsKey(itraqKey))
      {
        t.Annotations[itraqKey] = new ITraqQuantificationResult();
      }
    }

    public override List<IPropertyConverter<T>> GetRelativeConverter(List<T> items)
    {
      var dsChannelMap = new Dictionary<string, HashSet<string>>();
      foreach (var t in items)
      {
        if (t.Annotations.ContainsKey(itraqKey))
        {
          var lrrr = t.Annotations[itraqKey] as ITraqQuantificationResult;
          foreach (var ds in lrrr.DatasetMap)
          {
            if (!dsChannelMap.ContainsKey(ds.Key))
            {
              dsChannelMap[ds.Key] = new HashSet<string>();
            }
            dsChannelMap[ds.Key].UnionWith(ds.Value.RatioMap.Keys);
          }
        }
      }

      return DoGetConverter(dsChannelMap);
    }

    private List<IPropertyConverter<T>> DoGetConverter(Dictionary<string, HashSet<string>> dsChannelMap)
    {
      if (0 == dsChannelMap.Count)
      {
        return null;
      }

      List<IPropertyConverter<T>> result = new List<IPropertyConverter<T>>();

      var dss = dsChannelMap.Keys.OrderBy(m => m).ToList();
      foreach (var ds in dss)
      {
        var channels = dsChannelMap[ds].OrderBy(m => m).ToList();
        foreach (var channel in channels)
        {
          result.Add(new ITraqQuantificationBaseConverter<T>(prefix, ds, channel, m => m.RatioStr, (m, v) => m.RatioStr = v));
        }
      }
      return result;
    }

    public override List<IPropertyConverter<T>> GetRelativeConverter(string header, char delimiter)
    {
      string[] parts = header.Split(delimiter);

      var datasets = (from p in parts
                      where p.StartsWith("IR_")
                      let pp = p.Substring(3)
                      let pos = pp.IndexOf('_')
                      where pos > 0
                      let dsName = pp.Substring(0, pos)
                      let channelName = pp.Substring(pos + 1)
                      select new { dsName, channelName }).ToList();

      Dictionary<string, HashSet<string>> dsChannelMap = new Dictionary<string, HashSet<string>>();

      var dsMap = datasets.GroupBy(m => m.dsName);

      foreach (var ds in dsMap)
      {
        if (!dsChannelMap.ContainsKey(ds.Key))
        {
          dsChannelMap[ds.Key] = new HashSet<string>();
        }

        dsChannelMap[ds.Key].UnionWith(from d in ds
                                       select d.channelName);
      }

      return DoGetConverter(dsChannelMap);
    }
  }
}