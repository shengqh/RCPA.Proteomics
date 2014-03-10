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

namespace RCPA.Tools.Quantification
{
  public partial class DtaselectFileSplitterUI : AbstractFileProcessorUI
  {
    public static readonly string Title = "Dtaselect File Splitter";
    public static readonly string Version = "1.0.1";

    private RcpaIntegerField resultCount;

    public DtaselectFileSplitterUI()
    {
      InitializeComponent();

      base.SetFileArgument("Dtaselect", new OpenFileArgument("Dtaselect", "txt"));

      this.Text = Constants.GetSQHTitle(Title, Version);
      this.resultCount = new RcpaIntegerField(txtResultCount, "ResultCount", "Split Result Count", 4, true);

      this.AddComponent(resultCount);
    }

    protected override IFileProcessor GetFileProcessor()
    {
      return new DtaselectFileSplitter(resultCount.Value);
    }
  }

  public class DtaselectFileSplitterCommand : IToolSecondLevelCommand
  {
    #region IToolCommand Members

    public string GetClassification()
    {
      return MenuCommandType.Quantification;
    }

    public string GetCaption()
    {
      return DtaselectFileSplitterUI.Title;
    }

    public string GetVersion()
    {
      return DtaselectFileSplitterUI.Version;
    }

    public void Run()
    {
      new DtaselectFileSplitterUI().MyShow();
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

