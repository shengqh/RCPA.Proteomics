using RCPA.Proteomics.Summary;
using RCPA.Proteomics.Utils;
using System.Collections.Generic;
using System.IO;

namespace RCPA.Proteomics.Sequest
{
  public class ProLuCIDSqtParser : AbstractSequestSpectrumParser
  {
    public ProLuCIDSqtParser()
    { }

    public override SearchEngineType Engine { get { return SearchEngineType.SEQUEST; } }

    public override List<IIdentifiedSpectrum> ReadFromFile(string fileName)
    {
      var result = new List<IIdentifiedSpectrum>();

      using (var sr = new StreamReader(fileName))
      {
        Progress.SetRange(1, sr.BaseStream.Length);

        IdentifiedSpectrum spec = null;
        IdentifiedPeptide pep = null;
        bool calDeltaCn = false;
        bool saveProtein = false;
        while (!sr.EndOfStream)
        {
          if (Progress.IsCancellationPending())
          {
            throw new UserTerminatedException();
          }

          string line = sr.ReadLine();
          if (line.StartsWith("H"))
          {
            continue;
          }

          if (line.StartsWith("S"))
          {
            if (Progress.IsCancellationPending())
            {
              throw new UserTerminatedException();
            }

            Progress.SetPosition(1, sr.GetCharpos());

            spec = new IdentifiedSpectrum();
            var parts = line.Split('\t');
            spec.Query.FileScan.FirstScan = int.Parse(parts[1]);
            spec.Query.FileScan.LastScan = int.Parse(parts[2]);
            spec.Query.Charge = int.Parse(parts[3]);
            spec.ExperimentalMass = double.Parse(parts[6]);
            spec.TheoreticalIonCount = (int)(double.Parse(parts[7]));
            spec.Query.MatchCount = int.Parse(parts[9]);
            result.Add(spec);
            calDeltaCn = true;
            saveProtein = false;
          }
          else if (line.StartsWith("M"))
          {
            var parts = line.Split('\t');
            var rank = int.Parse(parts[1]);
            if (rank == 1)
            {
              spec.Rank = 1;
              spec.SpRank = int.Parse(parts[2]);
              spec.TheoreticalMass = double.Parse(parts[3]);
              spec.DeltaScore = 1.0;
              spec.Score = double.Parse(parts[5]);
              spec.SpScore = double.Parse(parts[6]);
              spec.MatchedIonCount = int.Parse(parts[7]);
              spec.TheoreticalIonCount = int.Parse(parts[8]);
              pep = new IdentifiedPeptide(spec);
              pep.Sequence = parts[9];
              saveProtein = true;
            }
            else
            {
              saveProtein = false;
              if (calDeltaCn)
              {
                var pureseq = PeptideUtils.GetPureSequence(parts[9]);
                if (!pureseq.Equals(spec.Peptide.PureSequence))
                {
                  spec.DeltaScore = double.Parse(parts[4]);
                  calDeltaCn = false;
                }
              }
            }
          }
          else if (line.StartsWith("L"))
          {
            if (saveProtein)
            {
              var parts = line.Split('\t');
              pep.AddProtein(parts[1]);
            }
          }
        }

        Progress.End();
      }

      return result;
    }
  }
}