using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RCPA.Proteomics.Statistic
{
  public class RawIonStatisticMainBuilder : AbstractParallelMainProcessor
  {
    private double productIonPPM;
    
    private double minRelativeIntensity;

    public RawIonStatisticMainBuilder(IEnumerable<string> ASourceFiles, double productIonPPM, double minRelativeIntensity)
      : base(ASourceFiles)
    {
      this.productIonPPM = productIonPPM;
      this.minRelativeIntensity = minRelativeIntensity;
    }

    protected override IParallelTaskFileProcessor GetTaskProcessor(string targetDir, string fileName)
    {
      return new RawIonStatisticTaskBuilder(targetDir, productIonPPM, minRelativeIntensity);
    }
  }
}
