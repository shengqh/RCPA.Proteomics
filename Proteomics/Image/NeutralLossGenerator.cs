using System;
using System.Collections.Generic;
using System.Linq;

namespace RCPA.Proteomics.Image
{
  public class NeutralLossGenerator
  {
    /// <summary>
    /// 根据给定的中性丢失类型候选者，以及给定的组合数，得到所有可能的组合（包括小于该给定组合数的组合类型）
    /// </summary>
    /// <param name="nlTypes"></param>
    /// <param name="maxLevel"></param>
    /// <returns></returns>
    public List<INeutralLossType> GetTotalCombinationValues(IEnumerable<INeutralLossType> nlTypes, int maxLevel)
    {
      List<INeutralLossType> result = new List<INeutralLossType>();
      for (int i = 1; i <= maxLevel; i++)
      {
        List<INeutralLossType> subResult = GetCombinationValues(nlTypes, i);

        foreach (INeutralLossType aType in subResult)
        {
          AddToDistinctList(result, aType);
        }
      }

      return result;
    }

    /// <summary>
    /// 添加到列表中，如果质量一样就保留组合类型最少的那个
    /// </summary>
    /// <param name="nlTypes">中性丢失类型列表</param>
    /// <param name="aType">添加的类型</param>
    public void AddToDistinctList(List<INeutralLossType> nlTypes, INeutralLossType aType)
    {
      INeutralLossType findType = nlTypes.Find(t => Math.Abs(t.Mass - aType.Mass) < 0.0001);
      if (null != findType)
      {
        if (findType.Count > aType.Count)
        {
          nlTypes.Remove(findType);
          nlTypes.Add(aType);
        }
      }
      else
      {
        nlTypes.Add(aType);
      }
    }

    /// <summary>
    /// 根据给定的中性丢失类型候选者，以及给定的组合数，得到该组合数的所有组合方式
    /// </summary>
    /// <param name="nlTypes"></param>
    /// <param name="numberOfCombination"></param>
    /// <returns></returns>
    public List<INeutralLossType> GetCombinationValues(IEnumerable<INeutralLossType> nlTypes, int numberOfCombination)
    {
      List<INeutralLossType> result = new List<INeutralLossType>();

      if (nlTypes.Count() >= numberOfCombination)
      {
        if (numberOfCombination == 1)
        {
          return nlTypes.Distinct().ToList();
        }

        var subTypes = nlTypes.Skip(1);

        INeutralLossType firstType = nlTypes.First();

        List<INeutralLossType> subs = GetCombinationValues(subTypes, numberOfCombination - 1);

        foreach (INeutralLossType aType in subs)
        {
          CombinedNeutralLossType curType = new CombinedNeutralLossType(aType);
          if (curType.InsertNeutralLossType(firstType))
          {
            AddToDistinctList(result, curType);
          }
        }

        List<INeutralLossType> combinedSubTypes = GetCombinationValues(subTypes, numberOfCombination);
        foreach (INeutralLossType aType in combinedSubTypes)
        {
          AddToDistinctList(result, aType);
        }
      }
      return result;
    }
  }
}
