using System.Collections.Generic;
using System.IO;
using System.Linq;
using RCPA.Utils;
using RCPA.Proteomics.Quantification;

namespace RCPA.Tools.Quantification
{
  public class Agilent2Ms1DirectoryBuilder : AbstractMs1DirectoryBuilder
  {
    public Agilent2Ms1DirectoryBuilder(string targetDir, string program, string version)
      : base(targetDir, program, version)
    { }

    protected override List<string> GetFiles(string rawDirectory)
    {
      return (from d in Directory.GetDirectories(rawDirectory)
              where d.EndsWith(".d")
              select d).ToList();
    }

    protected override AbstractMs1FileBuilder GetBuilder(string targetDir, string program, string version)
    {
      return new Agilent2Ms1FileBuilder(targetDir, program, version);
    }
  }
}