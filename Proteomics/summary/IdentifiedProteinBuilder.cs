using System.Collections.Generic;

namespace RCPA.Proteomics.Summary
{
  public class IdentifiedProteinBuilder : IIdentifiedProteinBuilder
  {
    public IdentifiedProteinBuilder()
    {
    }

    #region IIdentifiedProteinBuilder Members

    public List<IIdentifiedProtein> Build<T>(IEnumerable<T> spectra) where T : IIdentifiedSpectrumBase
    {
      Dictionary<string, IIdentifiedProtein> proteins = new Dictionary<string, IIdentifiedProtein>();
      HashSet<string> inserted = new HashSet<string>();
      foreach (var spectrum in spectra)
      {
        inserted.Clear();
        foreach (var peptide in spectrum.Peptides)
        {
          foreach (string ac in peptide.Proteins)
          {
            //如果一个蛋白中多个肽段都对应了这个谱图，只选择第一个肽段加入谱图
            if (inserted.Contains(ac))
            {
              continue;
            }
            inserted.Add(ac);

            if (!proteins.ContainsKey(ac))
            {
              IIdentifiedProtein pro = new IdentifiedProtein();
              pro.Name = ac;
              proteins[ac] = pro;
            }

            proteins[ac].Peptides.Add(peptide);
          }
        }
      }

      return new List<IIdentifiedProtein>(proteins.Values);
    }

    #endregion
  }
}
