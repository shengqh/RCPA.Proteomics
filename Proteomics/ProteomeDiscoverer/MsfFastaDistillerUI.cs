using RCPA.Gui;
using RCPA.Gui.Command;
using RCPA.Gui.FileArgument;

namespace RCPA.Proteomics.ProteomeDiscoverer
{
  public partial class MsfFastaDistillerUI : AbstractFileProcessorUI
  {
    public static string title = "Extract Protein Sequence From MSF File";
    public static string version = "1.0.0";

    public MsfFastaDistillerUI()
    {
      InitializeComponent();

      base.SetFileArgument("MSFDatabase", new OpenFileArgument("MSF Database", "msf"));

      this.Text = Constants.GetSQHTitle(title, version);
    }

    protected override IFileProcessor GetFileProcessor()
    {
      return new MsfFastaDistiller();
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
        new MsfFastaDistillerUI().MyShow();
      }

      #endregion
    }
  }
}