using System;
using RCPA.Proteomics.Summary;

namespace RCPA.Proteomics.Sequest
{
  public delegate IIdentifiedSpectrum SequestPeptideParser(string line);

  public class SequestPeptideHit : IdentifiedSpectrum
  {
    public SequestPeptideHit()
    {
      ClusterNode = "";
      ProteinFromOutFile = false;
    }

    public double DiffOfMassPPM
    {
      get
      {
        if (0 == Query.Charge)
        {
          throw new Exception("Assign charge first!");
        }

        return PrecursorUtils.mz2ppm(ExperimentalMH/Query.Charge, TheoreticalMinusExperimentalMass/Query.Charge);
      }
    }

    public string ClusterNode { get; set; }

    //Is protein information built from outfile?
    private bool ProteinFromOutFile { get; set; }

    public int TrypsinSite { get; set; }

    public bool NeedCheckTrypsin { get; set; }

    //other
    public int Index { get; set; }

    public void Clear()
    {
      DigestProtease = null;
      ClusterNode = "";
      ClearPeptides();
      Annotations.Clear();
    }
  }
}