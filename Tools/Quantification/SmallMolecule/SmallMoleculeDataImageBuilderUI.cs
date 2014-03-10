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
  public partial class SmallMoleculeDataImageBuilderUI : AbstractFileProcessorUI
  {
    private static readonly string title = "Small Molecule - Significant Peak Image Builder";

    private static readonly string version = "1.0.0";

    private RcpaDirectoryField sampleFile;

    private RcpaDirectoryField refFile;
    
    private RcpaDirectoryField targetDir;
    
    private RcpaTextField peakPattern;

    public SmallMoleculeDataImageBuilderUI()
    {
      InitializeComponent();

      base.SetFileArgument("SigFile",new OpenFileArgument("Result", ".sig"));

      sampleFile = new RcpaDirectoryField(button1, textBox1, "SampleDir", "Sample Data", true);
      AddComponent(sampleFile);

      refFile = new RcpaDirectoryField(button2, textBox2, "RefDir", "Reference Data", true);
      AddComponent(refFile);

      targetDir = new RcpaDirectoryField(button3, textBox3, "TargetDir", "Target Image", true);
      AddComponent(targetDir);

      peakPattern = new RcpaTextField(textBox4, "PeakPattern", "Peak Pattern (P for positive, N for negative)", "P", true);
      AddComponent(peakPattern);

      this.Text = Constants.GetSQHTitle(title, version);
    }

    protected override IFileProcessor GetFileProcessor()
    {
      return new SmallMoleculeDataImageBuilder(sampleFile.FullName, refFile.FullName, targetDir.FullName, peakPattern.Text);
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
        new SmallMoleculeDataImageBuilderUI().MyShow();
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
