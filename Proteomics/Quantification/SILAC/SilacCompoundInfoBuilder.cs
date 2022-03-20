using RCPA.Proteomics.Isotopic;
using System;
using System.Collections.Generic;

namespace RCPA.Proteomics.Quantification.SILAC
{
  public class SilacCompoundInfo
  {
    private readonly CompoundInfo heavy;
    private readonly CompoundInfo light;

    public SilacCompoundInfo(CompoundInfo light, CompoundInfo heavy)
    {
      this.light = light;
      this.heavy = heavy;
    }

    public CompoundInfo Light
    {
      get { return this.light; }
    }

    public CompoundInfo Heavy
    {
      get { return this.heavy; }
    }

    public bool IsSilacData()
    {
      return light.Mz != heavy.Mz;
    }

    private static bool IsMzEquals(double theoreticalMz, double observedMz, double mzTolerance)
    {
      return Math.Abs(theoreticalMz - observedMz) < mzTolerance;
    }

    public bool IsMzEquals(double observedMz, double mzTolerance)
    {
      return IsMzEquals(light.Mz, observedMz, mzTolerance) || IsMzEquals(heavy.Mz, observedMz, mzTolerance);
    }
  }

  public class SilacCompoundInfoBuilder
  {
    private readonly ISilacIsotopeFile silacFile;

    public SilacCompoundInfoBuilder(string silacFilename, bool isMonoisotopic)
    {
      if (isMonoisotopic)
      {
        this.silacFile = new MonoisotopicSilacIsotopeFile(silacFilename);
      }
      else
      {
        this.silacFile = new AverageSilacIsotopeFile(silacFilename);
      }
    }

    public SilacCompoundInfo Build(IPeptideInfo peptideInfo)
    {
      var cis = new List<CompoundInfo>();

      double sampleMz = this.silacFile.SampleCalculator.GetMz(peptideInfo.Sequence, peptideInfo.Charge);

      AtomComposition sampleAtomComposition = this.silacFile.SampleAtomCompositionCalculator.GetAtomComposition(peptideInfo);

      cis.Add(new CompoundInfo(true, sampleMz, sampleAtomComposition, peptideInfo.Charge));

      double refMz = this.silacFile.ReferenceCalculator.GetMz(peptideInfo.Sequence, peptideInfo.Charge);

      AtomComposition refAtomComposition = this.silacFile.ReferenceAtomCompositionCalculator.GetAtomComposition(peptideInfo);

      cis.Add(new CompoundInfo(false, refMz, refAtomComposition, peptideInfo.Charge));

      //sort by ascending compoundMz
      cis.Sort();

      return new SilacCompoundInfo(cis[0], cis[1]);
    }

    public bool IsModificationDefined(char modifiedChar)
    {
      return this.silacFile.IsModificationDefined(modifiedChar);
    }
  }
}