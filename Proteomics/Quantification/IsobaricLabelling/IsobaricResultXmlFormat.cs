using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using RCPA.Gui;
using System.Xml.Linq;
using RCPA.Proteomics.Spectrum;
using System.Xml;
using RCPA.Format;

namespace RCPA.Proteomics.Quantification.IsobaricLabelling
{
  /// <summary>
  /// 通过XmlReader和XmlWriter快速读写IsobaricResult文件。
  /// </summary>
  public class IsobaricResultXmlFormat : AbstractIsobaricResultXmlFormat
  {
    public void WriteChannels(XmlWriter xw, IsobaricType itype, IsobaricScan item)
    {
      xw.WriteStartElement("Ions");
      var refItems = itype.Channels;
      for (int i = 0; i < refItems.Count; i++)
      {
        xw.WriteElementFormat(refItems[i].Name, "{0:0.0}", item[i]);
      }
      xw.WriteEndElement();
    }

    public void WriteElementPeakList(XmlWriter xw, PeakList<Peak> pkls, string pklName, bool isPeakInWindow)
    {
      xw.WriteStartElement(pklName);

      foreach (var p in pkls)
      {
        xw.WriteStartElement("Peak");
        xw.WriteAttributeFormat("mz", "{0:0.#####}", p.Mz);
        xw.WriteAttributeFormat("intensity", "{0:0.0}", p.Intensity);
        xw.WriteAttribute("charge", p.Charge);
        if (isPeakInWindow)
        {
          xw.WriteAttribute("tag", p.Tag);
        }
        xw.WriteEndElement();
      }

      if (isPeakInWindow)
      {
        xw.WriteStartElement("Precursor");
        xw.WriteElement("MasterScan", pkls.Precursor.MasterScan);
        xw.WriteElementFormat("MonoIsotopicMass", "{0:0.00000}", pkls.Precursor.MonoIsotopicMass);
        xw.WriteElementFormat("IsolationMass", "{0:0.00000}", pkls.Precursor.IsolationMass);
        xw.WriteElementFormat("IsolationWidth", "{0:0.00}", pkls.Precursor.IsolationWidth);
        xw.WriteElement("Charge", pkls.Precursor.Charge);
        xw.WriteElementFormat("Intensity", "{0:0.0}", pkls.Precursor.Intensity);
        xw.WriteEndElement();
      }

      xw.WriteEndElement();
    }

    #region IFileFormat<ITraqResult> Members

    public override IsobaricResult ReadFromFile(string fileName)
    {
      IsobaricResult result = new IsobaricResult();
      result.Mode = IsobaricScanXmlUtils.GetMode(fileName);
      result.PlexType = IsobaricScanXmlUtils.GetIsobaricType(fileName);

      using (var reader = new IsobaricResultXmlFormatReader())
      {
        reader.Progress = this.Progress;
        reader.ReadReporters = this.HasReporters;
        reader.ReadPeaks = this.ReadPeaks;
        reader.Accept = this.Accept;

        reader.OpenFile(fileName);
        IsobaricScan item;
        while (null != (item = reader.Next(result.PlexType)))
        {
          result.Add(item);
        }
      }

      return result;
    }

    public override void WriteToFile(string fileName, IsobaricResult t)
    {
      using (XmlTextWriter w = new XmlTextWriter(fileName, Encoding.ASCII))
      {
        StartWriteDocument(w, t.Mode, t.PlexType.Name);

        foreach (var item in t)
        {
          WriteIsobaricItem(w, t.PlexType, item);
        }

        EndWriteDocument(w);
      }

      var indexBuilder = new IsobaricResultXmlIndexBuilder(true)
      {
        Progress = this.Progress
      };

      indexBuilder.Process(fileName);
    }

    public void EndWriteDocument(XmlTextWriter w)
    {
      w.WriteEndElement();
      w.WriteEndDocument();
    }

    public void StartWriteDocument(XmlTextWriter w, string mode, string isobaricType)
    {
      w.Formatting = Formatting.Indented;
      w.WriteStartDocument();
      w.WriteStartElement("IsobaricResult");
      w.WriteAttribute("Mode", mode);
      w.WriteAttribute("IsobaricType", isobaricType);
    }

    public void WriteIsobaricItem(XmlTextWriter w, IsobaricType itype, IsobaricScan item)
    {
      w.WriteStartElement("IsobaricScan");

      w.WriteElement("Experimental", item.Experimental);
      w.WriteElement("ScanMode", item.ScanMode);
      w.WriteElement("Scan", item.Scan.Scan);
      w.WriteElementFormat("RetentionTime", "{0:0.0}", item.Scan.RetentionTime);
      w.WriteElementFormat("IonInjectionTime", "{0:0.000}", item.Scan.IonInjectionTime);
      w.WriteElementFormat("PrecursorPercentage", "{0:0.000}", item.PrecursorPercentage);

      if (HasReporters)
      {
        WriteChannels(w, itype, item);
      }

      WriteElementPeakList(w, item.RawPeaks, "RawPeaks", false);

      WriteElementPeakList(w, item.PeakInIsolationWindow, "PeakInIsolationWindow", true);

      w.WriteEndElement();

      w.Flush();
    }

    #endregion

    public IsobaricResultXmlFormat(bool hasReporters = true, bool readPeaks = true) : base(hasReporters, readPeaks) { }
  }
}


