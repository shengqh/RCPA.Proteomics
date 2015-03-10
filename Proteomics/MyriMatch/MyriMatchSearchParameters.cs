using RCPA.Proteomics.MSGF;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RCPA.Proteomics.MyriMatch
{
  public enum MyriMatchEnzyme { Unknown, Trypsin, Trypsin_P, Chymotrypsin, TrypChymo, Lys__C, Lys__C_P, Asp__N, PepsinA, CNBr };

  public class MyriMatchSearchParameters : DatasetOption
  {
    private static Dictionary<MSGFEnzyme, MyriMatchEnzyme> enzymeMap = new Dictionary<MSGFEnzyme, MyriMatchEnzyme>();
    static MyriMatchSearchParameters()
    {
      enzymeMap[MSGFEnzyme.Trypsin] = MyriMatchEnzyme.Trypsin_P;
      enzymeMap[MSGFEnzyme.Chymotrypsin] = MyriMatchEnzyme.Chymotrypsin;
      enzymeMap[MSGFEnzyme.LysC] = MyriMatchEnzyme.Lys__C;
      enzymeMap[MSGFEnzyme.AspN] = MyriMatchEnzyme.Asp__N;
    }

    public MyriMatchSearchParameters()
    {
      NumChargeStates = 3;
      OutputFormat = "pepXML";
      OutputSuffix = ".myrimatch";
      TicCutoffPercentage = 0.95;
      MaxPeakCount = 100;
      MaxResultRank = 2;
      MinPeptideMass = 600;
      MaxPeptideMass = 5000;
      MinPeptideLength = 5;
      MaxPeptideLength = 50;
      UseSmartPlusThreeModel = false;
      ComputeXCorr = false;
      UseMultipleProcessors = true;
      ResultsPerBatch = 5000;

      CleavageRules = MyriMatchEnzyme.Unknown;
      SpectrumListFilters = "";
      FragmentationAutoRule = true;
      PrecursorMzToleranceRule = "mono";
      MonoisotopeAdjustmentSet = new int[] { 0, 1 };
    }

    /// <summary>
    /// Controls the number of charge states that MyriMatch will handle during all stages of the program. It is especially 
    /// important during determination of charge state (see DuplicateSpectra for more information).
    /// </summary>
    public int NumChargeStates { get; set; }

    /// <summary>
    /// MyriMatch can write identifications in either “mzIdentML” or “pepXML” format.
    /// SHENGQH: pepXML recommended since mzIdentML result is not compatible with MGF input format. The scan information will be lost.
    /// </summary>
    public string OutputFormat { get; set; }

    /// <summary>
    /// The output of a MyriMatch job will be an identification file for each input file. The string specified by this parameter 
    /// will be appended to each output filename. It is useful for differentiating jobs within a single directory.
    /// </summary>
    public string OutputSuffix { get; set; }

    /// <summary>
    /// In order to maximize the effectiveness of the MVH scoring algorithm, an important step in preprocessing the experimental 
    /// spectra is filtering out noise peaks. Noise peaks are filtered out by sorting the original peaks in descending order of 
    /// intensity, and then picking peaks from that list until the cumulative ion current of the picked peaks divided by the total 
    /// ion current (TIC) is greater than or equal to this parameter. Lower percentages mean that less of the spectrums total 
    /// intensity will be allowed to pass through preprocessing. See the section on Advanced Usage for tips on how to use this parameter optimally.
    /// </summary>
    public double TicCutoffPercentage { get; set; }

    /// <summary>
    /// Filters out all peaks except the MaxPeakCount most intense peaks.
    /// </summary>
    public int MaxPeakCount { get; set; }

    /// <summary>
    /// This parameter sets the maximum rank of peptide-spectrum-matches to report for each spectrum. A rank is all PSMs that score 
    /// the same (common for isobaric residues and ambiguous modification localization). MyriMatch may report extra ranks in order 
    /// to ensure that the top target match and top decoy match from each digestion specificity (full, semi, non) is reported.
    /// </summary>
    public int MaxResultRank { get; set; }

    /// <summary>
    /// This important parameter allows the user to control the way peptides are generated from the protein database. It can be used 
    /// to configure the search on tryptic peptides only, on non-tryptics, or anything in between. It can even be used to test multiple 
    /// residue motifs at a potential cleavage site. This parameter describes which amino acids are valid on the N and C termini of a 
    /// digestion site. The parameter is specified in PSI-MS regular expression syntax (a limited Perl regular expression syntax). 
    /// MyriMatch can recognize the following protease names and automatically use the corresponding regular expression for this parameter.
    /// 
    /// Protease names:
    /// -       “Trypsin” (allows for cut after K or R)
    /// -       “Trypsin/P” (normal trypsin cut, disallows cutting when the site is before a proline)
    /// -       "Chymotrypsin” (allows cut after F,Y,W,L. Disallows cutting before proline)
    /// -       "TrypChymo” (combines “Trypsin/P” and “Chymotrypsin” cleavage rules)
    /// -       “Lys-C”
    /// -       “Lys-C/P” (Lys-C, disallowing cutting before proline)
    /// -       “Asp-N”
    /// -       “PepsinA” (Cuts right after F, L)
    /// -       “CNBr” (Cyanogen bromide)
    /// -       “Formic_acid” (Formic acid)
    /// -       “NoEnzyme” (not supported; use the proper enzyme and set MinTerminiCleavages to 0)
    /// </summary>
    public MyriMatchEnzyme CleavageRules { get; set; }

    /// <summary>
    /// When preprocessing the experimental spectra, any spectrum with a precursor mass that is less than the specified mass will be disqualified. 
    /// This parameter is useful to eliminate inherently unidentifiable spectra from an input data set. A setting of 500 for example, will eliminate 
    /// most 3-residue matches and clean up the output file quite a lot.
    /// </summary>
    public double MinPeptideMass { get; set; }

    /// <summary>
    /// When preprocessing the experimental spectra, any spectrum with a precursor mass that exceeds the specified mass will be disqualified.
    /// </summary>
    public double MaxPeptideMass { get; set; }

    /// <summary>
    /// When digesting proteins, any peptide which does not meet or exceed the specified length will be disqualified.
    /// </summary>
    public int MinPeptideLength { get; set; }

    /// <summary>
    /// When digesting proteins, any peptide which exceeds this specified length will be disqualified.
    /// </summary>
    public int MaxPeptideLength { get; set; }

    /// <summary>
    /// Once a candidate sequence has been generated from the protein database, MyriMatch determines which spectra will be compared to the sequence. 
    /// For each unique charge state of those spectra, a set of theoretical fragment ions is generated by one of several different algorithms.
    /// 
    /// For +1 and +2 precursors, a +1 b and y ion is always predicted at each peptide bond.
    /// 
    /// For +3 and higher precursors, the fragment ions predicted depend on the way this parameter is set. When this parameter is true, 
    /// then for each peptide bond, an internal calculation is done to estimate the basicity of the b and y fragment sequence. The precursors 
    /// protons are distributed to those ions based on that calculation, with the more basic sequence generally getting more of the protons. 
    /// For example, when this parameter is true, each peptide bond of a +3 precursor will either generate a +2 bi and a +1 yi ion, or a +1 bi 
    /// and a +2 yi ion. For a +4 precursor, depending on basicity, a peptide bond breakage may result in a +1 bi and a +3 yi ion, a +2 bi and 
    /// a +2 yi ion, or a +3 bi and a +1 yi ion. When this parameter is false, however, ALL possible charge distributions for the fragment ions 
    /// are generated for every peptide bond. So a +3 sequence of length 10 will always have theoretical +1 y5, +2 y5, +1 b5, and +2 b5 ions.
    /// </summary>
    public bool UseSmartPlusThreeModel { get; set; }

    /// <summary>
    /// If true, a Sequest-like cross correlation (xcorr) score will be calculated for the top ranking hits in each spectrum’s result set.
    /// </summary>
    public bool ComputeXCorr { get; set; }

    /// <summary>
    /// MyriMatch is designed to take advantage of (symmetric) multiprocessor systems by multithreading the database search. 
    /// A search process on an SMP system will spawn one worker thread for each processing unit (where a processing unit can 
    /// be either a core on a multi-core CPU or a separate CPU entirely). The main thread then generates a list of worker numbers
    /// which is equal to the number of worker threads multiplied by this parameter. The worker threads then take a worker number
    /// from the list and use that number to iterate through the protein list. It is possible that one thread will be assigned all 
    /// the proteins that generate a few candidates while another thread is assigned all the proteins that generate many candidates, 
    /// resulting in one thread finishing its searching early. By having each thread use multiple worker numbers, the chance of one
    /// thread being penalized for picking all the easy proteins is reduced because if it finishes early it can just pick a new number. 
    /// The only disadvantage to this system is that picking the new number incurs some overhead because of synchronizing with the
    /// other worker threads that might be trying to pick a worker number at the same time. The default value is a nice compromise 
    /// between incurring that overhead and minimizing wasted time.
    /// </summary>
    public bool UseMultipleProcessors { get; set; }

    /// <summary>
    /// This parameter sets a number of batches per node to strive for when using the MPI-based parallelization features.
    /// Setting this too low means that some nodes will finish before others (idle processor time), while setting it too high 
    /// means more overhead in network transmission as each batch is smaller.
    /// </summary>
    public int ResultsPerBatch { get; set; }

    /// <summary>
    /// A semicolon-delimited list of filters applied to spectra as it is read in. Supported filters are defined by ProteoWizard:
    /// http://forge.fenchurch.mc.vanderbilt.edu/scm/viewvc.php/*checkout*/trunk/doc/index.html?root=myrimatch
    /// </summary>
    public string SpectrumListFilters { get; set; }

    /// <summary>
    /// This parameter determines which ion series are used to build the theoretical spectrum for each candidate peptide. Possible values are:
    /// CID: b, y
    /// ETD: c, z*
    /// manual: user-defined (a comma-separated list of [abcxyz] or z* (z+1), e.g. manual:b,y,z
    /// </summary>
    public string FragmentationRule { get; set; }

    /// <summary>
    /// If true, MyriMatch will automatically choose the fragmentation rule based on the activation type of each MSn spectrum. 
    /// This allows a single search to handle CID and ETD spectra (i.e. an interleaved or decision tree run). If false or if 
    /// the input format does not specify the input format then FragmentationRule is used (see above).
    /// </summary>
    public bool FragmentationAutoRule { get; set; }

    /// <summary>
    /// This parameter controls the automatic selection of precursor mass type. For data from Thermo instruments, 
    /// using the “auto” setting on a RAW, mzML, or mz5 file will automatically choose monoisotopic or average mass 
    /// values (and the corresponding precursor tolerance). For other instruments or older data formats, the “mono” 
    /// or “avg” tolerance should be set explicitly.
    /// </summary>
    public string PrecursorMzToleranceRule { get; set; }

    /// <summary>
    /// Sometimes a mass spectrometer will pick the wrong isotope as the monoisotope of an eluting peptide. 
    /// When using narrow tolerances for monoisotopic precursors, this can cause identifiable spectra to be 
    /// missed. This parameter defines a set of isotopes (0 being the instrument-called monoisotope) to try 
    /// as the monoisotopic precursor m/z. To disable this technique, set the value to “0”.
    /// </summary>
    public int[] MonoisotopeAdjustmentSet { get; set; }

    public void WriteToFile(string fileName)
    {
      using (var sw = new StreamWriter(fileName, false, Encoding.ASCII))
      {
        sw.NewLine = Environment.NewLine;
        sw.WriteLine(@"ProteinDatabase= ""{0}""", this.Database.Replace("\\", "/"));

        sw.WriteLine(@"StaticMods = ""{0}""", this.StaticModification);
        sw.WriteLine(@"DynamicMods = ""{0}""", this.DynamicModification);
        sw.WriteLine(@"MinTerminiCleavages = {0}", this.MinimumTerminiCleavages);
        sw.WriteLine(@"CleavageRules = ""{0}""", this.GetCleavageRules());
        sw.WriteLine(@"MaxMissedCleavages = {0}", this.MaximumMissedCleavages);
        sw.WriteLine(@"MaxDynamicMods = {0}", this.MaximumDynamicModifications);

        sw.WriteLine(@"DecoyPrefix = ""{0}""", this.DecoyPrefix);
        sw.WriteLine(@"NumChargeStates = {0}", this.NumChargeStates);
        sw.WriteLine(@"OutputFormat= ""{0}""", this.OutputFormat);
        sw.WriteLine(@"OutputSuffix= ""{0}""", this.OutputSuffix);
        sw.WriteLine(@"TicCutoffPercentage = {0}", this.TicCutoffPercentage);
        sw.WriteLine(@"MaxPeakCount = {0}", this.MaxPeakCount);
        sw.WriteLine(@"MaxResultRank = {0}", this.MaxResultRank);
        sw.WriteLine(@"MinPeptideMass = {0} Da", this.MinPeptideMass);
        sw.WriteLine(@"MaxPeptideMass = {0} Da", this.MaxPeptideMass);
        sw.WriteLine(@"MinPeptideLength = {0}", this.MinPeptideLength);
        sw.WriteLine(@"MaxPeptideLength = {0}", this.MaxPeptideLength);
        sw.WriteLine(@"UseSmartPlusThreeModel = {0}", this.UseSmartPlusThreeModel);
        sw.WriteLine(@"ComputeXCorr = {0}", this.ComputeXCorr);
        sw.WriteLine(@"UseMultipleProcessors = {0}", this.UseMultipleProcessors);
        sw.WriteLine(@"ResultsPerBatch = {0}", this.ResultsPerBatch);

        sw.WriteLine(@"SpectrumListFilters = """"", this.SpectrumListFilters);
        sw.WriteLine(@"FragmentationAutoRule = {0}", this.FragmentationAutoRule);
        sw.WriteLine(@"PrecursorMzToleranceRule = {0}", this.PrecursorMzToleranceRule);
        sw.WriteLine(@"AvgPrecursorMzTolerance = {0}", this.GetAvgPrecursorMzTolerance());
        sw.WriteLine(@"MonoPrecursorMzTolerance = {0}", this.GetMonoPrecursorMzTolerance());
        sw.WriteLine(@"MonoisotopeAdjustmentSet = {0}", this.GetMonoisotopeAdjustmentSet());
        sw.WriteLine(@"FragmentMzTolerance = {0}", this.GetFragmentMzTolerance());
      }
    }

    private string GetCleavageRules()
    {
      var cr = MyriMatchEnzyme.Unknown;
      if (CleavageRules != MyriMatchEnzyme.Unknown)
      {
        cr = CleavageRules;
      }
      else if (enzymeMap.ContainsKey(EnzymeId))
      {
        cr = enzymeMap[EnzymeId];
      }
      else
      {
        throw new Exception("Set MyriMatch CleavageRules first!");
      }

      return CleavageRules.ToString().Replace("__", "-").Replace("_", "/");
    }

    private string GetMonoisotopeAdjustmentSet()
    {
      return "[" + (from m in MonoisotopeAdjustmentSet select m.ToString()).Merge(",") + "]";
    }

    private string GetMonoPrecursorMzTolerance()
    {
      if (this.PrecursorTolerancePPM)
      {
        return string.Format("{0} ppm", this.PrecursorTolerance);
      }
      else
      {
        return string.Format("{0} mz", this.PrecursorTolerance);
      }
    }

    private string GetAvgPrecursorMzTolerance()
    {
      return GetMonoPrecursorMzTolerance();
    }

    private object GetFragmentMzTolerance()
    {
      if (this.FragmentTolerancePPM)
      {
        return string.Format("{0} ppm", this.FragmentTolerance);
      }
      else
      {
        return string.Format("{0} mz", this.FragmentTolerance);
      }
    }
  }
}
