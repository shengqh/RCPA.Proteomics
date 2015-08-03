using RCPA.Proteomics.Mascot;
using RCPA.Proteomics.Summary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RCPA.Proteomics
{
  public class SearchResultToPeptideBuilderOptions
  {
    public SearchEngineType Engine { get; set; }

    public string InputFile { get; set; }

    public string OutputFile { get { return this.InputFile + ".peptides"; } }
  }

  public class SearchResultToPeptideBuilder : AbstractThreadProcessor
  {
    private SearchResultToPeptideBuilderOptions options;
    public SearchResultToPeptideBuilder(SearchResultToPeptideBuilderOptions options)
    {
      this.options = options;
    }

    public override IEnumerable<string> Process()
    {
      var parser = options.Engine.GetFactory().GetParser(options.InputFile, false);
      var spectra = parser.ReadFromFile(options.InputFile);
      new MascotPeptideTextFormat().WriteToFile(options.OutputFile, spectra);

      return new[] { options.OutputFile };
    }
  }
}
