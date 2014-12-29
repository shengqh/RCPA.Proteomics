using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using RCPA.Gui;
using RCPA.Proteomics.MzIdent;
using RCPA.Gui.FileArgument;
using RCPA.Proteomics.Summary.Uniform;

namespace RCPA.Proteomics.MzIdent
{
  public partial class MzIdentDatasetPanel : ExpectValueDatasetPanel
  {
    private AbstractMzIdentDatasetOptions mzIdentOptions { get { return Options as AbstractMzIdentDatasetOptions; } }

    public MzIdentDatasetPanel()
    {
      InitializeComponent();

      this.datFiles = new RcpaListViewMultipleFileField(
        this.btnAddFiles,
        this.btnRemoveFiles,
        this.btnLoad,
        this.btnSave,
        this.lvDatFiles,
        "MzIdentFiles",
        new OpenFileArgument("MzIdent", "mzid"),
        true,
        false);
      AddComponent(this.datFiles);
    }
  }
}
