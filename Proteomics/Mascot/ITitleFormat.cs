using RCPA.Proteomics.Spectrum;

namespace RCPA.Proteomics.Mascot
{
  public interface ITitleFormat
  {
    string FormatName { get; }

    string Example { get; }

    string Build<T>(PeakList<T> pkls) where T : IPeak;
  }

  public abstract class AbstractTitleFormat : ITitleFormat
  {
    public abstract string FormatName { get; }

    public abstract string Example { get; }

    public abstract string Build<T>(PeakList<T> pkls) where T : IPeak;

    public override string ToString()
    {
      return this.FormatName + " : " + this.Example;
    }
  }
}
