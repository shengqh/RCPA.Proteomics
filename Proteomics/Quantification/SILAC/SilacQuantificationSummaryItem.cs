using System.Collections.Generic;
using System.IO;
using System.Linq;
using System;
using RCPA.Proteomics.Spectrum;
using MathNet.Numerics.Statistics;
using RCPA.Utils;
using RCPA.Proteomics.Isotopic;
using MathNet.Numerics.Distributions;

namespace RCPA.Proteomics.Quantification.SILAC
{
  public class SilacQuantificationSummaryItem : IIsotopicProfiles
  {
    private double referenceAbundance;
    private double sampleAbundance;

    public SilacQuantificationSummaryItem(bool sampleIsLight)
    {
      this.SampleIsLight = sampleIsLight;
      this.sampleAbundance = 0.0;
      this.referenceAbundance = 0.0;
      this.ProfileLength = 0;
    }

    public string RawFilename { get; set; }

    public string SoftwareVersion { get; set; }

    public string PeptideSequence { get; set; }

    public int Charge { get; set; }

    public bool SampleIsLight { get; set; }

    public string SampleAtomComposition { get; set; }

    public string ReferenceAtomComposition { get; set; }

    public List<Peak> SampleProfile { get; set; }

    public List<Peak> ReferenceProfile { get; set; }

    public int ProfileLength { get; set; }

    public List<Peak> LightProfile
    {
      get
      {
        return SampleIsLight ? SampleProfile : ReferenceProfile;
      }
      set
      {
        if (SampleIsLight)
          SampleProfile = value;
        else
          ReferenceProfile = value;
      }
    }

    public List<Peak> HeavyProfile
    {
      get
      {
        return SampleIsLight ? ReferenceProfile : SampleProfile;
      }
      set
      {
        if (SampleIsLight)
          ReferenceProfile = value;
        else
          SampleProfile = value;
      }
    }

    public string LightAtomComposition
    {
      get
      {
        return SampleIsLight ? SampleAtomComposition : ReferenceAtomComposition;
      }
      set
      {
        if (SampleIsLight)
          SampleAtomComposition = value;
        else
          ReferenceAtomComposition = value;
      }
    }

    public string HeavyAtomComposition
    {
      get
      {
        return SampleIsLight ? ReferenceAtomComposition : SampleAtomComposition;
      }
      set
      {
        if (SampleIsLight)
          ReferenceAtomComposition = value;
        else
          SampleAtomComposition = value;
      }
    }

    public double Ratio { get; set; }

    public double RegressionCorrelation { get; set; }

    public double SampleAbundance
    {
      get
      {
        if (this.sampleAbundance == 0.0)
        {
          InitAbundanceFromEnvelopes();
        }
        return this.sampleAbundance;
      }
      set { this.sampleAbundance = value; }
    }

    public double ReferenceAbundance
    {
      get
      {
        if (this.referenceAbundance == 0.0)
        {
          InitAbundanceFromEnvelopes();
        }
        return this.referenceAbundance;
      }
      set { this.referenceAbundance = value; }
    }

    public SilacEnvelopes ObservedEnvelopes { get; set; }

    public List<IsotopicProfile> GetIsotopicProfiles()
    {
      List<IsotopicProfile> result = new List<IsotopicProfile>();

      result.Add(new IsotopicProfile()
      {
        Profile = this.SampleProfile,
        DisplayName = "Sample",
        DisplayColor = SilacQuantificationConstants.SAMPLE_COLOR
      });

      result.Add(new IsotopicProfile()
      {
        Profile = this.ReferenceProfile,
        DisplayName = "Reference",
        DisplayColor = SilacQuantificationConstants.REFERENCE_COLOR
      });

      return result;
    }

    public void InitAbundanceFromEnvelopes()
    {
      if (Ratio > 1)
      {
        this.sampleAbundance = 0.0;
        foreach (SilacPeakListPair pair in ObservedEnvelopes)
        {
          if (!pair.Enabled)
          {
            continue;
          }

          double curSampleAbundance = SampleIsLight ? pair.LightIntensity : pair.HeavyIntensity;
          if (curSampleAbundance > this.sampleAbundance)
          {
            this.sampleAbundance = curSampleAbundance;
          }
        }

        this.referenceAbundance = this.sampleAbundance / Ratio;
      }
      else
      {
        this.referenceAbundance = 0.0;
        foreach (SilacPeakListPair pair in ObservedEnvelopes)
        {
          if (!pair.Enabled)
          {
            continue;
          }

          double curRefAbundance = SampleIsLight ? pair.HeavyIntensity : pair.LightIntensity;
          if (curRefAbundance > this.referenceAbundance)
          {
            this.referenceAbundance = curRefAbundance;
          }
        }

        this.sampleAbundance = this.referenceAbundance * Ratio;
      }
    }

