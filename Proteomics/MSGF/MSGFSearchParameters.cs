using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RCPA.Proteomics.MSGF
{
  public enum MSGFFragmentMethod { InSpectrumOrCID, CID, ETD, HCD };
  public enum MSGFMS2Detector { LowResolution, OrbitrapFTICR, TOF, QExactive };
  public enum MSGFEnzyme { Unspecific, Trypsin, Chymotrypsin, LysC, LysN, GluC, ArgC, AspN, AlphaLP, NoCleavage };
  public enum MSGFProtocol { Automatic, Phosphorylation, iTRAQ, iTRAQPhospho, TMT, Standard };

  public class MSGFSearchParameters
  {
    public MSGFSearchParameters()
    {
      NumberOfTolerableTermini = 2;
      Enzyme = MSGFEnzyme.Trypsin;
    }

    /// <summary>
    /// *.mzML, *.mzXML, *.mgf, *.ms2, *.pkl or *_dta.txt
    /// </summary>
    public string SpectrumFile { get; set; }

    /// <summary>
    /// *.fasta or *.fa
    /// </summary>
    public string DatabaseFile { get; set; }

    /// <summary>
    /// Default: [SpectrumFileName].mzid
    /// </summary>
    public string OutputFile { get; set; }

    /// <summary>
    /// e.g. 2.5Da, 20ppm or 0.5Da,2.5Da, Default:20ppm
    /// Use comma to set asymmetric values. E.g. "-t 0.5Da,2.5Da" will set 0.5Da to the minus (expMass<theoMass) and 2.5Da to plus (expMass>theoMass)
    /// </summary>
    public string PrecursorMassTolerance { get; set; }

    /// <summary>
    /// Range of allowed isotope peak errors, Default:0,1
    /// Takes into account of the error introduced by chooosing a non-monoisotopic peak for fragmentation.
    /// The combination of -t and -ti determins the precursor mass tolerance.
    /// E.g. "-t 20ppm -ti -1,2" tests abs(exp-calc-n*1.00335Da)<20ppm for n=-1, 0, 1, 2.
    /// </summary>
    public string IsotopeErrorRange { get; set; }

    /// <summary>
    /// Number of concurrent threads to be executed, Default: Number of available cores
    /// </summary>
    public int NumThreads { get; set; }

    /// <summary>
    /// 0: don't search decoy database (Default), 1: search decoy database
    /// </summary>
    public int SearchDecoyDatabase { get; set; }

    /// <summary>
    /// 0: As written in the spectrum or CID if no info (Default), 1: CID, 2: ETD, 3: HCD
    /// </summary>
    public MSGFFragmentMethod FragmentMethod { get; set; }

    /// <summary>
    /// 0: Low-res LCQ/LTQ (Default), 1: Orbitrap/FTICR,2: TOF, 3: Q-Exactive
    /// </summary>
    public MSGFMS2Detector MS2Detector { get; set; }

    /// <summary>
    /// 0: unspecific cleavage, 1: Trypsin (Default), 2: Chymotrypsin, 3: Lys-C, 4: Lys-N, 5: glutamyl endopeptidase, 6: Arg-C, 7: Asp-N, 8: alphaLP, 9: no cleavage
    /// </summary>
    public MSGFEnzyme Enzyme { get; set; }

    /// <summary>
    ///  0: Automatic (Default), 1: Phosphorylation, 2: iTRAQ, 3: iTRAQPhospho, 4: TMT, 5: Standard
    /// </summary>
    public MSGFProtocol Protocol { get; set; }

    /// <summary>
    /// Number of Tolerable Termini, 0/1/2 Default: 2
    /// E.g. For trypsin, 0: non-tryptic, 1: semi-tryptic, 2: fully-tryptic peptides only.
    /// </summary>
    public int NumberOfTolerableTermini { get; set; }

    /// <summary>
    /// Modification file, Default: standard aminoacids with fixed C+57
    /// Example:
    /// 
    /// #max number of modifications per peptide
    /// NumMods=2
    /// 
    /// #fixed modifications
    /// 57.021464,C,fix,any,Carbamidomethyl
    /// 
    /// #variable modifications
    /// 15.994915,M,opt,any,Oxidation
    /// 42.010565,*,opt,Prot-N-term,Acetyl
    /// </summary>
    public string ModificationFileName { get; set; }

    /// <summary>
    /// Minimum peptide length to consider, Default:6
    /// </summary>
    public int MinPepLength { get; set; }

    /// <summary>
    /// Maximum peptide length to consider, Default:40
    /// </summary>
    public int MaxPepLength { get; set; }

    /// <summary>
    /// Minimum precursor charge to consider if charges are not specified in the spectrum file, Default: 2
    /// </summary>
    public int MinCharge { get; set; }

    /// <summary>
    /// Maximum precursor charge to consider if charges are not specified in the spectrum file, Default: 3
    /// </summary>
    public int MaxCharge { get; set; }

    /// <summary>
    /// Number of matches per spectrum to be reported, Default: 1
    /// </summary>
    public int NumMatchesPerSpec { get; set; }

    /// <summary>
    /// 0/1, 0: output basic scores only (Default), 1: output additional features
    /// </summary>
    public int AddFeatures { get; set; }

    public string GetParameters()
    {
      var sb = new StringBuilder();

      sb.AppendFormat(" -s \"{0}\"", SpectrumFile);

      sb.AppendFormat(" -d \"{0}\"", DatabaseFile);

      if (!string.IsNullOrEmpty(OutputFile))
      {
        sb.AppendFormat(" -o \"{0}\"", OutputFile);
      }

      if (!string.IsNullOrEmpty(PrecursorMassTolerance))
      {
        sb.AppendFormat(" -t {0}", PrecursorMassTolerance);
      }

      if (!string.IsNullOrEmpty(IsotopeErrorRange))
      {
        sb.AppendFormat(" -ti \"{0}\"", IsotopeErrorRange);
      }

      if (NumThreads > 0)
      {
        sb.AppendFormat(" -thread {0}", NumThreads);
      }

      sb.AppendFormat(" -tda {0}", SearchDecoyDatabase);

      sb.AppendFormat(" -m {0}", Convert.ToInt32(FragmentMethod));

      sb.AppendFormat(" -inst {0}", Convert.ToInt32(MS2Detector));

      sb.AppendFormat(" -e {0}", Convert.ToInt32(Enzyme));

      sb.AppendFormat(" -protocol {0}", Convert.ToInt32(Protocol));

      sb.AppendFormat(" -ntt {0}", NumberOfTolerableTermini);

      if (!string.IsNullOrEmpty(ModificationFileName))
      {
        sb.AppendFormat(" -mod \"{0}\"", ModificationFileName);
      }

      if (MinPepLength > 0)
      {
        sb.AppendFormat(" -minLength {0}", MinPepLength);
      }

      if (MaxPepLength > 0)
      {
        sb.AppendFormat(" -maxLength {0}", MaxPepLength);
      }

      if (MinCharge > 0)
      {
        sb.AppendFormat(" -minCharge {0}", MinCharge);
      }

      if (MaxCharge > 0)
      {
        sb.AppendFormat(" -maxCharge {0}", MaxCharge);
      }

      if (NumMatchesPerSpec > 0)
      {
        sb.AppendFormat(" -n {0}", NumMatchesPerSpec);
      }

      sb.AppendFormat(" -addFeatures {0}", AddFeatures);

      return sb.ToString();
    }
  }
}
