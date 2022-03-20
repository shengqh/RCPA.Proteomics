using RCPA.Proteomics.Summary;
using System.Collections.Generic;

namespace RCPA.Tools.Quantification
{
  public class QuantificationChromotographList : List<QuantificationChromotograph>
  {
    public bool AddIdentifiedSpectrum(IIdentifiedSpectrum spectrum)
    {
      int startScan = spectrum.Query.FileScan.FirstScan;

      foreach (QuantificationChromotograph pklList in this)
      {
        if (!pklList.ContainScan(startScan))
        {
          continue;
        }

        pklList.SetScanIdentified(startScan);
        pklList.IdentifiedSpectra.Add(spectrum);

        return true;
      }

      return false;
    }

    public void AddChromotograph(QuantificationChromotograph chro)
    {
      string scanCurr = chro.GetScanString();

      foreach (var qChro in this)
      {
        string scanOld = qChro.GetScanString();
        if (scanOld.Equals(scanCurr))
        {
          qChro.IdentifiedSpectra.AddRange(chro.IdentifiedSpectra);
          return;
        }
      }

      this.Add(chro);
    }
  }
}
