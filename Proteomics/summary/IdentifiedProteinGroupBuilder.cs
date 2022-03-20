using RCPA.Gui;
using System.Collections.Generic;
using System.Linq;

namespace RCPA.Proteomics.Summary
{
  public class IdentifiedProteinGroupBuilder : ProgressClass, IIdentifiedProteinGroupBuilder
  {
    #region IIdentifiedProteinGroupBuilder Members

    public List<IIdentifiedProteinGroup> Build(List<IIdentifiedProtein> proteins)
    {
      var result = new List<IIdentifiedProteinGroup>();

      Progress.SetMessage("Initializing protein group/spectra map ...");
      var groupMap = new Dictionary<IIdentifiedProteinGroup, HashSet<IIdentifiedSpectrum>>();
      foreach (IIdentifiedProtein protein in proteins)
      {
        IIdentifiedProteinGroup group = new IdentifiedProteinGroup();
        group.Add(protein);

        var spectraSet = new HashSet<IIdentifiedSpectrum>(protein.GetSpectra());
        groupMap[group] = spectraSet;

        result.Add(group);
      }

      Progress.SetMessage("Sorting protein groups ...");
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

      Progress.SetMessage("Initializing peptide group count ...");
      InitializePeptideGroupCount(result);

      Progress.SetMessage("Extracting distinct protein groups ...");
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
      Progress.SetMessage("There are {0} distinct and {1} undistinct protein groups. ", result.Count, temp.Count);

      Progress.SetMessage("Removing redundant protein groups from undistinct protein groups...");

      var oldcount = temp.Count;
      Progress.SetRange(0, oldcount);
      //删除被包含的group
      for (int i = temp.Count - 1; i > 0; i--)
      {
        Progress.SetPosition(oldcount - i);
        HashSet<IIdentifiedSpectrum> iSpectra = groupMap[temp[i]];
        for (int j = i - 1; j >= 0; j--)
        {
          HashSet<IIdentifiedSpectrum> jSpectra = groupMap[temp[j]];
          if (jSpectra.Count == iSpectra.Count)
          {
            continue;
          }

          if (iSpectra.All(l => jSpectra.Contains(l)))
          {
            //删除group i
            temp.RemoveAt(i);
            break;
          }
        }
      }

      RemoveUndistinctProteinGroups(temp);

      result.AddRange(temp);

      Progress.SetMessage("Sorting proteins in group ...");
      result.ForEach(m => m.SortByProteinName());

      Progress.SetMessage("Building protein groups done.");
      return result;
    }

    protected virtual void RemoveUndistinctProteinGroups(List<IIdentifiedProteinGroup> temp)
    {
      Progress.SetMessage("Removing protein groups without any distinct peptide in {0} undistinct protein groups. ", temp.Count);
      //删除没有unique peptide的group
      InitializePeptideGroupCount(temp);
      for (int i = temp.Count - 1; i > 0; i--)
      {
        if (temp[i][0].Peptides.All(m => m.Spectrum.GroupCount > 1))
        {
          foreach (var pep in temp[i].GetPeptides())
          {
            pep.GroupCount--;
          }
          temp.RemoveAt(i);
        }
      }
    }

    protected void InitializePeptideGroupCount(List<IIdentifiedProteinGroup> result)
    {
      foreach (var pg in result)
      {
        foreach (var p in pg)
        {
          foreach (var pep in p.Peptides)
          {
            pep.Spectrum.GroupCount = 0;
          }
        }
      }

      foreach (var m in result)
      {
        foreach (var pep in m[0].GetSpectra())
        {
          pep.GroupCount++;
        }
      }
    }

    #endregion
  }
}