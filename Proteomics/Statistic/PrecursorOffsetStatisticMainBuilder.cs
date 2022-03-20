using System.Collections.Generic;

namespace RCPA.Proteomics.Statistic
{
  public class PrecursorOffsetStatisticMainBuilder : AbstractParallelMainFileProcessor
  {
    private double productIonPPM;

    private double minRelativeIntensity;

    private double minFrequency;

    public PrecursorOffsetStatisticMainBuilder(IEnumerable<string> ASourceFiles, double productIonPPM, double minRelativeIntensity, double minFrequency)
      : base(ASourceFiles)
    {
      this.productIonPPM = productIonPPM;
      this.minRelativeIntensity = minRelativeIntensity;
      this.minFrequency = 0.05;
    }

    protected override IParallelTaskFileProcessor GetTaskProcessor(string targetDir, string fileName)
    {
      var options = new RawIonStatisticTaskBuilderOptions()
      {
        InputFile = fileName,
        MinRelativeIntensity = minRelativeIntensity,
        ProductIonPPM = productIonPPM,
        TargetDirectory = targetDir,
        MinFrequency = minFrequency
      };
      return new PrecursorOffsetStatisticTaskBuilder(options);
    }
  }
}
