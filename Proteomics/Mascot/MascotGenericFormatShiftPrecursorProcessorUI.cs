using RCPA.Gui;
using RCPA.Gui.Command;
using RCPA.Gui.FileArgument;
using RCPA.Proteomics.Summary;

namespace RCPA.Proteomics.Mascot
{
  public partial class MascotGenericFormatShiftPrecursorProcessorUI : AbstractProcessorUI
  {
    public static string Title = "Mascot Generic Format File Precursor Shifting Processor";
    public static string Version = "1.2.0";

    private RcpaComboBox<ITitleParser> titleParsers;

    public MascotGenericFormatShiftPrecursorProcessorUI()
    {
      InitializeComponent();

      this.datFiles.FileArgument = new OpenFileArgument("Mascot MGF", new string[] { "mgf", "msm" });
      AddComponent(new RcpaMultipleFileComponent(this.datFiles.GetItemInfos(), "MGFFiles", "Mgf Files", false, true));

      this.titleParsers = new RcpaComboBox<ITitleParser>(cbTitleFormat, "TitleFormat", TitleParserUtils.GetTitleParsers().ToArray(), 0);
      AddComponent(this.titleParsers);

      this.targetDirectory.SetDirectoryArgument("TargetDirectory", "Target");

      this.Text = Constants.GetSQHTitle(Title, Version);
    }

    protected override IProcessor GetProcessor()
    {
      return new MascotGenericFormatShiftPrecursorProcessor(new MascotGenericFormatShiftPrecursorProcessorOptions()
      {
        InputFiles = this.datFiles.FileNames,
        OutputDirectory = targetDirectory.FullName,
        ShiftMass = shiftMass.Value,
        ShiftScan = shiftScan.Value,
        TitleFormat = titleParsers.SelectedItem.FormatName
      });
    }
  }
}
