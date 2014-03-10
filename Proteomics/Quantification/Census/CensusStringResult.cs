using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RCPA.Proteomics.Summary;

namespace RCPA.Proteomics.Quantification.Census
{
  public class CensusStringResult
  {
    private List<string> headers = new List<string>();

    private List<ProteinGroupEntry> proteins = new List<ProteinGroupEntry>();

    public List<string> Headers
    {
      get { return headers; }
      set { this.headers = value; }
    }

    public List<ProteinGroupEntry> Proteins
    {
      get { return proteins; }
      set { this.proteins = value; }
    }
  }
}
