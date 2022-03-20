﻿using RCPA.Gui;
using System;
using System.Windows.Forms;

namespace RCPA.Proteomics.Summary.Uniform
{
  public partial class DatasetPanelBase : UserControl, IDatasetFormat
  {
    private RcpaCheckBox enabled;

    private RcpaTextField datasetName;

    private RcpaCheckBox filterPrecursorPPMTolerance;

    private RcpaDoubleField precursorPPMTolerance;

    private RcpaCheckBox filterPrecursorIsotopic;

    private RcpaCheckBox filterPrecursorByDynamicTolerance;

    private RcpaCheckBox searchedByDifferentParameters;

    private RcpaComboBox<IScoreFunction> scoreFunctions;

    protected RcpaComponentList componentList { get; set; }

    private IDatasetOptions options;

    public IDatasetOptions Options
    {
      get
      {
        return this.options;
      }
      set
      {
        this.options = value;

        DoAfterSettingOptions();
      }
    }

    protected virtual void DoAfterSettingOptions()
    {
      if (this.options != null)
      {
        RemoveComponent(this.scoreFunctions);
        this.scoreFunctions = new RcpaComboBox<IScoreFunction>(cbScoreFunctions, "ScoreFunction", this.options.SearchEngine.GetFactory().GetScoreFunctions(), 0, true);
        AddComponent(this.scoreFunctions);
      }
    }

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

    public DatasetPanelBase()
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

      this.searchedByDifferentParameters = new RcpaCheckBox(this.cbSearchedByDifferentParameters, "SearchedByDifferentParameters", false);
      AddComponent(this.searchedByDifferentParameters);
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
      if (null != comp && this.componentList.ContainsKey(comp))
      {
        this.componentList.Remove(comp);
      }
    }

    protected void SetComponentEnabled(IRcpaComponent comp, bool enabled)
    {
      this.componentList[comp] = enabled;
    }

    protected virtual void DoBeforeValidateComponent()
    {
      this.precursorPPMTolerance.Required = this.filterPrecursorPPMTolerance.Checked;
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

      cbSearchedByDifferentParameters.Checked = Options.SearchedByDifferentParameters;

      bool bFound = false;
      for (int i = 0; i < scoreFunctions.Items.Length; i++)
      {
        if (scoreFunctions.Items[i].ScoreName.Equals(Options.ScoreFunction.ScoreName))
        {
          scoreFunctions.SelectedIndex = i;
          bFound = true;
          break;
        }
      }
      if (!bFound)
      {
        scoreFunctions.SelectedIndex = 0;
      }
    }

    public virtual void SaveToDataset(bool selectedOnly)
    {
      Options.Name = this.datasetName.Text;
      Options.Enabled = this.enabled.Checked;

      Options.FilterByPrecursor = this.filterPrecursorPPMTolerance.Checked;
      Options.FilterByPrecursorIsotopic = this.filterPrecursorIsotopic.Checked;
      Options.FilterByPrecursorDynamicTolerance = this.filterPrecursorByDynamicTolerance.Checked;
      Options.ScoreFunction = scoreFunctions.SelectedItem;
      Options.SearchedByDifferentParameters = cbSearchedByDifferentParameters.Checked;

      if (Options.FilterByPrecursor)
      {
        Options.PrecursorPPMTolerance = this.precursorPPMTolerance.Value;
      }
    }

    public virtual bool HasValidFile(bool selectedOnly)
    {
      throw new NotImplementedException();
    }
  }
}
