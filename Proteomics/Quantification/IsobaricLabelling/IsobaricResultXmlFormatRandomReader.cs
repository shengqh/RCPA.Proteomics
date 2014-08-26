using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using RCPA.Gui;
using System.Xml.Linq;
using RCPA.Proteomics.Spectrum;
using System.Xml;
using RCPA.Format;
using System.Diagnostics;

namespace RCPA.Proteomics.Quantification.IsobaricLabelling
{
  /// <summary>
  /// Read the IsobaricItem of specific scan of experimental.
  /// </summary>
  public class IsobaricResultXmlFormatRandomReader : ProgressClass, IDisposable
  {
    public Dictionary<string, FileIndexItem> _indexItems;

    public IsobaricResultXmlFormatRandomReader(bool readReporters = true, bool readPeaks = true)
    {
      this.ReadReporters = readReporters;
      this.ReadPeaks = readPeaks;
    }

    public bool ReadReporters { get; set; }

    public bool ReadPeaks { get; set; }

    public Predicate<IsobaricScan> Accept { get; set; }

    public Stream _stream { get; private set; }

    public void Open(string fileName)
    {
      var indexFile = IsobaricResultXmlIndexBuilder.GetTargetFile(fileName);
      if (!File.Exists(indexFile))
      {
        new IsobaricResultXmlIndexBuilder() { Progress = this.Progress }.Process(fileName);
      }

      Progress.SetMessage("Reading index from " + indexFile + " ...");
      this._indexItems = new FileIndexFormat().ReadFromFile(indexFile).ToDictionary(m => m.Key);
      Progress.SetMessage("Reading index from " + indexFile + " finished.");

      this._stream = FileUtils.OpenReadFile(fileName);
    }

    public byte[] ReadXmlBytes(string experimental, int scan)
    {
      Debug.Assert(null != _indexItems);

      var key = GetKey(experimental, scan);
      if (!_indexItems.ContainsKey(key))
      {
        throw new ArgumentException("Cannot find index item of {0}", key);
      }

      var item = _indexItems[key];
      _stream.Position = item.StartPosition;
      var bytes = new byte[item.Length];
      this._stream.Read(bytes, 0, (int)item.Length);

      return bytes;
    }

    private static string GetKey(string experimental, int scan)
    {
      return string.Format("{0},{1}", experimental, scan);
    }

    public string ReadXmlString(string experimental, int scan)
    {
      return Encoding.ASCII.GetString(ReadXmlBytes(experimental, scan));
    }

    public bool Has(string experimental, int scan)
    {
      Debug.Assert(null != _indexItems);

      var key = GetKey(experimental, scan);

      return _indexItems.ContainsKey(key);
    }

    public IsobaricScan Read(string experimental, int scan, IsobaricType plexType)
    {
      var xml = ReadXmlBytes(experimental, scan);

      return IsobaricScanXmlUtils.Parse(xml, plexType, this.ReadReporters, this.ReadPeaks, this.Accept);
    }

    public void Close()
    {
      if (null != _stream)
      {
        _stream.Close();
        _stream = null;
      }
    }

    #region IDisposable Members

    public void Dispose()
    {
      Dispose(true);
      GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
      if (!m_disposed)
      {
        if (disposing)
        {
          // Release managed resources
        }

        if (null != _stream)
        {
          _stream.Close();
          _stream = null;
        }

        m_disposed = true;
      }
    }

    ~IsobaricResultXmlFormatRandomReader()
    {
      Dispose(false);
    }

    private bool m_disposed;

    #endregion
  }
}



