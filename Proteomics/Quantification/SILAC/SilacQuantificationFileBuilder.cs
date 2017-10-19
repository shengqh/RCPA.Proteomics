using System;
using System.Collections.Generic;
using System.Text;
using RCPA;
using RCPA.Proteomics.Mascot;
using RCPA.Proteomics.Raw;
using RCPA.Proteomics.Quantification;
using RCPA.Proteomics;
using RCPA.Utils;
using RCPA.Proteomics.Utils;
using System.IO;
using System.Linq;
using RCPA.Proteomics.Sequest;
using RCPA.Proteomics.Isotopic;
using MathNet.Numerics.Statistics;
using RCPA.Proteomics.Summary;
using RCPA.Proteomics.Spectrum;
using RCPA.Gui;

namespace RCPA.Proteomics.Quantification.SILAC
{
  /// <summary>
  /// 根据给定的肽段列表，在一个raw文件中进行定量。
  /// </summary>
  public class SilacQuantificationFileBuilder : ProgressClass
  {
    private static readonly double MAX_DELTA_MZ = 0.1;

    private SilacCompoundInfoBuilder _sciBuilder;

    private IRawFile _rawReader;

    private IIsotopicProfileBuilder2 profileBuilder = new EmassProfileBuilder();

    public string SoftwareVersion { get; set; }

    private SilacQuantificationOption option;
    public SilacQuantificationFileBuilder(SilacQuantificationOption option)
    {
      this.option = option;
      this._rawReader = new CacheRawFile(option.RawFormat.GetRawFile());
      this._sciBuilder = new SilacCompoundInfoBuilder(option.SilacParamFile, true);
    }

