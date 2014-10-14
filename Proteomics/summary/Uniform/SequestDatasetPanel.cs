using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using RCPA.Gui;
using System.IO;
using RCPA.Gui.FileArgument;

namespace RCPA.Proteomics.Summary.Uniform
{
  public partial class SequestDatasetPanel : DatasetPanelBase
  {
    private readonly RcpaListViewMultipleDirectoryField dataDirs;

    private readonly RcpaCheckBox filterByXcorr;

    private readonly RcpaDoubleField minXcorr1;

    private readonly RcpaDoubleField minXcorr2;

    private readonly RcpaDoubleField minXcorr3;

    private readonly RcpaCheckBox filterByDeltaCn;

    private readonly RcpaDoubleField minDeltaCn;

    private readonly RcpaCheckBox filterBySpRank;

    private readonly RcpaIntegerField maxSpRank;

    private readonly RcpaCheckBox filterByEvalue;

    private readonly RcpaDoubleField maxEvalue;

    public SequestDatasetOptions SequestOption { get { return Options as SequestDatasetOptions; } }

    private OpenFileArgument zipFiles = new OpenFileArgument("Zipped dtas/outs or dta/out file", "zip", true);

    private OpenFileArgument xmlFiles = new OpenFileArgument("Comet xml/Proteome discoverer MSF file", new []{ "xml", "msf"}, true);

    public SequestDatasetPanel()
    {
      InitializeComponent();

      this.filterByXcorr = new RcpaCheckBox(this.cbFilterByXcorr, "FilterByXcorr", false);
      AddComponent(this.filterByXcorr);

      this.minXcorr1 = new RcpaDoubleField(this.txtXcorr1, "MinXcorr1", "Min Xcorr for Charge 1", 1.9, false);
      AddComponent(this.minXcorr1);

      this.minXcorr2 = new RcpaDoubleField(this.txtXcorr2, "MinXcorr2", "Min Xcorr for Charge 2", 2.2, false);
      AddComponent(this.minXcorr2);

      this.minXcorr3 = new RcpaDoubleField(this.txtXcorr3, "MinXcorr3", "Min Xcorr for Charge 3", 3.75, false);
      AddComponent(this.minXcorr3);

      this.filterByDeltaCn = new RcpaCheckBox(this.cbFilterByDeltaCn, "FilterByDeltaCn", true);
      AddComponent(this.filterByDeltaCn);

      this.minDeltaCn = new RcpaDoubleField(this.txtMinDeltaCn, "MinDeltaCn", "Min DeltaCn", 0.1, true);
      AddComponent(this.minDeltaCn);

      this.filterBySpRank = new RcpaCheckBox(this.cbFilterBySpRank, "FilterBySpRank", false);
      AddComponent(this.filterBySpRank);

      this.maxSpRank = new RcpaIntegerField(this.txtSpRank, "MaxSpRank", "Max Sp Rank", 4, false);
      AddComponent(this.maxSpRank);

      this.filterByEvalue = new RcpaCheckBox(this.cbFilterByEvalue, "FilterByEvalue", true);
      AddComponent(this.filterByEvalue);

      this.maxEvalue = new RcpaDoubleField(this.txtMaxEvalue, "MaxEvalue", "Max Evalue", 0.05, true);
      AddComponent(this.maxEvalue);

      this.dataDirs = new RcpaListViewMultipleDirectoryField(
        this.btnAddFiles,
        this.btnRemoveFiles,
        this.btnLoad,
        this.btnSave,
        this.lvDirectories,
        "Directories",
        "SEQUEST out/outs directory",
        true,
        false);

      dataDirs.Validator.ValidateFunc = (m =>
      {
        if (m.ToLower().EndsWith(".zip") || m.ToLower().EndsWith(".xml") || m.ToLower().EndsWith(".msf"))
        {
          return File.Exists(m);
        }
        else
        {
          return Directory.Exists(m);
        }
      });

      AddComponent(this.dataDirs);
    }

    protected override void DoBeforeValidateComponent()
    {
      base.DoBeforeValidateComponent();

      this.minXcorr1.Required = this.cbFilterByXcorr.Checked;

      this.minXcorr2.Required = this.cbFilterByXcorr.Checked;

      this.minXcorr3.Required = this.cbFilterByXcorr.Checked;

      this.minDeltaCn.Required = this.cbFilterByDeltaCn.Checked;

      this.maxSpRank.Required = this.cbFilterBySpRank.Checked;

      this.maxEvalue.Required = this.cbFilterByEvalue.Checked;
    }

    public override void LoadFromDataset()
    {
      base.LoadFromDataset();

      this.filterByXcorr.Checked = this.SequestOption.FilterByXcorr;
      this.filterByDeltaCn.Checked = this.SequestOption.FilterByDeltaCn;
      this.filterBySpRank.Checked = this.SequestOption.FilterBySpRank;

      this.minXcorr1.Value = this.SequestOption.MinXcorr1;
      this.minXcorr2.Value = this.SequestOption.MinXcorr2;
      this.minXcorr3.Value = this.SequestOption.MinXcorr3;

      this.minDeltaCn.Value = this.SequestOption.MinDeltaCn;
      this.maxSpRank.Value = this.SequestOption.MaxSpRank;

      this.filterByEvalue.Checked = this.SequestOption.FilterByEvalue;
      this.maxEvalue.Value = this.SequestOption.MaxEvalue;
      
      this.dataDirs.ClearItems();
      this.dataDirs.AddDirectories(this.SequestOption.PathNames.ToArray());
    }

    public override void SaveToDataset()
    {
      base.SaveToDataset();

      this.SequestOption.FilterByXcorr = this.filterByXcorr.Checked;
      this.SequestOption.FilterByDeltaCn = this.filterByDeltaCn.Checked;
      this.SequestOption.FilterBySpRank = this.filterBySpRank.Checked;
      this.SequestOption.FilterByEvalue = this.filterByEvalue.Checked;


      this.SequestOption.MinXcorr1 = this.minXcorr1.Value;
      this.SequestOption.MinXcorr2 = this.minXcorr2.Value;
      this.SequestOption.MinXcorr3 = this.minXcorr3.Value;

      this.SequestOption.MinDeltaCn = this.minDeltaCn.Value;
      this.SequestOption.MaxSpRank = this.maxSpRank.Value;

      this.SequestOption.MaxEvalue = this.maxEvalue.Value;

      this.SequestOption.PathNames = new List<string>(dataDirs.GetAllItems());
    }

    private void btnAddSubDirectories_Click(object sender, EventArgs e)
    {
      using (var form = new InputDirectoryForm("Input folder", "Folder", ""))
      {
        if (form.ShowDialog(this) == DialogResult.OK)
        {
          string[] dirs = Directory.GetDirectories(form.Value);
          this.dataDirs.AddDirectories(dirs);
        }
      }
    }

    private void btnAddZips_Click(object sender, EventArgs e)
    {
      var dlg = zipFiles.GetFileDialog();
      if (dlg.ShowDialog() == DialogResult.OK)
      {
        this.dataDirs.AddItems(dlg.FileNames);
      }
    }

    private void btnXml_Click(object sender, EventArgs e)
    {
      var dlg = xmlFiles.GetFileDialog();
      if (dlg.ShowDialog() == DialogResult.OK)
      {
        this.dataDirs.AddItems(dlg.FileNames);
      }
    }
  }
}
