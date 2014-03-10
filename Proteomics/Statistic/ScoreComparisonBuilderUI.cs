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

namespace RCPA.Proteomics.Statistic
{
  public partial class ScoreComparisonBuilderUI : AbstractFileProcessorUI
  {
    private static readonly string title = "Score Comparison Builder";
    private static readonly string version = "1.0.0";

    private RcpaFileField targetFastaFile;
    private SaveFileArgument saveFile;

    public ScoreComparisonBuilderUI()
    {
      InitializeComponent();

      base.SetFileArgument("FirstPeptideFile", new OpenFileArgument("First Identified Peptides", "peptides"));

      this.targetFastaFile = new RcpaFileField(btnFastaFile, txtFastaFile, "SecondPeptideFile", new OpenFileArgument("Second Identified Peptides", "peptides"), true);
      AddComponent(this.targetFastaFile);

      saveFile = new SaveFileArgument("Output Data", ".txt");

      this.Text = Constants.GetSQHTitle(title, version);
    }

    protected override string GetOriginFile()
    {
      if (saveFile.GetFileDialog().ShowDialog() == System.Windows.Forms.DialogResult.OK)
      {
        return saveFile.GetFileDialog().FileName;
      }
      else
      {
        return null;
      }
    }

    protected override IFileProcessor GetFileProcessor()
    {
      var firstFile = base.GetOriginFile();
      var secondFile = targetFastaFile.FullName;

      return new ScoreComparisonBuilder(firstFile, secondFile);
    }

    public class Command : IToolCommand
    {
      #region IToolCommand Members

      public string GetClassification()
      {
        return MenuCommandType.Statistic;
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
        new ScoreComparisonBuilderUI().MyShow();
      }

      #endregion
    }
  }
}

