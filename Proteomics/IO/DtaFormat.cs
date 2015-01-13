using System.IO;
using System.Text.RegularExpressions;
using RCPA.Proteomics.Spectrum;

namespace RCPA.Proteomics.IO
{
  public class DtaFormat<T> : IFileFormat<PeakList<T>> where T : IPeak, new()
  {
    #region IFileFormat<PeakList<T>> Members

    public PeakList<T> ReadFromFile(string dtaFilename)
    {
      var fi = new FileInfo(dtaFilename);
      if (!fi.Exists)
      {
        throw new FileNotFoundException("Cannot find the file " + dtaFilename);
      }

      var result = new PeakList<T>();
      SequestFilename sf = SequestFilename.Parse(fi.Name);
      result.Experimental = sf.Experimental;
      result.ScanTimes.Add(new ScanTime(sf.FirstScan, 0.0));
      if (sf.FirstScan != sf.LastScan)
      {
        result.ScanTimes.Add(new ScanTime(sf.LastScan, 0.0));
      }
      result.PrecursorCharge = sf.Charge;

      using (var filein = new StreamReader(new FileStream(dtaFilename, FileMode.Open, FileAccess.Read)))
      {
        string lastline;
        while ((lastline = filein.ReadLine()) != null)
        {
          if (lastline.Trim().Length > 0)
          {
            break;
          }
        }
        if (lastline == null)
        {
          return null;
        }

        string[] parts = Regex.Split(lastline, @"\s+");
        double precursorMass = MyConvert.ToDouble(parts[0]);
        result.PrecursorCharge = int.Parse(parts[1]);
        result.PrecursorMZ = PrecursorUtils.MHToMz(precursorMass, result.PrecursorCharge, true);

        while ((lastline = filein.ReadLine()) != null)
        {
          if (lastline.Length == 0)
          {
            continue;
          }

          if (lastline[0] == '>')
          {
            break;
          }

          string[] peakParts = Regex.Split(lastline, @"\s+");
          var peak = new T();
          peak.Mz = MyConvert.ToDouble(peakParts[0]);
          peak.Intensity = MyConvert.ToDouble(peakParts[1]);
          if (peakParts.Length > 2)
          {
            peak.Charge = int.Parse(peakParts[2]);
          }

          result.Add(peak);
        }

        return result;
      }
    }

    public void WriteToFile(string dtaFilename, PeakList<T> t)
    {
      using (var sw = new StreamWriter(dtaFilename))
      {
        sw.WriteLine(MyConvert.Format("{0:0.####}\t{1}", PrecursorUtils.MzToMH(t.PrecursorMZ, t.PrecursorCharge, true),
                                   t.PrecursorCharge));
        foreach (T peak in t)
        {
          sw.WriteLine(MyConvert.Format("{0:0.####} {1:0.#}", peak.Mz, peak.Intensity));
        }
      }
    }

    #endregion
  }
}