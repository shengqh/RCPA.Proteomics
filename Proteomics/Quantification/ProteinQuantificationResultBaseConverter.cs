using System;

namespace RCPA.Proteomics.Quantification
{
  /// <summary>
  /// 读取或者保存蛋白质定量结果到IAnnotation中。
  /// 首先根据objectKey取出ProteinQuantificationResult，然后根据dsName取出对应的QuantificationItem，最后根据GetValue和SetValue进行数据读取。
  /// </summary>
  /// <typeparam name="T">annotation对象</typeparam>
  public class ProteinQuantificationResultBaseConverter<T> : AbstractPropertyConverter<T> where T : IAnnotation
  {
    private string objectKey;

    private string headerPrefix;

    private string dsName;

    private Func<QuantificationItem, string> GetValue;

    private Action<QuantificationItem, string> SetValue;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="objectKey">从IAnnoation存取ProteinQuantificationResult的Key</param>
    /// <param name="headerPrefix">表征这个Converter的标识</param>
    /// <param name="dsName">数据集名称</param>
    /// <param name="getValue">读值函数</param>
    /// <param name="setValue">设值函数</param>
    public ProteinQuantificationResultBaseConverter(string objectKey, string headerPrefix, string dsName, Func<QuantificationItem, string> getValue, Action<QuantificationItem, string> setValue)
    {
      this.objectKey = objectKey;
      this.headerPrefix = headerPrefix;
      this.dsName = dsName;
      this.GetValue = getValue;
      this.SetValue = setValue;
    }

    public override string Name
    {
      get { return headerPrefix + "_" + dsName; }
    }

    public override string GetProperty(T t)
    {
      var item = FindItem(t);

      if (null == item)
      {
        return string.Empty;
      }

      return GetValue(item);
    }

    public override void SetProperty(T t, string value)
    {
      var item = FindOrCreateItem(t);

      SetValue(item, value);
    }

    private QuantificationItem FindItem(T t)
    {
      if (t.Annotations.ContainsKey(objectKey))
      {
        var pqr = t.Annotations[objectKey] as ProteinQuantificationResult;
        if (pqr.Items.ContainsKey(dsName))
        {
          return pqr.Items[dsName];
        }
      }

      return null;
    }

    private QuantificationItem FindOrCreateItem(T t)
    {
      if (!t.Annotations.ContainsKey(objectKey))
      {
        t.Annotations[objectKey] = new ProteinQuantificationResult();
      }

      var pqr = t.Annotations[objectKey] as ProteinQuantificationResult;
      if (!pqr.Items.ContainsKey(dsName))
      {
        var item = new QuantificationItem();
        pqr.Items[dsName] = item;
        return item;
      }
      else
      {
        return pqr.Items[dsName];
      }
    }
  }
}