    public void Quantify(string rawFileName, List<IIdentifiedSpectrum> spectra, string detailDir)
    {
      if (!Directory.Exists(detailDir))
      {
        Directory.CreateDirectory(detailDir);
      }

      var experimental = RawFileFactory.GetExperimental(rawFileName);

      Dictionary<string, DifferentRetentionTimeEnvelopes> spectrumKeyMap = new Dictionary<string, DifferentRetentionTimeEnvelopes>();

      Dictionary<SilacEnvelopes, List<IIdentifiedSpectrum>> envelopeSpectrumGroup = new Dictionary<SilacEnvelopes, List<IIdentifiedSpectrum>>();

      double precursorPPM = GetPrecursorPPM(spectra);

      try
      {
        _rawReader.Open(rawFileName);

        int firstScanNumber = _rawReader.GetFirstSpectrumNumber();
        int lastScanNumber = _rawReader.GetLastSpectrumNumber();

        Progress.SetRange(1, spectra.Count);
        int pepCount = 0;

        for (int s = 0; s < spectra.Count; s++)
        {
          Console.WriteLine(s);
          IIdentifiedSpectrum spectrum = spectra[s];

          SilacQuantificationSummaryItem.ClearAnnotation(spectrum);

          if (Progress.IsCancellationPending())
          {
            throw new UserTerminatedException();
          }

          int startScan = spectrum.Query.FileScan.FirstScan;
          if (startScan > lastScanNumber)
          {
            spectrum.GetOrCreateQuantificationItem().RatioStr = "OUT_OF_RANGE";
            continue;
          }

          Progress.SetPosition(pepCount++);

          IIdentifiedPeptide sp = spectrum.Peptide;

          string seq = GetMatchSequence(spectrum);

          IPeptideInfo peptideInfo = new IdentifiedPeptideInfo(seq, spectrum.TheoreticalMH, spectrum.Query.Charge);

          SilacCompoundInfo sci = GetSilacCompoundInfo(peptideInfo);

          //如果轻重离子理论质荷比一样，忽略
          if (!sci.IsSilacData())
          {
            spectrum.GetOrCreateQuantificationItem().RatioStr = "NOT_SILAC";
            continue;
          }

          //如果轻重离子理论质荷比与观测值不一致，忽略
          if (!sci.IsMzEquals(spectrum.ObservedMz, MAX_DELTA_MZ))
          {
            ValidateModifications(seq);

            spectrum.GetOrCreateQuantificationItem().RatioStr = "WRONG_IDENTIFICATION";
            continue;
          }

          //如果没有找到相应的FullScan，忽略
          int identifiedFullScan = _rawReader.FindPreviousFullScan(startScan, firstScanNumber);
          if (-1 == identifiedFullScan)
          {
            spectrum.GetOrCreateQuantificationItem().RatioStr = "NO_PROFILE";
            continue;
          }

          DifferentRetentionTimeEnvelopes pkls = FindEnvelopes(spectrumKeyMap, spectrum, sci);

          SilacEnvelopes envelope = pkls.FindSilacEnvelope(identifiedFullScan);

          //如果该scan被包含在已经被定量的结果中，忽略
          if (envelope != null)
          {
            envelope.SetScanIdentified(identifiedFullScan, spectrum.IsExtendedIdentification());
            envelopeSpectrumGroup[envelope].Add(spectrum);
            continue;
          }

          //从原始文件中找出该spectrum的定量信息
          int maxIndex = Math.Min(option.ProfileLength - 1, pkls.LightProfile.FindMaxIndex());

          double mzTolerance = PrecursorUtils.ppm2mz(sci.Light.Mz, option.PPMTolerance);

          //如果FullScan没有相应的离子，忽略。（鉴定错误或者扩展定量时候，会出现找不到pair的现象）
          SilacPeakListPair splp = GetLightHeavyPeakList(_rawReader, sci, maxIndex, mzTolerance, identifiedFullScan);
          if (null == splp)
          {
            spectrum.GetOrCreateQuantificationItem().RatioStr = "NO_PROFILE";
            continue;
          }

          splp.IsIdentified = true;
          splp.IsExtendedIdentification = spectrum.IsExtendedIdentification();

          SilacEnvelopes envelopes = new SilacEnvelopes();
          envelopes.Add(splp);

          //向前查找定量信息
          int fullScan = identifiedFullScan;
          while ((fullScan = _rawReader.FindPreviousFullScan(fullScan - 1, firstScanNumber)) != -1)
          {
            if (_rawReader.IsBadDataScan(fullScan))
            {
              continue;
            }

            var item = GetLightHeavyPeakList(_rawReader, sci, maxIndex, mzTolerance, fullScan);
            if (null == item)
            {
              break;
            }

            envelopes.Add(item);
          }
          envelopes.Reverse();

          //向后查找定量信息
          fullScan = identifiedFullScan;
          while ((fullScan = _rawReader.FindNextFullScan(fullScan + 1, lastScanNumber)) != -1)
          {
            if (_rawReader.IsBadDataScan(fullScan))
            {
              continue;
            }

            var item = GetLightHeavyPeakList(_rawReader, sci, maxIndex, mzTolerance, fullScan);
            if (null == item)
            {
              break;
            }

            envelopes.Add(item);
          }

          //对每个scan计算轻重的离子丰度
          envelopes.ForEach(m => m.CalculateIntensity(pkls.LightProfile, pkls.HeavyProfile));

          pkls.Add(envelopes);

          envelopeSpectrumGroup.Add(envelopes, new List<IIdentifiedSpectrum>());
          envelopeSpectrumGroup[envelopes].Add(spectrum);
        }
      }
      finally
      {
        _rawReader.Close();
      }

      foreach (string key in spectrumKeyMap.Keys)
      {
        DifferentRetentionTimeEnvelopes pkls = spectrumKeyMap[key];

        foreach (SilacEnvelopes envelopes in pkls)
        {
          if (0 == envelopes.Count)
          {
            continue;
          }

          List<IIdentifiedSpectrum> mps = envelopeSpectrumGroup[envelopes];

          double mzTolerance = PrecursorUtils.ppm2mz(mps[0].Query.ObservedMz, option.PPMTolerance);

          string scanStr = GetScanRange(envelopes);

          string resultFilename = detailDir + "\\" + mps[0].Query.FileScan.Experimental + "." + PeptideUtils.GetPureSequence(mps[0].Sequence) + "." + mps[0].Query.Charge + scanStr + ".silac";

          IPeptideInfo peptideInfo = new IdentifiedPeptideInfo(mps[0].GetMatchSequence(), mps[0].TheoreticalMH, mps[0].Query.Charge);
          SilacCompoundInfo sci = GetSilacCompoundInfo(peptideInfo);

          SilacQuantificationSummaryItem item = new SilacQuantificationSummaryItem(sci.Light.IsSample);
          item.RawFilename = rawFileName;
          item.SoftwareVersion = this.SoftwareVersion;
          item.PeptideSequence = mps[0].Sequence;
          item.Charge = mps[0].Charge;
          item.LightAtomComposition = sci.Light.Composition.ToString();
          item.HeavyAtomComposition = sci.Heavy.Composition.ToString();
          item.LightProfile = pkls.LightProfile;
          item.HeavyProfile = pkls.HeavyProfile;
          item.ObservedEnvelopes = envelopes;

          item.ValidateScans(sci, precursorPPM);

          item.Smoothing();
          item.CalculateRatio();

          new SilacQuantificationSummaryItemXmlFormat().WriteToFile(resultFilename, item);

          int maxScoreItemIndex = FindMaxScoreItemIndex(mps);

          for (int i = 0; i < mps.Count; i++)
          {
            if (maxScoreItemIndex == i)
            {
              item.AssignToAnnotation(mps[i], resultFilename);
            }
            else
            {
              item.AssignDuplicationToAnnotation(mps[i], resultFilename);
            }
          }
        }
      }

      foreach (IIdentifiedSpectrum mph in spectra)
      {
        mph.InitializeRatioEnabled();
      }
    }

