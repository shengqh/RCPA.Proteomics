using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace RCPA.Proteomics.Summary
{
  public class Peptide2GroupTextReader
  {
    public void LinkPeptideToGroup(string fileName, Dictionary<int, IIdentifiedSpectrum> spectra, Dictionary<int, IIdentifiedProteinGroup> groups)
    {
      if (!File.Exists(fileName))
      {
        throw new FileNotFoundException("File not exist : " + fileName);
      }

      long fileSize = new FileInfo(fileName).Length;

      char[] splitChar = {'\t'};

      using (var br = new StreamReader(fileName))
      {
        string line = br.ReadLine();

        while ((line = br.ReadLine()) != null)
        {
          if (0 == line.Trim().Length)
          {
            break;
          }

          string[] parts = line.Split(splitChar);
          if (parts.Length < 2)
          {
            break;
          }

          int spectraId = int.Parse(parts[0]);
          int groupId = int.Parse(parts[1]);

          if (!groups.ContainsKey(groupId))
          {
            throw new ArgumentException(MyConvert.Format("Cannot find group id {0} in groups.", groupId));
          }

          if (!spectra.ContainsKey(spectraId))
          {
            throw new ArgumentException(MyConvert.Format("Cannot find spectrum id {0} in spectra.", groupId));
          }

          groups[groupId].AddIdentifiedSpectrum(spectra[spectraId]);
        }
      }
    }


  }
}
