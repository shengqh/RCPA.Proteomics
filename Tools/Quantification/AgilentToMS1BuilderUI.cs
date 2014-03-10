using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using RCPA.Gui;
using System.IO;
using RCPA.Gui.Command;

namespace RCPA.Tools.Quantification
{
  public partial class AgilentToMS1BuilderUI : AbstractFileProcessorUI
  {
    public AgilentToMS1BuilderUI()
    {
      InitializeComponent();

      base.SetDirectoryArgument("RawDir", "Agilent .d/root");

      this.targetDir = new RcpaDirectoryField(this.btnTargetDir, this.txtTargetDir, "TargetDir", "Target MS1", false);

      AddComponent(this.targetDir);

      this.Text = Constants.GetSQHTitle(title, version);
    }

    private static readonly string title = "Agilent To MS1 Builder";

    private static readonly string version = "1.0.0";

    private readonly RcpaDirectoryField targetDir;

    protected override IFileProcessor GetFileProcessor()
    {
      DirectoryInfo originalDir = new DirectoryInfo(GetOriginFile());
      if (originalDir.Name.EndsWith(".d"))
      {
        string targetDirName;
        if (targetDir.Exists)
        {
          targetDirName = targetDir.FullName;
        }
        else
        {
          targetDirName = originalDir.Parent.FullName;
        }

        return new Agilent2Ms1FileBuilder(targetDirName, title, version);
      }
      else
      {
        string targetDirName;
        if (targetDir.Exists)
        {
          targetDirName = targetDir.FullName;
        }
        else
        {
          targetDirName = originalDir.FullName;
        }

        return new Agilent2Ms1DirectoryBuilder(targetDirName, title, version);
      }
    }

    public class Command : IToolCommand
    {
      #region IToolCommand Members

      public string GetClassification()
      {
        return MenuCommandType.Quantification;
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
        new AgilentToMS1BuilderUI().MyShow();
      }

      #endregion
    }
  }
}
