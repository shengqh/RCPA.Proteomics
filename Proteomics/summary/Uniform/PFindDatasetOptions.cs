using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using RCPA.Proteomics.Summary;
using System.Windows.Forms;
using RCPA.Proteomics.PFind;

namespace RCPA.Proteomics.Summary.Uniform
{
  public class PFindDatasetOptions : AbstractExpectValueDatasetOptions
  {
    public override SearchEngineType SearchEngine
    {
      get { return SearchEngineType.PFIND; }
    }

    public override IDatasetBuilder GetBuilder()
    {
      return new PFindDatasetBuilder(this);
    }

    public override UserControl CreateControl()
    {
      var result = new PFindDatasetPanel();

      result.Option = this;

      return result;
    }

    protected override OptimalResultCalculator NewOptimalResultCalculator()
    {
      return new PFindOptimalExpectValueCalculator();
    }
  }
}
