using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RCPA.Proteomics.Snp
{
  public class SapPredictedWriter : IFileWriter<List<SapPredicted>>
  {
    public SapPredictedWriter() { }

    public void WriteToFile(string fileName, List<SapPredicted> t)
    {
      using (var sw = new StreamWriter(fileName))
      {
        sw.WriteLine(GetHeader());

        foreach (var predict in t)
        {
          sw.WriteLine(GetValue(predict));
        }
      }
    }

    protected virtual string GetValue(SapPredicted predict)
    {
      return string.Format("{0}\t{1}\t{2}\t{3}\t{4}\t{5}\t{6}\t{7}\t{8}\t{9}\t{10}\t{11}",
        predict.Ms2.FileScan.LongFileName,
        predict.Ms2.Precursor,
        predict.Ms2.Charge,
        predict.LibMs2.Precursor,
        predict.Matched.PrecursorMatched.ConvertAll(m => m.ToString()).Merge(";"),
        predict.Matched.MS3Matched.ConvertAll(m => m.ToString()).Merge(";"),
        predict.LibMs2.Peptide,
        predict.Target.Source,
        predict.Target.Target,
        predict.Target.DeltaMass,
        MutationUtils.IsDeamidatedMutation(predict.Target.Source, predict.Target.Target),
        MutationUtils.IsSingleNucleotideMutation(predict.Target.Source, predict.Target.Target));
    }

    protected virtual string GetHeader()
    {
      return "FIleScan\tPrecursor\tCharge\tLibPrecursor\tMatchedMs3Precursor\tMatchedMs3Ions\tLibSequence\tFromAA\tToAA\tDeltaMass\tIsDeamidatedMutation\tIsSingleNucleotideMutation";
    }
  }
}
