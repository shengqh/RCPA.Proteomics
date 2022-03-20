using RCPA.Proteomics.Raw;
using RCPA.Proteomics.Spectrum;
using RCPA.Utils;
using System;
using System.Collections.Generic;
using System.IO;

namespace RCPA.Proteomics.IO
{
  public class Raw2MzXmlProcessor : AbstractThreadFileProcessor
  {
    private const char lf = (char)0xA;
    private const int precision = 32;
    private const int sigBytes = precision / 8;
    public static string version = "1.0.0";
    private readonly Dictionary<string, string> dataProcessingOperations;
    private readonly string dataProcessingSoftware;
    private readonly string dataProcessingSoftwareVersion;

    private readonly bool doCentroid;
    private readonly bool fullMsOnly;
    private readonly RawFileImpl rawFile;
    private readonly bool saveToSourceDirectory;
    private readonly DirectoryInfo targetDirectory;

    public Raw2MzXmlProcessor(RawFileImpl rawFile, bool fullMsOnly, bool doCentroid, string targetDirectoryName,
                              string dataProcessingSoftware, string dataProcessingSoftwareVersion,
                              Dictionary<string, string> dataProcessingOperations)
    {
      this.rawFile = rawFile;
      this.fullMsOnly = fullMsOnly;
      this.doCentroid = doCentroid;
      if (null == targetDirectoryName || 0 == targetDirectoryName.Length)
      {
        this.saveToSourceDirectory = true;
      }
      else
      {
        this.saveToSourceDirectory = false;
        this.targetDirectory = new DirectoryInfo(targetDirectoryName);
        if (!this.targetDirectory.Exists)
        {
          this.targetDirectory.Create();
        }
      }
      this.dataProcessingSoftware = dataProcessingSoftware;
      this.dataProcessingSoftwareVersion = dataProcessingSoftwareVersion;
      this.dataProcessingOperations = dataProcessingOperations;
    }

    public override IEnumerable<string> Process(string filename)
    {
      var sourceFile = new FileInfo(filename);
      if (!sourceFile.Exists)
      {
        throw new ArgumentException("File not exist : " + filename);
      }

      string targetFilename;
      if (this.fullMsOnly)
      {
        targetFilename = sourceFile.Name + ".FullMS.mzXML";
      }
      else
      {
        targetFilename = sourceFile.Name + ".mzXML";
      }

      if (this.saveToSourceDirectory)
      {
        targetFilename = sourceFile.Directory.FullName + "\\" + targetFilename;
      }
      else
      {
        targetFilename = this.targetDirectory.FullName + "\\" + targetFilename;
      }

      using (var sw = new StreamWriter(targetFilename))
      {
        try
        {
          Progress.SetMessage("Opening file " + sourceFile.FullName + "...");
          this.rawFile.Open(filename);

          List<int> scans = GetOutputScans();
          var scanIndeies = new List<Pair<int, long>>();

          Progress.SetMessage("Saving file " + targetFilename + "...");
          Progress.SetMessage("Writing Xml header ...");
          WriteXmlHeader(sw, sourceFile, scans);

          Progress.SetRange(0, scans.Count);
          for (int i = 0; i < scans.Count; i++)
          {
            if (Progress.IsCancellationPending())
            {
              throw new UserTerminatedException();
            }
            Progress.SetPosition(i);
            WriteXmlScan(sw, scans[i], scanIndeies);
          }
          Progress.SetMessage("Writing scan finished.");
          Progress.SetPosition(scans.Count);

          Progress.SetMessage("Writing scan index ...");
          WriteXmlScanIndex(sw, scanIndeies);

          Progress.SetMessage("Writing file sha1 ...");
          WriteXmlSha1(sw, targetFilename);

          Progress.SetMessage("Finished. File has been saved to " + targetFilename);
        }
        finally
        {
          this.rawFile.Close();
        }
      }

      return new[] { targetFilename };
    }

    private void WriteXmlSha1(StreamWriter sw, string resultFile)
    {
      sw.Write(" <sha1>");
      sw.Flush();
      string sha1;
      try
      {
        sha1 = HashUtils.GetSHA1Hash(resultFile).ToLower();
      }
      catch (Exception ex)
      {
        throw new Exception("Exception when calculating sha1 value of file " + resultFile, ex);
      }
      sw.Write(sha1 + "</sha1>" + lf);
      sw.Write("</mzXML>" + lf);
    }

    private void WriteXmlScanIndex(StreamWriter sw, List<Pair<int, long>> scanIndeies)
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
    }

    private List<int> GetOutputScans()
    {
      var result = new List<int>();
      int firstScan = this.rawFile.GetFirstSpectrumNumber();
      int lastScan = this.rawFile.GetLastSpectrumNumber();
      for (int scan = firstScan; scan <= lastScan; scan++)
      {
        int msLevel = this.rawFile.GetMsLevel(scan);
        if (this.fullMsOnly && 1 != msLevel)
        {
          continue;
        }

        result.Add(scan);
      }
      return result;
    }