    public void AssignToAnnotation(IAnnotation mph, string resultFilename)
    {
      int observedCount = 0;
      foreach (SilacPeakListPair pkl in ObservedEnvelopes)
      {
        if (pkl.Enabled)
        {
          observedCount++;
        }
      }

      var item = mph.GetOrCreateQuantificationItem();
      item.Ratio = Ratio;
      item.Correlation = RegressionCorrelation;
      item.ScanCount = observedCount;
      item.SampleIntensity = SampleAbundance;
      item.ReferenceIntensity = ReferenceAbundance;
      item.Filename = new FileInfo(resultFilename).Name;
    }

    public void AssignDuplicationToAnnotation(IAnnotation mph, string resultFilename)
    {
      var item = mph.GetOrCreateQuantificationItem();
      item.RatioStr = "DUP";
      item.Filename = new FileInfo(resultFilename).Name;
    }

    public static void ClearAnnotation(IAnnotation mph)
    {
      mph.ClearQuantificationItem();
    }

    private double[][] GetIntensityArray()
    {
      var lightIntensities = new List<double>();
      var heavyIntensities = new List<double>();

      foreach (SilacPeakListPair splp in ObservedEnvelopes)
      {
        if (splp.Enabled)
        {
          lightIntensities.Add(splp.LightIntensity);
          heavyIntensities.Add(splp.HeavyIntensity);
        }
      }

      var result = new double[2][];
      if (SampleIsLight)
      {
        result[1] = lightIntensities.ToArray();
        result[0] = heavyIntensities.ToArray();
      }
      else
      {
        result[0] = lightIntensities.ToArray();
        result[1] = heavyIntensities.ToArray();
      }
      return result;
    }

    public void Smoothing()
    {
      double[] lights = (from splp in ObservedEnvelopes
                         select splp.LightIntensity).ToArray();
      lights = MathUtils.SavitzkyGolay7Point(lights);

      double[] heavys = (from splp in ObservedEnvelopes
                         select splp.HeavyIntensity).ToArray();
      heavys = MathUtils.SavitzkyGolay7Point(heavys);

      for (int i = 0; i < lights.Length; i++)
      {
        ObservedEnvelopes[i].LightIntensity = lights[i] > 0 ? lights[i] : 1;
        ObservedEnvelopes[i].HeavyIntensity = heavys[i] > 0 ? heavys[i] : 1;
      }
    }

    public void CalculateRatio()
    {
      double[][] intensities = GetIntensityArray();

      if (intensities[0].Length > 0)
      {
        LinearRegressionRatioResult lrrr = LinearRegressionRatioCalculator.CalculateRatio(intensities);
        Ratio = lrrr.Ratio;
        RegressionCorrelation = lrrr.RSquare;
      }
      else
      {
        Ratio = 1.0;
        RegressionCorrelation = 0.0;
      }

      InitAbundanceFromEnvelopes();
    }

    private Dictionary<SilacPeakListPair, double> GetPPMList_Old(Func<SilacPeakListPair, Peak> mzFunc)
    {
      var identified = ObservedEnvelopes.FindAll(m => m.IsIdentified);

      var mzs = from id in identified
                let peak = mzFunc(id)
                where peak.Intensity > 0
                select peak.Mz;

      if (mzs.Count() == 0)
      {
        return new Dictionary<SilacPeakListPair, double>();
      }

      double mz = mzs.Average();

      return (from ob in ObservedEnvelopes
              let peak = mzFunc(ob)
              where peak.Intensity > 0
              let ppm = PrecursorUtils.mz2ppm(peak.Mz, peak.Mz - mz)
              select new { OB = ob, PPM = ppm }).ToDictionary((m => m.OB), (m => m.PPM));
    }

    private Dictionary<SilacPeakListPair, double> GetPPMList(Func<SilacPeakListPair, Peak> mzFunc, double theoreticalMz)
    {
      var identified = ObservedEnvelopes.FindAll(m => m.IsIdentified);

      var mzs = from id in identified
                let peak = mzFunc(id)
                where peak.Intensity > 0
                select peak.Mz;

      if (mzs.Count() == 0)
      {
        return new Dictionary<SilacPeakListPair, double>();
      }

      double mz = mzs.Average();

      return (from ob in ObservedEnvelopes
              let peak = mzFunc(ob)
              where peak.Intensity > 0
              let ppm = PrecursorUtils.mz2ppm(peak.Mz, peak.Mz - mz)
              select new { OB = ob, PPM = ppm }).ToDictionary((m => m.OB), (m => m.PPM));
    }


    public Dictionary<SilacPeakListPair, double> GetSamplePPMList()
    {
      int maxIndex = SampleProfile.FindMaxIndex();

      if (SampleIsLight)
      {
        return GetPPMList((m => m.Light[maxIndex]), SampleProfile[maxIndex].Mz);
      }
      else
      {
        return GetPPMList((m => m.Heavy[maxIndex]), SampleProfile[maxIndex].Mz);
      }
    }

    public Dictionary<SilacPeakListPair, double> GetReferencePPMList()
    {
      int maxIndex = ReferenceProfile.FindMaxIndex();

      if (SampleIsLight)
      {
        return GetPPMList((m => m.Heavy[maxIndex]), ReferenceProfile[maxIndex].Mz);
      }
      else
      {
        return GetPPMList((m => m.Light[maxIndex]), ReferenceProfile[maxIndex].Mz);
      }
    }

