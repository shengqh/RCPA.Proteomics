using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;
using RCPA.Utils;
using RCPA.Proteomics.Summary;
using RCPA.Proteomics.Mascot;

namespace RCPA.Proteomics.Raw
{
  public class RawFileFactory
  {
    public static readonly string SupportedRawFormatString = "Raw/mzData/mzXml";

    public static string[] GetSupportedRawFormats()
    {
      //      return new string[] { ".raw", ".mzdata", ".mzdata.xml", ".mzxml", ".wiff" };
      return new string[] { ".raw", ".mzdata", ".mzdata.xml", ".mzxml" };
    }

    public static IRawFile2 GetRawFileReader(string fileName, bool isTandemMS3 = false)
    {
      IRawFile2 result = DoGetRawFileReader(fileName, isTandemMS3);

      result.Open(fileName);

      return result;
    }

    private static IRawFile2 DoGetRawFileReader(string aFileName, bool isTandemMS3 = false)
    {
      var fileName = aFileName.ToLower();

      IRawFile2 result;

      if (fileName.EndsWith(".mzdata.xml") || fileName.EndsWith(".mzdata"))
      {
        result = new MzDataImpl();
      }
      else if (fileName.EndsWith(".mzxml"))
      {
        result = new MzXMLImpl2();
      }
      //else if (Directory.Exists(fileName))
      //{
      //  result = new AgilentDirectoryImpl();
      //}
      else if (fileName.EndsWith("mgf"))
      {
        result = new MascotGenericFormatImpl();
      }
      //else if (fileName.EndsWith("wiff"))
      //{
      //  result = new WiffImpl();
      //}
      else
      {
        result = new RawFileImpl();
      }

      if (isTandemMS3)
      {
        result.MasterScanFinder = new MasterScanParallelMS3Finder();
      }

      return result;
    }

    public static string GetExperimental(string fileName)
    {
      var reader = DoGetRawFileReader(fileName);
      return reader.GetFileNameWithoutExtension(fileName);
    }

    public static Dictionary<string, string> GetExperimentalMap(IEnumerable<string> files)
    {
      return files.ToDictionary(m => GetExperimental(m));
    }
  }
}
