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

namespace RCPA.Proteomics.Distiller
{
  public partial class SnpPeptideDistillerUI : AbstractFileProcessorUI
  {
    public static readonly string title = "Snp Peptide Distiller";

    public static readonly string version = "1.0.0";

    private RcpaTextField snpPattern;

    public SnpPeptideDistillerUI()
    {
      InitializeComponent();

      SetFileArgument("PeptideFile", new OpenFileArgument("Peptide", "peptides"));

      snpPattern = new RcpaTextField(txtSnpPattern, "SnpPattern", "SNP Pattern", @"_\S+\d+\S+", true);
      AddComponent(snpPattern);

      this.Text = Constants.GetSQHTitle(title, version);
    }

    protected override IFileProcessor GetFileProcessor()
    {
      return new SnpPeptideDistiller(snpPattern.Text);
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
        new SnpPeptideDistillerUI().MyShow();
      }

      #endregion
    }
  }
}

