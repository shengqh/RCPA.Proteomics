using RCPA.Gui;
using RCPA.Gui.Command;

namespace RCPA.Tools.Utils
{
  public partial class ChineseFileName2PinYingProcessorUI : AbstractFileProcessorUI
  {
    public static string title = "Convert Chinese Filename To PinYin";

    public ChineseFileName2PinYingProcessorUI()
    {
      InitializeComponent();
      base.SetDirectoryArgument("Mp3Dir", "MP3");
      this.Text = Constants.GetSQHTitle(title, ChineseFileName2PinYingProcessor.version);

    }
    protected override IFileProcessor GetFileProcessor()
    {
      return new ChineseFileName2PinYingProcessor();
    }
  }

  public class ChineseFileName2PinYingProcessorCommand : IToolCommand
  {
    #region IToolCommand Members

    public string GetClassification()
    {
      return MenuCommandType.Misc;
    }

    public string GetCaption()
    {
      return ChineseFileName2PinYingProcessorUI.title;
    }

    public string GetVersion()
    {
      return ChineseFileName2PinYingProcessor.version;
    }

    public void Run()
    {
      new ChineseFileName2PinYingProcessorUI().MyShow();
    }

    #endregion
  }
}

