using System;

namespace RCPA.Proteomics.Quantification.O18
{
  public class O18SampleAbundanceLabellingDuringDigestionCalculator : ISampleAbundanceCalculator
  {
    private bool fixedEfficiency;
    private double purityOfO18Water;

    public O18SampleAbundanceLabellingDuringDigestionCalculator(double purityOfO18Water)
      : this(purityOfO18Water, true)
    {
    }

    public O18SampleAbundanceLabellingDuringDigestionCalculator(double purityOfO18Water, bool fixedEfficiency)
    {
      this.purityOfO18Water = purityOfO18Water;
      this.fixedEfficiency = fixedEfficiency;
    }

    /**
     * the whole labelling procedure contains two steps
     * first one is caused by enzyme and labelled the peptide with one O18.
     * the efficiency of this step is 100%.
     * second one is add one O18 on peptide
     * the efficiency of this step is unknown.
     * let's assume the abundance from O16 sample is x,
     * the abundance from O18 labelled sample is y,
     * the purity of O18 H2O is p,
     * the efficiency of second step is k (here k must be less than or equal to p)
     * only one replacement from O16 to O18 for each peptide occurs in second step,
     * then the final abundance of O16, Single O18, Double O18 have those formula:
     * O16 = x + (1 - k) * (1 - p) * y
     * O181 = (k(1-p) + p(1-k)) * y
     * O182 = p * k * y
     **/

    #region ISampleAbundanceCalculator Members

    public SampleAbundanceInfo Calculate(SpeciesAbundanceInfo speciesAbundance)
    {
      double o16 = speciesAbundance.O16;
      double o18_1 = speciesAbundance.O181;
      double o18_2 = speciesAbundance.O182;

      //Console.Out.WriteLine("{0:0.00}\t{1:0.00}\t{2:0.00}", o16, o18_1, o18_2);

      double k;
      if (this.fixedEfficiency)
      {
        k = purityOfO18Water; //consider k equals to p
      }
      else
      {
        //Consider that O16 cannot be labelled to O18 in second step to decrease the complexibility
        //I(O18_1)/I(O18_2) = ((1-p)k + (1-k)p) /pk
        //k = 1 / (2 + O181/O182 - 1/p)
        k = 1 / (2 + o18_1 / o18_2 - 1 / purityOfO18Water);

        Console.Out.WriteLine("k=" + k);

        if (k > purityOfO18Water || k <= 0)
        {
          k = purityOfO18Water; //consider k equals to p
        }
      }
      //then, calculate the intensity from O18 labelled sample, assume as y
      //I(O18_1) + I(O18_2) = (1-k)py + (1-p)ky + pky
      //y = (I(O18_1) + I(O18_2)) / ((1-k)p + (1-p)k + pk)
      double y = (o18_1 + o18_2) / ((1 - k) * purityOfO18Water + (1 - purityOfO18Water) * k + purityOfO18Water * k);

      //finally, calculate the intensity from O16 labelled sample, assume as x
      //I(O16) = x + (1-k)(1-purity) * y
      //x = I(O16) - (1-k)(1-purity) * y
      double x = o16 - (1 - k) * (1 - purityOfO18Water) * y;

      if (x < 0) // it cannot be fixed, just use simple formula
      {
        x = o16;
        y = o18_1 + o18_2;
        k = purityOfO18Water;
      }

      var result = new SampleAbundanceInfo();
      result.O16 = x;
      result.O18 = y;
      result.LabellingEfficiency = k;
      result.CalculateRatio();
      return result;
    }

    #endregion
  }
}