using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace RCPA.Proteomics
{
  public class AtomComposition : Dictionary<Atom, int>
  {
    private static readonly Regex reg = new Regex(@"([A-Z][a-z]{0,1}|\([A-Z][a-z]{0,1}\d*\))(\d*)");

    public AtomComposition(string composition)
    {
      MatchCollection mc = reg.Matches(composition);
      foreach (Match m in mc)
      {
        Atom atom = Atom.ValueOf(m.Groups[1].Value);
        if (0 == m.Groups[2].Value.Length)
        {
          Add(atom, 1);
        }
        else
        {
          Add(atom, int.Parse(m.Groups[2].Value));
        }
      }
    }

    public string GetFormula()
    {
      var atoms = new List<Atom>(Keys);
      atoms.Sort();

      var sb = new StringBuilder();
      foreach (Atom atom in atoms)
      {
        sb.Append(atom.Symbol);
        if (this[atom] > 1)
        {
          sb.Append(this[atom]);
        }
      }

      return sb.ToString();
    }

    public string GetCHNOSFormula()
    {
      var sb = new StringBuilder();
      foreach (Atom atom in Atom.ItemCHNOS)
      {
        if (ContainsKey(atom))
        {
          int count = this[atom];
          if (count > 0)
          {
            sb.Append(atom.Symbol);
            if (this[atom] > 1)
            {
              sb.Append(this[atom]);
            }
          }
        }
      }

      return sb.ToString();
    }

    public override String ToString()
    {
      return GetFormula();
    }

    public int GetAtomCount(Atom atom)
    {
      if (ContainsKey(atom))
      {
        return this[atom];
      }
      else
      {
        return 0;
      }
    }

    public void Add(AtomComposition another)
    {
      foreach (Atom atom in another.Keys)
      {
        if (ContainsKey(atom))
        {
          this[atom] = this[atom] + another[atom];
        }
        else
        {
          Add(atom, another[atom]);
        }
      }
    }
  }
}