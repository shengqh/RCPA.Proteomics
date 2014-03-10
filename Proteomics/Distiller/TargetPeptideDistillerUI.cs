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
using System.Text.RegularExpressions;

namespace RCPA.Proteomics.Distiller
{
  public partial class TargetPeptideDistillerUI : AbstractFileProcessorUI
  {
    public static readonly string title = "Target Peptide Distiller";

    public static readonly string version = "1.0.0";

    private RcpaTextField decoyPattern;

    public TargetPeptideDistillerUI()
    {
      InitializeComponent();

      SetFileArgument("PeptideFile", new OpenFileArgument("Peptides", "peptides"));

      this.decoyPattern = new RcpaTextField(txtDecoyPattern, "DecoyPattern", "Decoy pattern", "^REV", true);
      AddComponent(this.decoyPattern);

      this.Text = Constants.GetSQHTitle(title, version);
    }

    protected override void DoBeforeValidate()
    {
      base.DoBeforeValidate();

      try
      {
        new Regex(decoyPattern.Text);
      }
      catch (Exception)
      {
        throw new Exception("Wrong regression expression format of decoy pattern.");
      }
    }

    protected override IFileProcessor GetFileProcessor()
    {
      return new TargetPeptideDistiller(decoyPattern.Text);
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
        new TargetPeptideDistillerUI().MyShow();
      }

      #endregion
    }
  }
}

