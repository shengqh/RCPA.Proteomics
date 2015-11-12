using RCPA.Gui;
using RCPA.Gui.FileArgument;
using RCPA.Proteomics.Sequest;
using RCPA.Proteomics.Summary.Uniform;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace RCPA.Proteomics.Comet
{
  public partial class CometDatasetPanel : AbstractSequestDatasetPanel
  {
    public CometDatasetOptions CometOption { get { return Options as CometDatasetOptions; } }

    private readonly RcpaCheckBox filterByEvalue;

    private readonly RcpaDoubleField maxEvalue;

    private RcpaListViewMultipleFileField dataFiles;

    public CometDatasetPanel()
    {
      InitializeComponent();

      this.filterByEvalue = new RcpaCheckBox(this.cbFilterByEvalue, "FilterByEvalue", false);
      AddComponent(this.filterByEvalue);

      this.maxEvalue = new RcpaDoubleField(this.txtMaxEvalue, "MaxEvalue", "Max Evalue", 0.05, true);
      AddComponent(this.maxEvalue);

      this.dataFiles = new RcpaListViewMultipleFileField(
        this.btnAddFiles,
        this.btnRemoveFiles,
        this.btnLoad,
        this.btnSave,
        this.lvDirectories,
        "Files",
        new OpenFileArgument("Comet Xml", new []{"xml", "peptides"}, true),
        true,
        false);

      AddComponent(this.dataFiles);
    }

    protected override void DoBeforeValidateComponent()
    {
      base.DoBeforeValidateComponent();

      this.maxEvalue.Required = this.cbFilterByEvalue.Checked;
    }

    public override void LoadFromDataset()
    {
      base.LoadFromDataset();

      this.filterByEvalue.Checked = this.CometOption.FilterByEvalue;
      this.maxEvalue.Value = this.CometOption.MaxEvalue;
      
      this.dataFiles.ClearItems();
      this.dataFiles.AddItems(this.CometOption.PathNames.ToArray());
    }

    public override void SaveToDataset(bool selectedOnly)
    {
      base.SaveToDataset(selectedOnly);

      this.CometOption.FilterByEvalue = this.filterByEvalue.Checked;
      this.CometOption.MaxEvalue = this.maxEvalue.Value;

      if (selectedOnly)
      {
        this.CometOption.PathNames = new List<string>(dataFiles.GetSelectedItems());
      }
      else
      {
        this.CometOption.PathNames = new List<string>(dataFiles.GetAllItems());
      }
    }

    public override bool HasValidFile(bool selectedOnly)
    {
      if (selectedOnly)
      {
        return dataFiles.GetSelectedItems().Length > 0;
      }

      return dataFiles.GetAllItems().Length > 0;
    }
  }
}
