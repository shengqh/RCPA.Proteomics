using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RCPA.Converter;

namespace RCPA.Proteomics.Summary
{
  public class PeptideLineFormat : LineFormat<IIdentifiedSpectrum>
  {
    public PeptideLineFormat(string headers)
      : base(IdentifiedSpectrumPropertyConverterFactory.GetInstance(), headers)
    { }

    public PeptideLineFormat(string headers, string engine)
      : base(IdentifiedSpectrumPropertyConverterFactory.GetInstance(), headers, engine)
    { }

    public PeptideLineFormat(string headers, string engine, List<IIdentifiedSpectrum> items)
      : base(IdentifiedSpectrumPropertyConverterFactory.GetInstance(), headers, engine, items)
    { }
  }
}
