using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using RCPA.Gui;
using RCPA.Gui.FileArgument;
using RCPA.Gui.Command;
using System.IO;

namespace RCPA.Proteomics.Quantification.IsobaricLabelling
{
  public partial class IsobaricResultMultipleFileDistillerUI : AbstractProcessorFileUI
  {
    private static readonly string title = "Isobaric Labelling Multiple File Distiller";

    private static readonly string version = "1.4.5";

    private string[] rawExtentions = new string[] { "raw", "mzData.xml", "mzData", "mzXML", "mgf" };

    private RcpaCheckBox individual;

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

      individual = new RcpaCheckBox(cbIndividual, "Individual", false);
      AddComponent(individual);

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

      channels.PlexType = plexTypes.SelectedItem;
    }
    protected override IFileProcessor GetFileProcessor()
    {
      var reader = readers.SelectedItem;
      reader.PlexType = plexTypes.SelectedItem;

      var mPeakCount = Math.Max(1, minPeakCount.Value);

      var options = new IsobaricResultMultipleFileDistillerOptions()
      {
        Reader = reader,
        Individual = individual.Checked,
        MinPeakCount = mPeakCount,
        RawFiles = rawFiles.FileNames,
        PrecursorPPMTolerance = precursorPPMTolerance.Value,
        ProductPPMTolerance = productPPMTolerance.Value,
        RequiredChannels = channels.GetFuncs()
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
    }
    protected override string GetOriginFile()
    {
      if (individual.Checked)
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
        return MenuCommandType.Quantification_IsobaricLabelling + "_NEW";
      }

      #endregion
    }

    private void cbPlexType_SelectedValueChanged(object sender, EventArgs e)
    {
      if (plexTypes != null && plexTypes.SelectedItem != null)
      {
        channels.PlexType = plexTypes.SelectedItem;
      }
    }
  }
}
