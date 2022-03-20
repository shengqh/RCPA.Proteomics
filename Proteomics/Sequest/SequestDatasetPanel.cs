using RCPA.Gui;
using RCPA.Gui.FileArgument;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace RCPA.Proteomics.Sequest
{
  public partial class SequestDatasetPanel : AbstractSequestDatasetPanel
  {
    private RcpaListViewMultipleDirectoryField dataDirs;

    private OpenFileArgument zipFiles = new OpenFileArgument("Zipped dtas/outs or dta/out file", new[] { "zip", "peptides" }, true);

    private OpenFileArgument msfFiles = new OpenFileArgument("Proteome discoverer MSF file", new[] { "msf", "peptides" }, true);

    public SequestDatasetPanel()
    {
      InitializeComponent();

      this.dataDirs = new RcpaListViewMultipleDirectoryField(
        this.btnAddFiles,
        this.btnRemoveFiles,
        this.btnLoad,
        this.btnSave,
        this.lvDirectories,
        "Directories",
        "SEQUEST out/outs directory",
        true,
        false);

      dataDirs.Validator.ValidateFunc = (m =>
      {
        if (m.ToLower().EndsWith(".zip") || m.ToLower().EndsWith(".msf") || m.ToLower().EndsWith(".peptides"))
        {
          return File.Exists(m);
        }
        else
        {
          return Directory.Exists(m);
        }
      });

      AddComponent(this.dataDirs);
    }

    public override void LoadFromDataset()
    {
      base.LoadFromDataset();

      this.dataDirs.ClearItems();
      this.dataDirs.AddDirectories(this.SequestOption.PathNames.ToArray());
    }

    public override void SaveToDataset(bool selectedOnly)
    {
      base.SaveToDataset(selectedOnly);

      if (selectedOnly)
      {
        this.SequestOption.PathNames = new List<string>(dataDirs.GetSelectedItems());
      }
      else
      {
        this.SequestOption.PathNames = new List<string>(dataDirs.GetAllItems());
      }
    }

    private void btnAddSubDirectories_Click(object sender, EventArgs e)
    {
      using (var form = new InputDirectoryForm("Input folder", "Folder", ""))
      {
        if (form.ShowDialog(this) == DialogResult.OK)
        {
          string[] dirs = Directory.GetDirectories(form.Value);
          this.dataDirs.AddDirectories(dirs);
        }
      }
    }

    private void btnAddZips_Click(object sender, EventArgs e)
    {
      var dlg = zipFiles.GetFileDialog();
      if (dlg.ShowDialog() == DialogResult.OK)
      {
        this.dataDirs.AddItems(dlg.FileNames);
      }
    }

    public override bool HasValidFile(bool selectedOnly)
    {
      if (selectedOnly)
      {
        return dataDirs.GetSelectedItems().Length > 0;
      }

      return dataDirs.GetAllItems().Length > 0;
    }

    private void btnMsfFiles_Click(object sender, EventArgs e)
    {
      var dlg = msfFiles.GetFileDialog();
      if (dlg.ShowDialog() == DialogResult.OK)
      {
        this.dataDirs.AddItems(dlg.FileNames);
      }
    }
  }
}
