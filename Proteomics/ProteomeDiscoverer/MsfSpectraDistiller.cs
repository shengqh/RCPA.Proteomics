using RCPA.Proteomics.Sequest;
using RCPA.Proteomics.Summary;
using RCPA.Seq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace RCPA.Proteomics.ProteomeDiscoverer
{
  public class MsfSpectraDistiller : AbstractSequestSpectraDistiller
  {
    public MsfSpectraDistiller(IFileFormat<List<IIdentifiedSpectrum>> peptideFormat)
      : base(null, peptideFormat)
    { }

    public override List<IIdentifiedSpectrum> ParseSpectra(string msfFile, string modStr, int stepCount, int totalCount)
    {
      List<IIdentifiedSpectrum> result;

      string peptideFilename = GetPeptideFilename(msfFile, modStr);

      if (new FileInfo(peptideFilename).Exists)
      {
        Progress.SetMessage(MyConvert.Format("{0}/{1} : Reading peptides file {2}", stepCount, totalCount,
                                          peptideFilename));
        result = peptideFormat.ReadFromFile(peptideFilename);
      }
      else
      {
        Progress.SetMessage(MyConvert.Format("{0}/{1} : Parsing xml file {2}", stepCount, totalCount,
                                          msfFile));

        var processor = new MsfDatabaseToNoredundantProcessor() { Progress = this.Progress };
        var proteins = processor.ParseProteins(msfFile);
        result = (from p in proteins
                  from pep in p.Peptides
                  select pep.Spectrum).Distinct().ToList();
        result.Sort((m1, m2) =>
        {
          var res = m1.Query.FileScan.Experimental.CompareTo(m2.Query.FileScan.Experimental);
          if (res == 0)
          {
            res = m1.Query.FileScan.FirstScan.CompareTo(m2.Query.FileScan.FirstScan);
          }
          return res;
        });

        peptideFormat.WriteToFile(peptideFilename, result);
      }

      return result;
    }
  }
}
