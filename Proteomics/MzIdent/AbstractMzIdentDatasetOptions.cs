using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using RCPA.Proteomics.Summary;
using System.Windows.Forms;
using RCPA.Proteomics.PFind;
using RCPA.Proteomics.MzIdent;
using RCPA.Proteomics.Summary.Uniform;

namespace RCPA.Proteomics.MzIdent
{
  public abstract class AbstractMzIdentDatasetOptions : AbstractExpectValueDatasetOptions
  {
    public AbstractMzIdentDatasetOptions()
    {
      this.SearchEngine = GetSearchEngine();
    }

    protected abstract SearchEngineType GetSearchEngine();

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
  }
}
