using System.IO;

namespace RCPA.Proteomics.Summary
{
  public class DtaselectResultReader
  {
    public DtaselectResult ReadFromFile(string filename)
    {
      DtaselectResult result = new DtaselectResult();

      using (StreamReader sr = new StreamReader(filename))
      {
        string lastLine;
        while ((lastLine = sr.ReadLine()) != null)
        {
          result.Headers.Add(lastLine);
          if (lastLine.StartsWith("Unique\tFileName"))
          {
            break;
          }
        }

        lastLine = sr.ReadLine();
        while (lastLine != null && lastLine.Trim().Length != 0 && !lastLine.StartsWith("\tProteins"))
        {
          string[] proteinParts = lastLine.Split(new char[] { '\t' });

          DtaselectProteinItem protein = new DtaselectProteinItem();
          protein.Context = lastLine;
          protein.Name = proteinParts[0];
          result.Proteins.Add(protein);

          while ((lastLine = sr.ReadLine()) != null)
          {
            if (!lastLine.StartsWith("\t"))
            {
              break;
            }
            if (lastLine.StartsWith("\tProteins"))
            {
              break;
            }

            string[] parts = lastLine.Split(new char[] { '\t' });
            DtaselectPeptideItem peptide = new DtaselectPeptideItem();
            peptide.Context = lastLine;
            peptide.Filename = parts[1].Trim();

            protein.Peptides.Add(peptide);
          }
        }
      }

      return result;
    }
  }
}
