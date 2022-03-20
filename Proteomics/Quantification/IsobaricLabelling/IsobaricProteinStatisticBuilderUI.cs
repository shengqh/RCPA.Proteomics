using RCPA.Gui;
using RCPA.Gui.Command;
using RCPA.Gui.FileArgument;

namespace RCPA.Proteomics.Quantification.IsobaricLabelling
{
  public partial class IsobaricProteinStatisticBuilderUI : AbstractProcessorUI
  {
    private static readonly string title = "Isobaric Labeling Protein Statistic Builder";
    private static readonly string version = "1.2.2";

    private RcpaFileField proteinFile;
    private RcpaFileField designFile;
    private RcpaFileField quanPeptideFile;
    private RcpaComboBox<string> methods;

    public IsobaricProteinStatisticBuilderUI()
    {
      InitializeComponent();

      this.proteinFile = new RcpaFileField(btnProteinFile, txtProteinFile, "ProteinFile", new OpenFileArgument("Proteins", "noredundant"), true);
      this.AddComponent(this.proteinFile);

      this.designFile = new RcpaFileField(btnDesignFile, txtIsobaricXmlFile, "IsobaricDesignFile", new OpenFileArgument("Isobaric Labeling Experimental Design", "experimental.xml"), true);
      this.AddComponent(this.designFile);

      this.quanPeptideFile = new RcpaFileField(btnQuanPeptideFile, txtQuanPeptideFile, "QuanPeptideFile", new OpenFileArgument("Quantified Peptide", "quan.tsv"), true);
      this.AddComponent(this.quanPeptideFile);

      this.methods = new RcpaComboBox<string>(cbRatioCalculator, "PeptideToProteinMethod", new[] { "Median", "Sum" }, 0, true, "How to calculate protein ratio from peptide?");
      this.AddComponent(methods);

      this.Text = Constants.GetSQHTitle(title, version);
    }

    protected override void ValidateComponents()
    {
      base.ValidateComponents();
      new IsobaricLabelingExperimentalDesign().LoadFromFile(designFile.FullName);
    }

    protected override IProcessor GetProcessor()
    {
      var option = GetStatisticOption();

      return new IsobaricProteinStatisticBuilder(option);
    }

    protected IsobaricProteinStatisticBuilderOptions GetStatisticOption()
    {
      var option = new IsobaricProteinStatisticBuilderOptions();

      option.ProteinFileName = proteinFile.FullName;
      option.ExpermentalDesignFile = designFile.FullName;
      option.QuanPeptideFileName = quanPeptideFile.FullName;
      option.PeptideToProteinMethod = methods.SelectedItem;

      return option;
    }

    public class Command : IToolSecondLevelCommand
    {
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
        new IsobaricProteinStatisticBuilderUI().MyShow();
      }

      #endregion

      #region IToolSecondLevelCommand Members

      public string GetSecondLevelCommandItem()
      {
        return MenuCommandType.Quantification_IsobaricLabelling_NEW;
      }

      #endregion
    }
  }
}
