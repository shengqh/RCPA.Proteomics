using RCPA.Commandline;
using RCPA.Gui.Command;

namespace RCPA.Proteomics.Mascot
{
  public class MascotGenericFormatShiftPrecursorProcessorCommand : AbstractCommandLineCommand<MascotGenericFormatShiftPrecursorProcessorOptions>, IToolCommand
  {
    public override string Name
    {
      get { return "mgf_shift_precursor"; }
    }

    public override string Description
    {
      get { return "mgf_shift_precursor-Spectrum_Match distiller"; }
    }

    public override RCPA.IProcessor GetProcessor(MascotGenericFormatShiftPrecursorProcessorOptions options)
    {
      return new MascotGenericFormatShiftPrecursorProcessor(options);
    }

    #region IToolCommand Members

    public string GetClassification()
    {
      return MenuCommandType.Mascot;
    }

    public string GetCaption()
    {
      return MascotGenericFormatShiftPrecursorProcessorUI.Title;
    }

    public string GetVersion()
    {
      return MascotGenericFormatShiftPrecursorProcessorUI.Version;
    }

    public void Run()
    {
      new MascotGenericFormatShiftPrecursorProcessorUI().MyShow();
    }

    #endregion
  }
}
