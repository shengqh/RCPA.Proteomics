using RCPA.Proteomics.Summary;
using RCPA.Proteomics.Summary.Uniform;

namespace RCPA.Proteomics.XTandem
{
  public class XTandemDatasetBuilder : AbstractOneParserDatasetBuilder<XTandemDatasetOptions>
  {
    public XTandemDatasetBuilder(XTandemDatasetOptions options) : base(options) { }
  }
}