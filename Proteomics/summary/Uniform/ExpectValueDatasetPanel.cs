using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using RCPA.Gui;
using RCPA.Gui.FileArgument;

namespace RCPA.Proteomics.Summary.Uniform
{
  public partial class ExpectValueDatasetPanel : TitleDatasetPanel
  {
    protected RcpaListViewMultipleFileField datFiles;

    protected RcpaCheckBox filterByExpectValue;

    protected RcpaCheckBox filterByScore;

    protected RcpaDoubleField maxExpectValue;

    protected RcpaDoubleField minScore;

    private AbstractExpectValueDatasetOptions ExpectValueOption { get { return Options as AbstractExpectValueDatasetOptions; } }

    public ExpectValueDatasetPanel()
    {
      InitializeComponent();

      this.filterByScore = new RcpaCheckBox(this.cbFilterByScore, "FilterByScore", true);
      AddComponent(this.filterByScore);

      this.minScore = new RcpaDoubleField(this.txtMinScore, "MinScore", "Min score", 10, true);
      AddComponent(this.minScore);

      this.filterByExpectValue = new RcpaCheckBox(this.cbFilterByEvalue, "FilterByEvalue", false);
      AddComponent(this.filterByExpectValue);

      this.maxExpectValue = new RcpaDoubleField(this.txtMaxEvalue, "MaxEvalue", "Max Evalue", 0.05, false);
      AddComponent(this.maxExpectValue);
    }

    protected override void DoBeforeValidateComponent()
    {
      base.DoBeforeValidateComponent();

      this.minScore.Required = this.filterByScore.Checked;

      this.maxExpectValue.Required = this.filterByExpectValue.Checked;
    }

    public override void LoadFromDataset()
    {
      base.LoadFromDataset();

      this.filterByScore.Checked = ExpectValueOption.FilterByScore;
      if (ExpectValueOption.FilterByScore)
      {
        this.minScore.Value = ExpectValueOption.MinScore;
      }

      this.filterByExpectValue.Checked = ExpectValueOption.FilterByExpectValue;
      if (ExpectValueOption.FilterByExpectValue)
      {
        this.maxExpectValue.Value = ExpectValueOption.MaxExpectValue;
      }

      datFiles.ClearItems();
      datFiles.AddItems(Options.PathNames.ToArray());
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
