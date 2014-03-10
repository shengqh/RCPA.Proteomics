using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RCPA.Proteomics.Quantification.Srm
{
  public class DisplayTypeChangedEventArgs : EventArgs
  {
    public DisplayType NewType { get; set; }
  }

  public delegate void DisplayTypeChangedEventHandler(object sender, DisplayTypeChangedEventArgs e);
}
