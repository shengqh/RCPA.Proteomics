using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using RCPA.Gui;
using RCPA.Gui.Command;
using RCPA.Gui.FileArgument;
using RCPA.Proteomics.Mascot;
using RCPA.Proteomics.Summary;
using RCPA.Seq;
using RCPA.Proteomics.PFind;
using RCPA.Proteomics.Summary.Uniform;

namespace RCPA.Tools.Summary
{
  public partial class pFindSummaryBuilderUI : SummaryBuilderUI
  {
    public static readonly string title = "BuildSummary - pFind Summary Builder";

    private readonly RcpaListViewMultipleFileField datFiles;

    private readonly RcpaCheckBox filterByExpectValue;

    private readonly RcpaCheckBox filterByScore;

    private readonly RcpaDoubleField maxExpectValue;

    private readonly RcpaDoubleField minScore;

    public pFindSummaryBuilderUI()
    {
      InitializeComponent();

      this.filterByScore = new RcpaCheckBox(this.cbFilterByScore, "FilterByScore", false);
      AddComponent(this.filterByScore);

      this.minScore = new RcpaDoubleField(this.txtMinScore, "MinScore", "Min score", 10, false);
      AddComponent(this.minScore);

      this.filterByExpectValue = new RcpaCheckBox(this.cbFilterByEvalue, "FilterByEvalue", true);
      AddComponent(this.filterByExpectValue);

      this.maxExpectValue = new RcpaDoubleField(this.txtMaxEvalue, "MaxEvalue", "Max Evalue", 0.05, true);
      AddComponent(this.maxExpectValue);

      this.datFiles = new RcpaListViewMultipleFileField(
        this.btnAddFiles,
        this.btnRemoveFiles,
        this.btnLoad,
        this.btnSave,
        this.lvDatFiles,
        "pFindFiles",
        new OpenFileArgument("pFind Protein", "proteins.txt"),
        true,
        true);
      AddComponent(this.datFiles);

      Text = Constants.GetSQHTitle(title, UniformBuildSummaryUI.version);
    }

    protected override void DoBeforeValidate()
    {
      base.DoBeforeValidate();

      this.minScore.Required = this.filterByScore.Checked;

      this.maxExpectValue.Required = this.filterByExpectValue.Checked;
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
            var dsoptions = new PFindDatasetOptions();
            dsoptions.Name = key;
            dsoptions.Parent = conf;
            dsoptions.TitleParserName = this.titleParsers.SelectedItem.FormatName;

            dsmap[key] = dsoptions;
            conf.DatasetList.Add(dsoptions);
          }
          dsmap[key].PathNames.Add(item.SubItems[0].Text);
        }
      }

      foreach (MascotDatasetOptions dataset in conf.DatasetList)
      {
        dataset.FilterByScore = this.filterByScore.Checked;
        if (dataset.FilterByScore)
        {
          dataset.MinScore = this.minScore.Value;
        }

        dataset.FilterByExpectValue = this.filterByExpectValue.Checked;
        if (dataset.FilterByExpectValue)
        {
          dataset.MaxExpectValue = this.maxExpectValue.Value;
        }
      }
    }

    protected override void LoadDatasetList(BuildSummaryOptions options)
    {
      if (options.DatasetList.Count > 0)
      {
        var conf = options.DatasetList[0] as PFindDatasetOptions;

        this.filterByScore.Checked = conf.FilterByScore;
        if (conf.FilterByScore)
        {
          this.minScore.Value = conf.MinScore;
        }

        this.filterByExpectValue.Checked = conf.FilterByExpectValue;
        if (conf.FilterByExpectValue)
        {
          this.maxExpectValue.Value = conf.MaxExpectValue;
        }

        foreach (ITitleParser obj in titleParsers.Items)
        {
          if (obj.FormatName.Equals(conf.TitleParserName))
          {
            titleParsers.SelectedItem = obj;
            break;
          }
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
        MessageBox.Show(this, "Select pFind result files first!");
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

    #region Nested type: Command

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
        new pFindSummaryBuilderUI().MyShow();
      }

      #endregion
    }

    #endregion

    protected override string GetTitleSample(string fileName)
    {
      return PFindSpectrumParser.GetTitleSample(fileName);
    }
  }
}