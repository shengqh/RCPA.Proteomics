namespace RCPA.Proteomics.Summary.Uniform
{
  public class UniformSpectrumPeptideFdrBuilder : AbstractIdentifiedSpectrumBuilder
  {
    public DatasetList BuildResult { get; private set; }

    #region IIdentifiedSpectrumBuilder Members

    protected override IdentifiedSpectrumBuilderResult DoBuild(string parameterFile)
    {
      var fdrCalc = Options.FalseDiscoveryRate.GetFalseDiscoveryRateCalculator();

      BuildResult = new DatasetList();

      //从配置进行初始化
      BuildResult.InitFromOptions(Options.DatasetList, this.Progress, parameterFile);

      string optimalFile = FileUtils.ChangeExtension(parameterFile, ".optimal");

      new OptimalFileTextWriter().WriteToFile(optimalFile, BuildResult);

      Progress.SetMessage("Peptide fdr filter done ...");
      return new IdentifiedSpectrumBuilderResult()
      {
        Spectra = BuildResult.GetSpectra(),
        PeptideFDR = Options.FalseDiscoveryRate.MaxPeptideFdr
      };
    }

    #endregion
  }
}
