using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using RCPA.Gui.FileArgument;
using RCPA.Gui.Command;
using RCPA.Seq;
using RCPA.Gui;

namespace RCPA.Tools.Database
{
  public partial class ReversedDatabaseBuilderUI : AbstractFileProcessorUI
  {
    public static string title = "Reversed Database Builder";
    public static string version = "1.0.3";

    private RcpaCheckBox reversedOnly;

    private RcpaCheckBox includeContaminantProteins;

    private RcpaFileField contaminantFile;

    private RcpaCheckBox switchAminoacids;

    private RcpaTextField aminoacids;

    private RcpaComboBox<string> forward;

    public ReversedDatabaseBuilderUI()
    {
      InitializeComponent();

      SetFileArgument("Database", new OpenFileArgument("Source Database (*.fasta)", "fasta"));

      reversedOnly = new RcpaCheckBox(cbReversedDatabaseOnly, "ReversedOnly", false);
      AddComponent(reversedOnly);

      includeContaminantProteins = new RcpaCheckBox(cbContaminantFile, "IncludeContaminantFile", false);
      AddComponent(includeContaminantProteins);

      contaminantFile = new RcpaFileField(btnContaminantFile, txtContaminantFile, "ContaminantFile", new OpenFileArgument("Contaminant Proteins (*.fasta)", "fasta"), false);
      btnContaminantFile.Text = "...";
      AddComponent(contaminantFile);

      switchAminoacids = new RcpaCheckBox(cbSwitch, "Switch", true);
      AddComponent(switchAminoacids);

      aminoacids = new RcpaTextField(txtTermini, "Aminoacids", "Protease termini", "KR", true);
      AddComponent(aminoacids);

      forward = new RcpaComboBox<string>(cbPrior, "Forward", new string[] { "previous", "next" }, 1);
      AddComponent(forward);

      this.Text = Constants.GetSQHTitle(title, version);
    }

    protected override void DoBeforeValidate()
    {
      base.DoBeforeValidate();

      contaminantFile.Required = includeContaminantProteins.Checked;
      aminoacids.Required = switchAminoacids.Checked;
    }

    protected override IFileProcessor GetFileProcessor()
    {
      bool pesudo = switchAminoacids.Checked;
      string termini = aminoacids.Text;
      bool isForward = forward.SelectedIndex == 0;

      if (includeContaminantProteins.Checked)
      {
        return new ReversedDatabaseBuilder(!reversedOnly.Checked, pesudo, termini, isForward, contaminantFile.FullName);
      }
      else
      {
        return new ReversedDatabaseBuilder(!reversedOnly.Checked, pesudo, termini, isForward);
      }
    }
  }

  public class ReversedDatabaseBuilderCommand : IToolCommand
  {
    #region IToolCommand Members

    public string GetClassification()
    {
      return MenuCommandType.Database;
    }

    public string GetCaption()
    {
      return ReversedDatabaseBuilderUI.title;
    }

    public string GetVersion()
    {
      return ReversedDatabaseBuilderUI.version;
    }

    public void Run()
    {
      new ReversedDatabaseBuilderUI().MyShow();
    }

    #endregion
  }
}
