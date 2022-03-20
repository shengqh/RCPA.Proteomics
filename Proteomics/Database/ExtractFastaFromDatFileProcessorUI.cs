using RCPA.Gui;
using RCPA.Gui.Command;
using RCPA.Gui.FileArgument;

namespace RCPA.Proteomics.Database
{
  public partial class ExtractFastaFromDatFileProcessorUI : AbstractFileProcessorUI
  {
    public static readonly string title = "Extract Fasta From Uniport Dat File";

    public static readonly string version = "1.0.0";

    public ExtractFastaFromDatFileProcessorUI()
    {
      InitializeComponent();

      SetFileArgument("DatFile", new OpenFileArgument("Uniprot Dat", "dat"));

      this.Text = Constants.GetSQHTitle(title, version);
    }

    protected override IFileProcessor GetFileProcessor()
    {
      return new ExtractFastaFromDatFileProcessor();
    }

    public class Command : IToolCommand
    {
      #region IToolCommand Members

      public string GetClassification()
      {
        return MenuCommandType.Database;
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
        new ExtractFastaFromDatFileProcessorUI().MyShow();
      }

      #endregion
    }
  }
}

