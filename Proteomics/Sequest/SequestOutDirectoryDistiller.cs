using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RCPA.Proteomics.Sequest;
using System.IO;
using RCPA.Gui;
using RCPA.Proteomics.Summary;

namespace RCPA.Proteomics.Sequest
{
  public class SequestOutDirectoryDistiller : AbstractSequestSpectraDistiller
  {
    public SequestOutDirectoryDistiller(ISpectrumParser parser, IFileFormat<List<IIdentifiedSpectrum>> peptideFormat)
      : base(parser, peptideFormat)
    { }

    public override List<IIdentifiedSpectrum> ParseSpectra(string dir, string modStr, int stepCount, int totalCount)
    {
      List<IIdentifiedSpectrum> result;

      string peptideFilename = GetPeptideFilename(dir, modStr);

      if (new FileInfo(peptideFilename).Exists && MatchedIntensityExist(peptideFilename))
      {
        Progress.SetMessage(MyConvert.Format("{0}/{1} : Reading peptides file {2}", stepCount, totalCount,
                                          peptideFilename));
        result = peptideFormat.ReadFromFile(peptideFilename);
      }
      else
      {
        Progress.SetMessage(MyConvert.Format("{0}/{1} : Parsing directory {2}", stepCount, totalCount,
                                          dir));

        result = parser.ReadFromFile(dir);

        string sequestParamFile = dir + "\\sequest.params";
        if (File.Exists(sequestParamFile))
        {
          Protease defaultProtease = new SequestParamFile().ReadFromFile(sequestParamFile).Protease;
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
