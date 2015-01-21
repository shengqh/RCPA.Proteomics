using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using RCPA.Proteomics.Sequest;
using RCPA.Proteomics.Summary;

namespace RCPA.Proteomics.PeptideProphet
{
  public class AbstractPepXmlWriter : IFileWriter<List<IIdentifiedSpectrum>>
  {
    private readonly Aminoacids aas = new Aminoacids();

    //private SequestParam currentSequestParams;

    //private Dictionary<char, double> staticAminoacidModification;

    private PepXmlWriterParameters parameters;

    public AbstractPepXmlWriter(PepXmlWriterParameters parameters)
    {
      this.parameters = parameters;
    }

    //private void WriteModifications(SequestParam sp)
    //{
    //  var aas = new Aminoacids();
    //  foreach (SequestStaticModification ssm in sp.StaticModification.Keys)
    //  {
    //    double addMass = sp.StaticModification[ssm];
    //    if (addMass != 0.0)
    //    {
    //      switch (ssm)
    //      {
    //        case SequestStaticModification.add_Cterm_peptide:
    //        case SequestStaticModification.add_Cterm_protein:
    //        case SequestStaticModification.add_Nterm_peptide:
    //        case SequestStaticModification.add_Nterm_protein:
    //          break;
    //        default:
    //          char aa = SequestParam.ModificationToAminoacid(ssm);
    //          sw.WriteLine(
    //            "      <aminoacid_modification aminoacid=\"{0}\" massdiff=\"{1:0.0000}\" mass=\"{2:0.0000}\" variable=\"N\"/>",
    //            aa, addMass, aas[aa].MonoMass + addMass);
    //          break;
    //      }
    //    }
    //  }
    //  foreach (String aminoacids in sp.Diff_search_options.Keys)
    //  {
    //    double addMass = sp.Diff_search_options[aminoacids];
    //    foreach (char aa in aminoacids)
    //    {
    //      sw.WriteLine(
    //        "      <aminoacid_modification aminoacid=\"{0}\" massdiff=\"{1:0.0000}\" mass=\"{2:0.0000}\" variable=\"Y\"/>",
    //        aa, addMass, aas[aa].MonoMass + addMass);
    //    }
    //  }
    //}

