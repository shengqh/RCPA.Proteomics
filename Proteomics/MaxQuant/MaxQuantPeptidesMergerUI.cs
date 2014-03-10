using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using RCPA.Gui;
using RCPA.Gui.Command;
using RCPA.Gui.FileArgument;
using RCPA.Proteomics.Mascot;
using RCPA.Proteomics.Summary;
using RCPA.Seq;
using RCPA.Proteomics.Sequest;

namespace RCPA.Proteomics.MaxQuant
{
  public partial class MaxQuantPeptidesMergerUI : AbstractProcessorFileUI
  {
    public static readonly string title = "MaxQuant Peptides Merger";

    public static readonly string version = "1.0.0";

    private SaveFileArgument targetFile;

    public MaxQuantPeptidesMergerUI()
    {
      InitializeComponent();

      this.peptideFiles.FileArgument = new OpenFileArgument("MaxQuant Peptide", "txt");
      AddComponent(new RcpaMultipleFileComponent(this.peptideFiles.GetItemInfos(),"PeptideFiles","Peptide Files", false, true));

      this.peptideFileBin.GetName = DoGetName;
      AddComponent(this.peptideFileBin);

      this.targetFile = new SaveFileArgument("Target Peptide", "txt");

      Text = Constants.GetSQHTitle(title, version);
    }

    protected string DoGetName(string oldName)
    {
      return new FileInfo(oldName).Name;
    }

    protected override string GetOriginFile()
    {
      FileDialog dlg = this.targetFile.GetFileDialog();
      if (dlg.ShowDialog() == DialogResult.OK)
      {
        return dlg.FileName;
      }
      else
      {
        throw new UserTerminatedException();
      }
    }

    protected override IFileProcessor GetFileProcessor()
    {
      return new MaxQuantPeptidesMerger(this.peptideFileBin.GetClassificationSet());
    }

    #region Nested type: Command

    public class Command : IToolCommand
    {
      #region IToolCommand Members

      public string GetClassification()
      {
        return MenuCommandType.MaxQuant;
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
        new MaxQuantPeptidesMergerUI().MyShow();
      }

      #endregion
    }

    #endregion

    private void peptideFileBin_GetData(object sender, EventArgs e)
    {
      List<string> filenames = this.peptideFiles.FileNames.ToList();
      filenames.Sort();
      peptideFileBin.InitializeClassificationSet(filenames);
    }
  }
}