using RCPA.Gui;
using RCPA.Gui.Command;
using RCPA.Gui.FileArgument;
using System.Windows.Forms;

namespace RCPA.Tools.Quantification
{
  public partial class CensusResultFileMergerUI : AbstractFileProcessorUI
  {
    private RcpaListBoxMultipleFileField resultFiles;

    public static readonly string Title = "Census Result Files Merger";
    public static readonly string Version = "1.0.7";

    private RcpaCheckBox isLabelFree;

    private RcpaCheckBox isPeptideLevel;

    private SaveFileArgument targetFile;

    public CensusResultFileMergerUI()
    {
      InitializeComponent();

      targetFile = new SaveFileArgument("Merged Census Result", "txt");

      this.isLabelFree = new RcpaCheckBox(cbLabelFree, "IsLabelFree", false);
      AddComponent(this.isLabelFree);

      this.isPeptideLevel = new RcpaCheckBox(cbPeptideLevel, "IsPeptideLevel", false);
      AddComponent(this.isPeptideLevel);

      this.resultFiles = new RcpaListBoxMultipleFileField(btnAdd, btnRemove, null, null, null, lbResultFiles, "ResultFile", new OpenFileArgument("Census Result", "txt"), true, true);
      AddComponent(this.resultFiles);

      this.Text = Constants.GetSQHTitle(Title, Version);
    }

    protected override string GetOriginFile()
    {
      var fileDialog = targetFile.GetFileDialog();

      if (fileDialog.ShowDialog(this) == DialogResult.OK)
      {
        return fileDialog.FileName;
      }

      return null;
    }

    protected override IFileProcessor GetFileProcessor()
    {
      if (isPeptideLevel.Checked)
      {
        return new CensusResultPeptideMerger(resultFiles.SelectedFileNames, isLabelFree.Checked);
      }

      return new CensusResultProteinMerger(resultFiles.SelectedFileNames, isLabelFree.Checked);
    }
  }

  public class CensusResultFileMergerCommand : IToolSecondLevelCommand
  {
    #region IToolCommand Members

    public string GetClassification()
    {
      return MenuCommandType.Quantification;
    }

    public string GetCaption()
    {
      return CensusResultFileMergerUI.Title;
    }

    public string GetVersion()
    {
      return CensusResultFileMergerUI.Version;
    }

    public void Run()
    {
      new CensusResultFileMergerUI().MyShow();
    }

    #endregion

    #region IToolSecondLevelCommand Members

    public string GetSecondLevelCommandItem()
    {
      return "Census";
    }

    #endregion
  }
}
