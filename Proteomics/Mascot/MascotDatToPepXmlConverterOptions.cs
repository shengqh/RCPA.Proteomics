using CommandLine;
using RCPA.Commandline;
using RCPA.Proteomics.Summary;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RCPA.Proteomics.Mascot
{
  public class MascotDatToPepXmlConverterOptions : AbstractOptions
  {
    public const int DEFAULT_MinPeptideLength = 7;

    public MascotDatToPepXmlConverterOptions()
    {
      this.MinPeptideLength = DEFAULT_MinPeptideLength;
    }

    [OptionList('i', "inputFiles", Required = true, MetaValue = "FILE_LIST", HelpText = "Input MASCOT dat files")]
    public IList<string> InputFiles { get; set; }

    [Option('t', "titleName", Required = true, MetaValue = "STRING", HelpText = "Title format")]
    public string TitleFormat { get; set; }

    [Option('n', "notCombine", DefaultValue = false, MetaValue = "BOOLEAN", HelpText = "Not combine rank 1 PSMs with different sequences as 1 entry")]
    public bool NotCombine { get; set; }

    [Option('o', "outputDirectory", Required = false, MetaValue = "DIRECTORY", HelpText = "Output directory")]
    public string OutputDirectory { get; set; }

    [Option('l', "minPeptideLength", DefaultValue = DEFAULT_MinPeptideLength, MetaValue = "INT", HelpText = "Minimum peptide length")]
    public int MinPeptideLength { get; set; }

    public override bool PrepareOptions()
    {
      foreach (var file in InputFiles)
      {
        if (!File.Exists(file))
        {
          ParsingErrors.Add(string.Format("File not exists {0}.", file));
        }
      }

      if (!string.IsNullOrEmpty(OutputDirectory) && !Directory.Exists(OutputDirectory))
      {
        ParsingErrors.Add(string.Format("Directory not exists {0}.", OutputDirectory));
      }

      if (GetTitleParser() == null)
      {
        ParsingErrors.Add(string.Format("Title is not exists {0}, select one from following list:\n{1}",
          TitleFormat,
          TitleParserUtils.GetTitleParsers().ConvertAll(m => m.FormatName).Merge("\n")));
      }

      return ParsingErrors.Count == 0;
    }

    public ITitleParser GetTitleParser()
    {
      return TitleParserUtils.FindByName(this.TitleFormat);
    }

    public string GetOutputFile(string inputFile)
    {
      if (string.IsNullOrEmpty(OutputDirectory))
      {
        return Path.ChangeExtension(inputFile, ".pep.xml");
      }
      else
      {
        return Path.Combine(OutputDirectory, Path.GetFileNameWithoutExtension(inputFile) + ".pep.xml");
      }
    }
  }
}
