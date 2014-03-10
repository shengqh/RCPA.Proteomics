using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using RCPA.Gui;
using RCPA.Proteomics.Summary;
using RCPA.Proteomics.Mascot;
using RCPA.Gui.Command;
using RCPA.Proteomics.Utils;
using RCPA.Gui.Image;
using System.IO;
using System.Threading.Tasks;
using System.Threading;

namespace RCPA.Proteomics.Quantification.ITraq
{
  public partial class ITraqQuantificationSummaryViewerUI : ComponentUI
  {
    private static readonly string title = "Isobaric Labelling Quantification Summary Viewer";

    private static readonly string version = "1.0.3";

    public event UpdateQuantificationItemEvent UpdateSpectrum;

    protected void OnUpdateSpectrum(UpdateQuantificationItemEventArgs e)
    {
      if (UpdateSpectrum != null)
      {
        UpdateSpectrum(this, e);
      }
    }

    public ITraqQuantificationSummaryViewerUI()
    {
      InitializeComponent();

      this.Text = Constants.GetSQHTitle(title, version);

      UpdateSpectrum += new ZedGraphITraqSpectrum(zgcSpectrum, this.CreateGraphics(), "ITraq Scan Information").Update;
    }

    private void mnuExit_Click(object sender, EventArgs e)
    {
      Close();
    }

    private MascotResultTextFormat format = new MascotResultTextFormat();
    private IIdentifiedResult proteins;
    private ITraqProteinStatisticOption option;
    private string proteinFile;
    private string itrapFile;

    private void mnuOpen_Click(object sender, EventArgs e)
    {
      var dlg = new ITraqOpenFileDialog();
      dlg.LoadOption();

      if (dlg.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
      {
        proteinFile = dlg.ProteinFile;
        itrapFile = dlg.ITraqFile;

        var paramFile = Path.ChangeExtension(dlg.ProteinFile, "param");
        if (!File.Exists(paramFile))
        {
          MessageBox.Show(this, "Error find parameter file " + paramFile);
          return;
        }

        option = new ITraqProteinStatisticOption();
        option.LoadFromFile(paramFile);

        Progress.Begin();

        var task = Task.Factory.StartNew(() => LoadData(), TaskCreationOptions.LongRunning);
        task.ContinueWith((m) => this.Invoke(new Action(UpdateData)), TaskContinuationOptions.OnlyOnRanToCompletion);
      }
    }

    private void UpdateData()
    {
      if (!Progress.IsCancellationPending())
      {
        Progress.SetMessage("Updating protein informations ...");

        UpdateProteins();

        if (tvResult.Nodes.Count > 0)
        {
          tvResult.SelectedNode = tvResult.Nodes[0];
        }

        Progress.SetMessage("Finished.");
      }
    }

    private void LoadData()
    {
      try
      {
        Progress.SetMessage("Reading proteins from " + proteinFile + " ...");
        format.Progress = Progress;
        proteins = format.ReadFromFile(proteinFile);

        List<IIdentifiedSpectrum> spectra = proteins.GetSpectra();

        Progress.SetMessage("Reading itraqs from " + itrapFile + " ...");
        ITraqItemUtils.LoadITraq(spectra, itrapFile, true, Progress);

        for(int i = proteins.Count-1;i >= 0;i--)
        {
          var group = proteins[i];
          group[0].Peptides.RemoveAll(m => m.Spectrum.FindIsobaricItem() == null);
          if (group[0].Peptides.Count == 0)
          {
            proteins.Remove(group);
          }
        }
      }
      catch (UserTerminatedException)
      {
        Progress.SetMessage("User terminated.");
      }
      catch (Exception ex)
      {
        Progress.SetMessage("Error : {0}", ex.Message);
        MessageBox.Show(ex.Message);
      }
    }

    private void UpdateProteins()
    {
      tvResult.BeginUpdate();
      try
      {
        tvResult.Nodes.Clear();
        foreach (var g in proteins)
        {
          var subNode = tvResult.Nodes.Add(string.Format("{0} ({1})", g[0].Name, g[0].Peptides.Count));
          subNode.Tag = g;

          var uniquePeptides = g[0].Peptides.GroupBy(m => PeptideUtils.GetMatchedSequence(m.Sequence)).OrderByDescending(n => n.Count()).ThenBy(n => n.Key);
          foreach (var up in uniquePeptides)
          {
            var subPNode = subNode.Nodes.Add(string.Format("{0} ({1})", up.Key, up.Count()));
            subPNode.Tag = new List<IIdentifiedSpectrum>(from p in up select p.Spectrum);

            var peps = (from p in up
                        orderby p.Spectrum.Query.FileScan.Experimental, p.Spectrum.Query.FileScan.FirstScan
                        select p).ToList();

            foreach (var p in peps)
            {
              var ppNode = subPNode.Nodes.Add(string.Format("{0}, {1:0.##}", p.Spectrum.Query.FileScan.LongFileName, p.Spectrum.Score));
              ppNode.Tag = p.Spectrum;
            }
          }
        }
      }
      finally
      {
        tvResult.EndUpdate();
      }
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
        new ITraqQuantificationSummaryViewerUI().MyShow();
      }

      #endregion

      #region IToolSecondLevelCommand Members

      public string GetSecondLevelCommandItem()
      {
        return MenuCommandType.Quantification_IsobaricLabelling;
      }

      #endregion
    }

