using RCPA.Proteomics.XTandem;

namespace RCPA.Proteomics.Summary.Uniform
{
  public class XtandemDatasetBuilder : AbstractOneParserDatasetBuilder<XtandemDatasetOptions>
  {
    public XtandemDatasetBuilder(XtandemDatasetOptions options) : base(options) { }

    protected override ISpectrumParser GetSpectrumParser()
    {
      var datParser = new XTandemSpectrumXmlParser(options.GetTitleParser(), options.Parent.Database.GetAccessNumberParser());
      datParser.Progress = Progress;
      return datParser;
    }
  }
}