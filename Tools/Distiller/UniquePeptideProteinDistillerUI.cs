using RCPA.Gui;
using RCPA.Gui.Command;
using RCPA.Gui.FileArgument;

namespace RCPA.Tools.Distiller
{
  public partial class UniquePeptideProteinDistillerUI : AbstractFileProcessorUI
  {
    public static readonly string title = "Identified Protein Unique Peptide Distiller";

    public static readonly string version = "1.0.0";

    public UniquePeptideProteinDistillerUI()
    {
      InitializeComponent();

      SetFileArgument("ProteinFile", new OpenFileArgument("Protein", "noredundant"));

      this.Text = Constants.GetSQHTitle(title, version);
    }

    protected override IFileProcessor GetFileProcessor()
    {
      return new UniquePeptideProteinDistiller();
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
        new UniquePeptideProteinDistillerUI().MyShow();
      }

      #endregion
    }
  }
}

