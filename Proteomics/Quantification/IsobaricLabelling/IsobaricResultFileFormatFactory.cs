using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RCPA.Proteomics.Quantification.IsobaricLabelling
{
  public static class IsobaricResultFileFormatFactory
  {
    public static IIsobaricResultFormat GetXmlFormat(bool hasReporters = true, bool readPeaks = true)
    {
      return new IsobaricResultXmlFormat(hasReporters, readPeaks);
    }

    public static IsobaricResultXmlFormatRandomReader GetXmlReader(bool readReporters = true, bool readPeaks = true)
    {
      return new IsobaricResultXmlFormatRandomReader(readReporters, readPeaks);
    }
  }
}
