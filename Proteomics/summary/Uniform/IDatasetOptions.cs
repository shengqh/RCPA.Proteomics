using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Windows.Forms;

namespace RCPA.Proteomics.Summary.Uniform
{
  public interface IDatasetOptions : IXml
  {
    string Name { get; set; }

    bool Enabled { get; set; }

    BuildSummaryOptions Parent { get; set; }

    SearchEngineType SearchEngine { get; set; }

    bool SearchedByDifferentParameters { get; set; }

    bool FilterByPrecursor { get; set; }

    bool FilterByPrecursorIsotopic { get; set; }

    bool FilterByPrecursorDynamicTolerance { get; set; }

    double PrecursorPPMTolerance { get; set; }

    IScoreFunction ScoreFunction { get; set; }

    IFilter<IIdentifiedSpectrum> GetFilter();

    List<string> PathNames { get; set; }

    List<IIdentifiedSpectrum> Spectra { get; set; }

    IDatasetBuilder GetBuilder();

    UserControl CreateControl();

    IOptimalResultCalculator GetOptimalResultCalculator();
  }
}
