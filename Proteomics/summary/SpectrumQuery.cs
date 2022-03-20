using RCPA.Proteomics.Summary;

namespace RCPA.Proteomics
{
  public class SpectrumQuery : ISpectrumQuery
  {
    #region ISpectrumQuery Members

    private SequestFilename fileScan = new SequestFilename();

    public SequestFilename FileScan
    {
      get { return fileScan; }
      set
      {
        if (value != null) fileScan = value;
        else fileScan = new SequestFilename();
      }
    }

    public int Charge
    {
      get { return fileScan.Charge; }
      set { fileScan.Charge = value; }
    }

    public double ObservedMz { get; set; }

    public int QueryId { get; set; }

    public int MatchCount { get; set; }

    public string Title { get; set; }

    public double RetentionTime { get; set; }

    public double RetentionLength { get; set; }

    #endregion
  }
}
