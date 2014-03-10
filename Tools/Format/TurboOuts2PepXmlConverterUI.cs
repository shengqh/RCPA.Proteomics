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
using RCPA.Gui.Command;

namespace RCPA.Tools.Format
{
  public partial class TurboOuts2PepXmlConverterUI : AbstractTurboProcessorUI
  {
    private static readonly string title = "Turbo Outs To PepXml Converter";
    private static readonly string version = "1.0.2";

    private RcpaDirectoryField targetDir;

    public TurboOuts2PepXmlConverterUI()
    {
      InitializeComponent();

      this.Text = Constants.GetSQHTitle(title, version);

      this.targetDir = new RcpaDirectoryField(btnOutsFile, txtOutsFile, "TargetDir", "Target PepXml", true);
      this.AddComponent(targetDir);

      base.SetFileArgument("OutsFile", new OpenFileArgument("Sequest Outs", "outs"));
      base.SetDirectoryArgument("OutsDir", "Outs");
    }

    protected override IFileProcessor GetFileProcessor()
    {
      if (IsBatchMode())
      {
        return new Outs2PepXmlDirectoryProcessor(targetDir.FullName);
      }
      else
      {
        return new Outs2PepXmlProcessor(targetDir.FullName);
      }
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
        new TurboOuts2PepXmlConverterUI().MyShow();
      }

      #endregion
    }
  }
}