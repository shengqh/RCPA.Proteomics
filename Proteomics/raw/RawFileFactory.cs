using System.Collections.Generic;
using System.Linq;

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

    public static IRawFile2 GetRawFileReaderWithoutOpen(string fileName, bool isParallelMS3 = false)
    {
      return DoGetRawFileReader(fileName, isParallelMS3);
    }

    /// <summary>
    /// ParallelMS3: MS1->MS2->MS2->MS3->MS3->MS1->...
    /// Otherwise:   MS1->MS2->MS3->MS2->MS3->MS1->...
    /// </summary>
    /// <param name="fileName"></param>
    /// <param name="isParallelMS3">True:MS1->MS2->MS2->MS3->MS3->MS1</param>
    /// <returns></returns>
    public static IRawFile2 GetRawFileReader(string fileName, bool isParallelMS3 = false)
    {
      IRawFile2 result = DoGetRawFileReader(fileName, isParallelMS3);

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
