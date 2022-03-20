using RCPA.Gui.Command;

namespace RCPA.Proteomics.Quantification.ITraq
{
  public partial class ITraqProteinStatisticBuilderUI : AbstractITraqProteinStatisticBuilderUI
  {
    private static readonly string title = "Isobaric Labelling Protein Statistic Builder";
    private static readonly string version = "1.1.5";

    public ITraqProteinStatisticBuilderUI()
    {
      InitializeComponent();

      this.Text = Constants.GetSQHTitle(title, version);
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
        new ITraqProteinStatisticBuilderUI().MyShow();
      }

      #endregion

      #region IToolSecondLevelCommand Members

      public string GetSecondLevelCommandItem()
      {
        return MenuCommandType.Quantification_IsobaricLabelling;
      }

      #endregion
    }
  }
}
