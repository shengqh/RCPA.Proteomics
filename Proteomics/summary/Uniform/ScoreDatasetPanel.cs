using RCPA.Gui;
using System;
using System.Collections.Generic;

namespace RCPA.Proteomics.Summary.Uniform
{
  public partial class ScoreDatasetPanel : TitleDatasetPanel
  {
    protected RcpaListViewMultipleFileField datFiles;

    protected RcpaCheckBox filterByScore;

    protected RcpaDoubleField minScore;

    private AbstractScoreDatasetOptions ScoreOption { get { return Options as AbstractScoreDatasetOptions; } }

    public ScoreDatasetPanel()
    {
      InitializeComponent();

      this.filterByScore = new RcpaCheckBox(this.cbFilterByScore, "FilterByScore", false);
      AddComponent(this.filterByScore);

      this.minScore = new RcpaDoubleField(this.txtMinScore, "MinScore", "Min score", 1.0, false);
      AddComponent(this.minScore);
    }

    protected override void DoBeforeValidateComponent()
    {
      base.DoBeforeValidateComponent();

      this.minScore.Required = this.filterByScore.Checked;
    }

    public override void LoadFromDataset()
    {
      base.LoadFromDataset();

      this.filterByScore.Checked = ScoreOption.FilterByScore;
      if (ScoreOption.FilterByScore)
      {
        this.minScore.Value = ScoreOption.MinScore;
      }

      datFiles.ClearItems();
      datFiles.AddItems(Options.PathNames.ToArray());
    }

    public override void SaveToDataset(bool selectedOnly)
    {
      base.SaveToDataset(selectedOnly);

      ScoreOption.FilterByScore = this.filterByScore.Checked;
      if (ScoreOption.FilterByScore)
      {
        ScoreOption.MinScore = this.minScore.Value;
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
        return datFiles.SelectFileNames.Length > 0;
      }

      return datFiles.FileNames.Length > 0;
    }
  }
}
