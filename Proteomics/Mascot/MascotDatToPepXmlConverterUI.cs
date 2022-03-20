using RCPA.Gui;
using RCPA.Gui.Command;
using RCPA.Gui.FileArgument;
using RCPA.Proteomics.Summary;

namespace RCPA.Proteomics.Mascot
{
  public partial class MascotDatToPepXmlConverterUI : AbstractProcessorUI
  {
    private static string title = "Mascot Dat To PepXml Format Converter";
    private static string version = "1.0.0";

    private RcpaComboBox<ITitleParser> titleParsers;

    public MascotDatToPepXmlConverterUI()
    {
      InitializeComponent();

      this.minPeptideLength.DefaultValue = MascotDatToPepXmlConverterOptions.DEFAULT_MinPeptideLength.ToString();
      this.targetDir.SetDirectoryArgument("TargetDir", "Target PepXML");
      this.datFiles.FileArgument = new OpenFileArgument("Mascot Dat", "dat");
      AddComponent(new RcpaMultipleFileComponent(this.datFiles.GetItemInfos(), "DatFiles", "Dat Files", false, true));

      this.titleParsers = new RcpaComboBox<ITitleParser>(cbTitleFormat, "TitleFormat", TitleParserUtils.GetTitleParsers().ToArray(), 0);
      AddComponent(this.titleParsers);

      this.Text = Constants.GetSQHTitle(title, version);
    }

    protected override IProcessor GetProcessor()
    {
      return new MascotDatToPepXmlConverter(new MascotDatToPepXmlConverterOptions()
      {
        InputFiles = datFiles.FileNames,
        TitleFormat = titleParsers.SelectedItem.FormatName,
        MinPeptideLength = minPeptideLength.Value,
        OutputDirectory = targetDir.FullName
      });
    }

    public class Command : IToolCommand
    {
      #region IToolCommand Members

      public string GetClassification()
      {
        return MenuCommandType.Mascot;
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
        new MascotDatToPepXmlConverterUI().MyShow();
      }

      #endregion
    }
  }
}
