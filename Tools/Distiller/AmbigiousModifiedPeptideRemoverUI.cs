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
  public partial class AmbigiousModifiedPeptideRemoverUI : AbstractFileProcessorUI
  {
    public static readonly string title = "Ambigious Modified Peptide Remover";

    public static readonly string version = "1.0.0";

    public AmbigiousModifiedPeptideRemoverUI()
    {
      InitializeComponent();

      SetFileArgument("PeptideFile", new OpenFileArgument("Peptides", "peptides"));

      this.Text = Constants.GetSQHTitle(title, version);
    }

    protected override IFileProcessor GetFileProcessor()
    {
      return new AmbigiousModifiedPeptideRemover();
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
        new AmbigiousModifiedPeptideRemoverUI().MyShow();
      }

      #endregion
    }
  }
}

