using RCPA.Seq;
using System.Collections.Generic;
using System.IO;

namespace RCPA.Proteomics.Database
{
  public class ReversedDatabaseIndexBuilder : AbstractThreadProcessor
  {
    private ReversedDatabaseBuilderOptions options;

    public ReversedDatabaseIndexBuilder(ReversedDatabaseBuilderOptions options)
    {
      this.options = options;
    }

    public override IEnumerable<string> Process()
    {
      using (StreamWriter sw = new StreamWriter(options.OutputFile))
      {
        int index = 0;

        if (options.ContaminantFile != null)
        {
          Progress.SetMessage("Processing contaminant proteins : " + options.ContaminantFile + " ...");
          ProcessFile(ref index, sw, options.ContaminantFile, true);
        }

        Progress.SetMessage("Generating and writing reversed version of " + options.InputFile + " ...");
        ProcessFile(ref index, sw, options.InputFile, false);
      }

      Progress.SetMessage("Finished.");

      return new[] { options.OutputFile };
    }

    private void ProcessFile(ref int index, StreamWriter sw, string fastaFile, bool isContaminant)
    {
      FastaFormat ff = new FastaFormat();

      using (StreamReader sr = new StreamReader(fastaFile))
      {
        Progress.SetRange(0, sr.BaseStream.Length);

        Sequence seq;
        while ((seq = ff.ReadSequence(sr)) != null)
        {
          Progress.SetPosition(sr.BaseStream.Position);

          if (isContaminant)
          {
            if (!seq.Reference.StartsWith("CON_"))
            {
              seq.Reference = "CON_" + seq.Reference;
            }
          }

          if (options.ReversedOnly)
          {
            ff.WriteSequence(sw, seq);
          }

          if (options.IsPseudoAminoacid)
          {
            options.PseudoAminoacidBuilder.Build(seq);
          }

          index++;
          Sequence reversedSeq = SequenceUtils.GetReversedSequence(seq.SeqString, index);

          ff.WriteSequence(sw, reversedSeq);
        }
      }
    }
  }
}
