using RCPA.Gui;
using RCPA.Gui.FileArgument;
using RCPA.Proteomics.Summary.Uniform;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace RCPA.Proteomics.Sequest
{
  public partial class AbstractSequestDatasetPanel : DatasetPanelBase
  {
    private readonly RcpaCheckBox filterByXcorr;

    private readonly RcpaDoubleField minXcorr1;

    private readonly RcpaDoubleField minXcorr2;

    private readonly RcpaDoubleField minXcorr3;

    private readonly RcpaCheckBox filterByDeltaCn;

    private readonly RcpaDoubleField minDeltaCn;

    private readonly RcpaCheckBox filterBySpRank;

    private readonly RcpaIntegerField maxSpRank;

    public SequestDatasetOptions SequestOption { get { return Options as SequestDatasetOptions; } }

    public AbstractSequestDatasetPanel()
    {
      InitializeComponent();

      this.filterByXcorr = new RcpaCheckBox(this.cbFilterByXcorr, "FilterByXcorr", false);
      AddComponent(this.filterByXcorr);

      this.minXcorr1 = new RcpaDoubleField(this.txtXcorr1, "MinXcorr1", "Min Xcorr for Charge 1", 1.0, false);
      AddComponent(this.minXcorr1);

      this.minXcorr2 = new RcpaDoubleField(this.txtXcorr2, "MinXcorr2", "Min Xcorr for Charge 2", 1.5, false);
      AddComponent(this.minXcorr2);

      this.minXcorr3 = new RcpaDoubleField(this.txtXcorr3, "MinXcorr3", "Min Xcorr for Charge 3", 2.0, false);
      AddComponent(this.minXcorr3);

      this.filterByDeltaCn = new RcpaCheckBox(this.cbFilterByDeltaCn, "FilterByDeltaCn", true);
      AddComponent(this.filterByDeltaCn);

      this.minDeltaCn = new RcpaDoubleField(this.txtMinDeltaCn, "MinDeltaCn", "Min DeltaCn", 0.1, true);
      AddComponent(this.minDeltaCn);

      this.filterBySpRank = new RcpaCheckBox(this.cbFilterBySpRank, "FilterBySpRank", false);
      AddComponent(this.filterBySpRank);

      this.maxSpRank = new RcpaIntegerField(this.txtSpRank, "MaxSpRank", "Max Sp Rank", 4, false);
      AddComponent(this.maxSpRank);
    }

    protected override void DoBeforeValidateComponent()
    {
      base.DoBeforeValidateComponent();

      this.minXcorr1.Required = this.cbFilterByXcorr.Checked;

      this.minXcorr2.Required = this.cbFilterByXcorr.Checked;

      this.minXcorr3.Required = this.cbFilterByXcorr.Checked;

      this.minDeltaCn.Required = this.cbFilterByDeltaCn.Checked;

      this.maxSpRank.Required = this.cbFilterBySpRank.Checked;
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
    }

    public override void SaveToDataset(bool selectedOnly)
    {
      base.SaveToDataset(selectedOnly);

      this.SequestOption.FilterByXcorr = this.filterByXcorr.Checked;
      this.SequestOption.FilterByDeltaCn = this.filterByDeltaCn.Checked;
      this.SequestOption.FilterBySpRank = this.filterBySpRank.Checked;

      this.SequestOption.MinXcorr1 = this.minXcorr1.Value;
      this.SequestOption.MinXcorr2 = this.minXcorr2.Value;
      this.SequestOption.MinXcorr3 = this.minXcorr3.Value;

      this.SequestOption.MinDeltaCn = this.minDeltaCn.Value;
      this.SequestOption.MaxSpRank = this.maxSpRank.Value;
    }
  }
}
