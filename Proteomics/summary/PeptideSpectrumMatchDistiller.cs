using System.Collections.Generic;

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
      var factory = searchEngine.GetFactory();

      string suffix = options.Rank2 ? ".rank2" : string.Empty;
      var result = new List<string>();
      foreach (var inputfile in options.InputFiles)
      {
        ISpectrumParser parser = factory.GetParser(inputfile, options.Rank2);
        if (!string.IsNullOrEmpty(options.TitleType))
        {
          parser.TitleParser = TitleParserUtils.FindByName(options.TitleType);
        }
        parser.Progress = this.Progress;

        Progress.SetMessage("Parsing " + inputfile + "...");
        var spectra = parser.ReadFromFile(inputfile);
        var format = factory.GetPeptideFormat(true);

        var outputFile = string.IsNullOrEmpty(options.OutputFile) ? inputfile + suffix + ".peptides" : options.OutputFile;
        Progress.SetMessage("Writing {0} PSMs to {1}...", spectra.Count, outputFile);
        format.WriteToFile(outputFile, spectra);

        result.Add(outputFile);
      }

      return result;
    }
  }
}