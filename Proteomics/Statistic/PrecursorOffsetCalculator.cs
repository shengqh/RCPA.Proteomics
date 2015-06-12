using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RCPA.Proteomics.Mascot;
using MathNet.Numerics.Statistics;
using System.IO;
using RCPA.Proteomics.Summary;

namespace RCPA.Proteomics.Statistic
{
  public class PrecursorOffsetCalculator : AbstractThreadFileProcessor
  {
    private static readonly double PPM_TOLERANCE = 100;

    private List<IIdentifiedSpectrum> spectra;

    public PrecursorOffsetCalculator(List<IIdentifiedSpectrum> spectra = null)
    {
      this.spectra = spectra;
    }

    public override IEnumerable<string> Process(string fileName)
    {
      Progress.SetMessage("Processing precursor offset of " + fileName + "...");

      Dictionary<string, Dictionary<int, List<double>>> ppmMap;
      if (this.spectra == null)
      {
        ppmMap = GetPPMMapFromFile(fileName);
      }
      else
      {
        ppmMap = GetPPMMapFromSpectra();
      }

      var result = fileName + ".offset";
      using (var sw = new StreamWriter(result))
      {
        sw.WriteLine("##Offset=ppm(TheoreticalMass-ExperimentalMass)");
        sw.WriteLine("File\tIsotope\tMeanOffset\tSDOffset\tMedianOffset");
        var exps = ppmMap.Keys.OrderBy(m => m).ToList();
        foreach (var exp in exps)
        {
          var dic = ppmMap[exp];
          var isotopics = dic.Keys.OrderBy(m => m).ToList();
          foreach (var isotopic in isotopics)
          {
            var lst = dic[isotopic];
            var mean = Statistics.Mean(lst);
            var sd = Statistics.StandardDeviation(lst);
            var median = Statistics.Median(lst);
            sw.WriteLine("{0}\t{1}\t{2:0.00}\t{3:0.00}\t{4:0.00}", exp, isotopic, mean, sd, median);
            lst.Clear();
          }
          dic.Clear();
        }
        ppmMap.Clear();
        ppmMap = null;
      }

      System.GC.Collect();

      Progress.End();

      return new string[] { result };
    }

    private Dictionary<string, Dictionary<int, List<double>>> GetPPMMapFromSpectra()
    {
      Dictionary<string, Dictionary<int, List<double>>> result = new Dictionary<string, Dictionary<int, List<double>>>();
      Dictionary<string, HashSet<int>> scanMap = new Dictionary<string, HashSet<int>>();
      foreach (var spec in this.spectra)
      {
        ProcessPSM(result, scanMap, spec);
      }

      foreach (var s in scanMap.Values)
      {
        s.Clear();
      }
      scanMap.Clear();
      scanMap = null;

      System.GC.Collect();
      return result;
    }

    private Dictionary<string, Dictionary<int, List<double>>> GetPPMMapFromFile(string fileName)
    {
      Dictionary<string, Dictionary<int, List<double>>> result = new Dictionary<string, Dictionary<int, List<double>>>();
      Dictionary<string, HashSet<int>> scanMap = new Dictionary<string, HashSet<int>>();
      using (var sr = new StreamReader(fileName))
      {
        string line = sr.ReadLine();
        var format = new PeptideLineFormat(line);

        Progress.SetRange(0, sr.BaseStream.Length);

        int count = 0;
        while ((line = sr.ReadLine()) != null)
        {
          if (string.IsNullOrWhiteSpace(line))
          {
            break;
          }

          count++;
          if (count % 1000 == 0)
          {
            Progress.SetPosition(sr.GetCharpos());

            if (Progress.IsCancellationPending())
            {
              throw new UserTerminatedException();
            }
          }

          var spec = format.ParseString(line);

          ProcessPSM(result, scanMap, spec);
        }
      }

      foreach (var s in scanMap.Values)
      {
        s.Clear();
      }
      scanMap.Clear();
      scanMap = null;

      System.GC.Collect();
      return result;
    }

    private void ProcessPSM(Dictionary<string, Dictionary<int, List<double>>> ppmMap, Dictionary<string, HashSet<int>> scanMap, IIdentifiedSpectrum spec)
    {
      Dictionary<int, List<double>> dic;
      List<double> ppmList;
      HashSet<int> scans;
      if (!ppmMap.TryGetValue(spec.Query.FileScan.Experimental, out dic))
      {
        dic = new Dictionary<int, List<double>>();
        ppmMap[spec.Query.FileScan.Experimental] = dic;

        scans = new HashSet<int>();
        scanMap[spec.Query.FileScan.Experimental] = scans;
      }
      else
      {
        scans = scanMap[spec.Query.FileScan.Experimental];
        if (scans.Contains(spec.Query.FileScan.FirstScan))
        {
          return;
        }
      }

      scans.Add(spec.Query.FileScan.FirstScan);

      int isotopic = 0;
      double ppm = PrecursorUtils.mz2ppm(spec.TheoreticalMass, spec.TheoreticalMinusExperimentalMass);
      if (ppm > PPM_TOLERANCE)
      {
        var newppm = PrecursorUtils.mz2ppm(spec.TheoreticalMass, spec.TheoreticalMinusExperimentalMass - Atom.C13_GAP);
        if (Math.Abs(newppm) < PPM_TOLERANCE)
        {
          isotopic = -1;
          ppm = newppm;
        }
      }
      else
      {
        var newppm = ppm;
        var newisotopic = 0;
        while ((newppm < -PPM_TOLERANCE))
        {
          newisotopic++;
          newppm = PrecursorUtils.mz2ppm(spec.TheoreticalMass, spec.TheoreticalMinusExperimentalMass + newisotopic * Atom.C13_GAP);
        }

        if (Math.Abs(newppm) < PPM_TOLERANCE)
        {
          isotopic = newisotopic;
          ppm = newppm;
        }
      }

      if (!dic.TryGetValue(isotopic, out ppmList))
      {
        ppmList = new List<double>();
        dic[isotopic] = ppmList;
      }
      ppmList.Add(ppm);
    }
  }
}
