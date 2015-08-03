using RCPA.Proteomics.PropertyConverter;
using RCPA.Proteomics.Quantification;
using RCPA.Proteomics.MaxQuant;
using RCPA.Proteomics.Quantification.ITraq;
using RCPA.Converter;

namespace RCPA.Proteomics.Summary
{
  public sealed class IdentifiedSpectrumPropertyConverterFactory : PropertyConverterFactory<IIdentifiedSpectrum>
  {
    public IdentifiedSpectrumPropertyConverterFactory()
    {
      _ignoreKey.Add(QuantificationConsts.DUPLICATED_SPECTRA_KEY);

      this.RegisterConverter(new IdentifiedSpectrumIdConverter<IIdentifiedSpectrum>(), "SpectrumId");

      this.RegisterConverter(new IdentifiedSpectrumEngineConverter<IIdentifiedSpectrum>());

      this.RegisterConverter(new IdentifiedSpectrumFileScanConverter<IIdentifiedSpectrum>(), "File, Scan(s)", "FileScan");

      this.RegisterConverter(new IdentifiedSpectrumFileScanConverterDtaselect<IIdentifiedSpectrum>(), "Query Spec");

      this.RegisterConverter(new IdentifiedSpectrumSequenceConverter<IIdentifiedSpectrum>());

      this.RegisterConverter(new IdentifiedSpectrumSequenceConverterDtaselect<IIdentifiedSpectrum>());

      this.RegisterConverter(new IdentifiedSpectrumObservedMzConverter<IIdentifiedSpectrum>(), "m/z");

      this.RegisterConverter(new IdentifiedSpectrumIsoelectricPointConverter<IIdentifiedSpectrum>());

      this.RegisterConverter(new IdentifiedSpectrumMatchedTICConverter<IIdentifiedSpectrum>());

      this.RegisterConverter(new IdentifiedSpectrumObservedMHConverter<IIdentifiedSpectrum>(), "M+H+");

      this.RegisterConverter(new IdentifiedSpectrumSiteProbabilityConverter<IIdentifiedSpectrum>());

      this.RegisterConverter(new IdentifiedSpectrumTheoreticalMHConverter<IIdentifiedSpectrum>(), "CalcM+H+", "MH", "TheoreticalMH");

      this.RegisterConverter(new IdentifiedSpectrumTheoreticalMinusExperimentalMassConverter<IIdentifiedSpectrum>());

      this.RegisterConverter(new IdentifiedSpectrumExperimentalMinusTheoreticalMassConverter<IIdentifiedSpectrum>());

      this.RegisterConverter(new IdentifiedSpectrumChargeConverter<IIdentifiedSpectrum>());

      this.RegisterConverter(new IdentifiedSpectrumRankConverter<IIdentifiedSpectrum>());

      this.RegisterConverter(new IdentifiedSpectrumScoreConverter<IIdentifiedSpectrum>(), "pMatch Score", "XCorr", "XC", "Amanda Score");

      this.RegisterConverter(new IdentifiedSpectrumProbabilityConverter<IIdentifiedSpectrum>(), "PValue");

      this.RegisterConverter(new IdentifiedSpectrumDeltaScoreConverter<IIdentifiedSpectrum>(), "DeltCN", "DeltaCn");

      this.RegisterConverter(new IdentifiedSpectrumSpScoreConverter<IIdentifiedSpectrum>(), "SvmScore");

      this.RegisterConverter(new IdentifiedSpectrumExpectValueConverter<IIdentifiedSpectrum>(), "SpScore", "Sp");

      this.RegisterConverter(new IdentifiedSpectrumSpRankConverter<IIdentifiedSpectrum>(), "SpRank");

      this.RegisterConverter(new IdentifiedSpectrumQueryConverter<IIdentifiedSpectrum>(), "QueryId");

      this.RegisterConverter(new IdentifiedSpectrumIonsConverter<IIdentifiedSpectrum>());

      this.RegisterConverter(new IdentifiedSpectrumReferenceConverter<IIdentifiedSpectrum>());

      this.RegisterConverter(new IdentifiedSpectrumMissCleavageConverter<IIdentifiedSpectrum>());

      this.RegisterConverter(new IdentifiedSpectrumModificationConverter<IIdentifiedSpectrum>(), "Modifications");

      this.RegisterConverter(new IdentifiedSpectrumIonProportionConverter<IIdentifiedSpectrum>());

      this.RegisterConverter(new IdentifiedSpectrumDiffModifiedCandidateConverter<IIdentifiedSpectrum>());

      this.RegisterConverter(new IdentifiedSpectrumTagConverter<IIdentifiedSpectrum>());

      this.RegisterConverter(new AnnotationConverter<IIdentifiedSpectrum>("Redundancy", "1"));

      this.RegisterConverter(new AnnotationConverter<IIdentifiedSpectrum>("TotalIntensity", "0.0"));

      this.RegisterConverter(new IdentifiedSpectrumMatchCountConverter<IIdentifiedSpectrum>());

      this.RegisterConverter(new IdentifiedSpectrumDecoyConverter<IIdentifiedSpectrum>(), "T/D Hit", "Target/Decoy");

      this.RegisterConverter(new IdentifiedSpectrumQValueConverter<IIdentifiedSpectrum>(), "FDR");

      this.RegisterConverter(new IdentifiedSpectrumNumProteaseTerminiConverter<IIdentifiedSpectrum>());

      this.RegisterConverter(new MaxQuantBestLocalizationRawFileConverter<IIdentifiedSpectrum>());
      this.RegisterConverter(new MaxQuantBestLocalizationScanNumberConverter<IIdentifiedSpectrum>());
      this.RegisterConverter(new MaxQuantReverseConverter<IIdentifiedSpectrum>());
      this.RegisterConverter(new MaxQuantContaminantConverter<IIdentifiedSpectrum>());
      this.RegisterConverter(new MaxQuantModifiedSequenceConverter<IIdentifiedSpectrum>());
      this.RegisterConverter(new MaxQuantPureSequenceConverter<IIdentifiedSpectrum>());
      this.RegisterConverter(new MaxQuantItemListRatioConverter<IIdentifiedSpectrum>());

      this.RegisterConverter(new QuantificationItemRatioConverter<IIdentifiedSpectrum>());

      this.RegisterConverter(new ITraqItemPlexConverter<IIdentifiedSpectrum>());
      this.RegisterConverter(new ITraqItemI113Converter<IIdentifiedSpectrum>());
      this.RegisterConverter(new ITraqItemI114Converter<IIdentifiedSpectrum>());
      this.RegisterConverter(new TMTPlex6I126Converter<IIdentifiedSpectrum>());

      this.RegisterConverter(new IdentifiedSpectrumTheoreticalMinusExperimentalMassPPMConverter<IIdentifiedSpectrum>());

      this.RegisterConverter(new IdentifiedSpectrumRetentionTimeConverter<IIdentifiedSpectrum>());
    }

    private static IdentifiedSpectrumPropertyConverterFactory factory;
    public static IdentifiedSpectrumPropertyConverterFactory GetInstance()
    {
      if (factory == null)
      {
        factory = new IdentifiedSpectrumPropertyConverterFactory();
      }
      return factory;
    }

    public override IIdentifiedSpectrum Allocate()
    {
      return new IdentifiedSpectrum();
    }
  }
}