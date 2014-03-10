using System;

namespace RCPA.Proteomics.Quantification.O18
{
  public class O18SampleAbundanceLabellingPostDigestionCalculator : ISampleAbundanceCalculator
  {
    private const double MIN_LABELLING_EFFICIENCY = 0.1;

    private  double purityOfO18Water;
    public O18SampleAbundanceLabellingPostDigestionCalculator( double purityOfO18Water){
      this.purityOfO18Water = purityOfO18Water;
    }


    /**
     * the whole procedure contains two steps:
     * first one is digestion at normal condition.
     * second one is labelling at 18O water,the efficiency of this step is unknown.
     * let's assume the abundance from O16 sample is x,
     * the abundance from O18 labelled sample is y,
     * the purity of O18 H2O is p,
     * the efficiency of second step is k (here k must be less than or equal to p),
     * then the final abundance of O16, Single O18, Double O18 have those formula:
     * O16 = x + (1 - k) * (1 - k) * y
     * O181 = 2 * k * (1 - k) * y
     * O182 = k * k * y
     * 
     * since it's very difficult to distinguish between extreme high O16 sample and extreme low labelling efficiency,
     * we prefer extreme high o16 sample than extreme low labelling efficiency. So when labelling efficiency is lower than 0.2, 
     * we will consider it as full labelling efficiency (equals to purity of O18 water).
     **/

    #region ISampleAbundanceCalculator Members

    public SampleAbundanceInfo Calculate(SpeciesAbundanceInfo speciesAbundance)
    {
      double o16 = speciesAbundance.O16;
      double o18_1 = speciesAbundance.O181;
      double o18_2 = speciesAbundance.O182;

      //Console.Out.WriteLine("{0:0.00}\t{1:0.00}\t{2:0.00}", o16, o18_1, o18_2);

      double k;
      if (o18_2 == 0.0)
      {
        k = purityOfO18Water;
      }
      else
      {
        k = 2/(2 + o18_1/o18_2);
      }

      //Console.Out.WriteLine("k=" + k);
      if (k > purityOfO18Water)
      {
//consider k equals to p
        k = purityOfO18Water;
      }
      else if (k <= MIN_LABELLING_EFFICIENCY)
      {
        if (o18_1 > o18_2)
        {
          Console.Out.WriteLine("k={0}; O16={1}; O181={2}; O182={3}", k, o16, o18_1, o18_2);
        }
        else
        {
          k = purityOfO18Water;
        }
      }

      double x, y;
      if (k == 1.0)
      {
        y = o18_1 + o18_2;
        x = o16;
      }
      else
      {
        double y1 = o18_1/(2*k*(1 - k));
        double y2 = o18_2/(k*k);

        y = Math.Max(y1, y2);

        x = o16 - y*(1 - k)*(1 - k);
        if (x < 0) // it cannot be fixed, just use simple formula
        {
          x = 0;
        }
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