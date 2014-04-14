using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using RCPA;
using RCPA.Proteomics.Mascot;
using RCPA.Proteomics.Raw;
using RCPA.Proteomics;
using RCPA.Utils;
using RCPA.Proteomics.Utils;
using System.IO;
using RCPA.Proteomics.Summary;
using RCPA.Proteomics.Spectrum;

namespace RCPA.Proteomics.Quantification.O18
{
  public class O18QuantificationFileProcessor : AbstractThreadFileProcessor
  {
    private IFileFormat<O18QuantificationSummaryItem> fileFormat;

    private double gapO18O16 = Atom.O18.MonoMass - Atom.O.MonoMass;

    private Dictionary<int, double> chargeGapMap1 = new Dictionary<int, double>();

    private Dictionary<int, double> chargeGapMap2 = new Dictionary<int, double>();

    private O18QuantificationFileProcessorOptions options;

    public O18QuantificationFileProcessor()
    {
      this.fileFormat = new O18QuantificationSummaryItemXmlFormat();

      for (int i = 1; i < 20; i++)
      {
        chargeGapMap1[i] = gapO18O16 / i;
        chargeGapMap2[i] = (gapO18O16 * 2) / i;
      }
    }

    private class DifferentRetentionTimeEnvelopes : List<O18QuanEnvelopes> { }

