using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RCPA.Seq;
using RCPA.Proteomics.Mascot;
using System.IO;

namespace RCPA.Proteomics.Snp
{
  /// <summary>
  /// 读取SNP数据库，将我们筛选得到的one2one结果跟数据库比对，看有多少是SNP数据库里面已经得到验证的。
  /// </summary>
  public class DatabaseSAPValidator : AbstractThreadFileProcessor
  {
    private string database;

    public DatabaseSAPValidator(string database)
    {
      this.database = database;
    }

    public override IEnumerable<string> Process(string fileName)
    {
      Progress.SetMessage("Reading sequences from " + database + " ...");
      var seqs = SequenceUtils.Read(new FastaFormat(), database);
      seqs.RemoveAll(m => m.Name.StartsWith("rev_") || !m.Name.Contains("|#"));

      var format = new MascotPeptideTextFormat();

      Progress.SetMessage("Procesing peptides from " + Path.GetFileName(fileName) + " ...");
      var peptides = format.ReadFromFile(fileName);

      Progress.SetRange(0, peptides.Count);
      foreach (var peptide in peptides)
      {
        Progress.Increment(1);
        var pureSeq = peptide.Annotations["PureSequence"] as string;
        foreach (var seq in seqs)
        {
          if (seq.SeqString.Contains(pureSeq))
          {
            peptide.Annotations["MutDB"] = seq.Name;
            break;
          }
        }
      }

      var result = fileName + ".mutdb";
      using (StreamWriter sw = new StreamWriter(fileName + ".mutdb"))
      {
        sw.WriteLine(format.PeptideFormat.GetHeader() + "\tMutDB");
        foreach (var peptide in peptides)
        {
          sw.Write(format.PeptideFormat.GetString(peptide));
          if (peptide.Annotations.ContainsKey("MutDB"))
          {
            sw.WriteLine("\t" + peptide.Annotations["MutDB"]);
          }
          else
          {
            sw.WriteLine("\t");
          }
        }
      }

      return new string[] { result };
    }
  }
}
