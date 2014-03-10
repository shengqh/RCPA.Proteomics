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
  public partial class DatasetPanelBase : UserControl, IDatasetFormat
  {
    private RcpaCheckBox enabled;

    private RcpaTextField datasetName;

    private RcpaCheckBox filterPrecursorPPMTolerance;

    private RcpaDoubleField precursorPPMTolerance;

    private RcpaCheckBox filterPrecursorIsotopic;

    private RcpaCheckBox filterPrecursorByDynamicTolerance;

    protected RcpaComponentList componentList { get; set; }

    public IDatasetOptions Option { get; set; }

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
    }

    public virtual void ValidateComponents()
    {
      DoBeforeValidateComponent();

      this.componentList.ValidateComponent();
    }

    public virtual void LoadFromDataset()
    {
      this.datasetName.Text = Option.Name;
      this.enabled.Checked = Option.Enabled;

      cbFilterByPrecursor.Checked = Option.FilterByPrecursor;
      cbFilterByPrecursorIsotopic.Checked = Option.FilterByPrecursorIsotopic;
      cbFilterByDynamicPrecursorTolerance.Checked = Option.FilterByPrecursorDynamicTolerance;
      if (Option.FilterByPrecursor)
      {
        this.precursorPPMTolerance.Value = Option.PrecursorPPMTolerance;
      }
    }

    public virtual void SaveToDataset()
    {
      Option.Name = this.datasetName.Text;
      Option.Enabled = this.enabled.Checked;

      Option.FilterByPrecursor = this.filterPrecursorPPMTolerance.Checked;
      Option.FilterByPrecursorIsotopic = this.filterPrecursorIsotopic.Checked;
      Option.FilterByPrecursorDynamicTolerance = this.filterPrecursorByDynamicTolerance.Checked;
      if (Option.FilterByPrecursor)
      {
        Option.PrecursorPPMTolerance = this.precursorPPMTolerance.Value;
      }
    }
  }
}
