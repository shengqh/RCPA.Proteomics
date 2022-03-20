namespace RCPA.Proteomics.Summary
{
  public delegate bool ScoreCompareFunc(double score, double limitScore);

  public class ScoreComparator
  {
    public static bool LessThanOrEquals(double score, double limitScore)
    {
      return score <= limitScore;
    }


    public static bool LargeThanOrEquals(double score, double limitScore)
    {
      return score >= limitScore;
    }
  }
}
