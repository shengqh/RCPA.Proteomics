namespace RCPA.Proteomics.Isotopic
{
  public static class IsotopicBuilderFactory
  {
    public static IIsotopicProfileBuilder2 GetBuilder()
    {
      return new EmassProfileBuilder();
    }
  }
}
