namespace RCPA.Proteomics.Summary.Uniform
{
  public interface IDatasetFactory
  {
    string Name { get; }

    IDatasetOptions CreateOptions();
  }
}
