using RCPA.Gui;
using RCPA.Gui.Command;
using RCPA.Gui.FileArgument;

namespace RCPA.Proteomics.MaxQuant
{
  public partial class MaxQuantEvidenceToPeptideProcessorUI : AbstractProcessorUI
  {
    static string title = "MaxQuant Evidence To Peptide";
    static string version = "1.0.2";

    public MaxQuantEvidenceToPeptideProcessorUI()
    {
      InitializeComponent();

      evidenceFile.FileArgument = new OpenFileArgument("MaxQuant Evidence", "txt");

      this.Text = Constants.GetSQHTitle(title, version);
    }

    protected override IProcessor GetProcessor()
    {
      var options = new MaxQuantEvidenceToPeptideProcessorOptions()
      {
        InputFile = evidenceFile.FullName
      };
      return new MaxQuantEvidenceToPeptideProcessor(options);
    }

    #region Nested type: Command

    public class Command : IToolCommand
    {
      #region IToolCommand Members

      public string GetClassification()
      {
        return MenuCommandType.MaxQuant;
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
        new MaxQuantEvidenceToPeptideProcessorUI().MyShow();
      }
      #endregion
    }
    #endregion
  }
}
