using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using RCPA.Utils;
using System.IO;
using System.Collections.ObjectModel;
using System.Diagnostics;
using RCPA.Proteomics.Spectrum;

namespace RCPA.Proteomics.Raw
{
  public class MzDataImpl : AbstractRawFile
  {
    private bool valid;

    private XmlDocument doc;

    private XmlHelper xmlHelper = null;

    private XmlNode spectrumListNode;

    public MzDataImpl()
    {
      this.valid = false;
      this.FileName = string.Empty;
    }

    public MzDataImpl(string rawFilename)
    {
      this.valid = false;
      Open(rawFilename);
    }

    private XmlNode lastNode;

    #region IRawFile Members

    public override bool IsScanValid(int scan)
    {
      return null != GetSpectrumNode(scan);
    }

    public override int GetFirstSpectrumNumber()
    {
      if (null == spectrumListNode.FirstChild)
      {
        return 0;
      }

      return Convert.ToInt32(spectrumListNode.FirstChild.Attributes["id"].Value);
    }

    public override int GetLastSpectrumNumber()
    {
      if (null == spectrumListNode.LastChild)
      {
        return 0;
      }

      return Convert.ToInt32(spectrumListNode.LastChild.Attributes["id"].Value);
    }

    public override bool IsProfileScanForScanNum(int scan)
    {
      return !IsCentroidScanForScanNum(scan);
    }

    public override bool IsCentroidScanForScanNum(int scan)
    {
      XmlNode spectrumNode = GetValidSpectrumNode(scan);

      XmlNode acqSpecificationNode = spectrumNode.SelectSingleNode("spectrumDesc/spectrumSettings/acqSpecification");

      return acqSpecificationNode.Attributes["spectrumType"].Value.Equals("discrete");
    }

    public override Peak GetPrecursorPeak(int scan)
    {
      if (1 == GetMsLevel(scan))
      {
        return new Peak(0, 0);
      }

      XmlNode spectrumNode = GetValidSpectrumNode(scan);
      XmlNode ionSelectionNode = spectrumNode.SelectSingleNode("spectrumDesc/precursorList/precursor/ionSelection");
      XmlNode mzNode = xmlHelper.GetFirstChildByNameAndAttribute(ionSelectionNode, "cvParam", "accession", "PSI:1000040");

      return new Peak(MyConvert.ToDouble(mzNode.Attributes["value"].Value), 0.0);
    }

    public override string GetScanMode(int scan)
    {
      XmlNode spectrumNode = GetValidSpectrumNode(scan);
      XmlNode ionSelectionNode = spectrumNode.SelectSingleNode("spectrumDesc/precursorList/precursor/activation");
      XmlNode mzNode = xmlHelper.GetFirstChildByNameAndAttribute(ionSelectionNode, "cvParam", "accession", "PSI:1000044");
      if (mzNode != null)
      {
        return mzNode.Attributes["value"].Value.ToLower();
      }
      else
      {
        return "";
      }
    }

    public override void Open(string fileName)
    {
      Close();

      if (!File.Exists(fileName))
      {
        throw new ArgumentException("File not exists : " + fileName);
      }

      this.doc = new XmlDocument();
      this.doc.Load(fileName);
      this.FileName = fileName;

      this.xmlHelper = new XmlHelper(this.doc);

      this.spectrumListNode = xmlHelper.GetFirstChild(doc.DocumentElement, "spectrumList");

      this.valid = true;
      return;
    }

    public override bool Close()
    {
      this.doc = null;
      this.spectrumListNode = null;
      this.FileName = string.Empty;

      return true;
    }

    public override int GetNumSpectra()
    {
      return Convert.ToInt32(spectrumListNode.Attributes["count"].Value);
    }

    public override bool IsValid()
    {
      return valid;
    }

    public override int GetMsLevel(int scan)
    {
      XmlNode spectrumInstrument = GetSpectrumInstrument(scan);
      return Convert.ToInt32(spectrumInstrument.Attributes["msLevel"].Value);
    }

