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
using RCPA.Proteomics.Raw;

namespace RCPA.Tools.Mascot
{
  public partial class TurboRaw2MzXmlProcessorUI : AbstractTurboProcessorUI
  {
    private static readonly string title = "Turbo Thermo Raw To mzXML Converter";
    private static readonly string version = "1.0.0";

    private RcpaDirectoryField targetDir;
    private RcpaCheckBox fullMsOnly;
    private RcpaCheckBox centroid;

    public TurboRaw2MzXmlProcessorUI()
    {
      InitializeComponent();

      this.Text = Constants.GetSQHTitle(title, version);

      this.targetDir = new RcpaDirectoryField(btnTarget, txtTarget, "TargetDir", "Target MzXml", true);
      this.fullMsOnly = new RcpaCheckBox(cbFullMsOnly, "FullMsOnly", false);
      this.centroid = new RcpaCheckBox(cbCentroid, "Centroid", true);
      this.AddComponent(targetDir);
      this.AddComponent(fullMsOnly);
      this.AddComponent(centroid);

      base.SetFileArgument("RawFile", new OpenFileArgument("Thermo Raw", "raw"));
      base.SetDirectoryArgument("RawDir", "Raw");
    }

    protected override IFileProcessor GetFileProcessor()
    {
      if (IsBatchMode())
      {
        return new Raw2MzXmlMultipleProcessor(new RawFileImpl(), fullMsOnly.Checked, centroid.Checked, targetDir.FullName, this.Text, Raw2MzXmlProcessor.version, new Dictionary<string, string>());
      }
      else
      {
        return new Raw2MzXmlProcessor(new RawFileImpl(), fullMsOnly.Checked, centroid.Checked, targetDir.FullName, this.Text, Raw2MzXmlProcessor.version, new Dictionary<string, string>());
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
        new TurboRaw2MzXmlProcessorUI().MyShow();
      }

      #endregion
    }
  }
}