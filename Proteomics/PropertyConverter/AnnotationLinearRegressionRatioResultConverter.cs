using RCPA.Proteomics.Quantification;

namespace RCPA.Proteomics.PropertyConverter
{
  public class AnnotationLinearRegressionRatioResult_RatioConverter
    <T> : AbstractPropertyConverter<T> where T : IAnnotation
  {
    private string _name;
    public override string Name
    {
      get
      {
        return this._name;
      }
    }

    public AnnotationLinearRegressionRatioResult_RatioConverter(string name)
    {
      this._name = name;
    }

    public override string GetProperty(T t)
    {
      if (t.Annotations.ContainsKey(this._name))
      {
        var lrrr = t.Annotations[this._name] as LinearRegressionRatioResult;
        return MyConvert.Format("{0:0.0000}", lrrr.Ratio);
      }

      return "";
    }

    public override void SetProperty(T t, string value)
    {
      if (value.Length == 0)
      {
        t.Annotations.Remove(this._name);
        return;
      }

      LinearRegressionRatioResult lrrr;
      if (!t.Annotations.ContainsKey(this._name))
      {
        lrrr = new LinearRegressionRatioResult();
        t.Annotations[this._name] = lrrr;
      }
      else
      {
        lrrr = t.Annotations[this._name] as LinearRegressionRatioResult;
      }

      lrrr.Ratio = MyConvert.ToDouble(value);
    }
  }

  public abstract class AbstractAnnotationLinearRegressionRatioResult_Converter<T> : AbstractPropertyConverter<T>
    where T : IAnnotation
  {
    private string _ratioName;
    private string _name;

    public AbstractAnnotationLinearRegressionRatioResult_Converter(string ratioName, string name)
    {
      _ratioName = ratioName;
      _name = name;
    }

    public override string Name
    {
      get { return _name; }
    }

    protected abstract string GetValue(LinearRegressionRatioResult lrrr);

    public override string GetProperty(T t)
    {
      if (t.Annotations.ContainsKey(_ratioName))
      {
        var lrrr = t.Annotations[_ratioName] as LinearRegressionRatioResult;
        return GetValue(lrrr);
      }

      return "";
    }

    protected abstract void SetValue(LinearRegressionRatioResult lrrr, double v);

    public override void SetProperty(T t, string value)
    {
      if (value.Length == 0)
      {
        t.Annotations.Remove(_ratioName);
        return;
      }

      LinearRegressionRatioResult lrrr;
      if (!t.Annotations.ContainsKey(_ratioName))
      {
        lrrr = new LinearRegressionRatioResult();
        t.Annotations[_ratioName] = lrrr;
      }
      else
      {
        lrrr = t.Annotations[_ratioName] as LinearRegressionRatioResult;
      }

      SetValue(lrrr, MyConvert.ToDouble(value));
    }

  }

  public class AnnotationLinearRegressionRatioResult_RSquareConverter<T> : AbstractAnnotationLinearRegressionRatioResult_Converter<T>
    where T : IAnnotation
  {
    public AnnotationLinearRegressionRatioResult_RSquareConverter(string ratioName, string name) : base(ratioName, name)
    { }

    protected override string GetValue(LinearRegressionRatioResult lrrr)
    {
      return MyConvert.Format("{0:0.0000}", lrrr.RSquare);
    }

    protected override void SetValue(LinearRegressionRatioResult lrrr, double v)
    {
      lrrr.RSquare = v;
    }
  }

  public class AnnotationLinearRegressionRatioResult_FCalcConverter<T> : AbstractAnnotationLinearRegressionRatioResult_Converter<T>
    where T : IAnnotation
  {
    public AnnotationLinearRegressionRatioResult_FCalcConverter(string ratioName, string name) : base(ratioName, name)
    { }

    protected override string GetValue(LinearRegressionRatioResult lrrr)
    {
      return MyConvert.Format("{0:0.0000}", lrrr.TValue);
    }

    protected override void SetValue(LinearRegressionRatioResult lrrr, double v)
    {
      lrrr.TValue = v;
    }
  }

  public class AnnotationLinearRegressionRatioResult_FProbabilityConverter<T> : AbstractAnnotationLinearRegressionRatioResult_Converter<T>
    where T : IAnnotation
  {
    public AnnotationLinearRegressionRatioResult_FProbabilityConverter(string ratioName, string name) : base(ratioName, name)
    { }

    protected override string GetValue(LinearRegressionRatioResult lrrr)
    {
      return MyConvert.Format("{0:0.0000}", lrrr.PValue);
    }

    protected override void SetValue(LinearRegressionRatioResult lrrr, double v)
    {
      lrrr.PValue = v;
    }
  }
}