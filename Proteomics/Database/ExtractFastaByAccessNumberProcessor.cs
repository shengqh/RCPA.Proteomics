using RCPA.Seq;
using System.Collections.Generic;
using System.IO;

namespace RCPA.Proteomics.Database
{
  public class ExtractFastaByAccessNumberProcessor : AbstractThreadFileProcessor
  {
    private bool replaceName;
    private IAccessNumberParser parser;
    private string database;

    public ExtractFastaByAccessNumberProcessor(IAccessNumberParser parser, string database, bool replaceName)
    {
      this.parser = parser;
      this.database = database;
      this.replaceName = replaceName;
    }

    public override IEnumerable<string> Process(string fileName)
    {
      var result = new List<string>();

      string[] acLines = File.ReadAllLines(fileName);

      var acs = new HashSet<string>();
      foreach (var acline in acLines)
      {
        string ac;
        if (!parser.TryParse(acline, out ac))
        {
          ac = acline;
        }

        acs.Add(ac);
      }

      var findAcs = new HashSet<string>();

      var resultFile = fileName + ".fasta";
      result.Add(resultFile);

      var ff = new FastaFormat();
      using (StreamWriter sw = new StreamWriter(resultFile))
      using (StreamReader sr = new StreamReader(database))
      {
        Progress.SetRange(0, sr.BaseStream.Length);

        Sequence seq;
        while ((seq = ff.ReadSequence(sr)) != null)
        {
          Progress.SetPosition(sr.BaseStream.Position);

          string curAc;
          if (!parser.TryParse(seq.Name, out curAc))
          {
            curAc = seq.Name;
          }

          if (acs.Contains(curAc))
          {
            findAcs.Add(curAc);
            if (this.replaceName)
            {
              seq.Reference = curAc;
            }
            ff.WriteSequence(sw, seq);
          }
        }
      }

      acs.ExceptWith(findAcs);

      var missFile = fileName + ".miss";
      if (acs.Count > 0)
      {
        using (StreamWriter sw = new StreamWriter(missFile))
        {
          foreach (var ac in acs)
          {
            sw.WriteLine(ac);
          }
        }
        result.Add(missFile);
      }
      else if (File.Exists(missFile))
      {
        File.Delete(missFile);
      }

      return result;
    }
  }
}
