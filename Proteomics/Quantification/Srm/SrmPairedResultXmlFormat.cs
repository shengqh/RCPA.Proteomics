using RCPA.Gui;
using RCPA.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace RCPA.Proteomics.Quantification.Srm
{
  public class SrmPairedResultXmlFormat : ProgressClass, IFileFormat<SrmPairedResult>
  {
    #region IFileReader<SrmPairedResult> Members

    private SrmPairedPeptideItem ItemToPeptide(XElement item)
    {
      if (item == null)
      {
        return null;
      }

      SrmPairedPeptideItem result = new SrmPairedPeptideItem();
      result.Enabled = Convert.ToBoolean(item.Element("Enabled").Value);

      result.ProductIonPairs = new List<SrmPairedProductIon>();
      foreach (var p in item.Descendants("ProductIonPair"))
      {
        SrmPairedProductIon ion = new SrmPairedProductIon();

        ion.Enabled = Convert.ToBoolean(p.Element("Enabled").Value);
        ion.Ratio = MyConvert.ToDouble(p.Element("Ratio").Value);
        ion.Distance = MyConvert.ToDouble(p.Element("Distance").Value);
        ion.RegressionCorrelation = MyConvert.ToDouble(p.Element("RegressionCorrelation").Value);

        var scanEle = p.Elements("Scan");
        if (scanEle.Count() > 0)
        {
          var scans = (from a in scanEle
                       select new SrmPairedScan()
                       {
                         Light = SrmScanFileFormat.ElementToScan(a.Element("Light")),
                         Heavy = SrmScanFileFormat.ElementToScan(a.Element("Heavy"))
                       }).ToList();

          if (scans.Count > 0)
          {
            if (scans[0].Light != null)
            {
              var light = new SrmTransition(scans[0].Light.PrecursorMz, scans[0].Light.ProductMz);
              light.Intensities = (from s in scans
                                   select s.Light).ToList();
              ion.Light = light;
            }

            if (scans[0].Heavy != null)
            {
              var heavy = new SrmTransition(scans[0].Heavy.PrecursorMz, scans[0].Heavy.ProductMz);
              heavy.Intensities = (from s in scans
                                   select s.Heavy).ToList();
              ion.Heavy = heavy;
            }
          }
        }
        else
        {
          ion.Light = ElementToTransaction(p.Element("LightTransaction"));
          ion.Heavy = ElementToTransaction(p.Element("HeavyTransaction"));
        }

        if (p.Element("LightArea") != null)
        {
          ion.LightArea = MyConvert.ToDouble(p.Element("LightArea").Value);
          ion.HeavyArea = MyConvert.ToDouble(p.Element("HeavyArea").Value);
        }

        result.ProductIonPairs.Add(ion);
      }

      if (item.Element("Ratio") != null)
      {
        result.Ratio = MyConvert.ToDouble(item.Element("Ratio").Value);
        if (item.Element("SD").Value.Equals("-"))
        {
          result.SD = double.NaN;
        }
        else
        {
          result.SD = MyConvert.ToDouble(item.Element("SD").Value);
        }
      }
      else
      {
        result.CalculatePeptideRatio();
      }

      var objname = string.Empty;
      var pepname = string.Empty;
      var pepcharge = 0;

      if (item.Element("ObjectName") != null)
      {
        objname = item.Element("ObjectName").Value;
      }

      if (item.Element("PrecursorFormula") != null)
      {
        pepname = item.Element("PrecursorFormula").Value;
      }

      if (item.Element("PrecursorCharge") != null)
      {
        pepcharge = int.Parse(item.Element("PrecursorCharge").Value);
      }

      result.ProductIonPairs.ForEach(m =>
      {
        if (m.Light != null)
        {
          m.Light.ObjectName = objname;
          m.Light.PrecursorFormula = pepname;
          m.Light.PrecursorCharge = pepcharge;
        }

        if (m.Heavy != null)
        {
          m.Heavy.ObjectName = objname;
          m.Heavy.PrecursorFormula = pepname;
          m.Heavy.PrecursorCharge = pepcharge;
        }
      });

      return result;
    }

    private SrmTransition ElementToTransaction(XElement xElement)
    {
      if (xElement == null)
      {
        return null;
      }

      SrmTransition result = new SrmTransition(0, 0);

      if (xElement.Element("Noise") != null)
      {
        result.Noise = MyConvert.ToDouble(xElement.Element("Noise").Value);
      }
      if (xElement.Element("SignalToNoise") != null)
      {
        result.SignalToNoise = MyConvert.ToDouble(xElement.Element("SignalToNoise").Value);
      }

      result.Intensities = (from a in xElement.Descendants("Scan")
                            select SrmScanFileFormat.ElementToScan(a)).ToList();

      if (result.Intensities.Count > 0)
      {
        result.PrecursorMZ = result.Intensities[0].PrecursorMz;
        result.ProductIon = result.Intensities[0].ProductMz;
      }

      return result;
    }

    private XElement TransactionToElement(SrmTransition trans, string name)
    {
      if (trans == null)
      {
        return null;
      }

      return new XElement(name,
        new XElement("Noise", trans.Noise),
        new XElement("SignalToNoise", trans.SignalToNoise),
        from s in trans.Intensities
        select SrmScanFileFormat.ScanToElement(s, "Scan"));
    }

    private XElement PeptideToItem(SrmPairedPeptideItem item)
    {
      if (null == item)
      {
        return null;
      }

      return new XElement("Item",
        new XElement("Enabled", item.Enabled.ToString()),
        new XElement("Ratio", MyConvert.Format("{0:0.0000}", item.Ratio)),
        new XElement("SD", double.IsInfinity(item.SD) || double.IsNaN(item.SD) ? "-" : MyConvert.Format("{0:0.0000}", item.SD)),
        new XElement("ObjectName", item.ObjectName),
        new XElement("PrecursorFormula", item.PrecursorFormula),
        new XElement("PrecursorCharge", item.PrecursorCharge),
        from p in item.ProductIonPairs
        select new XElement("ProductIonPair",
          new XElement("Enabled", p.Enabled.ToString()),
          new XElement("Ratio", MyConvert.Format("{0:0.0000}", p.Ratio)),
          new XElement("Distance", MyConvert.Format("{0:0.0000}", p.Distance)),
          new XElement("RegressionCorrelation", MyConvert.Format("{0:0.0000}", p.RegressionCorrelation)),
          new XElement("LightArea", MyConvert.Format("{0:0.0}", p.LightArea)),
          new XElement("HeavyArea", MyConvert.Format("{0:0.0}", p.HeavyArea)),
          new XElement("Noise", MyConvert.Format("{0:0.00}", p.Noise)),
          new XElement("LightSignalToNoise", MyConvert.Format("{0:0.00}", p.LightSignalToNoise)),
          new XElement("HeavySignalToNoise", MyConvert.Format("{0:0.00}", p.HeavySignalToNoise)),
          TransactionToElement(p.Light, "LightTransaction"),
          TransactionToElement(p.Heavy, "HeavyTransaction")));
    }

    public SrmPairedResult ReadFromFile(string fileName)
    {
      if (!File.Exists(fileName))
      {
        throw new FileNotFoundException(fileName);
      }

      XElement root = XElement.Load(fileName);

      var result = new SrmPairedResult();
      result.FileName = fileName;

      result.Options.FromXml(root);

      var items = root.Descendants("Item").ToList();
      Progress.SetRange(0, items.Count);

      foreach (var item in items)
      {
        Progress.Increment(1);
        result.Add(ItemToPeptide(item));
      }

      result.CalculateTransitionRatio();

      result.ForEach(m => m.ProductIonPairs.ForEach(n =>
      {
        n.FileName = result.PureFileName;
      }));

      result.AssignDecoy();

      return result;
    }

    #endregion

    #region IFileWriter<SrmPairedResult> Members

    public void WriteToFile(string fileName, SrmPairedResult t)
    {
      XElement root = new XElement("MRM",
        t.Options.ToXml(false),
        from c in t
        select PeptideToItem(c));

      root.Save(fileName);

      t.FileName = fileName;
    }

    #endregion
  }
}
