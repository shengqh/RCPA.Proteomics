namespace RCPA.Proteomics.Summary.Uniform
{
  public interface ITitleDatasetOptions : IDatasetOptions
  {
    string TitleParserName { get; set; }

    ITitleParser GetTitleParser();
  }
}
