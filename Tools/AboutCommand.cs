using RCPA.Gui.Command;

namespace RCPA.Tools
{
  public class AboutCommand : IToolCommand
  {
    #region IToolCommand Members

    public string GetClassification()
    {
      return MenuCommandType.Help;
    }

    public string GetCaption()
    {
      return "About ProteomicsTools";
    }

    public string GetVersion()
    {
      return "";
    }

    public void Run()
    {
      new AboutForm().Show();
    }

    #endregion
  }
}
