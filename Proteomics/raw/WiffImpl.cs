//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using Clearcore2.Data.AnalystDataProvider;
//using Clearcore2.Data.DataAccess.SampleData;
//using Clearcore2.Data;
//using RCPA.Proteomics.Spectrum;

//namespace RCPA.Proteomics.Raw
//{
//  public class WiffScan
//  {
//    public int Sample { get; set; }

//    public int Period { get; set; }

//    public int Experiment { get; set; }

//    public int Cycle { get; set; }

//    public int ScanIndex { get; set; }
//  }

//  public class WiffImpl : AbstractRawFile
//  {
//    private AnalystWiffDataProvider provider;

//    private Batch batch;

//    private string[] sampleNames;

//    public Dictionary<int, WiffScan> WiffScanMap { get; private set; }

//    private int firstScan, lastScan;

//    public string[] GetSampleNames()
//    {
//      if (null == sampleNames)
//      {
//        // make duplicate sample names unique by appending the duplicate count
//        // e.g. foo, bar, foo (2), foobar, bar (2), foo (3)
//        Dictionary<string, int> duplicateCountMap = new Dictionary<string, int>();
//        sampleNames = batch.GetSampleNames();

//        // inexplicably, some files have more samples than sample names;
//        // pad the name vector with duplicates of the last sample name;
//        for (int i = 0; i < sampleNames.Length; ++i)
//        {
//          if (duplicateCountMap.ContainsKey(sampleNames[i]))
//          {
//            duplicateCountMap[sampleNames[i]]++;
//          }
//          else
//          {
//            duplicateCountMap[sampleNames[i]] = 1;
//          }
//          int duplicateCount = duplicateCountMap[sampleNames[i]];
//          if (duplicateCount > 1)
//            sampleNames[i] += " (" + duplicateCount.ToString() + ")";
//        }
//      }

//      return sampleNames;
//    }

//    private void InitializeScanMap()
//    {
//      var samples = GetSampleNames();

//      int scanIndex = 0;
//      this.WiffScanMap = new Dictionary<int, WiffScan>();
//      for (int s = 0; s < samples.Length; s++)
//      {
//        scanIndex++;
//        var sample = batch.GetSample(s);
//        var msSample = sample.MassSpectrometerSample;
//        var p = 0;
//        var expCount = msSample.ExperimentCount;
//        for (int e = 0; e < expCount; e++)
//        {
//          var exp = msSample.GetMSExperiment(e);
//          var cycleCount = exp.Details.NumberOfScans;
//          for (int c = 0; c < cycleCount; c++)
//          {
//            scanIndex++;
//            var scan = new WiffScan()
//            {
//              ScanIndex = scanIndex,
//              Sample = s,
//              Period = p,
//              Experiment = e,
//              Cycle = c
//            };
//            this.WiffScanMap[scanIndex] = scan;
//          }
//        }
//      }

//      firstScan = 1;
//      lastScan = scanIndex;
//    }

//    private int currentSample;
//    private int currentPeriod;
//    private int currentExperiment;
//    private int currentCycle;

//    public Sample Sample { get; private set; }
//    public MassSpectrometerSample MSSample { get; private set; }
//    public MSExperiment Experiment { get; private set; }
//    public MassSpectrum Spectrum { get; private set; }
//    public MassSpectrumInfo SpectrumInfo { get; private set; }

//    private void SetScan(int scanIndex)
//    {
//      if (!WiffScanMap.ContainsKey(scanIndex))
//      {
//        throw new ArgumentException(string.Format("Scan is not in range [{0}, {1}]", GetFirstSpectrumNumber(), GetLastSpectrumNumber()));
//      }

//      var scan = WiffScanMap[scanIndex];

//      if (scan.ScanIndex != currentSample)
//      {
//        this.Sample = batch.GetSample(scan.ScanIndex - 1);
//        this.MSSample = Sample.MassSpectrometerSample;
//        currentSample = scan.Sample;
//        currentPeriod = -1;
//        currentExperiment = -1;
//        currentCycle = -1;
//      }

//      if (scan.Period != currentPeriod)
//      {
//        currentPeriod = scan.Period;
//        currentExperiment = -1;
//        currentCycle = -1;
//      }

//      if (scan.Experiment != currentExperiment)
//      {
//        this.Experiment = this.MSSample.GetMSExperiment(currentExperiment);
//        currentExperiment = scan.Experiment;
//        currentCycle = -1;
//      }

//      if (scan.Cycle != currentCycle)
//      {
//        currentCycle = scan.Cycle;
//        this.Spectrum = this.Experiment.GetMassSpectrum(scan.Cycle);
//        this.SpectrumInfo = this.Experiment.GetMassSpectrumInfo(scan.Cycle);
//      }
//    }

//    public override int GetFirstSpectrumNumber()
//    {
//      return firstScan;
//    }

//    public override int GetLastSpectrumNumber()
//    {
//      return lastScan;
//    }

//    public override bool IsProfileScanForScanNum(int scan)
//    {
//      return !IsCentroidScanForScanNum(scan);
//    }

//    public override bool IsCentroidScanForScanNum(int scan)
//    {
//      SetScan(scan);
//      return !SpectrumInfo.CentroidMode;
//    }

//    public override Peak GetPrecursorPeak(int scan)
//    {
//      SetScan(scan);
//      var result = new Peak();
//      if (SpectrumInfo.IsProductSpectrum)
//      {
//        result.Mz = SpectrumInfo.ParentMZ;
//        result.Intensity = 0;
//        result.Charge = SpectrumInfo.ParentChargeState;
//      }
//      return result;
//    }

//    public override void Open(string fileName)
//    {
//      Licenser.LicenseKey = "";
//      provider = new AnalystWiffDataProvider();
//      batch = AnalystDataProviderFactory.CreateBatch(fileName, provider);
//      sampleNames = null;
//      InitializeScanMap();
//    }

//    public override bool Close()
//    {
//      try { provider.Close(); }
//      catch { }

//      provider = null;
//      batch = null;
//      sampleNames = null;
//      WiffScanMap = null;
//      return true;
//    }

//    public override int GetNumSpectra()
//    {
//      return lastScan - firstScan + 1;
//    }

//    public override bool IsValid()
//    {
//      return provider != null;
//    }

//    public override int GetMsLevel(int scan)
//    {
//      return SpectrumInfo.MSLevel == 0 ? 1 : SpectrumInfo.MSLevel;
//    }

//    public override string GetScanMode(int scan)
//    {
//      return "CID";
//    }

//    public override double ScanToRetentionTime(int scan)
//    {
//      SetScan(scan);
//      return SpectrumInfo.StartRT;
//    }

//    public override PeakList<Peak> GetPeakList(int scan)
//    {
//      var result = new PeakList<Peak>();
//      var mzs = Spectrum.GetActualXValues();
//      var intensities = Spectrum.GetActualYValues();
//      for (int i = 0; i < mzs.Length; i++)
//      {
//        result.Add(new Peak(mzs[i], intensities[i]));
//      }

//      return result;
//    }

//    public override PeakList<Peak> GetPeakList(int scan, double minMz, double maxMz)
//    {
//      var result = new PeakList<Peak>();
//      var mzs = Spectrum.GetActualXValues();
//      var intensities = Spectrum.GetActualYValues();
//      for (int i = 0; i < mzs.Length; i++)
//      {
//        if (mzs[i] < minMz)
//        {
//          continue;
//        }

//        if (mzs[i] > maxMz)
//        {
//          break;
//        }

//        result.Add(new Peak(mzs[i], intensities[i]));
//      }

//      return result;
//    }
//  }
//}
