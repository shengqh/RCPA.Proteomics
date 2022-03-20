using RCPA.Converter;
using RCPA.Proteomics.PropertyConverter;
using RCPA.Proteomics.PropertyConverter.Mascot;
using RCPA.Proteomics.Quantification;
using RCPA.Proteomics.Quantification.ITraq;

namespace RCPA.Proteomics.Summary
{
  public sealed class IdentifiedProteinPropertyConverterFactory : PropertyConverterFactory<IIdentifiedProtein>
  {
    private IdentifiedProteinPropertyConverterFactory() { }

    public static IdentifiedProteinPropertyConverterFactory GetInstance()
    {
      IdentifiedProteinPropertyConverterFactory result = new IdentifiedProteinPropertyConverterFactory();

      IPropertyConverter<IIdentifiedProtein> nameConverter = new IdentifiedProteinNameConverter<IIdentifiedProtein>();
      result.RegisterConverter(nameConverter);
      result.RegisterConverter(new PropertyAliasConverter<IIdentifiedProtein>(nameConverter, "Locus"));

      IPropertyConverter<IIdentifiedProtein> despConverter = new IdentifiedProteinDescriptionConverter<IIdentifiedProtein>();
      result.RegisterConverter(despConverter);

      IPropertyConverter<IIdentifiedProtein> refConverter = new IdentifiedProteinReferenceConverter<IIdentifiedProtein>();
      result.RegisterConverter(refConverter);
      result.RegisterConverter(new PropertyAliasConverter<IIdentifiedProtein>(refConverter, "Descriptive Name"));

      IPropertyConverter<IIdentifiedProtein> massConverter = new IdentifiedProteinMassConverter<IIdentifiedProtein>();
      result.RegisterConverter(massConverter);
      result.RegisterConverter(new PropertyAliasConverter<IIdentifiedProtein>(massConverter, "MW"));
      result.RegisterConverter(new PropertyAliasConverter<IIdentifiedProtein>(massConverter, "MolWt"));

      result.RegisterConverter(new MascotProteinTotalScoreConverter<IIdentifiedProtein>());

      IPropertyConverter<IIdentifiedProtein> uniqueConverter = new IdentifiedProteinUniquePeptideCountConverter<IIdentifiedProtein>();
      result.RegisterConverter(uniqueConverter);
      result.RegisterConverter(new PropertyAliasConverter<IIdentifiedProtein>(uniqueConverter, "UniquePepCount"));
      result.RegisterConverter(new PropertyAliasConverter<IIdentifiedProtein>(uniqueConverter, "Sequence Count"));

      IPropertyConverter<IIdentifiedProtein> spectrumConverter = new IdentifiedProteinSpectrumCountConverter<IIdentifiedProtein>();
      result.RegisterConverter(spectrumConverter);
      result.RegisterConverter(new PropertyAliasConverter<IIdentifiedProtein>(spectrumConverter, "Spectrum Count"));

      result.RegisterConverter(new AnnotationLinearRegressionRatioResult_RatioConverter<IIdentifiedProtein>("LR_Ratio"));
      result.RegisterConverter(new AnnotationLinearRegressionRatioResult_RSquareConverter<IIdentifiedProtein>("LR_Ratio", "LR_RSquare"));
      result.RegisterConverter(new AnnotationLinearRegressionRatioResult_FCalcConverter<IIdentifiedProtein>("LR_Ratio", "LR_FCalc"));
      result.RegisterConverter(new AnnotationLinearRegressionRatioResult_FProbabilityConverter<IIdentifiedProtein>("LR_Ratio", "LR_FProb"));

      IPropertyConverter<IIdentifiedProtein> coverageConverter = new IdentifiedProteinCoverageConverter<IIdentifiedProtein>();
      result.RegisterConverter(coverageConverter);
      result.RegisterConverter(new PropertyAliasConverter<IIdentifiedProtein>(coverageConverter, "Sequence Coverage"));
      result.RegisterConverter(new PropertyAliasConverter<IIdentifiedProtein>(coverageConverter, "CoverPercent"));

      IPropertyConverter<IIdentifiedProtein> piConverter = new IdentifiedProteinIsoelectricPointConverterter<IIdentifiedProtein>();
      result.RegisterConverter(piConverter);
      result.RegisterConverter(new PropertyAliasConverter<IIdentifiedProtein>(piConverter, "pI"));

      result.RegisterConverter(new AnnotationConverter<IIdentifiedProtein>("Length", "1"));
      result.RegisterConverter(new AnnotationConverter<IIdentifiedProtein>("Validation Status", "U"));

      result.RegisterConverter(new QuantificationItemRatioConverter<IIdentifiedProtein>());
      result._ignoreKey.Add("S_FILE");
      result._ignoreKey.Add("S_SCANS");

      result.RegisterConverter(new SilacProteinQuantificationResultConverter2<IIdentifiedProtein>());
      result.RegisterConverter(new ITraqQuantificationResultConverter<IIdentifiedProtein>());

      result.RegisterConverter(new IdentifiedProteinDecoyConverter<IIdentifiedProtein>());

      return result;
    }

    public override IIdentifiedProtein Allocate()
    {
      return new IdentifiedProtein();
    }
  }
}
