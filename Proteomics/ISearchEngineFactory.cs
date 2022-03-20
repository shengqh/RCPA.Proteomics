using RCPA.Proteomics.Mascot;
using RCPA.Proteomics.Summary;
using RCPA.Proteomics.Summary.Uniform;
using System.Collections.Generic;

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
    ISpectrumParser GetParser(string name, bool extractRank2);

    /// <summary>
    /// Get all possible score functions used for false discovery rate calculation.
    /// The score functions should be ordered by priority.
    /// </summary>
    /// <returns></returns>
    IScoreFunction[] GetScoreFunctions();

    ///// <summary>
    ///// Get score functions of the search engine
    ///// </summary>
    ///// <returns>Score functions</returns>
    //IScoreFunctions GetScoreFunctions();

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

    IScoreFunction FindScoreFunction(string scoreName);

    /// <summary>
    /// Get peptide format to save the parsing result
    /// </summary>
    /// <returns>Format reader/writer</returns>
    IIdentifiedPeptideTextFormat GetPeptideFormat(bool notExportSummary = false);
  }

  public abstract class AbstractSearchEngineFactory : ISearchEngineFactory
  {
    public AbstractSearchEngineFactory(SearchEngineType engineType)
    {
      this.EngineType = engineType;
    }

    public SearchEngineType EngineType { get; private set; }

    public IScoreFunction FindScoreFunction(string scoreName)
    {
      var lst = GetScoreFunctions();
      foreach (var scoreFunction in lst)
      {
        if (scoreFunction.ScoreName.Equals(scoreName))
        {
          return scoreFunction;
        }
      }

      return lst[0];
    }

    public abstract ISpectrumParser GetParser(string name, bool extractRank2);

    public abstract List<IIdentifiedSpectrum> GetHighConfidentPeptides(List<IIdentifiedSpectrum> source);

    public abstract IDatasetOptions GetOptions();

    public abstract IScoreFunction[] GetScoreFunctions();

    public virtual IIdentifiedPeptideTextFormat GetPeptideFormat(bool notExportSummary = false)
    {
      return new MascotPeptideTextFormat()
      {
        NotExportSummary = notExportSummary
      };
    }
  }
}
