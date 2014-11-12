using RCPA.Gui;
using RCPA.Gui.FileArgument;
using RCPA.Proteomics.Summary;
using RCPA.Proteomics.Summary.Uniform;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace RCPA.Proteomics.Percolator
{
  public partial class PercolatorDatasetPanel : ExpectValueDatasetPanel
  {
    private PercolatorDatasetOptions PercolatorOption { get { return Options as PercolatorDatasetOptions; } }

    public PercolatorDatasetPanel()
    {
      InitializeComponent();

      this.datFiles = new RcpaListViewMultipleFileField(
        this.btnAddFiles,
        this.btnRemoveFiles,
        this.btnLoad,
        this.btnSave,
        this.lvDatFiles,
        "PercolatorFiles",
        new OpenFileArgument("Percolator MSF", "msf"),
        true,
        false);
      AddComponent(this.datFiles);
    }

    public override bool HasValidFile(bool selectedOnly)
    {
      var files = selectedOnly ? datFiles.GetSelectedItems() : datFiles.GetAllItems();
      foreach (var file in files)
      {
        var input = PercolatorFileParser.GetInputFile(file);
        if (!File.Exists(input))
        {
          throw new FileNotFoundException(string.Format("Cannot find corresponding percolator input file {0} for {1}", input, file));
        }

        var output = PercolatorFileParser.GetOutputFile(file);
        if (!File.Exists(output))
        {
          throw new FileNotFoundException(string.Format("Cannot find corresponding percolator output file {0} for {1}", output, file));
        }
      }

      return files.Length > 0;
    }
  }
}