    public override double ScanToRetentionTime(int scan)
    {
      XmlNode spectrumInstrument = GetSpectrumInstrument(scan);
      XmlNode rtNode = xmlHelper.GetFirstChildByNameAndAttribute(spectrumInstrument, "cvParam", "accession", "PSI:1000038");
      return MyConvert.ToDouble(rtNode.Attributes["value"].Value);
    }

    public override PeakList<Peak> GetPeakList(int scan)
    {
      double[][] pkl = GetPeakArray(scan);

      PeakList<Peak> result = new PeakList<Peak>();
      for (int i = 0; i < pkl[0].Length; i++)
      {
        result.Add(new Peak(pkl[0][i], pkl[1][i]));
      }

      return result;
    }

    public override PeakList<Peak> GetPeakList(int scan, double minMz, double maxMz)
    {
      double[][] pkl = GetPeakArray(scan);

      PeakList<Peak> result = new PeakList<Peak>();
      for (int i = 0; i < pkl[0].Length; i++)
      {
        if (pkl[0][i] < minMz)
        {
          continue;
        }

        if (pkl[0][i] > maxMz)
        {
          break;
        }

        result.Add(new Peak(pkl[0][i], pkl[1][i]));
      }

      return result;
    }

    #endregion

    private XmlNode GetSpectrumNode(int scan)
    {
      if (lastNode != null && lastNode.Attributes["id"].Value.Equals(scan.ToString()))
      {
        return lastNode;
      }

      lastNode = xmlHelper.GetFirstChildByNameAndAttribute(spectrumListNode, "spectrum", "id", scan.ToString());
      return lastNode;
    }

    private XmlNode GetValidSpectrumNode(int scan)
    {
      XmlNode result = GetSpectrumNode(scan);

      if (null == result)
      {
        throw new Exception(MyConvert.Format("Scan {0} not exists, call IsValidScan first.", scan));
      }

      return result;
    }

    private XmlNode GetSpectrumInstrument(int scan)
    {
      XmlNode scanNode = GetValidSpectrumNode(scan);

      XmlNode spectrumInstrument = scanNode.SelectSingleNode("spectrumDesc/spectrumSettings/spectrumInstrument");

      return spectrumInstrument;
    }

    private static double[] DecodePeakList(XmlNode data)
    {
      int dataPrecision = Convert.ToInt32(data.Attributes["precision"].Value);

      int expectedLength = Convert.ToInt32(data.Attributes["length"].Value);

      bool isBigEndian = data.Attributes["endian"].Value.Equals("big");

      byte[] dataBytes = System.Convert.FromBase64String(data.InnerText);

      double[] dataMz;
      if (dataPrecision == 32)
      {
        dataMz = MathUtils.Byte32ToDoubleList(expectedLength, isBigEndian, dataBytes);
      }
      else  //dataPrecision == 64
      {
        dataMz = MathUtils.Byte64ToDoubleList(expectedLength, isBigEndian, dataBytes);
      }
      return dataMz;
    }

    private double[][] GetPeakArray(int scan)
    {
      double[][] pkl = new double[2][];

      XmlNode spectrum = GetValidSpectrumNode(scan);

      XmlNode dataMzNode = spectrum.SelectSingleNode("mzArrayBinary/data");

      pkl[0] = DecodePeakList(dataMzNode);

      XmlNode dataIntNode = spectrum.SelectSingleNode("intenArrayBinary/data");

      pkl[1] = DecodePeakList(dataIntNode);
      return pkl;
    }

    #region IRawFile Members

    public override ScanTime GetScanTime(int scan)
    {
      return new ScanTime(scan, ScanToRetentionTime(scan));
    }

    public override string GetFileNameWithoutExtension(string rawFileName)
    {
      if (rawFileName.ToLower().EndsWith(".mzdata.xml"))
      {
        return Path.GetFileNameWithoutExtension(Path.GetFileNameWithoutExtension(rawFileName));
      }
      else
      {
        return Path.GetFileNameWithoutExtension(rawFileName);
      }
    }

    #endregion
  }
}
