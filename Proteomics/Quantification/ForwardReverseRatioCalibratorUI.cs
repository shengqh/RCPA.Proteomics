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

namespace RCPA.Proteomics.Quantification
{
  public partial class ForwardReverseRatioCalibratorUI : AbstractFileProcessorUI
  {
    public static readonly string title = "Forward Reverse Ratio Calibrator";

    public static readonly string version = "1.0.0";

    public ForwardReverseRatioCalibratorUI()
    {
      InitializeComponent();

      base.SetFileArgument("SourceFile", new OpenFileArgument("Peptide Ratio", "ratio"));

      Text = Constants.GetSQHTitle(title, version);
    }

    protected override IFileProcessor GetFileProcessor()
    {
      return new ForwardReverseRatioCalibrator();
    }

    #region Nested type: Command

    public class Command : IToolCommand
    {
      #region IToolCommand Members

      public string GetClassification()
      {
        return MenuCommandType.Distiller;
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
        new ForwardReverseRatioCalibratorUI().MyShow();
      }
      #endregion
    }
    #endregion
  }
}
