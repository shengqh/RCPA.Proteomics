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
using RCPA.Proteomics.Summary.Uniform;

namespace RCPA.Proteomics.MzIdent
{
  public partial class MzIdentDatasetPanel : TitleDatasetPanel
  {
    protected RcpaListViewMultipleFileField datFiles;

    protected RcpaCheckBox filterByScore;

    protected RcpaDoubleField minScore;

    private AbstractMzIdentDatasetOptions mzIdentOptions { get { return Options as AbstractMzIdentDatasetOptions; } }

    public MzIdentDatasetPanel()
    {
      InitializeComponent();

      this.filterByScore = new RcpaCheckBox(this.cbFilterByScore, "FilterByScore", true);
      AddComponent(this.filterByScore);

      this.minScore = new RcpaDoubleField(this.txtMinScore, "MinScore", "Min score", 0, true);
      AddComponent(this.minScore);

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
      datFiles.SelectAll();
    }

    public override void SaveToDataset(bool selectedOnly)
    {
      base.SaveToDataset(selectedOnly);

      mzIdentOptions.FilterByScore = this.filterByScore.Checked;
      if (mzIdentOptions.FilterByScore)
      {
        mzIdentOptions.MinScore = this.minScore.Value;
      }

      if (selectedOnly)
      {
        Options.PathNames = new List<string>(datFiles.GetSelectedItems());
      }
      else
      {
        Options.PathNames = new List<string>(datFiles.GetAllItems());
      }
    }

    private void lvDatFiles_SizeChanged(object sender, EventArgs e)
    {
      this.lvDatFiles.Columns[0].Width = this.lvDatFiles.ClientSize.Width - this.lvDatFiles.Columns[1].Width;
    }

    public override bool HasValidFile(bool selectedOnly)
    {
      if (selectedOnly)
      {
        return datFiles.GetSelectedItems().Length > 0;
      }

      return datFiles.GetAllItems().Length > 0;
    }
  }
}
