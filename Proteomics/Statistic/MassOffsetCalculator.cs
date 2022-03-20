using MathNet.Numerics.Statistics;
using RCPA.Proteomics.Raw;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace RCPA.Proteomics.Statistic
{
  public class OffsetValue
  {
    public int Count { get; set; }
    public double IonOffset { get; set; }
    public double MedianInWindow { get; set; }
  }

  public class OffsetEntry
  {
    public OffsetEntry(int scan, double rt, double ionOffset, int count)
    {
      this.Scan = scan;
      this.RetentionTime = rt;
      this.InitValue = new OffsetValue();
      this.RefineValue = new OffsetValue();
      this.InitValue.IonOffset = ionOffset;
      this.InitValue.Count = count;
    }

    public int Scan { get; set; }

    public double RetentionTime { get; set; }

    public OffsetValue InitValue { get; set; }

    public OffsetValue RefineValue { get; set; }

    public static List<OffsetEntry> ReadFromFile(string fileName)
    {
      var result = new List<OffsetEntry>();
      var lines = File.ReadAllLines(fileName);
      for (int i = 1; i < lines.Length; i++)
      {
        var parts = lines[i].Split('\t');
        if (parts.Length < 6)
        {
          break;
        }

        OffsetEntry entry = new OffsetEntry(int.Parse(parts[0]), double.Parse(parts[2]), double.Parse(parts[4]), int.Parse(parts[3]));
        entry.InitValue.MedianInWindow = double.Parse(parts[5]);

        if (parts.Length > 6)
        {
          entry.RefineValue.Count = int.Parse(parts[6]);
          entry.RefineValue.IonOffset = int.Parse(parts[7]);
          entry.RefineValue.MedianInWindow = int.Parse(parts[8]);
        }

        result.Add(entry);
      }
      return result;
    }
  }

  public class MonitorIon
  {
    public MonitorIon(double mz, double precursurPPM)
    {
      _mz = mz;
      this.PrecursorPPM = precursurPPM;
    }

    private double _mz = 0.0;
    public double Mz
    {
      get
      {
        return _mz;
      }
      set
      {
        _mz = value;
        InitMzRange();
      }
    }

    private double _precursorMz = 0.0;
    public double PrecursorPPM
    {
      set
      {
        _precursorMz = PrecursorUtils.ppm2mz(Mz, value);
        InitMzRange();
      }
    }

    private double _offsetMz = 0.0;
    public double OffsetPPM
    {
      set
      {
        _offsetMz = PrecursorUtils.ppm2mz(Mz, value);
        InitMzRange();
      }
    }

    public double MinMz { get; set; }

    public double MaxMz { get; set; }

    private void InitMzRange()
    {
      MinMz = Mz + _offsetMz - _precursorMz;
      MaxMz = Mz + _offsetMz + _precursorMz;
    }
  }

  public class MassOffsetCalculator : AbstractParallelTaskFileProcessor
  {
    protected double maxShiftPPM;
    protected MonitorIon[] monitorIons;
    protected double rtWindow;

    public MassOffsetCalculator(double[] monitorIons, double maxShiftPPM, double rtWindow)
    {
      this.maxShiftPPM = maxShiftPPM;
      this.monitorIons = (from ion in monitorIons
                          select new MonitorIon(ion, maxShiftPPM)).ToArray();
      this.rtWindow = rtWindow / 2;
    }

    public override IEnumerable<string> Process(string fileName)
    {
      var result = fileName + ".offset";

      using (StreamWriter sw = new StreamWriter(result))
      {
        List<OffsetEntry> offsets = GetOffsets(fileName);

        sw.WriteLine("Scan\tOffset\tRT\tInitIonCount\tInitMedianByIon\tInitMedianInWindow");
        offsets.ForEach(m => sw.WriteLine("{0}\t{1:0.0000}\t{2:0.0000}\t{3}\t{4:0.0000}\t{5:0.0000}",
          m.Scan, m.RefineValue.MedianInWindow, m.RetentionTime,
          m.InitValue.Count, m.InitValue.IonOffset, m.InitValue.MedianInWindow));
      }

      return new string[] { result };
    }

    protected void DoGetOffsets(List<OffsetEntry> result, IRawFile reader, string msg)
    {
      var firstScan = reader.GetFirstSpectrumNumber();
      var lastScan = reader.GetLastSpectrumNumber();

      SetMessage(msg);
      SetRange(firstScan, lastScan);

      for (int scan = firstScan; scan <= lastScan; scan++)
      {
        if (reader.GetMsLevel(scan) == 1)
        {
          SetPosition(scan);

          if (IsLoopStopped)
          {
            return;
          }

          if (Progress.IsCancellationPending())
          {
            throw new UserTerminatedException();
          }

          var pkl = reader.GetPeakList(scan);
          var rt = reader.ScanToRetentionTime(scan);

          var offsetValues = GetOffsetsFromPeakList(pkl);

          if (offsetValues.Count > 0)
          {
            var weightedPPM = GetWeightedPPM(offsetValues);
            result.Add(new OffsetEntry(scan, rt, weightedPPM, offsetValues.Count));
          }
          else
          {
            result.Add(new OffsetEntry(scan, rt, 0, 0));
          }
        }
      }
      CalculateOffset(result, m => m.InitValue);
    }

    public virtual List<OffsetEntry> GetOffsets(string fileName)
    {
      var result = new List<OffsetEntry>();

      foreach (var ion in monitorIons)
      {
        ion.PrecursorPPM = this.maxShiftPPM;
        ion.OffsetPPM = 0;
      }

      using (var reader = RawFileFactory.GetRawFileReader(fileName))
      {
        DoGetOffsets(result, reader, "Calculating mass shift from " + fileName + "...");
      }

      return result;
    }

    protected List<Pair<double, double>> GetOffsetsFromPeakList(Spectrum.PeakList<Spectrum.Peak> pkl)
    {
      var offsetValues = new List<Pair<double, double>>();
      for (int i = 0; i < monitorIons.Length; i++)
      {
        var peaks = pkl.FindAll(m => m.Charge <= 1 && m.Mz >= monitorIons[i].MinMz && m.Mz <= monitorIons[i].MaxMz);
        if (peaks.Count > 0)
        {
          var mz = (from p in peaks
                    orderby p.Intensity descending
                    select p).First();
          var offset = PrecursorUtils.mz2ppm(monitorIons[i].Mz, monitorIons[i].Mz - mz.Mz);
          offsetValues.Add(new Pair<double, double>(offset, mz.Intensity));
        }
      }
      return offsetValues;
    }

    protected double GetWeightedPPM(List<Pair<double, double>> offsetValues)
    {
      return (from off in offsetValues
              select off.First * off.Second).Sum() / (from off in offsetValues
                                                      select off.Second).Sum();
    }

    protected void CalculateOffset(List<OffsetEntry> offsets, Func<OffsetEntry, OffsetValue> func)
    {
      for (int i = 0; i < offsets.Count; i++)
      {
        List<OffsetEntry> window = new List<OffsetEntry>();
        for (int j = i; j >= 0; j--)
        {
          if (offsets[i].RetentionTime - offsets[j].RetentionTime < rtWindow)
          {
            if (func(offsets[j]).Count > 0)
            {
              window.Add(offsets[j]);
            }
          }
          else
          {
            break;
          }
        }

        for (int j = i + 1; j < offsets.Count; j++)
        {
          if (offsets[j].RetentionTime - offsets[i].RetentionTime < rtWindow)
          {
            if (func(offsets[j]).Count > 0)
            {
              window.Add(offsets[j]);
            }
          }
          else
          {
            break;
          }
        }

        if (window.Count > 0)
        {
          func(offsets[i]).MedianInWindow = Statistics.Median(from w in window select func(w).IonOffset);
        }
        else
        {
          func(offsets[i]).MedianInWindow = -1;
        }
      }

      for (int i = 0; i < offsets.Count; i++)
      {
        if (func(offsets[i]).MedianInWindow == -1)
        {
          OffsetEntry before = null;
          OffsetEntry after = null;

          for (int j = i - 1; j >= 0; j--)
          {
            if (func(offsets[j]).Count > 0)
            {
              before = offsets[j];
              break;
            }
          }

          for (int j = i + 1; j < offsets.Count; j++)
          {
            if (func(offsets[j]).Count > 0)
            {
              after = offsets[j];
              break;
            }
          }

          if (before == null || after == null)
          {
            continue;
          }

          func(offsets[i]).IonOffset = (func(before).IonOffset + func(after).IonOffset) / 2;
          func(offsets[i]).MedianInWindow = (func(before).MedianInWindow + func(after).MedianInWindow) / 2;
        }
      }
    }
  }

  public class MassOffsetCalculator2 : MassOffsetCalculator
  {
    private double precursorPPM;

    public MassOffsetCalculator2(double[] monitorIons, double initPPM, double precursorPPM, double rtWindow)
      : base(monitorIons, initPPM, rtWindow)
    {
      this.precursorPPM = precursorPPM;
    }

    public override IEnumerable<string> Process(string fileName)
    {
      var result = fileName + ".offset";

      using (StreamWriter sw = new StreamWriter(result))
      {
        List<OffsetEntry> offsets = GetOffsets(fileName);

        sw.WriteLine("Scan\tOffset\tRT\tInitIonCount\tInitMedianByIon\tInitMedianInWindow\tRefineIonCount\tRefineMedianByIon\tRefineMedianInWindow");
        offsets.ForEach(m => sw.WriteLine("{0}\t{1:0.0000}\t{2:0.0000}\t{3}\t{4:0.0000}\t{5:0.0000}\t{6}\t{7:0.0000}\t{8:0.0000}", m.Scan, m.RefineValue.MedianInWindow, m.RetentionTime,
          m.InitValue.Count, m.InitValue.IonOffset, m.InitValue.MedianInWindow,
          m.RefineValue.Count, m.RefineValue.IonOffset, m.RefineValue.MedianInWindow));
      }
      return new string[] { result };
    }

    public override List<OffsetEntry> GetOffsets(string fileName)
    {
      var result = new List<OffsetEntry>();

      foreach (var ion in monitorIons)
      {
        ion.PrecursorPPM = this.maxShiftPPM;
        ion.OffsetPPM = 0;
      }

      using (var reader = new CacheRawFile(RawFileFactory.GetRawFileReader(fileName)))
      {
        DoGetOffsets(result, reader, "Processing " + fileName + ", first round ...");

        var firstScan = reader.GetFirstSpectrumNumber();
        var lastScan = reader.GetLastSpectrumNumber();

        var dic = result.ToDictionary(m => m.Scan);

        SetMessage("Processing " + fileName + ", second round ...");
        SetRange(firstScan, lastScan);
        for (int scan = firstScan; scan <= lastScan; scan++)
        {
          if (reader.GetMsLevel(scan) == 1)
          {
            SetPosition(scan);

            if (IsLoopStopped)
            {
              return null;
            }

            if (Progress.IsCancellationPending())
            {
              throw new UserTerminatedException();
            }

            var pkl = reader.GetPeakList(scan);
            var rt = reader.ScanToRetentionTime(scan);

            var offsetEntry = dic[scan];
            foreach (var ion in monitorIons)
            {
              ion.PrecursorPPM = this.precursorPPM;
              ion.OffsetPPM = offsetEntry.InitValue.MedianInWindow;
            }

            var offsetValues = GetOffsetsFromPeakList(pkl);

            if (offsetValues.Count > 0)
            {
              offsetEntry.RefineValue.IonOffset = GetWeightedPPM(offsetValues);
              offsetEntry.RefineValue.Count = offsetValues.Count;
            }
            else
            {
              offsetEntry.RefineValue.IonOffset = 0.0;
              offsetEntry.RefineValue.Count = 0;
            }
          }
        }

        CalculateOffset(result, m => m.RefineValue);
      }

      return result;
    }
  }
}
