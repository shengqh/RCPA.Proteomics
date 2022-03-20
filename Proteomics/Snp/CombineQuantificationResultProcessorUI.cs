using RCPA.Gui;
using RCPA.Gui.Command;
using RCPA.Gui.FileArgument;

namespace RCPA.Proteomics.Snp
{
  public partial class CombineQuantificationResultProcessorUI : AbstractFileProcessorUI
  {
    private static readonly string title = "Combination of mutation and quantification builder";
    private static readonly string version = "1.0.1";

    private RcpaFileField itraqFile;

    public CombineQuantificationResultProcessorUI()
    {
      InitializeComponent();

      base.SetFileArgument("PeptideFile", new OpenFileArgument("Single Aminoacid Polymorphism Peptides", "mut"));

      this.itraqFile = new RcpaFileField(btnDatabase, txtDatabase, "IsobaricQuantificationPeptide", new OpenFileArgument("Isobaric Unique Peptide Quantification", "unique.itraq"), true);
      AddComponent(this.itraqFile);

      this.Text = Constants.GetSQHTitle(title, version);
    }

    protected override IFileProcessor GetFileProcessor()
    {
      return new CombineQuantificationResultProcessor(itraqFile.FullName);
    }

    public class Command : IToolCommand
    {
      #region IToolCommand Members

      public string GetClassification()
      {
        return MenuCommandType.Misc;
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
        new CombineQuantificationResultProcessorUI().MyShow();
      }

      #endregion
    }
  }
}
