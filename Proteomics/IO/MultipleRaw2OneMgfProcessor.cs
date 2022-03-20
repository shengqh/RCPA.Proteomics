using RCPA.Proteomics.Raw;
using RCPA.Proteomics.Spectrum;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace RCPA.Proteomics.IO
{
  public class MultipleRaw2OneMgfProcessor : AbstractRawConverterProcessor
  {
    private bool firstOpen = true;

    protected FileInfo saveToMergedFile;

    public MultipleRaw2OneMgfProcessor(IRawFile2 rawFile, IPeakListWriter<Peak> writer, double retentionTimeTolerance,
                                       double ppmPrecursorTolerance, double ppmPeakTolerance,
                                       IProcessor<PeakList<Peak>> pklProcessor, FileInfo saveToMergedFile)
      : base(rawFile, writer, retentionTimeTolerance, ppmPrecursorTolerance, ppmPeakTolerance, pklProcessor)
    {
      this.saveToMergedFile = saveToMergedFile;
    }

    protected override List<string> WritePeakLists(FileInfo rawFile, List<PeakList<Peak>> mergedPklList)
    {
      Progress.SetMessage("Saving " + mergedPklList.Count + " peak list ... ");
      StreamWriter sw = null;
      try
      {
        if (this.firstOpen)
        {
          this.firstOpen = false;
          sw = new StreamWriter(new FileStream(this.saveToMergedFile.FullName, FileMode.Create));
          sw.WriteLine("###Converter=" + programInfo);
          sw.WriteLine("###Rawfile=" + rawFile.Name);
          writer.WriteToStream(sw, mergedPklList);
        }
        else
        {
          sw = new StreamWriter(new FileStream(this.saveToMergedFile.FullName, FileMode.Append));
          sw.WriteLine("###Rawfile=" + rawFile.Name);
          writer.AppendToStream(sw, mergedPklList);
        }
      }
      finally
      {
        sw.Close();
      }
      return new string[] { this.saveToMergedFile.FullName }.ToList();
    }
  }
}