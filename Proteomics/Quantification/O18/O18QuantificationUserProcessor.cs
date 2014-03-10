using System;
using System.Collections.Generic;
using System.Text;
using RCPA;
using RCPA.Proteomics.Raw;
using RCPA.Proteomics.Quantification;
using RCPA.Proteomics;
using RCPA.Utils;
using RCPA.Proteomics.Utils;
using System.IO;
using RCPA.Proteomics.Spectrum;
using RCPA.Proteomics.Quantification.O18;

namespace RCPA.Proteomics.Quantification.O18
{
  /***
   * Calculate O16/O18 ratio based on user assigned precursorMz, precursorCharge and peptide sequence
   */
  public class O18QuantificationUserProcessor : AbstractThreadFileProcessor
  {
    public static string version = "1.0.0";

    private IFileFormat<O18QuantificationSummaryItem> fileFormat;
    private bool isPostDigestionLabelling;
    private string rawFilename;
    private double ppmTolerance;
    private double purityOfO18Water;
    private int profileLength = 6;
    private string peptideSequence;
    private string modificationFormula;
    private double precursorMz;
    private int precursorCharge;
    private double mzTolerance;

    public O18QuantificationUserProcessor(IFileFormat<O18QuantificationSummaryItem> fileFormat, bool isPostDigestionLabelling, string rawFilename, double purityOfO18Water, string peptideSequence, string modificationFormula, double precursorMz, int precursorCharge, double ppmTolerance)
    {
      this.fileFormat = fileFormat;
      this.isPostDigestionLabelling = isPostDigestionLabelling;
      this.rawFilename = rawFilename;
      this.ppmTolerance = ppmTolerance;
      this.purityOfO18Water = purityOfO18Water;
      this.peptideSequence = peptideSequence;
      this.modificationFormula = modificationFormula;
      this.precursorMz = precursorMz;
      this.precursorCharge = precursorCharge;
      this.mzTolerance = PrecursorUtils.ppm2mz(precursorMz, ppmTolerance);
    }

    public override IEnumerable<string> Process(string resultFilename)
    {
      List<string> result = new List<string>();

      double gapO18O16 = Atom.O18.MonoMass - Atom.O.MonoMass;

      using (IRawFile rawFile = new RawFileImpl(rawFilename))
      {
        int firstScanNumber = rawFile.GetFirstSpectrumNumber();
        int lastScanNumber = rawFile.GetLastSpectrumNumber();

        List<O18QuanEnvelope> envelopes = new List<O18QuanEnvelope>();

        for (int scan = firstScanNumber; scan <= lastScanNumber; scan++)
        {
          if (1 == rawFile.GetMsLevel(scan))
          {
            var envelope = GetCorrespondingEnvelope(rawFile, scan);

            //if the monoisotopic peak of both O16 and O18 have no charge, 
            //throw current envelope and exit the loop
            if (0 == envelope[0].Charge && 0 == envelope[4].Charge)
            {
              break;
            }

            envelopes.Add(envelope);
          }
        }

        List<int> scanList = new List<int>();
        foreach (PeakList<Peak> pkl in envelopes)
        {
          scanList.Add(pkl.ScanTimes[0].Scan);
        }
        scanList.Sort();

        StringBuilder sb = new StringBuilder();
        if (scanList.Count > 0)
        {
          sb.Append("." + scanList[0] + "-" + scanList[scanList.Count - 1]);
        }

        if (envelopes.Count == 0)
        {
          using (StreamWriter sw = new StreamWriter(resultFilename))
          {
            sw.WriteLine("No envelope found in raw file.");
          }
        }
        else
        {
          O18QuantificationPeptideProcessor processor =
            new O18QuantificationPeptideProcessor(fileFormat,
              isPostDigestionLabelling,
              rawFilename,
              PeptideUtils.GetPureSequence(peptideSequence),
              purityOfO18Water,
              envelopes,
              mzTolerance,
              modificationFormula,
              0.0,
              1.0);
          processor.Process(resultFilename);
        }

        result.Add(resultFilename);
      }

      return result;
    }

    private O18QuanEnvelope GetCorrespondingEnvelope(IRawFile rawFile, int scan)
    {
      PeakList<Peak> pkl = rawFile.GetPeakList(scan);

      pkl = pkl.FindEnvelopeDirectly(precursorMz, precursorCharge, mzTolerance, profileLength, () => new Peak());

      O18QuanEnvelope result = new O18QuanEnvelope();

      result.AddRange(pkl);
      result.ScanTimes.Add(new ScanTime(scan, 0.0));

      return result;
    }
  }
}
