namespace RCPA.Proteomics.Summary
{
  public interface IFalseDiscoveryRateCalculator
  {
    double Calculate(int decoyCount, int targetCount);

    double Calculate(OptimalResult or);
  }

  public abstract class AbstractFalseDiscoveryRateCalculator : IFalseDiscoveryRateCalculator
  {
    public abstract double Calculate(int decoyCount, int targetCount);

    public double Calculate(OptimalResult or)
    {
      return Calculate(or.PeptideCountFromDecoyDB, or.PeptideCountFromTargetDB);
    }
  }

  public class TargetFalseDiscoveryRateCalculator : AbstractFalseDiscoveryRateCalculator
  {
    public override double Calculate(int decoyCount, int targetCount)
    {
      if (0 == targetCount)
      {
        if (0 == decoyCount)
        {
          return 0.0;
        }
        else
        {
          return 1.0;
        }
      }

      return (double)decoyCount / targetCount;
    }

    public override string ToString()
    {
      return "Target";
    }
  }

  public class TotalFalseDiscoveryRateCalculator : AbstractFalseDiscoveryRateCalculator
  {
    public override double Calculate(int decoyCount, int targetCount)
    {
      if (0 == targetCount)
      {
        if (0 == decoyCount)
        {
          return 0.0;
        }
        else
        {
          return 1.0;
        }
      }

      if (decoyCount > targetCount)
      {
        return 1.0;
      }

      return (double)(decoyCount * 2) / (decoyCount + targetCount);
    }

    public override string ToString()
    {
      return "Global";
    }
  }
}
