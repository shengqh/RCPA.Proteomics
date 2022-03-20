﻿using System;
using System.Collections.Generic;
using System.IO;

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
      return string.Format("{0}\t{1}\t{2}\t{3}\t{4}\t{5}\t{6}\t{7}\t{8}\t{9}\t{10}\t{11}\t{12}\t{13}\t{14}\t{15}\t{16}\t{17}",
        predict.Ms2.GetFileScans(),
        predict.Ms2.Precursor,
        predict.Ms2.Charge,
        predict.LibMs2.Precursor,
        predict.Matched.PrecursorMatched.ConvertAll(m => m.ToString()).Merge(";"),
        predict.Matched.MS3Matched.ConvertAll(m => m.ToString()).Merge(";"),
        predict.LibMs2.GetFileScans(),
        predict.LibMs2.Peptide,
        predict.LibMs2.Score,
        predict.LibMs2.ExpectValue,
        predict.LibMs2.Proteins,
        predict.Target.TargetType,
        predict.Target.Source,
        predict.Target.Target.Merge(","),
        predict.Target.DeltaMass,
        (predict.Ms2.Precursor - predict.LibMs2.Precursor) * predict.Ms2.Charge,
        predict.Target.IsDeamidatedMutation,
        predict.Target.IsSingleNucleotideMutation);
    }

    protected virtual string GetHeader()
    {
      return "FileScan\tPrecursor\tCharge\tLibPrecursor\tMatchedMs3Precursor\tMatchedMs3Ions\tLibFileScan\tLibSequence\tLibScore\tLibExpectValue\tLibProteins\tVarientType\tFrom\tTo\tExpectDeltaMass\tRealDeltaMass\tIsDeamidatedMutation\tIsSingleNucleotideMutation";
    }
  }
}
