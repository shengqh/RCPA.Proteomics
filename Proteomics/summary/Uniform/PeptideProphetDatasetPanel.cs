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
  public partial class PeptideProphetDatasetPanel : TitleDatasetPanel
  {
    private RcpaDoubleField minPvalue;
    private RcpaCheckBox filterByMinPvalue;

    private PeptideProphetDatasetOptions CurrentOptions { get { return Option as PeptideProphetDatasetOptions; } }

    public PeptideProphetDatasetPanel()
    {
      InitializeComponent();

      this.xmlFiles.FileArgument = new OpenFileArgument("PeptidePhophet XML", "xml");
      AddComponent(new RcpaMultipleFileComponent(this.xmlFiles.GetItemInfos(), "XmlFiles", "PeptidePhophet Xml Files", false, true));

      this.filterByMinPvalue = new RcpaCheckBox(cbFilterByPValue, "FilterByPValue", true);
      AddComponent(this.filterByMinPvalue);

      this.minPvalue = new RcpaDoubleField(txtMinProbabilityValue, "MinPValue", "Minimum Probability Value", 0.5, true);
      AddComponent(this.minPvalue);
    }

    protected override void DoBeforeValidateComponent()
    {
      base.DoBeforeValidateComponent();

      this.minPvalue.Required = this.filterByMinPvalue.Checked;
    }

    public override void LoadFromDataset()
    {
      base.LoadFromDataset();

      this.filterByMinPvalue.Checked = CurrentOptions.FilterByMinPValue;
      if (CurrentOptions.FilterByMinPValue)
      {
        this.minPvalue.Value = CurrentOptions.MinPValue;
      }

      xmlFiles.Items.Clear();
      xmlFiles.Items.AddRange(CurrentOptions.PathNames.ToArray());
    }

    public override void SaveToDataset()
    {
      base.SaveToDataset();

      CurrentOptions.FilterByMinPValue = this.filterByMinPvalue.Checked;
      if (CurrentOptions.FilterByMinPValue)
      {
        CurrentOptions.MinPValue = this.minPvalue.Value;     
      }

      Option.PathNames = xmlFiles.FileNames.ToList();
    }

  }
}
