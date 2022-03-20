using RCPA.Gui;
using RCPA.Gui.Command;
using RCPA.Gui.FileArgument;
using System;

namespace RCPA.Proteomics.Format
{
  public partial class MaxQuantToMgfProcessorUI : AbstractFileProcessorUI
  {
    private static readonly string title = "MaxQuant APL To Mascot Generic Format Converter";
    private static readonly string version = "1.0.0";

    private RcpaMultipleFileComponent rawFileComp;
    private RcpaCheckBox mergeFile;

    public MaxQuantToMgfProcessorUI()
    {
      InitializeComponent();

      this.Text = Constants.GetSQHTitle(title, version);

      this.rawFiles.FileArgument = new OpenFileArgument("MaxQuant APL", ".apl");
      this.rawFileComp = new RcpaMultipleFileComponent(this.rawFiles.GetItemInfos(), "AplFiles", "MaxQuant APL", false, true);

      this.AddComponent(rawFileComp);

      this.mergeFile = new RcpaCheckBox(cbMerge, "MergeFile", false);
      AddComponent(this.mergeFile);
    }

    protected override IFileProcessor GetFileProcessor()
    {
      if (mergeFile.Checked)
      {
        return new MaxQuantToSingleMgfProcessor(this.rawFiles.GetItemInfos().Items.GetAllItems());
      }
      else
      {
        return new MaxQuantToMgfProcessor(this.rawFiles.GetItemInfos().Items.GetAllItems());
      }
    }

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
        new MaxQuantToMgfProcessorUI().MyShow();
      }

      #endregion
    }

    private void cbMerge_CheckedChanged(object sender, EventArgs e)
    {
      if (cbMerge.Checked)
      {
        this.SetFileArgument("TargetFile", new SaveFileArgument("Merged MGF", "mgf"));
      }
      else
      {
        this.SetDirectoryArgument("TargetDir", "Target MGF");
      }
    }
  }
}