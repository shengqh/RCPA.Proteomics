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
  public partial class ExpectValueDatasetPanel2 : TitleDatasetPanel
  {
    protected FilePanel datFiles;

    protected RcpaCheckBox filterByExpectValue;

    protected RcpaCheckBox filterByScore;

    protected RcpaDoubleField maxExpectValue;

    protected RcpaDoubleField minScore;

    private AbstractExpectValueDatasetOptions ExpectValueOption { get { return Options as AbstractExpectValueDatasetOptions; } }

    public ExpectValueDatasetPanel2()
    {
      InitializeComponent();

      this.filterByScore = new RcpaCheckBox(this.cbFilterByScore, "FilterByScore", true);
      AddComponent(this.filterByScore);

      this.minScore = new RcpaDoubleField(this.txtMinScore, "MinScore", "Min score",20, true);
      AddComponent(this.minScore);

      this.filterByExpectValue = new RcpaCheckBox(this.cbFilterByEvalue, "FilterByEvalue", false);
      AddComponent(this.filterByExpectValue);

      this.maxExpectValue = new RcpaDoubleField(this.txtMaxEvalue, "MaxEvalue", "Max Evalue", 0.05, false);
      AddComponent(this.maxExpectValue);

      this.datFiles = CreateFilePanel();
      AddComponent(this.datFiles);

      this.datFiles.Dock = System.Windows.Forms.DockStyle.Fill;
      this.datFiles.Location = new System.Drawing.Point(3, 16);
      this.datFiles.Name = "datFilePanel";
      this.datFiles.Size = new System.Drawing.Size(1010, 246);
      this.datFiles.TabIndex = 0;
      this.groupBox2.Controls.Add(this.datFiles);
      this.groupBox2.ResumeLayout(false);
      this.ResumeLayout(false);
    }

    protected virtual FilePanel CreateFilePanel()
    {
      var result = new FilePanel();
      return result;
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

      datFiles.LoadFromDataset(Options);
    }

    public override void SaveToDataset()
    {
      base.SaveToDataset();

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

      datFiles.SaveToDataset(Options);
    }
  }
}
