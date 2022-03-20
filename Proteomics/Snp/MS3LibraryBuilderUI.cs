﻿using RCPA.Gui;
using RCPA.Gui.Command;
using RCPA.Gui.FileArgument;

namespace RCPA.Proteomics.Snp
{
  public partial class MS3LibraryBuilderUI : AbstractProcessorUI
  {
    private static readonly string title = "MS3 Library Builder";
    public static readonly string Version = "1.0.8";

    private RcpaDoubleField precursorPPM;
    private RcpaDoubleField fragmentPPM;
    private RcpaIntegerField maxFragmentPeakCount;
    private RcpaIntegerField minSpectraPerPeptide;

    public MS3LibraryBuilderUI()
    {
      InitializeComponent();

      this.Text = Constants.GetSQHTitle(title, Version);

      rawFiles.FileArgument = new OpenFileArgument("Raw", "raw");
      AddComponent(this.rawFiles);

      this.precursorPPM = new RcpaDoubleField(txtPrecursorPPM, "PrecursorPPM", "Precursor PPM", 20, true);
      AddComponent(this.precursorPPM);

      this.fragmentPPM = new RcpaDoubleField(txtFragmentPPM, "FragmentPPM", "Fragment Ion PPM", 50, true);
      AddComponent(this.fragmentPPM);

      this.maxFragmentPeakCount = new RcpaIntegerField(txtMaxFragmentPeakCount, "MaxFragmentPeakCount", "Maximum Fragment Peak Count", 10, true);
      AddComponent(this.maxFragmentPeakCount);

      this.minSpectraPerPeptide = new RcpaIntegerField(txtMinIdentificationCount, "MinIdentificationCount", "Minimum Identified Spectra Per Peptide", 2, true);
      AddComponent(this.minSpectraPerPeptide);

      peptideFile.FileArgument = new OpenFileArgument("Peptide", "peptides");

      outputFile.FileArgument = new SaveFileArgument("Output Library", "xml");
    }

    protected override IProcessor GetProcessor()
    {
      string dbFile;
      if (peptideFile.Exists)
      {
        dbFile = peptideFile.FullName;
      }
      else
      {
        dbFile = outputFile.FullName;
      }

      var options = new MS3LibraryBuilderOptions()
      {
        PrecursorPPMTolerance = this.precursorPPM.Value,
        FragmentPPMTolerance = this.fragmentPPM.Value,
        MaxFragmentPeakCount = this.maxFragmentPeakCount.Value,
        PeptideFile = this.peptideFile.FullName,
        RawFiles = this.rawFiles.FileNames,
        OutputFile = this.outputFile.FullName,
        MinIdentifiedSpectraPerPeptide = this.minSpectraPerPeptide.Value,
        MaxTerminalLossLength = maxTerminalLoss.Value,
        MinSequenceLength = minSequenceLength.Value,
        Modification = txtModification.Text
      };

      return new MS3LibraryBuilder(options);
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
        return Version;
      }

      public void Run()
      {
        new MS3LibraryBuilderUI().MyShow();
      }

      #endregion
    }
  }
}

