using System;
using RCPA.Proteomics.Spectrum;
using RCPA.Proteomics.Isotopic;
using System.Collections.Generic;

namespace RCPA.Proteomics.Quantification
{
  public class CompoundInfo : IComparable<CompoundInfo>
  {
    public readonly bool IsSample;

    public readonly double Mz;

    public readonly AtomComposition Composition;

    public readonly int Charge;

    public List<Peak> Profile { get; set; }

    public CompoundInfo(bool isSample, double compoundMz, AtomComposition compoundAtomComposition, int compoundCharge)
    {
      this.IsSample = isSample;
      this.Mz = compoundMz;
      this.Composition = compoundAtomComposition;
      this.Charge = compoundCharge;
    }

    #region IComparable<CompoundInfo> Members

    public int CompareTo(CompoundInfo other)
    {
      if (null == other)
      {
        return -1;
      }

      return Mz.CompareTo(other.Mz);
    }

    #endregion
  }
}