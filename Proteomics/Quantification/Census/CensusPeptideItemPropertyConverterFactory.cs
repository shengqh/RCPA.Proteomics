using RCPA.Converter;
namespace RCPA.Proteomics.Quantification.Census
{
  public sealed class CensusPeptideItemPropertyConverterFactory : PropertyConverterFactory<CensusPeptideItem>
  {
    private CensusPeptideItemPropertyConverterFactory()
    {
    }

    private static CensusPeptideItemPropertyConverterFactory instance;

    public static CensusPeptideItemPropertyConverterFactory GetInstance()
    {
      if (instance == null)
      {
        instance = new CensusPeptideItemPropertyConverterFactory();

        instance.RegisterConverter(new CensusPeptideItem_UNIQUE_Converter());
        instance.RegisterConverter(new CensusPeptideItem_SEQUENCE_Converter());

        IPropertyConverter<CensusPeptideItem> ratioConverter = new CensusPeptideItem_RATIO_Converter();
        instance.RegisterConverter(ratioConverter);

        instance.RegisterConverter(new CensusPeptideItem_REGRESSION_FACTOR_Converter());
        instance.RegisterConverter(new CensusPeptideItem_DETERMINANT_FACTOR_Converter());

        IPropertyConverter<CensusPeptideItem> samIntConverter = new CensusPeptideItem_SAM_INT_Converter();
        instance.RegisterConverter(samIntConverter);

        IPropertyConverter<CensusPeptideItem> refIntConverter = new CensusPeptideItem_REF_INT_Converter();
        instance.RegisterConverter(refIntConverter);

        instance.RegisterConverter(new CensusPeptideItem_AREA_RATIO_Converter());
        instance.RegisterConverter(new CensusPeptideItem_PROFILE_SCORE_Converter());
        instance.RegisterConverter(new CensusPeptideItem_FILE_NAME_Converter());
        instance.RegisterConverter(new CensusPeptideItem_SCAN_Converter());
      }

      return instance;
    }

    public override CensusPeptideItem Allocate()
    {
      return new CensusPeptideItem();
    }
  }

  //UNIQUE	SEQUENCE	RATIO	REGRESSION_FACTOR	DETERMINANT_FACTOR	SAM_INT	REF_INT	AREA_RATIO	PROFILE_SCORE	FILE_NAME
  public class CensusPeptideItem_UNIQUE_Converter : AbstractPropertyConverter<CensusPeptideItem>
  {
    public override string Name
    {
      get { return "UNIQUE"; }
    }

    public override string GetProperty(CensusPeptideItem t)
    {
      return t.Unique;
    }

    public override void SetProperty(CensusPeptideItem t, string value)
    {
      t.Unique = value;
    }
  }

  public class CensusPeptideItem_SEQUENCE_Converter : AbstractPropertyConverter<CensusPeptideItem>
  {
    public override string Name
    {
      get { return "SEQUENCE"; }
    }

    public override string GetProperty(CensusPeptideItem t)
    {
      return t.Sequence;
    }

    public override void SetProperty(CensusPeptideItem t, string value)
    {
      t.Sequence = value;
    }
  }

  public class CensusPeptideItem_RATIO_Converter : AbstractPropertyConverter<CensusPeptideItem>
  {
    public override string Name
    {
      get { return "RATIO"; }
    }

    public override string GetProperty(CensusPeptideItem t)
    {
      if (t.Ratio == -1)
      {
        return "OL";
      }

      return MyConvert.Format("{0:0.00}", t.Ratio);
    }

    public override void SetProperty(CensusPeptideItem t, string value)
    {
      if (value.Equals("OL"))
      {
        t.Ratio = -1;
      }
      else
      {
        t.Ratio = MyConvert.ToDouble(value);
      }
    }
  }

  public class CensusPeptideItem_REGRESSION_FACTOR_Converter : AbstractPropertyConverter<CensusPeptideItem>
  {
    public override string Name
    {
      get { return "REGRESSION_FACTOR"; }
    }

    public override string GetProperty(CensusPeptideItem t)
    {
      return MyConvert.Format("{0:0.00}", t.RegressionFactor);
    }

