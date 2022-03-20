using RCPA.Proteomics;
using RCPA.Proteomics.Raw;
using RCPA.Proteomics.Summary;
using System;

namespace RCPA.Tools.Quantification
{
  public class QuantificationChromotographBuilder
  {
    private ITheoreticalEnvelopesBuilder builder;

    private double ppmTolerance;

    private Predicate<QuantificationScan> validate;

    public QuantificationChromotographBuilder(ITheoreticalEnvelopesBuilder builder, double ppmTolerance, Predicate<QuantificationScan> validate)
    {
      this.builder = builder;
      this.ppmTolerance = ppmTolerance;
      this.validate = validate;
    }

    public QuantificationChromotograph Build(IIdentifiedSpectrum mphit, IRawFile reader)
    {
      QuantificationChromotograph result = new QuantificationChromotograph();

      var envelopes = builder.Build(mphit);

      int startScan = mphit.Query.FileScan.FirstScan;

      double mzTolerance = PrecursorUtils.ppm2mz(envelopes[0][0], ppmTolerance);

      bool bFirst = true;

      int firstScanNumber = reader.GetFirstSpectrumNumber();
      int lastScanNumber = reader.GetLastSpectrumNumber();

      //backward
      for (int scan = startScan; scan >= firstScanNumber; scan--)
      {
        if (1 == reader.GetMsLevel(scan))
        {
          QuantificationScan qscan = reader.GetPeakList(scan).GetQuantificationScan(envelopes, mzTolerance);

          if (!validate(qscan))
          {
            break;
          }

          if (bFirst)
          {
            qscan.IsIdentified = true;
            bFirst = false;
          }
          result.Insert(0, qscan);
        }
      }

      //forward
      for (int scan = startScan + 1; scan <= lastScanNumber; scan++)
      {
        if (1 == reader.GetMsLevel(scan))
        {
          QuantificationScan qscan = reader.GetPeakList(scan).GetQuantificationScan(envelopes, mzTolerance);

          if (!validate(qscan))
          {
            break;
          }

          result.Add(qscan);
        }
      }

      if (result.Count > 0)
      {
        result.IdentifiedSpectra.Add(mphit);
      }

      return result;
    }
  }
}
