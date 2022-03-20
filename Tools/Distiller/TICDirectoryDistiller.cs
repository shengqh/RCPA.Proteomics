using RCPA;
using System.IO;

namespace TICDistiller
{
  public class TICDirectoryDistiller : AbstractDirectoryProcessor
  {
    protected override IFileProcessor GetFileProcessor()
    {
      return new TICFileDistiller(true);
    }

    protected override string[] GetFiles(string directoryName)
    {
      return Directory.GetFiles(directoryName, "*.raw");
    }
  }
}
