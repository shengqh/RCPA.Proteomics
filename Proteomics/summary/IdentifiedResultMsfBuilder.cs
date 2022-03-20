using RCPA.Proteomics.ProteomeDiscoverer;
using RCPA.Seq;
using System;
using System.Linq;

namespace RCPA.Proteomics.Summary
{
  public class IdentifiedResultMsfBuilder : AbstractIdentifiedResultBuilder
  {
    private string[] msfFiles;
    private IStringParser<string> acParser;

    public IdentifiedResultMsfBuilder(string[] msfFiles, IStringParser<string> acParser)
    {
      this.msfFiles = msfFiles;
      this.acParser = acParser;
    }

    #region IIdentifiedResultBuilder Members

    protected override bool FillSequence(IIdentifiedResult result)
    {
      foreach (var msfFile in msfFiles)
      {
        var proteins = new MsfDatabaseToNoredundantProcessor().ParseProteinSequences(msfFile).ToDictionary(m => acParser.GetValue(m.Name));
        foreach (var g in result)
        {
          foreach (var p in g)
          {
            var name = acParser.GetValue(p.Name);
            Sequence seq;
            if (proteins.TryGetValue(name, out seq))
            {
              p.Sequence = seq.SeqString;
              p.Reference = seq.Reference;
              p.Name = name;
            }
          }
        }
      }

      foreach (var g in result)
      {
        foreach (IIdentifiedProtein protein in g)
        {
          if (protein.Name.StartsWith(MsfDatabaseParser.DECOY_PREFIX))
          {
            continue;
          }

          if (protein.Sequence == null)
          {
            throw new Exception(
              MyConvert.Format(
                "Couldn't find sequence of protein {0}, change access number pattern or select another database.",
                protein.Name));
          }
        }
      }

      return true;
    }

    #endregion
  }
}
