using RCPA.Gui;
using RCPA.Proteomics.Summary;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace RCPA.Proteomics.Percolator
{
  public class PercolatorFileParser : ProgressClass, ISpectrumParser
  {
    private ISpectrumParser baseParser;

    public PercolatorFileParser(ISpectrumParser baseParser)
    {
      this.baseParser = baseParser;
    }

    public SearchEngineType Engine
    {
      get { return baseParser.Engine; }
    }

    public ITitleParser TitleParser
    {
      get
      {
        return baseParser.TitleParser;
      }
      set
      {
        baseParser.TitleParser = value;
      }
    }

    public List<IIdentifiedSpectrum> ReadFromFile(string fileName)
    {
      var result = baseParser.ReadFromFile(fileName);

      var resultMap = result.ToDictionary(m => m.Id.ToString());

      var input = GetInputFile(fileName);

      using (var sr = new StreamReader(input))
      {
        string line;

        while ((line = sr.ReadLine()) != null)
        {
          if (line.Contains("<fragSpectrumScan"))
          {
            var scanNumber = line.StringAfter("scanNumber=\"").StringBefore("\"");
            IIdentifiedSpectrum spec;
            if (resultMap.TryGetValue(scanNumber, out spec))
            {
              line = sr.ReadLine();
              spec.Tag = line.StringAfter("id=\"").StringBefore("\"");
            }
            else
            {
              Console.WriteLine("Cannot find {0} in database", scanNumber);
            }
          }
        }
      }

      result.ForEach(m =>
      {
        if (string.IsNullOrEmpty(m.Tag))
        {
          Console.WriteLine("Cannot find query {0} at input file, score {1}", m.Query.FileScan.LongFileName, m.Score);
        }
      });

      var inputMap = result.Where(m => !string.IsNullOrEmpty(m.Tag)).ToDictionary(m => m.Tag);

      var output = GetOutputFile(fileName);
      XElement root = XElement.Load(output);
      var psms = root.FindElement("psms").FindElements("psm");
      foreach (var psm in psms)
      {
        var id = psm.FindAttribute("psm_id").Value;
        IIdentifiedSpectrum spec;
        if (inputMap.TryGetValue(id, out spec))
        {
          spec.SpScore = double.Parse(psm.FindElement("svm_score").Value);
        }
      }

      return result;
    }

    public static string GetOutputFile(string fileName)
    {
      return FileUtils.ChangeExtension(fileName, ".output.xml");
    }

    public static string GetInputFile(string fileName)
    {
      return FileUtils.ChangeExtension(fileName, ".input.xml");
    }
  }
}
