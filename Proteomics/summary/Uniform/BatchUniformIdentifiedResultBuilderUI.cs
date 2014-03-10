using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using RCPA.Gui;
using RCPA.Gui.Command;

namespace RCPA.Proteomics.Summary.Uniform
{
  public partial class BatchUniformIdentifiedResultBuilderUI : AbstractFileProcessorUI
  {
    public static string title = "Batch BuildSummary - A general framework for assembling protein identifications";
    public static string version = "1.0.0";

    public BatchUniformIdentifiedResultBuilderUI()
    {
      InitializeComponent();

      SetDirectoryArgument("directory", "Parameter File");

      Text = Constants.GetSQHTitle(title, version);
    }

    protected override IFileProcessor GetFileProcessor()
    {
      return new BatchUniformIdentifiedResultBuilder();
    }


    #region Nested type: Command

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
        new BatchUniformIdentifiedResultBuilderUI().MyShow();
      }

      #endregion
    }

    #endregion

  }
}
