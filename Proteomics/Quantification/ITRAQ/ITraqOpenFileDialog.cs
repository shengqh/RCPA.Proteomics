using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using RCPA.Gui;
using RCPA.Gui.FileArgument;

namespace RCPA.Proteomics.Quantification.ITraq
{
  public partial class ITraqOpenFileDialog : AbstractUI
  {
    public ITraqOpenFileDialog()
    {
      InitializeComponent();

      proteinFile.FileArgument = new OpenFileArgument("Quantified Protein", "itraq");

      itraqFile.FileArgument = new OpenFileArgument("ITraq Information", "itraq.xml");

      RegisterRcpaComponent();
    }

    protected override void DoRealGo()
    {
      this.DialogResult = DialogResult.OK;
      Close();
    }

    public string ProteinFile
    {
      get
      {
        return proteinFile.FullName;
      }
    }

    public string ITraqFile
    {
      get
      {
        return itraqFile.FullName;
      }
    }

    private void btnClose_Click(object sender, EventArgs e)
    {
      Close();
    }
  }
}
