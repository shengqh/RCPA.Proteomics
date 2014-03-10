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
using System.IO;
using RCPA.Proteomics.PFind;

namespace RCPA.Proteomics.Summary.Uniform
{
  public partial class PFindDatasetPanel : ExpectValueDatasetPanel
  {
    private PFindDatasetOptions PFindOption { get { return Option as PFindDatasetOptions; } }

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
      return base.GetTitleSample();
    }
  }
}
