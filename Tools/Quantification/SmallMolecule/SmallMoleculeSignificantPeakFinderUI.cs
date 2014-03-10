using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using RCPA.Gui;
using System.IO;
using RCPA.Gui.Command;
using RCPA.Gui.FileArgument;

namespace RCPA.Tools.Quantification.SmallMolecule
{
  public partial class SmallMoleculeSignificantPeakFinderUI : AbstractFileProcessorUI
  {
    private RcpaFileField sampleFile;
    private RcpaFileField refFile;

    public SmallMoleculeSignificantPeakFinderUI()
    {
      InitializeComponent();

      base.SetFileArgument("SaveFile",new SaveFileArgument("Result", ".sig"));
      
      sampleFile = new RcpaFileField(button1, textBox1, "SampleFile", new OpenFileArgument("Sample Data", ".data"), true);
      AddComponent(sampleFile);

      refFile = new RcpaFileField(button2, textBox2, "RefFile", new OpenFileArgument("Reference Data", ".data"), true);
      AddComponent(refFile);

      this.Text = Constants.GetSQHTitle(title, version);
    }

    private static readonly string title = "Small Molecule - Significant Peak Finder";

    private static readonly string version = "1.0.0";

    protected override IFileProcessor GetFileProcessor()
    {
      return new SmallMoleculeSignificantPeakFinder(sampleFile.FullName, refFile.FullName);
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
        new SmallMoleculeSignificantPeakFinderUI().MyShow();
      }

      #endregion

      #region IToolSecondLevelCommand Members

      public string GetSecondLevelCommandItem()
      {
        return "Small Molecule";
      }

      #endregion
    }
  }
}
