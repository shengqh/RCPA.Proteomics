using RCPA.Gui;
using RCPA.Gui.FileArgument;
using RCPA.Proteomics.Summary.Uniform;

namespace RCPA.Proteomics.MSAmanda
{
  public partial class MSAmandaDatasetPanel : ScoreDatasetPanel
  {
    public MSAmandaDatasetPanel()
    {
      InitializeComponent();

      this.datFiles = new RcpaListViewMultipleFileField(
        this.btnAddFiles,
        this.btnRemoveFiles,
        this.btnLoad,
        this.btnSave,
        this.lvDatFiles,
        "MSAmandaFiles",
        new OpenFileArgument("MSAmanda", "csv"),
        true,
        false);
      AddComponent(this.datFiles);
      minScore.DefaultValue = "100";
    }
  }
}
