using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using RCPA.Gui;
using RCPA;
using RCPA.Gui.FileArgument;
using RCPA.Gui.Command;
using System.IO;
using RCPA.Proteomics.Quantification;
using RCPA.Proteomics.Summary;
using RCPA.Seq;

namespace RCPA.Proteomics.Snp
{
  /// <summary>
  /// 读取fasta文件和一系列的pNovo结果，根据给定的最小score进行筛选，获取完全酶解、没有miss位点、
  /// 与数据库中单位点突变的肽段，与原来的数据库构建成一个新的数据库，以便进行数据库搜索验证。
  /// </summary>
  public partial class PNovoSAPValidatorUI : AbstractProcessorUI
  {
    private static readonly string title = "pNovo SAP Validator";
    private static readonly string version = "2.1.6";

    private RcpaDoubleField minScore;
    private RcpaComboBox<ITitleParser> titleParsers;
    private RcpaComboBox<IAccessNumberParser> acParsers;
    private RcpaComboBox<string> proteases;
    private RcpaIntegerField threadCount;
    private RcpaIntegerField minLength;

    public PNovoSAPValidatorUI()
    {
      InitializeComponent();

      this.Text = Constants.GetSQHTitle(title, version);

      pNovoFiles.FileArgument = new OpenFileArgument("pNovo Result", "txt");
      AddComponent(this.pNovoFiles);

      this.minScore = new RcpaDoubleField(txtMinScore, "MinScore", "Minmum score", 0.65, true);
      AddComponent(this.minScore);

      this.threadCount = new RcpaIntegerField(txtThreadCount, "ThreadCount", "Thread count", Environment.ProcessorCount - 1, true);
      AddComponent(this.threadCount);

      toolTip1.SetToolTip(txtThreadCount, MyConvert.Format("Suggest max value = {0}", Environment.ProcessorCount + 1));

      this.titleParsers = new RcpaComboBox<ITitleParser>(cbTitleFormat, "TitleFormat", TitleParserUtils.GetTitleParsers().ToArray(), -1);
      AddComponent(this.titleParsers);

      fastaFile.FileArgument = new OpenFileArgument("Fasta To Find Mutation", "fasta");

      databaseFile.FileArgument= new OpenFileArgument("Fasta To Merge Mutated Peptide", "fasta");

      this.acParsers = new RcpaComboBox<IAccessNumberParser>(cbAccessNumberPattern, "AccessNumberParser", AccessNumberParserFactory.GetParsers().ToArray(), -1);
      AddComponent(this.acParsers);

      this.proteases = new RcpaComboBox<string>(cbProtease, "Protease", ProteaseManager.GetNames().ToArray(), -1);
      AddComponent(this.proteases);

      this.minLength = new RcpaIntegerField(txtMinLength, "MinLength", "Minimum Peptide Length", 6, true);
      AddComponent(this.minLength);
    }

    protected override IProcessor GetProcessor()
    {
      string dbFile;
      if (fastaFile.Exists)
      {
        dbFile = fastaFile.FullName;
      }
      else
      {
        dbFile = databaseFile.FullName;
      }

      var options = new PNovoSAPValidatorOptions()
      {
        PnovoFiles = pNovoFiles.SelectedFileNames,
        DatabaseFastaFile = dbFile,
        TitleParser = titleParsers.SelectedItem,
        AccessNumberParser = acParsers.SelectedItem,
        Enzyme = ProteaseManager.GetProteaseByName(proteases.SelectedItem),
        MinScore = minScore.Value,
        ThreadCount = threadCount.Value,
        IgnoreNtermMutation = ignoreNTerm.Checked,
        IgnoreDeamidatedMutation = ignoreDeamidatedMutation.Checked,
        IgnoreMultipleNucleotideMutation = ignoreMultipleNucleotideMutation.Checked,
        MinLength = minLength.Value,
        TargetDirectory = targetDirectory.FullName,
        TargetFastaFile = databaseFile.FullName
      };

      return new PNovoSAPValidator(options);
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
        new PNovoSAPValidatorUI().MyShow();
      }

      #endregion
    }
  }
}

