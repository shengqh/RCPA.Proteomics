using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace RCPA.Proteomics.Sequest
{
  public class SequestOutHeaderParser
  {
    private Regex dynamicModificationPattern = new Regex("\\(\\S+(\\S)\\s([\\+|-]\\d+\\.{0,1}\\d+)\\)");

    private Regex staticModificationPattern = new Regex("([A-Z])=(\\d+\\.{0,1}\\d+)");

    private Regex massTypePattern = new Regex("fragment\\stol.+?(\\S+)/(\\S+)");

    public Dictionary<char, double> ParseDynamicModification(string line)
    {
      var result = new Dictionary<char, double>();

      Match dMatcher = dynamicModificationPattern.Match(line);
      while (dMatcher.Success)
      {
        result[dMatcher.Groups[1].Value[0]] = MyConvert.ToDouble(dMatcher.Groups[2].Value);
        dMatcher = dMatcher.NextMatch();
      }

      return result;
    }

    public Dictionary<char, double> ParseStaticModification(string line)
    {
      var result = new Dictionary<char, double>();
      Match sMatcher = staticModificationPattern.Match(line);
      while (sMatcher.Success)
      {
        result[sMatcher.Groups[1].Value[0]] = MyConvert.ToDouble(sMatcher.Groups[2].Value);
        sMatcher = sMatcher.NextMatch();
      }

      return result;
    }

    public bool ParseMassType(string line, out bool precursorMonoMass, out bool peakMonoMass)
    {
      Match matcher = massTypePattern.Match(line);
      if (matcher.Success)
      {
        precursorMonoMass = !matcher.Groups[1].Value.Equals("AVG");
        peakMonoMass = !matcher.Groups[2].Value.Equals("AVG");
        return true;
      }
      else
      {
        precursorMonoMass = true;
        peakMonoMass = true;
        return false;
      }
    }
  }
}
