﻿using RCPA.Gui;
using RCPA.Gui.FileArgument;
using RCPA.Proteomics.Summary;
using RCPA.Proteomics.Summary.Uniform;
using System;

namespace RCPA.Proteomics.XTandem
{
  public partial class XTandemDatasetPanel : ExpectValueDatasetPanel
  {
    private XTandemDatasetOptions XtandemOption { get { return Options as XTandemDatasetOptions; } }

    private RcpaCheckBox ignoreAnticipagedCleavageSite;

    public XTandemDatasetPanel()
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

    public override void SaveToDataset(bool selectedOnly)
    {
      base.SaveToDataset(selectedOnly);

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
