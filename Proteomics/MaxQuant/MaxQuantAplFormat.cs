using RCPA.Gui;
using RCPA.Proteomics.Spectrum;
using RCPA.Proteomics.Summary;
using System;
using System.Collections.Generic;
using System.IO;

namespace RCPA.Proteomics.MaxQuant
{
  public class MaxQuantAplFormat : ProgressClass, IFileReader<List<PeakList<Peak>>>
  {
    private ITitleParser parser;
    public MaxQuantAplFormat(ITitleParser parser)
    {
      this.parser = parser;
    }

    #region IFileReader<List<PeakList<Peak>>> Members

    public List<PeakList<Peak>> ReadFromFile(string fileName)
    {
      List<PeakList<Peak>> result = new List<PeakList<Peak>>();

      using (StreamReader sr = new StreamReader(fileName))
      {
        Progress.SetRange(0, sr.BaseStream.Length);
        string line;
        Dictionary<string, string> headers = new Dictionary<string, string>();
        List<string> peaks = new List<string>();
        while ((line = sr.ReadLine()) != null)
        {
          if (line.Trim().Equals("peaklist start"))
          {
            Progress.SetPosition(StreamUtils.GetCharpos(sr));

            headers.Clear();
            peaks.Clear();

            bool inHeader = true;
            while ((line = sr.ReadLine()) != null)
            {
              var tline = line.Trim();
              if (tline.Equals("peaklist end"))
              {
                break;
              }

              if (tline.Length == 0)
              {
                continue;
              }

              if (!inHeader)
              {
                peaks.Add(tline);
              }
              else if (Char.IsLetter(tline[0]))
              {
                var pos = tline.IndexOf('=');
                var key = tline.Substring(0, pos);
                var value = tline.Substring(pos + 1);
                headers[key] = value;
              }
              else
              {
                inHeader = false;
                peaks.Add(tline);
              }
            }

            if (headers.Count > 0 && peaks.Count > 0)
            {
              PeakList<Peak> pkl = new PeakList<Peak>();
              pkl.PrecursorMZ = MyConvert.ToDouble(headers["mz"]);
              pkl.PrecursorCharge = Convert.ToInt32(headers["charge"]);
              pkl.MsLevel = 2;
              pkl.ScanMode = headers["fragmentation"];
              SequestFilename sf = parser.GetValue(headers["header"]);
              pkl.ScanTimes.Add(new ScanTime(sf.FirstScan, 0.0));
              pkl.Experimental = sf.Experimental;

              result.Add(pkl);

              foreach (var l in peaks)
              {
                var p = l.Split('\t');
                if (p.Length > 1)
                {
                  pkl.Add(new Peak(MyConvert.ToDouble(p[0]), MyConvert.ToDouble(p[1])));
                }
              }
            }
          }
        }
      }

      return result;
    }

    #endregion
  }
}
