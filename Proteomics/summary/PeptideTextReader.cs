using System;
using System.Collections.Generic;
using System.IO;
using RCPA.Proteomics.Summary;
using RCPA.Gui;

namespace RCPA.Proteomics.Sequest
{
  public class PeptideTextReader : ProgressClass, IFileReader<List<IIdentifiedSpectrum>>
  {
    private string engineName;

    public PeptideTextReader(string engineName)
    {
      this.engineName = engineName;
    }

    public LineFormat<IIdentifiedSpectrum> PeptideFormat { get; set; }

    #region IFileReader<List<IIdentifiedSpectrum>> Members

    public List<IIdentifiedSpectrum> ReadFromFile(string filename)
    {
      if (!File.Exists(filename))
      {
        throw new FileNotFoundException("File not exist : " + filename);
      }

      var result = new List<IIdentifiedSpectrum>();

      long fileSize = new FileInfo(filename).Length;

      Progress.SetRange(0, fileSize);
      using (var br = new StreamReader(filename))
      {
        String line = br.ReadLine();

        PeptideFormat = new LineFormat<IIdentifiedSpectrum>(IdentifiedSpectrumPropertyConverterFactory.GetInstance(),
                                                            line, engineName);

        while ((line = br.ReadLine()) != null)
        {
          if (0 == line.Trim().Length)
          {
            break;
          }

          Progress.SetPosition(br.BaseStream.Position);

          result.Add(PeptideFormat.ParseString(line));
        }
      }

      string enzymeFile = filename + ".enzyme";
      if (File.Exists(enzymeFile))
      {
        new ProteaseFile().Fill(enzymeFile, result);
      }

      return result;
    }

    #endregion
  }
}