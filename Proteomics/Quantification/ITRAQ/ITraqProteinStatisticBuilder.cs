using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RCPA.Proteomics.Quantification;
using RCPA.Proteomics.Summary;
using RCPA.Proteomics.Sequest;
using MathNet.Numerics.Statistics;
using System.IO;
using RCPA.Utils;
using RCPA.Proteomics.Utils;
using RCPA.Proteomics.Quantification.ITraq;
using RCPA.Proteomics.Mascot;
using MathNet.Numerics.Distributions;
using RCPA.Numerics;

namespace RCPA.Proteomics.Quantification.ITraq
{
  public class ITraqProteinStatisticBuilder : AbstractITraqProteinStatisticBuilder
  {
    public ITraqProteinStatisticBuilder(ITraqProteinStatisticOption option)
      : base(option)
    { }

    private MascotResultTextFormat format;

    protected override IIdentifiedResult GetIdentifiedResult(string fileName)
    {
      format = new MascotResultTextFormat();
      return format.ReadFromFile(fileName);
    }

    protected override MascotResultTextFormat GetFormat(IIdentifiedResult ir)
    {
      format.InitializeByResult(ir);
      return format;
    }
  }
}
