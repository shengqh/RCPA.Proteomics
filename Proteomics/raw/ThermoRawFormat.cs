using System.IO;

namespace RCPA.Proteomics.Raw
{
  public class ThermoRawFormat : IRawFormat
  {
    #region IRawFormat Members

    public string Description
    {
      get { return "Thermo Fisher Raw File"; }
    }

    public string GetRawFile(string rawDir, string experimental)
    {
      return new FileInfo(rawDir + "/" + experimental + ".raw").FullName;
    }

    public string GetRawFile(string rawDir, string experimental, bool existOnly)
    {
      string result = GetRawFile(rawDir, experimental);

      if (File.Exists(result))
      {
        return result;
      }

      throw new FileNotFoundException(MyConvert.Format("Cannot find raw file {0}.", result), result);
    }

    public IRawFile2 GetRawFile()
    {
      return new RawFileImpl();
    }

    public override string ToString()
    {
      return Description;
    }

    #endregion
  }
}
