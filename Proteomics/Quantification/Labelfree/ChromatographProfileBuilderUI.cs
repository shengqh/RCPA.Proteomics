using RCPA.Commandline;
using RCPA.Gui;
using RCPA.Gui.Command;
using RCPA.Gui.FileArgument;
using RCPA.Proteomics.Raw;
using RCPA.Utils;
using System.ComponentModel;
using System.IO;

namespace RCPA.Proteomics.Quantification.Labelfree
{
  public partial class ChromatographProfileBuilderUI : AbstractProcessorUI
  {
    public static readonly string title = "Chromatograph Profile Builder";

    public static readonly string version = "1.0.1";

    private RcpaFileField peptideFile;
    private RcpaDirectoryField rawDirectory;

    public ChromatographProfileBuilderUI()
    {
      InitializeComponent();

      this.peptideFile = new RcpaFileField(btnPeptideFile, txtPeptideFile, "PeptideFile", new OpenFileArgument("peptides", "peptides"), true);
      this.AddComponent(this.peptideFile);

      this.rawDirectory = new RcpaDirectoryField(btnRawDirectory, txtRawDirectory, "RawDirectory", "Raw", true);
      this.AddComponent(this.rawDirectory);

      this.Text = Constants.GetSQHTitle(title, version);
    }

    protected override ProgressChangedEventHandler GetProgressChanged()
    {
      return new WorkerProgressChangedTextBoxProxy(txtLog, new[] { progressBar }).ProgressChanged;
    }

    protected override void ValidateComponents()
    {
      base.ValidateComponents();
      ExternalProgramConfig.GetExternalProgram("R");
    }

    protected override IProcessor GetProcessor()
    {
      var options = new ChromatographProfileBuilderOptions()
      {
        InputFile = peptideFile.FullName,
        OutputFile = Path.ChangeExtension(peptideFile.FullName, ".labelfree.tsv"),
        RawFiles = Directory.GetFiles(rawDirectory.FullName, "*.raw", SearchOption.AllDirectories),
        DrawImage = rbDrawImage.Checked,
        Overwrite = rbOverwrite.Checked
      };
      return new ChromatographProfileBuilder(options);
    }

    public class Command : AbstractCommandLineCommand<ChromatographProfileBuilderOptions>, IToolCommand
    {
      public override string Name
      {
        get
        {
          return "label_free";
        }
      }

      public override string Description
      {
        get
        {
          return "Label free quantificaion";
        }
      }

      public override IProcessor GetProcessor(ChromatographProfileBuilderOptions options)
      {
        return new ChromatographProfileBuilder(options);
      }

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
        new ChromatographProfileBuilderUI().MyShow();
      }

      #endregion
    }
  }
}

