using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using RCPA.Gui;
using RCPA.Gui.FileArgument;
using RCPA.Proteomics.Processor;
using RCPA;
using System.IO;
using RCPA.Proteomics.IO;
using RCPA.Proteomics;
using RCPA.Gui.Command;
using RCPA.Proteomics.Summary;

namespace RCPA.Proteomics.ProteomeDiscoverer
{
  public partial class MsfFileToNoredundantProcessorUI : AbstractFileProcessorUI
  {
    public static string title = "MSF To BuildSummary";
    public static string version = "1.1.1";

    private RcpaCheckBox excelFormat;

    public MsfFileToNoredundantProcessorUI()
    {
      InitializeComponent();

      base.SetFileArgument("MSFExcel", new OpenFileArgument("MSF Text/Excel/Msf", new string[] { "txt", "xls","msf" }));

      excelFormat = new RcpaCheckBox(cbExcelFormat, "ExcelFormat", false);
      AddComponent(excelFormat);

      this.Text = Constants.GetSQHTitle(title, version);
    }

    protected override IFileProcessor GetFileProcessor()
    {
      var file = GetOriginFile();
      
      if (file.ToUpper().EndsWith("MSF"))
      {
        return new MsfDatabaseToNoredundantProcessor();
      }

      if (excelFormat.Checked)
      {
        return new MsfExcelToNoredundantProcessor();
      }

      return new MsfTextToNoredundantProcessor();
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
        new MsfFileToNoredundantProcessorUI().MyShow();
      }

      #endregion
    }
  }
}