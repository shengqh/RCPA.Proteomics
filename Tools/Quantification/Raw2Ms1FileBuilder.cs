﻿using System;
using System.Collections.Generic;
using System.IO;
using RCPA.Proteomics.Raw;
using RCPA.Utils;
using RCPA.Proteomics.Spectrum;

namespace RCPA.Proteomics.Quantification
{
  public class Raw2Ms1FileBuilder : AbstractMs1FileBuilder
  {
    private string program;

    private string version;

    public Raw2Ms1FileBuilder(string targetDir, string program, string version)
      : base(new RawFileImpl(), targetDir)
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