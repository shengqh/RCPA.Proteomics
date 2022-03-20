using RCPA.Gui;
using RCPA.Gui.Command;
using RCPA.Gui.FileArgument;
using RCPA.Proteomics.Summary;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace RCPA.Proteomics.Quantification.IsobaricLabelling
{
  public partial class IsobaricLabelingExperimentalDesignBuilderUI : AbstractUI
  {
    private RcpaFileField dataFile;
    private RcpaFileField iTraqFile;

    private SaveFileArgument saveArgument = new SaveFileArgument("Experimental Design", ".experimental.xml");
    private OpenFileArgument loadArgument = new OpenFileArgument("Experimental Design", ".experimental.xml");

    private static string title = "Isobaric Labeling Experimental Design Builder";
    private static string version = "1.0.0";

    public IsobaricLabelingExperimentalDesignBuilderUI()
    {
      InitializeComponent();

      this.dataFile = new RcpaFileField(btnDataFile, txtDataFile, "DataFile", new OpenFileArgument("Peptides/Proteins", new[] { "peptides", "noredundant" }), true);
      this.dataFile.AfterBrowseFileEvent += btnLoad_Click;
      this.AddComponent(this.dataFile);

      this.iTraqFile = new RcpaFileField(btnIsobaricXmlFile, txtIsobaricXmlFile, "IsobaricXmlFile", new OpenFileArgument("Isobaric XML", "isobaric.xml"), true);
      this.AddComponent(this.iTraqFile);

      this.Text = Constants.GetSQHTitle(title, version);
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

    protected IsobaricLabelingExperimentalDesign GetStatisticOption()
    {
      var option = new IsobaricLabelingExperimentalDesign();

      option.DatasetMap = pnlClassification.GetClassificationSet();
      option.IsobaricFile = iTraqFile.FullName;
      option.References = GetReferenceFuncs();
      option.PlexType = IsobaricScanXmlUtils.GetIsobaricType(txtIsobaricXmlFile.Text);

      return option;
    }

    private void btnLoad_Click(object sender, EventArgs e)
    {
      try
      {
        HashSet<string> experimentals = new IdentifiedResultExperimentalReader().ReadFromFile(dataFile.FullName);

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

    private void txtXmlFile_TextChanged(object sender, EventArgs e)
    {
      if (File.Exists(txtIsobaricXmlFile.Text))
      {
        try
        {
          refChannels.PlexType = IsobaricScanXmlUtils.GetIsobaricType(txtIsobaricXmlFile.Text);
        }
        catch (Exception ex)
        {
          MessageBox.Show(this, string.Format("Failed to get isobaric type from {0}, error: {1}", txtIsobaricXmlFile.Text, ex.Message));
        }
      }
    }

    protected override void DoRealGo()
    {
      var dlg = saveArgument.GetFileDialog();
      if (dlg.ShowDialog(this) == DialogResult.OK)
      {
        GetStatisticOption().SaveToFile(dlg.FileName);
        MessageBox.Show(this, "Design saved to " + dlg.FileName, "Congratulation", MessageBoxButtons.OK, MessageBoxIcon.Information);
      }
    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
      var dlg = loadArgument.GetFileDialog();
      if (dlg.ShowDialog(this) == DialogResult.OK)
      {
        IsobaricLabelingExperimentalDesign option = new IsobaricLabelingExperimentalDesign();
        try
        {
          option.LoadFromFile(dlg.FileName);
          AssignFromOption(option);
        }
        catch (Exception ex)
        {
          MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
      }
    }

    private void AssignFromOption(IsobaricLabelingExperimentalDesign option)
    {
      iTraqFile.FullName = option.IsobaricFile;
      refChannels.SelectedIons = (from f in option.References
                                  select f.Name).Merge(",");
      pnlClassification.SetClassificationSet(option.DatasetMap);
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
        new IsobaricLabelingExperimentalDesignBuilderUI().MyShow();
      }

      #endregion

      #region IToolSecondLevelCommand Members

      public string GetSecondLevelCommandItem()
      {
        return MenuCommandType.Quantification_IsobaricLabelling_NEW;
      }

      #endregion
    }
  }
}
