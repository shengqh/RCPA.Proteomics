using RCPA.Proteomics.Summary;
using System;
using System.Collections.Generic;
using System.IO;

namespace RCPA.Tools.Summary
{
  public class SpectrumEntry : IDisposable
  {
    private bool isProteinFile = false;
    public SpectrumEntry(string resultFile, bool isProteinFile)
    {
      resultWriter = new StreamWriter(resultFile);
      this.isProteinFile = isProteinFile;
      if (isProteinFile)
      {
        fastaWriter = new StreamWriter(resultFile + ".fasta");
      }
    }

    ~SpectrumEntry()
    {
      if (!hasDisposed)
      {
        resultWriter.Close();
        if (isProteinFile)
        {
          fastaWriter.Close();
        }
        hasDisposed = true;
      }
    }

    private bool hasDisposed = false;

    private StreamWriter resultWriter = null;

    public StreamWriter ResultWriter
    {
      get { return resultWriter; }
    }

    private StreamWriter fastaWriter = null;

    public StreamWriter FastaWriter
    {
      get { return fastaWriter; }
    }

    private List<IIdentifiedSpectrum> spectra = new List<IIdentifiedSpectrum>();

    public List<IIdentifiedSpectrum> Spectra
    {
      get { return spectra; }
      set { spectra = value; }
    }

    #region IDisposable Members

    public void Dispose()
    {
      if (!hasDisposed)
      {
        resultWriter.Close();
        if (isProteinFile)
        {
          fastaWriter.Close();
        }
        hasDisposed = true;
      }
    }

    #endregion
  }

  public abstract class AbstractSeparatorBySpectrumFilter : AbstractThreadFileProcessor
  {
    protected Dictionary<IFilter<IIdentifiedSpectrum>, string> filterMap;

    private bool isProteinFile = false;

    public AbstractSeparatorBySpectrumFilter(Dictionary<IFilter<IIdentifiedSpectrum>, string> filterMap, bool isProteinFile)
    {
      this.filterMap = filterMap;
      this.isProteinFile = isProteinFile;
    }

    public override IEnumerable<string> Process(string filename)
    {
      List<string> result = new List<string>();

      Dictionary<IFilter<IIdentifiedSpectrum>, SpectrumEntry> map = new Dictionary<IFilter<IIdentifiedSpectrum>, SpectrumEntry>();
      foreach (IFilter<IIdentifiedSpectrum> filter in filterMap.Keys)
      {
        string resultFile = filename + "." + filterMap[filter];

        result.Add(resultFile);

        map[filter] = new SpectrumEntry(resultFile, isProteinFile);
      }

      return DoProcess(filename, result, map);
    }

    protected abstract IEnumerable<string> DoProcess(string filename, List<string> result, Dictionary<IFilter<IIdentifiedSpectrum>, SpectrumEntry> map);
  }
}
