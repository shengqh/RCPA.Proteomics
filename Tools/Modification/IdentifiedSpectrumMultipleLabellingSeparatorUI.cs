using RCPA.Gui.Command;
using RCPA.Gui.FileArgument;
using RCPA.Tools.Summary;

namespace RCPA.Tools.Modification
{
  public partial class IdentifiedSpectrumMultipleLabellingSeparatorUI : AbstractMultipleLabellingSeparatorUI
  {
    public static readonly string title = "Separate Peptides Based on Multiple Labelling";

    public static readonly string version = "1.0.0";

    public IdentifiedSpectrumMultipleLabellingSeparatorUI()
    {
      InitializeComponent();

      this.Text = Constants.GetSQHTitle(title, version);

      SetFileArgument("PeptideFile", new OpenFileArgument("Identified Peptides", "peptides"));
    }

    protected override IFileProcessor GetFileProcessor()
    {
      return new IdentifiedSpectrumSeparatorBySpectrumFilter(GetFilterMap());
    }

    public class Command : IToolCommand
    {
      #region IToolCommand Members

      public string GetClassification()
      {
        return MenuCommandType.Modification;
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
        new IdentifiedSpectrumMultipleLabellingSeparatorUI().MyShow();
      }

      #endregion
    }
  }
}
