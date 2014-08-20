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
using System.IO;

namespace RCPA.Proteomics.Format
{
  public partial class Mgf2Ms2ProcessorUI : AbstractFileProcessorUI
  {
    private static string title = "MGF to MS2 Converter";
    private static string version = "1.0.0";

    public Mgf2Ms2ProcessorUI()
    {
      InitializeComponent();

      this.SetFileArgument("SourceFile", new OpenFileArgument("Mascot Generic Format","mgf"));

      this.Text = Constants.GetSQHTitle(title, version);
    }

    protected override IFileProcessor GetFileProcessor()
    {
      return new Mgf2Ms2Processor(Path.ChangeExtension(GetOriginFile(), ".ms2"), new DefaultTitleParser());
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
        new Mgf2Ms2ProcessorUI().MyShow();
      }

      #endregion
    }
  }
}
