using RCPA.Proteomics.Sequest;
using RCPA.Proteomics.Summary;
using RCPA.Seq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace RCPA.Proteomics.Comet
{
  public class CometSpectraDistiller : AbstractSequestSpectraDistiller
  {
    public CometSpectraDistiller(IFileFormat<List<IIdentifiedSpectrum>> peptideFormat) : base(null, peptideFormat) { }

    public override List<IIdentifiedSpectrum> ParseSpectra(string xmlFile, string modStr, int stepCount, int totalCount)
    {
      List<IIdentifiedSpectrum> result;

      string peptideFilename = GetPeptideFilename(xmlFile, modStr);

      if (new FileInfo(peptideFilename).Exists)
      {
        Progress.SetMessage(MyConvert.Format("{0}/{1} : Reading peptides file {2}", stepCount, totalCount,
                                          peptideFilename));
        result = peptideFormat.ReadFromFile(peptideFilename);
      }
      else
      {
        Progress.SetMessage(MyConvert.Format("{0}/{1} : Parsing xml file {2}", stepCount, totalCount,
                                          xmlFile));

        result = new CometXmlParser()
        {
          TitleParser = new DefaultTitleParser(TitleParserUtils.GetTitleParsers())
        }.ReadFromFile(xmlFile);

        peptideFormat.WriteToFile(peptideFilename, result);
      }

      return result;
    }
  }
}
