using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using RCPA.Gui;
using RCPA.Gui.FileArgument;
using System.IO;
using RCPA.Proteomics.XTandem;

namespace RCPA.Proteomics.Summary.Uniform
{
  public partial class XtandemDatasetPanel : ExpectValueDatasetPanel
  {
    private XtandemDatasetOptions XtandemOption { get { return Options as XtandemDatasetOptions; } }

    private RcpaCheckBox ignoreAnticipagedCleavageSite;

    public XtandemDatasetPanel()
    {
      InitializeComponent();

      this.datFiles = new RcpaListViewMultipleFileField(
        this.btnAddFiles,
        this.btnRemoveFiles,
        this.btnLoad,
        this.btnSave,
        this.lvDatFiles,
        "XmlFiles",
        new OpenFileArgument("XTandem Xml", "xml"),
        true,
        false);
      AddComponent(this.datFiles);

      this.ignoreAnticipagedCleavageSite = new RcpaCheckBox(cbIgnoreAnticipatedCleavageSite, "ignoreAnticipagedCleavageSite", true);
      AddComponent(this.ignoreAnticipagedCleavageSite);

      base.filterByExpectValue.Checked = true;
      base.filterByScore.Checked = false;
    }

    public override void LoadFromDataset()
    {
      base.LoadFromDataset();

      this.ignoreAnticipagedCleavageSite.Checked = XtandemOption.IgnoreUnanticipatedPeptide;
    }

    public override void SaveToDataset()
    {
      base.SaveToDataset();

      XtandemOption.IgnoreUnanticipatedPeptide = this.ignoreAnticipagedCleavageSite.Checked;
    }

    private void btnMgfFiles_Click(object sender, EventArgs e)
    {
      SummaryUtils.FindXtandemSourceXml(lvDatFiles);
    }

    private void btnRenameDat_Click(object sender, EventArgs e)
    {
      SummaryUtils.RenameAsXtandemSourceXml(lvDatFiles);
    }

    protected override string GetTitleSample()
    {
      if (datFiles.SelectFileNames.Length > 0)
      {
        return XTandemSpectrumXmlParser.GetTitleSample(datFiles.SelectFileNames[0]);
      }
      return base.GetTitleSample();
    }
  }
}
