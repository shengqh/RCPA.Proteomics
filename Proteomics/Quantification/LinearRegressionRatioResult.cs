namespace RCPA.Proteomics.Quantification
{
  public class LinearRegressionRatioResult
  {
    public static readonly string Header = "LR_Ratio\tLR_RSquare\tLR_FCalc\tLR_FProbability";

    public LinearRegressionRatioResult()
      : this(0.0, 0.0)
    {
    }

    public LinearRegressionRatioResult(double ratio, double rSquare)
    {
      this.Ratio = ratio;
      this.RSquare = rSquare;
      this.PointCount = 0;
      this.TValue = 0.0;
      this.PValue = 0.0;
      this.Distance = 0.0;
      this.ReferenceIntensity = 0.0;
      this.SampleIntensity = 0.0;
    }

    public double ReferenceIntensity { get; set; }

    public double SampleIntensity { get; set; }

    /// <summary>
    /// Linear regression ratio
    /// </summary>
    public double Ratio {get;set;}

    /// <summary>
    /// Linear regression distance
    /// </summary>
    public double Distance { get; set; }

    /// <summary>
    /// R Square of linear regression model
    /// </summary>
    public double RSquare{get;set;}

    /// <summary>
    /// Point count used in linear regression model
    /// </summary>
    public double PointCount { get; set; }

    /// <summary>
    /// t statistic value of linear regression model
    /// </summary>
    public double TValue{get;set;}

    /// <summary>
    /// Stand deviation of linear regression model
    /// </summary>
    public double Stdev { get; set; }

    /// <summary>
    /// Pvalue of linear regression model
    /// </summary>
    public double PValue{get;set;}

    //public override string ToString()
    //{
    //  return MyConvert.Format("{0:0.0000},{1:0.0000},{2:0.0000},{3:0.0000}",
    //                       this.ratio,
    //                       this.rSquare,
    //                       this.fCalculatedValue,
    //                       this.fProbability);
    //}

    public override string ToString()
    {
      return MyConvert.Format("{0:0.0000}", this.Ratio);
    }

    public void CopyFrom(LinearRegressionRatioResult source)
    {
      if(source == null)
      {
        return;
      }

      this.Ratio = source.Ratio;
      this.RSquare = source.RSquare;
      this.PointCount = source.PointCount;
      this.TValue = source.TValue;
      this.PValue = source.PValue;
      this.Distance = source.Distance;
      this.ReferenceIntensity = source.ReferenceIntensity;
      this.SampleIntensity = source.SampleIntensity;
    }

  }
}