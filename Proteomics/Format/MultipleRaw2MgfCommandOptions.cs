using System;
using RCPA.Commandline;
using RCPA.Gui.Command;
using CommandLine;

namespace RCPA.Proteomics.Format
{
  public class MultipleRaw2MgfCommandOptions : AbstractOptions
  {
    [Option('c', "configFile", Required = true, MetaValue = "FILE", HelpText = "Configuration file")]
    public string InputFile { get; set; }

    [Option("create", MetaValue = "BOOL", HelpText = "Create empty configuration file")]
    public bool Create { get; set; }

    public override bool PrepareOptions()
    {
      if (!Create)
      {
        CheckFile("Configuration file", InputFile);

        try
        {
          new MultipleRaw2MgfOptions().LoadFromFile(InputFile);
        }
        catch (Exception ex)
        {
          ParsingErrors.Add("Error to read configuration file: " + ex.Message);
        }
      }

      return ParsingErrors.Count == 0;
    }
  }
}
