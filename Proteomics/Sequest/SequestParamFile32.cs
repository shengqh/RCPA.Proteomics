using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace RCPA.Proteomics.Sequest
{
  internal class SequestParamFile32 : AbstractSequestParamFile
  {
    protected override void ParseSpecial(SequestParam result, List<String> contents,
                                         Dictionary<string, Pair<string, string>> parameters)
    {
      result.Normalize_Score = int.Parse(parameters["normalize_xcorr"].First);

      ParseEnzyme(result, contents, parameters);
    }

    private void ParseEnzyme(SequestParam result, List<string> contents,
                             Dictionary<string, Pair<string, string>> parameters)
    {
      bool bFound = false;
      foreach (String line in contents)
      {
        if (line.StartsWith("enzyme_info"))
        {
          String[] parts = line.Split('=');

          String[] parts2 = Regex.Split(parts[1].Trim(), @"\s+");
          result.Protease = new Protease(parts2[0],
                                         parts2[2].Equals("1"),
                                         parts2[3].Equals("-") ? "" : parts2[3],
                                         parts2[4].Equals("-") ? "" : parts2[4]);
          bFound = true;
          break;
        }
      }

      if (!bFound)
      {
        throw new Exception("Cannot find enzyme information, are you sure it was generated from Bioworks 3.2 or upper?");
      }
    }
  }
}