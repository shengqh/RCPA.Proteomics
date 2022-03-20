namespace RCPA.Proteomics.Quantification
{
  public class QuantificationItemBase
  {
    public bool HasRatio
    {
      get
      {
        return string.IsNullOrEmpty(_state);
      }
    }

    public bool Enabled { get; set; }

    private double _ratio;

    public double Ratio
    {
      get
      {
        return _ratio;
      }
      set
      {
        _ratio = value;
        _state = string.Empty;
      }
    }

    private string _state = string.Empty;

    public string RatioStr
    {
      get
      {
        if (!HasRatio)
        {
          return _state;
        }

        return MyConvert.Format("{0:0.0000}", Ratio);
      }
      set
      {
        if (!string.IsNullOrEmpty(value))
        {
          double res;
          if (MyConvert.TryParse(value, out res))
          {
            _ratio = res;
            _state = string.Empty;
          }
          else
          {
            _state = value;
            _ratio = 1;
          }
        }
        else
        {
          _state = string.Empty;
          _ratio = 1;
        }
      }
    }
  }
}

