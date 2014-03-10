using System.Drawing;
using System.IO;
namespace RCPA.Proteomics.Quantification.SILAC
{
  public static class SilacQuantificationConstants
  {
    public static string SILAC_KEY = "S_COUNT";

    public static readonly Color IDENTIFIED_COLOR = Color.Blue;
    public static readonly Color PLOY_COLOR = Color.FromArgb(204, 204, 255);
    public static readonly Color REFERENCE_COLOR = Color.Green;
    public static readonly Color SAMPLE_COLOR = Color.Red;
  }
}