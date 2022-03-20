using RCPA.Gui;
using RCPA.Gui.Command;
using RCPA.Gui.FileArgument;

namespace RCPA.Proteomics.Format
{
  public partial class TripleTOFTextToMGFMainProcessorUI : AbstractFileProcessorUI
  {
    private static readonly string title = "Triple TOF Peak List Text To Mascot Generic Format Converter";

    private static readonly string version = "1.0.0";

    private RcpaDoubleField precursorTolerance;

    public TripleTOFTextToMGFMainProcessorUI()
    {
      InitializeComponent();

      this.Text = Constants.GetSQHTitle(title, version);

      this.SetDirectoryArgument("TargetDir", "Target MGF");

      this.precursorTolerance = new RcpaDoubleField(txtPrecursorTolerance, "PrecursorTolerance", "Use precursor tolerance to find precursor from MS1 spectrum", 200, true);
      AddComponent(this.precursorTolerance);

      this.rawFiles.FileArgument = new OpenFileArgument("TripleTOF Peak List Text", "txt");
    }

    protected override IFileProcessor GetFileProcessor()
    {
      double dPrecursorTolerance;
      if (cbUsePrecursorTolerance.Checked)
      {
        dPrecursorTolerance = precursorTolerance.Value;
      }
      else
      {
        dPrecursorTolerance = 0.0;
      }
      return new TripleTOFTextToMGFMainProcessor(this.rawFiles.GetItemInfos().Items.GetAllItems(), this.Text, dPrecursorTolerance);
    }

    public class Command : IToolCommand
    {
      #region IToolCommand Members

      public string GetClassification()
      {
        return MenuCommandType.Format;
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
        new TripleTOFTextToMGFMainProcessorUI().MyShow();
      }

      #endregion
    }
  }
}