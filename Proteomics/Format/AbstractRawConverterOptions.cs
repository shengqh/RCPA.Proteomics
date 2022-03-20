namespace RCPA.Proteomics.Format
{
  public abstract class AbstractRawConverterOptions
  {
    public AbstractRawConverterOptions()
    {
      this.ExtractRawMS3 = false;
      this.Overwrite = true;
    }

    public string TargetDirectory { get; set; }

    public bool ExtractRawMS3 { get; set; }

    public bool GroupByMode { get; set; }

    public bool GroupByMsLevel { get; set; }

    public bool Overwrite { get; set; }

    public abstract string Extension { get; }
  }
}
