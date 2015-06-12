using System.Collections.Generic;
using System.Linq;
using RCPA.Gui;

namespace RCPA.Proteomics.Summary
{
  public class IdentifiedProteinGroupBuilder : ProgressClass, IIdentifiedProteinGroupBuilder
  {
    #region IIdentifiedProteinGroupBuilder Members

    public List<IIdentifiedProteinGroup> Build(List<IIdentifiedProtein> proteins)
    {
      var result = new List<IIdentifiedProteinGroup>();

      var groupMap = new Dictionary<IIdentifiedProteinGroup, HashSet<IIdentifiedSpectrum>>();
      foreach (IIdentifiedProtein protein in proteins)
      {
        IIdentifiedProteinGroup group = new IdentifiedProteinGroup();
        group.Add(protein);

        var spectraSet = new HashSet<IIdentifiedSpectrum>(protein.GetSpectra());
        groupMap[group] = spectraSet;

        result.Add(group);
      }

      result.Sort((m1, m2) =>
      {
        int ret = -m1[0].PeptideCount.CompareTo(m2[0].PeptideCount);
        if (ret == 0)
        {
          ret = -m1[0].UniquePeptideCount.CompareTo(m2[0].UniquePeptideCount);
        }
        return ret;
      });

      Progress.SetMessage("Merging proteins with same peptide-spectrum matches ...");

      Progress.SetRange(0, result.Count);
      //首先合并所有内容相同的group
      for (int i = result.Count - 1; i > 0; i--)
      {
        Progress.SetPosition(result.Count - i);
        HashSet<IIdentifiedSpectrum> iSpectra = groupMap[result[i]];
        for (int j = i - 1; j >= 0; j--)
        {
          if (result[j][0].PeptideCount == result[i][0].PeptideCount &&
            result[j][0].UniquePeptideCount == result[i][0].UniquePeptideCount)
          {
            HashSet<IIdentifiedSpectrum> jSpectra = groupMap[result[j]];

            if (jSpectra.SetEquals(iSpectra))
            {
              //如果内容一致，则合并两个group
              foreach (IIdentifiedProtein protein in result[i])
              {
                result[j].Add(protein);
              }

              //删除group i
              result.RemoveAt(i);
              break;
            }
          }
          else
          {
            break;
          }
        }
      }

      Progress.SetMessage("Removing redundant protein groups ...");
      Progress.SetRange(0, result.Count);

      InitializePeptideGroupCount(result);

      var temp = result;
      result = new List<IIdentifiedProteinGroup>();
      for (int i = temp.Count - 1; i > 0; i--)
      {
        if (temp[i].GetPeptides().All(m => m.GroupCount == 1))
        {
          result.Add(temp[i]);
          temp.RemoveAt(i);
        }
      }


      //删除被包含的group
      for (int i = temp.Count - 1; i > 0; i--)
      {
        Progress.SetPosition(temp.Count - i);
        HashSet<IIdentifiedSpectrum> iSpectra = groupMap[temp[i]];
        for (int j = i - 1; j >= 0; j--)
        {
          HashSet<IIdentifiedSpectrum> jSpectra = groupMap[temp[j]];
          if (jSpectra.Count == iSpectra.Count)
          {
            continue;
          }

          if (jSpectra.IsSupersetOf(iSpectra))
          {
            //删除group i
            temp.RemoveAt(i);
            break;
          }
        }
      }

      //删除没有unique peptide的group
      while (true)
      {
        InitializePeptideGroupCount(temp);
        bool bFind = false;
        for (int i = temp.Count - 1; i > 0; i--)
        {
          if (temp[i].GetPeptides().All(m => m.GroupCount > 1))
          {
            bFind = true;
            temp.RemoveAt(i);
            break;
          }
        }
        if (!bFind)
        {
          break;
        }
      }

      result.AddRange(temp);

      result.ForEach(m => m.SortByProteinName());

      return result;
    }

    private static void InitializePeptideGroupCount(List<IIdentifiedProteinGroup> result)
    {
      result.ForEach(m =>
      {
        foreach (var pep in m.GetPeptides())
        {
          pep.GroupCount = 0;
        }
      });

      result.ForEach(m =>
      {
        foreach (var pep in m.GetPeptides())
        {
          pep.GroupCount++;
        }
      });

    }

    #endregion
  }
}