using RCPA.Gui;
using RCPA.Proteomics.PropertyConverter;
using RCPA.Proteomics.Summary;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace RCPA.Proteomics.Sequest
{
  public class IdentifiedSpectrumBaseTextFormat : ProgressClass, IFileFormat<List<IIdentifiedSpectrumBase>>
  {
    public IdentifiedSpectrumBaseTextFormat()
    { }

    #region IFileFormat<List<IIdentifiedSpectrumBase>> Members

    public List<IIdentifiedSpectrumBase> ReadFromFile(string filename)
    {
      if (!File.Exists(filename))
      {
        throw new FileNotFoundException("File not exist : " + filename);
      }

      var result = new List<IIdentifiedSpectrumBase>();

      long fileSize = new FileInfo(filename).Length;

      Progress.SetRange(0, fileSize);

      char[] cs = new char[] { '\t' };
      using (var br = new StreamReader(filename))
      {
        String line = br.ReadLine();

        List<string> headers = line.Split(cs).ToList();

        int seqIndex = headers.IndexOf("Sequence");
        int refIndex = headers.IndexOf("Reference");

        var seqConverter = new IdentifiedSpectrumSequenceConverter<IIdentifiedSpectrumBase>();
        var refConverter = new IdentifiedSpectrumSequenceConverter<IIdentifiedSpectrumBase>();

        while ((line = br.ReadLine()) != null)
        {
          if (0 == line.Trim().Length)
          {
            break;
          }

          string[] parts = line.Split(cs);
          var item = new IdentifiedSpectrumBase();
          seqConverter.SetProperty(item, parts[seqIndex]);
          refConverter.SetProperty(item, parts[refIndex]);
          item.SummaryLine = line;

          result.Add(item);
        }
      }

      return result;
    }

    public void WriteToFile(string filename, List<IIdentifiedSpectrumBase> spectra)
    {
      throw new InvalidOperationException("Unsupported");
    }

    #endregion
  }
}