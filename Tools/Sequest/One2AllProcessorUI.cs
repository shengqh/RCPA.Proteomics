using RCPA.Gui;
using RCPA.Gui.Command;
using RCPA.Gui.Event;
using RCPA.Gui.FileArgument;
using RCPA.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;

namespace RCPA.Tools.Sequest
{
  public partial class One2AllProcessorUI : AbstractMultipleProgressFileProcessorUI
  {
    public static readonly string title = "Extract dta/out from dtas/outs files";

    public static readonly string version = "1.0.2";

    private RcpaMultipleFileComponent dtasFiles;

    private RcpaDirectoryField targetDir;

    private RcpaCheckBox extractToSameDirectory;

    private OpenFileDialog openDialog;

    public One2AllProcessorUI()
    {
      InitializeComponent();

      this.SetFileArgument("Peptides", new OpenFileArgument("BuildSummary Peptides", "peptides"));

      lbDtaFiles.FileArgument = new OpenFileArgument("Sequest Dtas", "dtas");

      this.dtasFiles = new RcpaMultipleFileComponent(lbDtaFiles.GetItemInfos(), "DtasFiles", "Sequest Dtas File", false, true);
      this.AddComponent(this.dtasFiles);

      this.targetDir = new RcpaDirectoryField(btnTargetDirectory, txtTargetDirectory, "TargetDirectory", "Target Directory", true);
      this.AddComponent(this.targetDir);

      this.extractToSameDirectory = new RcpaCheckBox(cbExtractToSameDirectory, "ExtractToSameDirectory", false);
      this.AddComponent(this.extractToSameDirectory);

      this.Text = Constants.GetSQHTitle(title, version);
    }

    protected override IFileProcessor GetFileProcessor()
    {
      return new One2AllProcessor(new List<string>(lbDtaFiles.FileNames), targetDir.FullName, extractToSameDirectory.Checked);
    }

    private void btnAddAll_Click(object sender, EventArgs e)
    {
      using (var form = new InputDirectoryForm("Input folder", "Folder", ""))
      {
        if (form.ShowDialog(this) == DialogResult.OK)
        {
          List<string> findFiles = FileUtils.GetFiles(form.Value, "*.dtas", true);
          foreach (string findFile in findFiles)
          {
            if (lbDtaFiles.Items.Contains(findFile))
            {
              continue;
            }

            lbDtaFiles.Items.Add(findFile);
          }

          findFiles = FileUtils.GetFiles(form.Value, "*.dtas.zip", true);
          foreach (string findFile in findFiles)
          {
            if (lbDtaFiles.Items.Contains(findFile))
            {
              continue;
            }

            lbDtaFiles.Items.Add(findFile);
          }
        }
      }
    }

    private void btnLoad_Click(object sender, EventArgs e)
    {
      if (this.openDialog == null)
      {
        this.openDialog = new OpenFileDialog();
        this.openDialog.Filter = "File list(*.lst)|*.lst|All Files(*.*)|*.*";
      }

      if (this.openDialog.ShowDialog(this) == DialogResult.OK)
      {
        ListFileReader reader = new ListFileReader();

        List<string> dirOrFiles = reader.ReadFromFile(openDialog.FileName);

        if (dirOrFiles.Count == 0)
        {
          SimpleItemInfos infos = new SimpleItemInfos();

          ItemInfosEventHandlers handlers = new ItemInfosEventHandlers(infos);

          handlers.Adaptor.LoadFromXml(XElement.Load(openDialog.FileName, LoadOptions.SetBaseUri));

          dirOrFiles = infos.Items.GetAllItems().ToList();
        }

        List<string> files = new List<string>();
        foreach (string dirOrFile in dirOrFiles)
        {
          if (Directory.Exists(dirOrFile))
          {
            files.AddRange(new DirectoryInfo(dirOrFile).GetFiles("*.dtas").ToList().ConvertAll(m => m.FullName));
            continue;
          }

          files.Add(dirOrFile);
        }

        lbDtaFiles.FileNames = files.ToArray();
      }
    }
  }

  public class One2AllProcessorCommand : IToolCommand
  {
    #region IToolCommand Members

    public string GetClassification()
    {
      return MenuCommandType.Sequest;
    }

    public string GetCaption()
    {
      return One2AllProcessorUI.title;
    }

    public string GetVersion()
    {
      return One2AllProcessorUI.version;
    }

    public void Run()
    {
      new One2AllProcessorUI().MyShow();
    }

    #endregion
  }

}
