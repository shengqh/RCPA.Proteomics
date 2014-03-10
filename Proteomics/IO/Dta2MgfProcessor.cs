using System;
using System.Collections.Generic;
using System.IO;
using RCPA.Proteomics.Spectrum;

namespace RCPA.Proteomics.IO
{
  public class Dta2MgfProcessor : AbstractThreadFileProcessor
  {
    //The program information which will be writen to mgf file.
    private string programInfo;

    private string saveToDir;

    private IPeakListWriter<Peak> writer;

    public Dta2MgfProcessor(string programInfo, IPeakListWriter<Peak> writer, string saveToDir)
    {
      this.programInfo = programInfo;
      this.writer = writer;
      this.saveToDir = saveToDir;
    }

    public static string GetResultFile(string saveToDir, string dtaDirectory)
    {
      return saveToDir + "\\" + new DirectoryInfo(dtaDirectory).Name + ".mgf";
    }

    protected string GetResultFile(string dtaDirectory)
    {
      return GetResultFile(this.saveToDir, dtaDirectory);
    }

    public override IEnumerable<string> Process(string dtaDirectory)
    {
      if (!Directory.Exists(dtaDirectory))
      {
        throw new ArgumentException("Directory not exists : " + dtaDirectory);
      }

      List<PeakList<Peak>> pklList = DtaIO.GetPeakListFromDtaDirectory<Peak>(dtaDirectory, Progress);

      MergeSameScan(pklList);

      string resultFile = GetResultFile(dtaDirectory);
      Progress.SetMessage("Saving " + pklList.Count + " peak list to " + resultFile + " ...");
      using (var sw = new StreamWriter(resultFile))
      {
        sw.WriteLine("COM=" + this.programInfo);
        this.writer.WriteToStream(sw, pklList);
      }
      Progress.SetMessage("Succeed!");

      return new[] { resultFile };
    }

    /// <summary>
    /// 合并集合中来自相同scan的离子列表，保留第一个，将precursorCharge设置为0
    /// </summary>
    /// <param name="pklList">离子列表集合</param>
    public static void MergeSameScan(List<PeakList<Peak>> pklList)
    {
      pklList.Sort((m1, m2) =>
      {
        int result = m1.Experimental.CompareTo(m2.Experimental);

        if (0 == result)
        {
          result = m1.FirstScan - m2.FirstScan;
        }

        if (0 == result)
        {
          result = m1.PrecursorCharge - m2.PrecursorCharge;
        }

        return result;
      });

      for (int i = pklList.Count - 1; i > 0; i--)
      {
        PeakList<Peak> iPkl = pklList[i];
        PeakList<Peak> jPkl = pklList[i - 1];
        if (iPkl.Experimental.Equals(jPkl.Experimental) && iPkl.FirstScan == jPkl.FirstScan && iPkl.FirstScan != 0)
        {
          jPkl.PrecursorCharge = 0;
          pklList.RemoveAt(i);
        }
      }
    }
  }
}