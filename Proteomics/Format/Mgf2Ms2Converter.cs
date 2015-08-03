using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using RCPA.Utils;
using RCPA.Proteomics.Mascot;
using RCPA.Proteomics.Summary;

namespace RCPA.Proteomics.Format
{
  public class Mgf2Ms2Converter : AbstractThreadProcessor
  {
    private Mgf2Ms2ConverterOptions options;

    public Mgf2Ms2Converter(Mgf2Ms2ConverterOptions options)
    {
      this.options = options;
    }

    public override IEnumerable<string> Process()
    {
      var titleParser = TitleParserUtils.FindByName(options.TitleType);

      Progress.SetMessage("Parsing " + options.InputFile + "...");

      var outputFile = options.OutputFile + ".tmp";

      new Mgf2Ms2Processor(outputFile, titleParser) { Progress = this.Progress }.Process(options.InputFile);

      if (File.Exists(outputFile))
      {
        File.Move(outputFile, options.OutputFile);
      }

      return new[] { options.OutputFile };
    }
  }
}