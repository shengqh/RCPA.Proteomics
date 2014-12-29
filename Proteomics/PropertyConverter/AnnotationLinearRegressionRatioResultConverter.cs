using RCPA.Proteomics.Quantification;

namespace RCPA.Proteomics.PropertyConverter
{
  public class AnnotationLinearRegressionRatioResult_RatioConverter
    <T> : AbstractPropertyConverter<T> where T : IAnnotation
  {
    public override string Name
    {
      get { return "LR_Ratio"; }
    }

    public override string GetProperty(T t)
    {
      if (t.Annotations.ContainsKey(LinearRegressionRatioResult.Header))
      {
        var lrrr = t.Annotations[LinearRegressionRatioResult.Header] as LinearRegressionRatioResult;
        return MyConvert.Format("{0:0.0000}", lrrr.Ratio);
      }

      return "";
    }

    public override void SetProperty(T t, string value)
    {
      if (value.Length == 0)
      {
        t.Annotations.Remove(LinearRegressionRatioResult.Header);
        return;
      }

      LinearRegressionRatioResult lrrr;
      if (!t.Annotations.ContainsKey(LinearRegressionRatioResult.Header))
      {
        lrrr = new LinearRegressionRatioResult();
        t.Annotations[LinearRegressionRatioResult.Header] = lrrr;
      }
      else
      {
        lrrr = t.Annotations[LinearRegressionRatioResult.Header] as LinearRegressionRatioResult;
      }

      lrrr.Ratio = MyConvert.ToDouble(value);
    }
  }

  public class AnnotationLinearRegressionRatioResult_RSquareConverter<T> : AbstractPropertyConverter<T>
    where T : IAnnotation
  {
    public override string Name
    {
      get { return "LR_RSquare"; }
    }

    public override string GetProperty(T t)
    {
      if (t.Annotations.ContainsKey(LinearRegressionRatioResult.Header))
      {
        var lrrr = t.Annotations[LinearRegressionRatioResult.Header] as LinearRegressionRatioResult;
        return MyConvert.Format("{0:0.0000}", lrrr.RSquare);
      }

      return "";
    }

    public override void SetProperty(T t, string value)
    {
      if (value.Length == 0)
      {
        t.Annotations.Remove(LinearRegressionRatioResult.Header);
        return;
      }

      LinearRegressionRatioResult lrrr;
      if (!t.Annotations.ContainsKey(LinearRegressionRatioResult.Header))
      {
        lrrr = new LinearRegressionRatioResult();
        t.Annotations[LinearRegressionRatioResult.Header] = lrrr;
      }
      else
      {
        lrrr = t.Annotations[LinearRegressionRatioResult.Header] as LinearRegressionRatioResult;
      }

      lrrr.RSquare = MyConvert.ToDouble(value);
    }
  }

  public class AnnotationLinearRegressionRatioResult_FCalcConverter<T> : AbstractPropertyConverter<T>
    where T : IAnnotation
  {
    public override string Name
    {
      get { return "LR_FCalc"; }
    }

    public override string GetProperty(T t)
    {
      if (t.Annotations.ContainsKey(LinearRegressionRatioResult.Header))
      {
        var lrrr = t.Annotations[LinearRegressionRatioResult.Header] as LinearRegressionRatioResult;
        return MyConvert.Format("{0:0.0000}", lrrr.TValue);
      }

      return "";
    }

    public override void SetProperty(T t, string value)
    {
      if (value.Length == 0)
      {
        t.Annotations.Remove(LinearRegressionRatioResult.Header);
        return;
      }

      LinearRegressionRatioResult lrrr;
      if (!t.Annotations.ContainsKey(LinearRegressionRatioResult.Header))
      {
        lrrr = new LinearRegressionRatioResult();
        t.Annotations[LinearRegressionRatioResult.Header] = lrrr;
      }
      else
      {
        lrrr = t.Annotations[LinearRegressionRatioResult.Header] as LinearRegressionRatioResult;
      }

      lrrr.TValue = MyConvert.ToDouble(value);
    }
  }

  public class AnnotationLinearRegressionRatioResult_FProbabilityConverter<T> : AbstractPropertyConverter<T>
    where T : IAnnotation
  {
    public override string Name
    {
      get { return "LR_FProbability"; }
    }

    public override string GetProperty(T t)
    {
      if (t.Annotations.ContainsKey(LinearRegressionRatioResult.Header))
      {
        var lrrr = t.Annotations[LinearRegressionRatioResult.Header] as LinearRegressionRatioResult;
        return MyConvert.Format("{0:0.0000}", lrrr.PValue);
      }

      return "";
    }

    public override void SetProperty(T t, string value)
    {
      if (value.Length == 0)
      {
        t.Annotations.Remove(LinearRegressionRatioResult.Header);
        return;
      }

      LinearRegressionRatioResult lrrr;
      if (!t.Annotations.ContainsKey(LinearRegressionRatioResult.Header))
      {
        lrrr = new LinearRegressionRatioResult();
        t.Annotations[LinearRegressionRatioResult.Header] = lrrr;
      }
      else
      {
        lrrr = t.Annotations[LinearRegressionRatioResult.Header] as LinearRegressionRatioResult;
      }

      lrrr.PValue = MyConvert.ToDouble(value);
    }
  }
}