using RCPA.Gui;
using RCPA.Gui.Command;
using RCPA.Gui.FileArgument;
using RCPA.Proteomics.Mascot;
using RCPA.Proteomics.Summary;
using RCPA.Utils;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace RCPA.Proteomics.Snp
{
  public partial class SpectrumSAPValidatorUI : ComponentUI
  {
    private static readonly string title = "Spectrum SAP Validator";
    private static readonly string version = "1.0.1";

    private RcpaFileField peptideFile;
    private RcpaDirectoryField imageDirectory;

    private List<IIdentifiedSpectrum> spectra;

    private Func<IIdentifiedSpectrum, bool> colorFunc;

    public SpectrumSAPValidatorUI()
    {
      InitializeComponent();
      peptideFile = new RcpaFileField(btnPeptideFile, txtPeptideFile, "PeptideFile", new OpenFileArgument("Peptides", "peptides"), true);
      AddComponent(peptideFile);

      imageDirectory = new RcpaDirectoryField(btnImageDirectory, txtImageDirectory, "ImageDirectory", "Images", true);
      AddComponent(imageDirectory);

      this.Text = Constants.GetSQHTitle(title, version);
    }

    private void GetCurrentImageFilename(out string mutationFilename, out string originalFilename)
    {
      if (gvPeptides.SelectedRows.Count == 0)
      {
        mutationFilename = string.Empty;
        originalFilename = string.Empty;
        return;
      }

      var spectrum = spectra[gvPeptides.SelectedRows[0].Index];
      string imageFilename = GetImageFilename(spectrum);
      var isMutation = (bool)(spectrum.Annotations["IsMutation"]);
      var index = spectrum.Annotations["Index"].ToString();

      var correspond = (from s in spectra
                        let cindex = s.Annotations["Index"].ToString()
                        let cIsMutation = (bool)(s.Annotations["IsMutation"])
                        where cindex.Equals(index) && s.Charge == spectrum.Charge && cIsMutation != isMutation
                        orderby s.Score descending
                        select s).ToList();

      if (correspond.Count == 0)
      {
        string selectFilename = GetImageFilename(spectrum);

        if (isMutation)
        {
          mutationFilename = selectFilename;
          originalFilename = string.Empty;
        }
        else
        {
          mutationFilename = string.Empty;
          originalFilename = selectFilename;
        }
        lblMutation.Text = string.Empty;
      }
      else
      {
        IIdentifiedSpectrum oriSp, mutSp;

        if (isMutation)
        {
          mutSp = spectrum;
          oriSp = correspond[0];
        }
        else
        {
          oriSp = spectrum;
          mutSp = correspond[0];
        }
        mutationFilename = GetImageFilename(mutSp);
        originalFilename = GetImageFilename(oriSp);

        int mutationSite = -1;

        var mutSeq = mutSp.Peptide.PureSequence;
        var oriSeq = oriSp.Peptide.PureSequence;
        MutationUtils.IsMutationOneIL(mutSeq, oriSeq, ref mutationSite);

        if (mutationSite == 0)
        {
          lblMutation.Text = MyConvert.Format(".{0}.{1}\n.{2}.{3}",
            oriSeq[mutationSite],
            oriSeq.Substring(mutationSite + 1),
            mutSeq[mutationSite],
            mutSeq.Substring(mutationSite + 1));
        }
        else if (mutationSite == mutSeq.Length - 1)
        {
          lblMutation.Text = MyConvert.Format("{0}.{1}.\n{2}.{3}.",
            oriSeq.Substring(0, mutationSite - 1),
            oriSeq[mutationSite],
            mutSeq.Substring(0, mutationSite - 1),
            mutSeq[mutationSite]);
        }
        else
        {
          lblMutation.Text = MyConvert.Format("{0}.{1}.{2}\n{3}.{4}.{5}",
            oriSeq.Substring(0, mutationSite - 1),
            oriSeq[mutationSite],
            oriSeq.Substring(mutationSite + 1),
            mutSeq.Substring(0, mutationSite - 1),
            mutSeq[mutationSite],
            mutSeq.Substring(mutationSite + 1));
        }
      }
    }

    private string GetImageFilename(IIdentifiedSpectrum spectrum)
    {
      var jpgName = GetImageFileName(spectrum, "jpg");
      if (File.Exists(jpgName))
      {
        return jpgName;
      }

      var pngName = GetImageFileName(spectrum, "png");
      if (File.Exists(pngName))
      {
        return pngName;
      }

      var tiffName = GetImageFileName(spectrum, "tiff");
      if (File.Exists(tiffName))
      {
        return tiffName;
      }

      return string.Empty;
    }

    private string GetImageFileName(IIdentifiedSpectrum spectrum, string ext)
    {
      var sf = spectrum.Query.FileScan;
      var jpgName = MyConvert.Format("{0}.{1}.{2}.{3}.{4}", sf.Experimental, sf.FirstScan, sf.LastScan, sf.Charge, ext);
      jpgName = new FileInfo(imageDirectory.FullName + "\\" + jpgName).FullName;
      return jpgName;
    }

    private void btnLoad_Click(object sender, EventArgs e)
    {
      try
      {
        this.ValidateComponents();
      }
      catch (Exception ex)
      {
        MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        return;
      }

      spectra = new MascotPeptideTextFormat().ReadFromFile(peptideFile.FullName);
      //spectra.Sort((m1, m2) =>
      //{
      //  var result = GetIndex(m1).CompareTo(GetIndex(m2));
      //  if (result == 0)
      //  {
      //    result = m1.Sequence.CompareTo(m2.Sequence);
      //  }

      //  if (result == 0)
      //  {
      //    result = -m1.GetPrecursorMz().CompareTo(m2.GetPrecursorMz());
      //  }
      //  return result;
      //});

      spectra.ForEach(m => m.Annotations["IsMutation"] = m.Proteins.Any(n => n.StartsWith("MUL_")));

      if (spectra.All(m => (bool)(m.Annotations["IsMutation"])))
      {
        colorFunc = n => GetIndex(n) % 2 == 0;
      }
      else
      {
        colorFunc = n => (bool)(n.Annotations["IsMutation"]);
      }

      bsSpectrum.DataSource = spectra;
    }

    private void tvPeptides_AfterSelect(object sender, TreeViewEventArgs e)
    {
      ShowImage();
    }

    private void ShowImage()
    {
      string mutationFilename, originalFilename;

      GetCurrentImageFilename(out mutationFilename, out originalFilename);
      if (File.Exists(originalFilename))
      {
        txtOriginalFilename.Text = Path.GetFileName(originalFilename);
        pbOriginal.Load(originalFilename);
      }
      else
      {
        txtOriginalFilename.Text = "";
        pbOriginal.Image = null;
      }

      if (File.Exists(mutationFilename))
      {
        txtMutationFilename.Text = Path.GetFileName(mutationFilename);
        pbMutation.Load(mutationFilename);
      }
      else
      {
        txtMutationFilename.Text = "";
        pbMutation.Image = null;
      }
    }

    private void SpectrumSnpValidatorUI_Load(object sender, EventArgs e)
    {
      LoadOption();
    }

    private void SpectrumSnpValidatorUI_FormClosing(object sender, FormClosingEventArgs e)
    {
      SaveOption();
    }


    public class Command : IToolCommand
    {
      #region IToolCommand Members

      public string GetClassification()
      {
        return MenuCommandType.Misc;
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
        new SpectrumSAPValidatorUI().MyShow();
      }

      #endregion
    }

    private void gvPeptides_CellValueNeeded(object sender, DataGridViewCellValueEventArgs e)
    {
      var dpName = gvPeptides.Columns[e.ColumnIndex].HeaderText;
      var spectrum = spectra[e.RowIndex];
      if (dpName == "Index")
      {
        e.Value = GetIndex(spectrum);
      }
      else if (dpName == "Peptide")
      {
        e.Value = spectrum.GetSequences(" ! ");
      }
      else if (dpName == "FileScan")
      {
        e.Value = spectrum.Query.FileScan.ShortFileName;
      }
      else if (dpName == "Protein")
      {
        e.Value = spectrum.GetProteins(" ! ");
      }
    }

    private static Regex indexReg = new Regex(@"(\d+)");
    private static int GetIndex(IIdentifiedSpectrum spectrum)
    {
      var index = spectrum.Annotations["Index"].ToString();
      return Convert.ToInt32(indexReg.Match(index).Groups[1].Value);
    }

    private void gvPeptides_SelectionChanged(object sender, EventArgs e)
    {
      ShowImage();
    }

    private void gvPeptides_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
    {
      if (gvPeptides.Columns[e.ColumnIndex].DataPropertyName == "ObservedMz")
      {
        var spectrum = spectra[e.RowIndex];
        e.Value = MyConvert.Format("{0:0.00000}", spectrum.ObservedMz);
      }
    }

    private System.Drawing.Color mycolor = Color.FromArgb(183, 245, 253);
    private void gvPeptides_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
    {
      if (gvPeptides.Rows.Count != 0)
      {
        for (int i = 0; i < gvPeptides.Rows.Count - 1; i++)
        {
          if (colorFunc(spectra[i]))
          {
            this.gvPeptides.Rows[i].DefaultCellStyle.BackColor = mycolor;
          }
        }
      }
    }

    private void btnClose_Click(object sender, EventArgs e)
    {
      Close();
    }
  }
}
