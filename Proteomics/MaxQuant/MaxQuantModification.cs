using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace RCPA.Proteomics.MaxQuant
{
  public class MaxQuantModificationItem
  {
    public string Symbol { get; set; }
    public string FullName { get; set; }
    public string ShortName { get; set; }
    public AtomComposition Composition { get; set; }

    /// <summary>
    /// Write the modification to SILAC INI file
    /// </summary>
    /// <param name="sw"></param>
    public void WriteToSilacINI(StreamWriter sw)
    {
      sw.WriteLine("<{0}>\tSAM\tREF", Symbol);
      WriteAtom(sw, Atom.C, "C");
      WriteAtom(sw, Atom.H, "H");
      WriteAtom(sw, Atom.O, "O");
      WriteAtom(sw, Atom.N, "N");
      WriteAtom(sw, Atom.S, "S");
      WriteAtom(sw, Atom.P, "P");
      WriteAtom(sw, new[] { Atom.N15, Atom.Nx }, "15N");
      WriteAtom(sw, new[] { Atom.H2, Atom.Hx }, "2H");
      WriteAtom(sw, new[] { Atom.C13, Atom.Cx }, "13C");
      WriteAtom(sw, new[] { Atom.O18, Atom.Ox }, "18O", false);
      sw.WriteLine();
    }

    private void WriteAtom(StreamWriter sw, Atom atom, string atomName)
    {
      if (Composition.ContainsKey(atom))
      {
        sw.WriteLine("{0}\t{1}\t{1}", atomName, Composition[atom]);
      }
      else
      {
        sw.WriteLine("{0}\t0\t0", atomName);
      }
    }

    private void WriteAtom(StreamWriter sw, IList<Atom> atoms, string atomName, bool outputZero = true)
    {
      foreach (var atom in atoms)
      {
        if (Composition.ContainsKey(atom))
        {
          sw.WriteLine("{0}\t{1}\t{1}", atomName, Composition[atom]);
          return;
        }
      }

      if (outputZero)
      {
        sw.WriteLine("{0}\t0\t0", atomName);
      }
    }
  }

  public class MaxQuantModificationList : List<MaxQuantModificationItem>
  {
    public static MaxQuantModificationList ReadFromFile(string fileName)
    {
      var result = new MaxQuantModificationList();

      var doc = XDocument.Load(fileName);
      var items = (from mod in doc.Descendants("modification")
                   let fullName = mod.Attribute("title").Value
                   let shortName = "(" + fullName.Substring(0, 2).ToLower() + ")"
                   select new MaxQuantModificationItem()
                   {
                     FullName = fullName,
                     ShortName = shortName,
                     Composition = new AtomComposition(mod.Attribute("composition").Value.Replace(" ", "").Replace("(", "").Replace(")", ""))
                   }).ToList();
      result.AddRange(items);
      return result;
    }
  }
}
