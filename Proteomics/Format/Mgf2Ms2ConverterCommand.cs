using RCPA.Commandline;
using RCPA.Gui.Command;

namespace RCPA.Proteomics.Format
{
  public class Mgf2Ms2ConverterCommand : AbstractCommandLineCommand<Mgf2Ms2ConverterOptions>
  {
    public override string Name
    {
      get { return "MGF2MS2"; }
    }

    public override string Description
    {
      get { return "Convert MGF to MS2"; }
    }

    public override RCPA.IProcessor GetProcessor(Mgf2Ms2ConverterOptions options)
    {
      return new Mgf2Ms2Converter(options);
    }
  }
}
