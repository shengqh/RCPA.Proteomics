using System.IO;

namespace RCPA.Proteomics.Quantification.Labelfree
{
  public class ChromatographProfileTextWriter : IFileWriter<ChromatographProfile>
  {
    public void WriteToFile(string fileName, ChromatographProfile chro)
    {
      using (var sw = new StreamWriter(fileName))
      {
        sw.WriteLine("Scan\tRetentionTime\tIsotopic\tMz\tIntensity\tPPMTolerance\tSignalToNoise\tProfileCorrelation\tIdentified");
        foreach (var profile in chro.Profiles)
        {
          var corr = profile.CalculateProfileCorrelation(chro.IsotopicIons);
          for (int i = 0; i < profile.Count; i++)
          {
            sw.WriteLine("{0}\t{1:0.###}\t{2}\t{3}\t{4}\t{5}\t{6}\t{7}\t{8}",
              profile.Scan,
              profile.RetentionTime,
              i + 1,
              profile[i].Mz,
              profile[i].Intensity,
              profile[i].PPMDistance,
              profile[i].GetSignalToNose(),
              corr,
              profile.Identified);
          }
        }
      }
    }
  }
}
