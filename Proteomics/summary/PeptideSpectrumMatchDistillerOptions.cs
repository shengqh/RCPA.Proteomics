using CommandLine;
using RCPA.Commandline;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace RCPA.Proteomics.Summary
{
  public class PeptideSpectrumMatchDistillerOptions : AbstractOptions
  {
    public PeptideSpectrumMatchDistillerOptions()
    {
      this.TitleType = string.Empty;
      this.Rank2 = false;
    }

    [OptionList('i', "inputFiles", Required = true, MetaValue = "FILE", HelpText = "Input search result files")]
    public IList<string> InputFiles { get; set; }

    [Option('e', "engineType", Required = true, MetaValue = "STRING", HelpText = "Engine type: MASCOT, SEQUEST, Comet, XTandem, PFind, PeptidePhophet, MyriMatch, MSGF, OMSSA, MSAmanda, Percolator")]
    public string EngineType { get; set; }

    [Option('t', "titleType", Required = false, MetaValue = "STRING", HelpText = "Title format, if required, set as help for display all valid title types")]
    public string TitleType { get; set; }

    [Option('o', "outputFile", Required = false, MetaValue = "FILE", HelpText = "Output peptides file")]
    public string OutputFile { get; set; }

    [Option("rank2", Required = false, MetaValue = "BOOLEAN", HelpText = "Extract rank 2 PSM (for Comet/MSAmanda/MSGF/MyriMatch only)")]
    public bool Rank2 { get; set; }

    public SearchEngineType GetSearchEngineType()
    {
      return EnumUtils.StringToEnum<SearchEngineType>(this.EngineType, SearchEngineType.Unknown);
    }

    public override bool PrepareOptions()
    {
      foreach (var inputfile in this.InputFiles)
      {
        if (!File.Exists(inputfile))
        {
          ParsingErrors.Add(string.Format("Input file not exists {0}.", inputfile));
        }
      }

      var engine = GetSearchEngineType();
      if (engine == SearchEngineType.Unknown)
      {
        if (!this.EngineType.ToLower().Equals("help"))
        {
          ParsingErrors.Add(string.Format("Unknown search engine type {0}", this.EngineType));
        }
        ParsingErrors.Add(string.Format("  Those engines are valid: {0}", (GetValidEngines()).Merge(", ")));
      }

      if (!string.IsNullOrEmpty(TitleType) && TitleParserUtils.FindByName(TitleType) == null)
      {
        if (!this.TitleType.ToLower().Equals("help"))
        {
          ParsingErrors.Add(string.Format("Unknown title type {0}.", this.TitleType));
        }
        ParsingErrors.Add("  Those titles are valid:");
        (from t in TitleParserUtils.GetTitleParsers()
         select t.FormatName).ToList().ForEach(m => ParsingErrors.Add("    " + m));
      }

      if (Rank2 && engine != SearchEngineType.Unknown)
      {
        foreach (var inputfile in InputFiles)
        {
          try
          {
            var parser = engine.GetFactory().GetParser(inputfile, this.Rank2);
          }
          catch (Exception ex)
          {
            ParsingErrors.Add(ex.Message);
            break;
          }
        }
      }

      return ParsingErrors.Count == 0;
    }

    public static string[] GetValidRank2Engines()
    {
      return new[] { "Comet", "MSAmanda", "MSGF", "MyriMatch" };
    }

    public static string[] GetValidEngines()
    {
      return (from v in EnumUtils.EnumToStringArray<SearchEngineType>()
              where !v.Equals(SearchEngineType.Unknown.ToString())
              select v).ToArray();
    }
  }
}
