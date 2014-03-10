using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using RCPA.Gui;
using RCPA.Gui.Command;
using RCPA.Gui.FileArgument;
using RCPA.Proteomics.Mascot;
using RCPA.Proteomics.Summary;
using RCPA.Seq;
using RCPA.Tools.Summary;

namespace RCPA.Tools.Distiller
{
  public partial class MaxQuantMgfDistillerUI : AbstractFileProcessorUI
  {
    public static readonly string title = "MaxQuant MGF File Distiller";

    public static readonly string version = "1.0.1";

    private readonly RcpaListViewMultipleFileField mgfFiles;

    private readonly RcpaComboBox<ITitleParser> titleParsers;

    private RcpaFileField singleFileName;
    private RcpaCheckBox singleFile;

    public MaxQuantMgfDistillerUI()
    {
      InitializeComponent();

      this.SetFileArgument("PeptideFile", new OpenFileArgument("BuildSummary Peptides", "peptides"));

      List<ITitleParser> allParsers = TitleParserUtils.GetTitleParsers();

      this.titleParsers = new RcpaComboBox<ITitleParser>(this.cbTitleFormat, "TitleFormat", allParsers.ToArray(), 0);
      AddComponent(this.titleParsers);

      this.mgfFiles = new RcpaListViewMultipleFileField(
        this.btnAddFiles,
        this.btnRemoveFiles,
        this.btnLoad,
        this.btnSave,
        this.lvMgfFiles,
        "MgfFiles",
        new OpenFileArgument("Mascot Generic Format", new string[] { "msm", "mgf" }),
        true,
        true);
      AddComponent(this.mgfFiles);

      this.singleFile = new RcpaCheckBox(cbSingleFile, "SingleFile", false);
      AddComponent(this.singleFile);

      this.singleFileName = new RcpaFileField(btnMgfFile, txtSingleFile, "SingleFilename", new SaveFileArgument("MGF", "mgf"), false);
      AddComponent(this.singleFileName);

      Text = Constants.GetSQHTitle(title, version);
    }

    protected override void DoBeforeValidate()
    {
      base.DoBeforeValidate();

      this.singleFileName.Required = singleFile.Checked;
    }

    private void lvDatFiles_SizeChanged(object sender, EventArgs e)
    {
      this.lvMgfFiles.Columns[0].Width = this.lvMgfFiles.ClientSize.Width - this.lvMgfFiles.Columns[1].Width;
    }

    private void btnTitle_Click(object sender, EventArgs e)
    {
      this.lvMgfFiles.BeginUpdate();
      try
      {
        foreach (ListViewItem item in this.lvMgfFiles.Items)
        {
          string msmFile = item.Text;
          if (File.Exists(msmFile))
          {
            while (item.SubItems.Count < 2)
            {
              item.SubItems.Add("");
            }

            using (var sr = new StreamReader(msmFile))
            {
              MascotGenericFormatSectionReader reader = new MascotGenericFormatSectionReader(sr);
              item.SubItems[1].Text = reader.GetNextTitle();
            }
          }
        }
      }
      finally
      {
        this.lvMgfFiles.EndUpdate();
      }
    }

    protected override IFileProcessor GetFileProcessor()
    {
      return new MaxQuantMgfDistiller(mgfFiles.SelectFileNames, titleParsers.SelectedItem, singleFile.Checked, singleFileName.FullName);
    }

    #region Nested type: Command

    public class Command : IToolCommand
    {
      #region IToolCommand Members

      public string GetClassification()
      {
        return MenuCommandType.MaxQuant;
      }

      public string GetCaption()
      {
        return title;
      }

      public string GetVersion()
      {
        return version;
      }

      public void Run()
      {
        new MaxQuantMgfDistillerUI().MyShow();
      }

      #endregion
    }

    #endregion
  }
}