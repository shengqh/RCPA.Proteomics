using System.Collections.Generic;

namespace RCPA.Proteomics.Summary
{
  public class DtaselectResult
  {
    private List<string> headers = new List<string>();

    public List<string> Headers
    {
      get { return headers; }
    }

    private List<DtaselectProteinItem> proteins = new List<DtaselectProteinItem>();

    public List<DtaselectProteinItem> Proteins
    {
      get { return proteins; }
    }

    public Dictionary<string, DtaselectProteinItem> GetProteinMap()
    {
      Dictionary<string, DtaselectProteinItem> result = new Dictionary<string, DtaselectProteinItem>();
      foreach (DtaselectProteinItem protein in proteins)
      {
        result[protein.Name] = protein;
      }
      return result;
    }
  }
}
