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
using RCPA.Proteomics.Quantification;
using RCPA.Proteomics.Quantification.O18;

namespace RCPA.Proteomics.Quantification.O18
{
  public partial class O18QuantificationUserProcessorUI : AbstractFileProcessorUI
  {
    public static string title = "O18 Relative Quantification User-Specific Calculator";

    private RcpaFileField rawFile;
    private RcpaDoubleField purityOfWater;
    private RcpaDoubleField precursorPPMTolerance;
    private RcpaDoubleField precursorMz;
    private RcpaIntegerField precursorCharge;
    private RcpaTextField peptideSequence;
    private RcpaTextField modificationFormula;
    private RcpaCheckBox postDigestionLabelling;

    public O18QuantificationUserProcessorUI()
    {
      InitializeComponent();

      base.SetFileArgument("O18Result", new SaveFileArgument("O18 Ratio Result", "o18"));

      this.Text = Constants.GetSQHTitle(title, O18QuantificationUserProcessor.version);
      this.rawFile = new RcpaFileField(btnRawFile, txtRawFile, "RawFile", new OpenFileArgument("Thermo Raw", "raw"), true);
      this.purityOfWater = new RcpaDoubleField(txtPurityOfO18Water, "PurityOfO18Water", "purity of O18 water", 0.95, true);
      this.precursorPPMTolerance = new RcpaDoubleField(txtPrecursorTolerance, "PrecursorPPMTolerance", "precursor PPM tolerance", 50, true);
      this.precursorMz = new RcpaDoubleField(txtPrecursorMz, "PrecursorMz", "precursor m/z", 0.0, true);
      this.precursorCharge = new RcpaIntegerField(txtPrecursorCharge, "PrecursorCharge", "precursor charge", 2, true);
      this.peptideSequence = new RcpaTextField(txtPeptideSequence, "PeptideSequence", "peptide sequence", "", true);
      this.modificationFormula = new RcpaTextField(txtModificationFormula, "ModificationFormula", "modification formula(such like H3PO4)", "", false);
      this.postDigestionLabelling = new RcpaCheckBox(cbPostDigestionLabelling, "PostDigestionLabelling", false);

      this.AddComponent(rawFile);
      this.AddComponent(purityOfWater);
      this.AddComponent(precursorPPMTolerance);
      this.AddComponent(precursorMz);
      this.AddComponent(precursorCharge);
      this.AddComponent(peptideSequence);
      this.AddComponent(modificationFormula);
      this.AddComponent(postDigestionLabelling);
    }

    protected override IFileProcessor GetFileProcessor()
    {
      return new O18QuantificationUserProcessor(
        new O18QuantificationSummaryItemXmlFormat(),
        postDigestionLabelling.Checked,
        rawFile.FullName, 
        purityOfWater.Value, 
        peptideSequence.Text, 
        modificationFormula.Text, 
        precursorMz.Value, 
        precursorCharge.Value, 
        precursorPPMTolerance.Value);
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
        return O18QuantificationUserProcessorUI.title;
      }

      public string GetVersion()
      {
        return O18QuantificationUserProcessor.version;
      }

      public void Run()
      {
        new O18QuantificationUserProcessorUI().MyShow();
      }

      #endregion

      #region IToolSecondLevelCommand Members

      public string GetSecondLevelCommandItem()
      {
        return "O18";
      }

      #endregion
    }
  }

}

