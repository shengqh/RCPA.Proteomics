using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using RCPA.Gui;
using System.Xml.Linq;
using RCPA.Proteomics.Spectrum;
using System.Xml;

namespace RCPA.Proteomics.Quantification.IsobaricLabelling
{
  /// <summary>
  /// 通过XmlReader和XmlWriter顺序读取IsobaricResult文件。
  /// </summary>
  public class IsobaricResultXmlFormatReader : ProgressClass, IDisposable
  {
    public IsobaricResultXmlFormatReader(bool readReporters = true, bool readPeaks = true)
    {
      this.ReadReporters = readReporters;
      this.ReadPeaks = readPeaks;
      this.Accept = m => true;
    }

    public bool ReadReporters { get; set; }

    public bool ReadPeaks { get; set; }

    public Predicate<IsobaricItem> Accept { get; set; }

    public Stream _stream { get; private set; }

    public XmlReader _reader { get; private set; }

    private XmlReaderSettings _setting = new XmlReaderSettings()
    {
      IgnoreComments = true,
      IgnoreWhitespace = true,
      IgnoreProcessingInstructions = true
    };

    private void InitializeByStream()
    {
      this._reader = XmlReader.Create(this._stream, _setting);
      Progress.SetRange(1, _stream.Length);
    }

    public void OpenFile(string fileName)
    {
      this._stream = new FileStream(fileName, FileMode.Open);
      InitializeByStream();
    }

    public void OpenByContent(string content)
    {
      _stream = new MemoryStream(Encoding.ASCII.GetBytes(content));
      InitializeByStream();
    }

    public void Close()
    {
      Dispose(false);
    }

    public IsobaricItem Next()
    {
      Progress.SetPosition(_stream.Position);

      return IsobaricItemXmlUtils.Parse(_reader, ReadReporters, ReadPeaks, Accept, true);
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

        if (null != _reader)
        {
          _reader.Close();
          _reader = null;
        }

        if (null != _stream)
        {
          _stream.Close();
          _stream = null;
        }

        m_disposed = true;
      }
    }

    ~IsobaricResultXmlFormatReader()
    {
      Dispose(false);
    }

    private bool m_disposed;

    #endregion
  }
}



