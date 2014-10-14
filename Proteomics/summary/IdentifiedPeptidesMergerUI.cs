using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using RCPA.Gui;
using RCPA.Gui.Command;
using RCPA.Gui.FileArgument;
using RCPA.Proteomics.Mascot;
using RCPA.Proteomics.Summary;
using RCPA.Seq;
using RCPA.Proteomics.Sequest;

namespace RCPA.Tools.Summary
{
  public partial class IdentifiedPeptidesMergerUI : AbstractFileProcessorUI
  {
    public static readonly string title = "Identified Peptide Merger";

    public static readonly string version = "1.0.0";

    private RcpaFileField targetFile;

    public IdentifiedPeptidesMergerUI()
    {
      InitializeComponent();

      this.peptideFiles.FileArgument = new OpenFileArgument("Identified Peptide", "peptides");
      AddComponent(new RcpaMultipleFileComponent(this.peptideFiles.GetItemInfos(),"PeptideFiles","Peptide Files", false, true));

      this.targetFile = new RcpaFileField(btnOriginalFile, txtOriginalFile, "TargetFile", new SaveFileArgument("Target Peptides", "peptides"), false);

      Text = Constants.GetSQHTitle(title, version);
    }

    protected override string GetOriginFile()
    {
      FileDialog dlg = this.targetFile.GetDialog();
      if (dlg.ShowDialog() == DialogResult.OK)
      {
        this.targetFile.FullName = dlg.FileName;
        return dlg.FileName;
      }
      else
      {
        throw new UserTerminatedException();
      }
    }

    protected override IFileProcessor GetFileProcessor()
    {
      return new IdentifiedPeptidesMerger(this.peptideFiles.FileNames);
    }

    #region Nested type: Command

    public class Command : IToolCommand
    {
      #region IToolCommand Members

      public string GetClassification()
      {
        return MenuCommandType.Summary;
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
        new IdentifiedPeptidesMergerUI().MyShow();
      }

      #endregion
    }

    #endregion
  }
}