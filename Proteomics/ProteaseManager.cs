using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

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
 */

namespace RCPA.Proteomics
{
  /**
   * Registry and utility methods for Proteases.
   * @author Mark Schreiber, modified by Quanhu Sheng
   */

  public sealed class ProteaseManager
  {
    private static readonly Dictionary<string, Protease> name2Protease = new Dictionary<string, Protease>();

    private ProteaseManager()
    { }

    static ProteaseManager()
    {
      Load();
    }

    /**
     * Creates and registers a new Protease. In future the Protease can be recovered
     * using the getProteaseByName() method.
     * @param name the name of the Protease
     * @param endoProtease is it an endo protease?
     * @param cleaveRes the cleavege residues
     * @param notCleaveRes the exceptions to the cleavage residues
     * @return a reference to the new Protease
     * @throws IllegalSymbolException if the cleaveRes or notCleaveRes are not
     * from the PROTEIN alphabet
     * @throws BioException if a Protease with the same name already exists.
     */

    public static Protease CreateProtease(
      string name,
      bool endoProtease,
      string cleaveRes,
      string notCleaveRes)
    {
      var p = new Protease(name, endoProtease, cleaveRes, notCleaveRes);
      RegisterProtease(p);
      return p;
    }

    public static Protease ValueOf(string line)
    {
      string[] lines = line.Split("\t".ToCharArray());
      if (lines.Length == 4)
      {
        return FindOrCreateProtease(lines[0], lines[1].Equals("1"), lines[2].Equals("-") ? "" : lines[2],
                                    lines[3].Equals("-") ? "" : lines[3]);
      }

      if (lines.Length == 8)
      {
        return FindOrCreateProtease(lines[0], lines[4].Equals("1"), lines[5].Equals("-") ? "" : lines[5],
                                    lines[7].Equals("-") ? "" : lines[7]);
      }

      throw new Exception(line + " is not a valid enzyme line");
    }

    public static Protease FindOrCreateProtease(
      string name,
      bool endoProtease,
      string cleaveRes,
      string notCleaveRes)
    {
      string newName = name;
      int count = 1;
      while (Registered(newName))
      {
        Protease p = GetProteaseByName(newName);
        if (p.IsEndoProtease == endoProtease && p.CleaveageResidues.Equals(cleaveRes) &&
            p.NotCleaveResidues.Equals(notCleaveRes))
        {
          return p;
        }

        newName = name + "_" + count;
        count++;
      }

      var result = new Protease(newName, endoProtease, cleaveRes, notCleaveRes);
      RegisterProtease(result);
      return result;
    }

    private static string GetShortName(string name)
    {
      StringBuilder sb = new StringBuilder();
      foreach (var c in name)
      {
        if (Char.IsLetterOrDigit(c))
        {
          sb.Append(Char.ToLower(c));
        }
      }
      return sb.ToString();
    }
    /**
     * Registers a protease and ensures its flyweight status
     * @param prot the Protease to register
     */
    public static void RegisterProtease(Protease protease)
    {
      var name = GetShortName(protease.Name);
      if (Registered(protease.Name))
      {
        throw new ArgumentException(
          "A Protease has already been registered with the name "
          + protease.Name, "protease");
      }
      name2Protease[name] = protease;
    }

    /**
     * Gets a Protease instance by name.
     * @param proteaseName the name of a registered Protease (case sensistive)
     * @return a fly-weight Protease instance
     * @throws BioException if no protease is registered by that name
     */

    public static Protease GetProteaseByName(string proteaseName)
    {
      var name = GetShortName(proteaseName);
      if (!name2Protease.ContainsKey(name))
      {
        throw new ArgumentException("No protease has been registered by name " + proteaseName, "proteaseName");
      }

      return name2Protease[name];
    }

    public static List<string> GetNames()
    {
      return name2Protease.Values.ToList().ConvertAll(m => m.Name).ToList();
    }

    /**
     * Has a Protease been registered with that name?
     * @param proteaseName the query
     * @return true if one has, false otherwise
     */

    public static bool Registered(string proteaseName)
    {
      var name = GetShortName(proteaseName);
      return name2Protease.ContainsKey(name);
    }

    private static string GetProteaseFile()
    {
      return FileUtils.AppPath() + "/proteases.xml";
    }

    public static void LoadFromFile(string proteaseFile)
    {
      name2Protease.Clear();
      XElement root = XElement.Load(proteaseFile);
      foreach (var ele in root.FindElements("protease"))
      {
        var ruleEles = ele.FindElements("rule");
        if (ruleEles == null || ruleEles.Count == 0)
        {
          continue;
        }

        var name = ele.FindAttribute("name").Value;
        var issemi = ele.FindAttribute("semispecific").Value;
        if (issemi.Equals("true"))
        {
          continue;
        }

        var isCterm = ruleEles[0].FindAttribute("sense").Value.Equals("C-Term");
        var cleavages = ruleEles[0].FindAttribute("cleave").Value;
        var notcleavages = ruleEles[0].FindAttribute("restrict").Value;

        CreateProtease(name, isCterm, cleavages, notcleavages);
      }
    }

    public static void Load()
    {
      LoadFromFile(GetProteaseFile());
    }
  }
}