using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using RCPA.Proteomics.Summary;
using RCPA.Utils;
using System.Diagnostics;
using RCPA.Seq;
using RCPA.Gui;
using RCPA.Proteomics.Sequest;
using RCPA.Proteomics.Comet;
using RCPA.Proteomics.ProteomeDiscoverer;
using RCPA.Proteomics.Summary.Uniform;

namespace RCPA.Proteomics.Comet
{
  public class CometDatasetBuilder : AbstractOneParserDatasetBuilder<CometDatasetOptions>
  {
    public CometDatasetBuilder(CometDatasetOptions options) : base(options) { }
  }
}