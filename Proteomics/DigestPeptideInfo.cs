using System.Collections.Generic;

namespace RCPA.Proteomics
{
  public class DigestPeptideInfo
  {
    private Dictionary<string, object> annotations;

    public string ProteinName { get; set; }

    public string PeptideSeq { get; set; }

    /// <summary>
    /// One-based index of sequence
    /// </summary>
    public RangeLocation PeptideLoc { get; set; }

    public int MissCleavage { get; set; }

    public Dictionary<string, object> Annotations
    {
      get
      {
        if (this.annotations == null)
        {
          this.annotations = new Dictionary<string, object>();
        }

        return this.annotations;
      }
    }

    public override string ToString()
    {
      return PeptideSeq;
    }
  }
}