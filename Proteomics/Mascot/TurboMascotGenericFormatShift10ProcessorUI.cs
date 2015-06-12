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

namespace RCPA.Proteomics.Mascot
{
  public partial class TurboMascotGenericFormatShift10ProcessorUI : AbstractTurboProcessorUI
  {
    public static readonly string title = "Turbo Mascot Generic Format Precursor Shift Processor";

    public static readonly string version = "1.0.2";

    private RcpaDoubleField shift;

    public TurboMascotGenericFormatShift10ProcessorUI()
    {
      InitializeComponent();

      this.shift = new RcpaDoubleField(txtShift, "ShiftMass", "Shift Mass", 10, true);
      AddComponent(this.shift);

      base.SetFileArgument("MgfFile", new OpenFileArgument("Mascot Generic Format", new string[] { "mgf", "msm" }));

      base.SetDirectoryArgument("MgfDirectory", "MGF");

      this.Text = Constants.GetSQHTitle(title, version);
    }

    protected override IFileProcessor GetFileProcessor()
    {
      if (tcBatchMode.SelectedIndex == 0)
      {
        return new MascotGenericFormatShiftPrecursorProcessor(this.shift.Value);
      }
      else
      {
        return new MascotGenericFormatShiftPrecursorMultipleFileProcessor(this.shift.Value);
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
