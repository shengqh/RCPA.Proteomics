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

namespace RCPA.Tools.Distiller
{
  public partial class UniquePeptideDistillerUI : AbstractFileProcessorUI
  {
    public static readonly string title = "Unique Peptide Distiller";

    public static readonly string version = "1.0.1";

    public UniquePeptideDistillerUI()
    {
      InitializeComponent();

      SetFileArgument("PeptideFile", new OpenFileArgument("Peptides", "peptides"));

      this.Text = Constants.GetSQHTitle(title, version) + " - Peptides with same sequences but different charges will be treated as different peptides";
    }

    protected override IFileProcessor GetFileProcessor()
    {
      return new UniquePeptideDistiller();
    }

    public class Command : IToolCommand
    {
      #region IToolCommand Members

      public string GetClassification()
      {
        return MenuCommandType.Distiller;
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
        new UniquePeptideDistillerUI().MyShow();
      }

      #endregion
    }
  }
}

