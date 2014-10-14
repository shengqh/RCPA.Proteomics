using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using RCPA.Proteomics.Summary;
using System.Windows.Forms;
using RCPA.Proteomics.PFind;
using RCPA.Proteomics.MzIdent;

namespace RCPA.Proteomics.Summary.Uniform
{
  public class MzIdentDatasetOptions : AbstractExpectValueDatasetOptions
  {
    public override IDatasetBuilder GetBuilder()
    {
      return new MzIdentDatasetBuilder(this);
    }

    public override UserControl CreateControl()
    {
      var result = new MzIdentDatasetPanel();

      result.Options = this;

      return result;
    }

    public ISpectrumParser GetParser()
    {
      var result = MzIdentParserFactory.Find(this.SearchEngine.ToString());
      result.TitleParser = GetTitleParser();
      return result;
    }

    protected override OptimalResultCalculator NewOptimalResultCalculator()
    {
      return new OptimalResultCalculator(GetParser().GetScoreFunctions());
    }
  }
}
