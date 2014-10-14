using RCPA.Gui;
using RCPA.Gui.FileArgument;
using RCPA.Proteomics.Summary;
using RCPA.Proteomics.Summary.Uniform;
using System;

namespace RCPA.Proteomics.PFind
{
  public partial class PFindDatasetPanel : ExpectValueDatasetPanel
  {
    private PFindDatasetOptions PFindOption { get { return Options as PFindDatasetOptions; } }

    public PFindDatasetPanel()
    {
      InitializeComponent();

      this.datFiles = new RcpaListViewMultipleFileField(
        this.btnAddFiles,
        this.btnRemoveFiles,
        this.btnLoad,
        this.btnSave,
        this.lvDatFiles,
        "PFindFiles",
        new OpenFileArgument("PFind Text", "txt"),
        true,
        false);
      AddComponent(this.datFiles);
    }

    private void btnMgfFiles_Click(object sender, EventArgs e)
    {
      SummaryUtils.FindPFindSource(lvDatFiles);
    }

    private void btnRenameDat_Click(object sender, EventArgs e)
    {
      SummaryUtils.RenameAsPFindSource(lvDatFiles);
    }

    protected override string GetTitleSample()
    {
      if (datFiles.SelectFileNames.Length > 0)
      {
        return PFindSpectrumParser.GetTitleSample(datFiles.SelectFileNames[0]);
      }
      else if (datFiles.FileNames.Length > 0)
      {
        return PFindSpectrumParser.GetTitleSample(datFiles.FileNames[0]);
      }
      return base.GetTitleSample();
    }

    private void btnFindDatabase_Click(object sender, EventArgs e)
    {
      SummaryUtils.FindPFindDatabase(lvDatFiles);
    }
  }
}
