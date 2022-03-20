using RCPA.Proteomics.Mascot;
using RCPA.Proteomics.ProteomeDiscoverer;
using RCPA.Proteomics.Summary;
using RCPA.Proteomics.Summary.Uniform;
using RCPA.Utils;
using System;
using System.IO;

namespace RCPA.Proteomics.Sequest
{
  public class SequestFactory : AbstractSequestFactory
  {
    public SequestFactory() : base(SearchEngineType.SEQUEST) { }

    public override IScoreFunction[] GetScoreFunctions()
    {
      return new IScoreFunction[] { new ScoreFunction("XCorr") };
    }

    private static readonly double _modificationDeltaScore = 0.08;
    public override ISpectrumParser GetParser(string name, bool extractRank2)
    {
      if (extractRank2)
      {
        throw new Exception("Extract rank2 PSM is not supported for Sequest");
      }

      if (Directory.Exists(name))
      {
        var dir = new DirectoryInfo(name);

        if (dir.GetFiles("*.outs").Length > 0 || dir.GetFiles("*.outs.zip").Length > 0)
        {
          return new SequestOutsParser(true, _modificationDeltaScore);
        }
        else
        {
          return new SequestOutDirectoryParser(true, _modificationDeltaScore);
        }
      }

      if (name.ToLower().EndsWith(".msf"))
      {
        return new MsfDatabaseParser(this.EngineType);
      }

      if (name.ToLower().EndsWith(".sqt"))
      {
        return new ProLuCIDSqtParser();
      }

      //zipfile
      if (ZipUtils.HasFile(name, m => m.ToLower().EndsWith(".out")))
      {
        return new SequestOutZipParser(true, _modificationDeltaScore);
      }
      else
      {
        return new SequestOutsParser(true, _modificationDeltaScore);
      }
    }

    public override IDatasetOptions GetOptions()
    {
      return new SequestDatasetOptions();
    }

    public override IIdentifiedPeptideTextFormat GetPeptideFormat(bool notExportSummary = false)
    {
      return new MascotPeptideTextFormat("\tFileScan\tSequence\tObs\tMH+\tDiff(MH+)\tDiffPPM\tCharge\tRank\tXCorr\tDeltaCn\tSpScore\tSpRank\tIons\tReference\tMissCleavage\tModification\tMatchCount\tNumProteaseTermini")
      {
        NotExportSummary = notExportSummary
      };
    }
  }
}
