using RCPA.Seq;
using System.Collections.Generic;
using System.IO;

namespace RCPA.Proteomics.Database
{
  /// <summary>
  /// Remove the N-terminal M from protein sequence
  /// </summary>
  public class RemoveNTerminalMBuilder : AbstractThreadFileProcessor
  {
    public override IEnumerable<string> Process(string fileName)
    {
      FastaFormat ff = new FastaFormat();
      var result = Path.ChangeExtension(fileName, ".dM.fasta");

      using (StreamReader sr = new StreamReader(fileName))
      using (StreamWriter sw = new StreamWriter(result))
      {
        Sequence seq;
        Progress.SetRange(1, sr.BaseStream.Length);
        while ((seq = ff.ReadSequence(sr)) != null)
        {
          Progress.SetPosition(StreamUtils.GetCharpos(sr));
          if (seq.SeqString.StartsWith("M"))
          {
            seq.SeqString = seq.SeqString.Substring(1);
            seq.Reference = seq.Name + " N-terminal-M-Removed " + seq.Description;
          }
          ff.WriteSequence(sw, seq);
        }
      }

      return new string[] { result };
    }
  }
}
