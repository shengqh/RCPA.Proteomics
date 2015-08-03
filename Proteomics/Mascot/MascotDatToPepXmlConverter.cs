using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RCPA.Proteomics.Summary;
using System.IO;
using System.Text.RegularExpressions;
using RCPA.Proteomics.PeptideProphet;

namespace RCPA.Proteomics.Mascot
{
  public class MascotDatToPepXmlConverter : AbstractThreadProcessor
  {
    private MascotDatToPepXmlConverterOptions options;

    public MascotDatToPepXmlConverter(MascotDatToPepXmlConverterOptions options)
    {
      this.options = options;
    }

    public override IEnumerable<string> Process()
    {
      var result = new List<string>();
      var titleParser = options.GetTitleParser();
      var parser = new MascotDatSpectrumParser(titleParser);
      foreach (var datFile in options.InputFiles)
      {
        Progress.SetMessage("Parsing the data file : " + datFile + " ...");
        var spectra = parser.ReadFromFile(datFile).Where(m => m.Peptide.PureSequence.Length >= options.MinPeptideLength).ToList();
        var targetFile = options.GetOutputFile(datFile);

        var pars = new PepXmlWriterParameters();
        pars.Protease = parser.CurrentProtease;
        pars.Modifications = new PepXmlModifications(parser.CurrentModifications);
        pars.Parameters = parser.CurrentParameters;
        pars.SourceFile = parser.CurrentParameters["FILE"].StringAfter("File Name:").Trim();
        pars.SearchDatabase = parser.CurrentParameters["DB"];
        pars.SearchEngine = "MASCOT";
        pars.NotCombineRank1PSMs = this.options.NotCombine;

        Progress.SetMessage("Writing {0} spectra to file : {1}", spectra.Count, targetFile);
        new MascotPepXmlWriter(pars).WriteToFile(targetFile, spectra);

        result.Add(targetFile);
      }

      return result;
    }
  }
}
