using RCPA.Proteomics.Summary;

namespace RCPA.Proteomics.Quantification.O18
{
  public class O18QuantificationResultMultipleFileWriter : AbstractIdentifiedResultMultipleFileWriter
  {
    public O18QuantificationResultMultipleFileWriter() : base() { }

    public O18QuantificationResultMultipleFileWriter(string[] perferAccessNumberRegexString) : base(perferAccessNumberRegexString) { }

    protected override string GetProteinHeader()
    {
      return "\tMW\tUniquePeptideCount\tLR_Ratio\tLR_RSquare\tLR_FCalc\tLR_FProbability";
    }

    protected override string GetPeptideHeader()
    {
      return "\tReference\t\"File, Scan(s)\"\tSequence\tINIT_O16_INTENSITY\tINIT_O18(1)_INTENSITY\tINIT_O18(2)_INTENSITY\tO18_LABELING_EFFICIENCY\tO18_RATIO";
    }

    protected override string GetPeptideFileName(string proteinFileName)
    {
      return FileUtils.ChangeExtension(proteinFileName, ".PepO18Quant");
    }
  }
}