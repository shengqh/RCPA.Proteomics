using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using RCPA.Proteomics;
using RCPA.Proteomics.Isotopic;
using RCPA.Proteomics.Spectrum;

namespace RCPA.Tools.Isotopic
{
  public class AtomCompositionIsotopicProfileBuilder : AbstractThreadFileProcessor
  {
    private IIsotopicProfileBuilder2 builder = new EmassProfileBuilder();

    #region IFileProcessor Members

    public override IEnumerable<string> Process(string filename)
    {
      string resultFile = filename + ".profile";
      using (StreamWriter sw = new StreamWriter(resultFile))
      {
        sw.WriteLine("Name\tFormula\tAtomComposition\tProfile");
        using (StreamReader sr = new StreamReader(filename))
        {
          bool bProcess = sr.BaseStream.Length > 0;

          if (bProcess)
          {
            Progress.SetRange(0, sr.BaseStream.Length);
          }

          string line;
          sr.ReadLine();

          Dictionary<string, List<Peak>> formulaProfileMap = new Dictionary<string, List<Peak>>();

          while ((line = sr.ReadLine()) != null)
          {
            if (bProcess)
            {
              Progress.SetPosition(sr.BaseStream.Position);
            }

            if (Progress.IsCancellationPending())
            {
              throw new UserTerminatedException();
            }

            if (line.Trim().Length == 0)
            {
              break;
            }

            string[] parts = line.Split(new char[] { '\t' });
            if (parts.Length < 3)
            {
              throw new Exception("It's not atom composition : " + line);
            }

            List<Peak> profile;
            if (formulaProfileMap.ContainsKey(parts[2]))
            {
              profile = formulaProfileMap[parts[2]];
            }
            else
            {
              AtomComposition ac = new AtomComposition(parts[2]);
              profile = builder.GetProfile(ac, 0, 0.00001);
              formulaProfileMap[parts[2]] = profile;
            }

            sw.Write("{0}\t{1}\t{2}", parts[0], parts[1], parts[2]);
            foreach (var value in profile)
            {
              sw.Write("\t{0:0.00000}:{1:0.00000}", value.Mz, value.Intensity);
            }
            sw.WriteLine();
          }
        }
      }

      return new[] { resultFile };
    }

    #endregion
  }
}
