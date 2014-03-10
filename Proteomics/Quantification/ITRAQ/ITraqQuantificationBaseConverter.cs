using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RCPA.Proteomics.Quantification.ITraq
{
  /// <summary>
  /// 读取或者保存ITRAQ蛋白质定量结果到IAnnotation中。
  /// 首先根据objectKey取出ITraqProteinQuantificationResult，然后根据dsName取出对应的ITraqProteinQuantificationItem，
  /// 再根据channelName取出ITraqProteinQuantificationChannelItem，最后根据GetValue和SetValue进行数据读取。
  /// </summary>
  /// <typeparam name="T">annotation对象</typeparam>
  public class ITraqQuantificationBaseConverter<T> : AbstractPropertyConverter<T> where T : IAnnotation
  {
    private string prefix;

    private string dsName;

    private string channelName;

    private Func<ITraqQuantificationChannelItem, string> GetValue;

    private Action<ITraqQuantificationChannelItem, string> SetValue;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="dsName">数据集名称</param>
    /// <param name="channelName">通道名称（例如114）</param>
    /// <param name="getValue">读值函数</param>
    /// <param name="setValue">设值函数</param>
    public ITraqQuantificationBaseConverter(string prefix, string dsName, string channelName, Func<ITraqQuantificationChannelItem, string> getValue, Action<ITraqQuantificationChannelItem, string> setValue)
    {
      this.prefix = prefix;
      this.dsName = dsName;
      this.channelName = channelName;
      this.GetValue = getValue;
      this.SetValue = setValue;
    }

    public override string Name
    {
      get { return prefix + "_" + dsName + "_" + channelName; }
    }

    public override string GetProperty(T t)
    {
      var item = t.FindITraqChannelItem(dsName, channelName);

      if (null == item)
      {
        return string.Empty;
      }

      return GetValue(item);
    }

    public override void SetProperty(T t, string value)
    {
      var item = t.FindOrCreateITraqChannelItem(dsName, channelName);

      SetValue(item, value);
    }
  }
}
