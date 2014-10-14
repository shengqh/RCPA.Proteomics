using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using RCPA.Gui;
using RCPA.Proteomics.MzIdent;
using RCPA.Gui.FileArgument;

namespace RCPA.Proteomics.Summary.Uniform
{
  public partial class MzIdentDatasetPanel : TitleDatasetPanel
  {
    private RcpaComboBox<SearchEngineType> engines;

    protected RcpaListViewMultipleFileField datFiles;

    protected RcpaCheckBox filterByScore;

    protected RcpaDoubleField minScore;

    private MzIdentDatasetOptions mzIdentOptions { get { return Options as MzIdentDatasetOptions; } }

    public MzIdentDatasetPanel()
    {
      InitializeComponent();

      this.filterByScore = new RcpaCheckBox(this.cbFilterByScore, "FilterByScore", true);
      AddComponent(this.filterByScore);

      this.minScore = new RcpaDoubleField(this.txtMinScore, "MinScore", "Min score", 0, true);
      AddComponent(this.minScore);

      this.engines = new RcpaComboBox<SearchEngineType>(this.cbEngines, "SearchEngine", MzIdentParserFactory.Engines, 0, true, "Search engine that generated the result");
      AddComponent(this.engines);

      this.datFiles = new RcpaListViewMultipleFileField(
        this.btnAddFiles,
        this.btnRemoveFiles,
        this.btnLoad,
        this.btnSave,
        this.lvDatFiles,
        "MzIdentFiles",
        new OpenFileArgument("MzIdent", "mzid"),
        true,
        false);
      AddComponent(this.datFiles);
    }

    protected override void DoBeforeValidateComponent()
    {
      base.DoBeforeValidateComponent();

      this.minScore.Required = this.filterByScore.Checked;
    }

    public override void LoadFromDataset()
    {
      base.LoadFromDataset();

      this.filterByScore.Checked = mzIdentOptions.FilterByScore;
      if (mzIdentOptions.FilterByScore)
      {
        this.minScore.Value = mzIdentOptions.MinScore;
      }

      datFiles.ClearItems();
      datFiles.AddItems(Options.PathNames.ToArray());
    }

    public override void SaveToDataset()
    {
      base.SaveToDataset();

      mzIdentOptions.FilterByScore = this.filterByScore.Checked;
      if (mzIdentOptions.FilterByScore)
      {
        mzIdentOptions.MinScore = this.minScore.Value;
      }

      Options.PathNames = new List<string>(datFiles.GetAllItems());
    }

    private void lvDatFiles_SizeChanged(object sender, EventArgs e)
    {
      this.lvDatFiles.Columns[0].Width = this.lvDatFiles.ClientSize.Width - this.lvDatFiles.Columns[1].Width;
    }
  }
}
