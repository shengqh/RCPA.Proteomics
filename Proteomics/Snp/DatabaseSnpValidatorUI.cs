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

namespace RCPA.Proteomics.Snp
{
  public partial class DatabaseSnpValidatorUI : AbstractFileProcessorUI
  {
    private static readonly string title = "Database SAP Validator";
    private static readonly string version = "1.0.1";

    private RcpaFileField fastaFile;

    public DatabaseSnpValidatorUI()
    {
      InitializeComponent();

      base.SetFileArgument("PeptideFile", new OpenFileArgument("Peptides", "peptides"));

      this.fastaFile = new RcpaFileField(btnDatabase, txtDatabase, "Database", new OpenFileArgument("Protein Fasta", "fasta"), true);
      AddComponent(this.fastaFile);

      this.Text = Constants.GetSQHTitle(title, version);
    }

    protected override IFileProcessor GetFileProcessor()
    {
      return new DatabaseSnpValidator(fastaFile.FullName);
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
        new DatabaseSnpValidatorUI().MyShow();
      }

      #endregion
    }
  }
}
