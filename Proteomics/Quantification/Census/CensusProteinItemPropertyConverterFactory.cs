using RCPA.Converter;
using System;
namespace RCPA.Proteomics.Quantification.Census
{
  public sealed class CensusProteinItemPropertyConverterFactory : PropertyConverterFactory<CensusProteinItem>
  {
    private CensusProteinItemPropertyConverterFactory()
    {
    }

    private static CensusProteinItemPropertyConverterFactory instance;

    public static CensusProteinItemPropertyConverterFactory GetInstance()
    {
      if (null == instance)
      {
        instance = new CensusProteinItemPropertyConverterFactory();

        instance.RegisterConverter(new CensusProteinItem_LOCUS_Converter());

        IPropertyConverter<CensusProteinItem> averageRatioConverter = new CensusProteinItem_WEIGHTED_AVERAGE_RATIO_Converter();
        instance.RegisterConverter(averageRatioConverter);
        instance.RegisterConverter(new PropertyAliasConverter<CensusProteinItem>(averageRatioConverter, "AVG(A_INT/B_INT)"));

        IPropertyConverter<CensusProteinItem> sdConverter = new CensusProteinItem_STANDARD_DEVIATION_Converter();
        instance.RegisterConverter(sdConverter);
        instance.RegisterConverter(new PropertyAliasConverter<CensusProteinItem>(sdConverter, "STDEV(A_INT/B_INT)"));

        instance.RegisterConverter(new CensusProteinItem_PEPTIDE_NUM_Converter());
        instance.RegisterConverter(new CensusProteinItem_SPEC_COUNT_Converter());
        instance.RegisterConverter(new CensusProteinItem_DESCRIPTION_Converter());
      }

      return instance;
    }

    public override CensusProteinItem Allocate()
    {
      return new CensusProteinItem();
    }
  }

  public class CensusProteinItem_LOCUS_Converter : AbstractPropertyConverter<CensusProteinItem>
  {
    public override string Name
    {
      get { return "LOCUS"; }
    }

    public override string GetProperty(CensusProteinItem t)
    {
      return t.Locus;
    }

    public override void SetProperty(CensusProteinItem t, string value)
    {
      t.Locus = value;
    }
  }

  public class CensusProteinItem_AVERAGE_RATIO_Converter : AbstractPropertyConverter<CensusProteinItem>
  {
    public override string Name
    {
      get { return "AVERAGE_RATIO"; }
    }

    public override string GetProperty(CensusProteinItem t)
    {
      if (t.AverageRatio == 0.0)
      {
        return "NA";
      }
      else
      {
        return MyConvert.Format("{0:0.00}", t.AverageRatio);
      }
    }

    public override void SetProperty(CensusProteinItem t, string value)
    {
      if (value.Equals("NA") || 0 == value.Length || value.Equals("?"))
      {
        t.AverageRatio = 0.0;
      }
      else
      {
        t.AverageRatio = MyConvert.ToDouble(value);
      }
    }
  }

  public class CensusProteinItem_STANDARD_DEVIATION_Converter : AbstractPropertyConverter<CensusProteinItem>
  {
    public override string Name
    {
      get { return "STANDARD_DEVIATION"; }
    }

    public override string GetProperty(CensusProteinItem t)
    {
      if (t.StandardDeviation == 0.0)
      {
        return "NA";
      }
      else
      {
        return MyConvert.Format("{0:0.00}", t.StandardDeviation);
      }
    }

    public override void SetProperty(CensusProteinItem t, string value)
    {
      if (value.Equals("NA") || 0 == value.Length || value.Equals("?"))
      {
        t.StandardDeviation = 0.0;
      }
      else
      {
        t.StandardDeviation = MyConvert.ToDouble(value);
      }
    }
  }

  public class CensusProteinItem_WEIGHTED_AVERAGE_RATIO_Converter : AbstractPropertyConverter<CensusProteinItem>
  {
    public override string Name
    {
      get { return "WEIGHTED_AVERAGE"; }
    }

    public override string GetProperty(CensusProteinItem t)
    {
      if (t.WeightedAverage == 0.0)
      {
        return "NA";
      }
      else
      {
        return MyConvert.Format("{0:0.00}", t.WeightedAverage);
      }
    }

    public override void SetProperty(CensusProteinItem t, string value)
    {
      if (value.Equals("NA") || 0 == value.Length || value.Equals("?"))
      {
        t.WeightedAverage = 0.0;
      }
      else
      {
        try
        {
          t.WeightedAverage = MyConvert.ToDouble(value);
        }
        catch (Exception)
        {
          t.WeightedAverage = 0.0;
        }
      }
    }
  }

  public class CensusProteinItem_PEPTIDE_NUM_Converter : AbstractPropertyConverter<CensusProteinItem>
  {
    public override string Name
    {
      get { return "PEPTIDE_NUM"; }
    }

    public override string GetProperty(CensusProteinItem t)
    {
      return t.PeptideNumber.ToString();
    }

    public override void SetProperty(CensusProteinItem t, string value)
    {
      if (value.Equals("NA") || 0 == value.Length || value.Equals("?"))
      {
        t.PeptideNumber = 0;
      }
      else
      {
        t.PeptideNumber = int.Parse(value);
      }
    }
  }

  public class CensusProteinItem_SPEC_COUNT_Converter : AbstractPropertyConverter<CensusProteinItem>
  {
    public override string Name
    {
      get { return "SPEC_COUNT"; }
    }

    public override string GetProperty(CensusProteinItem t)
    {
      return t.SpectraCount.ToString();
    }

    public override void SetProperty(CensusProteinItem t, string value)
    {
      t.SpectraCount = int.Parse(value);
    }
  }

  public class CensusProteinItem_DESCRIPTION_Converter : AbstractPropertyConverter<CensusProteinItem>
  {
    public override string Name
    {
      get { return "DESCRIPTION"; }
    }

    public override string GetProperty(CensusProteinItem t)
    {
      return t.Description;
    }

    public override void SetProperty(CensusProteinItem t, string value)
    {
      t.Description = value;
    }
  }
}