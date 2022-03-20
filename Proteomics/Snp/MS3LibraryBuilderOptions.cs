using RCPA.Commandline;
using RCPA.Proteomics.Modification;
using System;
using System.Collections.Generic;
using System.IO;

namespace RCPA.Proteomics.Snp
{
  public class MS3LibraryBuilderOptions : AbstractOptions
  {
    public MS3LibraryBuilderOptions()
    {
      PrecursorPPMTolerance = 20;
      FragmentPPMTolerance = 50;
      MaxFragmentPeakCount = 10;
      MinIdentifiedSpectraPerPeptide = 1;
      MaxTerminalLossLength = 2;
      MinSequenceLength = 7;
    }

    public string PeptideFile { get; set; }

    public IList<string> RawFiles { get; set; }

    public string OutputFile { get; set; }

    public string OutputUncombinedFile
    {
      get
      {
        return Path.ChangeExtension(this.OutputFile, ".uncombined.xml");
      }
    }

    public double PrecursorPPMTolerance { get; set; }

    public double FragmentPPMTolerance { get; set; }

    public int MaxFragmentPeakCount { get; set; }

    public override bool PrepareOptions()
    {
      if (!File.Exists(PeptideFile))
      {
        ParsingErrors.Add(string.Format("Peptide file not exists : {0}", PeptideFile));
      }

      foreach (var rawFile in RawFiles)
      {
        if (!File.Exists(rawFile))
        {
          ParsingErrors.Add(string.Format("Raw file not exists : {0}", rawFile));
        }
      }

      return ParsingErrors.Count == 0;
    }

    public int MinIdentifiedSpectraPerPeptide { get; set; }

    public int MinSequenceLength { get; set; }

    public int MaxTerminalLossLength { get; set; }

    public string Modification { get; set; }

    public Aminoacids GetAminoacids()
    {
      var mod = ModificationUtils.ParseFromOutFileLine(this.Modification);
      var result = new Aminoacids();
      mod.ForEach(m => result[m.Key].ResetMass(m.Value, m.Value));
      return result;

    }
  }
}
