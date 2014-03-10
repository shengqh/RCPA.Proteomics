using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Agilent.MassSpectrometry.DataAnalysis;
using RCPA.Proteomics.Raw;
using RCPA.Proteomics.Spectrum;
using RCPA.Proteomics;
using System.IO;
using RCPA.Utils;
using System.Text.RegularExpressions;
using RCPA.Proteomics.Quantification.SmallMolecule;

namespace RCPA.Tools.Quantification.SmallMolecule
{
  public class SmallMoleculeDataPreparationProcessor : AbstractThreadFileProcessor
  {
    private static int minPeakMz = 100;
    private static int maxPeakMz = 400;
    private AgilentDirectoryImpl rawFile = new AgilentDirectoryImpl();

    private static string GetFileName(DirectoryInfo dir)
    {
      string fileName = MyConvert.Format(@"{0}\{1}.{2}.{3}.txt", dir.Parent.FullName, dir.Name, minPeakMz, maxPeakMz);
      return fileName;
    }

    public override IEnumerable<string> Process(string rootDirectory)
    {
      DirectoryInfo[] dirs = new DirectoryInfo(rootDirectory).GetDirectories("*.d");

      Regex reg = new Regex(@"([\d-_]*)");
      Progress.SetRange(1, dirs.Length);
      int count = 0;
      foreach (var dir in dirs)
      {
        if (Progress.IsCancellationPending())
        {
          throw new UserTerminatedException();
        }

        count++;
        Progress.SetPosition(count);
        Progress.SetMessage("Processing " + dir.FullName + "...");

        Match match = reg.Match(dir.Name);
        string name = match.Groups[1].Value;
        if (name.StartsWith("_"))
        {
          name = name.Substring(1);
        }

        string fileName = GetFileName(dir);
        if (File.Exists(fileName))
        {
          continue;
        }

        double[] intensities = new double[maxPeakMz + 1];

        using (StreamWriter sw = new StreamWriter(fileName))
        {
          rawFile.Open(dir.FullName);

          sw.Write("{0:0}", rawFile.GetTotalIntensity());
          for (int peak = minPeakMz; peak <= maxPeakMz; peak++)
          {
            sw.Write("\t{0}", peak);
          }
          sw.WriteLine();

          for (int scan = rawFile.GetFirstSpectrumNumber(); scan <= rawFile.GetLastSpectrumNumber(); scan++)
          {
            if (Progress.IsCancellationPending())
            {
              sw.Close();
              File.Delete(fileName);
              throw new UserTerminatedException();
            }

            PeakList<Peak> pkl = rawFile.GetPeakList(scan, minPeakMz, maxPeakMz);

            for (int peak = minPeakMz; peak <= maxPeakMz; peak++)
            {
              intensities[peak] = 0.0;
            }

            pkl.ForEach(m =>
            {
              int mz = (int)Math.Abs(m.Mz);
              if (m.Intensity > intensities[mz])
              {
                intensities[mz] = m.Intensity;
              }
            });

            sw.Write(scan);
            for (int peak = minPeakMz; peak <= maxPeakMz; peak++)
            {
              sw.Write("\t{0:0.0}", intensities[peak]);
            }
            sw.WriteLine();
          }
        }
      }

      return new string[] { rootDirectory };
    }
  }
}
