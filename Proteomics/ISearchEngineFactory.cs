using RCPA.Proteomics.Summary;
using RCPA.Proteomics.Summary.Uniform;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RCPA.Proteomics
{
  public interface ISearchEngineFactory
  {
    /// <summary>
    /// Return search engine type
    /// </summary>
    SearchEngineType EngineType { get; }

    /// <summary>
    /// Get parser based on input file/directory name
    /// </summary>
    /// <param name="name">File or directory name</param>
    /// <returns>Spectrum parser</returns>
    ISpectrumParser GetParser(string name);

    /// <summary>
    /// Get score functions of the search engine
    /// </summary>
    /// <returns>Score functions</returns>
    IScoreFunctions GetScoreFunctions();

    /// <summary>
    /// Get very confident spectra
    /// </summary>
    /// <param name="source">Source spectra</param>
    /// <returns>High confident spectra</returns>
    List<IIdentifiedSpectrum> GetHighConfidentPeptides(List<IIdentifiedSpectrum> source);

    /// <summary>
    /// Get search engine options
    /// </summary>
    /// <returns>Options</returns>
    IDatasetOptions GetOptions();
  }
}
