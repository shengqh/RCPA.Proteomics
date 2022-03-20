using System;
using System.IO;

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
        try
        {
          IdentifiedResultUtils.FillSequenceFromFasta(acParser, fastaFilename, groups, Progress);
        }
        catch (Exception ex)
        {
          Progress.SetMessage("ERROR: fill sequence failed, file = {0}, error = {1}, trace = {2}", fastaFilename, ex.Message, ex.StackTrace);
          return false;
        }
        return true;
      }

      return false;
    }
  }
}
