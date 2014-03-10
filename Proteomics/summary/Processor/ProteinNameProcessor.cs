using System;
using System.Collections.Generic;
using System.Text;
using RCPA.Proteomics.Summary;
using System.Text.RegularExpressions;

namespace RCPA.Proteomics.Summary.Processor
{
  /// <summary>
  /// 根据给定的蛋白质描述信息优选顺序保留蛋白质群中的蛋白质
  /// </summary>
  public class ProteinNameProcessor : IProcessor<IIdentifiedProteinGroup>
  {
    protected readonly Regex[] perferReferenceRegeies;

    public ProteinNameProcessor()
      : this(new string[] { "SWISS-PROT", "|sp|", "REFSEQ_NP", "|ref|NP" })
    { }

    public ProteinNameProcessor(string[] perferReferenceRegexString)
    {
      this.perferReferenceRegeies = new Regex[perferReferenceRegexString.Length];

      for (int i = 0; i < perferReferenceRegexString.Length; i++)
      {
        this.perferReferenceRegeies[i] = new Regex(perferReferenceRegexString[i]);
      }
    }

    #region IProcessor<IIdentifiedProteinGroup> Members

    public IIdentifiedProteinGroup Process(IIdentifiedProteinGroup group)
    {
      List<IIdentifiedProtein> proteins = new List<IIdentifiedProtein>(group);

      foreach (Regex reg in perferReferenceRegeies)
      {
        if (proteins.Count == 1)
        {
          break;
        }

        List<IIdentifiedProtein> oldproteins = proteins;
        proteins = new List<IIdentifiedProtein>();

        foreach (IIdentifiedProtein protein in oldproteins)
        {
          if (reg.Match(protein.Reference).Success)
          {
            proteins.Add(protein);
          }
        }

        if (proteins.Count == 0)
        {
          proteins = oldproteins;
        }
      }

      for (int i = group.Count - 1; i >= 0; i--)
      {
        if (proteins.Contains(group[i]))
        {
          continue;
        }

        group.RemoveAt(i);
      }

      return group;
    }

    #endregion
  }
}
