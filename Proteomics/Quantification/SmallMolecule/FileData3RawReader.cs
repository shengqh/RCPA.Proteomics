using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RCPA.Proteomics.Raw;
using RCPA.Proteomics.Spectrum;
using System.IO;

namespace RCPA.Proteomics.Quantification.SmallMolecule
{
  public class FileData3RawReader : IFileReader<FileData3>
  {
    private int minPeakMz;
    private int maxPeakMz;
    private IRawFile rawFile;

    public FileData3RawReader(int minMz, int maxMz, IRawFile rawFile)
    {
      this.minPeakMz = minMz;
      this.maxPeakMz = maxMz;
      this.rawFile = rawFile;
    }

    #region IFileReader<FileData3> Members

    public FileData3 ReadFromFile(string fileName)
    {
      FileData3 result = new FileData3();

      result.FileName = new FileInfo(fileName).Name;

      rawFile.Open(fileName);
      try
      {
        for (int scan = rawFile.GetFirstSpectrumNumber(); scan <= rawFile.GetLastSpectrumNumber(); scan++)
        {
          PeakList<Peak> pkl = rawFile.GetPeakList(scan, minPeakMz - 1, maxPeakMz + 1);

          result.AddPeaks(minPeakMz, maxPeakMz, scan, pkl);
        }
      }
      finally
      {
        rawFile.Close();
      }

      return result;
    }

    #endregion
  }
}
