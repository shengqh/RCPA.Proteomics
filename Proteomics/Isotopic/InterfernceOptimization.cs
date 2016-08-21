using RCPA.Proteomics.Spectrum;
using RCPA.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RCPA.Proteomics.Isotopic
{
  public static class InterfernceOptimization
  {
    /// <summary>
    /// http://www.jiniannet.com/Article/I11403285446793
    /// 根据观察到的离子，构建所有可能的isotopic组合
    /// </summary>
    /// <param name="array"></param>
    /// <returns></returns>
    public static List<List<T>> GenerateCandidates<T>(List<List<T>> array)
    {
      List<List<T>> result = new List<List<T>>();
      int[] indexArray = new int[array.Count];

      int pointer = 0;

      bool run = true;

      int lastIndex;

      while (run)
      {
        run = false;
        List<T> sbr = new List<T>();

        for (int i = 0; i < array.Count; i++)
        {
          if (indexArray[i] != array[i].Count - 1)
          {
            run = true;
          }
          sbr.Add(array[i][indexArray[i]]);
        }
        result.Add(sbr);

        lastIndex = array.Count - 1 - pointer;
        if (indexArray[lastIndex] < array[lastIndex].Count - 1)
        {
          indexArray[lastIndex]++;
          continue;//本级增长成功，继续循环
        }

        //如果本级增长到最大，则向上递增
        while (lastIndex > 0)
        {
          pointer++;
          lastIndex = array.Count - 1 - pointer;
          //递增成功
          if (indexArray[lastIndex] < array[lastIndex].Count - 1)
          {
            indexArray[lastIndex]++;
            for (int i = lastIndex + 1; i < array.Count; i++)
            {
              indexArray[i] = 0;
            }
            pointer = 0;
            break;
          }
        }
      }

      return result;
    }

    public static List<Peak> ResolveByPearsonCorrelation(double[] profile, List<List<Peak>> observed)
    {
      if (observed.Any(l => l.Count > 1))
      {
        var maxLength = Math.Min(profile.Length, observed.Count);
        var candidates = GenerateCandidates(observed);
        var correlations = (from obs in candidates
                            let real = obs.ConvertAll(l => l.Intensity).ToArray()
                            select StatisticsUtils.PearsonCorrelation(real, profile, maxLength)).ToArray();
        int maxIndex = 0;
        double maxCorr = 0;
        for (int i = 0; i < correlations.Length; i++)
        {
          if (correlations[i] > maxCorr)
          {
            maxCorr = correlations[i];
            maxIndex = i;
          }
        }

        return candidates[maxIndex];
      }
      else
      {
        return (from obs in observed select obs.First()).ToList();
      }
    }
    public static List<Peak> ResolveByKullbackLeiblerDistance(double[] profile, List<List<Peak>> observed)
    {
      if (observed.Any(l => l.Count > 1))
      {
        var maxLength = Math.Min(profile.Length, observed.Count);
        var candidates = GenerateCandidates(observed);
        var distances = (from obs in candidates
                         let real = obs.ConvertAll(l => l.Intensity).ToArray()
                         select StatisticsUtils.KullbackLeiblerDistance(real, profile, maxLength)).ToArray();
        int minIndex = 0;
        double minDistance = double.MaxValue;
        for (int i = 0; i < distances.Length; i++)
        {
          if (distances[i] < minDistance)
          {
            minDistance = distances[i];
            minIndex = i;
          }
        }

        return candidates[minIndex];
      }
      else
      {
        return (from obs in observed select obs.First()).ToList();
      }
    }
  }
}
