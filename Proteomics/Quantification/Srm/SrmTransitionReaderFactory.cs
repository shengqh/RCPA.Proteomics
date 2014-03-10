using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RCPA.Proteomics.Quantification.Srm
{
  //public static class SrmTransitionReaderFactory
  //{
  //  private static List<IFileReader<List<SrmTransition>>> readers;

  //  static SrmTransitionReaderFactory()
  //  {
  //    readers = new List<IFileReader<List<SrmTransition>>>();
  //    readers.Add(new SrmTransitionAtaqsReader());
  //    readers.Add(new SrmTransitionAgilentReader());
  //  }

  //  public static List<IFileReader<List<SrmTransition>>> GetReaders()
  //  {
  //    return readers;
  //  }

  //  public static IFileReader<List<SrmTransition>> FindReader(string readername)
  //  {
  //    foreach (var reader in readers)
  //    {
  //      if (reader.ToString().Equals(readername))
  //      {
  //        return reader;
  //      }
  //    }

  //    return null;
  //  }

  //  public static IFileReader<List<SrmTransition>> GetDefaultReader()
  //  {
  //    return readers[0];
  //  }
  //}
}