    private SilacCompoundInfo GetSilacCompoundInfo(IPeptideInfo peptideInfo)
    {
      SilacCompoundInfo sci = _sciBuilder.Build(peptideInfo);
      sci.Light.Profile = profileBuilder.GetProfile(sci.Light.Composition, sci.Light.Charge, 0.0001);
      sci.Heavy.Profile = profileBuilder.GetProfile(sci.Heavy.Composition, sci.Heavy.Charge, 0.0001);
      return sci;
    }

    private double GetPrecursorPPM(List<IIdentifiedSpectrum> spectra)
    {
      double precursorPPM = option.PPMTolerance;
      if (spectra.Count >= 5)
      {
        var systemError = IdentifiedSpectrumUtils.GetDeltaPrecursorPPMAccumulator(spectra);
        if (!double.IsInfinity(systemError.StdDev) && !double.IsNaN(systemError.StdDev))
        {
          precursorPPM = systemError.StdDev * 3;
        }
      }
      return precursorPPM;
    }

    private DifferentRetentionTimeEnvelopes FindEnvelopes(Dictionary<string, DifferentRetentionTimeEnvelopes> peptideChargeMap, IIdentifiedSpectrum spectrum, SilacCompoundInfo sci)
    {
      var keys = GetIdentifiedSpectrumKey(spectrum, sci);

      DifferentRetentionTimeEnvelopes result = null;

      foreach (string key in keys)
      {
        if (peptideChargeMap.ContainsKey(key))
        {
          result = peptideChargeMap[key];
          break;
        }
      }

      if (result == null)
      {
        result = new DifferentRetentionTimeEnvelopes();
        result.LightProfile = sci.Light.Profile;
        result.HeavyProfile = sci.Heavy.Profile;
      }

      foreach (string key in keys)
      {
        if (!peptideChargeMap.ContainsKey(key))
        {
          peptideChargeMap.Add(key, result);
        }
      }

      return result;
    }

