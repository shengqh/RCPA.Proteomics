using System.IO;
using System.Linq;

namespace RCPA.Proteomics.Quantification.Labelfree
{
  public class ChromatographProfileTextWriter : IFileFormat<ChromatographProfile>
  {
    public ChromatographProfile ReadFromFile(string fileName)
    {
      var result = new ChromatographProfile();
      using (var sr = new StreamReader(fileName))
      {
        var line = sr.ReadLine();
        while ((line = sr.ReadLine()) != null)
        {
          var parts = line.Split('\t');
          var scan = int.Parse(parts[0]);
          ChromatographProfileScan profilescan;
          if (result.Profiles.Count > 0 && result.Profiles.Last().Scan == scan)
          {
            profilescan = result.Profiles.Last();
          }
          else
          {
            profilescan = new ChromatographProfileScan();
            result.Profiles.Add(profilescan);
            profilescan.Scan = scan;
            profilescan.RetentionTime = double.Parse(parts[1]);
            profilescan.Identified = bool.Parse(parts[9]);
          }

          var peak = new ChromatographProfileScanPeak();
          profilescan.Add(peak);
          peak.Isotopic = int.Parse(parts[2]);
          peak.Mz = double.Parse(parts[3]);
          peak.Intensity = double.Parse(parts[4]);
          peak.PPMDistance = double.Parse(parts[5]);
          peak.Noise = peak.Intensity / double.Parse(parts[6]);
        }
      }
      return result;
    }

    public void WriteToFile(string fileName, ChromatographProfile chro)
    {
      using (var sw = new StreamWriter(fileName))
      {
        sw.WriteLine("Scan\tRetentionTime\tIsotopic\tMz\tIntensity\tPPMTolerance\tSignalToNoise\tProfileCorrelation\tProfileDistance\tIdentified");
        foreach (var profile in chro.Profiles)
        {
          var corr = profile.CalculateProfileCorrelation(chro.IsotopicIntensities);
          var dist = profile.CalculateProfileDistance(chro.IsotopicIntensities);
          for (int i = 0; i < profile.Count; i++)
          {
            sw.WriteLine("{0}\t{1:0.###}\t{2}\t{3}\t{4}\t{5}\t{6}\t{7}\t{8}\t{9}",
              profile.Scan,
              profile.RetentionTime,
              i + 1,
              profile[i].Mz,
              profile[i].Intensity,
              profile[i].PPMDistance,
              profile[i].GetSignalToNose(),
              corr,
              dist,
              profile.Identified);
          }
        }
      }
    }

  }
}
