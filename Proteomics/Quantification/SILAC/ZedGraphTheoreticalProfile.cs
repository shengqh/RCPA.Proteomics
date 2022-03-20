using RCPA.Proteomics.Isotopic;
using ZedGraph;

namespace RCPA.Proteomics.Quantification.SILAC
{
  public class ZedGraphTheoreticalProfiles
  {
    private ZedGraphProfiles zgcProfiles;

    public ZedGraphTheoreticalProfiles(ZedGraphControl zgcGraph)
    {
      zgcProfiles = new ZedGraphProfiles(zgcGraph);
    }

    public void Update(object sender, UpdateQuantificationItemEventArgs e)
    {

      zgcProfiles.Update(e.Item as SilacQuantificationSummaryItem);
    }
  }
}
