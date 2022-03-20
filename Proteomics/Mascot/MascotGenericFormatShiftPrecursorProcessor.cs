using RCPA.Proteomics.Spectrum;
using System.Collections.Generic;
using System.IO;

namespace RCPA.Proteomics.Mascot
{
  public class MascotGenericFormatShiftPrecursorProcessor : AbstractThreadProcessor
  {
    private MascotGenericFormatShiftPrecursorProcessorOptions options;

    public MascotGenericFormatShiftPrecursorProcessor(MascotGenericFormatShiftPrecursorProcessorOptions options)
    {
      this.options = options;
    }

    public override IEnumerable<string> Process()
    {
      var result = new List<string>();

      var titleParser = options.GetTitleParser();
      var writer = new MascotGenericFormatWriter<Peak>();

      Progress.SetRange(0, options.InputFiles.Count);
      int count = 0;
      foreach (var file in options.InputFiles)
      {
        count++;

        string resultFilename = options.GetOutputFile(file);
        Progress.SetMessage("Processing {0}/{1} : {2} ...", count, options.InputFiles.Count, file);

        var tempFile = resultFilename + ".tmp";
        using (var sw = new StreamWriter(tempFile))
        {
          sw.WriteLine("###ShiftMass={0:0.#}", options.ShiftMass);
          sw.WriteLine("###ShiftScan={0}", options.ShiftScan);
          sw.WriteLine();

          using (var sr = new StreamReader(new FileStream(file, FileMode.Open)))
          {
            var iter = new MascotGenericFormatIterator<Peak>(sr);
            while (iter.HasNext())
            {
              if (Progress.IsCancellationPending())
              {
                throw new UserTerminatedException();
              }

              var pkl = iter.Next();

              UpdatePrecursor(titleParser, pkl, options.ShiftMass, options.ShiftScan);

              writer.Write(sw, pkl);
            }
          }
        }

        if (File.Exists(resultFilename))
        {
          File.Delete(resultFilename);
        }

        File.Move(tempFile, resultFilename);

        result.Add(resultFilename);
      }
      Progress.End();

      return result;
    }

    public static void UpdatePrecursor(Summary.ITitleParser titleParser, PeakList<Peak> pkl, double shiftMass, int shiftScan)
    {

      if (pkl.PrecursorCharge == 0)
      {
        pkl.PrecursorCharge = 2;
      }

      pkl.PrecursorMZ = shiftMass / pkl.PrecursorCharge + pkl.PrecursorMZ;
      foreach (var scantime in pkl.ScanTimes)
      {
        scantime.Scan += shiftScan;
      }

      var filescan = titleParser.GetValue(pkl.Annotations["TITLE"].ToString());
      filescan.FirstScan += shiftScan;
      filescan.LastScan += shiftScan;
      filescan.Charge = pkl.PrecursorCharge;
      pkl.Annotations["TITLE"] = filescan.LongFileName;
    }
  }
}