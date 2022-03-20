using RCPA.Gui;
using RCPA.Gui.Command;
using RCPA.Gui.FileArgument;
using RCPA.Proteomics.Summary;

namespace RCPA.Proteomics.PeptideProphet
{
  public partial class UpdatePepXmlModificationProcessorUI : AbstractFileProcessorUI
  {
    private static string title = "Update PepXml ModificationInfo Processor";
    private static string version = "1.0.0";

    private RcpaComboBox<ITitleParser> titleParsers;

    public UpdatePepXmlModificationProcessorUI()
    {
      InitializeComponent();

      this.SetDirectoryArgument("TargetDir", "Target DAT");

      this.datFiles.FileArgument = new OpenFileArgument("PepXml", new string[] { "pepXml", "pep.xml" });
      AddComponent(new RcpaMultipleFileComponent(this.datFiles.GetItemInfos(), "PepXml", "PepXml Files", false, true));

      this.titleParsers = new RcpaComboBox<ITitleParser>(cbTitleFormat, "TitleFormat", TitleParserUtils.GetTitleParsers().ToArray(), 0);
      AddComponent(this.titleParsers);

      this.Text = Constants.GetSQHTitle(title, version);
    }

    protected override IFileProcessor GetFileProcessor()
    {
      return new UpdatePepXmlModificationProcessor(titleParsers.SelectedItem, datFiles.FileNames);
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
        new UpdatePepXmlModificationProcessorUI().MyShow();
      }

      #endregion
    }
  }
}
