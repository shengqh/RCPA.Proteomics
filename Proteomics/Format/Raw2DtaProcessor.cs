using System;
using System.Collections.Generic;
using System.IO;
using RCPA.Proteomics.Raw;
using RCPA.Utils;
using RCPA.Proteomics.Spectrum;
using RCPA.Proteomics.IO;

namespace RCPA.Proteomics.Format
{
  public class Raw2DtaProcessor : AbstractRawTandemSpectrumConverter
  {
    private readonly GetResultDir getDir;

    private readonly DtaFormat<Peak> writer = new DtaFormat<Peak>();

    public Raw2DtaProcessor(IProcessor<PeakList<Peak>> pklProcessor, string saveToDir, bool splitMsLevel)
    {
      this.PeakListProcessor = pklProcessor;

      this.TargetDirectory = saveToDir;

      if (splitMsLevel)
      {
        this.getDir = GetMsLevelResultDir;
      }
      else
      {
        this.getDir = GetDirectResultDir;
      }
    }

    public DirectoryInfo GetDirectResultDir(string saveToDir, string experimental, int MsLevel)
    {
      return new DirectoryInfo(saveToDir + "\\" + experimental);
    }

    public DirectoryInfo GetMsLevelResultDir(string saveToDir, string experimental, int MsLevel)
    {
      return new DirectoryInfo(saveToDir + "\\MS" + MsLevel + "\\" + experimental);
    }

    #region Nested type: GetResultDir

    private delegate DirectoryInfo GetResultDir(string saveToDir, string experimental, int MsLevel);

    #endregion

    protected override void DoInitialize(string rawFileName)
    { }

    protected override void DoWritePeakList(IRawFile rawReader, PeakList<Peak> pkl, string rawFileName, List<string> result)
    {
      DirectoryInfo targetDir = this.getDir(this.TargetDirectory, pkl.Experimental, pkl.MsLevel);
      if (!targetDir.Exists)
      {
        targetDir.Create();
      }

      if (!result.Contains(targetDir.FullName))
      {
        result.Add(targetDir.FullName);
      }

      double maxMz = pkl[pkl.Count - 1].Mz;
      if (maxMz < pkl.PrecursorMZ * 1.3)
      {
        pkl.PrecursorCharge = 1;
      }

      int[] charges = null;
      if (0 == pkl.PrecursorCharge)
      {
        charges = new[] { 2, 3 };
      }
      else
      {
        charges = new[] { pkl.PrecursorCharge };
      }

      foreach (int charge in charges)
      {
        pkl.PrecursorCharge = charge;
        String dtaFilename = targetDir.FullName + "\\" + pkl.GetSequestDtaName();
        this.writer.WriteToFile(dtaFilename, pkl);
      }
    }

    protected override void DoFinalize(bool bReadAgain, IRawFile rawReader, string rawFileName, List<string> result)
    { }
  }
}