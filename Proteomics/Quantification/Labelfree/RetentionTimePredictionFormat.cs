using RCPA.Proteomics.Summary;
using System;
using System.Collections.Generic;

namespace RCPA.Proteomics.Quantification.Labelfree
{
  internal class RetentionTimePredictionFormat : IFileReader<List<IIdentifiedSpectrum>>
  {
    public List<IIdentifiedSpectrum> ReadFromFile(string fileName)
    {
      var result = new List<IIdentifiedSpectrum>();
      var anns = new AnnotationFormat().ReadFromFile(fileName);
      foreach (var ann in anns)
      {
        var peptideId = ann.Annotations["PeptideId"] as string;
        var sequence = peptideId.StringBefore("_");
        var spec = new IdentifiedSpectrum();
        var pep = new IdentifiedPeptide(spec);
        pep.Sequence = sequence;

        spec.Query.FileScan.Experimental = ann.Annotations["Sample"] as string;
        spec.Query.FileScan.RetentionTime = double.Parse(ann.Annotations["PredictionRetentionTime"] as string);
        spec.Query.FileScan.Charge = int.Parse(ann.Annotations["Charge"] as string);
        spec.IsPrecursorMonoisotopic = true;
        spec.TheoreticalMH = PrecursorUtils.MzToMH(double.Parse(ann.Annotations["TheoreticalMz"] as string), spec.Query.FileScan.Charge, true);

        result.Add(spec);
      }

      return result;
    }
  }
}