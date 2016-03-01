using CommandLine;
using RCPA.Commandline;

namespace RCPA.Proteomics.Quantification.Labelfree
{
  public class ChromatographProfileBuilderOptions : AbstractOptions
  {
    private const double DEFALUT_MzTolerancePPM = 20;
    private const int DEFAULT_ProfileLength = 3;
    private const double DEFAULT_RetentionTimeWindow = 5;
    private const double DEFAULT_MinimumCorrelation = 0.9;
    private const int DEFAULT_MinimumScanCount = 5;

    public ChromatographProfileBuilderOptions()
    {
      this.MzTolerancePPM = DEFALUT_MzTolerancePPM;
      this.ProfileLength = DEFAULT_ProfileLength;
      this.RetentionTimeWindow = DEFAULT_RetentionTimeWindow;
      this.MinimumCorrelation = DEFAULT_MinimumCorrelation;
      this.MinimumScanCount = DEFAULT_MinimumScanCount;
    }

    [Option('i', "inputFile", Required = true, MetaValue = "FILE", HelpText = "BuildSummary peptide file")]
    public string InputFile { get; set; }

    [Option('d', "rawDirectory", Required = true, MetaValue = "DIRECTORY", HelpText = "The root directory contains raw files")]
    public string RawDirectory { get; set; }

    [Option('o', "outputFile", Required = true, MetaValue = "FILE", HelpText = "Output file")]
    public string OutputFile { get; set; }

    [Option('t', "mzTolerancePPM", Required = false, DefaultValue = DEFALUT_MzTolerancePPM, MetaValue = "DOUBLE", HelpText = "Precursor m/z tolerance in PPM")]
    public double MzTolerancePPM { get; set; }

    [Option('l', "profileLength", Required = false, DefaultValue = DEFAULT_ProfileLength, MetaValue = "INT", HelpText = "Profile length")]
    public int ProfileLength { get; set; }

    [Option('t', "retentionTimeWindow", Required = false, DefaultValue = DEFAULT_RetentionTimeWindow, MetaValue = "DOUBLE", HelpText = "Maximum retention time window (minute)")]
    public double RetentionTimeWindow { get; set; }

    [Option('c', "minimumCorrelation", Required = false, DefaultValue = DEFAULT_MinimumCorrelation, MetaValue = "DOUBLE", HelpText = "Minimum correlation between real profile and theoritical profile")]
    public double MinimumCorrelation { get; set; }

    [Option('s', "minimumScanCount", Required = false, DefaultValue = DEFAULT_MinimumScanCount, MetaValue = "INT", HelpText = "Minimum scan count")]
    public int MinimumScanCount { get; set; }

    public override bool PrepareOptions()
    {
      CheckProperty("InputFile");
      CheckProperty("RawDirectory");

      return ParsingErrors.Count == 0;
    }
  }
}