    public override IEnumerable<string> Process(string optionFile)
    {
      this.options = O18QuantificationFileProcessorOptions.Load(optionFile);

      var calc = options.GetProteinRatioCalculator();
      var detailDirectory = options.GetDetailDirectory();
      if (!Directory.Exists(detailDirectory))
      {
        Directory.CreateDirectory(detailDirectory);
      }

      var format = new MascotResultTextFormat();

      IIdentifiedResult mr = format.ReadFromFile(options.ProteinFile);

      CheckRawFilename(mr, optionFile);

      Dictionary<string, List<IIdentifiedSpectrum>> filePepMap = GetFilePeptideMap(mr);

      Dictionary<IIdentifiedPeptide, O18QuantificationSummaryItem> pepResultMap = new Dictionary<IIdentifiedPeptide, O18QuantificationSummaryItem>();
      foreach (string filename in filePepMap.Keys)
      {
        Progress.SetMessage("Processing " + filename);

        string rawFilename = filename;
        if (new FileInfo(filename).Name.Equals("Cmpd.raw"))
        {
          rawFilename = FindRawFileName(options.ProteinFile);
        }

        string experimental = FileUtils.ChangeExtension(new FileInfo(rawFilename).Name, "");

        using (CacheRawFile rawFile = new CacheRawFile(rawFilename))
        {
          int firstScanNumber = rawFile.GetFirstSpectrumNumber();
          int lastScanNumber = rawFile.GetLastSpectrumNumber();

          List<IIdentifiedSpectrum> peps = filePepMap[filename];

          Dictionary<string, DifferentRetentionTimeEnvelopes> peptideChargeMap = new Dictionary<string, DifferentRetentionTimeEnvelopes>();
          Dictionary<O18QuanEnvelopes, List<IIdentifiedSpectrum>> pklMpMap = new Dictionary<O18QuanEnvelopes, List<IIdentifiedSpectrum>>();

          Progress.SetRange(0, peps.Count);
          foreach (IIdentifiedSpectrum mphit in peps)
          {
            if (Progress.IsCancellationPending())
            {
              throw new UserTerminatedException();
            }

            Progress.Increment(1);

            IIdentifiedPeptide mp = mphit.Peptide;

            if (mp.Sequence.EndsWith("-"))
            {
              //it cannot be O18 labelled, just skip it
              continue;
            }

            int startScan = mphit.Query.FileScan.FirstScan;

            double theoreticalMz = GetTheoretialO16Mz(gapO18O16, mphit);

            int theoreticalMass = (int)(theoreticalMz * mphit.Query.Charge + 0.5);
            string sequenceCharge = PeptideUtils.GetPureSequence(mphit.Sequence) + "." + mphit.Query.Charge + "." + theoreticalMass;
            if (!peptideChargeMap.ContainsKey(sequenceCharge))
            {
              peptideChargeMap.Add(sequenceCharge, new DifferentRetentionTimeEnvelopes());
            }

            bool bFound = false;
            DifferentRetentionTimeEnvelopes pkls = peptideChargeMap[sequenceCharge];
            foreach (var pklList in pkls)
            {
              if (pklList.Count == 0)
              {
                continue;
              }

              if (pklList[0].Scan > startScan)
              {
                continue;
              }

              if (pklList[pklList.Count - 1].Scan < startScan)
              {
                continue;
              }

              pklMpMap[pklList].Add(mphit);
              bFound = true;

              bool findIdentified = false;
              for (int i = 1; i < pklList.Count; i++)
              {
                if (pklList[i].ScanTimes[0].Scan > startScan)
                {
                  pklList[i - 1].IsIdentified = true;
                  findIdentified = true;
                  break;
                }
              }

              if (!findIdentified)
              {
                pklList[pklList.Count - 1].IsIdentified = true;
              }
            }

            if (bFound)
            {
              continue;
            }

            double mzTolerance = PrecursorUtils.ppm2mz(theoreticalMz, options.PPMTolerance);

            O18QuanEnvelopes envelopes = new O18QuanEnvelopes();

            bool bFirst = true;

            int count = 0;
            //backward
            for (int scan = startScan; scan >= firstScanNumber; scan--)
            {
              if (1 == rawFile.GetMsLevel(scan))
              {
                O18QuanEnvelope envelope = GetCorrespondingEnvelope(rawFile, theoreticalMz, mphit.Query.Charge, mzTolerance, scan);

                //At most one invalid scan inside both pre or post identification scan range.
                if (!IsValidEnvelope(envelope, mphit.Charge))
                {
                  if (count > 0)
                  {
                    envelopes.RemoveAt(0);
                    break;
                  }
                  else
                  {
                    count++;
                  }
                }
                else
                {
                  count = 0;
                }

                if (bFirst)
                {
                  envelope.IsIdentified = true;
                  bFirst = false;
                }

                envelopes.Insert(0, envelope);
              }
            }

            if (envelopes.Count == 0)
            {
              //If the identified scan has no quantification information ,ignore it.
              continue;
            }

            count = 0;
            //forward
            for (int scan = startScan + 1; scan <= lastScanNumber; scan++)
            {
              if (1 == rawFile.GetMsLevel(scan))
              {
                var envelope = GetCorrespondingEnvelope(rawFile, theoreticalMz, mphit.Query.Charge, mzTolerance, scan);

                //At most one invalid scan inside both pre or post identification scan range.
                if (!IsValidEnvelope(envelope, mphit.Charge))
                {
                  if (count > 0)
                  {
                    envelopes.RemoveAt(envelopes.Count - 1);
                    break;
                  }
                  else
                  {
                    count = 1;
                  }
                }
                else
                {
                  count = 0;
                }

                envelopes.Add(envelope);
              }
            }

            if (envelopes.Count == 0) 
            {
              continue;
            }

            string scanCurr = envelopes.GetScanRange();

            //check scan list again
            bFound = false;
            foreach (var pklList in pkls)
            {
              if (pklList.Count == 0)
              {
                continue;
              }

              string scanOld = pklList.GetScanRange();
              if (scanOld.Equals(scanCurr))
              {
                pklMpMap[pklList].Add(mphit);
                bFound = true;
                break;
              }
            }

            if (bFound)
            {
              continue;
            }

            pkls.Add(envelopes);

            pklMpMap.Add(envelopes, new List<IIdentifiedSpectrum>());
            pklMpMap[envelopes].Add(mphit);
          }

          var detailFilePrefix = options.GetDetailDirectory() + "\\" + new FileInfo(options.ProteinFile).Name;
          foreach (string sequenceCharge in peptideChargeMap.Keys)
          {
            DifferentRetentionTimeEnvelopes pkls = peptideChargeMap[sequenceCharge];
            foreach (var envelopes in pkls)
            {
              if (0 == envelopes.Count)
              {
                continue;
              }

              List<IIdentifiedSpectrum> mps = pklMpMap[envelopes];
              double mzTolerance = PrecursorUtils.ppm2mz(mps[0].Query.ObservedMz, options.PPMTolerance);

              O18QuantificationPeptideProcessor processor = new O18QuantificationPeptideProcessor(fileFormat, 
                options.IsPostDigestionLabelling, 
                rawFilename, 
                PeptideUtils.GetPureSequence(mps[0].Sequence), 
                options.PurityOfO18Water, 
                envelopes, mzTolerance, 
                "",
                options.GetScanPercentageStart() / 100,
                options.GetScanPercentageEnd() / 100);

              processor.TheoreticalMz = GetTheoretialO16Mz(gapO18O16, mps[0]);
              processor.Charge = mps[0].Charge;
              processor.SoftwareVersion = options.SoftwareVersion;

              var resultFilename = MyConvert.Format("{0}.{1}.{2}.{3}.{4}.O18",
                detailFilePrefix,
                experimental,
                PeptideUtils.GetPureSequence(mps[0].Sequence),
                mps[0].Charge,
                envelopes.GetScanRange());

              processor.Process(resultFilename);

              O18QuantificationSummaryItem item = fileFormat.ReadFromFile(resultFilename);

              int maxScoreItemIndex = FindMaxScoreItemIndex(mps);

              var relativeFile = Path.Combine(Path.GetFileName(options.GetDetailDirectory()), Path.GetFileName(resultFilename));

              for (int i = 0; i < mps.Count; i++)
              {
                if (maxScoreItemIndex == i)
                {
                  item.AssignToAnnotation(mps[i], relativeFile);
                }
                else
                {
                  item.AssignDuplicationToAnnotation(mps[i], relativeFile);
                }
              }
            }
          }
        }
      }

      List<IIdentifiedSpectrum> peptides = mr.GetSpectra();
      foreach (IIdentifiedSpectrum mphit in peptides)
      {
        if (!mphit.Annotations.ContainsKey(O18QuantificationConstants.O18_RATIO_SCANCOUNT))
        {
          mphit.Annotations[O18QuantificationConstants.O18_RATIO_SCANCOUNT] = "-";
        }

        mphit.SetEnabled(calc.HasPeptideRatio(mphit));
      }

      calc.Calculate(mr, m => true);

      string resultFile = FileUtils.ChangeExtension(optionFile, ".O18summary");

      format.InitializeByResult(mr);

      format.WriteToFile(resultFile, mr);

      Progress.SetMessage("Finished, result was saved to " + resultFile);

      return new[] { resultFile };
    }

