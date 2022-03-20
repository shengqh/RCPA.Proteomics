using RCPA.Commandline;
using System.Collections.Generic;

namespace RCPA.Proteomics.Format
{
  public class MultipleRaw2MgfCommandProcessor : AbstractThreadProcessor
  {
    private MultipleRaw2MgfCommandOptions options;
    public MultipleRaw2MgfCommandProcessor(MultipleRaw2MgfCommandOptions options)
    {
      this.options = options;
    }

    public override IEnumerable<string> Process()
    {
      var runOptions = new MultipleRaw2MgfOptions();
      if (options.Create)
      {
        runOptions.RawFiles = new[] { "sample1.raw", "sample2.raw" };
        runOptions.SaveToFile(options.InputFile);
        return new[] { options.InputFile };
      }

      runOptions.LoadFromFile(options.InputFile);
      return new MultipleRaw2MgfProcessor(runOptions).Process(runOptions.TargetDirectory);
    }
  }

  public class MultipleRaw2MgfCommand : AbstractCommandLineCommand<MultipleRaw2MgfCommandOptions>
  {
    public override string Name
    {
      get { return "raw2mgf"; }
    }

    public override string Description
    {
      get { return "Convert RAW to MGF/mzXML"; }
    }

    public override IProcessor GetProcessor(MultipleRaw2MgfCommandOptions options)
    {
      return new MultipleRaw2MgfCommandProcessor(options);
    }
  }

}
