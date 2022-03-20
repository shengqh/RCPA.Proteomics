using RCPA.Proteomics.Utils;
using System;
using System.Collections.Generic;

namespace RCPA.Proteomics.Image
{
  public abstract class AbstractPhosphoNeutralLossMatcher : AbstractNeutralLossMatcher
  {
    private Dictionary<char, double> dynamicModifications;

    protected Dictionary<char, double> DynamicModifications { get { return dynamicModifications; } }

    public AbstractPhosphoNeutralLossMatcher(Dictionary<char, double> dynamicModifications, double peakMzTolerance)
      : base(peakMzTolerance)
    {
      this.dynamicModifications = dynamicModifications;
      this.NeutralLossLevel = 2;
    }

    /// <summary>
    /// 根据给定的匹配序列（包含修饰信息）以及指定位置，判断该位置是否是磷酸化修饰。
    /// </summary>
    /// <param name="matchedSequence"></param>
    /// <param name="aminoacidIndex"></param>
    /// <returns></returns>
    public bool IsPhospho(string matchedSequence, int aminoacidIndex)
    {
      if (dynamicModifications == null)
      {
        return false;
      }

      if (!PeptideUtils.IsModified(matchedSequence, aminoacidIndex))
      {
        return false;
      }

      char c = matchedSequence[aminoacidIndex + 1];

      if (dynamicModifications.ContainsKey(c))
      {
        double value = dynamicModifications[c];
        if (Math.Abs(value - 80.0) < 0.1)
        {
          return true;
        }
      }
      return false;
    }

    /// <summary>
    /// 根据给定的磷酸化修饰氨基酸序列，得到磷酸化中性丢失类型列表
    /// </summary>
    /// <param name="phosphoString"></param>
    /// <returns></returns>
    public List<INeutralLossType> GetPhosphoNeutralLossCandidates(string phosphoAminoacids)
    {
      List<INeutralLossType> result = new List<INeutralLossType>();
      foreach (char modifiedAminoacid in phosphoAminoacids)
      {
        if (('S' == modifiedAminoacid || 'T' == modifiedAminoacid))
        {
          result.Add(NeutralLossConstants.NL_H3PO4);
        }
        else if ('Y' == modifiedAminoacid)
        {
          result.Add(NeutralLossConstants.NL_HPO3);
        }

      }
      return result;
    }

    /// <summary>
    /// 根据给定的磷酸化修饰氨基酸序列，以及是否可以脱水、脱氨，得到中性丢失的类型列表
    /// </summary>
    /// <param name="phosphoString"></param>
    /// <param name="canLossWater"></param>
    /// <param name="canLossAmmonia"></param>
    /// <returns></returns>
    public List<INeutralLossType> GetPhosphoNeutralLossTypes(string phosphoAminoacids, bool canLossWater, bool canLossAmmonia)
    {
      List<INeutralLossType> result = GetPhosphoNeutralLossCandidates(phosphoAminoacids);

      result.AddRange(GetNeutralLossTypes(canLossWater, canLossAmmonia));

      return result;
    }
  }
}
