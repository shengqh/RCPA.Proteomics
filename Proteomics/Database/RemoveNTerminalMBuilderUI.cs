using System;
using System.IO;
using System.Windows.Forms;
using RCPA.Gui;
using RCPA.Gui.Command;
using RCPA.Gui.FileArgument;
using RCPA.Proteomics.Mascot;
using RCPA.Proteomics.Summary;
using System.Collections.Generic;
using RCPA.Utils;

namespace RCPA.Proteomics.Database
{
  public partial class RemoveNTerminalMBuilderUI : AbstractFileProcessorUI
  {
    public static readonly string title = "Remove N-Terminal M from protein sequence";

    public static readonly string version = "1.0.0";

    public RemoveNTerminalMBuilderUI()
    {
      InitializeComponent();

      SetFileArgument("FastaFile", new OpenFileArgument("Protein Sequence", "fasta"));

      this.Text = Constants.GetSQHTitle(title, version);
    }

    protected override IFileProcessor GetFileProcessor()
    {
      return new RemoveNTerminalMBuilder();
    }

    public class Command : IToolCommand
    {
      #region IToolCommand Members

      public string GetClassification()
      {
        return MenuCommandType.Database;
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
        new RemoveNTerminalMBuilderUI().MyShow();
      }

      #endregion
    }
  }
}

