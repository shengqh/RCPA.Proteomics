using System.Collections.Generic;
using System.IO;
using RCPA.Utils;
using System.Text.RegularExpressions;
using RCPA.Proteomics.PropertyConverter;
using RCPA.Proteomics.Summary;

namespace RCPA.Proteomics.Quantification.Census
{
  public class CensusStringResultFormat : IFileFormat<CensusStringResult>
  {
    public CensusStringResultFormat()
    { }

    #region IFileFormat<CensusStringResult> Members

    public CensusStringResult ReadFromFile(string fileName)
    {
      var result = new CensusStringResult();

      result.Headers = CensusUtils.ReadHeaders(fileName);
      result.Proteins = ReadProteins(fileName);

      return result;
    }

    public void WriteToFile(string filename, CensusStringResult t)
    {
      using (var sw = new StreamWriter(filename))
      {
        foreach (string header in t.Headers)
        {
          sw.Write(header + "\n");
        }

        foreach (ProteinGroupEntry cpi in t.Proteins)
        {
          foreach (string protein in cpi.Proteins)
          {
            sw.Write(protein + "\n");
          }

          foreach (string peptide in cpi.Peptides)
          {
            sw.Write(peptide + "\n");
          }
        }
      }
    }

    #endregion

    private List<ProteinGroupEntry> ReadProteins(string filename)
    {
      var result = new List<ProteinGroupEntry>();

      ProteinGroupEntry lastItem = null;
      using (var sr = new StreamReader(filename))
      {
        string lastLine = sr.ReadLine();
        int peptideCount = 0;
        while (lastLine != null)
        {
          lastLine = lastLine.Trim();

          if (lastLine.StartsWith("S") || lastLine.StartsWith("&S"))
          {
            lastItem.Peptides.Add(lastLine);
            peptideCount++;
          }
          else if (lastLine.StartsWith("P"))
          {
            if (peptideCount != 0 || lastItem == null)
            {
              lastItem = new ProteinGroupEntry();
              result.Add(lastItem);
              peptideCount = 0;
            }
            lastItem.Proteins.Add(lastLine);
          }

          lastLine = sr.ReadLine();
        }
      }

      return result;
    }
  }
}