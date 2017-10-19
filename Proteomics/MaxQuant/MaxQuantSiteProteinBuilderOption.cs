using System.IO;

namespace RCPA.Proteomics.MaxQuant
{
  public class MaxQuantSiteProteinBuilderOption
  {
    public string MaxQuantModificationXml { get; set; }
    public string MaxQuantMSMSFile { get; set; }
    public string MaxQuantSiteFile { get; set; }
    public string OutputFilePrefix { get; set; }
  }
}