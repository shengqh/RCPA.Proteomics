using RCPA.Gui;
using RCPA.Gui.Command;
using RCPA.Gui.FileArgument;

namespace RCPA.Seq
{
  public partial class ShuffleDatabaseBuilderUI : AbstractFileProcessorUI
  {
    public static string title = "Shuffled Database Builder";
    public static string version = "1.0.0";

    private RcpaCheckBox reversedOnly;

    private RcpaCheckBox includeContaminantProteins;

    private RcpaFileField contaminantFile;

    private RcpaIntegerField klet;

    private RcpaIntegerField repeat;

    public ShuffleDatabaseBuilderUI()
    {
      InitializeComponent();

      SetFileArgument("Database", new OpenFileArgument("Source Database (*.fasta)", "fasta"));

      reversedOnly = new RcpaCheckBox(cbReversedDatabaseOnly, "ShuffledOnly", false);
      AddComponent(reversedOnly);

      includeContaminantProteins = new RcpaCheckBox(cbContaminantFile, "IncludeContaminantFile", false);
      AddComponent(includeContaminantProteins);

      klet = new RcpaIntegerField(txtKlet, "Klet", "Klet", 1, true);
      AddComponent(klet);

      repeat = new RcpaIntegerField(txtRepeat, "Repeat", "Repeat", 1, true);
      AddComponent(repeat);

      contaminantFile = new RcpaFileField(btnContaminantFile, txtContaminantFile, "ContaminantFile", new OpenFileArgument("Contaminant Proteins (*.fasta)", "fasta"), false);
      btnContaminantFile.Text = "...";
      AddComponent(contaminantFile);

      this.Text = Constants.GetSQHTitle(title, version);
    }

    protected override void DoBeforeValidate()
    {
      base.DoBeforeValidate();

      contaminantFile.Required = includeContaminantProteins.Checked;
    }

    protected override IFileProcessor GetFileProcessor()
    {
      if (includeContaminantProteins.Checked)
      {
        return new ShuffleDatabaseBuilder(!reversedOnly.Checked, contaminantFile.FullName, klet.Value, repeat.Value);
      }
      else
      {
        return new ShuffleDatabaseBuilder(!reversedOnly.Checked, klet.Value, repeat.Value);
      }
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
        new ShuffleDatabaseBuilderUI().MyShow();
      }

      #endregion
    }
  }
}
