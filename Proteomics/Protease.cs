using System.Collections.Generic;
using RCPA.Proteomics.Summary;
using System.Linq;

/*
 *                    BioJava development code
 *
 * This code may be freely distributed and modified under the
 * terms of the GNU Lesser General Public Licence.  This should
 * be distributed with the code.  If you do not have a copy,
 * see:
 *
 *      http://www.gnu.org/copyleft/lesser.html
 *
 * Copyright for this code is held jointly by the individual
 * authors.  These should be listed in @author doc comments.
 *
 * For more information on the BioJava project and its aims,
 * or to join the biojava-l mailing list, visit the home page
 * at:
 *
 *      http://www.biojava.org/
 *
 */

namespace RCPA.Proteomics
{
  /** The protease class stores parameters needed by Digest to digest a protein sequence.
   * A custom protease can be created or one derived from the attributes set in the
   * ProteaseManager.xml resource.
   * @author Michael Jones
   * @author Mark Schreiber (refactoring to ProteaseManager)
   */

  public class Protease : IProtease
  {
    private readonly string cleavageResidues;

    private readonly bool endoProtease = true;

    private readonly string name;
    private readonly string notCleaveResidues;

    public bool IsSemiSpecific { get; set; }

    public Protease(string name,
                    bool endoProtease,
                    string cleaveRes,
                    string notCleaveRes)
    {
      this.name = name;
      this.endoProtease = endoProtease;
      this.cleavageResidues = cleaveRes;
      this.notCleaveResidues = notCleaveRes;
      this.IsSemiSpecific = false;
    }

    /**
     * The list of residues that the protease will cleave at.
     * @return the residues as a SymbolList
     */

    public string CleaveageResidues
    {
      get { return this.cleavageResidues; }
    }

    /**
     * Gets the name of this Protease
     * @return the name as a String
     */

    public string Name
    {
      get { return this.name; }
    }


    /**
     * The list of residues that will prevent cleavage if they follow the cleavage
     * residue.
     */

    public string NotCleaveResidues
    {
      get { return this.notCleaveResidues; }
    }

    public bool IsEndoProtease
    {
      get { return this.endoProtease; }
    }

    public bool IsCleavageSite(char firstChar, char secondChar, char terminalChar)
    {
      if (firstChar == terminalChar || secondChar == terminalChar)
      {
        return true;
      }

      if (this.endoProtease)
      {
        if (-1 != this.cleavageResidues.IndexOf(firstChar) && -1 == this.notCleaveResidues.IndexOf(secondChar))
        {
          return true;
        }
      }
      else
      {
        if (-1 != this.cleavageResidues.IndexOf(secondChar) && -1 == this.cleavageResidues.IndexOf(firstChar))
        {
          return true;
        }
      }
      return false;
    }

    public int GetMissCleavageSiteCount(string sequence)
    {
      if (this.cleavageResidues.Length == 0 && this.notCleaveResidues.Length == 0) //NO_ENZYME
      {
        return 0;
      }

      int result = 0;
      for (int i = 0; i < sequence.Length - 1; i++)
      {
        if (IsCleavageSite(sequence[i], sequence[i + 1], '-'))
        {
          result++;
        }
      }
      return result;
    }

    public int GetProteaseTerminiCount(char beforeChar, string sequence, char afterChar, char terminalChar)
    {
      if (this.cleavageResidues.Length == 0 && this.notCleaveResidues.Length == 0) //NO_ENZYME
      {
        return 2;
      }

      int result = 0;

      if (IsCleavageSite(beforeChar, sequence[0], terminalChar))
      {
        result++;
      }

      if (IsCleavageSite(sequence[sequence.Length - 1], afterChar, terminalChar))
      {
        result++;
      }

      return result;
    }

    /**
     * Get the list of Protease names defined in the ProteaseManager
     * (Internally calls ProteaseManager.
     * @return A String array of protease names
     */

    public static List<string> GetProteaseList()
    {
      return ProteaseManager.GetNames();
    }

    /**
     * Retrieves a reference to the named Protease.
     * (Internally calls ProteaseManager.getProteaseByName())
     * @param proteaseName A protease name that is registered in the ProteaseManager (case sensitive)
     * @return A Protease instance for the given protease name
     */

    public static Protease GetProteaseByName(string proteaseName)
    {
      return ProteaseManager.GetProteaseByName(proteaseName);
    }
  }
}