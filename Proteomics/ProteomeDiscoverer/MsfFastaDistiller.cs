using RCPA.Proteomics.Summary;
using RCPA.Seq;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;

namespace RCPA.Proteomics.ProteomeDiscoverer
{
  public class MsfFastaDistiller : AbstractThreadFileProcessor
  {
    public override IEnumerable<string> Process(string fileName)
    {
      var parser = new MsfDatabaseParser(SearchEngineType.SEQUEST);
      var seqs = parser.ParseProteinSequences(fileName);

      SQLiteDBHelper sqlite = new SQLiteDBHelper(fileName);

      var result = new List<Sequence>();
      var aaReader = sqlite.ExecuteReader("select count(*) from peptides_decoy", null);
      if (aaReader.Read())
      {
        if (aaReader.GetInt32(0) > 0) // there are decoy database
        {
          foreach (var seq in seqs)
          {
            result.Add(seq);
            var revseq = new Sequence(MsfDatabaseParser.GetReversedReference(seq.Reference), SequenceUtils.GetReversedSequence(seq.SeqString));
            result.Add(revseq);
          }
        }
      }

      if (result.Count == 0)
      {
        result = seqs;
      }

      var fastafile = fileName + ".fasta";
      using (var sw = new StreamWriter(fastafile))
      {
        var ff = new FastaFormat();
        foreach (var seq in result)
        {
          ff.WriteSequence(sw, seq);
        }
      }

      return new[] { fastafile };
    }
  }
}