    private TreeNode proteinNode = null;
    private TreeNode uniquePeptideNode = null;

    private void tvResult_AfterSelect(object sender, TreeViewEventArgs e)
    {
      var node = e.Node;
      if (0 == node.Level)
      {
        if (node.Nodes.Count > 0)
        {
          DoUpdateUniquePeptide(node.Nodes[0]);
        }
        else
        {
          DoUpdateProtein(node);
        }
        //tcInformations.ShowTabPage(tpProtein);
      }
      else if (1 == node.Level)
      {
        if (node.Nodes.Count > 0)
        {
          DoUpdateSpectrum(node.Nodes[0]);
        }
        else
        {
          DoUpdateUniquePeptide(node.Nodes[0]);
        }
        //tcInformations.ShowTabPage(tpUniquePeptide);
      }
      else if (2 == node.Level)
      {
        DoUpdateSpectrum(node);
        //tcInformations.ShowTabPage(tpSpectrum);
      }
    }

    private void DoUpdateProtein(TreeNode node, bool forceRefresh = false)
    {
      if (proteinNode != node || forceRefresh)
      {
        proteinNode = node;

        var args = new UpdateQuantificationItemEventArgs(option, node.Tag);

        new ZedGraphITraqProtein2(zgcProtein, this.CreateGraphics(), "Protein").Update(null, args);
      }
    }

    private void DoUpdateUniquePeptide(TreeNode node, bool forceRefresh = false)
    {
      if (uniquePeptideNode != node || forceRefresh)
      {
        var args = new UpdateQuantificationItemEventArgs(option, node.Tag);

        new ZedGraphITraqProtein2(zgcUniquePeptide, this.CreateGraphics(), "Unique Peptide").Update(null, args);

        uniquePeptideNode = node;

        DoUpdateProtein(node.Parent);
      }
    }

    private void DoUpdateSpectrum(TreeNode node)
    {
      var spectrum = node.Tag as IIdentifiedSpectrum;

      var itraqItem = spectrum.FindIsobaricItem();

      var args = new UpdateQuantificationItemEventArgs(null, itraqItem);

      OnUpdateSpectrum(args);

      DoUpdateUniquePeptide(node.Parent);

      DoUpdateProtein(node.Parent.Parent);
    }

    private void ITraqQuantificationSummaryViewerUI_FormClosed(object sender, FormClosedEventArgs e)
    {
      tvResult.Nodes.Clear();
      proteins = null;
      GC.Collect();
      GC.WaitForFullGCComplete();
    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
      Progress.Cancel();
    }
  }
}
