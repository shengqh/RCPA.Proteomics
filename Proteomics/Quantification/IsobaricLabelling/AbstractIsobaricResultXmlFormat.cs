using RCPA.Gui;
using System;

namespace RCPA.Proteomics.Quantification.IsobaricLabelling
{
  public abstract class AbstractIsobaricResultXmlFormat : ProgressClass, IIsobaricResultFormat
  {
    public AbstractIsobaricResultXmlFormat(bool hasReporters = true, bool readPeaks = true)
    {
      this.HasReporters = hasReporters;
      this.ReadPeaks = readPeaks;
      this.Accept = m => true;
    }

    #region IFileFormat<IsobaricResult> Members

    public abstract IsobaricResult ReadFromFile(string fileName);

    public abstract void WriteToFile(string fileName, IsobaricResult t);

    #endregion

    #region IIsobaricResultFormat Members

    public bool HasReporters { get; set; }

    public bool ReadPeaks { get; set; }

    public Predicate<IsobaricScan> Accept { get; set; }

    #endregion
  }
}


