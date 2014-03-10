using System;
using System.IO;
using System.Windows.Forms;
using RCPA.Gui;
using RCPA.Gui.Command;
using RCPA.Gui.FileArgument;
using RCPA.Proteomics.Mascot;
using RCPA.Proteomics.Summary;
using System.Collections.Generic;
using RCPA.Utils;
using RCPA.Proteomics.Summary.Uniform;

namespace RCPA.Tools.Summary
{
  /// <summary>
  /// 从删选完毕的肽段出发构建蛋白质列表
  /// 20100126 ver 1.0.1 
  /// 增加了对新的buildsummary参数文件的兼容性。
  /// </summary>
  public partial class SummaryBuilderFromPeptidesUI : AbstractFileProcessorUI
  {
    public static readonly string title = "BuildSummary - From Peptides File";

    public static readonly string version = "1.0.1";

    public SummaryBuilderFromPeptidesUI()
    {
      InitializeComponent();

      SetFileArgument("PeptideFile", new OpenFileArgument("Peptides", "peptides"));

      this.Text = Constants.GetSQHTitle(title, version);
    }

    protected override string GetOriginFile()
    {
      string peptidesFilename = originalFile.FullName;
      return FileUtils.ChangeExtension(peptidesFilename, "param");
    }

    protected override IFileProcessor GetFileProcessor()
    {
      return new UniformIdentifiedResultBuilder(originalFile.FullName);
    }

    protected override void ValidateComponents()
    {
      base.ValidateComponents();
      string paramFile = GetOriginFile();
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

