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

    public static readonly string Version = "1.0.0";

    protected RcpaComboBox<ITitleParser> titleParsers;
    protected RcpaComboBox<string> engines;

    public PeptideSpectrumMatchDistillerUI()
    {
      InitializeComponent();

      this.peptideFile.FileArgument = new OpenFileArgument("Search Result", "*");

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
        EngineType = engines.SelectedItem,
        TitleType = titleParsers.SelectedItem.FormatName,
        InputFile = peptideFile.FullName
      };

      return new PeptideSpectrumMatchDistiller(options);
    }
  }
}

