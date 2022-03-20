namespace RCPA.Proteomics.Raw
{
  public interface IRawFormat
  {
    string Description { get; }

    string GetRawFile(string rawDir, string experimental);

    string GetRawFile(string rawDir, string experimental, bool existOnly);

    IRawFile2 GetRawFile();
  }
}
