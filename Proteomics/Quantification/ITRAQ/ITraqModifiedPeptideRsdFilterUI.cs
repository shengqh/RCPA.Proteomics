using RCPA.Gui;
using RCPA.Gui.Command;
using RCPA.Gui.FileArgument;
using RCPA.Proteomics.Quantification.ITraq;
using System;

namespace RCPA.Tools.Quantification
{
  public partial class ITraqPeptideRsdFilterUI : AbstractFileProcessorUI
  {
    private static readonly string title = "iTRAQ Peptide Rsd Filter";
    private static readonly string version = "1.0.1";

    private RcpaFileField iTraqFile;
    private RcpaTextField sampleIndecies;
    private RcpaTextField referenceIndecies;
    private RcpaTextField modifiedAminoacids;
    private RcpaDoubleField rsd;
    private RcpaDoubleField fold;

    public ITraqPeptideRsdFilterUI()
    {
      InitializeComponent();

      base.SetFileArgument("PeptidesFile", new OpenFileArgument("Peptides", "peptides"));

      this.iTraqFile = new RcpaFileField(btnRLocation, txtRLocation, "ITraqFile", new OpenFileArgument("iTRAQ", "itraq"), true);
      this.AddComponent(this.iTraqFile);

      Func<string, bool> validator = (m =>
      {
        string[] parts = m.Split(new char[] { ',' });
        if (parts.Length != 2)
        {
          return false;
        }

        int intResult;
        return int.TryParse(parts[0].Trim(), out intResult) && int.TryParse(parts[1].Trim(), out intResult);
      });

      this.referenceIndecies = new RcpaTextField(txtReferences, "referenceIndecies", "Reference Indecies", "114,115", true) { ValidateFunc = validator };
      this.AddComponent(referenceIndecies);

      this.sampleIndecies = new RcpaTextField(txtSamples, "sampleIndecies", "Sample Indecies", "116,117", true) { ValidateFunc = validator };
      this.AddComponent(sampleIndecies);

      this.modifiedAminoacids = new RcpaTextField(txtModifiedAminoacids, "ModifiedAminoacids", "Modified Aminoacids", "STY", false);
      this.AddComponent(modifiedAminoacids);

      this.rsd = new RcpaDoubleField(txtMaxRsd, "RSD", "Max RSD in Group", 0.4, true);
      this.AddComponent(rsd);

      fold = new RcpaDoubleField(txtFold, "Fold", "Significance Tolerance (fold)", 2, true);
      AddComponent(fold);

      this.Text = Constants.GetSQHTitle(title, version);
    }

    protected override bool IsProcessorSupportProgress()
    {
      return false;
    }

    protected override IFileProcessor GetFileProcessor()
    {
      string[] parts = txtReferences.Text.Split(new char[] { ',' });
      int normal1 = Convert.ToInt32(parts[0].Trim());
      int normal2 = Convert.ToInt32(parts[1].Trim());

      parts = txtSamples.Text.Split(new char[] { ',' });
      int phos1 = Convert.ToInt32(parts[0].Trim());
      int phos2 = Convert.ToInt32(parts[1].Trim());

      return new ITraqModifiedPeptideRsdFilter(iTraqFile.FullName, modifiedAminoacids.Text, new ITraqRatioCalculatorWithRsdFilter(phos1, phos2, normal1, normal2, rsd.Value), fold.Value);
    }

    public class Command : IToolSecondLevelCommand
    {
      #region IToolCommand Members

      public string GetClassification()
      {
        return MenuCommandType.Quantification;
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
        new ITraqPeptideRsdFilterUI().MyShow();
      }

      #endregion

      #region IToolSecondLevelCommand Members

      public string GetSecondLevelCommandItem()
      {
        return "ITRAQ";
      }

      #endregion
    }
  }
}
