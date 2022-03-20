using RCPA.Proteomics.Raw;
using RCPA.Proteomics.Spectrum;
using RCPA.Utils;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace RCPA.Proteomics.IO
{
  public class Raw2MgfProcessor : AbstractRawConverterProcessor
  {
    public static string version = "1.0.1";

    protected DirectoryInfo saveToDir;

    private bool groupByScanMode;

    public int DeduceIonCount { get; set; }

    public Raw2MgfProcessor(IRawFile2 rawReader, IPeakListWriter<Peak> writer, double retentionTimeTolerance, double ppmPrecursorTolerance,
                            double ppmPeakTolerance, IProcessor<PeakList<Peak>> pklProcessor, DirectoryInfo saveToDir, bool groupByScanMode)
      : base(rawReader, writer, retentionTimeTolerance, ppmPrecursorTolerance, ppmPeakTolerance, pklProcessor)
    {
      this.saveToDir = saveToDir;
      this.groupByScanMode = groupByScanMode;
      this.DeduceIonCount = 6;
    }

    public FileInfo GetResultFile(DirectoryInfo saveToDir, FileInfo rawFileName)
    {
      return new FileInfo(saveToDir.FullName + "\\" + rawReader.GetFileNameWithoutExtension(rawFileName.FullName) + ".mgf");
    }

    protected FileInfo GetResultFile(FileInfo rawFile)
    {
      return GetResultFile(this.saveToDir, rawFile);
    }

    protected override List<string> WritePeakLists(FileInfo rawFile, List<PeakList<Peak>> mergedPklList)
    {
      var result = new List<string>();

      FileInfo resultFile = GetResultFile(rawFile);

      if (groupByScanMode)
      {
        var modes = (from pkl in mergedPklList
                     select pkl.ScanMode).Distinct().ToList();
        if (modes.Count() > 1)
        {
          foreach (var mode in modes)
          {
            var pkls = (from pkl in mergedPklList
                        where pkl.ScanMode.Equals(mode)
                        select pkl).ToList();
            var file = FileUtils.ChangeExtension(resultFile.FullName, mode + ".mgf");
            WriteToFile(file, pkls);
            result.Add(file);
          }
          return result;
        }
      }

      WriteToFile(resultFile.FullName, mergedPklList);
      result.Add(resultFile.FullName);
      return result;
    }

    private void WriteToFile(string file, List<PeakList<Peak>> pkls)
    {
      Progress.SetMessage("Saving " + pkls.Count + " peak list to " + file + " ...");

      using (var sw = new StreamWriter(new FileStream(file, FileMode.Create)))
      {
        writer.Progress = Progress;
        writer.WriteToStream(sw, pkls);
      }
    }
  }
}