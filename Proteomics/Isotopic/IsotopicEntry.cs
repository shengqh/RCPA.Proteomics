using System.Linq;

namespace RCPA.Proteomics.Isotopic
{
  public class IsotopicEntry
  {
    public string Name { get; set; }

    public double Distance { get; set; }

    public double[] GetDistances(int[] charges)
    {
      return (from charge in charges
              select Distance / charge).ToArray();
    }

    public override string ToString()
    {
      return MyConvert.Format("{0} - {1:0.0000}", Name, Distance);
    }
  }
}
