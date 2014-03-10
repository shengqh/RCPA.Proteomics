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

namespace RCPA.Proteomics.Quantification
{
  public partial class QuantificationExportForm : ComponentUI
  {
    private RcpaCheckBox proteinName;
    private RcpaTextField proteinNamePattern;
    private RcpaCheckBox singleFile;
    private RcpaCheckBox exportScan;

    public QuantificationExportForm()
    {
      InitializeComponent();

      this.proteinName = new RcpaCheckBox(cbProteinName, "ProteinName", false);
      AddComponent(this.proteinName);

      this.proteinNamePattern = new RcpaTextField(txtProteinNamePattern, "ProteinNamePattern", "Keep only one protein name based on Regex Patterns, separated by ';' : (for example: _RAT ; _MOUSE)", "_RAT ; _MOUSE", false);
      AddComponent(this.proteinNamePattern);

      this.singleFile = new RcpaCheckBox(cbSingleFile, "SingleFile", true);
      AddComponent(this.singleFile);

      this.exportScan = new RcpaCheckBox(cbExportScan, "ExportScan", false);
      AddComponent(this.exportScan);
    }

    protected override void DoBeforeValidate()
    {
      this.proteinNamePattern.Required = this.proteinName.Checked;
    }

    private void btnOk_Click(object sender, EventArgs e)
    {
      try
      {
        ValidateComponents();
      }
      catch (Exception ex)
      {
        MessageBox.Show(this, ex.Message);
        return;
      }

      DialogResult = DialogResult.OK;
    }

    public bool IsFilterProteinName
    {
      get { return this.proteinName.Checked; }
      set { this.proteinName.Checked = value; }
    }

    public string ProteinNamePattern
    {
      get { return this.proteinNamePattern.Text; }
      set { this.proteinNamePattern.Text = value; }
    }

    public bool IsSingleFile
    {
      get { return this.singleFile.Checked; }
      set { this.singleFile.Checked = value; }
    }

    public bool IsExportScan
    {
      get { return this.exportScan.Checked; }
      set { this.exportScan.Checked = value; }
    }

    public void InitializeScan(List<string> allColumns, List<string> checkedColumns)
    {
      lbScans.Initialize(allColumns, checkedColumns, null);
    }

    public List<string> GetCheckedScanHeaders()
    {
      return lbScans.GetCheckedItems();
    }
  }
}
