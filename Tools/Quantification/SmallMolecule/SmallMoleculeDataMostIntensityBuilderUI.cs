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

namespace RCPA.Tools.Quantification.SmallMolecule
{
  public partial class SmallMoleculeDataMostIntensityBuilderUI : AbstractFileProcessorUI
  {
    private static readonly string title = "Small Molecule - Highest Intensity Peak Data Builder";

    private static readonly string version = "1.0.0";

    private RcpaTextField pattern;

    public SmallMoleculeDataMostIntensityBuilderUI()
    {
      InitializeComponent();

      base.SetDirectoryArgument("RootRawDir", "Agilent .d root");

      this.pattern = new RcpaTextField(textBox1, "NamePattern", "Name Pattern", @"(.+)\.d", true);
      this.AddComponent(pattern);

      this.Text = Constants.GetSQHTitle(title, version);
    }

    protected override IFileProcessor GetFileProcessor()
    {
      return new SmallMoleculeDataMostIntensityBuilder(pattern.Text, true);
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
        new SmallMoleculeDataMostIntensityBuilderUI().MyShow();
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
