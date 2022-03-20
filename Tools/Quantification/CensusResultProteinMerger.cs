using RCPA.Proteomics.Quantification.Census;
using System.Collections.Generic;

namespace RCPA.Tools.Quantification
{
  public class CensusResultProteinMerger : AbstractThreadFileProcessor
  {
    private string[] sourceFilenames;
    private bool isLabelFree;

    public CensusResultProteinMerger(string[] sourceFilenames, bool isLabelFree)
    {
      this.sourceFilenames = sourceFilenames;
      this.isLabelFree = isLabelFree;
    }

    public override IEnumerable<string> Process(string targetFilename)
    {
      Dictionary<string, CensusProteinItem> proteinMap = new Dictionary<string, CensusProteinItem>();

      CensusResultFormat crf = new CensusResultFormat(true, isLabelFree);
      Progress.SetRange(1, sourceFilenames.Length + 1);
      for (int i = 0; i < sourceFilenames.Length; i++)
      {
        string f = sourceFilenames[i];

        Progress.SetMessage("Reading from " + f + "...");
        Progress.SetPosition(i + 1);

        CensusResult cr = crf.ReadFromFile(f);
        foreach (CensusProteinItem cpi in cr.Proteins)
        {
          if (!proteinMap.ContainsKey(cpi.Locus))
          {
            proteinMap[cpi.Locus] = cpi;
          }
          else
          {
            proteinMap[cpi.Locus].Peptides.AddRange(cpi.Peptides);
          }
        }
      }

      List<CensusProteinItem> proteins = new List<CensusProteinItem>(proteinMap.Values);
      foreach (CensusProteinItem cpi in proteins)
      {
        cpi.Recalculate();
        cpi.Peptides.Sort(delegate (CensusPeptideItem a, CensusPeptideItem b)
        {
          int result = a.Sequence.CompareTo(b.Sequence);

          if (0 == result)
          {
            result = a.Filename.Charge.CompareTo(b.Filename.Charge);
          }

          if (0 == result)
          {
            result = a.Filename.LongFileName.CompareTo(b.Filename.LongFileName);
          }

          return result;
        });
      }

      proteins.Sort(delegate (CensusProteinItem a, CensusProteinItem b)
      {
        int result = b.PeptideNumber - a.PeptideNumber;

        if (0 == result)
        {
          result = b.SpectraCount - a.SpectraCount;
        }

        if (0 == result)
        {
          result = a.Locus.CompareTo(b.Locus);
        }

        return result;
      });

      CensusResult target = new CensusResult();
      target.Headers = CensusUtils.ReadHeaders(sourceFilenames[0]);
      target.Proteins = proteins;

      Progress.SetMessage("Writing to " + targetFilename + "...");
      crf.WriteToFile(targetFilename, target);
      Progress.SetMessage("Write to " + targetFilename + " finished.");
      Progress.SetPosition(sourceFilenames.Length + 1);

      return new[] { targetFilename };
    }
  }
}
