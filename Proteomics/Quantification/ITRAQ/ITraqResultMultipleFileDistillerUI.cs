using RCPA.Gui;
using RCPA.Gui.Command;
using RCPA.Gui.FileArgument;
using System;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;

namespace RCPA.Proteomics.Quantification.ITraq
{
  public partial class ITraqResultMultipleFileDistillerUI : AbstractProcessorFileUI
  {
    private static readonly string title = "Isobaric Labelling Multiple File Distiller";

    private static readonly string version = "1.4.4";

    private string[] rawExtentions = new string[] { "raw", "mzData.xml", "mzData", "mzXML", "mgf" };

    private RcpaCheckBox individual;

    private RcpaFileField2<ComboBox> isotopeFile;

    private RcpaIntegerField minPeakCount;

    private RcpaComboBox<IITraqRawReader> readers;

    private RcpaComboBox<IITraqNormalizationBuilder> normalizationBuilders;

    private RcpaCheckBox normalizationByIonInjectionTime;

    private RcpaComboBox<IsobaricType> plexTypes;

    private RcpaDoubleField precursorPPMTolerance;

    private string[] plexFiles;

    public ITraqResultMultipleFileDistillerUI()
    {
      InitializeComponent();

      rawFiles.FileArgument = new OpenFileArgument("Raw Files", rawExtentions);

      RcpaMultipleFileComponent adaptor = new RcpaMultipleFileComponent(rawFiles.GetItemInfos(), "RawFiles", "Raw File", false, true);
      AddComponent(adaptor);

      individual = new RcpaCheckBox(cbIndividual, "Individual", false);
      AddComponent(individual);

      minPeakCount = new RcpaIntegerField(txtMinPeakCount, "MinPeakCount", "Minmum peak count", 4, true);
      AddComponent(minPeakCount);

      plexFiles = new string[]{
        new FileInfo(Application.ExecutablePath).Directory.FullName + "\\itraq-4plex.csv",
        new FileInfo(Application.ExecutablePath).Directory.FullName + "\\itraq-8plex.csv",
        new FileInfo(Application.ExecutablePath).Directory.FullName + "\\tmt-6plex.csv"};

      plexTypes = new RcpaComboBox<IsobaricType>(cbPlexType, "PlexType", EnumUtils.EnumToArray<IsobaricType>(), 0);
      AddComponent(plexTypes);

      txtIsotopeFileName.Items.AddRange(plexFiles);
      isotopeFile = new RcpaFileField2<ComboBox>(btnIsotopeFile, txtIsotopeFileName, "IsotopeFile", new OpenFileArgument("Isotope Impurity Correction Table", "csv"), plexFiles[0], true, m => m.Text, (m, value) => m.Text = value);
      AddComponent(isotopeFile);

      readers = new RcpaComboBox<IITraqRawReader>(cbScanMode, "ScanMode", new IITraqRawReader[] { new ITraqRawHCDMS2Reader(), new ITraqRawHCDMS3Reader(), new ITraqRawHCDParallelMS3Reader(), new ITraqRawPQDCIDReader(), new ITraqRawPQDReader() }, 0);
      AddComponent(readers);

      normalizationBuilders = new RcpaComboBox<IITraqNormalizationBuilder>(cbNormalizationType, "NormalizationType",
        new IITraqNormalizationBuilder[] { new ITraqNormalizationByMedianIntensityBuilder(), new ITraqNormalizationByTotalIntensityBuilder(), new ITraqNormalizationNoneBuilder() }, 1);

      AddComponent(normalizationBuilders);

      normalizationByIonInjectionTime = new RcpaCheckBox(cbNormalizeByIonInjectionTime, "normalizationByIonInjectionTime", false);
      AddComponent(normalizationByIonInjectionTime);

      precursorPPMTolerance = new RcpaDoubleField(txtPrecursorPPMTolerance, "precursorPPMTolerance", "Precursor Tolerance (ppm)", 10, true);
      AddComponent(precursorPPMTolerance);

      this.Text = Constants.GetSQHTitle(title, version);
    }

    protected override IFileProcessor GetFileProcessor()
    {
      var reader = readers.SelectedItem;
      reader.PlexType = plexTypes.SelectedItem;

      var builder = normalizationBuilders.SelectedItem;
      builder.ByIonInjectionTime = normalizationByIonInjectionTime.Checked;

      var mPeakCount = Math.Max(1, minPeakCount.Value);
      if (reader.PlexType == IsobaricType.PLEX8)
      {
        //对于8标iTRAQ，121往往会受120影响，在某些非肽段的scan中，会出现121有非常低的丰度，而其他channel没有丰度，导致做normalization的时候出错（特别是中值法）。
        mPeakCount = Math.Max(2, mPeakCount);
      }

      return new ITraqResultMultipleFileDistiller(reader, rawFiles.FileNames, individual.Checked, mPeakCount, plexTypes.SelectedItem, isotopeFile.FullName, builder, precursorPPMTolerance.Value);
    }

    protected override ProgressChangedEventHandler GetProgressChanged()
    {
      return progress.ProgressChangedHander;
    }

    protected override void ValidateComponents()
    {
      base.ValidateComponents();

      var plexCount = plexTypes.SelectedItem.GetFuncs().Count;
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
        new ITraqResultMultipleFileDistillerUI().MyShow();
      }

      #endregion

      #region IToolSecondLevelCommand Members

      public string GetSecondLevelCommandItem()
      {
        return MenuCommandType.Quantification_IsobaricLabelling;
      }

      #endregion
    }

    private void cbPlexType_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (isotopeFile != null)
      {
        if (Array.IndexOf(plexFiles, isotopeFile.Text) != -1)
        {
          txtIsotopeFileName.SelectedIndex = cbPlexType.SelectedIndex;
        }

        var plexType = plexTypes.SelectedItem;
        var itemCount = plexType.GetDefinition().Items.Length;

        try
        {
          if (minPeakCount.Value > itemCount)
          {
            minPeakCount.Value = itemCount;
          }
        }
        catch (Exception)
        {
          minPeakCount.Value = itemCount;
        }
      }
    }

    private void cbNormalizationType_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (normalizationBuilders != null && normalizationByIonInjectionTime != null)
      {
        var builder = normalizationBuilders.SelectedItem;
        if (builder is ITraqNormalizationByTotalIntensityBuilder)
        {
          normalizationByIonInjectionTime.Checked = true;
        }
        else
        {
          normalizationByIonInjectionTime.Checked = false;
        }
      }
    }
  }
}
