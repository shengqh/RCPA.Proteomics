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
using RCPA.Utils;

namespace RCPA.Proteomics.Quantification.IsobaricLabelling
{
  /// <summary>
  /// 通过XmlReader和XmlWriter快速读写IsobaricResult文件。
  /// </summary>
  public class IsobaricResultXmlFormat : AbstractIsobaricResultXmlFormat
  {
    public void WriteChannels(XmlWriter xw, List<UsedChannel> used, IsobaricScan item)
    {
      xw.WriteStartElement("Reporters");
      for (int i = 0; i < used.Count; i++)
      {
        xw.WriteStartElement("Reporter");
        xw.WriteAttribute("name", used[i].Name);
        xw.WriteAttributeFormat("mz", "{0:0.#####}", item[i].Mz);
        xw.WriteAttributeFormat("intensity", "{0:0.0}", item[i].Intensity);
        xw.WriteEndElement();
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
      result.PlexType = IsobaricScanXmlUtils.GetIsobaricType(fileName);
      result.UsedChannels = IsobaricScanXmlUtils.GetUsedChannels(fileName, result.PlexType);
      result.Comments = IsobaricScanXmlUtils.GetComments(fileName);

      using (var reader = new IsobaricResultXmlFormatReader())
      {
        reader.Progress = this.Progress;
        reader.ReadReporters = this.HasReporters && result.UsedChannels != null;
        reader.ReadPeaks = this.ReadPeaks;
        reader.Accept = this.Accept;

        reader.OpenFile(fileName);
        IsobaricScan item;
        while (null != (item = reader.Next(result.UsedChannels)))
        {
          result.Add(item);
        }
      }

      return result;
    }

    public override void WriteToFile(string fileName, IsobaricResult t)
    {
      using (XmlTextWriter w = XmlUtils.CreateWriter(fileName, Encoding.ASCII))
      {
        StartWriteDocument(w, t);

        foreach (var item in t)
        {
          WriteIsobaricItem(w, t, item);
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

    public void StartWriteDocument(XmlTextWriter w, IsobaricResult ir)
    {
      w.Formatting = Formatting.Indented;
      w.WriteStartDocument();
      w.WriteStartElement("IsobaricResult");
      w.WriteAttribute("IsobaricType", ir.PlexType.ToString());

      //Write used channels
      if (ir.UsedChannels != null && ir.UsedChannels.Count > 0)
      {
        w.WriteAttribute("HasUsedChannel", true.ToString());
        w.WriteStartElement("UsedChannels");
        foreach (var channel in ir.UsedChannels)
        {
          w.WriteStartElement("UsedChannel");
          w.WriteAttribute("Index", channel.Index);
          w.WriteAttribute("Name", channel.Name);
          w.WriteAttribute("Mz", channel.Mz);
          w.WriteEndElement();
        }
        w.WriteEndElement();
      }

      //Write comments, including parameters
      w.WriteStartElement("Comments");
      foreach (var comment in ir.Comments)
      {
        w.WriteStartElement("Comment");
        w.WriteAttribute("Key", comment.Key);
        w.WriteAttribute("Value", comment.Value);
        w.WriteEndElement();
      }
      w.WriteEndElement();
    }

    public void WriteIsobaricItem(XmlTextWriter w, IsobaricResult ir, IsobaricScan item)
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
        WriteChannels(w, ir.UsedChannels, item);
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


