using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using RCPA.Gui;
using RCPA.Gui.Command;
using RCPA.Gui.FileArgument;
using RCPA.Proteomics.Mascot;
using RCPA.Proteomics.Summary;
using System.Collections.Generic;
using RCPA.Utils;
using RCPA.Proteomics.Summary.Uniform;

namespace RCPA.Proteomics.Summary
{
  public partial class PeptideSpectrumMatchDistillerUI : AbstractProcessorUI
  {
    public static readonly string Title = "Peptide-Spectrum-Match Distiller";

    public static readonly string Version = "1.0.1";

    protected RcpaComboBox<ITitleParser> titleParsers;
    protected RcpaComboBox<string> engines;

    public PeptideSpectrumMatchDistillerUI()
    {
      InitializeComponent();

      this.searchResultFiles.FileArgument = new OpenFileArgument("Search Result", "*");

      var parsers = TitleParserUtils.GetTitleParsers().ToArray();
      this.titleParsers = new RcpaComboBox<ITitleParser>(this.cbTitleFormat, "TitleFormat", parsers, parsers.Length - 1, true);
      AddComponent(this.titleParsers);

      var engineNames = PeptideSpectrumMatchDistillerOptions.GetValidEngines();
      this.engines = new RcpaComboBox<string>(this.cbEngines, "Engine", engineNames, 0);
      AddComponent(this.engines);

      this.Text = Constants.GetSQHTitle(Title, Version);
    }

    protected override IProcessor GetProcessor()
    {
      var options = new PeptideSpectrumMatchDistillerOptions()
      {
        InputFiles = searchResultFiles.FileNames,
        EngineType = engines.SelectedItem,
        TitleType = titleParsers.SelectedItem.FormatName,
        Rank2 = rbRank2.Enabled && rbRank2.Checked
      };

      return new PeptideSpectrumMatchDistiller(options);
    }

    private void cbEngines_SelectedIndexChanged(object sender, EventArgs e)
    {
      rbRank2.Enabled = PeptideSpectrumMatchDistillerOptions.GetValidRank2Engines().Contains(cbEngines.SelectedItem);
    }
  }
}

