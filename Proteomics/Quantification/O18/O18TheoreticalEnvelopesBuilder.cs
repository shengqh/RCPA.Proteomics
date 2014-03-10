using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RCPA.Proteomics.Spectrum;
using RCPA.Proteomics.Summary;
using RCPA.Proteomics;

namespace RCPA.Tools.Quantification.O18
{
  public class O18TheoreticalEnvelopesBuilder : ITheoreticalEnvelopesBuilder
  {
    private static readonly double gapO18O16 = Atom.O18.MonoMass - Atom.O.MonoMass;

    private int profileLength = 6;

    public O18TheoreticalEnvelopesBuilder() : this(6) { }

    public O18TheoreticalEnvelopesBuilder(int profileLength)
    {
      this.profileLength = profileLength;
    }

    #region ITheoreticalEnvelopesBuilder Members

    public List<Envelope> Build(IIdentifiedSpectrum mphit)
    {
      double theoreticalMz = mphit.GetTheoreticalMz();
      if (mphit.Modifications.Contains("18O(1)"))
      {
        theoreticalMz -= gapO18O16 / mphit.Query.Charge;
      }
      else if (mphit.Modifications.Contains("18O(2)"))
      {
        theoreticalMz -= (gapO18O16 * 2) / mphit.Query.Charge;
      }

      List<Envelope> result = new List<Envelope>();

      result.Add(new Envelope(theoreticalMz, mphit.Query.Charge, profileLength));

      return result;
    }

    #endregion
  }
}
