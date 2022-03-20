using RCPA.Utils;
using System.IO;

namespace RCPA.Proteomics.Sequest
{
  public static class StreamUtils
  {
    public static StreamReader GetOutsStream(string fileName)
    {
      if (fileName.ToLower().EndsWith(".zip"))
      {
        return ZipUtils.OpenFile(fileName, m => m.ToLower().EndsWith(".outs"));
      }
      else
      {
        return new StreamReader(fileName);
      }
    }

    public static StreamReader GetDtasStream(string fileName)
    {
      if (fileName.ToLower().EndsWith(".zip"))
      {
        return ZipUtils.OpenFile(fileName, m => m.ToLower().EndsWith(".dtas"));
      }
      else
      {
        return new StreamReader(fileName);
      }
    }

    public static StreamReader GetParameterFileStream(string fileName)
    {
      if (fileName.ToLower().EndsWith(".zip"))
      {
        if (ZipUtils.HasFile(fileName, m => m.ToLower().EndsWith(".out")))
        {
          return ZipUtils.OpenFile(fileName, m => m.ToLower().Equals("sequest.params"));
        }
        else
        {
          return ZipUtils.OpenFile(fileName, m => m.ToLower().EndsWith(".outs"));
        }
      }
      else
      {
        return new StreamReader(fileName);
      }
    }
  }
}
