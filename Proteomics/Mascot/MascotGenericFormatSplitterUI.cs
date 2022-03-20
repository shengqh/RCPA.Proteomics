using RCPA.Gui;
using RCPA.Gui.Command;
using RCPA.Gui.FileArgument;

namespace RCPA.Proteomics.Mascot
{
  public partial class MascotGenericFormatSplitterUI : AbstractFileProcessorUI
  {
    private RcpaDoubleField fileSize;

    private static string title = "Mascot Generic Format File Splitter";
    private static string version = "1.0.0";

    public MascotGenericFormatSplitterUI()
    {
      InitializeComponent();


      this.SetFileArgument("MGFFile", new OpenFileArgument("Mascot Generic Format", new string[] { "msm", "mgf" }));

      this.fileSize = new RcpaDoubleField(txtFileSize, "FileSize", "file size", 100.0, true);
      AddComponent(this.fileSize);

      this.Text = Constants.GetSQHTitle(title, version);
    }

    protected override IFileProcessor GetFileProcessor()
    {
      return new MascotGenericFormatSplitter(fileSize.Value);
    }

    public class Command : IToolCommand
    {
      #region IToolCommand Members

      public string GetClassification()
      {
        return MenuCommandType.Mascot;
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
        new MascotGenericFormatSplitterUI().MyShow();
      }

      #endregion
    }
  }
}
