using RCPA.Gui;
using System;
using System.Collections.Generic;

namespace RCPA.Proteomics.Summary.Uniform
{
  public partial class ExpectValueDatasetPanel : ScoreDatasetPanel
  {
    protected RcpaCheckBox filterByExpectValue;

    protected RcpaDoubleField maxExpectValue;

    private AbstractExpectValueDatasetOptions ExpectValueOption { get { return Options as AbstractExpectValueDatasetOptions; } }

    public ExpectValueDatasetPanel()
    {
      InitializeComponent();

      this.filterByExpectValue = new RcpaCheckBox(this.cbFilterByEvalue, "FilterByEvalue", false);
      AddComponent(this.filterByExpectValue);

      this.maxExpectValue = new RcpaDoubleField(this.txtMaxEvalue, "MaxEvalue", "Max Evalue", 0.05, false);
      AddComponent(this.maxExpectValue);
    }

    protected override void DoBeforeValidateComponent()
    {
      base.DoBeforeValidateComponent();

      this.maxExpectValue.Required = this.filterByExpectValue.Checked;
    }

    public override void LoadFromDataset()
    {
      base.LoadFromDataset();

      this.filterByExpectValue.Checked = ExpectValueOption.FilterByExpectValue;
      if (ExpectValueOption.FilterByExpectValue)
      {
        this.maxExpectValue.Value = ExpectValueOption.MaxExpectValue;
      }
    }

    public override void SaveToDataset(bool selectedOnly)
    {
      base.SaveToDataset(selectedOnly);

      ExpectValueOption.FilterByScore = this.filterByScore.Checked;
      if (ExpectValueOption.FilterByScore)
      {
        ExpectValueOption.MinScore = this.minScore.Value;
      }

      ExpectValueOption.FilterByExpectValue = this.filterByExpectValue.Checked;
      if (ExpectValueOption.FilterByExpectValue)
      {
        ExpectValueOption.MaxExpectValue = this.maxExpectValue.Value;
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
