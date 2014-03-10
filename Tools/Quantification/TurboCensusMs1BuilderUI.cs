using RCPA.Gui;
using RCPA.Gui.Command;
using RCPA.Gui.FileArgument;

namespace RCPA.Proteomics.Quantification
{
  public partial class TurboCensusMs1BuilderUI : AbstractTurboProcessorUI
  {
    private static readonly string title = "Turbo Raw To MS1 Builder";

    private static readonly string version = "1.0.1";

    private readonly RcpaDirectoryField targetDir;

    public TurboCensusMs1BuilderUI()
    {
      InitializeComponent();

      base.SetFileArgument("RawFile", new OpenFileArgument("Thermo Raw", "raw"));

      base.SetDirectoryArgument("RawDir", "Raw");

      this.targetDir = new RcpaDirectoryField(this.btnTargetDir, this.txtTargetDir, "TargetDir", "Target Directory", true);

      AddComponent(this.targetDir);

      this.Text = Constants.GetSQHTitle(title, version);
    }

    protected override IFileProcessor GetFileProcessor()
    {
      if (IsBatchMode())
      {
        return new Raw2Ms1DirectoryBuilder(this.targetDir.FullName, title, version);
      }

      return new Raw2Ms1FileBuilder(this.targetDir.FullName, title, version);
    }

    public class Command : IToolSecondLevelCommand
    {
      #region IToolCommand Members

      public string GetClassification()
      {
        return MenuCommandType.Quantification;
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
        new TurboCensusMs1BuilderUI().MyShow();
      }

      #endregion

      #region IToolSecondLevelCommand Members

      public string GetSecondLevelCommandItem()
      {
        return "Census";
      }

      #endregion
    }
  }
}