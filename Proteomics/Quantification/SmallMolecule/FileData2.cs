using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Drawing;
using RCPA.Utils;

namespace RCPA.Proteomics.Quantification.SmallMolecule
{

  /// <summary>
  /// Dictionary<PEAK, List<PeakItem>>
  /// </summary>
  public class FileData2 : Dictionary<string, List<PeakItem>>
  {
    public string FileName { get; set; }

    public double TotalIntensity { get; set; }

    public static FileData2 ReadFromFile(string fileName)
    {
      FileData2 result = new FileData2();
      result.Read(fileName);
      return result;
    }

    private Dictionary<string, double> maxIntensityMap;

    public Dictionary<string, double> MaxIntensityMap
    {
      get
      {
        if (maxIntensityMap == null)
        {
          InitMaxIntensityMap();
        }
        return maxIntensityMap;
      }
    }

    public void InitMaxIntensityMap()
    {
      maxIntensityMap = new Dictionary<string, double>();

      foreach (var p in this)
      {
        maxIntensityMap[p.Key] = (from v in p.Value select v.SmoothedIntensity).Max();
      }
    }

    public void Smooth()
    {
      foreach (var v in this.Values)
      {
        double[] intensities = (from item in v select item.Intensity).ToArray();
        var smoothedIntensities = MathUtils.SavitzkyGolay7Point(intensities);
        for (int i = 0; i < v.Count; i++)
        {
          v[i].SmoothedIntensity = smoothedIntensities[i];
        }
      }
      InitMaxIntensityMap();
    }

    public void Read(string fileName)
    {
      Read(fileName, null);
    }

    public void Read(string fileName, IEnumerable<string> keptPeaks)
    {
      Clear();

      this.FileName = new FileInfo(fileName).Name;

      using (StreamReader sr = new StreamReader(fileName))
      {
        string line = sr.ReadLine();
        string[] mzStr = line.Split('\t');
        double[] mzs = new double[mzStr.Length];
        for (int i = 0; i < mzStr.Length; i++)
        {
          if (char.IsLetter(mzStr[i][0]))
          {
            mzs[i] = MyConvert.ToDouble(mzStr[i].Substring(1));
          }
          else
          {
            mzs[i] = MyConvert.ToDouble(mzStr[i]);
          }
        }

        this.TotalIntensity = mzs[0];

        List<int> keptIndecies = new List<int> ();

        if (keptPeaks == null)
        {
          for (int i = 1; i < mzStr.Length; i++)
          {
            keptIndecies.Add(i);
          }
        }
        else
        {
          foreach (string p in keptPeaks)
          {
            var index = Array.FindIndex(mzStr, m=>m.Equals(p));
            if (index != -1)
            {
              keptIndecies.Add(index);
            }
          }
        }

        foreach (var i in keptIndecies)
        {
          this[mzStr[i]] = new List<PeakItem>();
        }

        while ((line = sr.ReadLine()) != null)
        {
          if (line.Trim().Length == 0)
          {
            break;
          }

          string[] parts = line.Split('\t');

          int scan = int.Parse(parts[0]);
          foreach (var i in keptIndecies)
          {

            this[mzStr[i]].Add(new PeakItem(scan, mzs[i], MyConvert.ToDouble(parts[i])));
          }
        }
      }

      InitMaxIntensityMap();
    }

    public static List<FileData2> ReadFiles(IEnumerable<string> fileNames, IEnumerable<string> keptPeaks)
    {
      List<FileData2> result = new List<FileData2>();

      foreach (var fileName in fileNames)
      {
        Console.WriteLine("Reading " + fileName + " ...");
        var data = new FileData2();
        data.Read(fileName, keptPeaks);
        result.Add(data);
      }

      return result;
    }
  }
}
