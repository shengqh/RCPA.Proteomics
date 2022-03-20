using RCPA.Gui;
using RCPA.Gui.Command;
using RCPA.Gui.FileArgument;

namespace RCPA.Tools.Format
{
  public partial class IdentifiedResult2DtaSelectProcessorUI : AbstractFileProcessorUI
  {
    public static readonly string title = "IdentifiedResult To Dtaselect Converter";
    public static readonly string version = "1.0.0";

    public IdentifiedResult2DtaSelectProcessorUI()
    {
      InitializeComponent();

      base.SetFileArgument("Noredundant", new OpenFileArgument("IdentifiedResult", new string[] { "noredundant" }));

      this.Text = Constants.GetSQHTitle(title, version);
    }

    protected override IFileProcessor GetFileProcessor()
    {
      return new IdentifiedResult2DtaSelectProcessor();
    }
  }

  public class IdentifiedResult2DtaSelectProcessorCommand : IToolCommand
  {
    #region IToolCommand Members

    public string GetClassification()
    {
      return MenuCommandType.Format;
    }

    public string GetCaption()
    {
      return IdentifiedResult2DtaSelectProcessorUI.title;
    }

    public string GetVersion()
    {
      return IdentifiedResult2DtaSelectProcessorUI.version;
    }

    public void Run()
    {
      new IdentifiedResult2DtaSelectProcessorUI().MyShow();
    }

    #endregion
  }
}

