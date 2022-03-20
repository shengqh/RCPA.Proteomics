using RCPA.Utils;
using System;
using System.Collections.Generic;
using System.IO;

namespace RCPA.Proteomics.Sequest
{
  public class DtasReader : IMergedFileReader
  {
    private int fileCount;

    private bool hasNext;

    private string lastLine;
    private string nextFilename;
    private StreamReader sr;

    public DtasReader()
    {
      this.sr = null;
      this.fileCount = 0;
      this.hasNext = false;
      this.lastLine = null;
      this.nextFilename = "";
    }

    public DtasReader(string filename)
      : this()
    {
      Open(filename);
    }

    #region IMergedFileReader Members

    public int FileCount
    {
      get { return this.fileCount; }
    }

    public bool HasNext
    {
      get { return this.hasNext; }
    }

    public string NextFilename
    {
      get { return this.nextFilename; }
    }

    protected virtual StreamReader OpenFile(string filename)
    {
      if (filename.ToLower().EndsWith(".zip"))
      {
        return ZipUtils.OpenFile(filename);
      }

      return new StreamReader(filename);
    }

    public bool Open(string filename)
    {
      Close();

      this.sr = OpenFile(filename);
      bool bStart = false;
      while ((this.lastLine = this.sr.ReadLine()) != null)
      {
        if (this.lastLine.StartsWith("DTAFILE_COUNT="))
        {
          String[] parts = this.lastLine.Split('=');
          this.fileCount = int.Parse(parts[1]);
          continue;
        }

        if (this.lastLine.Equals("[SEQUEST_DTA_FILES]"))
        {
          bStart = true;
          continue;
        }

        if (!bStart)
        {
          continue;
        }

        if (this.lastLine.EndsWith(".dta"))
        {
          this.hasNext = true;
          this.nextFilename = this.lastLine.Trim();
          break;
        }
      }

      return this.hasNext;
    }

    public void Close()
    {
      if (this.sr != null)
      {
        this.sr.Close();
        this.sr = null;
        this.fileCount = 0;
        this.hasNext = false;
        this.lastLine = null;
        this.nextFilename = "";
      }
    }

    public List<string> NextContent()
    {
      if (!this.hasNext)
      {
        throw new Exception("There is no any dta content. Call HasNext() before NextContent().");
      }

      this.hasNext = false;
      this.nextFilename = "";

      var result = new List<string>();
      while ((this.lastLine = this.sr.ReadLine()) != null)
      {
        if (this.lastLine.EndsWith(".dta"))
        {
          this.hasNext = true;
          this.nextFilename = this.lastLine.Trim();
          break;
        }

        if (this.lastLine.Trim().Length == 0)
        {
          continue;
        }

        result.Add(this.lastLine);
      }

      return result;
    }

    public void SkipNextContent()
    {
      if (!this.hasNext)
      {
        throw new Exception("There is no any dta content. Call HasNext() before NextContent().");
      }

      this.hasNext = false;
      this.nextFilename = "";

      while ((this.lastLine = this.sr.ReadLine()) != null)
      {
        if (this.lastLine.EndsWith(".dta"))
        {
          this.hasNext = true;
          this.nextFilename = this.lastLine.Trim();
          break;
        }
      }
    }

    public void Dispose()
    {
      Close();
    }

    #endregion
  }
}