using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RCPA.Proteomics.Summary;
using System.IO;

namespace RCPA.Proteomics.Quantification.SILAC
{
  public class SilacScanTextWriter : IIdentifiedSpectrumWriter
  {
    private static Dictionary<string, Func<SilacPeakListPair, string>> funcMap;



    static SilacScanTextWriter()
    {
      funcMap = new Dictionary<string, Func<SilacPeakListPair, string>>();
      var list = new List<string>();
      AddFunc(list, "Scan", m => string.Format("{0}", m.Scan));
      AddFunc(list, "Retentiontime", m => string.Format("{0:0.00}", m.Light.ScanTimes[0].RetentionTime));
      AddFunc(list, "Identified", m => m.IsIdentified.ToString());
    }

    private static void AddFunc(List<string> list, string name, Func<SilacPeakListPair, string> func)
    {
      funcMap[name] = func;
      list.Add(name);
    }

    private string detailDirectory;

    private SilacQuantificationSummaryItemXmlFormat format = new SilacQuantificationSummaryItemXmlFormat();

    private string[] headers;

    public SilacScanTextWriter(string detailDirectory)
    {
      this.detailDirectory = detailDirectory;
      this.headers = new string[]{"Scan", "Retentiontime", "Identified",
          QuantificationItem.KEY_REFERENCE_INTENSITY,
          QuantificationItem.KEY_SAMPLE_INTENSITY,
          QuantificationItem.KEY_RATIO,
      "Protein"};
    }

    public SilacScanTextWriter(string detailDirectory, string header)
    {
      this.detailDirectory = detailDirectory;
      this.headers = (from h in header.Split('\t')
                      where !string.IsNullOrWhiteSpace(h)
                      select h).ToArray();
    }

    #region IIdentifiedSpectrumWriter Members

    public string Header
    {
      get
      {
        return "\t\t" + headers.Merge("\t");
      }
    }

    public void WriteToStream(System.IO.StreamWriter sw, IIdentifiedSpectrum peptide)
    {
      string detailFile = this.detailDirectory + "\\" + peptide.GetQuantificationItem().Filename;

      if (File.Exists(detailFile))
      {
        SilacQuantificationSummaryItem item = format.ReadFromFile(detailFile);

        Func<SilacPeakListPair, double> SampleIntensityFunc;
        Func<SilacPeakListPair, double> ReferenceIntensityFunc;

        if (item.SampleIsLight)
        {
          SampleIntensityFunc = m => m.LightIntensity;
          ReferenceIntensityFunc = m => m.HeavyIntensity;
        }
        else
        {
          SampleIntensityFunc = m => m.HeavyIntensity;
          ReferenceIntensityFunc = m => m.LightIntensity;
        }

        var valueFuncs = new List<Func<SilacPeakListPair, string>>();
        foreach (var header in headers)
        {
          switch (header)
          {
            case "Scan": valueFuncs.Add(m => string.Format("{0}", m.Scan));
              break;
            case "Retentiontime": valueFuncs.Add(m => string.Format("{0:0.00}", m.Light.ScanTimes[0].RetentionTime));
              break;
            case "Identified": valueFuncs.Add(m => m.IsIdentified.ToString());
              break;
            case QuantificationItem.KEY_REFERENCE_INTENSITY: valueFuncs.Add(m => string.Format("{0:0.0}", ReferenceIntensityFunc(m)));
              break;
            case QuantificationItem.KEY_SAMPLE_INTENSITY: valueFuncs.Add(m => string.Format("{0:0.0}", SampleIntensityFunc(m)));
              break;
            case QuantificationItem.KEY_RATIO: valueFuncs.Add(m => string.Format("{0:0.0000}", SampleIntensityFunc(m) / ReferenceIntensityFunc(m)));
              break;
            case "Protein": valueFuncs.Add(m => peptide.GetProteins("/"));
              break;
          };
        }

        foreach (var scan in item.ObservedEnvelopes)
        {
          if (scan.Enabled)
          {
            var ri = ReferenceIntensityFunc(scan);
            var si = SampleIntensityFunc(scan);
            if (ri == 0.0 || si == 0.0)
            {
              continue;
            }

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
