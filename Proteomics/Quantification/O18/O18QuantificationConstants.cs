using System.Drawing;
namespace RCPA.Proteomics.Quantification.O18
{
  public sealed class O18QuantificationConstants
  {
    public static readonly string INIT_O16_INTENSITY = "INIT_O16_INTENSITY";

    public static readonly string INIT_O18_1_INTENSITY = "INIT_O18(1)_INTENSITY";

    public static readonly string INIT_O18_2_INTENSITY = "INIT_O18(2)_INTENSITY";

    public static readonly Color COLOR_O16 = Color.Red;

    public static readonly Color COLOR_O18_1 = Color.Green;

    public static readonly Color COLOR_O18_2 = Color.Blue;

    public static readonly Color COLOR_PLOY = Color.FromArgb(204, 204, 255);

    public static readonly Color COLOR_IDENTIFIED = Color.Brown;

    public static readonly string O16_INTENSITY = "O16_INTENSITY";

    public static readonly string O18_INTENSITY = "O18_INTENSITY";

    public static readonly string O18_LABELING_EFFICIENCY = "O18_LABELING_EFFICIENCY";

    public static readonly string O18_RATIO = "O18_RATIO";

    public static readonly string O18_RATIO_REGRESSION_CORRELATION = "O18_CORR";

    public static readonly string O18_RATIO_FILE = "O18_RATIO_FILE";

    public static readonly string O18_RATIO_SCANCOUNT = "O18_RATIO_COUNT";

    public static readonly string O18_EXPORT_PROTEIN_HEADER = "\tName\tDescription\tMW\tUniquePeptideCount\tLR_Ratio\tLR_RSquare\tLR_FCalc\tLR_FProbability";

    public static readonly string O18_EXPORT_PEPTIDE_HEADER = "\tReference\t\"File, Scan(s)\"\tSequence\tINIT_O16_INTENSITY\tINIT_O18(1)_INTENSITY\tINIT_O18(2)_INTENSITY\tO18_LABELING_EFFICIENCY\tO18_RATIO";
  }
}