    private bool AcceptScan(SilacPeakListPair pair, SilacCompoundInfo sci, int lightMaxIndex, double lightMinPPM, double lightMaxPPM, int heavyMaxIndex, double heavyMinPPM, double heavyMaxPPM)
    {
      //如果轻重标中任意一个最高峰不存在，循环结束。
      if (pair.Light[lightMaxIndex].Intensity == 0 || pair.Heavy[heavyMaxIndex].Intensity == 0)
      {
        return false;
      }

      //轻标最高峰的ppm
      double lightPPM = PrecursorUtils.mz2ppm(pair.Light[lightMaxIndex].Mz, sci.Light.Profile[lightMaxIndex].Mz - pair.Light[lightMaxIndex].Mz);
      if (lightPPM < lightMinPPM || lightPPM > lightMaxPPM)
      {
        return false;
      }

      //重标最高峰的ppm
      double heavyPPM = PrecursorUtils.mz2ppm(pair.Heavy[heavyMaxIndex].Mz, sci.Heavy.Profile[heavyMaxIndex].Mz - pair.Heavy[heavyMaxIndex].Mz);
      if (heavyPPM < heavyMinPPM || heavyPPM > heavyMaxPPM)
      {
        return false;
      }

      return true;
    }

    public void ValidateScans(SilacCompoundInfo sci, double ppmTolerance)
    {
      int lightMaxIndex = LightProfile.FindMaxIndex();
      int heavyMaxIndex = HeavyProfile.FindMaxIndex();

      var identified = ObservedEnvelopes.FindAll(m => m.IsIdentified);

      int minIdentifiedScan = identified.Min(m => m.Scan);
      int maxIdentifiedScan = identified.Max(m => m.Scan);

      ObservedEnvelopes.ForEach(m => m.Enabled = m.Scan >= minIdentifiedScan && m.Scan <= maxIdentifiedScan);

      int enabledCount = ObservedEnvelopes.FindAll(m => m.Enabled).Count;
      if (enabledCount < 5)
      {
        int preEnabled = (5 - enabledCount) / 2;
        if (preEnabled == 0)
        {
          preEnabled = 1;
        }
        int postEnabled = 5 - enabledCount - preEnabled;

        int first = ObservedEnvelopes.FindIndex(m => m.Enabled);
        for (int i = first - 1; i >= 0 && i >= first - preEnabled; i--)
        {
          ObservedEnvelopes[i].Enabled = true;
        }

        int last = ObservedEnvelopes.FindLastIndex(m => m.Enabled);
        for (int i = last + 1; i < ObservedEnvelopes.Count && i <= last + postEnabled; i++)
        {
          ObservedEnvelopes[i].Enabled = true;
        }
      }

      var enabled = ObservedEnvelopes.FindAll(m => m.Enabled);

      var lightAccumulator = new MeanStandardDeviation(from ob in enabled
                                                       let ppm = PrecursorUtils.mz2ppm(sci.Light.Profile[lightMaxIndex].Mz, sci.Light.Profile[lightMaxIndex].Mz - ob.Light[lightMaxIndex].Mz)
                                                       select ppm);

      var heavyAccumulator = new MeanStandardDeviation(from ob in enabled
                                                       let ppm = PrecursorUtils.mz2ppm(sci.Heavy.Profile[heavyMaxIndex].Mz, sci.Heavy.Profile[heavyMaxIndex].Mz - ob.Heavy[heavyMaxIndex].Mz)
                                                       select ppm);

      double lightPPM = Math.Max(ppmTolerance, lightAccumulator.StdDev * 3);
      double heavyPPM = Math.Max(ppmTolerance, heavyAccumulator.StdDev * 3);

      var lightMinPPM = lightAccumulator.Mean - lightPPM;
      var lightMaxPPM = lightAccumulator.Mean + lightPPM;
      var heavyMinPPM = heavyAccumulator.Mean - heavyPPM;
      var heavyMaxPPM = heavyAccumulator.Mean + heavyPPM;

      int fixedIndex = ObservedEnvelopes.FindIndex(m => m.IsIdentified);

      for (int i = fixedIndex; i >= 0; i--)
      {
        var pair = ObservedEnvelopes[i];
        if (pair.Enabled)
        {
          continue;
        }

        if (!AcceptScan(pair, sci, lightMaxIndex, lightMinPPM, lightMaxPPM, heavyMaxIndex, heavyMinPPM, heavyMaxPPM))
        {
          break;
        }

        pair.Enabled = true;
      }

      for (int i = fixedIndex; i < ObservedEnvelopes.Count; i++)
      {
        var pair = ObservedEnvelopes[i];
        if (pair.Enabled)
        {
          continue;
        }

        if (!AcceptScan(pair, sci, lightMaxIndex, lightMinPPM, lightMaxPPM, heavyMaxIndex, heavyMinPPM, heavyMaxPPM))
        {
          break;
        }

        pair.Enabled = true;
      }
    }

    public void CalculateCorrelation()
    {
      ObservedEnvelopes.ForEach(m => m.CalculateCorrelation(LightProfile, HeavyProfile));
    }
  }
}