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

namespace RCPA.Proteomics.Summary.Uniform
{
  public partial class MascotDatasetPanel : ExpectValueDatasetPanel
  {
    private MascotDatasetOptions MascotOption { get { return Option as MascotDatasetOptions; } }

    public MascotDatasetPanel()
    {
      InitializeComponent();

      this.datFiles = new RcpaListViewMultipleFileField(
        this.btnAddFiles,
        this.btnRemoveFiles,
        this.btnLoad,
        this.btnSave,
        this.lvDatFiles,
        "DatFiles",
        new OpenFileArgument("Mascot Dat", "dat"),
        true,
        false);
      AddComponent(this.datFiles);
    }

    private void btnMgfFiles_Click(object sender, EventArgs e)
    {
      SummaryUtils.FindMgf(lvDatFiles);
    }

    private void btnRenameDat_Click(object sender, EventArgs e)
    {
      SummaryUtils.RenameAsMgf(lvDatFiles);
    }

    private void btnFindDB_Click(object sender, EventArgs e)
    {
      SummaryUtils.FindDatDB(lvDatFiles);
    }

    protected override string GetTitleSample()
    {
      if (datFiles.SelectFileNames.Length > 0)
      {
        using (StreamReader sr = new StreamReader(datFiles.SelectFileNames[0]))
        {
          string line;
          while ((line = sr.ReadLine()) != null)
          {
            if (line.StartsWith("title="))
            {
              return Uri.UnescapeDataString(line.Substring(6));
            }
          }
        }
      }
      return base.GetTitleSample();
    }
  }
}
