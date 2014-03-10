using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using RCPA.Gui;
using RCPA.Gui.FileArgument;
using RCPA.Gui.Command;
using RCPA.Proteomics.Summary;

namespace RCPA.Proteomics.Format
{
  public partial class BuildSummaryWebFormatConverterUI : AbstractFileProcessorUI
  {
    private static string title = "BuildSummary Web Format Converter";
    private static string version = "1.0.0";

    public BuildSummaryWebFormatConverterUI()
    {
      InitializeComponent();

      this.SetFileArgument("SourceFile", new OpenFileArgument("BuildSummary Noredundant","noredundant"));

      this.Text = Constants.GetSQHTitle(title, version);
    }

    protected override IFileProcessor GetFileProcessor()
    {
      return new BuildSummaryWebFormatConverter();
    }

    public class Command : IToolCommand
    {
      #region IToolCommand Members

      public string GetClassification()
      {
        return MenuCommandType.Format;
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
        new BuildSummaryWebFormatConverterUI().MyShow();
      }

      #endregion
    }
  }
}
