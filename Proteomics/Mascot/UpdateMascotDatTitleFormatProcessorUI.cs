using RCPA.Gui;
using RCPA.Gui.Command;
using RCPA.Gui.FileArgument;
using RCPA.Proteomics.Summary;

namespace RCPA.Proteomics.Mascot
{
  public partial class UpdateMascotDatTitleFormatProcessorUI : AbstractFileProcessorUI
  {
    private static string title = "Update Mascot Dat Title Format Processor";
    private static string version = "1.0.1";

    private RcpaComboBox<ITitleParser> titleParsers;

    public UpdateMascotDatTitleFormatProcessorUI()
    {
      InitializeComponent();

      this.SetDirectoryArgument("TargetDir", "Target DAT");

      this.datFiles.FileArgument = new OpenFileArgument("Mascot Dat", "dat");
      AddComponent(new RcpaMultipleFileComponent(this.datFiles.GetItemInfos(), "DatFiles", "Dat Files", false, true));

      this.titleParsers = new RcpaComboBox<ITitleParser>(cbTitleFormat, "TitleFormat", TitleParserUtils.GetTitleParsers().ToArray(), 0);
      AddComponent(this.titleParsers);

      this.Text = Constants.GetSQHTitle(title, version);
    }

    protected override IFileProcessor GetFileProcessor()
    {
      return new UpdateMascotDatTitleFormatProcessor(titleParsers.SelectedItem, datFiles.FileNames);
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
        new UpdateMascotDatTitleFormatProcessorUI().MyShow();
      }

      #endregion
    }
  }
}
