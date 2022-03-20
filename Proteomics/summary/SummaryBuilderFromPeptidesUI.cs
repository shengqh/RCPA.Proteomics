using RCPA.Gui;
using RCPA.Gui.Command;
using RCPA.Gui.FileArgument;
using RCPA.Proteomics.Summary;
using System.IO;

namespace RCPA.Tools.Summary
{
  /// <summary>
  /// 从删选完毕的肽段出发构建蛋白质列表
  /// 20100126 ver 1.0.1 
  /// 增加了对新的buildsummary参数文件的兼容性。
  /// </summary>
  public partial class SummaryBuilderFromPeptidesUI : AbstractProcessorUI
  {
    public static readonly string title = "BuildSummary - From Peptides File";

    public static readonly string version = "1.0.1";

    public SummaryBuilderFromPeptidesUI()
    {
      InitializeComponent();

      this.peptideFile.FileArgument = new OpenFileArgument("Peptides", "peptides");

      this.Text = Constants.GetSQHTitle(title, version);
    }

    protected override IProcessor GetProcessor()
    {
      var paramFile = GetParamFile();
      return new UniformSummaryBuilder(new UniformSummaryBuilderOptions()
      {
        InputFile = paramFile,
        PeptideFile = peptideFile.FullName
      });
    }

    private string GetParamFile()
    {
      var paramFile = Path.ChangeExtension(peptideFile.FullName, "param");
      return paramFile;
    }

    protected override void ValidateComponents()
    {
      base.ValidateComponents();
      string paramFile = GetParamFile();
      if (!new FileInfo(paramFile).Exists)
      {
        throw new FileNotFoundException(paramFile);
      }
    }

    public class Command : IToolCommand
    {
      #region IToolCommand Members

      public string GetClassification()
      {
        return MenuCommandType.Summary;
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
        new SummaryBuilderFromPeptidesUI().MyShow();
      }

      #endregion
    }
  }
}

