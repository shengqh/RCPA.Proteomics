namespace RCPA.Proteomics.Image
{
  public interface INeutralLossType
  {
    string Name { get; }

    double Mass { get; }

    bool CanMultipleLoss { get; }

    int Count { get; }
  }
}
