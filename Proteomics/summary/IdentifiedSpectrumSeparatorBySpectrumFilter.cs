using RCPA.Proteomics.Mascot;
using RCPA.Proteomics.Summary;
using System.Collections.Generic;

namespace RCPA.Tools.Summary
{
  public class IdentifiedSpectrumSeparatorBySpectrumFilter : AbstractSeparatorBySpectrumFilter
  {
    public IdentifiedSpectrumSeparatorBySpectrumFilter(Dictionary<IFilter<IIdentifiedSpectrum>, string> filterMap)
      : base(filterMap, false)
    { }

    protected override IEnumerable<string> DoProcess(string filename, List<string> result, Dictionary<IFilter<IIdentifiedSpectrum>, SpectrumEntry> map)
    {
      try
      {
        var format = new MascotPeptideTextFormat();
        var spectra = format.ReadFromFile(filename);

        foreach (IFilter<IIdentifiedSpectrum> filter in map.Keys)
        {
          SpectrumEntry entry = map[filter];

          foreach (IIdentifiedSpectrum spectrum in spectra)
          {
            if (filter.Accept(spectrum))
            {
              entry.Spectra.Add(spectrum);
            }
          }

          if (entry.Spectra.Count > 0)
          {
            entry.ResultWriter.WriteLine(format.PeptideFormat.GetHeader());
            entry.Spectra.ForEach(m => entry.ResultWriter.WriteLine(format.PeptideFormat.GetString(m)));
          }
        }

        return result;
      }
      finally
      {
        foreach (SpectrumEntry entry in map.Values)
        {
          entry.Dispose();
        }
      }
    }
  }
}
