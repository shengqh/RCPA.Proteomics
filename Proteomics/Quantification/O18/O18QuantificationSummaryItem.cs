using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using RCPA.Utils;
using RCPA.Proteomics.Spectrum;

namespace RCPA.Proteomics.Quantification.O18
{
  public class SpeciesAbundanceInfo
  {
    private readonly List<SpeciesRegressionItem> regressionItems = new List<SpeciesRegressionItem>();
    public double O16 { get; set; }

    public double O181 { get; set; }

    public double O182 { get; set; }

    public double RegressionCorrelation { get; set; }

    public List<SpeciesRegressionItem> RegressionItems
    {
      get { return this.regressionItems; }
    }
  }

  public class SampleAbundanceInfo
  {
    private double ratio;
    public double O16 { get; set; }

    public double O18 { get; set; }

    public double Ratio
    {
      get
      {
        if (this.ratio == 0.0)
        {
          CalculateRatio();
        }
        return this.ratio;
      }
      set { this.ratio = value; }
    }

    public double LabellingEfficiency { get; set; }

    public void CalculateRatio(double maxRatio)
    {
      double minRatio = 1 / maxRatio;

      if (O16 == 0)
      {
        this.ratio = maxRatio;
        return;
      }

      if (O18 == 0)
      {
        this.ratio = minRatio;
        return;
      }

      this.ratio = Math.Max(minRatio, Math.Min(maxRatio, O18 / O16));
    }

    public void CalculateRatio()
    {
      CalculateRatio(50.0);
    }
  }

  public class O18QuantificationSummaryItem
  {
    private const string EnabledKey = "Enabled";
    private string additionalFormula = "";

    private List<Peak> peptideProfile = new List<Peak>();

    public string RawFilename { get; set; }

    public string SoftwareVersion { get; set; }

    public string PeptideSequence { get; set; }

    private double theoreticalO16Mz;
    public double TheoreticalO16Mz
    {
      get
      {
        if (this.theoreticalO16Mz == 0)
        {
          this.theoreticalO16Mz = GetTheoreticalO16Mz();
        }
        return this.theoreticalO16Mz;
      }
      set
      {
        this.theoreticalO16Mz = value;
      }
    }

    private double GetTheoreticalO16Mz()
    {
      var mzs = (from envelope in this.ObservedEnvelopes
                 where envelope.IsIdentified
                 select envelope[0].Mz);

      if (mzs.Count() > 0)
      {
        return mzs.Average();
      }

      return (from envelope in this.ObservedEnvelopes
              orderby envelope[0].Intensity descending
              select envelope[0].Mz).First();
    }

    private int charge;
    public int Charge
    {
      get
      {
        if (charge == 0)
        {
          charge = GetCharge();
        }
        return charge;
      }
      set
      {
        charge = value;
      }
    }

    private int GetCharge()
    {
      if (ObservedEnvelopes.Count > 0)
      {
        return (int)Math.Round(1.0 / (ObservedEnvelopes[0][1].Mz - ObservedEnvelopes[0][0].Mz));
      }

      return 0;
    }

    public string AdditionalFormula
    {
      get { return this.additionalFormula; }
      set
      {
        if (value == null)
        {
          this.additionalFormula = "";
        }
        else
        {
          this.additionalFormula = value;
        }
      }
    }

    public string PeptideAtomComposition { get; set; }

    public double PurityOfO18Water { get; set; }

    public bool IsPostDigestionLabelling { get; set; }

    public double ScanStartPercentage { get; set; }

    public double ScanEndPercentage { get; set; }

    public List<Peak> PeptideProfile
    {
      get { return this.peptideProfile; }
      set { this.peptideProfile = value; }
    }

    public List<O18QuanEnvelope> ObservedEnvelopes { get; set; }

    public SpeciesAbundanceInfo SpeciesAbundance { get; set; }

    public SampleAbundanceInfo SampleAbundance { get; set; }

    public O18QuantificationSummaryItem()
    {
      this.SpeciesAbundance = new SpeciesAbundanceInfo();
      this.SampleAbundance = new SampleAbundanceInfo();
      this.ObservedEnvelopes = new List<O18QuanEnvelope>();
      this.ScanStartPercentage = 0.0;
      this.ScanEndPercentage = 1.0;
    }

    public void InitializeScanRange()
    {
      int startIndex = (int)(this.ObservedEnvelopes.Count * this.ScanStartPercentage);
      int firstIdentified = this.ObservedEnvelopes.FindIndex(m => m.IsIdentified);
      startIndex = Math.Min(startIndex, firstIdentified);

      int endIndex = (int)(Math.Round(this.ObservedEnvelopes.Count * this.ScanEndPercentage)) - 1;
      int lastIdentified = this.ObservedEnvelopes.FindLastIndex(m => m.IsIdentified);
      endIndex = Math.Max(endIndex, lastIdentified);

      for (int i = 0; i < this.ObservedEnvelopes.Count; i++)
      {
        this.ObservedEnvelopes[i].Enabled = i >= startIndex && i <= endIndex;
      }
    }