    public override void SetProperty(CensusPeptideItem t, string value)
    {
      t.RegressionFactor = MyConvert.ToDouble(value);
    }
  }

  public class CensusPeptideItem_DETERMINANT_FACTOR_Converter : AbstractPropertyConverter<CensusPeptideItem>
  {
    public override string Name
    {
      get { return "DETERMINANT_FACTOR"; }
    }

    public override string GetProperty(CensusPeptideItem t)
    {
      return MyConvert.Format("{0:0.00}", t.DeterminantFactor);
    }

    public override void SetProperty(CensusPeptideItem t, string value)
    {
      t.DeterminantFactor = MyConvert.ToDouble(value);
    }
  }

  public class CensusPeptideItem_SAM_INT_Converter : AbstractPropertyConverter<CensusPeptideItem>
  {
    public override string Name
    {
      get { return "SAM_INT"; }
    }

    public override string GetProperty(CensusPeptideItem t)
    {
      return MyConvert.Format("{0:E2}", t.SampleIntensity);
    }

    public override void SetProperty(CensusPeptideItem t, string value)
    {
      t.SampleIntensity = MyConvert.ToDouble(value);
    }
  }

  public class CensusPeptideItem_REF_INT_Converter : AbstractPropertyConverter<CensusPeptideItem>
  {
    public override string Name
    {
      get { return "REF_INT"; }
    }

    public override string GetProperty(CensusPeptideItem t)
    {
      return MyConvert.Format("{0:E2}", t.ReferenceIntensity);
    }

    public override void SetProperty(CensusPeptideItem t, string value)
    {
      t.ReferenceIntensity = MyConvert.ToDouble(value);
    }
  }

  public class CensusPeptideItem_AREA_RATIO_Converter : AbstractPropertyConverter<CensusPeptideItem>
  {
    public override string Name
    {
      get { return "AREA_RATIO"; }
    }

    public override string GetProperty(CensusPeptideItem t)
    {
      return MyConvert.Format("{0:0.00}", t.AreaRatio);
    }

    public override void SetProperty(CensusPeptideItem t, string value)
    {
      if (value.Equals("INF"))
      {
        t.AreaRatio = 0.0;
      }
      else
      {
        t.AreaRatio = MyConvert.ToDouble(value);
      }
    }
  }

  public class CensusPeptideItem_PROFILE_SCORE_Converter : AbstractPropertyConverter<CensusPeptideItem>
  {
    public override string Name
    {
      get { return "PROFILE_SCORE"; }
    }

    public override string GetProperty(CensusPeptideItem t)
    {
      return MyConvert.Format("{0:0.00}", t.ProfileScore);
    }

    public override void SetProperty(CensusPeptideItem t, string value)
    {
      t.ProfileScore = MyConvert.ToDouble(value);
    }
  }

  public class CensusPeptideItem_FILE_NAME_Converter : AbstractPropertyConverter<CensusPeptideItem>
  {
    private bool PureExperiment = false;

    public override string Name
    {
      get { return "FILE_NAME"; }
    }

    public override string GetProperty(CensusPeptideItem t)
    {
      if (PureExperiment)
      {
        return t.Filename.Experimental;
      }
      else
      {
        return t.Filename.LongFileName;
      }
    }

    public override void SetProperty(CensusPeptideItem t, string value)
    {
      if (SequestFilename.IsLongFilename(value))
      {
        PureExperiment = false;
        t.Filename = SequestFilename.Parse(value);
      }
      else
      {
        PureExperiment = true;
        if (t.Filename == null)
        {
          t.Filename = new SequestFilename();
        }
        t.Filename.Experimental = value;
      }
    }
  }

  public class CensusPeptideItem_SCAN_Converter : AbstractPropertyConverter<CensusPeptideItem>
  {
    public override string Name
    {
      get { return "SCAN"; }
    }

    public override string GetProperty(CensusPeptideItem t)
    {
      return t.Filename.FirstScan.ToString();
    }

    public override void SetProperty(CensusPeptideItem t, string value)
    {
      t.Filename.FirstScan = int.Parse(value);
      t.Filename.LastScan = t.Filename.FirstScan;
    }
  }
}