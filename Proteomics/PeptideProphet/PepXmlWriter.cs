using System;
using System.Collections.Generic;
using System.IO;
using RCPA.Proteomics.Sequest;
using RCPA.Proteomics.Summary;

namespace RCPA.Proteomics.PeptideProphet
{
  public class PepXmlWriter : IDisposable
  {
    private readonly Aminoacids aas = new Aminoacids();
    private readonly string rawDataType;
    private SequestParam currentSequestParams;
    private bool isFileOpened;
    private bool isMsmsRunSummaryOpened;

    private string rawData;

    private Protease sampleProtease;

    private int searchId;
    private string sourceFilename;

    private int spectrumId;

    private Dictionary<char, double> staticAminoacidModification;
    private StreamWriter sw;

    public PepXmlWriter(string rawDataType)
    {
      this.rawDataType = rawDataType;
    }

    public Protease SampleProtease
    {
      get { return this.sampleProtease; }
      set { this.sampleProtease = value; }
    }

    public void Open(string resultFilename)
    {
      var resultFile = new FileInfo(resultFilename);

      this.sw = new StreamWriter(resultFilename);
      this.sw.WriteLine("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
      this.sw.WriteLine("<?xml-stylesheet type=\"text/xsl\" href=\"http://localhost:1441/pepXML_std.xsl\"?>");

      DateTime dt = DateTime.Now;
      this.sw.WriteLine(
        "<msms_pipeline_analysis date=\"{0}\" xmlns=\"http://regis-web.systemsbiology.net/pepXML\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xsi:schemaLocation=\"http://sashimi.sourceforge.net/schema_revision/pepXML/pepXML_v18.xsd\" summary_xml=\"{1}\">",
        dt.ToString("yyyy-MM-dd\\THH:mm:ss"),
        resultFile.Name);

      this.searchId = 0;
      this.spectrumId = 0;

      this.isFileOpened = true;
    }

    public void OpenMsmsRunSummary(string sourceFilename)
    {
      if (this.isMsmsRunSummaryOpened)
      {
        CloseMsmsRunSummary();
      }

      var sourceFile = new FileInfo(sourceFilename);
      this.sourceFilename = sourceFile.FullName.Replace('\\', '/');

      this.rawData = sourceFile.Extension;

      this.sw.WriteLine(
        "  <msms_run_summary base_name=\"{0}\" msManufacturer=\"ThermoFinnigan\" msModel=\"LTQ Orbitrap\" msIonization=\"ESI\" msMassAnalyzer=\"Ion Trap\" msDetector=\"EMT\" raw_data_type=\"raw\" raw_data=\"{1}\">",
        this.sourceFilename,
        this.rawData);

      this.searchId++;
      this.isMsmsRunSummaryOpened = true;
    }

    public void CloseMsmsRunSummary()
    {
      if (this.isMsmsRunSummaryOpened)
      {
        this.sw.WriteLine("  </msms_run_summary>");
        this.isMsmsRunSummaryOpened = false;
      }
    }

    public void WriteSequestParam(SequestParam sp)
    {
      if (!this.isMsmsRunSummaryOpened)
      {
        throw new Exception("Call OpenMsmsRunSummary before WriteSequestParam.");
      }

      if (this.sampleProtease == null)
      {
        this.sampleProtease = sp.Protease;
      }

      this.currentSequestParams = sp;
      this.staticAminoacidModification = sp.GetStaticAminoacidModification();

      //sample_enzyme
      this.sw.WriteLine("    <sample_enzyme name=\"{0}\">",
                        this.sampleProtease.Name);
      this.sw.WriteLine("      <specificity cut=\"{0}\" no_cut=\"{1}\" sense=\"{2}\"/>",
                        this.sampleProtease.CleaveageResidues,
                        this.sampleProtease.NotCleaveResidues,
                        this.sampleProtease.IsEndoProtease ? "C" : "N");
      this.sw.WriteLine("    </sample_enzyme>");

      //search_summary
      this.sw.WriteLine(
        "    <search_summary base_name=\"{0}\" search_engine=\"SEQUEST\" precursor_mass_type=\"{1}\" fragment_mass_type=\"{2}\" out_data_type=\"{3}\" out_data=\"{4}\" search_id=\"{5}\">",
        this.sourceFilename,
        sp.Mass_type_parent_annotation,
        sp.Mass_type_fragment_annotation,
        this.rawDataType,
        this.rawData,
        this.searchId);

      this.sw.WriteLine("      <search_database local_path=\"{0}\" type=\"{1}\"/>",
                        sp.First_database_name,
                        sp.Nucleotide_reading_frame == 0 ? "AA" : "NA");

      this.sw.WriteLine(
        "      <enzymatic_search_constraint enzyme=\"{0}\" max_num_internal_cleavages=\"{1}\" min_number_termini=\"{2}\"/>",
        sp.Protease.Name,
        sp.Max_num_internal_cleavage_sites,
        sp.Min_number_termini);

      WriteModifications(sp);

      //parameters
      this.sw.WriteLine("      <parameter name=\"peptide_mass_tol\" value=\"{0:0.0000}\"/>", sp.Peptide_mass_tolerance);
      this.sw.WriteLine("      <parameter name=\"ion_series\" value=\"{0}\"/>", sp.Ion_series);
      this.sw.WriteLine("      <parameter name=\"fragment_ion_tol\" value=\"{0:0.0000}\"/>", sp.Fragment_ion_tolerance);
      this.sw.WriteLine("      <parameter name=\"num_output_lines\" value=\"{0}\"/>", sp.Num_output_lines);
      this.sw.WriteLine("      <parameter name=\"num_results\" value=\"{0}\"/>", sp.Num_results);
      this.sw.WriteLine("      <parameter name=\"num_description_lines\" value=\"{0}\"/>", sp.Num_description_lines);
      this.sw.WriteLine("      <parameter name=\"show_fragment_ions\" value=\"{0}\"/>", sp.Show_fragment_ions);
      this.sw.WriteLine("      <parameter name=\"print_duplicate_references\" value=\"{0}\"/>",
                        sp.Print_duplicate_references);
      this.sw.WriteLine("      <parameter name=\"max_num_differential_AA_per_mod\" value=\"{0}\"/>",
                        sp.Max_num_differential_AA_per_mod);
      this.sw.WriteLine("      <parameter name=\"nucleotide_reading_frame\" value=\"{0}\"/>",
                        sp.Nucleotide_reading_frame);
      this.sw.WriteLine("      <parameter name=\"normalize_Score\" value=\"{0}\"/>", sp.Normalize_Score);
      this.sw.WriteLine("      <parameter name=\"remove_precursor_peak\" value=\"{0}\"/>", sp.Remove_precursor_peak);
      this.sw.WriteLine("      <parameter name=\"ion_cutoff_percentage\" value=\"{0:0.0000}\"/>",
                        sp.Ion_cutoff_percentage);
      this.sw.WriteLine("      <parameter name=\"max_num_internal_cleavage_sites\" value=\"{0}\"/>",
                        sp.Max_num_internal_cleavage_sites);
      this.sw.WriteLine("      <parameter name=\"protein_mass_filter\" value=\"{0} {1}\"/>",
                        sp.Protein_mass_filter.First, sp.Protein_mass_filter.Second);
      this.sw.WriteLine("      <parameter name=\"match_peak_count\" value=\"{0}\"/>", sp.Match_peak_count);
      this.sw.WriteLine("      <parameter name=\"match_peak_allowed_error\" value=\"{0}\"/>",
                        sp.Match_peak_allowed_error);
      this.sw.WriteLine("      <parameter name=\"match_peak_tolerance\" value=\"{0}\"/>", sp.Match_peak_tolerance);
      this.sw.WriteLine("      <parameter name=\"partial_sequence\" value=\"{0}\"/>", sp.Partial_sequence);
      this.sw.WriteLine("      <parameter name=\"sequence_header_filter\" value=\"{0}\"/>", sp.Sequence_header_filter);

      //end of search_summary
      this.sw.WriteLine("    </search_summary>");
    }

    public void Close()
    {
      if (!this.isFileOpened)
      {
        return;
      }

      CloseMsmsRunSummary();

      this.sw.WriteLine("</msms_pipeline_analysis>");
      this.sw.Close();
      this.isFileOpened = false;
    }

    public void WriteSequestPeptideHit(IIdentifiedSpectrum sph)
    {
      if (!this.isMsmsRunSummaryOpened)
      {
        throw new Exception("Call OpenMsmsRunSummary before WriteSequestPeptideHit.");
      }

      this.spectrumId++;

      this.sw.WriteLine(
        "    <spectrum_query spectrum=\"{0}.{1}.{2}.{4}\" start_scan=\"{1}\" end_scan=\"{2}\" precursor_neutral_mass=\"{3:0.0000}\" assumed_charge=\"{4}\" index=\"{5}\">",
        sph.Query.FileScan.Experimental,
        sph.Query.FileScan.FirstScan,
        sph.Query.FileScan.LastScan,
        sph.ExperimentalMH - Atom.H.AverageMass,
        sph.Query.Charge,
        this.spectrumId);

      this.sw.WriteLine("      <search_result>");

      double massDiff = sph.TheoreticalMinusExperimentalMass;

      //ignore some miss cleavage sites
      string pureSeq = sph.Peptides[0].PureSequence;
      if (this.sampleProtease.IsEndoProtease)
      {
        if ('-' != sph.Peptides[0].Sequence[0] && -1 != this.sampleProtease.CleaveageResidues.IndexOf(pureSeq[0]))
        {
          pureSeq = pureSeq.Substring(1);
        }
        if (-1 != this.sampleProtease.CleaveageResidues.IndexOf(pureSeq[pureSeq.Length - 1]) &&
            -1 != this.sampleProtease.CleaveageResidues.IndexOf(pureSeq[pureSeq.Length - 2]))
        {
          pureSeq = pureSeq.Substring(0, pureSeq.Length - 1);
        }
      }
      else
      {
        if ('-' != sph.Peptides[0].Sequence[sph.Peptides[0].Sequence.Length - 1] &&
            -1 != this.sampleProtease.CleaveageResidues.IndexOf(pureSeq[pureSeq.Length - 1]))
        {
          pureSeq = pureSeq.Substring(0, pureSeq.Length - 1);
        }
        if (-1 != this.sampleProtease.CleaveageResidues.IndexOf(pureSeq[0]) &&
            -1 != this.sampleProtease.CleaveageResidues.IndexOf(pureSeq[1]))
        {
          pureSeq = pureSeq.Substring(1);
        }
      }

      this.sw.WriteLine(
        "        <search_hit hit_rank=\"1\" peptide=\"{0}\" peptide_prev_aa=\"{1}\" peptide_next_aa=\"{2}\" protein=\"{3}\" num_tot_proteins=\"{4}\" num_matched_ions=\"{5}\" tot_num_ions=\"{6}\" calc_neutral_pep_mass=\"{7:0.0000}\" massdiff=\"{8}{9:0.0000}00\" num_tol_term=\"{10}\" num_missed_cleavages=\"{11}\" is_rejected=\"0\">",
        sph.Peptides[0].PureSequence,
        sph.Peptides[0].Sequence[0],
        sph.Peptides[0].Sequence[sph.Peptides[0].Sequence.Length - 1],
        sph.Peptides[0].Proteins[0],
        sph.Peptides[0].Proteins.Count,
        sph.MatchedIonCount,
        sph.TheoreticalIonCount,
        sph.TheoreticalMH - Atom.H.AverageMass,
        sph.TheoreticalMinusExperimentalMass <= 0 ? "+" : "",
        -sph.TheoreticalMinusExperimentalMass,
        2,
        this.sampleProtease.GetMissCleavageSiteCount(pureSeq));

      if (this.currentSequestParams.StaticModification.Count > 0)
      {
        bool bFirst = true;
        for (int i = 0; i < sph.Peptides[0].PureSequence.Length; i++)
        {
          char c = sph.Peptides[0].PureSequence[i];
          if (this.staticAminoacidModification.ContainsKey(c))
          {
            double mass = this.staticAminoacidModification[c];
            if (0.0 == mass)
            {
              continue;
            }

            if (bFirst)
            {
              this.sw.WriteLine("          <modification_info>");
              bFirst = false;
            }
            this.sw.WriteLine("            <mod_aminoacid_mass position=\"{0}\" mass=\"{1:0.0000}\"/>", i + 1,
                              this.aas[c].MonoMass + this.staticAminoacidModification[c]);
          }
        }
        if (!bFirst)
        {
          this.sw.WriteLine("          </modification_info>");
        }
      }

      this.sw.WriteLine("          <search_score name=\"Score\" value=\"{0:0.000}\"/>", sph.Score);
      this.sw.WriteLine("          <search_score name=\"DeltaScore\" value=\"{0:0.000}\"/>", sph.DeltaScore);
      this.sw.WriteLine("          <search_score name=\"DeltaScorestar\" value=\"0\"/>");
      this.sw.WriteLine("          <search_score name=\"spscore\" value=\"{0:0.0}\"/>", sph.SpScore);
      this.sw.WriteLine("          <search_score name=\"sprank\" value=\"{0}\"/>", sph.SpRank);

      if (sph.Peptides[0].Proteins.Count > 1)
      {
        for (int iProtein = 1; iProtein < sph.Peptides[0].Proteins.Count; iProtein++)
        {
          this.sw.WriteLine("          <alternative_protein protein=\"{0}\" />",
                            sph.Peptides[0].Proteins[iProtein]);
        }
      }

      this.sw.WriteLine("        </search_hit>");
      this.sw.WriteLine("      </search_result>");
      this.sw.WriteLine("    </spectrum_query>");
    }

    private void WriteModifications(SequestParam sp)
    {
      var aas = new Aminoacids();
      foreach (SequestStaticModification ssm in sp.StaticModification.Keys)
      {
        double addMass = sp.StaticModification[ssm];
        if (addMass != 0.0)
        {
          switch (ssm)
          {
            case SequestStaticModification.add_Cterm_peptide:
            case SequestStaticModification.add_Cterm_protein:
            case SequestStaticModification.add_Nterm_peptide:
            case SequestStaticModification.add_Nterm_protein:
              break;
            default:
              char aa = SequestParam.ModificationToAminoacid(ssm);
              this.sw.WriteLine(
                "      <aminoacid_modification aminoacid=\"{0}\" massdiff=\"{1:0.0000}\" mass=\"{2:0.0000}\" variable=\"N\"/>",
                aa, addMass, aas[aa].MonoMass + addMass);
              break;
          }
        }
      }

      foreach (String aminoacids in sp.Diff_search_options.Keys)
      {
        double addMass = sp.Diff_search_options[aminoacids];
        foreach (char aa in aminoacids)
        {
          this.sw.WriteLine(
            "      <aminoacid_modification aminoacid=\"{0}\" massdiff=\"{1:0.0000}\" mass=\"{2:0.0000}\" variable=\"Y\"/>",
            aa, addMass, aas[aa].MonoMass + addMass);
        }
      }
    }

    #region IDisposable Members

    public void Dispose()
    {
      Close();
    }

    #endregion
  }
}