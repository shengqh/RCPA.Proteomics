using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using RCPA.Proteomics.Summary;
using System.Windows.Forms;
using RCPA.Proteomics.Mascot;
using RCPA.Proteomics.PeptideProphet;
using RCPA.Proteomics.Summary.Uniform;

namespace RCPA.Proteomics.PeptideProphet
{
  public class InterProphetDatasetOptions : PeptideProphetDatasetOptions
  {
    public InterProphetDatasetOptions()
    {
      this.SearchEngine = SearchEngineType.iPhophet;
    }

    public override IDatasetBuilder GetBuilder()
    {
      return new InterProphetDatasetBuilder(this);
    }
  }
}
