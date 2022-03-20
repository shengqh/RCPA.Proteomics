using RCPA.Converter;

namespace RCPA.Proteomics.Summary
{
  public class ProteinLineFormat : LineFormat<IIdentifiedProtein>
  {
    public ProteinLineFormat(string headers)
      : base(IdentifiedProteinPropertyConverterFactory.GetInstance(), headers)
    { }

    public ProteinLineFormat(string headers, string engine)
      : base(IdentifiedProteinPropertyConverterFactory.GetInstance(), headers, engine)
    { }

    public ProteinLineFormat(PropertyConverterFactory<IIdentifiedProtein> factory, string headers)
      : base(factory, headers)
    { }

    public ProteinLineFormat(PropertyConverterFactory<IIdentifiedProtein> factory, string headers, string engine)
      : base(factory, headers, engine)
    { }

  }
}
