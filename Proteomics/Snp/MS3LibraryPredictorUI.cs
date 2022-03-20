using RCPA.Gui;
using RCPA.Gui.Command;
using RCPA.Gui.FileArgument;

namespace RCPA.Proteomics.Snp
{
  public partial class MS3LibraryPredictorUI : AbstractProcessorUI
  {
    private static readonly string title = "MS3 Peptide Variants Interpreter";

    private RcpaDoubleField ppmMS2Precursor;
    private RcpaDoubleField ppmMS3Precursor;
    private RcpaDoubleField ppmMS3FragmentIon;
    private RcpaIntegerField maxFragmentIonCount;
    private RcpaDoubleField minMS3PrecursorMz;
    private RcpaDoubleField minAminoacidSubstitutionDeltaMass;
    private RcpaIntegerField minMatchedMS3SpectrumCount;
    private RcpaIntegerField minMatchedMS3FragmentIonCount;

    public MS3LibraryPredictorUI()
    {
      InitializeComponent();

      this.Text = Constants.GetSQHTitle(title, MS3LibraryBuilderUI.Version);

      rawFiles.FileArgument = new OpenFileArgument("Raw", "raw");
      AddComponent(this.rawFiles);

      var dop = new MS3LibraryPredictorOptions();

      this.ppmMS2Precursor = new RcpaDoubleField(txtMS2PrecursorPPM, "MS2PrecursorPPM", "MS2 Precursor PPM", dop.MS2PrecursorPPMTolerance, true);
      AddComponent(this.ppmMS2Precursor);

      this.ppmMS3Precursor = new RcpaDoubleField(txtMS3PrecursorPPM, "MS3PrecursorPPM", "MS3 Precursor PPM", dop.MS3PrecursorPPMTolerance, true);
      AddComponent(this.ppmMS3Precursor);

      this.ppmMS3FragmentIon = new RcpaDoubleField(txtMS3FragmentPPM, "MS3FragmentIonPPM", "MS3 Fragment Ion PPM", dop.MS3FragmentIonPPMTolerance, true);
      AddComponent(this.ppmMS3FragmentIon);

      this.maxFragmentIonCount = new RcpaIntegerField(txtMaximumMS3FragmentIonCount, "MaxFragmentIonCount", "Maximum Fragment Ion Count", dop.MaximumFragmentPeakCount, true);
      AddComponent(this.maxFragmentIonCount);

      this.minMS3PrecursorMz = new RcpaDoubleField(txtMinimumMS3PrecursorMz, "MinMS3PrecursorMz", "Minimum MS3 Precursor m/z", dop.MinimumMS3PrecursorMz, true);
      AddComponent(this.minMS3PrecursorMz);

      this.minMatchedMS3SpectrumCount = new RcpaIntegerField(txtMinimumMatchedMS3SpectrumCount, "MinMatchedMS3SpectrumCount", "Minimum Matched MS3 Spectrum Count", dop.MinimumMatchedMS3SpectrumCount, true);
      AddComponent(this.minMatchedMS3SpectrumCount);

      this.minMatchedMS3FragmentIonCount = new RcpaIntegerField(txtMinimumMatchedMS3FragmentIonCount, "MinimumMatchedMS3FragmentIonCount", "Minimum Matched MS3 Fragment Ion Count", dop.MinimumMatchedMS3IonCount, true);
      AddComponent(this.minMatchedMS3FragmentIonCount);

      this.minAminoacidSubstitutionDeltaMass = new RcpaDoubleField(txtMinimumSubstitutionDeltaMass, "MinimumSubstitutionDeltaMass", "Minimum Aminoacid Substitution Delta Mass", dop.MinimumAminoacidSubstitutionDeltaMass, true);
      AddComponent(this.minAminoacidSubstitutionDeltaMass);

      this.allowTerminalLoss.Checked = dop.AllowTerminalLoss;
      this.allowTerminalExtension.Checked = dop.AllowTerminalExtension;
      this.ignoreDeamidatedMutation.Checked = dop.IgnoreDeamidatedMutation;
      this.isSingleNucleotideMutationOnly.Checked = dop.IsSingleNucleotideMutationOnly;

      libraryFile.FileArgument = new OpenFileArgument("Library", "xml");

      peptidesFile.FileArgument = new OpenFileArgument("Excluding Peptides", "peptides");

      fastaFile.FileArgument = new OpenFileArgument("Database", "fasta");

      outputFile.FileArgument = new SaveFileArgument("Output Database", "fasta");
    }

    protected override IProcessor GetProcessor()
    {
      var options = new MS3LibraryPredictorOptions()
      {
        RawFiles = this.rawFiles.FileNames,
        MinimumMS3PrecursorMz = this.minMS3PrecursorMz.Value,
        MS2PrecursorPPMTolerance = this.ppmMS2Precursor.Value,
        MS3PrecursorPPMTolerance = this.ppmMS3Precursor.Value,
        MS3FragmentIonPPMTolerance = this.ppmMS3FragmentIon.Value,
        MaximumFragmentPeakCount = this.maxFragmentIonCount.Value,
        MinimumMatchedMS3SpectrumCount = this.minMatchedMS3SpectrumCount.Value,
        MinimumMatchedMS3IonCount = this.minMatchedMS3FragmentIonCount.Value,
        MinimumAminoacidSubstitutionDeltaMass = minAminoacidSubstitutionDeltaMass.Value,
        IgnoreDeamidatedMutation = ignoreDeamidatedMutation.Checked,
        IsSingleNucleotideMutationOnly = isSingleNucleotideMutationOnly.Checked,
        AllowTerminalLoss = allowTerminalLoss.Checked,
        AllowTerminalExtension = allowTerminalExtension.Checked,
        LibraryFile = this.libraryFile.FullName,
        PeptidesFile = this.peptidesFile.FullName,
        DatabaseFastaFile = this.fastaFile.FullName,
        OutputFile = this.outputFile.FullName
      };

      return options.GetPredictor();
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
        return MS3LibraryBuilderUI.Version;
      }

      public void Run()
      {
        new MS3LibraryPredictorUI().MyShow();
      }

      #endregion
    }
  }
}

