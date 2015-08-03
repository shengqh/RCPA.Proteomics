using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using RCPA.Utils;
using RCPA.Proteomics.Mascot;
using RCPA.Proteomics.Comet;
using RCPA.Proteomics.MSAmanda;
using RCPA.Proteomics.MSGF;
using RCPA.Proteomics.MyriMatch;

namespace RCPA.Proteomics.Summary
{
  public class PeptideSpectrumMatchDistiller : AbstractThreadProcessor
  {
    private PeptideSpectrumMatchDistillerOptions options;

    public PeptideSpectrumMatchDistiller(PeptideSpectrumMatchDistillerOptions options)
    {
      this.options = options;
    }

    public override IEnumerable<string> Process()
    {
      var searchEngine = EnumUtils.StringToEnum<SearchEngineType>(options.EngineType, SearchEngineType.Unknown);

      ISpectrumParser parser = searchEngine.GetFactory().GetParser(options.InputFile, options.Rank2);
      string suffix = options.Rank2 ? ".rank2" : string.Empty;

      if (!string.IsNullOrEmpty(options.TitleType))
      {
        parser.TitleParser = TitleParserUtils.FindByName(options.TitleType);
      }
      parser.Progress = this.Progress;

      Progress.SetMessage("Parsing " + options.InputFile + "...");
      var spectra = parser.ReadFromFile(options.InputFile);
      var format = new MascotPeptideTextFormat()
      {
        NotExportSummary = true
      };
      var outputFile = string.IsNullOrEmpty(options.OutputFile) ? options.InputFile + suffix + ".peptides" : options.OutputFile;
      Progress.SetMessage("Writing {0} PSMs to {1}...", spectra.Count, outputFile);
      format.WriteToFile(outputFile, spectra);

      return new[] { outputFile };
    }
  }
}