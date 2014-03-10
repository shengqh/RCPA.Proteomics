using System;
using System.Collections.Generic;
using System.IO;
using RCPA.Utils;

namespace RCPA.Proteomics.Sequest
{
  public class OutsReader : IMergedFileReader
  {
    private bool hasNext;

    private string lastLine;

    private string nextFilename;
    private int outFileCount;
    private StreamReader sr;

    public OutsReader()
    {
      this.sr = null;
      this.outFileCount = 0;
      this.hasNext = false;
      this.lastLine = null;
      this.nextFilename = null;
    }

    public OutsReader(string filename)
      : this()
    {
      Open(filename);
    }

    #region IMergedFileReader Members

    public int FileCount
    {
      get { return this.outFileCount; }
    }

    public bool HasNext
    {
      get { return this.hasNext; }
    }

    public string NextFilename
    {
      get { return this.nextFilename; }
    }

    public bool Open(string filename)
    {
      Close();

      if (filename.ToLower().EndsWith(".zip"))
      {
        this.sr = ZipUtils.OpenFile(filename);
      }
      else
      {
        this.sr = new StreamReader(filename);
      }
      bool bStart = false;
      while ((this.lastLine = this.sr.ReadLine()) != null)
      {
        if (this.lastLine.StartsWith("OUTFILE_COUNT="))
        {
          String[] parts = this.lastLine.Split('=');
          this.outFileCount = int.Parse(parts[1]);
          continue;
        }

        if (this.lastLine.Equals("[SEQUEST_OUT_FILES]"))
        {
          bStart = true;
          continue;
        }

        if (!bStart)
        {
          continue;
        }

        if (this.lastLine.EndsWith(".out"))
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
        this.outFileCount = 0;
        this.hasNext = false;
        this.lastLine = null;
        this.nextFilename = null;
      }
    }

    public List<string> NextContent()
    {
      if (!this.hasNext)
      {
        throw new Exception("There is no any out content. Call HasNext() before NextContent().");
      }

      this.hasNext = false;

      var result = new List<string>();
      result.Add(this.lastLine);
      while ((this.lastLine = this.sr.ReadLine()) != null)
      {
        if (this.lastLine.EndsWith(".out"))
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
        if (this.lastLine.EndsWith(".out"))
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