using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RCPA.Proteomics.Mascot;
using System.IO;
using RCPA.Proteomics.Quantification.ITraq;

namespace RCPA.Proteomics.Snp
{
  public class CombineQuantificationResultProcessor : AbstractThreadFileProcessor
  {
    private string quantificationFile;

    public CombineQuantificationResultProcessor(string quantificationFile)
    {
      this.quantificationFile = quantificationFile;
    }

    public override IEnumerable<string> Process(string fileName)
    {
      Progress.SetMessage("Reading mutation file ...");
      var format = new MascotPeptideTextFormat();
      var spectra = format.ReadFromFile(fileName);

      var quanFormat = new MascotResultTextFormat();
      quanFormat.Progress = this.Progress;
      Progress.SetMessage("Reading quantification file ...");
      var ir = quanFormat.ReadFromFile(quantificationFile);

      if (ir.Count == 0)
      {
        throw new Exception("No quantification found!");
      }

      foreach (var pep in spectra)
      {
        var mutSeq = pep.Peptide.PureSequence.Replace('I','L');
        var mutProtein = ir.FirstOrDefault(m => m.Any(n => n.Name.Equals(mutSeq)));

        if (mutProtein != null)
        {
          AddRatio(pep, mutProtein, "MUL_");
        }

        var oriSeq = pep.Annotations["OriginalSequence"] as string;
        var oriProtein = ir.FirstOrDefault(m => m.Any(n => n.Name.Equals(oriSeq)));

        if (oriProtein != null)
        {
          AddRatio(pep, oriProtein, "ORI_");
        }
      }

      format.Initialize(spectra);

      var result = fileName + ".quantification";
      Progress.SetMessage("Writing peptide quantification file ...");
      format.WriteToFile(result, spectra);

      return new string[] { result };
    }

    private static void AddRatio(Summary.IIdentifiedSpectrum pep, Summary.IIdentifiedProteinGroup mutProtein, string name)
    {
      var qr = mutProtein[0].FindITraqQuantificationResult();
      foreach (var q in qr.DatasetMap)
      {
        foreach (var p in q.Value.RatioMap)
        {
          pep.Annotations[name + q.Key + "_" + p.Key] = p.Value.RatioStr;
        }
      }
    }
  }
}
