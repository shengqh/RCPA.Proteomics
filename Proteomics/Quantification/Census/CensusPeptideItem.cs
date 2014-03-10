using System.Collections.Generic;

namespace RCPA.Proteomics.Quantification.Census
{
  public class CensusPeptideItem : IAnnotation
  {
    private Dictionary<string, object> annotations;
    public bool Singleton { get; set; }

    public string Unique { get; set; }

    public string Sequence { get; set; }

    public double Ratio { get; set; }

    public double RegressionFactor { get; set; }

    public double DeterminantFactor { get; set; }

    public double Score { get; set; }

    public double DeltaScore { get; set; }

    public double SampleIntensity { get; set; }

    public double ReferenceIntensity { get; set; }

    public double AreaRatio { get; set; }

    public double SingletonScore { get; set; }

    public double ProfileScore { get; set; }

    public SequestFilename Filename { get; set; }

    public bool Enabled { get; set; }

    #region IAnnotation Members

    public Dictionary<string, object> Annotations
    {
      get
      {
        if (this.annotations == null)
        {
          this.annotations = new Dictionary<string, object>();
        }
        return this.annotations;
      }
    }

    #endregion

    public bool IsSameChro(CensusPeptideItem another)
    {
      if (null == another)
      {
        return false;
      }

      return Filename.Experimental.Equals(another.Filename.Experimental) &&
             Filename.Charge.Equals(another.Filename.Charge) &&
             Sequence.Equals(another.Sequence) &&
             Ratio.Equals(another.Ratio) &&
             SampleIntensity.Equals(another.SampleIntensity) &&
             ReferenceIntensity.Equals(another.ReferenceIntensity);
    }

    private static CensusPeptideItem Parse(string line)
    {
      var result = new CensusPeptideItem();

      if (line.StartsWith("S\t"))
      {
        result.Singleton = false;
      }
      else if (line.StartsWith("&S\t"))
      {
        result.Singleton = true;
      }
      else
      {
        return null;
      }

      string[] parts = line.Split(new[] {'\t'});
      if (parts.Length < 13)
      {
        return null;
      }

      result.Unique = parts[1];

      result.Sequence = parts[2];

      result.Ratio = MyConvert.ToDouble(parts[3]);

      result.RegressionFactor = MyConvert.ToDouble(parts[4]);

      result.DeterminantFactor = MyConvert.ToDouble(parts[5]);

      result.Score = MyConvert.ToDouble(parts[6]);

      result.DeltaScore = MyConvert.ToDouble(parts[7]);

      result.SampleIntensity = MyConvert.ToDouble(parts[8]);

      result.ReferenceIntensity = MyConvert.ToDouble(parts[9]);

      result.AreaRatio = MyConvert.ToDouble(parts[10]);

      result.SingletonScore = MyConvert.ToDouble(parts[11]);

      if (parts[12].EndsWith("."))
      {
        result.Filename = SequestFilename.Parse(parts[12]);
      }
      else
      {
        result.Filename = SequestFilename.Parse(parts[12] + ".");
      }

      return result;
    }
  }
}