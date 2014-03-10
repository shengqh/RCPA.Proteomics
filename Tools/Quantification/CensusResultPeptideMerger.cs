using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using RCPA.Proteomics.Quantification;
using RCPA.Proteomics.Quantification.Census;

namespace RCPA.Tools.Quantification
{
  public class CensusResultPeptideMerger : AbstractThreadFileProcessor
  {
    private string[] sourceFilenames;
    private bool isLabelFree;

    public CensusResultPeptideMerger(string[] sourceFilenames, bool isLabelFree)
    {
      this.sourceFilenames = sourceFilenames;
      this.isLabelFree = isLabelFree;
    }

    public override IEnumerable<string> Process(string targetFilename)
    {
      Dictionary<string, List<CensusPeptideItem>> peptideMap = new Dictionary<string, List<CensusPeptideItem>>();

      CensusResultFormat crf = new CensusResultFormat(true, isLabelFree);
      Progress.SetRange(1, sourceFilenames.Length + 1);
      for (int i = 0; i < sourceFilenames.Length; i++)
      {
        string f = sourceFilenames[i];

        Progress.SetMessage("Reading from " + f + "...");
        Progress.SetPosition(i + 1);

        List<CensusPeptideItem> peptides = crf.ReadPeptides(f);
        foreach (CensusPeptideItem cpi in peptides)
        {
          if (!peptideMap.ContainsKey(cpi.Sequence))
          {
            peptideMap[cpi.Sequence] = new List<CensusPeptideItem>();
          }

          peptideMap[cpi.Sequence].Add(cpi);
        }
      }

      List<string> sequences = new List<string>(peptideMap.Keys);
      sequences.Sort(delegate(string seq1, string seq2)
      {
        int result = peptideMap[seq2].Count - peptideMap[seq1].Count;
        if (0 == result)
        {
          result = seq1.CompareTo(seq2);
        }
        return result;
      });

      Progress.SetMessage("Writing to " + targetFilename + "...");
      using (var sw = new StreamWriter(targetFilename))
      {
        sw.WriteLine("PLINE\tSEQUENCE\tS_INT/R_INT\tINT(S)\tINT(R)");
        sw.WriteLine(crf.PeptideFormat.GetHeader());
        foreach (string seq in sequences)
        {
          List<CensusPeptideItem> items = peptideMap[seq];
          double sInt = 0.0;
          double rInt = 0.0;
          foreach (CensusPeptideItem item in items)
          {
            sInt += item.SampleIntensity;
            rInt += item.ReferenceIntensity;
          }

          double ratio;
          if (rInt != 0.0)
          {
            ratio = sInt / rInt;
          }
          else
          {
            ratio = 0.0;
          }
          sw.WriteLine(MyConvert.Format("P\t{0}\t{1:0.00}\t{2:0.0}\t{3:0.0}", seq, ratio, sInt, rInt));

          foreach (CensusPeptideItem item in items)
          {
            sw.WriteLine(crf.PeptideFormat.GetString(item));
          }
        }
      }
      Progress.SetMessage("Write to " + targetFilename + " finished.");
      Progress.SetPosition(sourceFilenames.Length + 1);

      return new[] { targetFilename };
    }
  }
}