    public void CalculateSpeciesAbundanceByLinearRegression()
    {
      var mergedPeaks = MergeEnvelopes();

      int maxLength = Math.Min(PeptideProfile.Count + 4, mergedPeaks.Count);

      double[][] a = GetProfileMatrix(maxLength);

      double[] x;

      this.SpeciesAbundance = CalculateSpeciesAbundance(maxLength, a, mergedPeaks, out x);

      double[] theoreticalIntensities = GetTheoreticalIntensities(maxLength, x);
      double[] observedIntensities = GetObservedIntensities(maxLength, mergedPeaks);

      double corr = StatisticsUtils.PearsonCorrelation(theoreticalIntensities, observedIntensities);

      this.SpeciesAbundance.RegressionCorrelation = corr;
      this.SpeciesAbundance.RegressionItems.Clear();

      int pepCharge = GetCharge();
      var gap = (Atom.O18.MonoMass - Atom.O.MonoMass) / pepCharge;
      var gapC = (Atom.C13.MonoMass - Atom.C.MonoMass) / pepCharge;
      double mzO16 = TheoreticalO16Mz;
      double mzO181 = TheoreticalO16Mz + gap;
      double mzO182 = mzO181 + gap;
      double[] mzs = { mzO16, mzO16 + gapC, mzO181, mzO181 + gapC, mzO182, mzO182 + gapC };

      for (int i = 0; i < maxLength && i < mzs.Length; i++)
      {
        var ri = new SpeciesRegressionItem();
        ri.Mz = mzs[i];
        ri.ObservedIntensity = observedIntensities[i];
        ri.RegressionIntensity = theoreticalIntensities[i];
        this.SpeciesAbundance.RegressionItems.Add(ri);
      }

      var calc = this.GetCalculator();
      this.SampleAbundance = calc.Calculate(this.SpeciesAbundance);
    }

    /// <summary>
    /// 将可用的PeakList的强度合并为一个总的PeakList，用于线性回归计算。
    /// </summary>
    /// <returns>合并后的PeakList</returns>
    private PeakList<Peak> MergeEnvelopes()
    {
      var mergedPeaks = new PeakList<Peak>();

      var initPeaks = this.ObservedEnvelopes.Where(m => m.Enabled).ToList();

      foreach (var envelope in initPeaks)
      {
        int maxCount = Math.Min(6, envelope.Count);
        for (int i = 0; i < maxCount; i++)
        {
          if (i >= mergedPeaks.Count)
          {
            mergedPeaks.Add(new Peak(envelope[i].Mz, envelope[i].Intensity));
          }
          else
          {
            mergedPeaks[i].Intensity = mergedPeaks[i].Intensity + envelope[i].Intensity;
          }
        }
      }
      return mergedPeaks;
    }

    private void CopyProfileMatrix(double[][] a, double[][] target)
    {
      for (int i = 0; i < target.Length; i++)
      {
        for (int j = 0; j < target[i].Length; j++)
        {
          target[i][j] = a[i][j];
        }
      }
    }

    public void CalculateIndividualAbundance()
    {
      if (ObservedEnvelopes.Count == 0)
      {
        return;
      }

      var calc = this.GetCalculator();

      int maxLength = ObservedEnvelopes[0].Count;

      double[][] a = GetProfileMatrix(maxLength);
      double[][] pp = GetProfileMatrix(maxLength);
      double[] x;

      foreach (var envelope in ObservedEnvelopes)
      {
        if (!envelope.Enabled)
        {
          continue;
        }

        CopyProfileMatrix(a, pp);

        envelope.SpeciesAbundance = CalculateSpeciesAbundance(maxLength, pp, envelope, out x);

        envelope.SampleAbundance = calc.Calculate(envelope.SpeciesAbundance);
      }
    }

    private SpeciesAbundanceInfo CalculateSpeciesAbundance(int maxLength, double[][] profileMatrix, PeakList<Peak> envelope, out double[] x)
    {
      double[] b = GetObservedIntensities(maxLength, envelope);

      x = new double[3];

      double rnorm;

      NonNegativeLeastSquaresCalc.NNLS(profileMatrix, maxLength, 3, b, x, out rnorm, null, null, null);

      var result = new SpeciesAbundanceInfo();

      result.O16 = x[0];
      result.O181 = x[1];
      result.O182 = x[2];

      return result;
    }

    private double[] GetTheoreticalIntensities(int maxLength, double[] x)
    {
      var result = new double[maxLength];

      double[][] profileMatrix = GetProfileMatrix(maxLength);
      for (int i = 0; i < maxLength; i++)
      {
        double intentity = 0.0;
        for (int j = 0; j < 3; j++)
        {
          intentity += profileMatrix[j][i] * x[j];
        }
        result[i] = intentity;
      }

      return result;
    }

