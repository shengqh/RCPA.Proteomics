using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using RCPA.Gui;
using RCPA.Gui.FileArgument;
using RCPA.Gui.Command;

namespace RCPA.Proteomics.Format
{
  public partial class MergeMgfFileProcessorUI : AbstractFileProcessorUI
  {
    private static string title = "Merge Mascot Generic Format Files";
    private static string version = "1.0.0";

    public MergeMgfFileProcessorUI()
    {
      InitializeComponent();

      sourceFiles.FileArgument = new OpenFileArgument("Mascot Generic Format", new string[] { "mgf", "msm" });
      AddComponent(sourceFiles);

      SetFileArgument("TargetFile", new SaveFileArgument("Target Mascot Generic Format", "mgf"));

      this.Text = Constants.GetSQHTitle(title, version);
    }

    protected override IFileProcessor GetFileProcessor()
    {
      return new MergeMgfFileProcessor(sourceFiles.FileNames);
    }

    public class Command : IToolCommand
    {
      #region IToolCommand Members

      public string GetClassification()
      {
        return MenuCommandType.Format;
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
        new MergeMgfFileProcessorUI().MyShow();
      }

      #endregion
    }
  }
}
