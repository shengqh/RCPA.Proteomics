using System;
using System.Collections.Generic;
using System.IO;
using RCPA.Proteomics.Raw;
using RCPA.Utils;
using RCPA.Proteomics.Spectrum;
using RCPA.Proteomics.Quantification;

namespace RCPA.Tools.Quantification
{
  public class Agilent2Ms1FileBuilder : AbstractMs1FileBuilder
  {
    private string program;

    private string version;

    public Agilent2Ms1FileBuilder(string targetDir, string program, string version)
      : base(new AgilentDirectoryImpl(), targetDir)
    {
      this.program = program;
      this.version = version;
    }

    protected override string GetProgramName()
    {
      return program;
    }

    protected override string GetProgramVersion()
    {
      return version;
    }
  }
}