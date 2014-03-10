namespace RCPA.Proteomics.Quantification
{
  public class LinearRegressionRatioResult
  {
    public static readonly string Header = "LR_Ratio\tLR_RSquare\tLR_FCalc\tLR_FProbability";
    private double fCalculatedValue;
    private double fProbability;
    private double ratio;
    private double rSquare;

    public LinearRegressionRatioResult()
      : this(0.0, 0.0)
    {
    }

    public LinearRegressionRatioResult(double ratio, double rSquare)
    {
      this.ratio = ratio;
      this.rSquare = rSquare;
      this.PointCount = 0;
      this.fCalculatedValue = 0.0;
      this.fProbability = 0.0;
      this.Distance = 0.0;
    }

    /// <summary>
    /// Linear regression ratio
    /// </summary>
    public double Ratio
    {
      get { return this.ratio; }
      set { this.ratio = value; }
    }

    /// <summary>
    /// Linear regression distance
    /// </summary>
    public double Distance { get; set; }

    /// <summary>
    /// R Square of linear regression
    /// </summary>
    public double RSquare
    {
      get { return this.rSquare; }
      set { this.rSquare = value; }
    }

    /// <summary>
    /// Point count in linear regression
    /// </summary>
    public double PointCount { get; set; }

    /// <summary>
    /// F-test calculated value of linear regression
    /// </summary>
    public double FCalculatedValue
    {
      get { return this.fCalculatedValue; }
      set { this.fCalculatedValue = value; }
    }

    /// <summary>
    /// F-test probability of linear regression
    /// </summary>
    public double FProbability
    {
      get { return this.fProbability; }
      set { this.fProbability = value; }
    }

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
      return MyConvert.Format("{0:0.0000}", this.ratio);
    }
  }
}