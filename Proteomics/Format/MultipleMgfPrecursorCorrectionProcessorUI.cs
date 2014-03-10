using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using RCPA.Gui;
using RCPA.Gui.FileArgument;
using RCPA.Proteomics.Processor;
using RCPA;
using System.IO;
using RCPA.Proteomics.IO;
using RCPA.Proteomics;
using RCPA.Gui.Command;
using RCPA.Proteomics.Mascot;
using RCPA.Proteomics.Spectrum;
using RCPA.Proteomics.Raw;

namespace RCPA.Proteomics.Format
{
  public partial class MultipleMgfPrecursorCorrectionProcessorUI : AbstractFileProcessorUI
  {
    private static readonly string title = "Multiple MGF Precursor Correction Processor";

    private static readonly string version = "1.0.0";

    private RcpaDirectoryField targetDir;

    public MultipleMgfPrecursorCorrectionProcessorUI()
    {
      InitializeComponent();

      this.Text = Constants.GetSQHTitle(title, version);

      this.SetFileArgument("PeptideFile", new OpenFileArgument("BuildSummary Peptides", "peptides"));

      targetDir = new RcpaDirectoryField(btnTargetDir, txtTargetDir, "TargetDir", "Target MGF", true);
      AddComponent(targetDir);

      mgfFiles.FileArgument = new OpenFileArgument("Mascot Generic Format", "mgf");
    }

    protected override IFileProcessor GetFileProcessor()
    {
      return new MultipleMgfPrecursorCorrectionMainProcessor(targetDir.FullName, mgfFiles.FileNames);
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
        new MultipleMgfPrecursorCorrectionProcessorUI().MyShow();
      }

      #endregion
    }
  }
}