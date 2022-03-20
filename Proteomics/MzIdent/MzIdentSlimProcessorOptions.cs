namespace RCPA.Proteomics.MzIdent
{
  public class MzIdentSlimProcessorOptions
  {
    public string[] SourceFiles { get; set; }

    public string TargetDirectory { get; set; }

    public bool Overwrite { get; set; }
  }
}
