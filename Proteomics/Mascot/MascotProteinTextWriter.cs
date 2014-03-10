using System.Collections.Generic;
using System.Text;
using RCPA.Proteomics.Summary;
using RCPA.Utils;

namespace RCPA.Proteomics.Mascot
{
  public class MascotProteinTextWriter
  {
    private readonly List<string> _annotationKeys;

    public MascotProteinTextWriter(IEnumerable<IIdentifiedProtein> items)
    {
      this._annotationKeys = AnnotationUtils.GetAnnotationKeys(items);
    }

    public string GetHeader()
    {
      var sb = new StringBuilder();
      sb.Append("Name\tMass\tTotalScore\tUniquePeptideCount\tPepCount\tDescription");
      foreach (string key in this._annotationKeys)
      {
        sb.Append("\t" + key);
      }
      return sb.ToString();
    }

    public string GetProteinString(IIdentifiedProtein mpro)
    {
      var sb = new StringBuilder();

      sb.Append(MyConvert.Format("{0}\t{1:0.0}\t{2}\t{3}\t{4}\t{5}",
                              mpro.Name,
                              mpro.MolecularWeight,
                              mpro.Score,
                              mpro.UniquePeptideCount,
                              mpro.Peptides.Count,
                              mpro.Description));

      foreach (string key in this._annotationKeys)
      {
        if (mpro.Annotations.ContainsKey(key))
        {
          sb.Append("\t" + mpro.Annotations[key]);
        }
        else
        {
          sb.Append("\t");
        }
      }
      return sb.ToString();
    }
  }
}