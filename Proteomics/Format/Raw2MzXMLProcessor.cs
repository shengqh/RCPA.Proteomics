using RCPA.Proteomics.Raw;
using RCPA.Proteomics.Spectrum;
using RCPA.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace RCPA.Proteomics.Format
{
  public class Raw2MzXMLProcessor : AbstractRawConverter
  {
    public static string version = "1.0.0";

    private StreamWriter sw = null;

    public string DataProcessingSoftware { get; set; }

    public string DataProcessingSoftwareVersion { get; set; }

    public Dictionary<string, string> dataProcessingOperations { get; set; }

    private MultipleRaw2MgfOptions options;

    private List<Pair<int, long>> scanIndeies;

    private Dictionary<string, StreamWriter> swMap = null;

    private List<string> mgfFiles = null;

    public Raw2MzXMLProcessor(MultipleRaw2MgfOptions options)
      : base(options)
    {
      this.options = options;
      this.dataProcessingOperations = new Dictionary<string, string>();
      this.scanIndeies = new List<Pair<int, long>>();
    }

    private StreamWriter GetStreamWriter(IRawFile rawReader, string peakMode, int msLevel, string fileName)
    {
      string curFile = GetPeakModeFileName(rawReader, peakMode, msLevel, fileName);

      if (swMap.ContainsKey(curFile))
      {
        return swMap[curFile];
      }

      mgfFiles.Add(curFile);

      var result = new StreamWriter(curFile);
      result.NewLine = "\n";
      swMap[curFile] = result;
      return result;
    }

    private List<int> GetOutputScans(IRawFile2 rawFile)
    {
      var result = new List<int>();
      int firstScan = rawFile.GetFirstSpectrumNumber();
      int lastScan = rawFile.GetLastSpectrumNumber();
      for (int scan = firstScan; scan <= lastScan; scan++)
      {
        int msLevel = rawFile.GetMsLevel(scan);
        result.Add(scan);
      }
      return result;
    }

    protected override void DoInitialize(IRawFile2 rawFile, string rawFileName)
    {
      var resultFile = GetResultFile(rawFile, rawFileName);

      var scans = GetOutputScans(rawFile);

      this.scanIndeies = new List<Pair<int, long>>();

      var utf8WithoutBom = new System.Text.UTF8Encoding(false);
      this.sw = new StreamWriter(resultFile, false, utf8WithoutBom);
      this.sw.NewLine = "\n";

      /* 
         xml header and namespace info 
      */
      sw.WriteLine(@"<?xml version=""1.0"" encoding=""ISO-8859-1""?>");
      sw.WriteLine(@"<mzXML xmlns=""http://sashimi.sourceforge.net/schema_revision/mzXML_3.1""");
      sw.WriteLine(@" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance""");
      sw.WriteLine(@" xsi:schemaLocation=""http://sashimi.sourceforge.net/schema_revision/mzXML_3.1 http://sashimi.sourceforge.net/schema_revision/mzXML_3.1/mzXML_idx_3.1.xsd"" >");

      int firstScan = rawFile.GetFirstSpectrumNumber();
      int lastScan = rawFile.GetLastSpectrumNumber();
      double startTime = rawFile.ScanToRetentionTime(firstScan) * 60;
      double endTime = rawFile.ScanToRetentionTime(lastScan) * 60;

      /* 
         begin msRun 
      */
      sw.WriteLine(MyConvert.Format(@" <msRun scanCount=""{0}"" startTime=""PT{1:G6}S"" endTime=""PT{2:G6}S"" >", scans.Count, startTime, endTime));

      string sha1;
      try
      {
        sha1 = HashUtils.GetSHA1Hash(rawFileName).ToLower();
      }
      catch (Exception ex)
      {
        throw new Exception("Exception when calculating sha1 value of file " + rawFileName, ex);
      }

      /* 
         parent (raw input) file 
      */
      //sw.WriteLine(@"  <parentFile fileName=""file://DATA-PC/L/5168TP_mouse_phos_TMT/raw/6.raw"" fileType=""RAWData"" fileSha1=""f6db29dd3e26dec7eb16422138f0b2b06f02ba06"" />");
      sw.WriteLine(@"  <parentFile fileName=""file://{0}"" fileType=""RAWData"" fileSha1=""{1}""/>", rawFileName.Replace("\\", "/"), sha1);

      if (rawFile is RawFileImpl)
      {
        var impl = rawFile as RawFileImpl;
        /* 
           mass spec instrument section 
        */
        // get the instrument model
        string instModel = impl.GetInstModel();

        // get acquisition software version
        string instSoftVersion = impl.GetInstSoftwareVersion();

        sw.WriteLine(@"  <msInstrument>");
        sw.WriteLine(@"   <msManufacturer category=""msManufacturer"" value=""Thermo Finnigan"" />");
        sw.WriteLine(@"   <msModel category=""msModel"" value=""{0}"" />", instModel);
        sw.WriteLine(@"   <msIonisation category=""msIonisation"" value=""NSI"" />");
        sw.WriteLine(@"   <msMassAnalyzer category=""msMassAnalyzer"" value=""unknown"" />");
        sw.WriteLine(@"   <msDetector category=""msDetector"" value=""unknown"" />");
        sw.WriteLine(@"   <software type=""acquisition"" name=""Xcalibur"" version=""{0}"" />", instSoftVersion);
        sw.WriteLine(@"  </msInstrument>");
      }

      /*
        data processing info
      */
      sw.WriteLine(@"  <dataProcessing>");
      sw.WriteLine(@"   <software type=""conversion"" name=""{0}"" version=""{1}"" />", this.DataProcessingSoftware, this.DataProcessingSoftwareVersion);
      foreach (string dataProcessingOperationName in this.dataProcessingOperations.Keys)
      {
        sw.WriteLine(MyConvert.Format(@"    <processingOperation name=""{0}"" value=""{1}""/>", dataProcessingOperationName, this.dataProcessingOperations[dataProcessingOperationName]));
      }
      sw.WriteLine("  </dataProcessing>");
    }

    protected override IEnumerable<string> DoProcess(string fileName, List<int> ignoreScans, int lastScan, bool bContinue)
    {
      var result = new List<string>();

      bool bReadAgain = false;

      using (var rawReader = RawFileFactory.GetRawFileReader(fileName))
      {
        try
        {
          DoInitialize(rawReader, fileName);

          string experimental = rawReader.GetFileNameWithoutExtension(fileName);

          SetMessage("Processing " + fileName + " ...");

          int firstSpectrumNumber = rawReader.GetFirstSpectrumNumber();
          int lastSpectrumNumber = rawReader.GetLastSpectrumNumber();
          //          int lastSpectrumNumber = 5000;

          SetRange(firstSpectrumNumber, lastSpectrumNumber);

          for (int scan = firstSpectrumNumber; scan <= lastSpectrumNumber; )
          {
            if (Progress.IsCancellationPending())
            {
              throw new UserTerminatedException();
            }

            if (IsLoopStopped)
            {
              return result;
            }

            scan = DoWritePeakList(rawReader, scan, fileName, result, experimental, lastSpectrumNumber, ignoreScans, ref bReadAgain);
            if (bReadAgain)
            {
              break;
            }
          }
        }
        finally
        {
          DoFinalize(bReadAgain, rawReader, fileName, result);
        }
      }

      if (bReadAgain)
      {
        return DoProcess(fileName, ignoreScans, 0, false);
      }
      else
      {
        return result;
      }
    }

    private int DoWritePeakList(IRawFile2 rawReader, int scan, string fileName, List<string> returnFiles, string experimental, int lastSpectrumNumber, List<int> ignoreScans, ref bool bReadAgain)
    {
      var result = scan + 1;
      if (ignoreScans.Contains(scan))
      {
        return result;
      }

      SetPosition(scan);

      int msLevel = rawReader.GetMsLevel(scan);

      if (!DoAcceptMsLevel(msLevel))
      {
        return result;
      }

      //Console.WriteLine("Reading scan {0}", scan);

      PeakList<Peak> pkl;
      try
      {
        pkl = rawReader.GetPeakList(scan);
      }
      catch (RawReadException ex)
      {
        ignoreScans.Add(ex.Scan);
        File.WriteAllLines(GetIgnoreScanFile(fileName), (from i in ignoreScans
                                                         let s = i.ToString()
                                                         select s).ToArray());
        bReadAgain = true;
        return result;
      }

      pkl.MsLevel = msLevel;
      pkl.Experimental = experimental;
      pkl.ScanTimes.Add(new ScanTime(scan, rawReader.ScanToRetentionTime(scan)));

      pkl.ScanMode = rawReader.GetScanMode(scan);

      PeakList<Peak> pklProcessed;
      if (msLevel > 1)
      {
        pkl.Precursor = new PrecursorPeak(rawReader.GetPrecursorPeak(scan));

        if (pkl.PrecursorCharge == 0)
        {
          pkl.PrecursorCharge = PrecursorUtils.GuessPrecursorCharge(pkl, pkl.PrecursorMZ);
        }

        if (options.ExtractRawMS3 && pkl.MsLevel == 3)
        {
          pklProcessed = pkl;
        }
        else
        {
          pklProcessed = this.PeakListProcessor.Process(pkl);
        }
      }
      else
      {
        pklProcessed = pkl;
      }

      if (null != pklProcessed && pklProcessed.Count > 0)
      {
        DoWritePeakList(rawReader, pklProcessed, fileName, returnFiles);

        if (!options.MzXmlNestedScan)
        {
          var intent = GetScanIntent(pkl.MsLevel);
          sw.WriteLine(intent + "</scan>");
        }

        while (result < lastSpectrumNumber && rawReader.GetMsLevel(result) > msLevel)
        {
          result = DoWritePeakList(rawReader, result, fileName, returnFiles, experimental, lastSpectrumNumber, ignoreScans, ref bReadAgain);
          if (bReadAgain)
          {
            return result;
          }
        }

        if (options.MzXmlNestedScan)
        {
          var intent = GetScanIntent(pkl.MsLevel);
          sw.WriteLine(intent + "</scan>");
        }
      }

      return result;
    }

    private string GetScanIntent(int msLevel)
    {
      if (options.MzXmlNestedScan)
      {
        return new string(' ', msLevel + 1);
      }
      else
      {
        return "  ";
      }
    }

    private const string lf = "\n";
    protected override void DoWritePeakList(IRawFile rawFile, PeakList<Peak> pkl, string rawFileName, List<string> result)
    {
      sw.Flush();
      scanIndeies.Add(new Pair<int, long>(pkl.ScanTimes[0].Scan, sw.BaseStream.Position));

      int scan = pkl.ScanTimes[0].Scan;
      var retentionTime = pkl.ScanTimes[0].RetentionTime;

      var activationMethod = string.Empty;

      var intent = GetScanIntent(pkl.MsLevel);

      if (rawFile is RawFileImpl)
      {
        var impl = rawFile as RawFileImpl;

        var rcf = new RawScanFilter();
        rcf.Filter = impl.GetFilterForScanNum(scan);
        activationMethod = rcf.ActivationMethod.ToUpper();

        /* scan header info begin */
        int numPakets = 0;
        double RT = 0;
        double lowMass = 0;
        double highMass = 0;
        double TIC = 0;
        double basePeakMass = 0;
        double basePeakIntensity = 0;
        int channel = 0;
        int uniformTime = 0;
        double frequency = 0;

        impl.GetScanHeaderInfoForScanNum(
          scan,
          ref numPakets,
          ref RT,
          ref lowMass,
          ref highMass,
          ref TIC,
          ref basePeakMass,
          ref basePeakIntensity,
          ref channel,
          ref uniformTime,
          ref frequency);

        lowMass = pkl.First().Mz;
        highMass = pkl.Last().Mz;

        sw.Write(MyConvert.Format(intent + "<scan num=\"{0}\"" + lf
                               + intent + " msLevel=\"{1}\"" + lf
                               + intent + " peaksCount=\"{2}\"" + lf
                               + intent + " polarity=\"{3}\"" + lf
                               + intent + " scanType=\"{4}\"" + lf
                               + intent + " filterLine=\"{5}\"" + lf
                               + intent + " retentionTime=\"PT{6:G6}S\"" + lf,
                               scan,
                               rcf.MsLevel,
                               pkl.Count,
                               rcf.Polarity,
                               rcf.ScanType,
                               rcf.Filter,
                               retentionTime * 60));

        sw.Write(MyConvert.Format(intent + " lowMz=\"{0:G6}\"" + lf
                               + intent + " highMz=\"{1:G6}\"" + lf
                               + intent + " basePeakMz=\"{2:G6}\"" + lf
                               + intent + " basePeakIntensity=\"{3:e5}\"" + lf
                               + intent + " totIonCurrent=\"{4:e5}\" >" + lf,
                               lowMass,
                               highMass,
                               basePeakMass,
                               basePeakIntensity,
                               TIC));

        if (rcf.MsLevel > 1)
        {
          sw.Write(intent + " collisionEnergy=\"{0:0}\"" + lf, rcf.CollisionEnergy);
        }

        pkl.PrecursorIntensity = impl.GetPrecursorPeak(scan).Intensity;
      }
      else
      {
        sw.Write(MyConvert.Format(intent + "<scan num=\"{0}\"" + lf
                               + intent + " msLevel=\"{1}\"" + lf
                               + intent + " peaksCount=\"{2}\"" + lf
                               + intent + " scanType=\"{3}\"" + lf
                               + intent + " retentionTime=\"PT{4:G8}S\"" + lf,
                               scan,
                               pkl.MsLevel,
                               pkl.Count,
                               pkl.ScanMode,
                               retentionTime * 60));

        var basePeak = pkl.FindMaxIntensityPeak();
        var TIC = pkl.Sum(m => m.Intensity);

        sw.Write(MyConvert.Format(intent + " lowMz=\"{0:0}\"" + lf
                               + intent + " highMz=\"{1:0}\"" + lf
                               + intent + " basePeakMz=\"{2:G6}\"" + lf
                               + intent + " basePeakIntensity=\"{3:e5}\"" + lf
                               + intent + " totIonCurrent=\"{4:e5}\" >" + lf,
                               pkl.First().Mz,
                               pkl.Last().Mz,
                               basePeak.Mz,
                               basePeak.Intensity,
                               TIC));
      }

      if (pkl.MsLevel > 1)
      {
        sw.Write(MyConvert.Format(intent + " <precursorMz precursorIntensity=\"{0:0.#####}\"", pkl.PrecursorIntensity));

        if (!string.IsNullOrEmpty(activationMethod))
        {
          sw.Write(" activationMethod=\"{0}\"", activationMethod);
        }
        if (pkl.PrecursorCharge > 0)
        {
          sw.Write(" precursorCharge=\"{0}\"", pkl.PrecursorCharge);
        }

        sw.Write(" >");
        sw.Write(MyConvert.Format("{0:0.######}</precursorMz>" + lf, pkl.PrecursorMZ));
      }
      /* scan header info end */

      /* peak list info begin */
      sw.WriteLine(intent + " <peaks precision=\"32\"");
      sw.WriteLine(intent + "  byteOrder=\"network\"");
      sw.WriteLine(intent + "  contentType=\"m/z-int\"");
      sw.WriteLine(intent + "  compressionType=\"none\"");
      sw.WriteLine(intent + "  compressedLen=\"0\" >" + MzxmlHelper.PeakListToBase64(pkl) + "</peaks>");
      /* peak list info end */
    }

    protected override void DoFinalize(bool bReadAgain, IRawFile rawReader, string rawFileName, List<string> result)
    {
      sw.WriteLine(" </msRun>");
      sw.Flush();

      // Save the offset for the indexOffset element
      long indexOffset = sw.BaseStream.Position;

      sw.WriteLine(" <index name=\"scan\" >");
      foreach (var scanIndex in scanIndeies)
      {
        sw.Write(MyConvert.Format("  <offset id=\"{0}\" >{1}</offset>" + lf,
                               scanIndex.First, scanIndex.Second));
      }
      sw.WriteLine(" </index>");
      sw.WriteLine(" <indexOffset>{0}</indexOffset>", indexOffset);

      sw.Write(" <sha1>");
      sw.Flush();
      string sha1;
      var resultFile = GetResultFile(rawReader, rawFileName);
      try
      {
        sha1 = HashUtils.GetSHA1Hash(resultFile).ToLower();
      }
      catch (Exception ex)
      {
        throw new Exception("Exception when calculating sha1 value of file " + resultFile, ex);
      }
      sw.WriteLine(sha1 + "</sha1>");
      sw.WriteLine("</mzXML>");

      sw.Close();

      result.Add(resultFile);
    }

    protected override bool DoAcceptMsLevel(int msLevel)
    {
      return true;
    }
  }
}