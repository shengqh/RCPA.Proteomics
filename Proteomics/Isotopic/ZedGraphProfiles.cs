using ZedGraph;

namespace RCPA.Proteomics.Isotopic
{
  public class ZedGraphProfiles
  {
    private ZedGraphControl zgcGraph;

    public ZedGraphProfiles(ZedGraphControl zgcGraph)
    {
      this.zgcGraph = zgcGraph;

      ZedGraphicExtension.InitGraph(zgcGraph, "Theoretical Isotopic", "Isotopic Position", "Abundance Percentage", true, 0.0);
    }

    public void Update(IIsotopicProfiles profiles)
    {
      ZedGraphicExtension.ClearData(this.zgcGraph, false);
      try
      {
        foreach (var profile in profiles.GetIsotopicProfiles())
        {
          var ppl = new PointPairList();
          for (int i = 0; i < profile.Profile.Count; i++)
          {
            ppl.Add((i + 1), profile.Profile[i].Intensity);
          }

          ZedGraphicExtension.AddDataToBar(this.zgcGraph, ppl, profile.DisplayName, profile.DisplayColor, true);
        }
      }
      finally
      {
        ZedGraphicExtension.UpdateGraph(this.zgcGraph);
      }
    }
  }
}
