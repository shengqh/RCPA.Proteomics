using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace RCPA.Proteomics.Swath
{
  public class MzXmlSwathWindowsProcessor : AbstractThreadProcessor
  {
    private MzXmlSwathWindowsProcessorOptions _options;

    public MzXmlSwathWindowsProcessor(MzXmlSwathWindowsProcessorOptions options)
    {
      this._options = options;
    }

    private class Window
    {
      public double Start { get; set; }
      public double End { get; set; }
    }

    private static Window EstimateCorrectSwathWindow(double center, List<Window> windows)
    {
      var results = new List<Window>();
      foreach (var win in windows)
      {
        if (center < win.Start)
        {
          continue;
        }

        if (center > win.End)
        {
          break;
        }

        results.Add(win);
      }

      if (results.Count > 1)
      {
        throw new Exception(string.Format("More than one entry in the param file match the center {0}", center));
      }

      if (results.Count == 0)
      {
        throw new Exception(string.Format("No entry in the param file matches the center {0}", center));
      }

      return windows[0];
    }

    public override IEnumerable<string> Process()
    {
      var windows = (from l in File.ReadAllLines(_options.WindowsFile)
                     let parts = l.Split('\t')
                     let start = double.Parse(parts[0])
                     let end = double.Parse(parts[1])
                     orderby start
                     select new Window() { Start = start, End = end }).ToList();

      using (var sr = new StreamReader(_options.InputFile))
      {
        using (var sw = new StreamWriter(_options.OutputFile))
        {
          string line;
          while ((line = sr.ReadLine()) != null)
          {
            if (line.Contains("<scan"))
            {
              break;
            }
            sw.WriteLine(line);
          }

          var scanwindow = 0;
          var swathscan = 0;
          var mybuffer = new StringBuilder(line);
          while ((line = sr.ReadLine()) != null)
          {
            if (line.Contains("<scan"))
            {
              mybuffer.Clear();
              mybuffer.AppendLine(line);
            }
            else
            {
              mybuffer.AppendLine(line);
              if (line.Contains("</scan"))
              {
                // We count the number of MS1 scans in the swathscan variable
                var buffer = mybuffer.ToString();
                if (buffer.Contains("msLevel=\"1\""))
                {
                  swathscan++;
                  scanwindow = 0;
                }
                else
                {
                  buffer = RewriteSingleScan(buffer, swathscan, scanwindow, windows);
                  scanwindow++;
                }
                sw.Write(buffer);
              }
            }
          }

          sw.WriteLine("  </msRun>");
          sw.WriteLine("</mzXML>");
        }
      }

      return new[] { _options.OutputFile };
    }

    private static Regex precursorReg = new Regex("<precursorMz([^>]*)>([^<]*)</precursorMz>");
    private static Regex attrReg = new Regex(@"(.*)(windowWideness=\S+)(.*)");
    private string RewriteSingleScan(string buffer, int swathscan, int scanwindow, List<Window> windows)
    {
      if (scanwindow >= windows.Count)
      {
        throw new Exception(string.Format("There are more swath window scan {0} than your defined windows {1}: {2}", scanwindow, windows.Count, buffer));
      }

      var win = windows[scanwindow];
      var start = win.Start;
      var end = win.End;

      var preM = precursorReg.Match(buffer);
      //check if window wideness is already present
      if (preM.Success)
      {
        var oldCenter = double.Parse(preM.Groups[2].Value);
        if (!(oldCenter >= start && oldCenter <= end))
        {
          Progress.SetMessage("WARNING: previous precursorMz {0} did not fall inside the expected SWATH window {1} to {2}.", oldCenter, start, end);
          var estWin = EstimateCorrectSwathWindow(oldCenter, windows);
          start = estWin.Start;
          end = estWin.End;
        }
      }

      var middle = (start + end) / 2.0;
      var width = end - start;

      return ReplaceWindow(buffer, middle, width, preM.Groups[1].Value);
    }

    public static string ReplaceWindow(string input, double middle, double width, string oldAttr)
    {
      var m = attrReg.Match(oldAttr);
      string precursor;
      if (m.Success)
      {
        precursor = string.Format("<precursorMz{0}windowWideness=\"{1:0.0}\"{2}>{3:0.0}</precursorMz>", m.Groups[1].Value, width, m.Groups[3].Value, middle);
      }
      else
      {
        precursor = string.Format("<precursorMz windowWideness=\"{0:0.0}\"{1}>{2:0.0}</precursorMz>", width, oldAttr, middle);
      }

      return precursorReg.Replace(input, precursor);
    }
  }
}
