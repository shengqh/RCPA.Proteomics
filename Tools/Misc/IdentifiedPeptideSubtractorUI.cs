using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using RCPA.Gui.FileArgument;
using RCPA.Gui;
using RCPA.Gui.Command;
using RCPA.Proteomics.Summary;

namespace RCPA.Tools.Misc
{
  public partial class IdentifiedPeptideSubtractorUI : AbstractFileProcessorUI
  {
    public static readonly string title = "Peptide Substractor";

    public static readonly string version = "1.0.1";

    private RcpaFileField sourceFile;

    public IdentifiedPeptideSubtractorUI()
    {
      InitializeComponent();

      this.Text = Constants.GetSQHTitle(title, version);

      SetFileArgument("SubtractFile", new OpenFileArgument("Subtract Peptide", "peptides"));

      this.sourceFile = new RcpaFileField(btnSource, txtSource, "SourceFile", new OpenFileArgument("Source Peptide", "peptides"),true);
      AddComponent(this.sourceFile);
    }

    protected override IFileProcessor GetFileProcessor()
    {
      return new IdentifiedPeptideSubtractor(this.sourceFile.FullName);
    }

    public class Command : IToolCommand
    {
      #region IToolCommand Members

      public string GetClassification()
      {
        return MenuCommandType.Misc;
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
        new IdentifiedPeptideSubtractorUI().MyShow();
      }

      #endregion
    }
  }
}
