using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RCPA.Utils;
using System.IO;
using RCPA.Format;

namespace RCPA.Proteomics.Quantification.Srm
{
  public static class SrmFormatFactory
  {
    private static List<IFileReader2<List<SrmTransition>>> readers;

    public static void Refresh()
    {
      var dir = FileUtils.GetTemplateDir();
      var files = Directory.GetFiles(dir.FullName, "*.srmformat");

      readers = new List<IFileReader2<List<SrmTransition>>>();
      files.ToList().ForEach(m => readers.Add(new TextFileReader<SrmTransition>(m)));
    }

    public static List<IFileReader2<List<SrmTransition>>> GetReaders()
    {
      if (readers == null)
      {
        Refresh();
      }
      return readers;
    }

    public static IFileReader2<List<SrmTransition>> FindReader(string readername)
    {
      foreach (var reader in readers)
      {
        if (reader.GetName().Equals(readername))
        {
          return reader;
        }
      }

      return null;
    }

    public static IFileReader2<List<SrmTransition>> GetDefaultReader()
    {
      return readers.FirstOrDefault();
    }
  }
}
