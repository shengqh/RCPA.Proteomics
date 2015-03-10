using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using RCPA.Gui;
using RCPA;
using RCPA.Gui.FileArgument;
using RCPA.Gui.Command;
using System.IO;
using RCPA.Proteomics.Quantification;
using RCPA.Proteomics.Summary;
using RCPA.Seq;

namespace RCPA.Proteomics.Snp
{
  /// <summary>
  /// 读取肽段文件，对每个肽段进行氨基酸插入，生成新的fasta库。
  /// </summary>
  public partial class AminoacidInsertionBuilderUI : AbstractProcessorUI
  {
    private static readonly string title = "Aminoacid Insertion Builder";
    private static readonly string version = "1.0.0";

    private RcpaFileField peptideFile;
    private RcpaFileField databaseFile;
    private RcpaFileField outputFile;

    public AminoacidInsertionBuilderUI()
    {
      InitializeComponent();

      this.Text = Constants.GetSQHTitle(title, version);

      this.peptideFile = new RcpaFileField(btnPeptideFile, txtPeptideFile, "PeptideFile", new OpenFileArgument("Peptide", "peptides"), true);
      AddComponent(this.peptideFile);

      this.databaseFile = new RcpaFileField(btnDatabaseFile, txtDatabaseFile, "DatabaseFile", new OpenFileArgument("Database", new[] { "fa", "fasta" }), true);
      AddComponent(this.databaseFile);

      this.outputFile = new RcpaFileField(btnOutputFile, txtOutputFile, "OutputFile", new SaveFileArgument("Output Database", "fasta"), true);
      AddComponent(this.outputFile);

    }

    protected override IProcessor GetProcessor()
    {
      return new AminoacidInsertionBuilder(new AminoacidInsertionBuilderOptions()
      {
        PeptideFile = this.peptideFile.FullName,
        DatabaseFile = this.databaseFile.FullName,
        OutputFile = this.outputFile.FullName,
        GenerateReversedPeptide = this.rbReversed.Checked

      });
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
        new AminoacidInsertionBuilderUI().MyShow();
      }

      #endregion
    }
  }
}

