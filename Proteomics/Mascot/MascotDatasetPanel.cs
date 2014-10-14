using RCPA.Gui;
using RCPA.Gui.FileArgument;
using RCPA.Proteomics.Summary;
using RCPA.Proteomics.Summary.Uniform;
using System;
using System.IO;

namespace RCPA.Proteomics.Mascot
{
  public partial class MascotDatasetPanel : ExpectValueDatasetPanel
  {
    private MascotDatasetOptions MascotOption { get { return Options as MascotDatasetOptions; } }

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
        new OpenFileArgument("Mascot DAT/MSF", new []{"msf", "dat"}),
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
      SummaryUtils.FindDatDatabase(lvDatFiles);
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
