using RCPA.Proteomics.Summary;
using RCPA.Proteomics.Summary.Uniform;

namespace RCPA.Proteomics.XTandem
{
  public class XtandemDatasetBuilder : AbstractOneParserDatasetBuilder<XtandemDatasetOptions>
  {
    public XtandemDatasetBuilder(XtandemDatasetOptions options) : base(options) { }
  }
}