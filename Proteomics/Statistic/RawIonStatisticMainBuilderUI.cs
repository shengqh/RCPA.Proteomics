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
using RCPA.Proteomics.Mascot;
using RCPA.Proteomics.Spectrum;
using RCPA.Proteomics.Raw;

namespace RCPA.Proteomics.Statistic
{
  public partial class RawIonStatisticMainBuilderUI : AbstractFileProcessorUI
  {
    private static readonly string title = "Raw Ion Frequency Statistic Builder";

    private static readonly string version = "1.0.5";

    private RcpaDoubleField productIonPPM;

    private RcpaDoubleField minRelativeIntensity;

    public RawIonStatisticMainBuilderUI()
    {
      InitializeComponent();

      this.Text = Constants.GetSQHTitle(title, version);

      this.SetDirectoryArgument("TargetDir", "Target");

      this.productIonPPM = new RcpaDoubleField(txtProductIonPPM, "ProductIonPPMTolerance", "Product Ion PPM Tolerance", 20, true);
      AddComponent(this.productIonPPM);

      this.minRelativeIntensity = new RcpaDoubleField (txtMinimumIonRelativeIntensity,"MinimumIonRelativeIntensity","Minimum Ion Relative Intensity",0.05, true);
      AddComponent(this.minRelativeIntensity);

      this.rawFiles.FileArgument = new OpenFileArgument("Raw", RawFileFactory.GetSupportedRawFormats());
    }

    protected override IFileProcessor GetFileProcessor()
    {
      if (cbCombine.Checked)
      {
        var options = new RawIonStatisticTaskBuilderOptions()
        {
          MinRelativeIntensity = minRelativeIntensity.Value,
          ProductIonPPM = productIonPPM.Value,
          MinFrequency = 0.05,
          TargetDirectory = GetOriginFile()
        };

        return new RawIonStatisticMultipleFileBuilder(options, this.rawFiles.FileNames);
        
      }
      else
      {
        return new RawIonStatisticMainBuilder(this.rawFiles.FileNames, productIonPPM.Value, minRelativeIntensity.Value, 0.05);
      }
    }

    public class Command : IToolCommand
    {
      #region IToolCommand Members

      public string GetClassification()
      {
        return MenuCommandType.Statistic;
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
        new RawIonStatisticMainBuilderUI().MyShow();
      }

      #endregion
    }
  }
}