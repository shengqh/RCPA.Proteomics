using RCPA.Gui;
using RCPA.Gui.FileArgument;
using RCPA.Proteomics.Summary.Uniform;
using System.Linq;

namespace RCPA.Proteomics.PeptideProphet
{
  public partial class PeptideProphetDatasetPanel : TitleDatasetPanel
  {
    private RcpaDoubleField minPvalue;
    private RcpaCheckBox filterByMinPvalue;

    private PeptideProphetDatasetOptions CurrentOptions { get { return Options as PeptideProphetDatasetOptions; } }

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

      this.filterByMinPvalue.Checked = CurrentOptions.FilterByMinProbability;
      if (CurrentOptions.FilterByMinProbability)
      {
        this.minPvalue.Value = CurrentOptions.MinPValue;
      }

      xmlFiles.Items.Clear();
      xmlFiles.Items.AddRange(CurrentOptions.PathNames.ToArray());
      xmlFiles.SelectAll();
    }

    public override void SaveToDataset(bool selectedOnly)
    {
      base.SaveToDataset(selectedOnly);

      CurrentOptions.FilterByMinProbability = this.filterByMinPvalue.Checked;
      if (CurrentOptions.FilterByMinProbability)
      {
        CurrentOptions.MinPValue = this.minPvalue.Value;
      }

      if (selectedOnly)
      {
        Options.PathNames = xmlFiles.SelectedFileNames.ToList();
      }
      else
      {
        Options.PathNames = xmlFiles.FileNames.ToList();
      }
    }


    public override bool HasValidFile(bool selectedOnly)
    {
      if (selectedOnly)
      {
        return xmlFiles.SelectedFileNames.Length > 0;
      }

      return xmlFiles.FileNames.Length > 0;
    }
  }
}
