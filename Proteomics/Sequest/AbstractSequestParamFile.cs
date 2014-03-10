using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace RCPA.Proteomics.Sequest
{
  internal abstract class AbstractSequestParamFile : ISequestParamFile
  {
    private Regex regex = new Regex(@"(.+?)=\s*(\S+)");

    #region ISequestParamFile Members

    public SequestParam ReadFromFile(String paramFilename)
    {
      List<String> contents = ReadSequestParamContents(paramFilename);

      Dictionary<string, Pair<string, string>> parameters = InitParameters(contents);

      var result = new SequestParam();

      ParseCommon(result, contents, parameters);

      ParseSpecial(result, contents, parameters);

      return result;
    }

    #endregion

    protected abstract void ParseSpecial(SequestParam result, List<String> contents,
                                         Dictionary<string, Pair<string, string>> parameters);

    private void ParseCommon(SequestParam result, List<String> contents,
                             Dictionary<string, Pair<string, string>> parameters)
    {
      result.First_database_name = parameters["first_database_name"].First;
      result.Peptide_mass_tolerance = MyConvert.ToDouble(parameters["peptide_mass_tolerance"].First);
      result.Ion_series = parameters["ion_series"].First;
      result.Fragment_ion_tolerance = MyConvert.ToDouble(parameters["fragment_ion_tolerance"].First);
      result.Num_output_lines = int.Parse(parameters["num_output_lines"].First);
      result.Num_results = int.Parse(parameters["num_results"].First);
      result.Num_description_lines = int.Parse(parameters["num_description_lines"].First);
      result.Show_fragment_ions = int.Parse(parameters["show_fragment_ions"].First);
      result.Print_duplicate_references = int.Parse(parameters["print_duplicate_references"].First);
      if (parameters.ContainsKey("max_num_differential_per_peptide"))
      {
        result.Max_num_differential_AA_per_mod = int.Parse(parameters["max_num_differential_per_peptide"].First);
      }
      else
      {
        result.Max_num_differential_AA_per_mod = int.Parse(parameters["max_num_differential_AA_per_mod"].First);
      }

      string diff_search_options = parameters["diff_search_options"].First;
      result.Diff_search_options.Clear();
      Match m = Regex.Match(diff_search_options, @"([\d.-]+)\s+(\S+)");
      if (m.Success)
      {
        do
        {
          double addMass = MyConvert.ToDouble(m.Groups[1].Value);
          if (addMass != 0.0)
          {
            result.Diff_search_options[m.Groups[2].Value] = addMass;
          }
          m = m.NextMatch();
        } while (m.Success);
      }

      string term_diff_search_options = parameters["term_diff_search_options"].First;
      String[] parts1 = Regex.Split(term_diff_search_options, @"\s+");
      result.Term_diff_search_options.First = MyConvert.ToDouble(parts1[0]);
      result.Term_diff_search_options.Second = MyConvert.ToDouble(parts1[1]);

      result.Nucleotide_reading_frame = int.Parse(parameters["nucleotide_reading_frame"].First);
      result.Mass_type_parent = int.Parse(parameters["mass_type_parent"].First);
      result.Mass_type_fragment = int.Parse(parameters["mass_type_fragment"].First);
      result.Remove_precursor_peak = int.Parse(parameters["remove_precursor_peak"].First);
      result.Ion_cutoff_percentage = MyConvert.ToDouble(parameters["ion_cutoff_percentage"].First);
      result.Max_num_internal_cleavage_sites = int.Parse(parameters["max_num_internal_cleavage_sites"].First);

      string protein_mass_filter = parameters["protein_mass_filter"].First;
      String[] parts2 = Regex.Split(protein_mass_filter, @"\s+");
      result.Protein_mass_filter.First = MyConvert.ToDouble(parts2[0]);
      result.Protein_mass_filter.Second = MyConvert.ToDouble(parts2[1]);

      result.Match_peak_count = int.Parse(parameters["match_peak_count"].First);
      result.Match_peak_allowed_error = int.Parse(parameters["match_peak_allowed_error"].First);
      result.Match_peak_tolerance = MyConvert.ToDouble(parameters["match_peak_tolerance"].First);
      result.Partial_sequence = parameters["partial_sequence"].First;
      result.Sequence_header_filter = parameters["sequence_header_filter"].First;

      foreach (string ssm in Enum.GetNames(typeof (SequestStaticModification)))
      {
        if (parameters.ContainsKey(ssm))
        {
          result.StaticModification[SequestParam.NameToModification(ssm)] = MyConvert.ToDouble(parameters[ssm].First);
        }
      }
    }

    private static Dictionary<string, Pair<string, string>> InitParameters(List<string> contents)
    {
      var parameters = new Dictionary<string, Pair<string, string>>();
      var separators = new[] {'=', ';'};
      foreach (String line in contents)
      {
        if (line.Trim().Equals("[SEQUEST_ENZYME_INFO]"))
        {
          break;
        }

        if (line.Trim().Length == 0)
        {
          continue;
        }

        String[] parts = line.Split(separators);
        if (parts.Length >= 3)
        {
          parameters[parts[0].Trim()] = new Pair<string, string>(parts[1].Trim(), parts[2].Trim());
        }
        else if (parts.Length == 2)
        {
          parameters[parts[0].Trim()] = new Pair<string, string>(parts[1].Trim(), null);
        }
        else
        {
          parameters[parts[0].Trim()] = new Pair<string, string>("", null);
        }
      }
      return parameters;
    }

    protected List<String> ReadSequestParamContents(String paramFilename)
    {
      var result = new List<string>();

      var br = StreamUtils.GetParameterFileStream(paramFilename);
      try
      {
        String line;
        while ((line = br.ReadLine()) != null)
        {
          if (line.Equals("[SEQUEST]"))
          {
            result.Clear();
          }
          else if (line.StartsWith("[SEQUEST_OUT]"))
          {
            break;
          }

          result.Add(line);
        }
      }
      finally
      {
        br.Close();
      }

      return result;
    }
  }
}