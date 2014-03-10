using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RCPA.Proteomics.Quantification.Srm
{
  public class DisplayFileItemChangedEventArgs : EventArgs
  {
    public int Index { get; set; }
    public string FileName { get; set; }
  }

  public delegate void DisplayFileItemChangedEventHandler(object sender, DisplayFileItemChangedEventArgs e);
}
