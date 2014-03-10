using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Linq;
using RCPA.Gui;
using RCPA.Gui.FileArgument;
using RCPA.Proteomics.Sequest;
using RCPA.Proteomics.Summary;
using RCPA.Gui.Command;
using RCPA.Proteomics.Distribution;
using System.IO;
using System.Text.RegularExpressions;

namespace RCPA.Proteomics.Statistic
{
  public partial class AbstractDistributionUI : AbstractProcessorFileUI
  {
    private RcpaFileField sourceFile;

    private RcpaTextField classificationTitle;

    private RcpaComboBox<PeptideFilterType> filterType;

    private RcpaIntegerField loopFrom, loopTo, loopStep;

    private RcpaCheckBox modifiedOnly;

    private RcpaTextField modifiedAminoacids;

    public AbstractDistributionUI()
    {
      InitializeComponent();

      sourceFile = new RcpaFileField(btnProteinFile, txtProteinFile, "SourceFile", GetSourceFileArgument(), true);
      AddComponent(sourceFile);

      classificationTitle = new RcpaTextField(txtClassificationTitle, "ClassificationTitle", "Classification title", "", true);
      AddComponent(classificationTitle);

      filterType = new RcpaComboBox<PeptideFilterType>(cbFilterType, "FilterType", Enum.GetValues(typeof(PeptideFilterType)).Cast<PeptideFilterType>().ToArray(), 0);
      AddComponent(filterType);

      loopFrom = new RcpaIntegerField(txtLoopFrom, "LoopFrom", "Loop from", 1, true);
      AddComponent(loopFrom);

      loopTo = new RcpaIntegerField(txtLoopTo, "LoopTo", "Loop to", 2, true);
      AddComponent(loopTo);

      loopStep = new RcpaIntegerField(txtLoopStep, "LoopStep", "Loop step", 1, true);
      AddComponent(loopStep);

      modifiedOnly = new RcpaCheckBox(cbModifiedOnly, "ModifiedOnly", false);
      AddComponent(modifiedOnly);

      modifiedAminoacids = new RcpaTextField(txtModifiedAminoacids, "ModifiedAminoacids", "Modified aminoacids", "STY", false);
      AddComponent(modifiedAminoacids);

      AddComponent(pnlClassification);
    }

    protected override void MyAfterLoadOption(object sender, EventArgs e)
    {
      base.MyAfterLoadOption(sender, e);

      cbClassifiedByTag_CheckedChanged(null, null);
    }

    private void btnLoad_Click(object sender, EventArgs e)
    {
      try
      {
        sourceFile.ValidateComponent();
      }
      catch (Exception ex)
      {
        MessageBox.Show(this, ex.Message, "Error");
        return;
      }

      HashSet<string> experimentals = new IdentifiedResultExperimentalReader().ReadFromFile(sourceFile.FullName);

      List<string> sortedExperimentals = new List<string>(experimentals);
      sortedExperimentals.Sort();

      if (sortedExperimentals.Count > 0)
      {
        pnlClassification.InitializeClassificationSet(sortedExperimentals);
      }
    }

    protected override void DoBeforeValidate()
    {
      modifiedAminoacids.Required = modifiedOnly.Checked;
    }

    protected override string GetOriginFile()
    {
      DistributionOption option = GetDistributionOption();

      FileInfo fi = new FileInfo(option.SourceFileName);
      string targetDir = MyConvert.Format(@"{0}\{1}_{2}_CLASSIFICATION",
        fi.DirectoryName,
        option.DistributionType,
        option.ClassificationPrinciple);

      if (!Directory.Exists(targetDir))
      {
        Directory.CreateDirectory(targetDir);
      }

      string result = MyConvert.Format(@"{0}\{1}.{2}_{3}.statistic.xml",
        targetDir,
        fi.Name,
        option.DistributionType,
        option.ClassificationPrinciple);

      new DistributionOptionXmlFormat().WriteToFile(result, option);

      return result;
    }

    protected DistributionOption GetDistributionOption()
    {
      DistributionOption option = new DistributionOption()
      {
        ClassificationPrinciple = classificationTitle.Text,
        ClassificationSet = GetClassificationSet(),
        DistributionType = GetDistributionType(),
        FilterFrom = loopFrom.Value,
        FilterTo = loopTo.Value,
        FilterStep = loopStep.Value,
        FilterType = filterType.SelectedItem,
        ModifiedPeptideOnly = modifiedOnly.Checked,
        ModifiedPeptide = modifiedAminoacids.Text,
        SourceFileName = sourceFile.FullName,
        ClassifiedByTag = cbClassifiedByTag.Checked
      };
      
      return option;
    }

    private Dictionary<string, List<string>> GetClassificationSet()
    {
      if (!cbClassifiedByTag.Checked)
      {
        return pnlClassification.GetClassificationSet();
      }
      else
      {
        var tags = IdentifiedTagReader.GetTags(sourceFile.FullName, GetDistributionType() == DistributionType.Protein);

        return (from t in tags orderby t select t).ToDictionary(m => m, m => new string[] { m }.ToList());
      }
    }

    protected virtual DistributionType GetDistributionType()
    {
      if (this.DesignMode)
      {
        return DistributionType.Protein;
      }
      else
      {
        throw new Exception("Implement GetDistributionType first");
      }
    }

    protected virtual OpenFileArgument GetSourceFileArgument()
    {
      return new OpenFileArgument("noredundant", "noredundant");
    }

    private void cbClassifiedByTag_CheckedChanged(object sender, EventArgs e)
    {
      btnLoad.Enabled = !cbClassifiedByTag.Checked;
      pnlClassification.Enabled = !cbClassifiedByTag.Checked;
    }
  }
}
