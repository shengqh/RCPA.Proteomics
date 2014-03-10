using System;
using System.Collections.Generic;
using System.Text;
using RCPA.Proteomics.Summary;
using RCPA.Gui;
using RCPA.Utils;
using System.IO;
using RCPA.Proteomics.Modification;

namespace RCPA.Proteomics.XTandem
{
  public class XTandemSpectrumXmlBuilder : ProgressClass, IIdentifiedSpectrumBuilder
  {
    public XTandemSpectrumXmlBuilder() { }

    #region IIdentifiedSpectrumBuilder Members

    public List<IIdentifiedSpectrum> Build(string parameterFile)
    {
      return null;
    }

    #endregion
  }
}