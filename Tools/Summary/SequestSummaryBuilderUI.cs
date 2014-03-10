using System;
using System.IO;
using System.Windows.Forms;
using RCPA.Gui;
using RCPA.Gui.Command;
using RCPA.Gui.FileArgument;
using RCPA.Proteomics.Sequest;
using RCPA.Proteomics.Summary;
using RCPA.Seq;
using RCPA.Proteomics.Summary.Uniform;
using System.Collections.Generic;

namespace RCPA.Tools.Summary
{
  public partial class SequestSummaryBuilderUI : SummaryBuilderUI
  {
    public static readonly string title = "BuildSummary - SEQUEST Summary Builder";

    private readonly RcpaListViewMultipleDirectoryField dataDirs;

    private readonly RcpaCheckBox filterByXcorr;

    private readonly RcpaDoubleField minXcorr1;

    private readonly RcpaDoubleField minXcorr2;

    private readonly RcpaDoubleField minXcorr3;

    private readonly RcpaCheckBox filterByDeltaCn;

    private readonly RcpaDoubleField minDeltaCn;

    private readonly RcpaCheckBox filterBySpRank;

    private readonly RcpaIntegerField maxSpRank;

    private OpenFileArgument zipFiles = new OpenFileArgument("Zipped dtas/outs or dta/out file", "zip");

    public SequestSummaryBuilderUI()
    {
      InitializeComponent();

      TitleVisible = false;

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

      this.dataDirs = new RcpaListViewMultipleDirectoryField(
        this.btnAddFiles,
        this.btnRemoveFiles,
        this.btnLoad,
        this.btnSave,
        this.lvDatFiles,
        "Directories",
        "SEQUEST out/outs directory or zipped file",
        true,
        true);
      AddComponent(this.dataDirs);

      dataDirs.Validator.ValidateFunc = (m =>
      {
        if (m.ToLower().EndsWith(".zip"))
        {
          return File.Exists(m);
        }
        else
        {
          return Directory.Exists(m);
        }
      });


      Text = Constants.GetSQHTitle(title, UniformBuildSummaryUI.version);
    }

    protected override void DoBeforeValidate()
    {
      base.DoBeforeValidate();

      this.minXcorr1.Required = this.cbFilterByXcorr.Checked;

      this.minXcorr2.Required = this.cbFilterByXcorr.Checked;

      this.minXcorr3.Required = this.cbFilterByXcorr.Checked;

      this.minDeltaCn.Required = this.cbFilterByDeltaCn.Checked;

      this.maxSpRank.Required = this.cbFilterBySpRank.Checked;
    }

    protected override void SaveDatasetList(BuildSummaryOptions conf)
    {
      Dictionary<string, IDatasetOptions> dsmap = new Dictionary<string, IDatasetOptions>();
      foreach (ListViewItem item in this.lvDatFiles.Items)
      {
        if (item.Selected)
        {
          var key = string.Empty;
          if (item.SubItems.Count >= 2)
          {
            key = item.SubItems[1].Text;
          }

          if (!dsmap.ContainsKey(key))
          {
            var dsoptions = new SequestDatasetOptions();
            dsoptions.Name = key;
            dsoptions.Parent = conf;
            dsmap[key] = dsoptions;
            conf.DatasetList.Add(dsoptions);
          }
          dsmap[key].PathNames.Add(item.SubItems[0].Text);
        }
      }

      foreach (SequestDatasetOptions dataset in conf.DatasetList)
      {
        dataset.FilterByXcorr = this.filterByXcorr.Checked;
        if (dataset.FilterByXcorr)
        {
          dataset.MinXcorr1 = this.minXcorr1.Value;
          dataset.MinXcorr2 = this.minXcorr2.Value;
          dataset.MinXcorr3 = this.minXcorr3.Value;
        }

        dataset.FilterByDeltaCn = this.filterByDeltaCn.Checked;
        if (dataset.FilterByDeltaCn)
        {
          dataset.MinDeltaCn = this.minDeltaCn.Value;
        }

        dataset.FilterBySpRank = this.filterBySpRank.Checked;
        if (dataset.FilterBySpRank)
        {
          dataset.MaxSpRank = this.maxSpRank.Value;
        }
      }
    }

    protected override void LoadDatasetList(BuildSummaryOptions options)
    {
      if (options.DatasetList.Count > 0)
      {
        var conf = options.DatasetList[0] as SequestDatasetOptions;

        this.filterByXcorr.Checked = conf.FilterByXcorr;
        if (conf.FilterByXcorr)
        {
          this.minXcorr1.Value = conf.MinXcorr1;
          this.minXcorr2.Value = conf.MinXcorr2;
          this.minXcorr3.Value = conf.MinXcorr3;
        }

        this.filterByDeltaCn.Checked = conf.FilterByDeltaCn;
        if (conf.FilterByDeltaCn)
        {
          this.minDeltaCn.Value = conf.MinDeltaCn;
        }

        this.filterBySpRank.Checked = conf.FilterBySpRank;
        if (conf.FilterBySpRank)
        {
          this.maxSpRank.Value = conf.MaxSpRank;
        }
      }

      this.lvDatFiles.Items.Clear();
      foreach (var dataset in options.DatasetList)
      {
        foreach (var file in dataset.PathNames)
        {
          ListViewItem item = this.lvDatFiles.Items.Add(file);
          item.SubItems.Add(dataset.Name);
          item.Selected = true;
        }
      }
    }

    private void lvDatFiles_SizeChanged(object sender, EventArgs e)
    {
      this.lvDatFiles.Columns[0].Width = this.lvDatFiles.ClientSize.Width - this.lvDatFiles.Columns[1].Width;
    }

    private void btnClassification_Click(object sender, EventArgs e)
    {
      if (this.lvDatFiles.SelectedIndices.Count == 0)
      {
        MessageBox.Show(this, "Select directories first!");
      }

      string oldClassification = this.lvDatFiles.SelectedItems[0].SubItems.Count >= 2
                                   ? this.lvDatFiles.SelectedItems[0].SubItems[1].Text
                                   : "";
      var form = new InputTextForm(null, "Classification", "Input classification", "Classification", oldClassification,
                                   false);
      if (form.ShowDialog() == DialogResult.OK)
      {
        foreach (ListViewItem item in this.lvDatFiles.SelectedItems)
        {
          while (item.SubItems.Count < 2)
          {
            item.SubItems.Add("");
          }
          item.SubItems[1].Text = form.Value.Trim();
        }
      }
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

    public class Command : IToolCommand
    {
      #region IToolCommand Members

      public string GetClassification()
      {
        return MenuCommandType.Summary;
      }

      public string GetCaption()
      {
        return title;
      }

      public string GetVersion()
      {
        return UniformBuildSummaryUI.version;
      }

      public void Run()
      {
        new SequestSummaryBuilderUI().MyShow();
      }

      #endregion
    }

    private void btnAddZips_Click(object sender, EventArgs e)
    {
      var dlg = zipFiles.GetFileDialog();
      if (dlg.ShowDialog() == DialogResult.OK)
      {
        this.dataDirs.AddItems(dlg.FileNames);
      }
    }
  }
}