    private List<string> GetIdentifiedSpectrumKey(IIdentifiedSpectrum spectrum, SilacCompoundInfo sci)
    {
      int theoreticalMass = (int)(sci.Light.Mz * sci.Light.Charge + 0.5);
      int charge = spectrum.Charge;

      List<string> keys = new List<string>();
      foreach (IIdentifiedPeptide peptide in spectrum.Peptides)
      {
        string sequenceCharge = PeptideUtils.GetPureSequence(peptide.Sequence) + "." + charge + "." + theoreticalMass;
        keys.Add(sequenceCharge);
      }
      return keys;
    }

    private void ValidateModifications(string seq)
    {
      char[] modifiedChars = PeptideUtils.GetModifiedChar(seq);
      if (modifiedChars.Length > 0)
      {
        foreach (char c in modifiedChars)
        {
          if (!_sciBuilder.IsModificationDefined(c))
          {
            throw new ArgumentException(MyConvert.Format("Modification {0} was not defined in SILAC configuration file : {1}.", c, option.SilacParamFile));
          }
        }
      }
    }

    private string GetMatchSequence(IIdentifiedSpectrum spectrum)
    {
      string result = spectrum.GetMatchSequence();

      if (option.IgnoreModifications != null && option.IgnoreModifications.Length > 0)
      {
        foreach (var c in option.IgnoreModifications)
        {
          result = result.Replace(c.ToString(), "");
        }
      }

      return result;
    }

    private static String GetScanRange(SilacEnvelopes envelopes)
    {
      List<int> scanList = new List<int>();
      foreach (SilacPeakListPair splp in envelopes)
      {
        scanList.Add(splp.Light.ScanTimes[0].Scan);
      }
      scanList.Sort();

      StringBuilder sb = new StringBuilder();
      sb.Append("." + scanList[0] + "-" + scanList[scanList.Count - 1]);

      String scanStr = sb.ToString();
      return scanStr;
    }

    private SilacPeakListPair GetLightHeavyPeakList(IRawFile rawFile, SilacCompoundInfo sci, int maxIndex, double mzTolerance, int scan)
    {
      PeakList<Peak> pkl = rawFile.GetPeakList(scan);
      if (pkl.Count == 0)
      {
        return null;
      }

      var scantime = new ScanTime(scan, rawFile.ScanToRetentionTime(scan));

      PeakList<Peak> light = pkl.FindEnvelopeDirectly(sci.Light.Profile, option.ProfileLength, mzTolerance, () => new Peak());

      if (!CheckPeakListCharge(light, maxIndex, sci.Light.Charge))
      {
        return null;
      }

      PeakList<Peak> heavy = pkl.FindEnvelopeDirectly(sci.Heavy.Profile, option.ProfileLength, mzTolerance, () => new Peak());

      //如果电荷不对，则认为该scan无效。
      if (!CheckPeakListCharge(heavy, maxIndex, sci.Heavy.Charge))
      {
        return null;
      }

      //如果轻或者重的总强度为0，则认为该scan无效。
      if (0 == light.Sum(m => m.Intensity) || 0 == heavy.Sum(m => m.Intensity))
      {
        return null;
      }

      light.ScanTimes.Add(scantime);
      heavy.ScanTimes.Add(scantime);

      return new SilacPeakListPair(light, heavy);
    }

    private static bool CheckPeakListCharge(PeakList<Peak> light, int maxIndex, int charge)
    {
      if (light.Count <= maxIndex)
      {
        return false;
      }

      if (light[maxIndex].Charge != 0 && light[maxIndex].Charge != charge)
      {
        return false;
      }

      return true;
    }

    private int FindMaxScoreItemIndex(List<IIdentifiedSpectrum> mps)
    {
      int result = 0;

      for (int i = 1; i < mps.Count; i++)
      {
        if (mps[i].Score > mps[result].Score)
        {
          result = i;
        }
      }

      return result;
    }
  }
}
