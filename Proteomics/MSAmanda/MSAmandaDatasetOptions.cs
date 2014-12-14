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

namespace RCPA.Proteomics.MSAmanda
{
  public class MSAmandaDatasetOptions : AbstractScoreDatasetOptions
  {
    public MSAmandaDatasetOptions()
    {
      this.SearchEngine = SearchEngineType.MSAmanda;
    }

    public override IDatasetBuilder GetBuilder()
    {
      return new MSAmandaDatasetBuilder(this);
    }

    public override UserControl CreateControl()
    {
      var result = new MSAmandaDatasetPanel();
      result.Options = this;
      return result;
    }
  }
}
