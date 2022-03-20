using RCPA.Gui;
using RCPA.Gui.Command;
using RCPA.Gui.FileArgument;

namespace RCPA.Tools.Distiller
{
  public partial class ProteinXPeptideDistillerUI : AbstractFileProcessorUI
  {
    public static readonly string title = "Extract Peptides from Protein File by Unique Peptide Count";

    public static readonly string version = "1.0.0";

    private RcpaIntegerField uniqueCount;

    private RcpaCheckBox uniqueOnly;

    public ProteinXPeptideDistillerUI()
    {
      InitializeComponent();

      this.Text = Constants.GetSQHTitle(title, version);

      SetFileArgument("ProteinFile", new OpenFileArgument("Protein", "noredundant"));

      this.uniqueCount = new RcpaIntegerField(txtUniquePeptide, "UniqueCount", "Unique Peptide Count", 2, true);

      this.uniqueOnly = new RcpaCheckBox(cbUniquePeptideOnly, "UniqueOnly", false);

      this.AddComponent(this.uniqueCount);

      this.AddComponent(this.uniqueOnly);
    }

    protected override IFileProcessor GetFileProcessor()
    {
      return new GetPeptideInUniqueProteinXProcessor(uniqueCount.Value, uniqueOnly.Checked);
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
        new ProteinXPeptideDistillerUI().MyShow();
      }

      #endregion
    }
  }
}
