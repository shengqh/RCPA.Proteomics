using RCPA.Proteomics.Summary;

namespace RCPA.Proteomics.Mascot
{
  public class MascotProteinTextReader2
  {
    private readonly IPropertyConverter<IIdentifiedProtein> converter;

    public MascotProteinTextReader2(string headers)
    {
      this.converter = IdentifiedProteinPropertyConverterFactory.GetInstance().GetConverters(headers, '\t');
    }

    public MascotProteinTextReader2(string headers, string version)
    {
      this.converter = IdentifiedProteinPropertyConverterFactory.GetInstance().GetConverters(headers, '\t', version);
    }

    public IIdentifiedProtein ParseString(string line)
    {
      IIdentifiedProtein result = new IdentifiedProtein();

      this.converter.SetProperty(result, line);

      return result;
    }
  }
}