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
  public class SequestOutZipDistiller : AbstractSequestSpectraDistiller
  {
    public SequestOutZipDistiller(ISequestOutParser parser, IFileFormat<List<IIdentifiedSpectrum>> peptideFormat)
      : base(parser, peptideFormat)
    { }

    public override List<IIdentifiedSpectrum> ParseSpectra(string zipFile, string modStr, int stepCount, int totalCount)
    {
      List<IIdentifiedSpectrum> result;

      string peptideFilename = GetPeptideFilename(zipFile, modStr);

      if (new FileInfo(peptideFilename).Exists && MatchedIntensityExist(peptideFilename))
      {
        Progress.SetMessage(MyConvert.Format("{0}/{1} : Reading peptides file {2}", stepCount, totalCount,
                                          peptideFilename));
        result = peptideFormat.ReadFromFile(peptideFilename);
      }
      else
      {
        Progress.SetMessage(MyConvert.Format("{0}/{1} : Parsing zip file {2}", stepCount, totalCount,
                                          zipFile));

        result = parser.ParsePeptides(zipFile);

        peptideFormat.WriteToFile(peptideFilename, result);

        if (StreamUtils.GetParameterFileStream(zipFile) != null)
        {
          Protease defaultProtease = new SequestParamFile().ReadFromFile(zipFile).Protease;
          foreach (IIdentifiedSpectrum spectrum in result)
          {
            spectrum.DigestProtease = defaultProtease;
            spectrum.NumMissedCleavages = defaultProtease.GetMissCleavageSiteCount(spectrum.Peptide.PureSequence);
          }
        }

        peptideFormat.WriteToFile(peptideFilename, result);
      }

      return result;
    }
  }
}
