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

namespace RCPA.Proteomics.Quantification.Srm
{
  public partial class MrmSelectFileForm : ComponentUI
  {
    public MrmSelectFileForm()
    {
      InitializeComponent();

      this.multipleFiles.FileArgument = new OpenFileArgument("MRM Distiller", ".mrm");
      this.AddComponent(new RcpaMultipleFileComponent(this.multipleFiles.GetItemInfos(), "MRMFiles", "MRM Files", true, true));

      LoadOption();
    }

    public string[] MrmFiles
    {
      get
      {
        return this.multipleFiles.FileNames;
      }
    }

    public string[] SelectedMrmFiles
    {
      get
      {
        return this.multipleFiles.SelectedFileNames;
      }
    }

    private void btnOk_Click(object sender, EventArgs e)
    {
      try
      {
        ValidateComponents();

        SaveOption();

        DialogResult = System.Windows.Forms.DialogResult.OK;

        Close();
      }
      catch (Exception ex)
      {
        MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }
  }
}
