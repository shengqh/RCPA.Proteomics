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

namespace RCPA.Tools.Quantification
{
  public partial class ITraqPhosphoPeptideStatisticBuilderUI : AbstractFileProcessorUI
  {
    private static readonly string title = "Identified Peptide Phosphorylation Ratio Builder";
    private static readonly string version = "1.0.2";

    private RcpaFileField iTraqFile;
    private RcpaTextField modificationIndecies;
    private RcpaTextField normalIndecies;
    private RcpaTextField modifiedAminoacids;

    public ITraqPhosphoPeptideStatisticBuilderUI()
    {
      InitializeComponent();

      base.SetFileArgument("PeptidesFile", new OpenFileArgument("Peptides", "peptides"));

      this.iTraqFile = new RcpaFileField(btnRLocation, txtRLocation, "ITraqFile", new OpenFileArgument("iTRAQ", "itraq"), true);
      this.AddComponent(this.iTraqFile);

      Func<string, bool> validator = (m =>
      {
        string[] parts = m.Split(new char[] { ',' });
        if (parts.Length != 2)
        {
          return false;
        }

        int intResult;
        return int.TryParse(parts[0].Trim(), out intResult) && int.TryParse(parts[1].Trim(), out intResult);
      });

      this.normalIndecies = new RcpaTextField(txtNormal, "NormalIndecies", "Normal Indecies", "114,115", true) { ValidateFunc = validator };
      this.AddComponent(normalIndecies);

      this.modificationIndecies = new RcpaTextField(txtModifications, "ModificationIndecies", "Modification Indecies", "116,117", true) { ValidateFunc = validator };
      this.AddComponent(modificationIndecies);

      this.modifiedAminoacids = new RcpaTextField(txtModifiedAminoacids, "ModifiedAminoacids", "Modified Aminoacids", "STY", true);
      this.AddComponent(modifiedAminoacids);

      this.Text = Constants.GetSQHTitle(title, version);
    }

    protected override bool IsProcessorSupportProgress()
    {
      return false;
    }

    protected override IFileProcessor GetFileProcessor()
    {
      string[] parts = txtNormal.Text.Split(new char[] { ',' });
      int normal1 = Convert.ToInt32(parts[0].Trim());
      int normal2 = Convert.ToInt32(parts[1].Trim());

      parts = txtModifications.Text.Split(new char[] { ',' });
      int phos1 = Convert.ToInt32(parts[0].Trim());
      int phos2 = Convert.ToInt32(parts[1].Trim());

      return new ITraqPhosphoPeptideStatisticBuilder(iTraqFile.FullName, modifiedAminoacids.Text, new ITraqPhosphoRatioCalculator(phos1, phos2, normal1, normal2));
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
        new ITraqPhosphoPeptideStatisticBuilderUI().MyShow();
      }

      #endregion

      #region IToolSecondLevelCommand Members

      public string GetSecondLevelCommandItem()
      {
        return "ITRAQ";
      }

      #endregion
    }
  }
}
