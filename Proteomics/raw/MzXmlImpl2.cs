using RCPA.Proteomics.Spectrum;
using RCPA.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace RCPA.Proteomics.Raw
{
  public class MzXMLImpl2 : AbstractRawFile
  {
    private XElement docRoot;

    private XElement msRun;

    private int numSpectra;

    private bool centroid = true;

    private int firstScan;

    private int lastScan;

    private List<XElement> scanMap;

    public MzXMLImpl2()
    {
      this.FileName = string.Empty;
    }

    public MzXMLImpl2(string filename)
    {
      Open(filename);
    }

    #region IRawFile Members

    public override int GetNumSpectra()
    {
      return this.numSpectra;
    }

    public override bool IsValid()
    {
      return this.docRoot != null;
    }

    public override void Open(string filename)
    {
      this.docRoot = XElement.Load(filename);
      this.FileName = filename;

      this.msRun = docRoot.FindFirstDescendant("msRun");

      this.numSpectra = Convert.ToInt32(this.msRun.Attribute("scanCount").Value);

      var dataProcessingNode = msRun.FindFirstDescendant("dataProcessing");
      if (null != dataProcessingNode.Attribute("centroided"))
      {
        this.centroid = dataProcessingNode.Attribute("centroided").Value.Equals("1");
      }

      this.scanMap = new List<XElement>(this.numSpectra);
      for (int i = 0; i < this.numSpectra; i++)
      {
        this.scanMap.Add(null);
      }

      AddScanMap(msRun, scanMap);

      this.firstScan = scanMap.FindIndex(m => m != null);
      this.lastScan = scanMap.FindLastIndex(m => m != null);
    }

    private void AddScanMap(XElement node, List<XElement> scanMap)
    {
      if (node.Name.LocalName == "scan")
      {
        int scan = Convert.ToInt32(node.Attribute("num").Value);
        while (scan >= scanMap.Count)
        {
          scanMap.Add(null);
        }
        scanMap[scan] = node;
      }

      foreach (var subNode in node.Elements())
      {
        AddScanMap(subNode, scanMap);
      }
    }

    public override bool Close()
    {
      this.scanMap.Clear();
      this.scanMap = null;
      this.msRun = null;
      this.docRoot = null;

      this.FileName = string.Empty;

      GC.Collect();
      GC.WaitForFullGCComplete();

      return true;
    }

    public override int GetMsLevel(int scan)
    {
      var node = GetValidSpectrumNode(scan);
      return int.Parse(node.Attribute("msLevel").Value);
    }

    public string GetScanType(int scan)
    {
      var node = GetValidSpectrumNode(scan);
      return node.Attribute("scanType").Value;
    }

    public override double ScanToRetentionTime(int scan)
    {
      var node = GetValidSpectrumNode(scan);
      string rt = node.Attribute("retentionTime").Value;
      Match m = Regex.Match(rt, "PT(.+)S");
      return MyConvert.ToDouble(m.Groups[1].Value) / 60.0;
    }

    public override PeakList<Peak> GetPeakList(int scan)
    {
      double[] mzAndIntensity = GetMzAndIntensities(scan);

      PeakList<Peak> result = new PeakList<Peak>();
      for (int i = 0; i < mzAndIntensity.Length; i += 2)
      {
        result.Add(new Peak(mzAndIntensity[i], mzAndIntensity[i + 1]));
      }

      return result;
    }

    public override PeakList<Peak> GetPeakList(int scan, double minMz, double maxMz)
    {
      double[] mzAndIntensity = GetMzAndIntensities(scan);

      PeakList<Peak> result = new PeakList<Peak>();
      for (int i = 0; i < mzAndIntensity.Length; i += 2)
      {
        if (mzAndIntensity[i] < minMz)
        {
          continue;
        }

        if (mzAndIntensity[i] > maxMz)
        {
          break;
        }

        result.Add(new Peak(mzAndIntensity[i], mzAndIntensity[i + 1]));
      }

      return result;
    }

    public override bool IsScanValid(int scan)
    {
      return null != GetSpectrumNode(scan);
    }

    public override int GetFirstSpectrumNumber()
    {
      if (0 == this.numSpectra)
      {
        return 0;
      }

      return firstScan;
    }

    public override int GetLastSpectrumNumber()
    {
      if (0 == this.numSpectra)
      {
        return 0;
      }

      return lastScan;
    }

    public override bool IsProfileScanForScanNum(int scan)
    {
      return !this.centroid;
    }

    public override bool IsCentroidScanForScanNum(int scan)
    {
      return this.centroid;
    }

    public override Peak GetPrecursorPeak(int scan)
    {
      Peak result = new Peak();

      if (GetMsLevel(scan) > 1)
      {
        XElement node = GetValidSpectrumNode(scan);

        XElement precursor = node.FindFirstDescendant("precursorMz");
        result.Mz = MyConvert.ToDouble(precursor.GetValue());
        result.Intensity = MyConvert.ToDouble(precursor.Attribute("precursorIntensity").Value);
        if (precursor.Attributes().Count() > 1)
        {
          var chargeItem = precursor.Attribute("precursorCharge");
          if (chargeItem != null)
          {
            result.Charge = Convert.ToInt32(chargeItem.Value);
          }
          else
          {
            result.Charge = 0;
          }
        }
      }

      return result;
    }

    #endregion

    private XElement GetSpectrumNode(int scan)
    {
      if (scan < 0 || scan >= scanMap.Count)
      {
        return null;
      }

      return scanMap[scan];
    }

    private XElement GetValidSpectrumNode(int scan)
    {
      XElement result = GetSpectrumNode(scan);

      if (null == result)
      {
        throw new Exception(MyConvert.Format("Scan {0} not exists, call IsValidScan first.", scan));
      }

      return result;
    }

    private double[] GetMzAndIntensities(int scan)
    {
      XElement node = GetValidSpectrumNode(scan);

      int expectCount = Convert.ToInt32(node.Attribute("peaksCount").Value) * 2;

      if (0 == expectCount)
      {
        return new double[] { };
      }

      var peaksItem = node.FindFirstDescendant("peaks");

      var precision = Convert.ToInt32(peaksItem.Attribute("precision").Value);
      string peaksStr = peaksItem.GetValue();

      bool isBigEndian = peaksItem.Attribute("byteOrder").Value.Equals("network");

      byte[] dataBytes = System.Convert.FromBase64String(peaksStr);
      if (precision == 32)
      {
        return MathUtils.Byte32ToDoubleList(expectCount, isBigEndian, dataBytes);
      }
      else
      {
        return MathUtils.Byte64ToDoubleList(expectCount, isBigEndian, dataBytes);
      }
    }

    public override string GetScanMode(int scan)
    {
      return "";
    }

    public string FileDescription
    {
      get { return "SPC/Institute for Systems Biology mzXML File"; }
    }

    public override ScanTime GetScanTime(int scan)
    {
      return new ScanTime(scan, ScanToRetentionTime(scan));
    }
  }
}