    private static bool IsValidEnvelope(O18QuanEnvelope envelope, int charge)
    {
      if (envelope[0].Intensity == 0 || envelope[4].Intensity == 0)
      {
        return false;
      }

      if (envelope[0].Charge == 0 && envelope[4].Charge == 0)
      {
        return false;
      }

      if (envelope[0].Charge != 0 && envelope[0].Charge != charge)
      {
        return false;
      }

      if (envelope[4].Charge != 0 && envelope[4].Charge != charge)
      {
        return false;
      }

      return true;
    }

    private static double GetTheoretialO16Mz(double gapO18O16, IIdentifiedSpectrum mphit)
    {
      double result = mphit.GetTheoreticalMz();
      if (mphit.Modifications.Contains("18O(1)"))
      {
        result -= gapO18O16 / mphit.Query.Charge;
      }
      else if (mphit.Modifications.Contains("18O(2)"))
      {
        result -= (gapO18O16 * 2) / mphit.Query.Charge;
      }
      return result;
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

    private void CheckRawFilename(IIdentifiedResult mr, string mascotTextResultFilename)
    {
      IIdentifiedSpectrum mpep = null;
      foreach (IIdentifiedProteinGroup mpg in mr)
      {
        foreach (IIdentifiedSpectrum mp in mpg.GetPeptides())
        {
          mpep = mp;
          break;
        }
        if (mpep != null)
        {
          break;
        }
      }

      if (mpep != null)
      {
        if (mpep.Query.FileScan.Experimental.Equals("Cmpd"))
        {
          string rawFilename = FindRawFileName(mascotTextResultFilename);
          string rawName = FileUtils.ChangeExtension(new FileInfo(rawFilename).Name, "");
          foreach (IdentifiedProteinGroup mpg in mr)
          {
            foreach (IIdentifiedSpectrum mp in mpg.GetPeptides())
            {
              if (mp.Query.FileScan.Experimental.Equals("Cmpd"))
              {
                mp.Query.FileScan.Experimental = rawName;
              }
            }
          }
        }
      }

    }

    private string FindRawFileName(string mascotTextResultFilename)
    {
      string textName = new FileInfo(mascotTextResultFilename).Name;
      string lastName = textName;
      string nextName = FileUtils.ChangeExtension(lastName, "");
      while (!nextName.Equals(lastName))
      {
        string result = options.RawDirectory + "\\" + nextName + options.RawExtension;
        if (File.Exists(result))
        {
          return result;
        }
        lastName = nextName;
        nextName = FileUtils.ChangeExtension(lastName, "");
      }

      throw new Exception("Cannot find corresponding raw file from " + mascotTextResultFilename);
    }

    private O18QuanEnvelope GetCorrespondingEnvelope(IRawFile rawFile, double theoreticalMz, int charge, double mzTolerance, int scan)
    {
      PeakList<Peak> pkl = rawFile.GetPeakList(scan);
      if (pkl.ScanTimes.Count == 0)
      {
        pkl.ScanTimes.Add(new ScanTime(scan, rawFile.ScanToRetentionTime(scan)));
      }

      PeakList<Peak> O16 = pkl.FindEnvelopeDirectly(theoreticalMz, charge, mzTolerance, 4, () => new Peak());

      PeakList<Peak> O181 = pkl.FindEnvelopeDirectly(theoreticalMz + chargeGapMap1[charge], charge, mzTolerance, 2, () => new Peak());
      for (int i = 2; i < 4; i++)
      {
        if (O16[i].Intensity < O181[i - 2].Intensity)
        {
          O16[i].Mz = O181[i - 2].Mz;
          O16[i].Intensity = O181[i - 2].Intensity;
        }
      }

      PeakList<Peak> O182 = pkl.FindEnvelopeDirectly(theoreticalMz + chargeGapMap2[charge], charge, mzTolerance, 2, () => new Peak());
      O16.AddRange(O182);

      return new O18QuanEnvelope(O16);
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

    private Dictionary<string, List<IIdentifiedSpectrum>> GetFilePeptideMap(IIdentifiedResult mr)
    {
      List<IIdentifiedSpectrum> peptides = mr.GetSpectra();
      Dictionary<string, List<IIdentifiedSpectrum>> filePepMap = new Dictionary<string, List<IIdentifiedSpectrum>>();
      foreach (IIdentifiedSpectrum mp in peptides)
      {
        string filename = new FileInfo(options.RawDirectory + "/" + mp.Query.FileScan.Experimental + options.RawExtension).FullName;
        if (!filePepMap.ContainsKey(filename))
        {
          filePepMap[filename] = new List<IIdentifiedSpectrum>();
        }
        filePepMap[filename].Add(mp);
      }
      return filePepMap;
    }
  }
}
