using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using RCPA.Proteomics.Summary;
using System.Windows.Forms;
using RCPA.Proteomics.PFind;
using RCPA.Proteomics.MzIdent;

namespace RCPA.Proteomics.MSGF
{
  public class MSGFDatasetOptions: AbstractMzIdentDatasetOptions
  {
    protected override SearchEngineType GetSearchEngine()
    {
      return SearchEngineType.MSGF;
    }
  }
}
