using System.Text;

namespace RCPA.Proteomics
{
  public class ChargeClass
  {
    private int[] defaultCharges;
    private string defaultChargeString;

    public ChargeClass(int[] defaultCharges)
    {
      this.defaultCharges = defaultCharges;
      if (defaultCharges.Length == 0)
      {
        this.defaultChargeString = "None";
      }
      else
      {
        StringBuilder sb = new StringBuilder();
        foreach (int charge in defaultCharges)
        {
          if (sb.Length == 0)
          {
            sb.Append(charge + "+");
          }
          else
          {
            sb.Append(" and " + charge + "+");
          }
        }
        this.defaultChargeString = sb.ToString();
      }
    }

    public int[] DefaultCharges
    {
      get { return defaultCharges; }
    }

    public override string ToString()
    {
      return defaultChargeString;
    }
  }
}
