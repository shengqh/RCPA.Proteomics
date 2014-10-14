using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using RCPA.Gui;

namespace RCPA.Proteomics.Summary
{
  public class IdentifiedResultBuilder : AbstractIdentifiedResultBuilder
  {
    private IStringParser<string> acParser;

    private string fastaFilename;

    public IdentifiedResultBuilder(IStringParser<string> acParser, string fastaFilename)
    {
      this.acParser = acParser;
      this.fastaFilename = fastaFilename;
    }

    protected override bool FillSequence(IIdentifiedResult groups)
    {
      if (File.Exists(fastaFilename))
      {
        IdentifiedResultUtils.FillSequenceFromFasta(acParser, fastaFilename, groups, Progress);
        return true;
      }

      return false;
    }
  }
}
