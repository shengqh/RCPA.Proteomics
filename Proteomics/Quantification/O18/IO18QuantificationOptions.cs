namespace RCPA.Proteomics.Quantification.O18
{
  public interface IO18QuantificationOptions : IPairQuantificationOptions
  {
    IFileFormat<O18QuantificationSummaryItem> GetIndividualFileFormat();
  }
}
