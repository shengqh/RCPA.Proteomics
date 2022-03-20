namespace RCPA.Proteomics.Raw
{
  public interface IMasterScanFinder
  {
    int Find(IRawFile reader, int scan);
  }
}
