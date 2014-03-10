using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RCPA.Proteomics.Summary;
using System.IO;

namespace RCPA.Proteomics.Quantification.O18
{
  public class O18ScanTextWriter : IIdentifiedSpectrumWriter
  {
    private static string DEFAULT_HEADERS;
    private static Dictionary<string, Func<O18QuanEnvelope, string>> funcMap;

    static O18ScanTextWriter()
    {
      funcMap = new Dictionary<string, Func<O18QuanEnvelope, string>>();
      var list = new List<string>();
      AddFunc(list, "Scan", m => string.Format("{0}", m.Scan));
      AddFunc(list, "Retentiontime", m => string.Format("{0:0.00}", m.ScanTimes[0].RetentionTime));
      AddFunc(list, "Identified", m => m.IsIdentified.ToString());
      AddFunc(list, O18QuantificationConstants.INIT_O16_INTENSITY, m => string.Format("{0:0.0}", m.SpeciesAbundance.O16));
      AddFunc(list, O18QuantificationConstants.INIT_O18_1_INTENSITY, m => string.Format("{0:0.0}", m.SpeciesAbundance.O181));
      AddFunc(list, O18QuantificationConstants.INIT_O18_2_INTENSITY, m => string.Format("{0:0.0}", m.SpeciesAbundance.O182));
      AddFunc(list, O18QuantificationConstants.O16_INTENSITY, m => string.Format("{0:0.0}", m.SampleAbundance.O16));
      AddFunc(list, O18QuantificationConstants.O18_INTENSITY, m => string.Format("{0:0.0}", m.SampleAbundance.O18));
      AddFunc(list, O18QuantificationConstants.O18_LABELING_EFFICIENCY, m => string.Format("{0:0.00}", m.SampleAbundance.LabellingEfficiency));
      AddFunc(list, O18QuantificationConstants.O18_RATIO, m => string.Format("{0:0.00}", m.SampleAbundance.Ratio));
      DEFAULT_HEADERS = list.Merge("\t");
    }

    private static void AddFunc(List<string> list, string name, Func<O18QuanEnvelope, string> func)
    {
      funcMap[name] = func;
      list.Add(name);
    }

    private string detailDirectory;

    private O18QuantificationSummaryItemXmlFormat format = new O18QuantificationSummaryItemXmlFormat();

    private string _header;

    private List<Func<O18QuanEnvelope, string>> valueFuncs;

    public O18ScanTextWriter(string detailDirectory, string header)
    {
      this.detailDirectory = detailDirectory;
      var parts = (from h in header.Split('\t')
                   let l = h.Trim()
                   where l.Length > 0
                   select l).ToList();

      valueFuncs = new List<Func<O18QuanEnvelope, string>>();
      foreach (var part in parts)
      {
        if (!funcMap.ContainsKey(part))
        {
          throw new Exception("Unknow header of O18 scan text writer : " + part);
        }

        valueFuncs.Add(funcMap[part]);
      }
      this._header = "\t\t" + parts.Merge("\t");
    }

    public O18ScanTextWriter(string detailDirectory) : this(detailDirectory, DEFAULT_HEADERS) { }

    #region IIdentifiedSpectrumWriter Members

    public string Header
    {
      get
      {
        return _header;
      }
    }

    public void WriteToStream(System.IO.StreamWriter sw, IIdentifiedSpectrum peptide)
    {
      string detailFile = this.detailDirectory + "\\" + peptide.Annotations[O18QuantificationConstants.O18_RATIO_FILE];

      if (File.Exists(detailFile))
      {
        O18QuantificationSummaryItem item = format.ReadFromFile(detailFile);
        var calc = item.GetCalculator();

        foreach (var scan in item.ObservedEnvelopes)
        {
          if (scan.Enabled)
          {
            scan.CalculateFromProfile(item.PeptideProfile, calc);
            sw.Write("\t");
            foreach (var func in valueFuncs)
            {
              sw.Write("\t{0}", func(scan));
            }
            sw.WriteLine();
          }
        }
      }
    }

    #endregion
  }
}
