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

namespace RCPA.Proteomics.Sequest
{
  public class SequestDatasetBuilder : AbstractOneParserDatasetBuilder<SequestDatasetOptions>
  {
    public SequestDatasetBuilder(SequestDatasetOptions options) : base(options) { }
  }
}