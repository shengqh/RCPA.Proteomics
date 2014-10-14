using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RCPA.Proteomics.PeptideProphet
{
  public class PeptideProphetModifications : List<PeptideProphetModificationItem>
  {
    public void AssignModificationChar()
    {
      this.ForEach(m => m.ModificationChar = string.Empty);

      var variables = this.FindAll(m => m.IsVariable);

      int charIndex = 1;
      for (int i = 0; i < variables.Count; i++)
      {
        for (int j = 0; j < i; j++)
        {
          if (variables[i].MassDiff == variables[j].MassDiff)
          {
            variables[i].ModificationChar = variables[j].ModificationChar;
            break;
          }
        }

        if (variables[i].ModificationChar == string.Empty)
        {
          variables[i].ModificationChar = ModificationConsts.MODIFICATION_CHAR[charIndex++].ToString();
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
      return findMod.ModificationChar;
    }
  }
}
