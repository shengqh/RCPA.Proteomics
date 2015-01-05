using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using RCPA.Proteomics.Mascot;
using RCPA.Gui.Command;
using RCPA.Gui.FileArgument;
using RCPA.Gui;

namespace RCPA.Proteomics.MaxQuant
{
  public partial class MaxQuantSiteToPeptideProcessorUI : AbstractProcessorUI
  {
    public static readonly string title = "MaxQuant Sites To Mascot Peptide Converter";
    public static readonly string version = "1.0.3";

    private RcpaDoubleField minLocalizationProbability;
    private RcpaDoubleField minScoreDiff;
    private RcpaTextField silacAminoacids;

    public MaxQuantSiteToPeptideProcessorUI()
    {
      InitializeComponent();

      siteFile.FileArgument = new OpenFileArgument("MaxQuant Sites", new string[] { "txt" });
      msmsFile.FileArgument = new OpenFileArgument("MaxQuant MSMS", new string[] { "txt" });

      this.minLocalizationProbability = new RcpaDoubleField(txtMinProbability, "minLocalizationProbability", "Minmum Localization Probability", 0.75, true);
      AddComponent(this.minLocalizationProbability);

      this.minScoreDiff = new RcpaDoubleField(txtMinScoreDiff, "minScoreDiff", "Minmum Score Diff", 5.0, true);
      AddComponent(this.minScoreDiff);

      this.silacAminoacids = new RcpaTextField(txtSILACAminoacids, "SILACAminoacids", "SILAC amino acids", "KR", false);
      this.silacAminoacids.PreCondition = cbSILAC;
      AddComponent(this.silacAminoacids);

      this.Text = Constants.GetSQHTitle(title, version);
    }

    protected override void DoBeforeValidate()
    {
      base.DoBeforeValidate();
      this.silacAminoacids.Required = cbSILAC.Checked;
      this.msmsFile.Required = cbSILAC.Checked;
    }

    protected override IProcessor GetProcessor()
    {
      var option = new MaxQuant2MascotPeptideProcessorOption()
      {
        SiteFile = siteFile.FullName,
        MinProbability = minLocalizationProbability.Value,
        MinDeltaScore = minScoreDiff.Value,
        IsSILAC = cbSILAC.Checked,
        SILACAminoacids = silacAminoacids.Text,
        MSMSFile = msmsFile.FullName
      };
     
      return new MaxQuant2MascotPeptideProcessor2(option);
    }

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
        new MaxQuantSiteToPeptideProcessorUI().MyShow();
      }

      #endregion
    }
  }
}

