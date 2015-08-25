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
  public partial class MS3LibraryPredictorUI : AbstractProcessorUI
  {
    private static readonly string title = "MS3 SAP Predictor";
    private static readonly string version = "1.0.0";

    private RcpaDoubleField precursorPPM;
    private RcpaDoubleField fragmentPPM;
    private RcpaIntegerField maxFragmentPeakCount;
    private RcpaDoubleField minMs3PrecursorMz;
    private RcpaIntegerField minMatchedMs3IonCount;

    public MS3LibraryPredictorUI()
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

      this.minMs3PrecursorMz = new RcpaDoubleField(txtMinMs3PrecursorMz, "MinMs3PrecursorMz", "Minimum MS3 Precursor m/z", 200, true);
      AddComponent(this.minMs3PrecursorMz);

      this.minMatchedMs3IonCount = new RcpaIntegerField(txtMinimumMatchedMs3IonCount, "MinimumMatchedMs3IonCount", "Minimum Matched MS3 Ion Count", 2, true);
      AddComponent(this.minMatchedMs3IonCount);

      peptideFile.FileArgument = new OpenFileArgument("Peptide", "peptides");

      libraryFile.FileArgument = new OpenFileArgument("Library", "xml");

      fastaFile.FileArgument = new OpenFileArgument("Database", "fasta");

      outputFile.FileArgument = new SaveFileArgument("Output Database", "fasta");
    }

    protected override IProcessor GetProcessor()
    {
      var options = new MS3LibraryPredictorOptions()
      {
        PrecursorPPMTolerance = this.precursorPPM.Value,
        FragmentPPMTolerance = this.fragmentPPM.Value,
        MaxFragmentPeakCount = this.maxFragmentPeakCount.Value,
        MinimumMs3PrecursorMz = this.minMs3PrecursorMz.Value,
        MinimumMatchedMs3IonCount = this.minMatchedMs3IonCount.Value,
        IgnoreDeamidatedMutation = ignoreDeamidatedMutation.Checked,
        IgnoreMultipleNucleotideMutation = ignoreMultipleNucleotideMutation.Checked,
        LibraryPeptideFile = this.peptideFile.FullName,
        LibraryFile = this.libraryFile.FullName,
        DatabaseFastaFile = this.fastaFile.FullName,
        RawFiles = this.rawFiles.FileNames,
        OutputFile = this.outputFile.FullName
      };

      return new MS3LibraryPredictor(options);
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
        new MS3LibraryPredictorUI().MyShow();
      }

      #endregion
    }
  }
}

