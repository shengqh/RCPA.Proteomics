using RCPA.Proteomics.Spectrum;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace RCPA.Proteomics.IO
{
  public class DtaDirectoryIterator<T> : AbstractPeakListIterator<T> where T : IPeak, new()
  {
    private static Regex peakPattern = new Regex(@"([0-9.]+)\s+([0-9.]+)");
    private static Regex precursorPattern = new Regex(@"([0-9.]+)\s+(\d+)");

    private readonly List<FileInfo> dtaFiles;

    private readonly DtaFormat<T> dtaFormat = new DtaFormat<T>();

    private int currentIndex;

    public DtaDirectoryIterator(string dir)
    {
      FileInfo[] files = new DirectoryInfo(dir).GetFiles();

      this.dtaFiles = new List<FileInfo>();
      foreach (FileInfo file in files)
      {
        if (file.Name.ToLower().EndsWith(".dta"))
        {
          this.dtaFiles.Add(file);
        }
      }

      morePeakListAvailable = this.dtaFiles.Count > 0;
    }

    public int Count
    {
      get { return this.dtaFiles.Count; }
    }

    protected override PeakList<T> DoReadNextPeakList(out bool hasNext)
    {
      if (this.currentIndex >= this.dtaFiles.Count)
      {
        throw new ArgumentException("CurrentIndex out of range "
                                    + this.dtaFiles.Count + " : " + this.currentIndex + "; call HasNext() first!");
      }

      FileInfo currentFile = this.dtaFiles[this.currentIndex];

      PeakList<T> result = this.dtaFormat.ReadFromFile(currentFile.FullName);

      this.currentIndex++;

      hasNext = this.currentIndex < this.dtaFiles.Count;

      return result;
    }

    public override string GetFormatName()
    {
      return "DtaDirectory";
    }
  }
}