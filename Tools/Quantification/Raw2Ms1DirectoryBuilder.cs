using System.Collections.Generic;

namespace RCPA.Proteomics.Quantification
{
  public class Raw2Ms1DirectoryBuilder : AbstractMs1DirectoryBuilder
  {
    public Raw2Ms1DirectoryBuilder(string targetDir, string program, string version)
      : base(targetDir, program, version)
    { }

    protected override List<string> GetFiles(string rawDirectory)
    {
      return FileUtils.GetFiles(rawDirectory, "*.raw");
    }

    protected override AbstractMs1FileBuilder GetBuilder(string targetDir, string program, string version)
    {
      return new Raw2Ms1FileBuilder(targetDir, program, version);
    }
  }
}