using System.Collections.Generic;
using System.IO;
using System.Text;
using uShuffle;

namespace RCPA.Seq
{
  public class ShuffleDatabaseBuilder : AbstractThreadFileProcessor
  {
    private bool combined;

    private string contaminantFile;

    private int klet;

    private int repeat;

    FastaFormat ff = new FastaFormat();

    public ShuffleDatabaseBuilder(bool combined, int klet, int repeat)
      : this(combined, null, klet, repeat)
    { }

    public ShuffleDatabaseBuilder(bool combined, string contaminantFile, int klet, int repeat)
    {
      this.combined = combined;
      this.contaminantFile = contaminantFile;
      this.klet = klet;
      this.repeat = repeat;
    }

    public override IEnumerable<string> Process(string sourceDatabase)
    {
      StringBuilder sb = new StringBuilder();
      List<int> seqLengths = new List<int>();

      List<Sequence> source = new List<Sequence>();
      if (contaminantFile != null)
      {
        Progress.SetMessage("Reading contaminant proteins : " + contaminantFile + " ...");
        ReadFile(source, contaminantFile, sb, seqLengths);
      }

      Progress.SetMessage("Reading " + sourceDatabase + " ...");
      ReadFile(source, sourceDatabase, sb, seqLengths);

      List<string> shuffledSeqs = UShuffle.Shuffle(sb.ToString(), klet, repeat);
      List<string> result = new List<string>();
      for (int i = 0; i < repeat; i++)
      {
        string targetFilename;
        if (combined)
        {
          targetFilename = FileUtils.ChangeExtension(sourceDatabase, MyConvert.Format("SHUFFLED{0}-{1}.fasta", klet, i + 1));
        }
        else
        {
          targetFilename = FileUtils.ChangeExtension(sourceDatabase, MyConvert.Format("SHUFFLED{0}-{1}_ONLY.fasta", klet, i + 1));
        }

        var shuffledSeq = shuffledSeqs[i];
        using (StreamWriter sw = new StreamWriter(targetFilename))
        {
          source.ForEach(m => ff.WriteSequence(sw, m));

          int index = 0;
          int pos = 0;
          foreach (var length in seqLengths)
          {
            index++;
            var name = "SHF_" + index.ToString().PadLeft(8, '0');
            var seq = new Sequence(name, shuffledSeq.Substring(pos, length));
            pos += length;

            ff.WriteSequence(sw, seq);
          }
        }

        result.Add(targetFilename);
      }
      Progress.SetMessage("Finished.");

      return result;
    }

    private void ReadFile(List<Sequence> sw, string filename, StringBuilder sb, List<int> seqLengths)
    {
      using (StreamReader sr = new StreamReader(filename))
      {
        Progress.SetRange(0, sr.BaseStream.Length);

        Sequence seq;
        while ((seq = ff.ReadSequence(sr)) != null)
        {
          Progress.SetPosition(StreamUtils.GetCharpos(sr));

          if (combined)
          {
            sw.Add(seq);
          }

          sb.Append(seq.SeqString);
          seqLengths.Add(seq.SeqString.Length);
        }
      }
    }
  }
}
