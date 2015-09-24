using CommandLine;
using RCPA.Commandline;
using RCPA.Proteomics.Summary;
using System;
using System.Collections.Generic;
using System.IO;

namespace RCPA.Proteomics.Mascot
{
  public class MascotGenericFormatShiftPrecursorProcessorOptions : AbstractOptions
  {
    private const double DEFAULT_ShiftMass = -10.0;
    
    public const int DEFAULT_ShiftScan = 10000000;

    private const string DEFAULT_TitleFormat = "DTA";

    [OptionList('i', "inputFiles", Required = true, MetaValue = "FILE_LIST", HelpText = "Input MASCOT dat files")]
    public IList<string> InputFiles { get; set; }

    [Option('t', "titleName", Required = false, DefaultValue=DEFAULT_TitleFormat, MetaValue = "STRING", HelpText = "Title format")]
    public string TitleFormat { get; set; }

    [Option('o', "outputDirectory", Required = false, MetaValue = "DIRECTORY", HelpText = "Output directory")]
    public string OutputDirectory { get; set; }

    [Option('m', "shiftMass", Required = false, DefaultValue=DEFAULT_ShiftMass, MetaValue = "DOUBLE", HelpText = "Delta mass added to old precursor")]
    public double ShiftMass { get; set; }

    [Option('s', "shiftScan", Required = false, DefaultValue = DEFAULT_ShiftScan, MetaValue = "DOUBLE", HelpText = "Delta scan added to old scan")]
    public int ShiftScan { get; set; }

    public MascotGenericFormatShiftPrecursorProcessorOptions()
    {
      this.ShiftMass = DEFAULT_ShiftMass;
      this.ShiftScan = DEFAULT_ShiftScan;
    }

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
        ParsingErrors.Add(string.Format("Title is not exists {0}.", TitleFormat));
        Console.WriteLine("Valid title format names : \n" + StringUtils.Merge(TitleParserUtils.GetTitleParsers().ConvertAll(m => m.FormatName), "\n") + "\n");
      }

      return ParsingErrors.Count == 0;
    }

    public ITitleParser GetTitleParser()
    {
      return TitleParserUtils.FindByName(this.TitleFormat);
    }

    public string GetOutputFile(string inputFile)
    {
      var shiftedExtension = string.Format(".shifted{0}daltons.mgf", this.ShiftMass);
      if (string.IsNullOrEmpty(OutputDirectory))
      {
        return Path.ChangeExtension(inputFile, shiftedExtension);
      }
      else
      {
        return Path.Combine(OutputDirectory, Path.GetFileNameWithoutExtension(inputFile) + shiftedExtension);
      }
    }
  }
}