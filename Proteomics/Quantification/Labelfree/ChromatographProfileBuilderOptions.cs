using CommandLine;
using RCPA.Commandline;
using RCPA.Utils;
using System;
using System.Collections.Generic;

namespace RCPA.Proteomics.Quantification.Labelfree
{
  public class ChromatographProfileBuilderOptions : AbstractOptions
  {
    private const double DEFALUT_MzTolerancePPM = 20;
    private const int DEFAULT_MinimumProfileLength = 3;
    private const double DEFAULT_MaximumRetentionTimeWindow = 5;
    private const double DEFAULT_MaximumProfileDistance = 0.1;
    private const double DEFAULT_MinimumProfileCorrelation = 0.9;
    private const int DEFAULT_MinimumScanCount = 5;
    private const int DEFAULT_ThreadCount = 0;
    private const double DEFAULT_MinimumIsotopicPercentage = 0.05;

    public ChromatographProfileBuilderOptions()
    {
      this.MzTolerancePPM = DEFALUT_MzTolerancePPM;
      this.MinimumProfileLength = DEFAULT_MinimumProfileLength;
      this.MaximumRetentionTimeWindow = DEFAULT_MaximumRetentionTimeWindow;
      this.MinimumIsotopicPercentage = DEFAULT_MinimumIsotopicPercentage;
      this.MaximumProfileDistance = DEFAULT_MaximumProfileDistance;
      this.MinimumProfileCorrelation = DEFAULT_MinimumProfileCorrelation;
      this.MinimumScanCount = DEFAULT_MinimumScanCount;
      this.ThreadCount = DEFAULT_ThreadCount;
    }

    [Option('i', "inputFile", Required = true, MetaValue = "FILE", HelpText = "BuildSummary peptide file")]
    public string InputFile { get; set; }

    [OptionList("rawFiles", MetaValue = "FILELIST", HelpText = "Raw file list")]
    public IList<string> RawFiles { get; set; }

    [Option('o', "outputFile", Required = true, MetaValue = "FILE", HelpText = "Output file")]
    public string OutputFile { get; set; }

    [Option('t', "mzTolerancePPM", Required = false, DefaultValue = DEFALUT_MzTolerancePPM, MetaValue = "DOUBLE", HelpText = "Precursor m/z tolerance in PPM")]
    public double MzTolerancePPM { get; set; }

    [Option('l', "profileLength", Required = false, DefaultValue = DEFAULT_MinimumProfileLength, MetaValue = "INT", HelpText = "Minimum profile length")]
    public int MinimumProfileLength { get; set; }

    [Option('t', "maximumRetentionTimeWindow", Required = false, DefaultValue = DEFAULT_MaximumRetentionTimeWindow, MetaValue = "DOUBLE", HelpText = "Maximum retention time window (minute)")]
    public double MaximumRetentionTimeWindow { get; set; }

    [Option('c', "maximumProfileDistance", Required = false, DefaultValue = DEFAULT_MaximumProfileDistance, MetaValue = "DOUBLE", HelpText = "Maximum kullback leibler distance between real profile and theoritical profile")]
    public double MaximumProfileDistance { get; set; }

    [Option("minimumProfileCorrelation", Required = false, DefaultValue = DEFAULT_MinimumProfileCorrelation, MetaValue = "DOUBLE", HelpText = "Minumum pearson correlation between real profile and theoritical profile")]
    public double MinimumProfileCorrelation { get; set; }

    [Option('s', "minimumScanCount", Required = false, DefaultValue = DEFAULT_MinimumScanCount, MetaValue = "INT", HelpText = "Minimum scan count")]
    public int MinimumScanCount { get; set; }

    [Option("minimumIsotopicPercentage", Required = false, DefaultValue = DEFAULT_MinimumIsotopicPercentage, MetaValue = "DOUBLE", HelpText = "Minimum isotipic percentage of profile")]
    public double MinimumIsotopicPercentage { get; set; }

    [Option("image", Required = false, MetaValue = "BOOLEAN", HelpText = "Draw image for each peptides")]
    public bool DrawImage { get; set; }

    [Option("thread", Required = false, DefaultValue = DEFAULT_ThreadCount, MetaValue = "INT", HelpText = "Thread number, 0 means using all possible cores (no more than number of raw files)")]
    public int ThreadCount { get; set; }

    [Option("overwrite", Required = false, MetaValue = "BOOLEAN", HelpText = "Overwrite old result")]
    public bool Overwrite { get; internal set; }

    public override bool PrepareOptions()
    {
      CheckProperty("InputFile");
      CheckProperty("RawFiles");
      try
      {
        SystemUtils.GetRExecuteLocation();
      }
      catch (Exception ex)
      {
        ParsingErrors.Add("Cannot find system R. Install R first. Error " + ex.Message);
      }

      return ParsingErrors.Count == 0;
    }
  }
}
