﻿using RCPA.Gui;
using RCPA.Gui.Command;
using RCPA.Gui.FileArgument;
using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;

namespace RCPA.Proteomics.Quantification.IsobaricLabelling
{
  public partial class IsobaricResultMultipleFileDistillerUI : AbstractProcessorFileUI
  {
    private static readonly string title = "Isobaric Labeling Multiple File Distiller";

    private static readonly string version = "1.4.6";

    private string[] rawExtentions = new string[] { "raw", "mzData.xml", "mzData", "mzXML", "mgf" };

    private RcpaIntegerField minPeakCount;

    private RcpaComboBox<IIsobaricRawReader> readers;

    private RcpaComboBox<IsobaricType> plexTypes;

    private RcpaDoubleField precursorPPMTolerance;

    private RcpaDoubleField productPPMTolerance;

    public IsobaricResultMultipleFileDistillerUI()
    {
      InitializeComponent();

      rawFiles.FileArgument = new OpenFileArgument("Raw Files", rawExtentions);

      RcpaMultipleFileComponent adaptor = new RcpaMultipleFileComponent(rawFiles.GetItemInfos(), "RawFiles", "Raw File", false, true);
      AddComponent(adaptor);

      minPeakCount = new RcpaIntegerField(txtMinPeakCount, "MinPeakCount", "Minmum peak count", 4, true);
      AddComponent(minPeakCount);

      plexTypes = new RcpaComboBox<IsobaricType>(cbPlexType, "PlexType", IsobaricTypeFactory.IsobaricTypes, 0);
      AddComponent(plexTypes);

      readers = new RcpaComboBox<IIsobaricRawReader>(cbScanMode, "ScanMode", new IIsobaricRawReader[] { new IsobaricRawHCDMS2Reader(), new IsobaricRawHCDMS3Reader(), new IsobaricRawHCDParallelMS3Reader(), new IsobaricRawPQDCIDReader(), new IsobaricRawPQDReader() }, 0);
      AddComponent(readers);

      precursorPPMTolerance = new RcpaDoubleField(txtPrecursorPPMTolerance, "precursorPPMTolerance", "Precursor Tolerance (ppm)", 10, true);
      AddComponent(precursorPPMTolerance);

      productPPMTolerance = new RcpaDoubleField(txtProductPPM, "productPPMTolerance", "Product Ion Tolerance (ppm)", 10, true);
      AddComponent(productPPMTolerance);

      this.Text = Constants.GetSQHTitle(title, version);
    }

    protected override void OnAfterLoadOption(EventArgs e)
    {
      base.OnAfterLoadOption(e);

      channelRequired.PlexType = plexTypes.SelectedItem;
      channelUsed.PlexType = plexTypes.SelectedItem;
    }
    protected override IFileProcessor GetFileProcessor()
    {
      var reader = readers.SelectedItem;
      reader.PlexType = plexTypes.SelectedItem;

      var mPeakCount = Math.Max(1, minPeakCount.Value);

      var options = new IsobaricResultMultipleFileDistillerOptions()
      {
        Reader = reader,
        Individual = cbIndividual.EnabledAndChecked,
        PerformMassCalibration = cbPerformMassCalibration.EnabledAndChecked,
        PerformPurityCorrection = cbPerformPurityCorrection.EnabledAndChecked,
        MinPeakCount = mPeakCount,
        RawFiles = rawFiles.FileNames,
        PrecursorPPMTolerance = precursorPPMTolerance.Value,
        ProductPPMTolerance = productPPMTolerance.Value,
        RequiredChannels = channelRequired.GetFuncs(),
        UsedChannels = channelUsed.GetFuncs()
      };

      //if (reader.PlexType == IsobaricType.PLEX8)
      //{
      //  //对于8标Isobaric，121往往会受120影响，在某些非肽段的scan中，会出现121有非常低的丰度，而其他channel没有丰度，导致做normalization的时候出错（特别是中值法）。
      //  mPeakCount = Math.Max(2, mPeakCount);
      //}

      return new IsobaricResultMultipleFileDistiller(options);
    }

    protected override ProgressChangedEventHandler GetProgressChanged()
    {
      return progress.ProgressChangedHander;
    }

    protected override void ValidateComponents()
    {
      base.ValidateComponents();

      var plexCount = plexTypes.SelectedItem.Channels.Count;
      var minCount = minPeakCount.Value;
      if (minCount > plexCount)
      {
        throw new Exception(string.Format("Invalid minimum peak count {0} for {1}", minCount, plexCount));
      }

      var used = channelUsed.GetFuncs();
      var required = channelRequired.GetFuncs();
      foreach (var req in required)
      {
        if (!used.Any(l => l.Index == req.Index))
        {
          throw new Exception(string.Format("Required channel {0} is not included in used channels", req.Name));
        }
      }
    }

    protected override string GetOriginFile()
    {
      if (cbIndividual.Checked)
      {
        return string.Empty;
      }

      if (saveDialog.ShowDialog(this) == DialogResult.OK)
      {
        return saveDialog.FileName;
      }

      return null;
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
        new IsobaricResultMultipleFileDistillerUI().MyShow();
      }

      #endregion

      #region IToolSecondLevelCommand Members

      public string GetSecondLevelCommandItem()
      {
        return MenuCommandType.Quantification_IsobaricLabelling_NEW;
      }

      #endregion
    }

    private void cbPlexType_SelectedValueChanged(object sender, EventArgs e)
    {
      if (plexTypes != null && plexTypes.SelectedItem != null)
      {
        channelRequired.PlexType = plexTypes.SelectedItem;
        channelUsed.PlexType = plexTypes.SelectedItem;
        //cbPerformMassCalibration.Enabled = !plexTypes.SelectedItem.Name.Equals("TMT10");
      }
    }
  }
}
