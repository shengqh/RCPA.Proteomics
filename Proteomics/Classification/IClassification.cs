namespace RCPA.Proteomics.Classification
{
  public interface IClassification<E>
  {
    string GetPrinciple();

    string GetClassification(E obj);
  }
}
