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
using RCPA.Proteomics.Format;

namespace RCPA.Proteomics.Format
{
  public partial class MultipleRaw2DtaProcessorUI : AbstractFileProcessorUI
  {
    public static string title = "Multiple Raw/mzData/mzXml To Dta Converter";
    public static string version = "1.1.0";

    private RcpaDoubleField minMassRange;
    private RcpaDoubleField maxMassRange;
    private RcpaDoubleField minIonIntensity;
    private RcpaIntegerField minIonCount;
    private RcpaDoubleField minTotalIonIntensity;

    public MultipleRaw2DtaProcessorUI()
    {
      InitializeComponent();

      this.Text = Constants.GetSQHTitle(title, version);

      SetDirectoryArgument("TargetDir", "Target Dta");

      this.minMassRange = new RcpaDoubleField(txtMWRangeFrom, "MWRangeFrom", "Min Mass", 600, true);
      this.maxMassRange = new RcpaDoubleField(txtMWRangeTo, "MWRangeTo", "Max Mass", 3600, true);
      this.minIonIntensity = new RcpaDoubleField(txtMinIonIntensity, "MinIonIntensity", "Min Ion Intensity", 1.0, true);
      this.minIonCount = new RcpaIntegerField(txtMinIonCount, "MinIonCount", "Min Ion Count", 15, true);
      this.minTotalIonIntensity = new RcpaDoubleField(txtMinIonIntensityThreshold, "MinTotalIonIntensity", "Min Total Ion Intensity", 100.0, true);

      this.AddComponent(minMassRange);
      this.AddComponent(maxMassRange);
      this.AddComponent(minIonIntensity);
      this.AddComponent(minIonCount);
      this.AddComponent(minTotalIonIntensity);

      this.rawFiles.FileArgument = new OpenFileArgument("Raw", new string[] { ".raw", ".mzdata", ".mzdata.xml", ".mzxml" });
    }

    protected override IFileProcessor GetFileProcessor()
    {
      PeakListMassRangeProcessor<Peak> p1 = new PeakListMassRangeProcessor<Peak>(minMassRange.Value, maxMassRange.Value, new int[] { 2, 3 });
      PeakListMinIonIntensityProcessor<Peak> p2 = new PeakListMinIonIntensityProcessor<Peak>(minIonIntensity.Value);
      PeakListMinIonCountProcessor<Peak> p3 = new PeakListMinIonCountProcessor<Peak>(minIonCount.Value);
      PeakListMinTotalIonIntensityProcessor<Peak> p4 = new PeakListMinTotalIonIntensityProcessor<Peak>(minTotalIonIntensity.Value);

      CompositeProcessor<PeakList<Peak>> p = new CompositeProcessor<PeakList<Peak>>();
      p.Add(p1);
      p.Add(p2);
      p.Add(p3);
      p.Add(p4);

      return new MultipleRaw2DtaProcessor(rawFiles.SelectedFileNames, p, cbMsLevel.Checked)
      {
        ParallelMode = cbParallel.Checked
      };
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
        new MultipleRaw2DtaProcessorUI().MyShow();
      }

      #endregion
    }
  }
}