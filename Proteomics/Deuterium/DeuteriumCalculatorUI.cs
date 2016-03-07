using RCPA.Commandline;
using RCPA.Gui;
using RCPA.Gui.Command;
using RCPA.Gui.FileArgument;
using System.ComponentModel;
using System.IO;

namespace RCPA.Proteomics.Deuterium
{
  public partial class DeuteriumCalculatorUI : AbstractProcessorUI
  {
    public static readonly string title = "Deuterium Calculator";

    public static readonly string version = "1.0.1";

    private RcpaFileField peptideFile;
    private RcpaDirectoryField rawDirectory;

    public DeuteriumCalculatorUI()
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

    protected override IProcessor GetProcessor()
    {
      var options = new DeuteriumCalculatorOptions()
      {
        InputFile = peptideFile.FullName,
        OutputFile = Path.ChangeExtension(peptideFile.FullName, ".deuterium.tsv"),
        RawDirectory = rawDirectory.FullName,
      };
      return new DeuteriumCalculator(options);
    }

    public class Command : AbstractCommandLineCommand<DeuteriumCalculatorOptions>, IToolCommand
    {
      public override string Name
      {
        get
        {
          return "deuterium_calc";
        }
      }

      public override string Description
      {
        get
        {
          return "Deuterium calculator";
        }
      }

      public override IProcessor GetProcessor(DeuteriumCalculatorOptions options)
      {
        return new DeuteriumCalculator(options);
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
        new DeuteriumCalculatorUI().MyShow();
      }

      #endregion
    }
  }
}

