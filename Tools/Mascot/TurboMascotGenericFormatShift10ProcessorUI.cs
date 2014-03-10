using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using RCPA.Proteomics.Mascot;
using RCPA.Gui.Command;
using RCPA.Gui.FileArgument;
using RCPA.Gui;

namespace RCPA.Tools.Mascot
{
  public partial class TurboMascotGenericFormatShift10ProcessorUI : AbstractTurboProcessorUI
  {
    public static readonly string title = "Turbo Mascot Generic Format Precursor Shift 10Da Processor";

    public static readonly string version = "1.0.1";

    public TurboMascotGenericFormatShift10ProcessorUI()
    {
      InitializeComponent();

      base.SetFileArgument("MgfFile", new OpenFileArgument("Mascot Generic Format", new string[] { "mgf", "msm" }));

      base.SetDirectoryArgument("MgfDirectory", "MGF");

      this.Text = Constants.GetSQHTitle(title, version);
    }

    protected override IFileProcessor GetFileProcessor()
    {
      if (tcBatchMode.SelectedIndex == 0)
      {
        return new MascotGenericFormatShiftPrecursorProcessor();
      }
      else
      {
        return new MascotGenericFormatShiftPrecursorMultipleFileProcessor();
      }
    }

    public class Command : IToolCommand
    {
      #region IToolCommand Members

      public string GetClassification()
      {
        return MenuCommandType.Mascot;
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
        new TurboMascotGenericFormatShift10ProcessorUI().MyShow();
      }

      #endregion
    }
  }

}
