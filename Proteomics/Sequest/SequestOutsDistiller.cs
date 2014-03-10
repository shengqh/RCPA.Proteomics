using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RCPA.Gui;
using RCPA.Utils;
using System.IO;
using RCPA.Proteomics.Summary;

namespace RCPA.Proteomics.Sequest
{
  public class SequestOutsDistiller : AbstractSequestSpectraDistiller
  {
    public SequestOutsDistiller(ISequestOutParser parser, IFileFormat<List<IIdentifiedSpectrum>> peptideFormat)
      : base(parser, peptideFormat)
    { }

    public override List<IIdentifiedSpectrum> ParseSpectra(string dir, string modStr, int stepCount, int totalCount)
    {
      FileInfo[] outsFiles = new DirectoryInfo(dir).GetFiles("*.outs");

      if (outsFiles.Length == 0)
      {
        outsFiles = new DirectoryInfo(dir).GetFiles("*.outs.zip");
      }

      var result = new List<IIdentifiedSpectrum>();
      foreach (var outsFile in outsFiles)
      {
        List<IIdentifiedSpectrum> subPeptides;
        string peptideFilename = GetPeptideFilename(outsFile.FullName, modStr);
        if (new FileInfo(peptideFilename).Exists && MatchedIntensityExist(peptideFilename))
        {
          Progress.SetMessage(MyConvert.Format("{0}/{1} : Reading peptides file {2}", stepCount, totalCount,
                                            peptideFilename));
          subPeptides = peptideFormat.ReadFromFile(peptideFilename);
        }
        else
        {
          Progress.SetMessage(MyConvert.Format("{0}/{1} : Parsing outs file {2}", stepCount, totalCount,
                                            outsFile.FullName));
          subPeptides = parser.ParsePeptides(outsFile.FullName);

          Protease defaultProtease = new SequestParamFile().ReadFromFile(outsFile.FullName).Protease;
          foreach (IIdentifiedSpectrum spectrum in subPeptides)
          {
            spectrum.DigestProtease = defaultProtease;
            spectrum.NumMissedCleavages = defaultProtease.GetMissCleavageSiteCount(spectrum.Peptide.PureSequence);
          }

          peptideFormat.WriteToFile(peptideFilename, subPeptides);
        }
        result.AddRange(subPeptides);
      }

      return result;
    }
  }
}
