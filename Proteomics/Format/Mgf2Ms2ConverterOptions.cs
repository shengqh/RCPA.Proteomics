using CommandLine;
using RCPA.Commandline;
using RCPA.Proteomics.Summary;
using System;
using System.IO;
using System.Linq;

namespace RCPA.Proteomics.Format
{
  public class Mgf2Ms2ConverterOptions : AbstractOptions
  {
    public Mgf2Ms2ConverterOptions()
    {
      this.TitleType = string.Empty;
    }

    [Option('i', "inputFile", Required = true, MetaValue = "FILE", HelpText = "Input MGF file")]
    public string InputFile { get; set; }

    [Option('t', "titleType", Required = true, MetaValue = "STRING", HelpText = "Title format, set as 'help' for display all valid title types")]
    public string TitleType { get; set; }

    [Option('o', "outputFile", Required = true, MetaValue = "FILE", HelpText = "Output MS2 file")]
    public string OutputFile { get; set; }

    public override bool PrepareOptions()
    {
      if (!File.Exists(this.InputFile))
      {
        ParsingErrors.Add(string.Format("Input file not exists {0}.", this.InputFile));
      }

      if (TitleParserUtils.FindByName(TitleType) == null)
      {
        if (!string.IsNullOrEmpty(this.TitleType) && !this.TitleType.ToLower().Equals("help"))
        {
          ParsingErrors.Add(string.Format("Unknown title type {0}.", this.TitleType));
        }
        ParsingErrors.Add("  Those titles are valid:");
        (from t in TitleParserUtils.GetTitleParsers()
         select t.FormatName).ToList().ForEach(m => ParsingErrors.Add("    " + m));
      }

      return ParsingErrors.Count == 0;
    }
  }
}
