using RCPA.Seq;
using System;
using System.Collections.Generic;
using System.IO;

namespace RCPA.Proteomics.Database
{
  public class ReversedDatabaseBuilder : AbstractThreadProcessor
  {
    private ReversedDatabaseBuilderOptions options;

    public ReversedDatabaseBuilder(ReversedDatabaseBuilderOptions options)
    {
      this.options = options;
    }

    public override IEnumerable<string> Process()
    {
      using (StreamWriter sw = new StreamWriter(options.OutputFile))
      {
        int index = 0;

        if (File.Exists(options.ContaminantFile))
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

          if (!options.ReversedOnly)
          {
            ff.WriteSequence(sw, seq);
          }

          if (options.IsPseudoAminoacid)
          {
            options.PseudoAminoacidBuilder.Build(seq);
          }

          index++;
          Sequence reversedSeq = GetReversedSequence(index, seq);

          ff.WriteSequence(sw, reversedSeq);
        }
      }
    }

    public Sequence GetReversedSequence(int index, Sequence seq)
    {
      if (options.DecoyType == DecoyType.Index)
      {
        return SequenceUtils.GetReversedSequence(seq.SeqString, index);
      }
      else
      {
        var description = options.DecoyKey + " " + seq.Description;
        var sequence = SequenceUtils.GetReversedSequence(seq.SeqString);
        string prefix = string.Empty, oldname;
        if (options.DecoyType == DecoyType.Middle)
        {
          oldname = seq.Name.StringAfter("|");
          if (oldname.Equals(seq.Name))
          {
            oldname = seq.Name.StringAfter(":");
            if (!oldname.Equals(seq.Name))
            {
              prefix = seq.Name.StringBefore(":") + ":";
            }
          }
          else
          {
            prefix = seq.Name.StringBefore("|") + "|";
          }
        }
        else
        {
          oldname = seq.Name;
        }
        var newname = prefix + options.DecoyKey + "_" + oldname;
        return new Sequence(newname + " " + description, sequence);
      }
    }
  }
}
