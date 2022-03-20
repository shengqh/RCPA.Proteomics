using RCPA.Gui;
using RCPA.Gui.Command;
using RCPA.Gui.FileArgument;
using RCPA.Proteomics.Mascot;

namespace RCPA.Tools.Mascot
{
  public partial class MascotResultHtml2TextProcessorUI : AbstractFileProcessorUI
  {
    public const string title = "MascotResult (Peptide Format) Html To Text Converter";
    public const string version = "1.0.0";

    public MascotResultHtml2TextProcessorUI()
    {
      InitializeComponent();

      base.SetFileArgument("HtmlFile", new OpenFileArgument("MascotResult (Peptide Format) Html", new string[] { "htm", "html" }));

      this.Text = Constants.GetSQHTitle(title, version);
    }

    protected override IFileProcessor GetFileProcessor()
    {
      return new MascotResultHtml2TextProcessor();
    }
  }

  public class MascotResultHtml2TextProcessorCommand : IToolCommand
  {
    #region IToolCommand Members

    public string GetClassification()
    {
      return MenuCommandType.Mascot;
    }

    public string GetCaption()
    {
      return MascotResultHtml2TextProcessorUI.title;
    }

    public string GetVersion()
    {
      return MascotResultHtml2TextProcessorUI.version;
    }

    public void Run()
    {
      new MascotResultHtml2TextProcessorUI().MyShow();
    }

    #endregion
  }
}

