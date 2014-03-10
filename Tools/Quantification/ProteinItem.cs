using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace RCPA.Tools.Quantification
{
  public class ProteinItem
  {
    public ProteinItem(string protein, ListViewItem lvItem)
    {
      this.protein = protein;
      this.lvItem = lvItem;
    }

    private string protein;

    public string Protein
    {
      get { return protein; }
    }

    private ListViewItem lvItem;

    public ListViewItem LvItem
    {
      get { return lvItem; }
    }

    public override string ToString()
    {
      return protein;
    }
  }

}
