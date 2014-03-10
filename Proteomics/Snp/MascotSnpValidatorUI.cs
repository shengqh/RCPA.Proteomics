using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using RCPA.Gui;
using RCPA;
using RCPA.Gui.FileArgument;
using RCPA.Gui.Command;
using System.IO;
using RCPA.Proteomics.Quantification;
using RCPA.Proteomics.Summary;
using RCPA.Seq;

namespace RCPA.Proteomics.Snp
{
  /// <summary>
  /// 读取Mascot进行SNP搜索的结果，对鉴定到的SNP数据与非SNP数据进行配对、比较。
  /// 20120210 - 2.0.4, 修复：读取Uniprot Xml文件进行注释时会报空指针错误。
  /// </summary>
  public partial class MascotSnpValidatorUI : AbstractFileProcessorUI
  {
    private static readonly string title = "Mascot SAP Validator";
    private static readonly string version = "2.0.6";

    private RcpaTextField mutationPattern;
    private RcpaFileField fastaFile;
    private RcpaComboBox<IAccessNumberParser> acParsers;

    private RcpaFileField dbFile;
    private RcpaFileField pnovoFile;

    public MascotSnpValidatorUI()
    {
      InitializeComponent();

      base.SetFileArgument("PeptideFile", new OpenFileArgument("Peptides", "peptides"));

      this.Text = Constants.GetSQHTitle(title, version);

      this.mutationPattern = new RcpaTextField(txtPattern, "MutationPattern", "Mutation pattern", "MUL_", true);
      AddComponent(this.mutationPattern);

      this.fastaFile = new RcpaFileField(btnFastaFile, txtFastaFile, "FastaFile", new OpenFileArgument("Protein Fasta", "fasta"), true);
      AddComponent(this.fastaFile);

      this.acParsers = new RcpaComboBox<IAccessNumberParser>(cbAccessNumberPattern, "AccessNumberParser", AccessNumberParserFactory.GetParsers().ToArray(), -1);
      AddComponent(this.acParsers);

      dbFile = new RcpaFileField(btnBrowse, txtDBFile, "DBFile", new OpenFileArgument("Uniprot Xml Database", "xml"), false);
      //dbFile.PreCondition = cbAnnotatedByDB;
      AddComponent(dbFile);

      pnovoFile = new RcpaFileField(btnPnovoPeptide, txtPnovoPeptide, "PNovoPeptide", new OpenFileArgument("PNovo Peptides", "peptides"), true);
      AddComponent(pnovoFile);

      AddComponent(pnlClassification);
    }

    protected override IFileProcessor GetFileProcessor()
    {
      return new MascotSnpValidator(mutationPattern.Text, fastaFile.FullName, acParsers.SelectedItem, new HashSet<int>(new[] { 1, 2, 3 }), pnovoFile.FullName)
      {
        ClassificationSet = pnlClassification.GetClassificationSet(),
        IgnoreNtermMutation = ignoreNTerm.Checked,
        IgnoreDeamidatedMutation = ignoreDeamidatedMutation.Checked,
        IgnoreMultipleNucleotideMutation = ignoreMultipleNucleotideMutation.Checked,
        UniprotXmlFile = cbAnnotatedByDB.Checked ? dbFile.FullName : string.Empty
      };
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
        new MascotSnpValidatorUI().MyShow();
      }

      #endregion
    }

    private void btnLoad_Click(object sender, EventArgs e)
    {
      try
      {
        originalFile.ValidateComponent();
      }
      catch (Exception ex)
      {
        MessageBox.Show(this, ex.Message, "Error");
        return;
      }

      HashSet<string> experimentals = new IdentifiedResultExperimentalReader().ReadFromFile(originalFile.FullName);

      List<string> sortedExperimentals = new List<string>(experimentals);
      sortedExperimentals.Sort();

      if (sortedExperimentals.Count > 0)
      {
        pnlClassification.InitializeClassificationSet(sortedExperimentals);
      }
    }
  }
}

