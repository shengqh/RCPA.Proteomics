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
  public partial class SmallMoleculeDataPreparationProcessorUI : AbstractFileProcessorUI
  {
    public SmallMoleculeDataPreparationProcessorUI()
    {
      InitializeComponent();

      base.SetDirectoryArgument("RootRawDir", "Agilent .d root");

      this.Text = Constants.GetSQHTitle(title, version);
    }

    private static readonly string title = "Small Molecule - Data Preparation Processor";

    private static readonly string version = "1.0.0";

    protected override IFileProcessor GetFileProcessor()
    {
      return new SmallMoleculeDataPreparationProcessor();
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
        new SmallMoleculeDataPreparationProcessorUI().MyShow();
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
