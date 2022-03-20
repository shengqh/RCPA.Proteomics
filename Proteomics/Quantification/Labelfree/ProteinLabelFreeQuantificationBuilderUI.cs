using RCPA.Gui;
using RCPA.Gui.Command;
using RCPA.Gui.FileArgument;
using RCPA.Proteomics.Summary;
using RCPA.Seq;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace RCPA.Proteomics.Quantification.Labelfree
{
  /// <summary>
  /// 本程序目的是读取蛋白质鉴定文件，根据用户设定分类以及选定的定量方法，采用spectra count进行label free定量。
  /// 输出结果为：
  /// name  class1 class2 class3 ...
  /// IPI1  0.546  0.345  0.255 ...
  /// IPI2  1.493  2.344  1.945 ...
  /// 
  /// </summary>
  public partial class ProteinLabelFreeQuantificationBuilderUI : AbstractFileProcessorUI
  {
    private static readonly string title = "Protein Label Free Quantification Builder";

    private static readonly string version = "1.0.0";

    private RcpaComboBox<IProteinLabelfreeQuantificationCalculator> calculators;

    private RcpaComboBox<IAccessNumberParser> parsers;

    public ProteinLabelFreeQuantificationBuilderUI()
    {
      InitializeComponent();

      SetFileArgument("ProteinFile", new OpenFileArgument("Protein", new string[] { "noredundant", "unduplicated" }));

      calculators = new RcpaComboBox<IProteinLabelfreeQuantificationCalculator>(
        cbFilterType,
        "LabelfreeQuantificationCalculator",
        new IProteinLabelfreeQuantificationCalculator[] {
          new NSAFProteinLabelfreeQuantificationCalculator(),
          new SInProteinLabelfreeQuantificationCalculator()
        },
        0);
      AddComponent(calculators);

      this.parsers = new RcpaComboBox<IAccessNumberParser>(cbAccessNumberParser, "Parser", AccessNumberParserFactory.GetParsers().ToArray(), 0);
      AddComponent(this.parsers);

      AddComponent(pnlClassification);

      this.Text = Constants.GetSQHTitle(title, version);
    }

    private void btnLoad_Click(object sender, EventArgs e)
    {
      try
      {
        this.originalFile.ValidateComponent();
      }
      catch (Exception ex)
      {
        MessageBox.Show(this, ex.Message, "Error");
        return;
      }

      HashSet<string> experimentals = new IdentifiedResultExperimentalReader().ReadFromFile(this.originalFile.FullName);

      List<string> sortedExperimentals = new List<string>(experimentals);
      sortedExperimentals.Sort();

      if (sortedExperimentals.Count > 0)
      {
        pnlClassification.InitializeClassificationSet(sortedExperimentals);
      }
    }

    protected override IFileProcessor GetFileProcessor()
    {
      return new ProteinLabelFreeQuantificationBuilder(calculators.SelectedItem, pnlClassification.GetClassificationSet(), parsers.SelectedItem);
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
        new ProteinLabelFreeQuantificationBuilderUI().MyShow();
      }

      #endregion

      #region IToolSecondLevelCommand Members

      public string GetSecondLevelCommandItem()
      {
        return "LabelFree";
      }

      #endregion
    }
  }
}
