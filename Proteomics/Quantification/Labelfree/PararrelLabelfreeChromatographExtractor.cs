//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using RCPA.Proteomics.Spectrum;
//using RCPA.Proteomics.Raw;
//using System.IO;
//using RCPA.Utils;
//using System.Threading.Tasks;
//using System.Threading;

//namespace RCPA.Proteomics.Quantification.Labelfree
//{
//  public class PararrelLabelfreeChromotographExtractor : AbstractThreadFileProcessor
//  {
//    private List<RetentionTimePeak> targetPeaks;
//    private List<string> rawFiles;
//    private IRawFile rawReader;
//    private double ppmTolerance;
//    private bool force;
//    private CancellationToken token;

//    public PararrelLabelfreeChromotographExtractor(IEnumerable<RetentionTimePeak> targetPeaks, List<string> rawFiles, double ppmTolerance, bool force)
//    {
//      this.targetPeaks = new List<RetentionTimePeak>(targetPeaks);
//      this.rawFiles = rawFiles;
//      this.ppmTolerance = ppmTolerance;
//      this.force = force;
//      this.token = new CancellationToken();

//      InitializeTargetPeaks();
//    }

//    private void InitializeTargetPeaks()
//    {
//      Parallel.ForEach(targetPeaks, m =>
//      {
//        m.Initialize();
//        m.MzTolerance = PrecursorUtils.ppm2mz(m.Mz, ppmTolerance);
//      });
//    }

//    public override IEnumerable<string> Process(string targetDir)
//    {
//      var option = new ParallelOptions()
//      {
//         MaxDegreeOfParallelism = Environment.ProcessorCount,
//         CancellationToken = token
//      };

//      Parallel.ForEach(rawFiles, option, raw =>
//      {
//        List<RetentionTimePeak> waitingPeaks = new List<RetentionTimePeak>();
//        foreach (var peak in targetPeaks)
//        {
//          string file = GetTargetFile(targetDir, raw, peak);
//          if (force || !File.Exists(file))
//          {
//            waitingPeaks.Add(peak);
//          }
//        }

//        if (waitingPeaks.Count == 0)
//        {
//          return;
//        }

//        using(var rawReader = RawFileFactory.GetRawFileReader(raw))
//        {
//          int firstScan = rawReader.GetFirstSpectrumNumber();
//          int lastScan = rawReader.GetLastSpectrumNumber();

//          waitingPeaks.ForEach(m => m.Chromotographs.Clear());

//          for (int scan = firstScan; scan <= lastScan; scan++)
//          {
//            if (rawReader.GetMsLevel(scan) != 1)
//            {
//              continue;
//            }

//            if (Progress.IsCancellationPending())
//            {
//              throw new UserTerminatedException();
//            }

//            PeakList<Peak> pkl = rawReader.GetPeakList(scan);

//            double rt = rawReader.ScanToRetentionTime(scan);
//            var st = new ScanTime(scan, rt);

//            foreach (var peak in waitingPeaks)
//            {
//              var env = pkl.FindEnvelopeDirectly(peak.PeakEnvelope, peak.MzTolerance, () => new Peak());
//              env.ScanTimes.Add(st);
//              peak.Chromotographs.Add(env);
//            }
//          }

//          waitingPeaks.ForEach(m => m.TrimPeaks());
//        }
//        finally
//        {
//          rawReader.Close();
//        }

//        foreach (var peak in waitingPeaks)
//        {
//          string file = GetTargetFile(targetDir, raw, peak);
//          new RetentionTimePeakXmlFormat().WriteToFile(file, peak);
//        }
//      }


//      return new string[] { targetDir };
//    }

//    private string GetTargetFile(string targetDir, string raw, RetentionTimePeak peak)
//    {
//      var result = MyConvert.Format(@"{0}\{1}_{2:0.0000}_{3:0.00}.chro", targetDir, FileUtils.ChangeExtension(new FileInfo(raw).Name, ""), peak.Mz, peak.RententionTime);
//      return new FileInfo(result).FullName;
//    }
//  }
//}
