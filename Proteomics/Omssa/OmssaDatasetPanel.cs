using RCPA.Gui;
using RCPA.Gui.FileArgument;
using RCPA.Proteomics.Summary.Uniform;

namespace RCPA.Proteomics.Omssa
{
  public partial class OmssaDatasetPanel : ExpectValueDatasetPanel
  {
    private OmssaDatasetOptions CurrentOptions { get { return Options as OmssaDatasetOptions; } }

    public OmssaDatasetPanel()
    {
      InitializeComponent();

      this.datFiles = new RcpaListViewMultipleFileField(
        this.btnAddFiles,
        this.btnRemoveFiles,
        this.btnLoad,
        this.btnSave,
        this.lvDatFiles,
        "DatFiles",
        new OpenFileArgument("OMSSA", "omx"),
        true,
        false);
      AddComponent(this.datFiles);
    }
  }
}
