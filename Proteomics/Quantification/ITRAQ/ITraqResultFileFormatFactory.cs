using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RCPA.Proteomics.Quantification.ITraq
{
  public static class ITraqResultFileFormatFactory
  {
    public static IITraqResultFileFormat GetXmlFormat()
    {
      return new ITraqResultXmlFormatFast();
    }

    public static ITraqResultXmlFormatRandomReader GetXmlReader()
    {
      return new ITraqResultXmlFormatRandomReader();
    }
  }
}
