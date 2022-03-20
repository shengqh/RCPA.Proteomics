using RCPA.Proteomics.IO;
using RCPA.Proteomics.Spectrum;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace RCPA.Proteomics.Mascot
{
  public class MascotGenericFormatWriter<T> : AbstractPeakListWriter<T> where T : IPeak
  {
    public ITitleFormat TitleFormat { get; set; }

    public int[] DefaultCharges { get; set; }

    public List<string> Comments { get; private set; }

    public MascotGenericFormatWriter(int[] defaultCharges)
    {
      this.DefaultCharges = defaultCharges;
      this.TitleFormat = new TitleFormatSequest();
      this.Comments = new List<string>();
    }

    public MascotGenericFormatWriter() : this(new[] { 2, 3 }) { }

    protected virtual void WriteFileHeader(StreamWriter sw)
    {
      foreach (string comment in this.Comments)
      {
        sw.WriteLine("###" + comment);
      }

      if (this.DefaultCharges.Length != 0)
      {
        var sb = new StringBuilder();
        foreach (int charge in this.DefaultCharges)
        {
          if (sb.Length == 0)
          {
            sb.Append(charge + "+");
          }
          else
          {
            sb.Append(" and " + charge + "+");
          }
        }

        sw.WriteLine();
        sw.WriteLine(MascotGenericFormatConstants.CHARGE_TAG + "=" + sb);
        sw.WriteLine();
      }
    }

    protected void WriteHeader(StreamWriter writer, PeakList<T> pl)
    {
      if (pl.ScanTimes.Count > 0)
      {
        writer.Write(MascotGenericFormatConstants.MSMS_SCAN_TAG + pl.ScanTimes[0].Scan);
        for (int i = 1; i < pl.ScanTimes.Count; i++)
        {
          writer.Write("/" + pl.ScanTimes[i].Scan);
        }
        writer.WriteLine();
      }

      if (pl.PrecursorOffsetPPM != 0)
      {
        writer.WriteLine("{0}{1:0.0000}ppm", MascotGenericFormatConstants.MSMS_PRECURSOR_OFFSET_TAG, pl.PrecursorOffsetPPM);
      }


      if (pl.ProductOffsetPPM != 0)
      {
        writer.WriteLine("{0}{1:0.0000}ppm", MascotGenericFormatConstants.MSMS_PRODUCTION_OFFSET_TAG, pl.ProductOffsetPPM);
      }
    }

    protected void WriteAnnotation(StreamWriter writer, PeakList<T> pl)
    {
      foreach (var de in pl.Annotations)
      {
        if (!de.Key.Equals(MascotGenericFormatConstants.TITLE_TAG))
        {
          writer.WriteLine("###" + de.Key + "=" + de.Value);
        }
      }
      writer.WriteLine(MascotGenericFormatConstants.BEGIN_PEAK_LIST_TAG);
      WriteAnnotationTitle(writer, pl);
      writer.Write(MascotGenericFormatConstants.PEPMASS_TAG + "=" + MyConvert.Format("{0:0.#####}", pl.PrecursorMZ));
      if (pl.PrecursorIntensity > 0.0)
      {
        writer.Write("\t" + MyConvert.Format("{0:0.0###}", pl.PrecursorIntensity));
      }
      writer.WriteLine();

      if (pl.PrecursorCharge > 0)
      {
        writer.WriteLine(MascotGenericFormatConstants.CHARGE_TAG + "=" + pl.PrecursorCharge + "+");
      }

      if (pl.ScanTimes.Count > 0)
      {
        if (pl.ScanTimes.HasRetentionTime())
        {
          writer.WriteLine(MascotGenericFormatConstants.RETENTION_TIME_TAG + "=" + pl.ScanTimes.RetentionTimesInSecond());
        }
        var scans = pl.ScanTimes.Scans();
        if (!string.IsNullOrEmpty(scans) && !scans.Equals("0") && !scans.Equals("0-0"))
        {
          writer.WriteLine(MascotGenericFormatConstants.SCAN_TAG + "=" + scans);
        }
      }

      //if (!string.IsNullOrWhiteSpace(pl.Experimental))
      //{
      //  writer.WriteLine(MascotGenericFormatConstants.RAWFILE_TAG + "=" + pl.Experimental);
      //}
    }

    protected void WriteAnnotationTitle(StreamWriter writer, PeakList<T> pl)
    {
      string title;
      if (pl.Annotations.ContainsKey(MascotGenericFormatConstants.TITLE_TAG))
      {
        title = pl.Annotations[MascotGenericFormatConstants.TITLE_TAG].ToString();
      }
      else
      {
        title = this.TitleFormat.Build(pl);
      }

      writer.WriteLine(MascotGenericFormatConstants.TITLE_TAG + "=" + title);
    }

    protected void WritePeak(StreamWriter writer, PeakList<T> pl)
    {
      PeakList<T> pkl;

      //Mascot Generic Format 要求每个spectrum不能超过10000个离子。
      if (pl.Count > 10000)
      {
        pkl = new PeakList<T>(pl);
        pkl.SortByIntensity();
        pkl.RemoveRange(10000, pkl.Count - 10000);
        pkl.SortByMz();
      }
      else
      {
        pkl = pl;
      }

      //if (pkl.Any(m => m.Charge > 0))
      //{
      //  foreach (T peak in pkl)
      //  {
      //    writer.WriteLine(MyConvert.Format("{0:0.#####} {1:0.0} {2}", peak.Mz, peak.Intensity, peak.Charge));
      //  }
      //}
      //else
      //{
      foreach (T peak in pkl)
      {
        writer.WriteLine(MyConvert.Format("{0:0.#####} {1:0.0###}", peak.Mz, peak.Intensity));
      }
      //}

      writer.WriteLine(MascotGenericFormatConstants.END_PEAK_LIST_TAG);
      writer.WriteLine();
    }

    public override string GetFormatName()
    {
      return "MascotGenericFormat";
    }

    public override void WriteToStream(StreamWriter sw, List<PeakList<T>> peakLists)
    {
      WriteFileHeader(sw);

      AppendToStream(sw, peakLists);
    }

    public override void AppendToStream(StreamWriter sw, List<PeakList<T>> peakLists)
    {
      Progress.SetRange(0, peakLists.Count);
      foreach (var pklList in peakLists)
      {
        Write(sw, pklList);

        Progress.Increment(1);
        if (Progress.IsCancellationPending())
        {
          throw new UserTerminatedException();
        }
      }
      Progress.SetPosition(peakLists.Count);
    }

    public override void Write(StreamWriter sw, PeakList<T> pkl)
    {
      WriteHeader(sw, pkl);
      WriteAnnotation(sw, pkl);
      WritePeak(sw, pkl);
    }
  }
}