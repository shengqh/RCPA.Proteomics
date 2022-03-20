using System;

namespace RCPA.Proteomics.Percolator
{
  public class PercolatorPeptideDistillerOptions
  {
    public PercolatorPeptideDistillerOptions()
    {
      PercolatorOutputFile = string.Empty;
    }

    public string PercolatorOutputFile { get; set; }

    private static string suffix = ".output.xml";

    public string PercolatorInputFile
    {
      get
      {
        if (PercolatorOutputFile.ToLower().EndsWith(suffix))
        {
          return PercolatorOutputFile.Substring(0, PercolatorOutputFile.Length - suffix.Length) + ".input.xml";
        }
        else
        {
          throw new Exception("Percolator output file must be end with .output.xml and percolator input file must be end with .input.xml");
        }
      }
    }
  }
}
