using System;
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
using RCPA.Proteomics.Summary;
using RCPA.Numerics;

namespace RCPA.Proteomics.Quantification.ITraq
{
  public partial class ITraqProteinStatisticBuilderUI : AbstractITraqProteinStatisticBuilderUI
  {
    private static readonly string title = "Isobaric Labelling Protein Statistic Builder";
    private static readonly string version = "1.1.5";

    public ITraqProteinStatisticBuilderUI()
    {
      InitializeComponent();

      this.Text = Constants.GetSQHTitle(title, version);
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
        new ITraqProteinStatisticBuilderUI().MyShow();
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
