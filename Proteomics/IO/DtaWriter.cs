using RCPA.Proteomics.Spectrum;
using System;
using System.Collections.Generic;
using System.IO;

namespace RCPA.Proteomics.IO
{
  public class DtaWriter<T> : AbstractPeakListWriter<T> where T : IPeak, new()
  {
    public override string GetFormatName()
    {
      return "DtaFormat";
    }

    public override void WriteToFile(string targetDirectory, List<PeakList<T>> peakLists)
    {
      var dtaFormat = new DtaFormat<T>();

      var di = new DirectoryInfo(targetDirectory);
      if (!di.Exists)
      {
        di.Create();
      }

      Progress.SetRange(0, peakLists.Count);
      foreach (var pkl in peakLists)
      {
        Progress.Increment(1);
        if (Progress.IsCancellationPending())
        {
          throw new UserTerminatedException();
        }

        int oldcharge = pkl.PrecursorCharge;
        int[] charges = null;
        if (0 == oldcharge)
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
          String dtaFilename = di.FullName + "\\" + pkl.GetSequestDtaName();
          dtaFormat.WriteToFile(dtaFilename, pkl);
        }
      }
    }

    public override void WriteToStream(StreamWriter sw, List<PeakList<T>> peakLists)
    {
      throw new InvalidOperationException("");
    }

    public override void AppendToStream(StreamWriter sw, List<PeakList<T>> peakLists)
    {
      throw new InvalidOperationException("");
    }

    public override void Write(StreamWriter sw, PeakList<T> pkl)
    {
      throw new InvalidOperationException("");
    }
  }
}