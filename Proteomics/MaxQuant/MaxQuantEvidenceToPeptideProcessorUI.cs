﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using RCPA.Gui;
using RCPA.Gui.FileArgument;
using RCPA.Gui.Command;

namespace RCPA.Proteomics.MaxQuant
{
  public partial class MaxQuantEvidenceToPeptideProcessorUI : AbstractFileProcessorUI
  {
    static string title = "MaxQuant Evidence To Peptide";
    static string version = "1.0.1";

    public MaxQuantEvidenceToPeptideProcessorUI()
    {
      InitializeComponent();

      SetFileArgument("MaxQuantEvidence", new OpenFileArgument("MaxQuant Evidence", "txt"));

      this.Text = Constants.GetSQHTitle(title, version);
    }

    protected override IFileProcessor GetFileProcessor()
    {
      return new MaxQuantEvidenceToPeptideProcessor();
    }

    #region Nested type: Command

    public class Command : IToolCommand
    {
      #region IToolCommand Members

      public string GetClassification()
      {
        return MenuCommandType.MaxQuant;
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
        new MaxQuantEvidenceToPeptideProcessorUI().MyShow();
      }
      #endregion
    }
    #endregion
  }
}
