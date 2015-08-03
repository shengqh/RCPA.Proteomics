using RCPA.Commandline;
using RCPA.Gui.Command;

namespace RCPA.Proteomics.Summary
{
  public class PeptideSpectrumMatchDistillerCommand : AbstractCommandLineCommand<PeptideSpectrumMatchDistillerOptions>, IToolCommand
  {
    public override string Name
    {
      get { return "PSM_distiller"; }
    }

    public override string Description
    {
      get { return "Peptide-Spectrum_Match distiller"; }
    }

    public override RCPA.IProcessor GetProcessor(PeptideSpectrumMatchDistillerOptions options)
    {
      return new PeptideSpectrumMatchDistiller(options);
    }

      #region IToolCommand Members

      public string GetClassification()
      {
        return MenuCommandType.Distiller;
      }

      public string GetCaption()
      {
        return PeptideSpectrumMatchDistillerUI.Title;
      }

      public string GetVersion()
      {
        return PeptideSpectrumMatchDistillerUI.Version;
      }

      public void Run()
      {
        new PeptideSpectrumMatchDistillerUI().MyShow();
      }

      #endregion
  }
}
