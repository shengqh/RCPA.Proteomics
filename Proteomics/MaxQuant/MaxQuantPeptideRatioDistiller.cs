using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using RCPA.Proteomics.Summary;
using MathNet.Numerics.Statistics;

namespace RCPA.Proteomics.MaxQuant
{
  public class MaxQuantPeptideRatioDistiller : AbstractThreadFileProcessor
  {
    private int minCount;

    public MaxQuantPeptideRatioDistiller(int minQuantifiedExperimentCount)
    {
      this.minCount = minQuantifiedExperimentCount;
    }

    public override IEnumerable<string> Process(string fileName)
    {
      MaxQuantPeptideTextFormat format = new MaxQuantPeptideTextFormat();

      var spectra = format.ReadFromFile(fileName);

      var ratioResult = fileName + ".ratio";
      var ratioNormResult = fileName + ".norm.ratio";

      ExtractRatio(spectra, ratioResult, m => m.Ratio);
      ExtractRatio(spectra, ratioNormResult, m => m.Ratio_Norm);

      return new[] { ratioResult, ratioNormResult };
    }

    private void ExtractRatio(List<IIdentifiedSpectrum> spectra, string saveFileName, Func<MaxQuantItem, string> GetRatio)
    {
      using (StreamWriter sw = new StreamWriter(saveFileName))
      {
        var itemList = spectra[0].GetMaxQuantItemList();

        sw.Write("Peptide");
        foreach (var item in itemList)
        {
          sw.Write("\t" + item.Name);
        }
        sw.WriteLine();
        //        sw.WriteLine("\tMean\tSD");

        List<double> ratios = new List<double>();
        foreach (var spectrum in spectra)
        {
          var curItem = spectrum.GetMaxQuantItemList();

          if (curItem.QuantifiedExperimentCount >= minCount)
          {
            sw.Write(spectrum.Peptide.Sequence);

            ratios.Clear();
            foreach (var item in curItem)
            {
              var ratio = GetRatio(item);

              if (ratio == string.Empty)
              {
                sw.Write("\tNA");
              }
              else
              {
                sw.Write("\t{0}", ratio);
                //var dRatio = Math.Log(MyConvert.ToDouble(ratio));
                //sw.Write("\t{0:0.0000}", dRatio);
                //ratios.Add(dRatio);
              }
            }
            sw.WriteLine();

            //Accumulator ac = new Accumulator(ratios.ToArray());

            //sw.WriteLine("\t{0:0.0000}\t{1:0.0000}", ac.Mean, ac.Sigma);

          }
        }
      }
    }
  }
}
