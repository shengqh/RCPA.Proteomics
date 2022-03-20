using RCPA.Gui;
using System;
using System.IO;
using System.Text;
using System.Xml;

namespace RCPA.Proteomics.Quantification.ITraq
{
  /// <summary>
  /// 通过XmlReader和XmlWriter快速读取ITraqResult文件。
  /// </summary>
  public class ITraqResultXmlFormatReader : ProgressClass, IDisposable
  {
    public ITraqResultXmlFormatReader()
    {
      ReadPeaks = true;
      Accept = m => true;
    }

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

    public static String GetMode(string fileName)
    {
      using (var reader = new ITraqResultXmlFormatReader())
      {
        reader.OpenFile(fileName);
        while (reader._reader.MoveToElement("ITraqResult"))
        {
          if (reader._reader.HasAttributes)
          {
            return reader._reader.GetAttribute("Mode");
          }
        }
      }
      return string.Empty;
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

      return ITraqItemXmlUtils.Parse(_reader, ReadPeaks, Accept, true);
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

    ~ITraqResultXmlFormatReader()
    {
      Dispose(false);
    }

    private bool m_disposed;

    #endregion
  }
}



