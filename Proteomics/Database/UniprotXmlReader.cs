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

namespace RCPA.Proteomics.Database
{
  /// <summary>
  /// 通过XmlReader和XmlWriter快速读取ITraqResult文件。
  /// </summary>
  public class UniprotXmlFormatRandomReader : ProgressClass, IDisposable
  {
    public Dictionary<string, FileIndexItem> _indexItems;

    public UniprotXmlFormatRandomReader() { }

    public Stream _stream { get; private set; }

    public void Open(string fileName)
    {
      var indexFile = UniprotXmlIndexBuilder.GetTargetFile(fileName);
      if (!File.Exists(indexFile))
      {
        new UniprotXmlIndexBuilder() { Progress = this.Progress }.Process(fileName);
      }

      Progress.SetMessage("Reading index from " + indexFile + " ...");
      this._indexItems = new FileIndexFormat().ReadFromFile(indexFile).ToDictionary(m => m.Key);
      Progress.SetMessage("Reading index from " + indexFile + " finished.");

      this._stream = FileUtils.OpenReadFile(fileName);
    }

    public byte[] ReadXmlBytes(string key)
    {
      Debug.Assert(null != _indexItems);

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

    public string ReadXmlString(string proteinName)
    {
      return Encoding.ASCII.GetString(ReadXmlBytes(proteinName));
    }

    public bool Has(string proteinName)
    {
      Debug.Assert(null != _indexItems);

      return _indexItems.ContainsKey(proteinName);
    }

    public UniprotEntry Read(string proteinName)
    {
      var xml = ReadXmlBytes(proteinName);
      var result = new UniprotEntry();
      result.ParseXml(xml);
      return result;
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

    ~UniprotXmlFormatRandomReader()
    {
      Dispose(false);
    }

    private bool m_disposed;

    #endregion
  }
}



