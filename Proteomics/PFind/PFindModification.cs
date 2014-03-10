using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Diagnostics;

namespace RCPA.Proteomics.PFind
{
  public class ModificationEntry
  {
    public string Modification { get; set; }

    public string Aminoacid { get; set; }

    public override string ToString()
    {
      return MyConvert.Format("{0}_{1}", Modification, Aminoacid);
    }
  }
  public class PFindModificationItem : List<ModificationEntry>
  {
    private readonly Dictionary<int, string> _modificationMap = new Dictionary<int, string>();
//    private readonly Regex g = new Regex(@"(.+?)\((.+)\)");
    private readonly Regex g = new Regex(@"(.+)_(.+)");

    public Dictionary<int, string> ModificationMap
    {
      get { return this._modificationMap; }
    }

    public string Modification
    {
      get
      {
        var sb = new StringBuilder(this.Count);
        foreach (var value in this)
        {
          sb.Append(MyConvert.Format(",{0}", value));
        }
        return sb.ToString();
      }
      set
      {
        Clear();
        if (value.Trim().Length > 0)
        {
          string[] parts = value.Split(new[] { ',' });
          for (int i = 1; i < parts.Length; i++)
          {
            Match m = this.g.Match(parts[i]);
            Trace.Assert(m.Success, MyConvert.Format("Fail to parse modification {0} of {1}", parts[i], value));
            var entry = new ModificationEntry()
            {
              Modification = m.Groups[1].Value,
              Aminoacid = m.Groups[2].Value
            };
            Add(entry);
            this._modificationMap[i] = entry.ToString();
          }
        }
      }
    }
  }

  public class PFindModification
  {
    private PFindModificationItem dynamicModification = new PFindModificationItem();
    private PFindModificationItem staticModification = new PFindModificationItem();

    public PFindModificationItem StaticModification
    {
      get { return this.staticModification; }
      set { this.staticModification = value; }
    }

    public PFindModificationItem DynamicModification
    {
      get { return this.dynamicModification; }
      set { this.dynamicModification = value; }
    }
  }
}