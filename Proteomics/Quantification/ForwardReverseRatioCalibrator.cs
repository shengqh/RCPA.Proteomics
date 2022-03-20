using RCPA.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace RCPA.Proteomics.Quantification
{
  internal class RatioItem
  {
    private List<double> _ratios = new List<double>();

    public string Name { get; set; }

    public List<double> Ratios { get { return _ratios; } }

    public double CalibratedRatio { get; set; }
  }

  internal class RatioList : List<RatioItem>
  {
    private List<string> _classificationNames = new List<string>();

    public List<string> ClassificationNames { get { return _classificationNames; } }
  }

  public class ForwardReverseRatioCalibrator : AbstractThreadFileProcessor
  {
    public override IEnumerable<string> Process(string fileName)
    {
      RatioList ratios = new RatioList();
      using (StreamReader sr = new StreamReader(fileName))
      {
        var line = sr.ReadLine();
        var splitChar = new[] { '\t' };
        var parts = line.Split(splitChar);
        ratios.ClassificationNames.AddRange(parts.Skip(1).Take(parts.Length - 3));

        while ((line = sr.ReadLine()) != null)
        {
          parts = line.Split(splitChar);
          RatioItem item = new RatioItem();
          item.Name = parts[0];
          for (int i = 1; i < parts.Length; i++)
          {
            double value;
            if (MyConvert.TryParse(parts[i], out value))
            {
              item.Ratios.Add(value);
            }
            else
            {
              item.Ratios.Add(double.NaN);
            }
          }

          ratios.Add(item);
        }
      }

      foreach (var m in ratios)
      {
        var rs = (from r in m.Ratios
                  where !double.IsNaN(r)
                  select r).ToList();

        if ((rs.Count % 2) == 1)
        {
          rs.Remove(0);
        }

        if (rs.Count == 0)
        {
          m.CalibratedRatio = double.NaN;
          continue;
        }

        if (rs.Count > 2)
        {
          var first = rs.Take(rs.Count / 2).Average();
          var last = rs.Skip(rs.Count / 2).Average();
          rs = new[] { first, last }.ToList();
        }

        m.CalibratedRatio = MathUtils.CarlibrateForwardReverseRatio(rs[0], rs[1]);
      }

      var result = fileName + ".calibrated";
      using (StreamWriter sw = new StreamWriter(result))
      {
        sw.WriteLine("Name\tCalibratedRatio");
        foreach (var m in ratios)
        {
          sw.WriteLine("{0}\t{1:0.0000}", m.Name, m.CalibratedRatio);
        }
      }

      return new[] { result };
    }
  }
}
