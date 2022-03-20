using RCPA.Gui;
using System;

namespace RCPA.Proteomics.Quantification.ITraq
{
  public abstract class AbstractITraqResultXmlFormat : ProgressClass, IITraqResultFileFormat
  {
    public AbstractITraqResultXmlFormat()
    {
      ReadPeaks = true;
      Accept = m => true;
    }

    #region IFileFormat<ITraqResult> Members

    public abstract IsobaricResult ReadFromFile(string fileName);

    public abstract void WriteToFile(string fileName, IsobaricResult t);

    #endregion

    #region IITraqResultFileFormat Members

    public bool ReadPeaks { get; set; }

    public Predicate<IsobaricItem> Accept { get; set; }

    #endregion
  }
}


