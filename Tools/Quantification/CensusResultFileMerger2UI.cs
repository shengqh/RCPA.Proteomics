using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using RCPA.Gui;
using RCPA.Gui.Command;
using RCPA.Gui.FileArgument;

namespace RCPA.Tools.Quantification
{
  public partial class CensusResultFileMerger2UI : AbstractFileProcessorUI
  {
    private RcpaListBoxMultipleFileField resultFiles;

    public static readonly string Title = "Fast Census Result Files Merger (for Census Chro File Splitter result only)";
    public static readonly string Version = "1.0.2";

    public CensusResultFileMerger2UI()
    {
      InitializeComponent();

      SetFileArgument("TargetFile", new SaveFileArgument("Merged Census Result", "txt"));

      this.resultFiles = new RcpaListBoxMultipleFileField(btnAdd, btnRemove, null, null, null, lbResultFiles, "ResultFile", new OpenFileArgument("Census Result", "txt"), true, true);
      AddComponent(this.resultFiles);

      this.Text = Constants.GetSQHTitle(Title, Version);
    }

    protected override IFileProcessor GetFileProcessor()
    {
      return new CensusResultProteinMerger2(resultFiles.SelectedFileNames);
    }

    public class Command : IToolSecondLevelCommand
    {
      #region IToolCommand Members

      public string GetClassification()
      {
        return MenuCommandType.Quantification;
      }

      public string GetCaption()
      {
        return Title;
      }

      public string GetVersion()
      {
        return Version;
      }

      public void Run()
      {
        new CensusResultFileMerger2UI().MyShow();
      }

      #endregion

      #region IToolSecondLevelCommand Members

      public string GetSecondLevelCommandItem()
      {
        return "Census";
      }

      #endregion
    }
  }
}
