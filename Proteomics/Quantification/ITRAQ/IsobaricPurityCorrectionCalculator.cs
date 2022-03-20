using RCPA.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace RCPA.Proteomics.Quantification.ITraq
{
  public class IsobaricImpurityCorrectionCalculator
  {
    public IsobaricType PlexType { get; private set; }

    private double[][] plexTable;

    public IsobaricImpurityCorrectionCalculator(IsobaricType plexType, string isotopeImpurityCorrectionTableFileName)
    {
      this.PlexType = plexType;
      this.plexTable = ParseTable(plexType, isotopeImpurityCorrectionTableFileName);
    }

    public static double[][] ParseTable(IsobaricType plexType, string isotopeImpurityFile)
    {
      var definition = plexType.GetDefinition();
      var items = definition.Items;
      string[] lines = File.ReadAllLines(isotopeImpurityFile);
      if (lines.Length < items.Length + 1)
      {
        throw new ArgumentException(string.Format("Wrong format of Isotope Impurity Correction Table File of {0}\n{1}", plexType, isotopeImpurityFile));
      }

      //Skip the header line
      lines = lines.Skip(1).ToArray();

      char splitChar;
      if (lines[0].Contains(','))
      {
        splitChar = ',';
      }
      else
      {
        splitChar = '\t';
      }

      //Each line should contain reagent name and five values (-2,-1,0,1,2)
      lines = (from l in lines
               let s = l.Trim().Split(splitChar)
               where s.Length == 6
               select l.Trim()).ToArray();

      if (lines.Length != items.Length)
      {
        throw new ArgumentException(string.Format("Length not equals between plex type {0} and impurity file {1}", plexType, isotopeImpurityFile));
      }

      var ignore = new List<int>();
      for (int i = definition.MaxIndex - 1; i > definition.MinIndex; i--)
      {
        if (!definition.IsValid(i))
        {
          ignore.Add(i - definition.MinIndex);
        }
      }

      int len = definition.MaxIndex - definition.MinIndex + 1;

      Dictionary<int, double[]> dic = new Dictionary<int, double[]>();
      int index = definition.MinIndex - 1;
      for (int i = 0; i < items.Length; i++)
      {
        var lst = new List<double>();
        for (int j = 0; j < len; j++)
        {
          lst.Add(0);
        }

        var p = GetIsotopes(lines[i], splitChar, ref index);

        var startPos = items[i].Index - definition.MinIndex - 2;

        var endPos = Math.Min(len, startPos + 5);
        var fromPos = Math.Max(0, startPos);

        for (int j = fromPos; j < endPos; j++)
        {
          var pIndex = j - startPos;
          lst[j] = p[pIndex];
        }

        foreach (var ign in ignore)
        {
          lst.RemoveAt(ign);
        }
        dic[index] = lst.ToArray();
      }

      return (from key in dic.Keys
              orderby key
              select dic[key]).ToArray();
    }

    private static Regex intReg = new Regex(@"(\d{3})");
    private static double[] GetIsotopes(string line, char splitChar, ref int index)
    {
      //First is reagent name, skip
      var parts = line.Split(splitChar);

      int curIndex = 0;
      if (int.TryParse(parts[0], out curIndex))
      {
        index = curIndex;
      }
      else
      {
        var m = intReg.Match(parts[0]);
        if (m.Success)
        {
          index = int.Parse(m.Groups[0].Value);
        }
        else
        {
          index++;
        }
      }

      var str = parts.Skip(1);

      var result = (from s in str
                    select MyConvert.ToDouble(s)).ToList();

      var sum = result.Sum();

      return (from r in result
              select r / sum).ToArray();
    }

    public double[][] CloneTable()
    {
      return (from vs in plexTable
              select (from v in vs
                      select v).ToArray()).ToArray();
    }

    public double[] Correct(double[] observed)
    {
      if (observed == null || observed.Length != plexTable.Length)
      {
        throw new ArgumentException(string.Format("Argument observed should be a length {0} array, current is {1}", plexTable.Length, observed.Length));
      }

      var x = new double[observed.Length];

      double rnorm = 0.0;
      NonNegativeLeastSquaresCalc.NNLS(CloneTable(), observed.Length, plexTable[0].Length, observed, x, out rnorm, null, null, null);

      return x;
    }

    public void Correct(IsobaricItem item)
    {
      if (item.PlexType != this.PlexType)
      {
        throw new ArgumentException(string.Format("Argument item should be {0}. Check your parameters!", this.PlexType));
      }

      var observed = item.ObservedIons();
      var x = Correct(observed);
      for (int i = 0; i < item.Definition.Items.Length; i++)
      {
        item[item.Definition.Items[i].Index] = Math.Max(ITraqConsts.NULL_INTENSITY, x[i]);
      }
    }
  }
}
