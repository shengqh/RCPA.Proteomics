using RCPA.Proteomics.Mascot;
using RCPA.Proteomics.Summary;

namespace RCPA.Proteomics.Quantification.ITraq
{
  public class ITraqProteinStatisticBuilder : AbstractITraqProteinStatisticBuilder
  {
    public ITraqProteinStatisticBuilder(ITraqProteinStatisticOption option)
      : base(option)
    { }

    private MascotResultTextFormat format;

    protected override IIdentifiedResult GetIdentifiedResult(string fileName)
    {
      format = new MascotResultTextFormat();
      return format.ReadFromFile(fileName);
    }

    protected override MascotResultTextFormat GetFormat(IIdentifiedResult ir)
    {
      format.InitializeByResult(ir);
      return format;
    }
  }
}
