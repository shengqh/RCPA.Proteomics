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
using RCPA.Proteomics.Sequest;
using RCPA.Proteomics.Isotopic;
using MathNet.Numerics.Statistics;
using RCPA.Proteomics.Summary;
using RCPA.Proteomics.Spectrum;
using RCPA.Proteomics.Quantification.SILAC;

namespace RCPA.Proteomics.Quantification.SILAC
{/*
  public class SilacQuantificationPeptideFileProcessor : AbstractThreadFileProcessor
  {
    private string _rawDir;

    private double _ppmTolerance;

    private SilacCompoundInfoBuilder _sciBuilder;

    private IIdentifiedSpectrumListFormat resultFormat;

    private static readonly int PROFILE_LENGTH = 3;

    private double maxDeltaMz = 0.1;

    private string silacParamFile;

    private string ignoreModifications;

    public SilacQuantificationPeptideFileProcessor(string rawDir, string silacParamFile, double ppmTolerance, IIdentifiedSpectrumListFormat resultFormat, string ignoreModifications)
    {
      this.silacParamFile = silacParamFile;
      this._rawDir = rawDir;
      this._sciBuilder = new SilacCompoundInfoBuilder(silacParamFile, true);
      this._ppmTolerance = ppmTolerance;
      this.resultFormat = resultFormat;
      this.ignoreModifications = ignoreModifications;
    }

    public SilacQuantificationPeptideFileProcessor(string rawDir, string silacParamFile, double ppmTolerance, IIdentifiedSpectrumListFormat resultFormat)
    {
      this.silacParamFile = silacParamFile;
      this._rawDir = rawDir;
      this._sciBuilder = new SilacCompoundInfoBuilder(silacParamFile, true);
      this._ppmTolerance = ppmTolerance;
      this.resultFormat = resultFormat;
      this.ignoreModifications = string.Empty;
    }

    private class DifferentRetentionTimeEnvelopes : List<SilacEnvelopes>
    {
      public List<double> LightProfile { get; set; }

      public List<double> HeavyProfile { get; set; }
    }

    public override IEnumerable<string> Process(string filename)
    {
      string resultFile = filename + ".SILACsummary";
      FileInfo resultFI = new FileInfo(resultFile);
      DirectoryInfo detailDir = new DirectoryInfo(resultFI.DirectoryName + "\\" + resultFI.Name + ".details");
      if (!detailDir.Exists)
      {
        detailDir.Create();
      }

      List<IIdentifiedSpectrum> spectra = resultFormat.ReadFromFile(filename);

      Dictionary<string, List<IIdentifiedSpectrum>> filePepMap = IdentifiedSpectrumUtils.GetRawPeptideMap(spectra);

      IRawFile rawReader = new RawFileImpl();
      IIsotopicProfileBuilder profileBuilder = new MolecularWeightCalculatorIsotopicProfileBuilder();

      List<IIdentifiedSpectrum> wrongIdentification = new List<IIdentifiedSpectrum>();

      Dictionary<IIdentifiedSpectrum, SilacQuantificationSummaryItem> pepResultMap = new Dictionary<IIdentifiedSpectrum, SilacQuantificationSummaryItem>();
      int fileCount = 0;
      foreach (string rawFilename in filePepMap.Keys)
      {
        fileCount++;
        FileInfo raw = new FileInfo(_rawDir + "\\" + rawFilename + ".RAW");

        if (Progress.IsCancellationPending())
        {
          throw new UserTerminatedException();
        }

        Progress.SetMessage(MyConvert.Format("{0}/{1} : Processing {2} ...", fileCount, filePepMap.Keys.Count, raw.FullName));

        rawReader.Open(raw.FullName);

        Console.Out.WriteLine(rawFilename);
        try
        {
          int firstScanNumber = rawReader.GetFirstSpectrumNumber();
          int lastScanNumber = rawReader.GetLastSpectrumNumber();

          List<IIdentifiedSpectrum> peps = filePepMap[rawFilename];

          Accumulator systemError = IdentifiedSpectrumUtils.GetDeltaPrecursorPPMAccumulator(peps);

          //double meanPPM = -0.5200;
          //double precursorPPM = 0.6880 * 3;

          double meanPPM = systemError.Mean;
          double precursorPPM;
          if (double.IsInfinity(systemError.Sigma) || double.IsNaN(systemError.Sigma))
          {
            precursorPPM = _ppmTolerance;
          }
          else
          {
            precursorPPM = systemError.Sigma * 3;
          }

          Dictionary<int, PeakList<Peak>> scanPklMap = new Dictionary<int, PeakList<Peak>>();

          Dictionary<string, DifferentRetentionTimeEnvelopes> peptideChargeMap = new Dictionary<string, DifferentRetentionTimeEnvelopes>();
          Dictionary<SilacEnvelopes, List<IIdentifiedSpectrum>> pklMpMap = new Dictionary<SilacEnvelopes, List<IIdentifiedSpectrum>>();

          Progress.SetRange(1, peps.Count);
          int pepCount = 0;
          foreach (IIdentifiedSpectrum spectrum in peps)
          {
            SilacQuantificationSummaryItem.ClearAnnotation(spectrum);

            if (Progress.IsCancellationPending())
            {
              throw new UserTerminatedException();
            }

            Progress.SetPosition(pepCount++);

            IIdentifiedPeptide sp = spectrum.Peptide;

            string seq = spectrum.GetMatchSequence();

            if (ignoreModifications != null && ignoreModifications.Length > 0)
            {
              foreach (var c in ignoreModifications)
              {
                seq = seq.Replace(c.ToString(), "");
              }
            }

            IPeptideInfo peptideInfo = new IdentifiedPeptideInfo(seq, spectrum.TheoreticalMH, spectrum.Query.Charge);
            SilacCompoundInfo sci = _sciBuilder.Build(peptideInfo);
            if (sci.Light.Mz == sci.Heavy.Mz)
            {
              spectrum.Annotations[SilacQuantificationConstants.SILAC_RATIO] = "NOT_SILAC";
              continue;
            }

            bool inRange = Math.Abs(sci.Light.Mz - spectrum.ObservedMz) < maxDeltaMz || Math.Abs(sci.Heavy.Mz - spectrum.ObservedMz) < maxDeltaMz;
            if (!inRange)
            {
              char[] modifiedChars = PeptideUtils.GetModifiedChar(seq);
              if (modifiedChars.Length > 0)
              {
                foreach (char c in modifiedChars)
                {
                  if (!_sciBuilder.IsModificationDefined(c))
                  {
                    throw new ArgumentException(MyConvert.Format("Modification {0} was not defined in SILAC configuration file : {1}.", c, silacParamFile));
                  }
                }
              }

              spectrum.Annotations[SilacQuantificationConstants.SILAC_RATIO] = "WRONG_IDENTIFICATION";
              continue;
            }

            int startScan = spectrum.Query.FileScan.FirstScan;
            int identifiedFullScan = 0;
            if (!rawReader.FindPreviousFullScan(startScan - 1, firstScanNumber, ref identifiedFullScan))
            {
              spectrum.Annotations[SilacQuantificationConstants.SILAC_RATIO] = "NO_PROFILE";
              continue;
            }

            int theoreticalMass = (int)(sci.Light.Mz * sci.Light.Charge + 0.5);
            int charge = spectrum.Charge;

            List<string> keys = new List<string>();
            foreach (IIdentifiedPeptide peptide in spectrum.Peptides)
            {
              string sequenceCharge = PeptideUtils.GetPureSequence(peptide.Sequence) + "." + charge + "." + theoreticalMass;
              keys.Add(sequenceCharge);
            }

            DifferentRetentionTimeEnvelopes pkls = null;
            foreach (string key in keys)
            {
              if (peptideChargeMap.ContainsKey(key))
              {
                pkls = peptideChargeMap[key];
                break;
              }
            }

            if (pkls == null)
            {
              pkls = new DifferentRetentionTimeEnvelopes();
              pkls.LightProfile = profileBuilder.GetProfile(sci.Light.Composition, PROFILE_LENGTH);
              pkls.HeavyProfile = profileBuilder.GetProfile(sci.Heavy.Composition, PROFILE_LENGTH);
            }

            foreach (string key in keys)
            {
              if (!peptideChargeMap.ContainsKey(key))
              {
                peptideChargeMap.Add(key, pkls);
              }
            }

            bool bFound = false;
            foreach (SilacEnvelopes se in pkls)
            {
              if (!se.ContainScan(identifiedFullScan))
              {
                continue;
              }

              pklMpMap[se].Add(spectrum);
              bFound = true;

              se.Find(m => m.Scan == identifiedFullScan).IsIdentified = true;
            }

            if (bFound) //already got profile
            {
              continue;
            }

            //find profile from raw file
            int maxIndex = CollectionUtils.FindMaxIndex(pkls.LightProfile);

            double mzTolerance = PrecursorUtils.ppm2mz(sci.Light.Mz, _ppmTolerance);

            SilacPeakListPair splp = GetLightHeavyPeakList(rawReader, scanPklMap, sci, maxIndex, mzTolerance, identifiedFullScan);
            if (null == splp)
            {
              spectrum.Annotations[SilacQuantificationConstants.SILAC_RATIO] = "NO_PROFILE";
              continue;
            }

            splp.IsIdentified = true;

            SilacEnvelopes envelopes = new SilacEnvelopes();
            envelopes.Add(splp);

            //backward
            int fullScan = identifiedFullScan;
            while (rawReader.FindPreviousFullScan(fullScan - 1, firstScanNumber, ref fullScan))
            {
              var item = GetLightHeavyPeakList(rawReader, scanPklMap, sci, maxIndex, mzTolerance, fullScan);
              if (null == item)
              {
                break;
              }

              envelopes.Add(item);
            }
            envelopes.Reverse();

            //forward
            fullScan = identifiedFullScan;
            while (rawReader.FindNextFullScan(fullScan + 1, lastScanNumber, ref fullScan))
            {
              var item = GetLightHeavyPeakList(rawReader, scanPklMap, sci, maxIndex, mzTolerance, fullScan);
              if (null == item)
              {
                break;
              }

              envelopes.Add(item);
            }

            envelopes.ForEach(m => m.CalculateIntensity(pkls.LightProfile, pkls.HeavyProfile));

            pkls.Add(envelopes);

            pklMpMap.Add(envelopes, new List<IIdentifiedSpectrum>());
            pklMpMap[envelopes].Add(spectrum);
          }

          foreach (string sequenceCharge in peptideChargeMap.Keys)
          {
            DifferentRetentionTimeEnvelopes pkls = peptideChargeMap[sequenceCharge];
            foreach (SilacEnvelopes envelopes in pkls)
            {
              if (0 == envelopes.Count)
              {
                continue;
              }

              List<IIdentifiedSpectrum> mps = pklMpMap[envelopes];
              double mzTolerance = PrecursorUtils.ppm2mz(mps[0].Query.ObservedMz, _ppmTolerance);

              String scanStr = GetScanRange(envelopes);

              string resultFilename = detailDir + "\\" + mps[0].Query.FileScan.Experimental + "." + PeptideUtils.GetPureSequence(mps[0].Sequence) + "." + mps[0].Query.Charge + scanStr + ".silac";

              IPeptideInfo peptideInfo = new IdentifiedPeptideInfo(mps[0].GetMatchSequence(), mps[0].TheoreticalMH, mps[0].Query.Charge);
              SilacCompoundInfo sci = _sciBuilder.Build(peptideInfo);

              SilacQuantificationSummaryItem item = new SilacQuantificationSummaryItem(sci.Light.IsSample);
              item.RawFilename = raw.FullName;
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
        }
        finally
        {
          rawReader.Close();
        }
      }

      List<IIdentifiedSpectrum> peptides = sr.GetSpectra();
      foreach (IIdentifiedSpectrum mph in peptides)
      {
        mph.SetEnabled(SilacQuantificationConstants.IsRatioValid(mph));
      }

      foreach (IIdentifiedProteinGroup mpg in sr)
      {
        SilacQuantificationUtils.CalculateProteinRatio(mpg);
        bool valid = SilacQuantificationConstants.IsRatioValid(mpg[0]);
        foreach (IIdentifiedProtein protein in mpg)
        {
          protein.SetEnabled(valid);
        }
      }

      resultFormat.InitializeByResult(sr);
      resultFormat.WriteToFile(resultFile, sr);

      return new[] { resultFile };
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

    private SilacPeakListPair GetLightHeavyPeakList(IRawFile rawFile, Dictionary<int, PeakList<Peak>> scanPklMap, SilacCompoundInfo sci, int maxIndex, double mzTolerance, int scan)
    {
      PeakList<Peak> light = GetCorrespondingEnvelope(rawFile, scanPklMap, sci.Light.Mz, sci.Light.Charge, mzTolerance, scan);
      PeakList<Peak> heavy = GetCorrespondingEnvelope(rawFile, scanPklMap, sci.Heavy.Mz, sci.Heavy.Charge, mzTolerance, scan);

      if (!CheckPeakList(light, maxIndex, sci.Light.Charge))
      {
        return null;
      }

      if (!CheckPeakList(heavy, maxIndex, sci.Heavy.Charge))
      {
        return null;
      }

      //if the monoisotopic peak of both light and heavy have no charge, 
      //throw current envelope and exit the loop
      if (0 == light[maxIndex].Intensity && 0 == heavy[maxIndex].Intensity)
      {
        return null;
      }

      light.RemoveRange(PROFILE_LENGTH, light.Count - PROFILE_LENGTH);
      heavy.RemoveRange(PROFILE_LENGTH, heavy.Count - PROFILE_LENGTH);

      return new SilacPeakListPair(light, heavy);
    }

    private static bool CheckPeakList(PeakList<Peak> light, int maxIndex, int charge)
    {
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

    private PeakList<Peak> GetCorrespondingEnvelope(IRawFile rawFile, Dictionary<int, PeakList<Peak>> scanPklMap, double theoreticalMz, int charge, double mzTolerance, int scan)
    {
      if (!scanPklMap.ContainsKey(scan))
      {
        scanPklMap[scan] = rawFile.GetPeakList(scan);
        scanPklMap[scan].ScanTimes.Add(new ScanTime(scan, 0.0));
      }

      PeakList<Peak> pkl = scanPklMap[scan];

      PeakList<Peak> envelope = pkl.FindEnvelopeDirectly(theoreticalMz, charge, mzTolerance, PROFILE_LENGTH, () => new Peak());
      return envelope;
    }

    private static void MergeTogether(double mzTolerance, PeakList<Peak> mergedEnvelopes, PeakList<Peak> envelope)
    {
      if (mergedEnvelopes.Count < envelope.Count)
      {
        PeakList<Peak> tmp = new PeakList<Peak>(mergedEnvelopes);
        mergedEnvelopes.Clear();
        mergedEnvelopes.AddRange(envelope);
        mergedEnvelopes.AddToCurrentPeakListIntensity(tmp, mzTolerance);
      }
      else
      {
        mergedEnvelopes.AddToCurrentPeakListIntensity(envelope, mzTolerance);
      }
    }

    private Dictionary<string, List<IIdentifiedSpectrum>> GetFilePeptideMap(IIdentifiedResult sr)
    {
      Dictionary<string, List<IIdentifiedSpectrum>> result = new Dictionary<string, List<IIdentifiedSpectrum>>();

      List<IIdentifiedSpectrum> peptides = sr.GetSpectra();
      foreach (IIdentifiedSpectrum sph in peptides)
      {
        string filename = _rawDir + "/" + sph.Query.FileScan.Experimental + ".raw";
        if (!result.ContainsKey(filename))
        {
          result[filename] = new List<IIdentifiedSpectrum>();
        }
        result[filename].Add(sph);
      }

      return result;
    }
  }
  */ 
}
