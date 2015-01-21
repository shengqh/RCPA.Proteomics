using System;
using System.Collections.Generic;
using System.IO;
using RCPA.Proteomics.Summary;
using RCPA.Utils;
using RCPA.Gui;
using RCPA.Proteomics.Mascot;

namespace RCPA.Proteomics.Summary.Uniform
{
  public abstract class AbstractDatasetBuilder<T> : ProgressClass, IDatasetBuilder where T : IDatasetOptions
  {
    protected T options;

    public AbstractDatasetBuilder(T options)
    {
      this.options = options;
    }

    #region IDatasetBuilder Members

    public IDatasetOptions Options { get { return options; } }

    public virtual List<IIdentifiedSpectrum> ParseFromSearchResult()
    {
      var result = DoParse();

      DoAfterParse(result);

      return result;
    }

    protected void DoAfterParse(List<IIdentifiedSpectrum> result)
    {
      Progress.SetMessage("Remove same spectrum matched with different peptides ...");
      IdentifiedSpectrumUtils.RemoveSpectrumWithAmbigiousAssignment(result);

      if (Options.FilterByPrecursor && Options.FilterByPrecursorDynamicTolerance)
      {
        Progress.SetMessage(MyConvert.Format("Filtering by precursor mass tolerance ...", result.Count));
        List<IIdentifiedSpectrum> highconfidents = options.SearchEngine.GetFactory().GetHighConfidentPeptides(result);
        DynamicPrecursorPPMFilter filter = new DynamicPrecursorPPMFilter(Options.PrecursorPPMTolerance, Options.FilterByPrecursorIsotopic);
        var sFilter = filter.GetFilter(highconfidents);
        result.RemoveAll(m => !sFilter.Accept(m));
      }

      if (Options.Parent != null && Options.Parent.Database.RemoveContamination)
      {
        var filter = Options.Parent.Database.GetContaminationNameFilter();

        if (null != filter)
        {
          Progress.SetMessage(MyConvert.Format("Filtering by contamination name : {0}", filter.ToString()));
          result.RemoveAll(m => filter.Accept(m));
        }
      }

      Progress.SetMessage(MyConvert.Format("Merging same spectrum from different search parameters ...", result.Count));
      IdentifiedSpectrumUtils.KeepTopPeptideFromSameEngineDifferentParameters(result);
      result.TrimExcess();
      GC.Collect();
      GC.WaitForPendingFinalizers();
      Progress.SetMessage(MyConvert.Format("Total {0} peptides passed minimum tolerance criteria.", result.Count));

      Progress.SetMessage("Parsing protein access number ...");
      IdentifiedSpectrumUtils.ResetProteinByAccessNumberParser(result, options.Parent.Database.GetAccessNumberParser());
      Progress.SetMessage("Parsing protein access number finished.");
    }

    public virtual void InitializeQValue(List<IIdentifiedSpectrum> spectra)
    {
      IScoreFunction scoreFunctions = Options.ScoreFunction;

      CalculateQValueFunc qValueFunc = Options.Parent.FalseDiscoveryRate.GetQValueFunction();

      IFalseDiscoveryRateCalculator fdrCalc = Options.Parent.FalseDiscoveryRate.GetFalseDiscoveryRateCalculator();

      qValueFunc(spectra, scoreFunctions, fdrCalc);
    }

    protected abstract List<IIdentifiedSpectrum> DoParse();

    #endregion
  }
}