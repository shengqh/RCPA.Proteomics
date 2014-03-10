using System.Collections.Generic;
using System.IO;
using RCPA.Proteomics.Isotopic;
using RCPA.Proteomics.Spectrum;

namespace RCPA.Proteomics.Quantification.O18
{
  public class O18QuantificationPeptideProcessor : AbstractThreadFileProcessor
  {
    private readonly Aminoacids _aas = new Aminoacids();
    private readonly string _additionalFormula;
    private readonly AtomComposition _atomComposition;
    private readonly IIsotopicProfileBuilder2 _cp = new EmassProfileBuilder();
    private readonly List<O18QuanEnvelope> _envelopes;
    private readonly IFileFormat<O18QuantificationSummaryItem> _fileFormat;

    private readonly bool _isPostDigestionLabelling;
    private readonly string _peptide;

    private readonly List<Peak> _profile;
    private readonly double _purityOfO18Water;

    private readonly string _rawFilename;

    private readonly double _scanPercentageEnd;
    private readonly double _scanPercentageStart;

    public O18QuantificationPeptideProcessor(IFileFormat<O18QuantificationSummaryItem> fileFormat,
      bool isPostDigestionLabelling, string rawFilename, string peptide,
      double purityOfO18Water, List<O18QuanEnvelope> envelopes, double mzTolerance,
      string additionalFormula, double scanPercentageStart, double scanPercentageEnd)
    {
      _fileFormat = fileFormat;
      _isPostDigestionLabelling = isPostDigestionLabelling;
      _rawFilename = new FileInfo(rawFilename).FullName;
      _peptide = peptide;
      _additionalFormula = additionalFormula;

      _atomComposition = _aas.GetPeptideAtomComposition(peptide);
      if (!string.IsNullOrEmpty(additionalFormula))
      {
        var additionalAtomComposition = new AtomComposition(additionalFormula);
        _atomComposition.Add(additionalAtomComposition);
      }
      _profile = _cp.GetProfile(_atomComposition, 0, 10);
      _purityOfO18Water = purityOfO18Water;
      _envelopes = envelopes;

      _scanPercentageStart = scanPercentageStart;
      _scanPercentageEnd = scanPercentageEnd;
    }

    public double TheoreticalMz { get; set; }

    public int Charge { get; set; }

    public string SoftwareVersion { get; set; }

    protected double PurityOfO18Water
    {
      get { return _purityOfO18Water; }
    }

    public override IEnumerable<string> Process(string fileName)
    {
      var summary = new O18QuantificationSummaryItem
      {
        RawFilename = _rawFilename,
        SoftwareVersion = SoftwareVersion,
        PeptideSequence = _peptide,
        AdditionalFormula = _additionalFormula,
        PeptideAtomComposition = _atomComposition.GetFormula(),
        PurityOfO18Water = _purityOfO18Water,
        PeptideProfile = _profile,
        ObservedEnvelopes = _envelopes,
        IsPostDigestionLabelling = _isPostDigestionLabelling,
        TheoreticalO16Mz = TheoreticalMz,
        Charge = Charge,
        ScanStartPercentage = _scanPercentageStart,
        ScanEndPercentage = _scanPercentageEnd
      };

      summary.InitializeScanRange();
      summary.CalculateSpeciesAbundanceByLinearRegression();

      _fileFormat.WriteToFile(fileName, summary);

      return new[] { fileName };
    }
  }
}