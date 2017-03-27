using RCPA.Gui;
using RCPA.Gui.FileArgument;
using RCPA.Proteomics.Summary.Uniform;

namespace RCPA.Proteomics.MSFlagger
{
  public partial class MSFlaggerDatasetPanel : ExpectValueDatasetPanel
  {
    private MSFlaggerDatasetOptions MSFlaggerOption { get { return Options as MSFlaggerDatasetOptions; } }

    public MSFlaggerDatasetPanel()
    {
      InitializeComponent();

      this.datFiles = new RcpaListViewMultipleFileField(
        this.btnAddFiles,
        this.btnRemoveFiles,
        this.btnLoad,
        this.btnSave,
        this.lvDatFiles,
        "XmlFiles",
        new OpenFileArgument("MSFlagger PepXml",  "pepXML"),
        true,
        false);
      AddComponent(this.datFiles);

      base.filterByExpectValue.Checked = true;
      base.filterByScore.Checked = false;
    }
  }
}
