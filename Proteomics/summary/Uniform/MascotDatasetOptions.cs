using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using RCPA.Proteomics.Summary;
using System.Windows.Forms;
using RCPA.Proteomics.Mascot;

namespace RCPA.Proteomics.Summary.Uniform
{
  public class MascotDatasetOptions : AbstractExpectValueDatasetOptions
  {
    public MascotDatasetOptions()
    {
      this.SearchEngine = SearchEngineType.MASCOT;
    }

    public override IDatasetBuilder GetBuilder()
    {
      return new MascotDatasetBuilder(this);
    }

    public override UserControl CreateControl()
    {
      var result = new MascotDatasetPanel();

      result.Options = this;

      return result;
    }

    protected override OptimalResultCalculator NewOptimalResultCalculator()
    {
      return new MascotOptimalScoreCalculator();
    }
  }
}
