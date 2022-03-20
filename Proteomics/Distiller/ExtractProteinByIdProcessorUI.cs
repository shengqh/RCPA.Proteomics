using RCPA.Gui;
using RCPA.Gui.Command;
using RCPA.Gui.FileArgument;

namespace RCPA.Proteomics.Distiller
{
  public partial class ExtractProteinByIdProcessorUI : AbstractFileProcessorUI
  {
    public static readonly string title = "Extract Protein By Access Number";

    public static readonly string version = "1.0.0";

    private RcpaFileField acFile;

    public ExtractProteinByIdProcessorUI()
    {
      InitializeComponent();

      SetFileArgument("AccessNumberFile", new OpenFileArgument("Access Number", "txt"));

      this.acFile = new RcpaFileField(btnAccessNumberFile, txtAccessNumberFile, "NoredundantFile", new OpenFileArgument("noredundant", "noredundant"), true);
      this.AddComponent(this.acFile);

      this.Text = Constants.GetSQHTitle(title, version);
    }

    protected override IFileProcessor GetFileProcessor()
    {
      return new ExtractProteinByIdProcessor(acFile.FullName);
    }

    public class Command : IToolCommand
    {
      #region IToolCommand Members

      public string GetClassification()
      {
        return MenuCommandType.Distiller;
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
        new ExtractProteinByIdProcessorUI().MyShow();
      }

      #endregion
    }
  }
}

