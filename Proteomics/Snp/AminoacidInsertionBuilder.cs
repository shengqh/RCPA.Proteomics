using RCPA.Proteomics.Mascot;
using RCPA.Proteomics.Summary;
using RCPA.Seq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace RCPA.Proteomics.Snp
{
  public class AminoacidInsertionBuilderOptions
  {
    public string PeptideFile { get; set; }
    public string DatabaseFile { get; set; }
    public string OutputFile { get; set; }
    public bool GenerateReversedPeptide { get; set; }
  }

  public class AminoacidInsertionBuilder : AbstractThreadProcessor
  {
    private AminoacidInsertionBuilderOptions options;

    public AminoacidInsertionBuilder(AminoacidInsertionBuilderOptions options)
    {
      this.options = options;
    }

    public override IEnumerable<string> Process()
    {
      var format = new MascotPeptideTextFormat();
      Progress.SetMessage("reading peptide-spectra-matches from " + options.PeptideFile + " ...");
      var spectra = format.ReadFromFile(options.PeptideFile);
      var seqMap = new Dictionary<string, IIdentifiedPeptide>();
      foreach (var spec in spectra)
      {
        seqMap[spec.Peptide.PureSequence] = spec.Peptide;
      }

      var aas = (from c in new Aminoacids().GetVisibleAminoacids()
                 where c != 'I'
                 select c.ToString()).Merge("");

      var ff = new FastaFormat();
      Progress.SetMessage("inserting amino acid ...");
      using (var sw = new StreamWriter(options.OutputFile))
      {
        sw.WriteLine(File.ReadAllText(options.DatabaseFile));

        var seqs = seqMap.Keys.OrderBy(m => m).ToArray();
        var reversed_index = 1000000;
        foreach (var seq in seqs)
        {
          for (int i = 0; i < seq.Length; i++)
          {
            for (int j = 0; j < aas.Length; j++)
            {
              var newsequence = seq.Insert(i, aas[j].ToString());
              var newref = string.Format("INS_{0}_{1}{2} Insertion of {3}", seq, i, aas[j], seqMap[seq].Proteins.Merge("/"));
              var newseq = new Sequence(newref, newsequence);
              ff.WriteSequence(sw, newseq);

              if (options.GenerateReversedPeptide)
              {
                var revsequence = SequenceUtils.GetReversedSequence(newsequence);
                var revref = string.Format("REVERSED_{0}", reversed_index++);
                var revseq = new Sequence(revref, revsequence);
                ff.WriteSequence(sw, revseq);
              }
            }
          }
        }
      }

      return new[] { options.OutputFile };
    }
  }
}
