using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace RCPA.Proteomics.Sequest
{
  internal class SequestParamFile31 : AbstractSequestParamFile
  {
    protected override void ParseSpecial(SequestParam result, List<string> contents,
                                         Dictionary<string, Pair<string, string>> parameters)
    {
      if (parameters.ContainsKey("normalize_Score"))
      {
        result.Normalize_Score = int.Parse(parameters["normalize_Score"].First);
      }
      else if (parameters.ContainsKey("normalize_score"))
      {
        result.Normalize_Score = int.Parse(parameters["normalize_score"].First);
      }

      ParseEnzyme(result, contents, parameters);
    }

    private void ParseEnzyme(SequestParam result, List<string> contents,
                             Dictionary<string, Pair<string, string>> parameters)
    {
      bool bFound = false;
      int enzymeNumber = 0;
      String enzymeLine = enzymeNumber + ".";
      foreach (String line in contents)
      {
        if (line.StartsWith("enzyme_number"))
        {
          String[] parts = line.Split('=');
          enzymeNumber = int.Parse(parts[1].Trim());
          enzymeLine = enzymeNumber + ".";
        }
        else if (line.StartsWith(enzymeLine))
        {
          String[] parts = Regex.Split(line, @"\s+");
          result.Protease = new Protease(parts[1],
                                         parts[2].Equals("1"),
                                         parts[3].Equals("-") ? "" : parts[3],
                                         parts[4].Equals("-") ? "" : parts[4]);
          bFound = true;
          break;
        }
      }

      if (!bFound)
      {
        throw new Exception("Cannot find enzyme information, are you sure it was generated from Bioworks 3.1 or lower?");
      }
    }
  }
}