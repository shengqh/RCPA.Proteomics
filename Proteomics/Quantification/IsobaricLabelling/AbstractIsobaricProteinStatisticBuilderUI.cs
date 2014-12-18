using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
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
using RCPA.Proteomics.Summary;
using RCPA.Numerics;

namespace RCPA.Proteomics.Quantification.IsobaricLabelling
{
  public partial class AbstractIsobaricProteinStatisticBuilderUI : AbstractFileProcessorUI
  {
    private RcpaFileField iTraqFile;
    private RcpaCheckBox normalize;
    private RcpaCheckBox modifiedPeptideOnly;
    private RcpaTextField modifiedChar;
    private RcpaComboBox<string> methods;

    public AbstractIsobaricProteinStatisticBuilderUI()
    {
      InitializeComponent();

      base.SetFileArgument("ProteinsFile", new OpenFileArgument("Proteins", "noredundant"));

      this.iTraqFile = new RcpaFileField(btnIsobaricXmlFile, txtIsobaricXmlFile, "IsobaricXmlFile", new OpenFileArgument("Isobaric XML", "isobaric.xml"), true);
      this.AddComponent(this.iTraqFile);

      normalize = new RcpaCheckBox(cbNormalize, "Normalize", false);
      AddComponent(normalize);

      modifiedPeptideOnly = new RcpaCheckBox(cbModifiedOnly, "ModifiedOnly", false);
      AddComponent(modifiedPeptideOnly);

      modifiedChar = new RcpaTextField(txtModifiedCharacter, "ModifiedChar", "Input modified characters which indicates isobaric labelling(such as @#)", "@#", false);
      modifiedChar.PreCondition = cbModifiedOnly;
      AddComponent(modifiedChar);

      methods = new RcpaComboBox<string>(cbRatioCalculator, "PeptideToProteinMethod", new[] { "Median", "Sum" }, 0, true, "How to calculate protein ratio from peptide?");
      AddComponent(methods);

      AddComponent(pnlClassification);
    }

    protected override void OnAfterLoadOption(EventArgs e)
    {
      base.OnAfterLoadOption(e);

      if (pnlClassification.Height < 50)
      {
        this.Height = this.Height + 200;
      }
    }

    protected override void ValidateComponents()
    {
      base.ValidateComponents();

      GetReferenceFuncs();
    }

    private List<IsobaricIndex> GetReferenceFuncs()
    {
      var usedChannels = IsobaricScanXmlUtils.GetUsedChannels(iTraqFile.FullName);
      var result = refChannels.GetFuncs();
      foreach (var refFunc in result)
      {
        bool bFound = false;
        for (int i = 0; i < usedChannels.Count; i++)
        {
          if (usedChannels[i].Name.Equals(refFunc.Name))
          {
            refFunc.Index = i;
            bFound = true;
            break;
          }
        }

        if (!bFound)
        {
          throw new Exception(string.Format("Channel {0} was not used in sample and cannot be used as reference, valid channels are {1}",
            refFunc.Name,
            usedChannels.ConvertAll(l => l.Name).Merge("/")));
        }
      }

      return result;
    }

    protected override void DoBeforeValidate()
    {
    }

    protected override bool IsProcessorSupportProgress()
    {
      return false;
    }

    protected override IFileProcessor GetFileProcessor()
    {
      var option = GetStatisticOption();

      return new IsobaricProteinStatisticBuilder(option);
    }

    protected IsobaricProteinStatisticBuilderOptions GetStatisticOption()
    {
      var option = new IsobaricProteinStatisticBuilderOptions();

      option.DatasetMap = pnlClassification.GetClassificationSet();
      option.IsobaricFileName = iTraqFile.FullName;
      option.ProteinFileName = GetOriginFile();
      option.MinimumProbability = 0.0;
      option.QuantifyModifiedPeptideOnly = modifiedPeptideOnly.Checked;
      option.ModificationChars = modifiedChar.Text;
      option.References = GetReferenceFuncs();
      option.PerformNormalizition = normalize.Checked;
      option.PeptideToProteinMethod = methods.SelectedItem;
      option.PlexType = IsobaricScanXmlUtils.GetIsobaricType(txtIsobaricXmlFile.Text);

      return option;
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

    private void txtRLocation_TextChanged(object sender, EventArgs e)
    {
      if (File.Exists(txtIsobaricXmlFile.Text))
      {
        try
        {
          refChannels.PlexType = IsobaricScanXmlUtils.GetIsobaricType(txtIsobaricXmlFile.Text);
        }
        catch (Exception)
        {
          MessageBox.Show(this, string.Format("{0} is not a valid isobaric xml file!", txtIsobaricXmlFile.Text));
        }
      }
    }
  }
}