    public void WriteToFile(string fileName, List<IIdentifiedSpectrum> t)
    {
      var resultFile = new FileInfo(fileName);

      using (var sw = new StreamWriter(fileName))
      {
        sw.WriteLine("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
        DateTime dt = DateTime.Now;
        sw.WriteLine("<msms_pipeline_analysis date=\"{0}\" xmlns=\"http://regis-web.systemsbiology.net/pepXML\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xsi:schemaLocation=\"http://sashimi.sourceforge.net/schema_revision/pepXML/pepXML_v117.xsd\" summary_xml=\"{1}\">",
          dt.ToString("yyyy-MM-dd\\THH:mm:ss"),
          fileName.Replace("\\", "/"));

        //Start of msms_run_summary
        sw.WriteLine("  <msms_run_summary base_name=\"{0}\" msManufacturer=\"unknown\" msModel=\"unknown\" raw_data_type=\"raw\" raw_data=\"{1}\">",
          FileUtils.ChangeExtension(this.parameters.SourceFile, ""),
          Path.GetExtension(parameters.SourceFile));

        //Export protease information
        sw.WriteLine(@"<sample_enzyme name=""{0}"">
<specificity cut=""{1}"" no_cut=""{2}"" sense=""{3}""/>
</sample_enzyme>)",
                  parameters.Protease.Name,
                  parameters.Protease.CleaveageResidues,
                  parameters.Protease.NotCleaveResidues,
                  parameters.Protease.IsEndoProtease ? "C" : "N");

        //search_summary
        sw.WriteLine("    <search_summary base_name=\"{0}\" search_engine=\"{1}\" precursor_mass_type=\"{2}\" fragment_mass_type=\"{3}\" search_id=\"1\">",
          parameters.SourceFile,
          parameters.SearchEngine,
          parameters.MassTypeParentAnnotation,
          parameters.MassTypeFragmentAnnotation);

        sw.WriteLine("      <search_database local_path=\"{0}\" type=\"AA\"/>",
                          parameters.SearchDatabase);

        sw.WriteLine("      <enzymatic_search_constraint enzyme=\"{0}\" max_num_internal_cleavages=\"{1}\" min_number_termini=\"{2}\"/>",
          parameters.Protease.Name,
          t.Max(l => l.NumMissedCleavages),
          t.Min(l => l.NumProteaseTermini));

        //WriteModifications(sp);

        //parameters
        //sw.WriteLine("      <parameter name=\"peptide_mass_tol\" value=\"{0:0.0000}\"/>", sp.Peptide_mass_tolerance);
        //sw.WriteLine("      <parameter name=\"ion_series\" value=\"{0}\"/>", sp.Ion_series);
        //sw.WriteLine("      <parameter name=\"fragment_ion_tol\" value=\"{0:0.0000}\"/>", sp.Fragment_ion_tolerance);
        //sw.WriteLine("      <parameter name=\"num_output_lines\" value=\"{0}\"/>", sp.Num_output_lines);
        //sw.WriteLine("      <parameter name=\"num_results\" value=\"{0}\"/>", sp.Num_results);
        //sw.WriteLine("      <parameter name=\"num_description_lines\" value=\"{0}\"/>", sp.Num_description_lines);
        //sw.WriteLine("      <parameter name=\"show_fragment_ions\" value=\"{0}\"/>", sp.Show_fragment_ions);
        //sw.WriteLine("      <parameter name=\"print_duplicate_references\" value=\"{0}\"/>",
        //                  sp.Print_duplicate_references);
        //sw.WriteLine("      <parameter name=\"max_num_differential_AA_per_mod\" value=\"{0}\"/>",
        //                  sp.Max_num_differential_AA_per_mod);
        //sw.WriteLine("      <parameter name=\"nucleotide_reading_frame\" value=\"{0}\"/>",
        //                  sp.Nucleotide_reading_frame);
        //sw.WriteLine("      <parameter name=\"normalize_Score\" value=\"{0}\"/>", sp.Normalize_Score);
        //sw.WriteLine("      <parameter name=\"remove_precursor_peak\" value=\"{0}\"/>", sp.Remove_precursor_peak);
        //sw.WriteLine("      <parameter name=\"ion_cutoff_percentage\" value=\"{0:0.0000}\"/>",
        //                  sp.Ion_cutoff_percentage);
        //sw.WriteLine("      <parameter name=\"max_num_internal_cleavage_sites\" value=\"{0}\"/>",
        //                  sp.Max_num_internal_cleavage_sites);
        //sw.WriteLine("      <parameter name=\"protein_mass_filter\" value=\"{0} {1}\"/>",
        //                  sp.Protein_mass_filter.First, sp.Protein_mass_filter.Second);
        //sw.WriteLine("      <parameter name=\"match_peak_count\" value=\"{0}\"/>", sp.Match_peak_count);
        //sw.WriteLine("      <parameter name=\"match_peak_allowed_error\" value=\"{0}\"/>",
        //                  sp.Match_peak_allowed_error);
        //sw.WriteLine("      <parameter name=\"match_peak_tolerance\" value=\"{0}\"/>", sp.Match_peak_tolerance);
        //sw.WriteLine("      <parameter name=\"partial_sequence\" value=\"{0}\"/>", sp.Partial_sequence);
        //sw.WriteLine("      <parameter name=\"sequence_header_filter\" value=\"{0}\"/>", sp.Sequence_header_filter);

        //End of search_summary
        sw.WriteLine("    </search_summary>");

        var spectrumId = 0;
        foreach (var sph in t)
        {
          spectrumId++;

          sw.WriteLine(
            "    <spectrum_query spectrum=\"{0}.{1}.{2}.{4}\" start_scan=\"{1}\" end_scan=\"{2}\" precursor_neutral_mass=\"{3:0.0000}\" assumed_charge=\"{4}\" index=\"{5}\">",
            sph.Query.FileScan.Experimental,
            sph.Query.FileScan.FirstScan,
            sph.Query.FileScan.LastScan,
            sph.ExperimentalMH - Atom.H.AverageMass,
            sph.Query.Charge,
            spectrumId);

          sw.WriteLine("      <search_result>");

          double massDiff = sph.TheoreticalMinusExperimentalMass;

          if (sph.Peptides[0].Proteins.Count > 0)
          {
            sw.WriteLine(
              "        <search_hit hit_rank=\"1\" peptide=\"{0}\" protein=\"{1}\" num_tot_proteins=\"{2}\" num_matched_ions=\"{3}\" tot_num_ions=\"{4}\" calc_neutral_pep_mass=\"{5:0.0000}\" massdiff=\"{6}{7:0.0000}00\" num_tol_term=\"{8}\" num_missed_cleavages=\"{9}\" is_rejected=\"0\">",
              sph.Peptides[0].PureSequence,
              sph.Peptides[0].Proteins[0],
              sph.Peptides[0].Proteins.Count,
              sph.MatchedIonCount,
              sph.TheoreticalIonCount,
              sph.TheoreticalMH - Atom.H.AverageMass,
              sph.TheoreticalMinusExperimentalMass <= 0 ? "+" : "",
              -sph.TheoreticalMinusExperimentalMass,
              2,
              sph.NumMissedCleavages);
          }
          else
          {
            sw.WriteLine(
              "        <search_hit hit_rank=\"1\" peptide=\"{0}\" num_matched_ions=\"{1}\" tot_num_ions=\"{2}\" calc_neutral_pep_mass=\"{3:0.0000}\" massdiff=\"{4}{5:0.0000}00\" num_tol_term=\"{6}\" num_missed_cleavages=\"{7}\" is_rejected=\"0\">",
              sph.Peptides[0].PureSequence,
              sph.MatchedIonCount,
              sph.TheoreticalIonCount,
              sph.TheoreticalMH - Atom.H.AverageMass,
              sph.TheoreticalMinusExperimentalMass <= 0 ? "+" : "",
              -sph.TheoreticalMinusExperimentalMass,
              sph.NumProteaseTermini,
              sph.NumMissedCleavages);
          }

          //if (this.currentSequestParams.StaticModification.Count > 0)
          //{
          //  bool bFirst = true;
          //  for (int i = 0; i < sph.Peptides[0].PureSequence.Length; i++)
          //  {
          //    char c = sph.Peptides[0].PureSequence[i];
          //    if (this.staticAminoacidModification.ContainsKey(c))
          //    {
          //      double mass = this.staticAminoacidModification[c];
          //      if (0.0 == mass)
          //      {
          //        continue;
          //      }

          //      if (bFirst)
          //      {
          //        sw.WriteLine("          <modification_info>");
          //        bFirst = false;
          //      }
          //      sw.WriteLine("            <mod_aminoacid_mass position=\"{0}\" mass=\"{1:0.0000}\"/>", i + 1,
          //                        this.aas[c].MonoMass + this.staticAminoacidModification[c]);
          //    }
          //  }
          //  if (!bFirst)
          //  {
          //    sw.WriteLine("          </modification_info>");
          //  }
          //}

          WriteScore(sw, sph);

          if (sph.Peptides[0].Proteins.Count > 1)
          {
            for (int iProtein = 1; iProtein < sph.Peptides[0].Proteins.Count; iProtein++)
            {
              sw.WriteLine("          <alternative_protein protein=\"{0}\" />",
                                sph.Peptides[0].Proteins[iProtein]);
            }
          }

          sw.WriteLine("        </search_hit>");
          sw.WriteLine("      </search_result>");
          sw.WriteLine("    </spectrum_query>");
        }

        //End of msms_run_summary
        sw.WriteLine("  </msms_run_summary>");
      }
    }

    protected virtual void WriteScore(StreamWriter sw, IIdentifiedSpectrum sph)
    {
      sw.WriteLine("          <search_score name=\"Score\" value=\"{0:0.000}\"/>", sph.Score);
    }
  }
}