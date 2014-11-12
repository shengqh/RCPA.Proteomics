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
  public partial class AbstractDatasetPanel : UserControl, IDatasetFormat
  {
    private RcpaCheckBox enabled;

    private RcpaTextField datasetName;

    private RcpaCheckBox filterPrecursorPPMTolerance;

    private RcpaDoubleField precursorPPMTolerance;

    private RcpaCheckBox filterPrecursorIsotopic;

    private RcpaCheckBox filterPrecursorByDynamicTolerance;

    private RcpaCheckBox filterByScore;

    private RcpaDoubleField minScoreCharge1;

    private RcpaDoubleField minScoreCharge2;

    private RcpaDoubleField minScoreCharge3;

    private RcpaCheckBox filterByDeltaScore;

    private RcpaDoubleField minDeltaScore;

    private RcpaCheckBox filterByEvalue;

    private RcpaDoubleField maxEvalue;

    protected RcpaComponentList componentList { get; set; }

    public virtual AbstractDatasetOptions2 Options { get; set; }

    protected RcpaListViewField dataFiles;

    public bool DatasetEnabled
    {
      get
      {
        return enabled.Checked;
      }
      set
      {
        enabled.Checked = value;
      }
    }

    public string DatasetName
    {
      get
      {
        return datasetName.Text;
      }
      set
      {
        datasetName.Text = value;
      }
    }

    public void SetShowName(bool value)
    {
      cbEnabled.Visible = value;
      lblName.Visible = value;
      txtDatasetName.Visible = value;
      if (!value)
      {
        pnlName.Visible = false;
        pnlName.Height = 0;
      }
    }

    public AbstractDatasetPanel()
    {
      InitializeComponent();

      componentList = new RcpaComponentList();

      this.enabled = new RcpaCheckBox(cbEnabled, "Enabled", true);
      AddComponent(enabled);

      this.datasetName = new RcpaTextField(txtDatasetName, "DatasetName", "Dataset Name", "", true);
      AddComponent(datasetName);

      this.filterPrecursorPPMTolerance = new RcpaCheckBox(this.cbFilterByPrecursor, "FilterPrecursor", false);
      AddComponent(this.filterPrecursorPPMTolerance);

      this.filterPrecursorIsotopic = new RcpaCheckBox(this.cbFilterByPrecursorIsotopic, "FilterPrecursorIsotopic", true);
      AddComponent(this.filterPrecursorIsotopic);

      this.precursorPPMTolerance = new RcpaDoubleField(this.txtPrecursorPPMTolerance, "PrecursorPPMTolerance", "Precursor Tolerance (ppm)", 10, false);
      AddComponent(this.precursorPPMTolerance);

      this.filterPrecursorByDynamicTolerance = new RcpaCheckBox(this.cbFilterByDynamicPrecursorTolerance, "FilterPrecursorByDynamicTolerance", true);
      AddComponent(this.filterPrecursorByDynamicTolerance);

      this.filterByScore = new RcpaCheckBox(this.cbFilterByScore, "FilterByScore", false);
      AddComponent(this.filterByScore);

      this.minScoreCharge1 = new RcpaDoubleField(this.txtScore1, "MinScore1", "Min Score for Charge 1", 1.0, false);
      AddComponent(this.minScoreCharge1);

      this.minScoreCharge2 = new RcpaDoubleField(this.txtScore2, "MinScore2", "Min Score for Charge 2", 1.5, false);
      AddComponent(this.minScoreCharge2);

      this.minScoreCharge3 = new RcpaDoubleField(this.txtScore3, "MinScore3", "Min Score for Charge 3", 2.0, false);
      AddComponent(this.minScoreCharge3);

      this.filterByDeltaScore = new RcpaCheckBox(this.cbFilterByDeltaScore, "FilterByDeltaScore", true);
      AddComponent(this.filterByDeltaScore);

      this.minDeltaScore = new RcpaDoubleField(this.txtMinDeltaScore, "MinDeltaScore", "Min Delta Score", 0.1, true);
      AddComponent(this.minDeltaScore);

      this.filterByEvalue = new RcpaCheckBox(this.cbFilterByEvalue, "FilterByEvalue", false);
      AddComponent(this.filterByEvalue);

      this.maxEvalue = new RcpaDoubleField(this.txtMaxEvalue, "MaxEvalue", "Max Evalue", 0.05, true);
      AddComponent(this.maxEvalue);

      this.dataFiles = InitializeDataFiles();
      AddComponent(this.dataFiles);
    }

    protected virtual RcpaListViewField InitializeDataFiles()
    {
      throw new NotImplementedException();
    }

    public void AddNameChangedEvent(EventHandler nameChanged)
    {
      this.txtDatasetName.TextChanged += nameChanged;
    }

    public void RemoveNameChangedEvent(EventHandler nameChanged)
    {
      this.txtDatasetName.TextChanged -= nameChanged;
    }

    protected void AddComponent(IRcpaComponent comp)
    {
      if (comp == null)
      {
        throw new ArgumentException("Argument cannot be null.");
      }

      this.componentList[comp] = true;
    }

    protected void RemoveComponent(IRcpaComponent comp)
    {
      this.componentList.Remove(comp);
    }

    protected void SetComponentEnabled(IRcpaComponent comp, bool enabled)
    {
      this.componentList[comp] = enabled;
    }

    protected virtual void DoBeforeValidateComponent()
    {
      this.precursorPPMTolerance.Required = this.filterPrecursorPPMTolerance.Checked;
      this.minScoreCharge1.Required = this.cbFilterByScore.Checked;
      this.minScoreCharge2.Required = this.cbFilterByScore.Checked;
      this.minScoreCharge3.Required = this.cbFilterByScore.Checked;
      this.minDeltaScore.Required = this.cbFilterByDeltaScore.Checked;
      this.maxEvalue.Required = this.cbFilterByEvalue.Checked;
    }

    public virtual void ValidateComponents()
    {
      DoBeforeValidateComponent();

      this.componentList.ValidateComponent();
    }

    public virtual void LoadFromDataset()
    {
      this.datasetName.Text = Options.Name;
      this.enabled.Checked = Options.Enabled;

      cbFilterByPrecursor.Checked = Options.FilterByPrecursor;
      cbFilterByPrecursorIsotopic.Checked = Options.FilterByPrecursorIsotopic;
      cbFilterByDynamicPrecursorTolerance.Checked = Options.FilterByPrecursorDynamicTolerance;
      if (Options.FilterByPrecursor)
      {
        this.precursorPPMTolerance.Value = Options.PrecursorPPMTolerance;
      }

      this.filterByScore.Checked = this.Options.FilterByXcorr;
      this.filterByDeltaScore.Checked = this.Options.FilterByDeltaCn;

      this.minScoreCharge1.Value = this.Options.MinXcorr1;
      this.minScoreCharge2.Value = this.Options.MinXcorr2;
      this.minScoreCharge3.Value = this.Options.MinXcorr3;

      this.minDeltaScore.Value = this.Options.MinDeltaCn;

      this.filterByEvalue.Checked = this.Options.FilterByEvalue;
      this.maxEvalue.Value = this.Options.MaxEvalue;

      this.dataFiles.ClearItems();
      this.dataFiles.AddItems(this.Options.PathNames.ToArray());
    }

    public virtual void SaveToDataset(bool selectedOnly)
    {
      Options.Name = this.datasetName.Text;
      Options.Enabled = this.enabled.Checked;

      Options.FilterByPrecursor = this.filterPrecursorPPMTolerance.Checked;
      Options.FilterByPrecursorIsotopic = this.filterPrecursorIsotopic.Checked;
      Options.FilterByPrecursorDynamicTolerance = this.filterPrecursorByDynamicTolerance.Checked;
      if (Options.FilterByPrecursor)
      {
        Options.PrecursorPPMTolerance = this.precursorPPMTolerance.Value;
      }

      this.Options.FilterByXcorr = this.filterByScore.Checked;
      this.Options.FilterByDeltaCn = this.filterByDeltaScore.Checked;
      this.Options.FilterByEvalue = this.filterByEvalue.Checked;

      this.Options.MinXcorr1 = this.minScoreCharge1.Value;
      this.Options.MinXcorr2 = this.minScoreCharge2.Value;
      this.Options.MinXcorr3 = this.minScoreCharge3.Value;

      this.Options.MinDeltaCn = this.minDeltaScore.Value;

      this.Options.MaxEvalue = this.maxEvalue.Value;

      if (selectedOnly)
      {
        this.Options.PathNames = new List<string>(dataFiles.GetSelectedItems());
      }
      else
      {
        this.Options.PathNames = new List<string>(dataFiles.GetAllItems());
      }
    }

    public virtual bool HasValidFile(bool selectedOnly)
    {
      if (selectedOnly)
      {
        return dataFiles.GetSelectedItems().Length > 0;
      }

      return dataFiles.GetAllItems().Length > 0;
    }
  }
}
