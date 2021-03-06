﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using RCPA.Gui;
using RCPA.Proteomics.Raw;
using RCPA.Proteomics;
using RCPA.Gui.FileArgument;
using RCPA.Gui.Command;
using RCPA.Gui.Image;
using ZedGraph;
using RCPA.Utils;
using RCPA.Proteomics.Quantification;
using System.IO;
using MathNet.Numerics.Statistics;
using MathNet.Numerics.Distributions;
using RCPA.Proteomics.Quantification.ITraq;

namespace RCPA.Tools.Quantification
{
  public partial class ITraqPeptideStatisticBuilderUI : AbstractFileProcessorUI
  {
    private static readonly string title = "Isobaric Labelling Peptide Statistic Builder";
    private static readonly string version = "1.1.1";

    private RcpaFileField iTraqFile;
    private RcpaDoubleField minProbability;

    public ITraqPeptideStatisticBuilderUI()
    {
      InitializeComponent();

      base.SetFileArgument("PeptidesFile", new OpenFileArgument("Peptides", "peptides"));

      this.iTraqFile = new RcpaFileField(btnRLocation, txtRLocation, "ITraqFile", new OpenFileArgument("iTRAQ", "itraq.xml"), true);
      this.AddComponent(this.iTraqFile);

      minProbability = new RcpaDoubleField(txtValidProbability, "MinValidProbability", "Minimum valid probability", 0.01, true);
      AddComponent(minProbability);

      this.AddComponent(itraqIons);

      this.Text = Constants.GetSQHTitle(title, version);
    }

    protected override bool IsProcessorSupportProgress()
    {
      return false;
    }

    protected override IFileProcessor GetFileProcessor()
    {
      return new ITraqPeptideStatisticBuilder(iTraqFile.FullName, itraqIons.GetFuncs(), minProbability.Value);
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
        new ITraqPeptideStatisticBuilderUI().MyShow();
      }

      #endregion

      #region IToolSecondLevelCommand Members

      public string GetSecondLevelCommandItem()
      {
        return MenuCommandType.Quantification_IsobaricLabelling;
      }

      #endregion
    }
  }
}
