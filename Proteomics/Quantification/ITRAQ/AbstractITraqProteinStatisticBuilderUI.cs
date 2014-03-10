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
  public partial class AbstractITraqProteinStatisticBuilderUI : AbstractFileProcessorUI
  {
    private RcpaFileField iTraqFile;
    private RcpaDoubleField minProbability;
    private RcpaCheckBox normalize;
    private RcpaComboBox<IRatioPeptideToProteinBuilder> builders;
    private RcpaCheckBox filterPeptide;
    private RcpaCheckBox modifiedPeptideOnly;
    private RcpaTextField modifiedChar;

    public AbstractITraqProteinStatisticBuilderUI()
    {
      InitializeComponent();

      base.SetFileArgument("ProteinsFile", new OpenFileArgument("Proteins", "noredundant"));

      this.iTraqFile = new RcpaFileField(btnRLocation, txtRLocation, "ITraqFile", new OpenFileArgument("iTRAQ", "itraq.xml"), true);
      this.AddComponent(this.iTraqFile);

      minProbability = new RcpaDoubleField(txtValidProbability, "MinValidProbability", "Minimum valid probability", 0.01, true);
      AddComponent(minProbability);

      normalize = new RcpaCheckBox(cbNormalize, "Normalize", false);
      AddComponent(normalize);

      builders = new RcpaComboBox<IRatioPeptideToProteinBuilder>(cbRatioCalculator, "RatioBuilder", RatioPeptideToProteinBuilderFactory.GetBuilders(), 0);
      AddComponent(builders);

      filterPeptide = new RcpaCheckBox(cbFilterPeptide, "FilterPeptide", true);
      AddComponent(filterPeptide);

      modifiedPeptideOnly = new RcpaCheckBox(cbModifiedOnly, "ModifiedOnly", false);
      AddComponent(modifiedPeptideOnly);

      modifiedChar = new RcpaTextField(txtModifiedCharacter, "ModifiedChar", "Input modified characters which indicates isobaric labelling(such as @#)", "@#", false);
      modifiedChar.PreCondition = cbModifiedOnly;
      AddComponent(modifiedChar);

      AddComponent(itraqIons);

      AddComponent(pnlClassification);
    }

    protected override void OnAfterLoadOption(EventArgs e)
    {
      base.OnAfterLoadOption(e);
      CheckValidationVisible();

      if (pnlClassification.Height < 50)
      {
        this.Height = this.Height + 200;
      }
    }

    protected override void DoBeforeValidate()
    {
      minProbability.Required = itraqIons.GetFuncs().Count == 2;
    }

    protected override bool IsProcessorSupportProgress()
    {
      return false;
    }

    protected override IFileProcessor GetFileProcessor()
    {
      var option = GetStatisticOption();

      return new ITraqProteinStatisticBuilder(option);
    }

    protected ITraqProteinStatisticOption GetStatisticOption()
    {
      ITraqProteinStatisticOption option = new ITraqProteinStatisticOption();

      option.DatasetMap = pnlClassification.GetClassificationSet();
      option.ITraqFileName = iTraqFile.FullName;
      option.MinimumProbability = filterPeptide.Checked ? minProbability.Value : 0.0;
      option.NormalizeByMedianRatio = normalize.Checked;
      option.RatioPeptideToProteinBuilder = builders.SelectedItem;
      option.QuantifyModifiedPeptideOnly = modifiedPeptideOnly.Checked;
      option.ModificationChars = modifiedChar.Text;
      option.References = itraqIons.GetFuncs();
      return option;
    }

    private void CheckValidationVisible()
    {
      var funcs = itraqIons.GetFuncs();
      cbFilterPeptide.Visible = funcs.Count == 2;
      txtValidProbability.Visible = funcs.Count == 2;
    }

    private void itraqIons_IonCheckedChanged(object sender, EventArgs e)
    {
      CheckValidationVisible();
    }

    private void btnLoad_Click(object sender, EventArgs e)
    {
      try
      {
        GetOriginFile();

        HashSet<string> experimentals = new IdentifiedResultExperimentalReader().ReadFromFile(GetOriginFile());

        List<string> sortedExperimentals = new List<string>(experimentals);
        sortedExperimentals.Sort();

        if (sortedExperimentals.Count > 0)
        {
          pnlClassification.InitializeClassificationSet(sortedExperimentals);
        }
      }
      catch (Exception ex)
      {
        MessageBox.Show(this, ex.Message, "Error");
      }
    }
  }
}
