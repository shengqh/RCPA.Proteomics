using RCPA.Gui;
using RCPA.Gui.Command;
using RCPA.Gui.FileArgument;
using RCPA.Proteomics.Distribution;

namespace RCPA.Proteomics.Statistic
{
  public partial class ProteinDistributionUI : AbstractDistributionUI
  {
    private static readonly string title = "Protein Distribution Builder";

    private static readonly string version = "1.1.0";

    private RcpaCheckBox exportPeptideCountOnly;

    public ProteinDistributionUI()
    {
      InitializeComponent();

      exportPeptideCountOnly = new RcpaCheckBox(cbPeptideCountOnly, "PeptideCountOnly", false);
      AddComponent(exportPeptideCountOnly);

      this.Text = Constants.GetSQHTitle(title, version);
    }

    protected override DistributionType GetDistributionType()
    {
      return DistributionType.Protein;
    }

    protected override OpenFileArgument GetSourceFileArgument()
    {
      return new OpenFileArgument("noredundant", "noredundant");
    }

    protected override IFileProcessor GetFileProcessor()
    {
      return new ProteinDistributionCalculator(true) { ExportPeptideCountOnly = exportPeptideCountOnly.Checked };
    }

    public class Command : IToolCommand
    {
      #region IToolCommand Members

      public string GetClassification()
      {
        return MenuCommandType.Statistic;
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
        new ProteinDistributionUI().MyShow();
      }

      #endregion
    }
  }

}