    private double[] GetObservedIntensities(int maxLength, PeakList<Peak> envelope)
    {
      var result = new double[maxLength];
      for (int i = 0; i < maxLength; i++)
      {
        result[i] = envelope[i].Intensity;
      }
      return result;
    }

    private double[][] GetProfileMatrix(int maxLength)
    {
      var a = new double[3][];
      for (int i = 0; i < 3; i++)
      {
        a[i] = new double[maxLength];
        for (int j = 0; j < maxLength; j++)
        {
          a[i][j] = 0.0;
        }
      }

      //O16
      for (int j = 0; j < PeptideProfile.Count && j < maxLength; j++)
      {
        a[0][j] = PeptideProfile[j].Intensity;
      }

      //O181
      for (int j = 2; j < PeptideProfile.Count && j < maxLength; j++)
      {
        a[1][j] += PeptideProfile[j - 2].Intensity;
      }

      //O182
      for (int j = 4; j < PeptideProfile.Count && j < maxLength; j++)
      {
        a[2][j] += PeptideProfile[j - 4].Intensity;
      }

      return a;
    }

    public void AssignToAnnotation(IAnnotation mph, string resultFilename)
    {
      int observedCount = 0;
      foreach (var pkl in this.ObservedEnvelopes)
      {
        if (pkl.Enabled)
        {
          observedCount++;
        }
      }

      mph.Annotations[O18QuantificationConstants.O18_RATIO] = SampleAbundance.Ratio;
      mph.Annotations[O18QuantificationConstants.O18_RATIO_SCANCOUNT] = observedCount;
      mph.Annotations[O18QuantificationConstants.INIT_O16_INTENSITY] = SpeciesAbundance.O16;
      mph.Annotations[O18QuantificationConstants.INIT_O18_1_INTENSITY] = SpeciesAbundance.O181;
      mph.Annotations[O18QuantificationConstants.INIT_O18_2_INTENSITY] = SpeciesAbundance.O182;

      mph.Annotations[O18QuantificationConstants.O18_INTENSITY] = SampleAbundance.O18;
      mph.Annotations[O18QuantificationConstants.O16_INTENSITY] = SampleAbundance.O16;

      mph.Annotations[O18QuantificationConstants.O18_LABELING_EFFICIENCY] = MyConvert.Format("{0:0.00}",
                                                                                          SampleAbundance.
                                                                                            LabellingEfficiency);
      mph.Annotations[O18QuantificationConstants.O18_RATIO_FILE] = new FileInfo(resultFilename).Name;
    }

    public void AssignDuplicationToAnnotation(IAnnotation mph, string resultFilename)
    {
      mph.Annotations[O18QuantificationConstants.O18_RATIO] = "DUP";
      mph.Annotations[O18QuantificationConstants.O18_RATIO_SCANCOUNT] = "";

      mph.Annotations[O18QuantificationConstants.INIT_O16_INTENSITY] = "";
      mph.Annotations[O18QuantificationConstants.INIT_O18_1_INTENSITY] = "";
      mph.Annotations[O18QuantificationConstants.INIT_O18_2_INTENSITY] = "";

      mph.Annotations[O18QuantificationConstants.O18_INTENSITY] = "";
      mph.Annotations[O18QuantificationConstants.O16_INTENSITY] = "";

      mph.Annotations[O18QuantificationConstants.O18_LABELING_EFFICIENCY] = "";

      mph.Annotations[O18QuantificationConstants.O18_RATIO_FILE] = new FileInfo(resultFilename).Name;
    }

    public List<O18PPMEntry> GetPPMList()
    {
      int pepCharge = GetCharge();

      var gap = (Atom.O18.MonoMass - Atom.O.MonoMass) / pepCharge;

      double mzO16 = TheoreticalO16Mz;
      double mzO181 = TheoreticalO16Mz + gap;
      double mzO182 = mzO181 + gap;

      var result = new List<O18PPMEntry>();

      ObservedEnvelopes.ForEach(m =>
      {
        var entry = new O18PPMEntry() { Scan = m.Scan };
        entry.O16 = m[0].Intensity > 0 ? PrecursorUtils.mz2ppm(m[0].Mz, m[0].Mz - mzO16) : double.NaN;
        entry.O181 = m[2].Intensity > 0 ? PrecursorUtils.mz2ppm(m[2].Mz, m[2].Mz - mzO181) : double.NaN;
        entry.O182 = m[4].Intensity > 0 ? PrecursorUtils.mz2ppm(m[4].Mz, m[4].Mz - mzO182) : double.NaN;
        result.Add(entry);
      });

      return result;
    }

    public ISampleAbundanceCalculator GetCalculator()
    {
      if (this.IsPostDigestionLabelling)
      {
        return new O18SampleAbundanceLabellingPostDigestionCalculator(this.PurityOfO18Water);
      }

      return new O18SampleAbundanceLabellingDuringDigestionCalculator(this.PurityOfO18Water);
    }

  }
}