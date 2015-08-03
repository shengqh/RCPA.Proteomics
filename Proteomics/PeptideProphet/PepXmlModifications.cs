using RCPA.Proteomics.Mascot;
using RCPA.Proteomics.Modification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RCPA.Proteomics.PeptideProphet
{
  public class PepXmlModifications : List<PepXmlModificationItem>
  {
    public PepXmlModifications() { }

    public PepXmlModifications(MascotModification mods)
    {
      var aas = new Aminoacids();
      foreach (var mod in mods.StaticModification)
      {
        var item = new PepXmlModificationItem()
        {
          IsVariable = false
        };
        AssignModification(aas, mod, item);
        this.Add(item);
      }

      foreach (var mod in mods.DynamicModification)
      {
        var item = new PepXmlModificationItem()
        {
          IsVariable = true,
          Symbol = mod.Symbol.ToString()
        };
        AssignModification(aas, mod, item);
        this.Add(item);
      }
    }

    private static void AssignModification(Aminoacids aas, MascotModificationEntry mod, PepXmlModificationItem item)
    {
      if (mod.Type.Equals("N-term"))
      {
        item.IsProteinTerminal = true;
        item.IsTerminalN = true;
        item.IsAminoacid = false;
        item.MassDiff = mod.DeltaMass;
        item.Mass = Atom.H.MonoMass + item.MassDiff;
      }
      else if (mod.Type.Equals("C-term"))
      {
        item.IsProteinTerminal = true;
        item.IsTerminalN = false;
        item.IsAminoacid = false;
        item.MassDiff = mod.DeltaMass;
        item.Mass = Atom.H.MonoMass + Atom.O.MonoMass + item.MassDiff;
      }
      else
      {
        item.IsProteinTerminal = false;
        item.IsTerminalN = false;
        item.IsAminoacid = true;
        item.Aminoacid = mod.Name;
        item.MassDiff = mod.DeltaMass;
        item.Mass = aas[mod.Type[0]].MonoMass + item.MassDiff;
      }
    }

    public void AssignModificationChar()
    {
      this.ForEach(m => m.Symbol = string.Empty);

      var variables = this.FindAll(m => m.IsVariable);

      int charIndex = 1;
      for (int i = 0; i < variables.Count; i++)
      {
        for (int j = 0; j < i; j++)
        {
          if (variables[i].MassDiff == variables[j].MassDiff)
          {
            variables[i].Symbol = variables[j].Symbol;
            break;
          }
        }

        if (variables[i].Symbol == string.Empty)
        {
          variables[i].Symbol = ModificationConsts.MODIFICATION_CHAR[charIndex++].ToString();
        }
      }
    }

    public bool HasModification(double mass)
    {
      return this.Find(m => m.Mass == mass) != null;
    }

    public string FindModificationChar(double mass)
    {
      var findMod = this.Find(m => m.Mass == mass);
      if (findMod == null)
      {
        throw new Exception(MyConvert.Format("Cannot find the modification corresponding to mass {0:0.000000}", mass));
      }
      return findMod.Symbol;
    }
  }
}
