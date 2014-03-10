using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace RCPA.Proteomics.MaxQuant
{
  public enum MaxQuantTagMatchRoleType { OneToOne, ManyToOne };

  /// <summary>
  /// 根据target中的条件，去source中查找相应的数据。一般，target来自筛选后的结果，source来自筛选前的结果。
  /// </summary>
  public class MaxQuantTagMatchRole
  {
    public MaxQuantTagMatchRole()
    {
      SplitChar = char.MinValue;
    }

    private bool NeedSplit { get { return SplitChar != char.MinValue; } }

    public string SourceTag { get; set; }

    public string TargetTag { get; set; }

    public char SplitChar { get; set; }

    public override string ToString()
    {
      var result = MyConvert.Format("SourceTag = \"{0}\"; TargetTag = \"{1}\"", SourceTag, TargetTag);
      if (NeedSplit)
      {
        result = result + MyConvert.Format("; SplitChar = '{0}'", SplitChar);
      }
      return result;
    }

    private IEnumerable<string> GetItems(IAnnotation item, string key)
    {
      if (NeedSplit)
      {
        var itemValue = item.Annotations[key] as string;
        return itemValue.Split(SplitChar);
      }
      else
      {
        return new string[] { item.Annotations[key] as string };
      }
    }

    public IEnumerable<string> GetTargetItems(IAnnotation target)
    {
      return GetItems(target, TargetTag);
    }

    public IEnumerable<string> GetSourceItems(IAnnotation source)
    {
      return GetItems(source, SourceTag);
    }
  }

  public class MaxQuantTagMatchRoles : List<MaxQuantTagMatchRole>, IXml
  {
    /// <summary>
    /// 检查该条目是否包含所有必需的内容
    /// </summary>
    /// <param name="source"></param>
    /// <returns></returns>
    public bool IsValidSource(IAnnotation source)
    {
      foreach (var role in this)
      {
        if (source.Annotations[role.SourceTag].ToString().Length == 0)
        {
          return false;
        }
      }
      return true;
    }

    public string[] GetTargetHeaders()
    {
      return (from role in this
              select role.TargetTag).ToArray();
    }


    public void Load(string configFile)
    {
      XElement root = XElement.Load(configFile);

      Load(root);
    }

    public void Save(string configFile)
    {
      XElement root = new XElement("Roles");

      Save(root);

      root.Save(configFile);
    }

    #region IXml Members

    public void Save(XElement parentNode)
    {
      foreach (var role in this)
      {
        parentNode.Add(new XElement("Role",
          new XElement("SourceTag", role.SourceTag),
          new XElement("TargetTag", role.TargetTag),
          new XElement("SplitChar", Convert.ToInt32(role.SplitChar))));
      }
    }

    public void Load(XElement parentNode)
    {
      this.Clear();
      foreach (var item in parentNode.Elements("Role"))
      {
        MaxQuantTagMatchRole role = new MaxQuantTagMatchRole();
        role.SourceTag = item.Element("SourceTag").Value;
        role.TargetTag = item.Element("TargetTag").Value;
        role.SplitChar = Convert.ToChar(Convert.ToInt32(item.Element("SplitChar").Value));
        this.Add(role);
      }
    }

    #endregion
  }

  public class MaxQuantTagMatcher
  {
    private MaxQuantTagMatchRoles roles;

    private IEnumerable<IAnnotation> targets;

    private Dictionary<string, IAnnotation> targetKeyMap = new Dictionary<string, IAnnotation>();

    /// <summary>
    /// 根据roles以及target集合，构建hash表，用于与source的快速比较
    /// </summary>
    /// <param name="roles">规则集合</param>
    /// <param name="targets">目标集合</param>
    public MaxQuantTagMatcher(MaxQuantTagMatchRoles roles, IEnumerable<IAnnotation> targets)
    {
      this.roles = roles;
      this.targets = targets;

      foreach (var target in targets)
      {
        List<string> entries = GetKeys(target, (role, item) => role.GetTargetItems(item));

        entries.ForEach(m => this.targetKeyMap[m] = target);
      }
    }

    private List<string> GetKeys(IAnnotation target, Func<MaxQuantTagMatchRole, IAnnotation, IEnumerable<string>> keyDistiller)
    {
      List<string> entries = new List<string>();
      foreach (var role in this.roles)
      {
        IEnumerable<string> keys = keyDistiller(role, target);
        if (entries.Count == 0)
        {
          entries.AddRange(keys);
        }
        else
        {
          entries = (from e in entries
                     from k in keys
                     select e + "_" + k).ToList();
        }
      }
      return entries;
    }

    private static readonly string foundkey = "FOUND";

    /// <summary>
    /// 判断source是否满足要求
    /// </summary>
    /// <param name="source">来源</param>
    public bool Match(IAnnotation source)
    {
      IEnumerable<string> entries = GetKeys(source, (role, item) => role.GetSourceItems(item));

      foreach (var e in entries)
      {
        if (targetKeyMap.ContainsKey(e))
        {
          var value = targetKeyMap[e];
          value.Annotations[foundkey] = true;
          return true;
        }
      }

      return false;
    }

    public List<IAnnotation> GetMissedTargets()
    {
      return (from t in this.targets
              where !t.Annotations.ContainsKey(foundkey)
              select t).ToList();
    }
  }
}
