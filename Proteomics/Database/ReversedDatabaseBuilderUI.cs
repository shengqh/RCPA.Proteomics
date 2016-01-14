using RCPA.Gui;
using RCPA.Gui.Command;
using RCPA.Gui.FileArgument;
using System;

namespace RCPA.Proteomics.Database
{
  public partial class ReversedDatabaseBuilderUI : AbstractProcessorUI
  {
    public static string title = "Reversed Database Builder";
    public static string version = "1.1.1";

    private RcpaCheckBox reversedOnly;

    private RcpaCheckBox includeContaminantProteins;

    private RcpaFileField contaminantFile;

    private RcpaCheckBox isPseudoAminoacid;

    private RcpaTextField pseudoAminoacids;

    private RcpaComboBox<string> isPseudoForward;

    private RcpaComboBox<DecoyType> decoyTypes;

    public ReversedDatabaseBuilderUI()
    {
      InitializeComponent();

      this.sourceDatabase.FileArgument = new OpenFileArgument("Source Database Fasta", new string[] { ".fasta", ".fa" });

      reversedOnly = new RcpaCheckBox(cbReversedDatabaseOnly, "ReversedOnly", false);
      AddComponent(reversedOnly);

      includeContaminantProteins = new RcpaCheckBox(cbContaminantFile, "IncludeContaminantFile", false);
      AddComponent(includeContaminantProteins);

      contaminantFile = new RcpaFileField(btnContaminantFile, txtContaminantFile, "ContaminantFile", new OpenFileArgument("Contaminant Proteins (*.fasta)", "fasta"), false);
      btnContaminantFile.Text = "...";
      AddComponent(contaminantFile);

      isPseudoAminoacid = new RcpaCheckBox(cbSwitch, "Switch", true);
      AddComponent(isPseudoAminoacid);

      pseudoAminoacids = new RcpaTextField(txtTermini, "Aminoacids", "Protease termini", "KR", true);
      AddComponent(pseudoAminoacids);

      isPseudoForward = new RcpaComboBox<string>(cbPrior, "Forward", new string[] { "previous", "next" }, 1);
      AddComponent(isPseudoForward);

      decoyTypes = new RcpaComboBox<DecoyType>(cbDecoyType, "DecoyType", DecoyType.Items, 0);
      AddComponent(decoyTypes);

      this.Text = Constants.GetSQHTitle(title, version);
    }

    protected override void DoBeforeValidate()
    {
      base.DoBeforeValidate();

      contaminantFile.Required = includeContaminantProteins.Checked;
      pseudoAminoacids.Required = isPseudoAminoacid.Checked;
    }

    protected override IProcessor GetProcessor()
    {
      var options = new ReversedDatabaseBuilderOptions()
      {
        ReversedOnly = reversedOnly.Checked,
        IsPseudoAminoacid = isPseudoAminoacid.Checked,
        PseudoAminoacids = pseudoAminoacids.Text,
        IsPseudoForward = isPseudoForward.SelectedIndex == 0,
        ContaminantFile = includeContaminantProteins.Checked ? contaminantFile.FullName : string.Empty,
        InputFile = sourceDatabase.FullName,
        DecoyType = decoyTypes.SelectedItem
      };

      if (!options.PrepareOptions())
      {
        throw new ArgumentException(options.ParsingErrors.Merge("\n"));
      }
      else
      {
        return new ReversedDatabaseBuilder(options);
      }
    }
  }
}
