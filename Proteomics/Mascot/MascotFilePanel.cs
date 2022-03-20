using RCPA.Gui;
using RCPA.Gui.FileArgument;
using RCPA.Proteomics.Summary;
using RCPA.Proteomics.Summary.Uniform;
using System;

namespace RCPA.Proteomics.Mascot
{
  public partial class MascotFilePanel : FilePanel
  {
    public MascotFilePanel()
    {
      InitializeComponent();

      this.datFiles = new RcpaListViewMultipleFileField(
        this.btnAddFiles,
        this.btnRemoveFiles,
        this.btnLoad,
        this.btnSave,
        this.lvDatFiles,
        "DatFiles",
        new OpenFileArgument("Mascot DAT/MSF", new[] { "dat", "msf" }),
        true,
        false);
    }

    private void btnSouceFiles_Click(object sender, EventArgs e)
    {
      SummaryUtils.FindMgf(lvDatFiles);
    }

    private void btnAutoRename_Click(object sender, EventArgs e)
    {
      SummaryUtils.RenameAsMgf(lvDatFiles);
    }

    private void btnFindDatabase_Click(object sender, EventArgs e)
    {
      SummaryUtils.FindDatDatabase(lvDatFiles);
    }
  }
}
