using System;
using System.IO;
using System.Windows.Forms;
using RCPA.Gui;
using RCPA.Gui.Command;
using RCPA.Gui.FileArgument;
using RCPA.Proteomics.Mascot;
using RCPA.Proteomics.Summary;
using System.Collections.Generic;
using RCPA.Seq;
using RCPA.Proteomics.XTandem;
using RCPA.Proteomics.Summary.Uniform;

namespace RCPA.Tools.Summary
{
  public partial class XTandemXmlSummaryBuilderUI : SummaryBuilderUI
  {
    public static readonly string title = "BuildSummary - XTandem XML Summary Builder";

    private RcpaCheckBox filterByScore;

    private RcpaDoubleField minScore;

    private RcpaCheckBox filterByExpectValue;

    private RcpaDoubleField maxExpectValue;

    private RcpaListViewMultipleFileField xmlFiles;

    private RcpaCheckBox ignoreUnanticipatedPeptide;

    public XTandemXmlSummaryBuilderUI()
    {
      InitializeComponent();

      filterByScore = new RcpaCheckBox(cbFilterByScore, "FilterByScore", false);
      AddComponent(filterByScore);

      minScore = new RcpaDoubleField(txtMinScore, "MinScore", "Min score", 25, false);
      AddComponent(minScore);

      filterByExpectValue = new RcpaCheckBox(cbFilterByEvalue, "FilterByEvalue", false);
      AddComponent(filterByExpectValue);

      maxExpectValue = new RcpaDoubleField(txtMaxEvalue, "MaxEvalue", "Max Evalue", 0.05, false);
      AddComponent(maxExpectValue);

      ignoreUnanticipatedPeptide = new RcpaCheckBox(cbIgnoreUnanticipatedPeptide, "IgnoreUnanticipatedPeptide", true);
      AddComponent(ignoreUnanticipatedPeptide);

      xmlFiles = new RcpaListViewMultipleFileField(
        btnAddFiles,
        btnRemoveFiles,
        btnLoad,
        btnSave,
        lvDatFiles,
        "XmlFiles",
        new OpenFileArgument("XTandem XML", "xml"),
        true,
        true);
      AddComponent(xmlFiles);

      this.Text = Constants.GetSQHTitle(title, UniformBuildSummaryUI.version);
    }

    protected override void DoBeforeValidate()
    {
      base.DoBeforeValidate();

      minScore.Required = cbFilterByScore.Checked;

      maxExpectValue.Required = cbFilterByEvalue.Checked;
    }

    protected override void SaveDatasetList(BuildSummaryOptions conf)
    {
      Dictionary<string, IDatasetOptions> dsmap = new Dictionary<string, IDatasetOptions>();
      foreach (ListViewItem item in this.lvDatFiles.Items)
      {
        if (item.Selected)
        {
          var key = string.Empty;
          if (item.SubItems.Count >= 3)
          {
            key = item.SubItems[2].Text;
          }

          if (!dsmap.ContainsKey(key))
          {
            var dsoptions = new XtandemDatasetOptions();
            dsoptions.Name = key;
            dsoptions.Parent = conf;
            dsoptions.TitleParserName = this.titleParsers.SelectedItem.FormatName;
            dsoptions.IgnoreUnanticipatedPeptide = ignoreUnanticipatedPeptide.Checked;

            dsmap[key] = dsoptions;
            conf.DatasetList.Add(dsoptions);
          }
          dsmap[key].PathNames.Add(item.SubItems[0].Text);
        }
      }

      foreach (XtandemDatasetOptions dataset in conf.DatasetList)
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
        var conf = options.DatasetList[0] as XtandemDatasetOptions;

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

        ignoreUnanticipatedPeptide.Checked = conf.IgnoreUnanticipatedPeptide;
      }

      this.lvDatFiles.Items.Clear();
      foreach (var dataset in options.DatasetList)
      {
        foreach (var file in dataset.PathNames)
        {
          ListViewItem item = this.lvDatFiles.Items.Add(file);
          item.SubItems.Add("");
          item.SubItems.Add(dataset.Name);
          item.Selected = true;
        }
      }
    }

    private void lvDatFiles_SizeChanged(object sender, EventArgs e)
    {
      lvDatFiles.Columns[0].Width = lvDatFiles.ClientSize.Width - lvDatFiles.Columns[1].Width - lvDatFiles.Columns[2].Width;
    }

    private void btnMgfFiles_Click(object sender, EventArgs e)
    {
      SummaryUtils.FindXtandemSourceXml(lvDatFiles);
    }

    private void btnClassification_Click(object sender, EventArgs e)
    {
      if (lvDatFiles.SelectedIndices.Count == 0)
      {
        MessageBox.Show(this, "Select dat files first!");
      }

      string oldClassification = lvDatFiles.SelectedItems[0].SubItems.Count >= 3 ? lvDatFiles.SelectedItems[0].SubItems[2].Text : "";
      InputTextForm form = new InputTextForm(null, "Classification", "Input classification", "Classification", oldClassification, false);
      if (form.ShowDialog() == DialogResult.OK)
      {
        foreach (ListViewItem item in lvDatFiles.SelectedItems)
        {
          while (item.SubItems.Count < 3)
          {
            item.SubItems.Add("");
          }
          item.SubItems[2].Text = form.Value.Trim();
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
        new XTandemXmlSummaryBuilderUI().MyShow();
      }

      #endregion
    }

    protected override string GetTitleSample(string fileName)
    {
      return XTandemSpectrumXmlParser.GetTitleSample(fileName);
    }
  }
}

