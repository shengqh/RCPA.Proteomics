using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Linq;
using RCPA.Gui;
using RCPA.Gui.FileArgument;
using RCPA.Proteomics.Sequest;
using RCPA.Proteomics.Summary;
using RCPA.Gui.Command;
using RCPA.Proteomics.Distribution;
using RCPA.Proteomics.Raw;

namespace RCPA.Proteomics.Statistic
{
  public partial class ScanCountCalculatorUI : AbstractFileProcessorUI
  {
    private static readonly string title = "Scan Count Calculator";

    private static readonly string version = "1.0.1";

    public ScanCountCalculatorUI()
    {
      InitializeComponent();

      this.Text = Constants.GetSQHTitle(title, version);

      this.SetDirectoryArgument("TargetDir", "Target");

      this.rawFiles.FileArgument = new OpenFileArgument("Raw", RawFileFactory.GetSupportedRawFormats());
    }

    protected override IFileProcessor GetFileProcessor()
    {
      return new ScanCountMainCalculator(this.rawFiles.FileNames, this.cbMergeResult.Checked);
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
        new ScanCountCalculatorUI().MyShow();
      }

      #endregion
    }
  }

}
