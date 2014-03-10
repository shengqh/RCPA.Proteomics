using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RCPA.Proteomics.Mascot;
using RCPA.Proteomics.Summary;
using System.Text.RegularExpressions;
using System.IO;
using RCPA.Proteomics.Utils;
using RCPA.Proteomics.MaxQuant;

namespace RCPA.Proteomics.Quantification.SILAC
{
  public class MaxQuantComparisionProcessor:AbstractThreadFileProcessor
  {
    string silacFile = @"X:\nzb\pCDIT\For_Mascot\sqh\all5.noredundant.phosphopair.SILACsummary";

    public override IEnumerable<string> Process(string fileName)
    {
      var peps = new MascotPeptideTextFormat().ReadFromFile(fileName);
      peps.RemoveAll(m => !(m.Annotations["Number of Phospho (STY)"] as string).Equals("1"));

      var silac = new MascotResultTextFormat().ReadFromFile(silacFile);
      var silacPeps = silac.GetSpectra();
      silacPeps.RemoveAll(m => m.GetQuantificationItem() == null ||!m.GetQuantificationItem().HasRatio );

      Regex reg = new Regex(@"Cx_(.+)");
      var silacMap = silacPeps.ToGroupDictionary(m => m.Peptide.PureSequence + GetModificationCount(m.Peptide, "STY") );

      int found = 0;
      int missed = 0;

      var matchFile = fileName + ".match";
      using (StreamWriter sw = new StreamWriter(matchFile))
      {
        sw.Write("Sequence");
        var mq = peps[0].GetMaxQuantItemList();
        foreach (var mqi in mq)
        {
          sw.Write("\tm_" + mqi.Name);
          sw.Write("\ts_" + mqi.Name);
        }
        sw.WriteLine();

        foreach (var p in peps)
        {
          var pureSeqKey = p.Peptide.PureSequence + p.Annotations["Number of Phospho (STY)"].ToString();

          if (silacMap.ContainsKey(pureSeqKey))
          {
            found++;
            Console.WriteLine("Find - " + pureSeqKey);

            var findPep = silacMap[pureSeqKey];
            var findPepMap = findPep.ToGroupDictionary(m => reg.Match(m.Query.FileScan.Experimental).Groups[1].Value);

            mq = p.GetMaxQuantItemList();
            sw.Write(p.Peptide.PureSequence);
            foreach (var mqi in mq)
            {
              if (string.IsNullOrEmpty(mqi.Ratio))
              {
                sw.Write("\t");
              }
              else
              {
                sw.Write("\t{0:0.00}", Math.Log(MyConvert.ToDouble(mqi.Ratio)));
              }

              if (!findPepMap.ContainsKey(mqi.Name))
              {
                sw.Write("\t");
              }
              else
              {
                var spectra = findPepMap[mqi.Name];
                spectra.Sort((m1,m2) => m2.GetQuantificationItem().Correlation.CompareTo(m1.GetQuantificationItem().Correlation));
                sw.Write("\t{0:0.00}", -Math.Log(spectra[0].GetQuantificationItem().Ratio));
              }
            }
            sw.WriteLine();
          }
          else
          {
            missed++;
            Console.WriteLine("Missed - " + pureSeqKey);
          }
        }
      }

      Console.WriteLine("Found = {0}; Missed = {1}", found, missed);
//      Regex reg =new Regex(@"Cx_(.+)");

      return new string[] { };
    }

    private string GetModificationCount(IIdentifiedPeptide pep, string p)
    {
      var result = 0;
      var matchedSeq = PeptideUtils.GetMatchedSequence(pep.Sequence);
      for (int i = 0; i < matchedSeq.Length - 1; i++)
      {
        if (p.Contains(matchedSeq[i]) && !Char.IsLetter(matchedSeq[i + 1]))
        {
          result++;
          i++;
        }
      }

      return result.ToString();
    }
  }
}
