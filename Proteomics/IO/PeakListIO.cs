using System;
using System.Collections.Generic;
using System.IO;
using RCPA.Utils;
using RCPA.Proteomics.Spectrum;
using RCPA.Gui;
using System.Runtime.Serialization.Formatters.Binary;

namespace RCPA.Proteomics.IO
{
  public interface IPeakListIterator<T> where T : IPeak
  {
    string GetFormatName();

    bool HasNext();

    PeakList<T> Next();
  }

  public abstract class AbstractPeakListIterator<T> : IPeakListIterator<T> where T : IPeak
  {
    protected bool morePeakListAvailable = true;

    #region IPeakListIterator<T> Members

    public bool HasNext()
    {
      return this.morePeakListAvailable;
    }

    public PeakList<T> Next()
    {
      if (!this.morePeakListAvailable)
      {
        throw new ArgumentException("Stream is empty");
      }

      try
      {
        return DoReadNextPeakList(out this.morePeakListAvailable);
      }
      catch (Exception e)
      {
        throw new Exception(string.Format("Could not read peak list {0}", e.ToString()));
      }
    }

    public abstract string GetFormatName();

    #endregion

    protected abstract PeakList<T> DoReadNextPeakList(out bool hasNext);
  }

  public interface IPeakListReader<T> where T : IPeak
  {
    string GetFormatName();

    List<PeakList<T>> ReadFromFile(string filename);
  }

  public interface IPeakListWriter<T> : ICloneable where T : IPeak
  {
    IProgressCallback Progress { get; set; }

    string GetFormatName();

    void Write(StreamWriter sw, PeakList<T> peaks);

    void WriteToFile(string filename, List<PeakList<T>> peakLists);

    void WriteToStream(StreamWriter streamWriter, List<PeakList<T>> peakLists);

    void AppendToStream(StreamWriter sw, List<PeakList<T>> peakLists);
  }

  public abstract class AbstractPeakListWriter<T> : ProgressClass, ICloneable, IPeakListWriter<T> where T : IPeak
  {
    private IProgressCallback progress = new EmptyProgressCallback();

    #region IPeakListWriter<T> Members

    public virtual void WriteToFile(string filename, List<PeakList<T>> peakLists)
    {
      using (var fileout = new StreamWriter(new FileStream(filename, FileMode.Create, FileAccess.Write)))
      {
        WriteToStream(fileout, peakLists);
      }
    }

    public virtual void WriteToStream(StreamWriter streamWriter, List<PeakList<T>> peakLists)
    {
      AppendToStream(streamWriter, peakLists);
    }

    public virtual void AppendToStream(StreamWriter streamWriter, List<PeakList<T>> peakLists)
    {
      this.progress.SetRange(0, peakLists.Count);
      try
      {
        this.progress.SetPosition(0);
        foreach (var pkl in peakLists)
        {
          Write(streamWriter, pkl);
          this.progress.Increment(1);
          if (this.progress.IsCancellationPending())
          {
            throw new UserTerminatedException();
          }
        }
      }
      finally
      {
        this.progress.End();
      }
    }

    public abstract string GetFormatName();

    public abstract void Write(StreamWriter sw, PeakList<T> peaks);

    #endregion

    #region ICloneable Members

    public virtual object Clone()
    {
      return this.MemberwiseClone();
    }

    #endregion
  }
}