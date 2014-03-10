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

namespace RCPA.Tools.Quantification
{
  public partial class CensusChroFileSplitterUI : AbstractFileProcessorUI
  {
    private static readonly string title = "Census Chro File Splitter";
    private static readonly string version = "1.0.2";

    private RcpaIntegerField resultCount;

    public CensusChroFileSplitterUI()
    {
      InitializeComponent();

      base.SetFileArgument("CensusChroFile", new OpenFileArgument("Census Chro", "xml"));

      this.Text = Constants.GetSQHTitle(title, version);
      this.resultCount = new RcpaIntegerField(txtResultCount, "ResultCount", "Split Result Count", 4, true);

      this.AddComponent(resultCount);
    }

    protected override IFileProcessor GetFileProcessor()
    {
      return new CensusChroFileSplitter(resultCount.Value);
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
        new CensusChroFileSplitterUI().MyShow();
      }

      #endregion

      #region IToolSecondLevelCommand Members

      public string GetSecondLevelCommandItem()
      {
        return "Census";
      }

      #endregion
    }
  }
}

