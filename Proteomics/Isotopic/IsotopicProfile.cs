using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using RCPA.Proteomics.Spectrum;

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
