using RCPA.Gui;
using RCPA.Gui.Command;
using RCPA.Gui.FileArgument;

namespace RCPA.Proteomics.Percolator
{
  public partial class MultiplePercolatorPeptideDistillerUI : AbstractProcessorUI
  {
    private static readonly string title = "Multiple Percolator Output Xml to Peptides Processor";

    private static readonly string version = "1.0.0";

    public MultiplePercolatorPeptideDistillerUI()
    {
      InitializeComponent();

      this.Text = Constants.GetSQHTitle(title, version);

      xmlFiles.FileArgument = new OpenFileArgument("Percolator Output Xml", ".output.xml");
    }

    protected override IProcessor GetProcessor()
    {
      return new MultiplePercolatorPeptideDistiller(new MultiplePercolatorPeptideDistillerOptions()
      {
        PercolatorOutputFiles = xmlFiles.FileNames,
      });
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
        new MultiplePercolatorPeptideDistillerUI().MyShow();
      }

      #endregion
    }
  }
}