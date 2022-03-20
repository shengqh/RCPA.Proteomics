using System.Collections.Generic;
using System.Linq;

namespace RCPA.Proteomics.Quantification.ITraq
{
  public class ITraqQuantificationChannelItem : QuantificationItemBase
  {
    public string ChannelName { get; set; }
  }

  public class ITraqQuantificationDatasetItem
  {
    public string DatasetName { get; set; }

    public Dictionary<string, ITraqQuantificationChannelItem> RatioMap { get; set; }

    public ITraqQuantificationDatasetItem()
    {
      RatioMap = new Dictionary<string, ITraqQuantificationChannelItem>();
    }
  }

  public class ITraqQuantificationResult
  {
    public Dictionary<string, ITraqQuantificationDatasetItem> DatasetMap { get; set; }

    public ITraqQuantificationResult()
    {
      DatasetMap = new Dictionary<string, ITraqQuantificationDatasetItem>();
    }
  }

  public static class ITraqQuantificationExtension
  {
    public static ITraqQuantificationResult FindITraqQuantificationResult<T>(this T t) where T : IAnnotation
    {
      if (t.Annotations.ContainsKey(ITraqConsts.ITRAQ_KEY))
      {
        return t.Annotations[ITraqConsts.ITRAQ_KEY] as ITraqQuantificationResult;
      }
      return null;
    }

    public static ITraqQuantificationResult FindOrCreateITraqQuantificationResult<T>(this T t) where T : IAnnotation
    {
      if (t.Annotations.ContainsKey(ITraqConsts.ITRAQ_KEY))
      {
        return t.Annotations[ITraqConsts.ITRAQ_KEY] as ITraqQuantificationResult;
      }
      else
      {
        var result = new ITraqQuantificationResult();
        t.Annotations[ITraqConsts.ITRAQ_KEY] = result;
        return result;
      }
    }

    public static ITraqQuantificationChannelItem FindITraqChannelItem<T>(this T t, string dsName, string channelName) where T : IAnnotation
    {
      var pqr = FindITraqQuantificationResult(t);
      if (null != pqr)
      {
        return (from item in pqr.DatasetMap
                where item.Key == dsName
                from channel in item.Value.RatioMap
                where channel.Key == channelName
                select channel.Value).FirstOrDefault();
      }

      return null;
    }

    public static ITraqQuantificationChannelItem FindOrCreateITraqChannelItem<T>(this T t, string dsName, string channelName) where T : IAnnotation
    {
      var pqr = FindOrCreateITraqQuantificationResult(t);

      if (!pqr.DatasetMap.ContainsKey(dsName))
      {
        pqr.DatasetMap[dsName] = new ITraqQuantificationDatasetItem()
        {
          DatasetName = dsName
        };
      }
      var dsItem = pqr.DatasetMap[dsName];

      if (!dsItem.RatioMap.ContainsKey(channelName))
      {
        dsItem.RatioMap[channelName] = new ITraqQuantificationChannelItem()
        {
          ChannelName = channelName
        };
      }
      return dsItem.RatioMap[channelName];
    }

  }
}
