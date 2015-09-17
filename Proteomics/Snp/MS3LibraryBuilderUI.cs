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
  public partial class MS3LibraryBuilderUI : AbstractProcessorUI
  {
    private static readonly string title = "MS3 Library Builder";
    private static readonly string version = "1.0.2";

    private RcpaDoubleField precursorPPM;
    private RcpaDoubleField fragmentPPM;
    private RcpaIntegerField maxFragmentPeakCount;
    private RcpaIntegerField minSpectraPerPeptide;

    public MS3LibraryBuilderUI()
    {
      InitializeComponent();

      this.Text = Constants.GetSQHTitle(title, version);

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
        MinIdentifiedSpectraPerPeptide = this.minSpectraPerPeptide.Value
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
        return version;
      }

      public void Run()
      {
        new MS3LibraryBuilderUI().MyShow();
      }

      #endregion
    }
  }
}

