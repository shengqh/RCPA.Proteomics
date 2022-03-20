using System;
using System.Collections.Generic;

namespace RCPA.Proteomics.Summary
{
  public delegate bool CheckPeptideEnabled(IIdentifiedPeptide mph);

  public interface IIdentifiedProtein : IAnnotation, ICloneable, IComparable<IIdentifiedProtein>
  {
    /// <summary>
    /// 取出鉴定来自于哪些spectrum。
    /// </summary>
    List<IIdentifiedSpectrum> GetSpectra();

    /// <summary>
    /// 由于同一个spectrum可能鉴定到同一个蛋白质中非常相似的两个肽段（例如仅仅I和L的差别），
    /// 这两个（或多个）肽段的分数完全相同。这些冗余肽段结果会都保留在Peptides中。
    /// </summary>
    List<IIdentifiedPeptide> Peptides { get; set; }

    /// <summary>
    /// 取出非冗余的肽段。对于同一个spectrum鉴定到的多个肽段，只保留第一个。
    /// </summary>
    IEnumerable<IIdentifiedPeptide> GetDistinctPeptides();

    /// <summary>
    /// Gets and sets GroupIndex
    /// </summary>
    int GroupIndex { get; set; }

    //string AccessNumber { get; set; }

    /// <summary>
    /// Gets and Sets 蛋白名
    /// </summary>
    string Name { get; set; }

    /// <summary>
    /// Gets and Sets FromDecoy
    /// </summary>
    bool FromDecoy { get; set; }

    /// <summary>
    /// Gets and Sets 蛋白描述
    /// </summary>
    string Description { get; set; }

    /// <summary>
    /// Gets and Sets 蛋白质在数据库中的说明，通常为Name + 空格 + Description
    /// </summary>
    string Reference { get; set; }

    /// <summary>
    /// Gets and Sets 蛋白质序列
    /// </summary>
    string Sequence { get; set; }

    /// <summary>
    /// Gets and Sets 蛋白质平均分子量
    /// </summary>
    double MolecularWeight { get; set; }

    /// <summary>
    /// Gets and Sets 蛋白质等电点，采用Expasy算法
    /// </summary>
    double IsoelectricPoint { get; set; }

    /// <summary>
    /// Gets and Sets 鉴定覆盖率
    /// </summary>
    double Coverage { get; set; }

    /// <summary>
    /// Gets and Sets 对应的谱图数目。当包含来源于多个搜索引擎的结果是，与通过GetDistinctPeptides()获得的谱图数目可能不一样。
    /// </summary>
    int PeptideCount { get; set; }

    /// <summary>
    /// Gets and Sets 序列唯一的肽段数目
    /// </summary>
    int UniquePeptideCount { get; set; }

    /// <summary>
    /// Gets and Sets 鉴定分数
    /// </summary>
    double Score { get; set; }

    /// <summary>
    /// 根据给定Mascot Query号查找Peptide。
    /// </summary>
    /// <param name="query"></param>
    /// <returns></returns>
    IIdentifiedPeptide FindPeptideByQuery(int query);

    /// <summary>
    /// 按照默认比较方法进行肽段排序
    /// </summary>
    void SortPeptides();

    /// <summary>
    /// 初始化序列唯一肽段个数
    /// </summary>
    void InitUniquePeptideCount();

    /// <summary>
    /// 根据func对肽段进行筛选，然后初始化筛选后的序列唯一肽段个数
    /// </summary>
    /// <param name="func"></param>
    void InitUniquePeptideCount(Func<IIdentifiedPeptide, bool> func);

    /// <summary>
    /// 计算覆盖率
    /// </summary>
    void CalculateCoverage();
  }
}