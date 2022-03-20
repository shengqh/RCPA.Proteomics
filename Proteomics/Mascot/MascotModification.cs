using RCPA.Proteomics.Modification;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace RCPA.Proteomics.Mascot
{
  public class MascotModificationEntry
  {
    public double DeltaMass { get; set; }
    public string Type { get; set; }
    public string Name { get; set; }
    public char Symbol { get; set; }
    public override string ToString()
    {
      return MyConvert.Format("{0:0.000000} {1} ({2})", DeltaMass, Name, Type);
    }
  }

  public class MascotModificationItem : List<MascotModificationEntry>
  {
    private string prefix;
    public MascotModificationItem(string prefix)
    {
      this.prefix = prefix;
    }

    private Regex reg = new Regex(@"(.+),(.+?)\s+\((.+)\)");

    public void AddModification(string value)
    {
      var match = reg.Match(value);
      if (!match.Success)
      {
        throw new Exception("Wrong mascot modification format : " + value);
      }

      Add(new MascotModificationEntry()
      {
        DeltaMass = MyConvert.ToDouble(match.Groups[1].Value),
        Name = match.Groups[2].Value.Trim(),
        Type = match.Groups[3].Value.Trim()
      });
    }

    public void Parse(Dictionary<string, string> values)
    {
      this.Clear();
      int index = 0;
      while (true)
      {
        index++;
        var key = prefix + index.ToString();
        if (!values.ContainsKey(key))
        {
          break;
        }

        AddModification(values[key]);
      }
    }
  }

  public class MascotModification
  {
    private MascotModificationItem dynamicModification = new MascotModificationItem("delta");
    private MascotModificationItem staticModification = new MascotModificationItem("FixedMod");

    public MascotModificationItem StaticModification
    {
      get { return this.staticModification; }
      set { this.staticModification = value; }
    }

    public MascotModificationItem DynamicModification
    {
      get { return this.dynamicModification; }
      set { this.dynamicModification = value; }
    }

    public void Parse(Dictionary<string, string> values)
    {
      this.dynamicModification.Parse(values);

      for (int i = 0; i < this.dynamicModification.Count; i++)
      {
        this.dynamicModification[i].Symbol = ModificationConsts.MODIFICATION_CHAR[i + 1];
      }

      this.staticModification.Parse(values);
    }
  }
}