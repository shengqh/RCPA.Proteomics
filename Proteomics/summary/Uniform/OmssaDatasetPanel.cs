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

namespace RCPA.Proteomics.Summary.Uniform
{
  public partial class OmssaDatasetPanel : ExpectValueDatasetPanel
  {
    private OmssaDatasetOptions CurrentOptions { get { return Options as OmssaDatasetOptions; } }

    public OmssaDatasetPanel()
    {
      InitializeComponent();

      this.xmlFiles.FileArgument = new OpenFileArgument("OMSSA Omx", "omx");
      AddComponent(new RcpaMultipleFileComponent(this.xmlFiles.GetItemInfos(), "OmxFiles", "OMSSA Omx Files", false, true));
    }

    public override void LoadFromDataset()
    {
      base.LoadFromDataset();

      xmlFiles.Items.Clear();
      xmlFiles.Items.AddRange(CurrentOptions.PathNames.ToArray());
    }

    public override void SaveToDataset()
    {
      base.SaveToDataset();
      Options.PathNames = xmlFiles.FileNames.ToList();
    }
  }
}
