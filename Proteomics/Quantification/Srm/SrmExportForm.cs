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
  public partial class SrmExportForm : AbstractUI
  {
    private RcpaFileField targetFile;

    private RcpaCheckBox removeDecoy;

    private RcpaCheckBox validOnly;

    public SrmExportForm()
    {
      InitializeComponent();

      this.targetFile = new RcpaFileField(button1, textBox1, "TargetFile", new SaveFileArgument("Quantification Result", "csv"), true);
      AddComponent(this.targetFile);

      removeDecoy = new RcpaCheckBox(cbTargetOnly, "RemoveDecoy", true);
      AddComponent(removeDecoy);

      validOnly = new RcpaCheckBox(cbValidOnly, "ValidOnly", true);
      AddComponent(validOnly);
    }

    protected override void DoRealGo()
    {
      try
      {
        ValidateComponents();
      }
      catch (Exception ex)
      {
        MessageBox.Show("Error : " + ex.Message);
        return;
      }

      DialogResult = DialogResult.OK;
    }

    public string TargetFile { get { return this.targetFile.FullName; } }

    public bool RemoveDecoy { get { return cbTargetOnly.Checked; } }

    public bool ValidOnly { get { return cbValidOnly.Checked; } }
  }
}
