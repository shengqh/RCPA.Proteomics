using System;
using System.Collections.Generic;
using System.IO;
using RCPA.Proteomics.Summary;
using RCPA.Gui;

namespace RCPA.Proteomics.Sequest
{
  public class ProteinTextReader : ProgressClass, IFileReader<List<IIdentifiedProtein>>
  {
    private string engineName;

    public ProteinTextReader(string engineName)
    {
      this.engineName = engineName;
    }

    public LineFormat<IIdentifiedProtein> ProteinFormat { get; set; }

    #region IFileReader<List<IIdentifiedProtein>> Members

    public List<IIdentifiedProtein> ReadFromFile(string filename)
    {
      if (!File.Exists(filename))
      {
        throw new FileNotFoundException("File not exist : " + filename);
      }

      var result = new List<IIdentifiedProtein>();

      long fileSize = new FileInfo(filename).Length;

      Progress.SetRange(0, fileSize);
      using (var br = new StreamReader(filename))
      {
        String line = br.ReadLine();

        ProteinFormat = new LineFormat<IIdentifiedProtein>(IdentifiedProteinPropertyConverterFactory.GetInstance(), line, engineName);

        while ((line = br.ReadLine()) != null)
        {
          if (0 == line.Trim().Length)
          {
            break;
          }

          Progress.SetPosition(br.BaseStream.Position);

          result.Add(ProteinFormat.ParseString(line));
        }
      }

      return result;
    }

    #endregion
  }
}