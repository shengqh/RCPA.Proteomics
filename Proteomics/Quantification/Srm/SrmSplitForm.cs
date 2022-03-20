using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace RCPA.Proteomics.Quantification.Srm
{
  public partial class SrmSplitForm : Form
  {
    public SrmSplitForm()
    {
      InitializeComponent();
      this.left = new SrmPairedPeptideItem();
      this.right = new SrmPairedPeptideItem();
    }

    private SrmPairedPeptideItem left;
    public SrmPairedPeptideItem LeftPeptide
    {
      get
      {
        return left;
      }
      set
      {
        left = value;
        gvLeft.DataSource = left.ProductIonPairs;
      }
    }

    private SrmPairedPeptideItem right;
    public SrmPairedPeptideItem RightPeptide
    {
      get
      {
        return right;
      }
      set
      {
        right = value;
        gvRight.DataSource = right.ProductIonPairs;
      }
    }

    private void btnLeftToRight_Click(object sender, EventArgs e)
    {
      Transfer(left, right, gvLeft, gvRight);
    }

    private static void Transfer(SrmPairedPeptideItem source, SrmPairedPeptideItem target, DataGridView gvSource, DataGridView gvTarget)
    {
      if (gvSource.SelectedRows.Count == 0)
      {
        return;
      }

      var rowIndecies = new List<int>();
      for (int i = 0; i < gvSource.SelectedRows.Count; i++)
      {
        rowIndecies.Add(gvSource.SelectedRows[i].Index);
      }

      rowIndecies.Sort();

      gvSource.DataSource = null;
      gvTarget.DataSource = null;

      try
      {
        for (int i = rowIndecies.Count - 1; i >= 0; i--)
        {
          target.ProductIonPairs.Add(source.ProductIonPairs[rowIndecies[i]]);
          source.ProductIonPairs.RemoveAt(rowIndecies[i]);
        }
      }
      finally
      {
        gvSource.DataSource = source.ProductIonPairs;
        gvTarget.DataSource = target.ProductIonPairs;
      }
    }

    private void btnRightToLeft_Click(object sender, EventArgs e)
    {
      Transfer(right, left, gvRight, gvLeft);
    }

    private void splitContainer2_Panel1_Resize(object sender, EventArgs e)
    {
      btnLeftToRight.Top = splitContainer2.Panel1.Height / 2 - btnLeftToRight.Height - 10;
      btnRightToLeft.Top = splitContainer2.Panel1.Height / 2 + 10;
    }
  }
}
