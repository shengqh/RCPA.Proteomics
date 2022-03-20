using RCPA.Proteomics.Spectrum;
using System.Collections.Generic;
using System.Drawing;

namespace RCPA.Proteomics.Isotopic
{
  public class IsotopicProfile
  {
    public IsotopicProfile()
    {
      Profile = new List<Peak>();
      DisplayName = "Profile";
      DisplayColor = Color.Black;
    }

    public List<Peak> Profile { get; set; }

    public string DisplayName { get; set; }

    public Color DisplayColor { get; set; }
  }
}