    private void WriteXmlHeader(StreamWriter sw, FileInfo sourceFile, List<int> scans)
    {
      /* 
         xml header and namespace info 
      */
      sw.Write("<?xml version=\"1.0\" encoding=\"ISO-8859-1\"?>" + lf
               + "<mzXML" + lf);
      sw.Write(" xmlns=\"http://sashimi.sourceforge.net/schema_revision/mzXML_2.0\"" + lf
               + " xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\"" + lf
               +
               " xsi:schemaLocation=\"http://sashimi.sourceforge.net/schema_revision/mzXML_2.0 http://sashimi.sourceforge.net/schema_revision/mzXML_2.0/mzXML_idx_2.0.xsd\">" +
               lf);

      int firstScan = this.rawFile.GetFirstSpectrumNumber();
      int lastScan = this.rawFile.GetLastSpectrumNumber();
      double startTime = this.rawFile.ScanToRetentionTime(firstScan) * 60;
      double endTime = this.rawFile.ScanToRetentionTime(lastScan) * 60;

      /* 
         begin msRun 
      */
      sw.Write(MyConvert.Format(" <msRun scanCount=\"{0}\"" + lf
                             + "        startTime=\"PT{1:0.000}S\"" + lf
                             + "        endTime=\"PT{2:0.000}S\">" + lf,
                             scans.Count, startTime, endTime));

      string sha1;
      try
      {
        sha1 = HashUtils.GetSHA1Hash(sourceFile.FullName).ToLower();
      }
      catch (Exception ex)
      {
        throw new Exception("Exception when calculating sha1 value of file " + sourceFile.Name, ex);
      }

      /* 
         parent (raw input) file 
      */
      sw.Write("  <parentFile fileName=\"file://" + sourceFile.Name + "\"" + lf
               + "              fileType=\"RAWData\"" + lf
               + "              fileSha1=\"" + sha1 + "\"/>" + lf);

      /* 
         mass spec instrument section 
      */
      // get the instrument model
      string instModel = this.rawFile.GetInstModel();

      // get acquisition software version
      string instSoftVersion = this.rawFile.GetInstSoftwareVersion();

      sw.Write("  <msInstrument>" + lf
               + "   <msManufacturer category=\"msManufacturer\" value=\"ThermoFinnigan\"/>" + lf
               + "   <msModel category=\"msModel\" value=\"" + instModel + "\"/>" + lf
               + "   <msIonisation category=\"msIonisation\" value=\"ESI\"/>" + lf
               + "   <msMassAnalyzer category=\"msMassAnalyzer\" value=\"Ion Trap\"/>" + lf
               + "   <msDetector category=\"msDetector\" value=\"EMT\"/>" + lf);
      sw.Write("   <software type=\"acquisition\"" + lf
               + "             name=\"Xcalibur\"" + lf
               + "             version=\"" + instSoftVersion + "\"/>" + lf
               + "  </msInstrument>" + lf);

      /*
        data processing info
      */
      sw.Write("  <dataProcessing centroided=\"" + (this.doCentroid ? 1 : 0) + "\">" + lf
               + "    <software type=\"conversion\"" + lf
               + "              name=\"" + this.dataProcessingSoftware + "\"" + lf
               + "              version=\"" + this.dataProcessingSoftwareVersion + "\"/>" + lf);

      foreach (string dataProcessingOperationName in this.dataProcessingOperations.Keys)
      {
        sw.Write(MyConvert.Format("    <processingOperation name=\"{0}\"" + lf
                               + "                         value=\"{1}\"/>" + lf,
                               dataProcessingOperationName,
                               this.dataProcessingOperations[dataProcessingOperationName]));
      }
      /*
      if (MIN_PEAKS_PER_SPECTRA > 0)
      {
        // Note the use of the namevaluetype element!
        m_fout << "    <processingOperation name=\"min_peaks_per_spectra\"" << lf
        << "                         value=\"" << MIN_PEAKS_PER_SPECTRA << "\"/>" << lf;
        // And the comment field to give a little bit more information about the meaning of
        // the last element.
        m_fout << "    <comment>Scans with total number of peaks less than min_peaks_per_spectra were not included in this XML file</comment>" << lf;
      }
      */
      sw.Write("  </dataProcessing>" + lf);
      // end data processing info
    }

    private void WriteXmlScan(StreamWriter sw, int scan, List<Pair<int, long>> scanIndeies)
    {
      sw.Flush();
      scanIndeies.Add(new Pair<int, long>(scan, sw.BaseStream.Position));

      var rcf = new RawScanFilter();
      rcf.Filter = this.rawFile.GetFilterForScanNum(scan);
      double retentionTime = this.rawFile.ScanToRetentionTime(scan);
      PeakList<Peak> pkl = this.rawFile.GetMassListFromScanNum(scan, this.doCentroid);

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

      this.rawFile.GetScanHeaderInfoForScanNum(
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

      if (rcf.MsLevel > 1)
      {
        this.rawFile.GetPeakListInfo(scan, pkl);
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
  }
}