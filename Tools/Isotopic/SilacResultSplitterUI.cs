using RCPA.Gui;
using RCPA.Gui.Command;
using RCPA.Gui.FileArgument;

namespace RCPA.Tools.Isotopic
{
  public partial class SilacResultSplitterUI : AbstractFileProcessorUI
  {
    private static readonly string title = "Silac Result Light/Heavy Splitter";
    private static readonly string version = "1.0.0";

    private RcpaFileField lightFile;
    private RcpaFileField heavyFile;

    public SilacResultSplitterUI()
    {
      InitializeComponent();

      base.SetFileArgument("BuildSummaryProteinsFile", new OpenFileArgument("BuildSummary Proteins", "noredundant"));

      this.Text = Constants.GetSQHTitle(title, version);

      this.lightFile = new RcpaFileField(btnLight, txtLightFile, "Light", new OpenFileArgument("Light Sequest Param", "params"), true);
      this.heavyFile = new RcpaFileField(btnHeavy, txtHeavyFile, "Heavy", new OpenFileArgument("Heavy Sequest Param", "params"), true);
      this.AddComponent(lightFile);
      this.AddComponent(heavyFile);
    }

    protected override IFileProcessor GetFileProcessor()
    {
      return new SilacResultSplitter(lightFile.FullName, heavyFile.FullName);
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
        new SilacResultSplitterUI().MyShow();
      }

      #endregion

      #region IToolSecondLevelCommand Members

      public string GetSecondLevelCommandItem()
      {
        return "SILAC";
      }

      #endregion
    }
  }
}

