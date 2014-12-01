using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using RCPA.Proteomics.Spectrum;
using RCPA.Proteomics.Raw;
using RCPA.Utils;
using RCPA.Proteomics.Mascot;
using RCPA.Proteomics.Statistic;
using RCPA.Proteomics.Processor;
using IranianExperts;

namespace RCPA.Proteomics.Format
{
  public class Raw2MzXMLProcessor : AbstractRawConverter
  {
    public static string version = "1.0.0";

    private StreamWriter sw = null;

    public string DataProcessingSoftware { get; set; }

    public string DataProcessingSoftwareVersion { get; set; }

    public Dictionary<string, string> dataProcessingOperations { get; set; }

    private List<Pair<int, long>> scanIndeies;

    public Raw2MzXMLProcessor()
    {
      this.dataProcessingOperations = new Dictionary<string, string>();
      this.scanIndeies = new List<Pair<int, long>>();
    }

    public string GetResultFile(IRawFile rawReader, string rawFileName)
    {
      return new FileInfo(this.TargetDirectory + "\\" + rawReader.GetFileNameWithoutExtension(rawFileName) + ".mzXML").FullName;
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

      this.sw = new StreamWriter(resultFile);
      this.sw.NewLine = "\n";

      /* 
         xml header and namespace info 
      */
      sw.WriteLine(@"<?xml version=""1.0"" encoding=""ISO-8859-1""?>
<mzXML xmlns=""http://sashimi.sourceforge.net/schema_revision/mzXML_2.0""
       xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance""
       xsi:schemaLocation=""http://sashimi.sourceforge.net/schema_revision/mzXML_2.0 http://sashimi.sourceforge.net/schema_revision/mzXML_2.0/mzXML_idx_2.0.xsd"">");

      int firstScan = rawFile.GetFirstSpectrumNumber();
      int lastScan = rawFile.GetLastSpectrumNumber();
      double startTime = rawFile.ScanToRetentionTime(firstScan) * 60;
      double endTime = rawFile.ScanToRetentionTime(lastScan) * 60;

      /* 
         begin msRun 
      */
      sw.WriteLine(MyConvert.Format(@" <msRun scanCount=""{0}""
startTime=""PT{1:0.000}S""
endTime=""PT{2:0.000}S"">", scans.Count, startTime, endTime));

      string sha1;
      try
      {
        sha1 = DTHasher.GetSHA1Hash(rawFileName).ToLower();
      }
      catch (Exception ex)
      {
        throw new Exception("Exception when calculating sha1 value of file " + rawFileName, ex);
      }

      /* 
         parent (raw input) file 
      */
      sw.WriteLine(@"  <parentFile fileName=""file://{0}""
fileType=""RAWData""
fileSha1=""{1}""/>", rawFileName.Replace("\\", "/"), sha1);

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

        sw.WriteLine(@"  <msInstrument>
    <msManufacturer category=""msManufacturer"" value=""ThermoFinnigan""/>
    <msModel category=""msModel"" value=""{0}""/>
    <msIonisation category=""msIonisation"" value=""ESI""/>   
    <msMassAnalyzer category=""msMassAnalyzer"" value=""Ion Trap""/> 
    <msDetector category=""msDetector"" value=""EMT""/>", instModel);
        sw.WriteLine(@"   <software type=""acquisition"" name=""Xcalibur"" version=""{0}""/>
  </msInstrument>", instSoftVersion);
      }

      /*
        data processing info
      */
      sw.WriteLine(@"  <dataProcessing>
    <software type=""conversion"" name=""{0}"" version=""{1}""/>", this.DataProcessingSoftware, this.DataProcessingSoftwareVersion);

      foreach (string dataProcessingOperationName in this.dataProcessingOperations.Keys)
      {
        sw.WriteLine(MyConvert.Format(@"    <processingOperation name=""{0}"" value=""{1}""/>",
                               dataProcessingOperationName,
                               this.dataProcessingOperations[dataProcessingOperationName]));
      }

      sw.WriteLine("  </dataProcessing>");
    }

    private const string lf = "\n";
    protected override void DoWritePeakList(IRawFile rawFile, PeakList<Peak> pkl, string rawFileName, List<string> result)
    {
      sw.Flush();
      scanIndeies.Add(new Pair<int, long>(pkl.ScanTimes[0].Scan, sw.BaseStream.Position));

      int scan = pkl.ScanTimes[0].Scan;
      var retentionTime = pkl.ScanTimes[0].RetentionTime;

      if (rawFile is RawFileImpl)
      {
        var impl = rawFile as RawFileImpl;

        var rcf = new RawScanFilter();
        rcf.Filter = impl.GetFilterForScanNum(scan);

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

        sw.Write(MyConvert.Format("  <scan num=\"{0}\"" + lf
                               + "        msLevel=\"{1}\"" + lf
                               + "        peaksCount=\"{2}\"" + lf
                               + "        polarity=\"{3}\"" + lf
                               + "        scanType=\"{4}\"" + lf
                               + "        retentionTime=\"PT{5:0.00}S\"" + lf,
                               scan,
                               rcf.MsLevel,
                               pkl.Count,
                               rcf.Polarity,
                               rcf.ScanType,
                               retentionTime * 60));
        if (rcf.MsLevel > 1)
        {
          sw.Write("        collisionEnergy=\"{0:0}\"" + lf, rcf.CollisionEnergy);
        }

        sw.Write(MyConvert.Format("        lowMz=\"{0:0}\"" + lf
                               + "        highMz=\"{1:0}\"" + lf
                               + "        basePeakMz=\"{2:0.000}\"" + lf
                               + "        basePeakIntensity=\"{3:0}\"" + lf
                               + "        totIonCurrent=\"{4}\">" + lf,
                               lowMass,
                               highMass,
                               basePeakMass,
                               basePeakIntensity,
                               TIC));
      }
      else
      {
        sw.Write(MyConvert.Format("  <scan num=\"{0}\"" + lf
                               + "        msLevel=\"{1}\"" + lf
                               + "        peaksCount=\"{2}\"" + lf
                               + "        scanType=\"{3}\"" + lf
                               + "        retentionTime=\"PT{4:0.00}S\"" + lf,
                               scan,
                               pkl.MsLevel,
                               pkl.Count,
                               pkl.ScanMode,
                               retentionTime * 60));

        var basePeak = pkl.FindMaxIntensityPeak();
        var TIC = pkl.Sum(m => m.Intensity);

        sw.Write(MyConvert.Format("        lowMz=\"{0:0}\"" + lf
                               + "        highMz=\"{1:0}\"" + lf
                               + "        basePeakMz=\"{2:0.000}\"" + lf
                               + "        basePeakIntensity=\"{3:0}\"" + lf
                               + "        totIonCurrent=\"{4}\">" + lf,
                               pkl.First().Mz,
                               pkl.Last().Mz,
                               basePeak.Mz,
                               basePeak.Intensity,
                               TIC));
      }


      if (pkl.MsLevel > 1)
      {
        sw.Write(MyConvert.Format("    <precursorMz precursorIntensity=\"{0:0.#####}\"", pkl.PrecursorIntensity));

        if (pkl.PrecursorCharge > 0)
        {
          sw.Write(" precursorCharge=\"" + pkl.PrecursorCharge + "\"");
        }

        sw.Write(">");
        sw.Write(MyConvert.Format("{0:0.######}</precursorMz>" + lf, pkl.PrecursorMZ));
      }
      /* scan header info end */

      /* peak list info begin */
      sw.Write("    <peaks precision=\"32\"" + lf
               + "           byteOrder=\"network\"" + lf
               + "           pairOrder=\"m/z-int\">");
      sw.Write(MzxmlHelper.PeakListToBase64(pkl) + "</peaks>" + lf);
      /* peak list info end */

      //I don't care if this scan is an child of last scan, just close it 
      sw.Write("  </scan>" + lf);
    }

    protected override void DoFinalize(bool bReadAgain, IRawFile rawReader, string rawFileName, List<string> result)
    {
      sw.Write(" </msRun>" + lf);
      sw.Flush();

      // Save the offset for the indexOffset element
      long indexOffset = sw.BaseStream.Position;

      sw.Write(" <index name=\"scan\">" + lf);
      foreach (var scanIndex in scanIndeies)
      {
        sw.Write(MyConvert.Format("  <offset id=\"{0}\">{1}</offset>" + lf,
                               scanIndex.First, scanIndex.Second));
      }
      sw.Write(" </index>" + lf);
      sw.Write(" <indexOffset>" + indexOffset + "</indexOffset>" + lf);

      sw.Write(" <sha1>");
      sw.Flush();
      string sha1;
      var resultFile = GetResultFile(rawReader, rawFileName);
      try
      {
        sha1 = DTHasher.GetSHA1Hash(resultFile).ToLower();
      }
      catch (Exception ex)
      {
        throw new Exception("Exception when calculating sha1 value of file " + resultFile, ex);
      }
      sw.Write(sha1 + "</sha1>" + lf);
      sw.Write("</mzXML>" + lf);

      sw.Close();

      result.Add(resultFile);
    }

    protected override bool DoAcceptMsLevel(int msLevel)
    {
      return true;
    }
  }
}