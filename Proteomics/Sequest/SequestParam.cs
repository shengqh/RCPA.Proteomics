using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace RCPA.Proteomics.Sequest
{
  public enum SequestStaticModification
  {
    add_Cterm_peptide,
    add_Cterm_protein,
    add_Nterm_peptide,
    add_Nterm_protein,
    add_G_Glycine,
    add_A_Alanine,
    add_S_Serine,
    add_P_Proline,
    add_V_Valine,
    add_T_Threonine,
    add_C_Cysteine,
    add_L_Leucine,
    add_I_Isoleucine,
    add_X_LorI,
    add_N_Asparagine,
    add_O_Ornithine,
    add_B_avg_NandD,
    add_D_Aspartic_Acid,
    add_Q_Glutamine,
    add_K_Lysine,
    add_Z_avg_QandE,
    add_E_Glutamic_Acid,
    add_M_Methionine,
    add_H_Histidine,
    add_F_Phenylalanine,
    add_R_Arginine,
    add_Y_Tyrosine,
    add_W_Tryptophan
  }

  public class SequestParam
  {
    private readonly Dictionary<string, double> diff_search_options = new Dictionary<string, double>();
    private readonly Pair<double, double> protein_mass_filter = new Pair<double, double>(0, 0);

    private readonly Dictionary<SequestStaticModification, double> staticModification =
      new Dictionary<SequestStaticModification, double>();

    private readonly Pair<double, double> term_diff_search_options = new Pair<double, double>(0, 0);

    public SequestParam()
    {
      Nucleotide_reading_frame = 0;
      Partial_sequence = "";
      Sequence_header_filter = "";
      Min_number_termini = 2;
    }

    public String First_database_name { get; set; }

    public double Peptide_mass_tolerance { get; set; }

    public String Ion_series { get; set; }

    public double Fragment_ion_tolerance { get; set; }

    public int Num_output_lines { get; set; }

    public int Num_results { get; set; }

    public int Num_description_lines { get; set; }

    public int Show_fragment_ions { get; set; }

    public int Print_duplicate_references { get; set; }

    public Protease Protease { get; set; }

    public int Max_num_differential_AA_per_mod { get; set; }

    public Dictionary<string, double> Diff_search_options
    {
      get { return this.diff_search_options; }
    }

    public Pair<double, double> Term_diff_search_options
    {
      get { return this.term_diff_search_options; }
    }

    public int Nucleotide_reading_frame { get; set; }

    public int Mass_type_parent { get; set; }

    public string Mass_type_parent_annotation
    {
      get
      {
        if (Mass_type_fragment == 1)
        {
          return "monoisotopic";
        }
        else
        {
          return "average";
        }
      }
    }

    public int Mass_type_fragment { get; set; }

    public string Mass_type_fragment_annotation
    {
      get
      {
        if (Mass_type_fragment == 1)
        {
          return "monoisotopic";
        }
        else
        {
          return "average";
        }
      }
    }

    public int Normalize_Score { get; set; }

    public int Remove_precursor_peak { get; set; }

    public double Ion_cutoff_percentage { get; set; }

    public int Max_num_internal_cleavage_sites { get; set; }

    public Pair<double, double> Protein_mass_filter
    {
      get { return this.protein_mass_filter; }
    }

    public int Match_peak_count { get; set; }

    public int Match_peak_allowed_error { get; set; }

    public double Match_peak_tolerance { get; set; }

    public string Partial_sequence { get; set; }

    public string Sequence_header_filter { get; set; }

    public int Min_number_termini { get; set; }

    public Dictionary<SequestStaticModification, double> StaticModification
    {
      get { return this.staticModification; }
    }

    public static char ModificationToAminoacid(SequestStaticModification ssm)
    {
      switch (ssm)
      {
        case SequestStaticModification.add_Cterm_peptide:
        case SequestStaticModification.add_Cterm_protein:
        case SequestStaticModification.add_Nterm_peptide:
        case SequestStaticModification.add_Nterm_protein:
          break;
        default:
          string name = ssm.ToString();
          Match m = Regex.Match(name, @"_(\S)_");
          if (m.Success)
          {
            return m.Groups[1].Value[0];
          }
          break;
      }
      return ' ';
    }

    public static SequestStaticModification AminoacidToModification(char aa)
    {
      String tmp = "_" + aa + "_";
      foreach (string ssm in Enum.GetNames(typeof(SequestStaticModification)))
      {
        if (ssm.IndexOf(tmp) != -1)
        {
          return (SequestStaticModification)Enum.Parse(typeof(SequestStaticModification), ssm);
        }
      }
      throw new ArgumentException("Cannot find modification for " + aa);
    }

    public static SequestStaticModification NameToModification(String name)
    {
      foreach (string ssm in Enum.GetNames(typeof(SequestStaticModification)))
      {
        if (ssm.Equals(name))
        {
          return (SequestStaticModification)Enum.Parse(typeof(SequestStaticModification), ssm);
        }
      }
      throw new ArgumentException("Cannot find modification for " + name);
    }

    public bool IsPrecursorMonoisotopic()
    {
      return Mass_type_parent == 1;
    }

    public Dictionary<char, double> GetStaticAminoacidModification()
    {
      var result = new Dictionary<char, double>();

      foreach (SequestStaticModification ssm in this.staticModification.Keys)
      {
        char c = ModificationToAminoacid(ssm);
        if (' ' == c)
        {
          continue;
        }

        result[c] = this.staticModification[ssm];
      }

      return result;
    }

    public IPeptideMassCalculator GetPeptideMassCalculator()
    {
      bool isMono = IsPrecursorMonoisotopic();

      var aas = new Aminoacids();
      Dictionary<char, double> staticModifications = GetStaticAminoacidModification();
      foreach (char aa in staticModifications.Keys)
      {
        aas[aa].ResetMass(aas[aa].MonoMass + staticModifications[aa], aas[aa].AverageMass + staticModifications[aa]);
      }

      var diff = new[] { '*', '#', '@', '^', '~', '$' };
      int i = 0;
      foreach (double mod in Diff_search_options.Values)
      {
        aas[diff[i++]].ResetMass(mod, mod);
      }

      double nterm = isMono ? Atom.H.MonoMass : Atom.H.AverageMass;
      double cterm = isMono ? Atom.H.MonoMass + Atom.O.MonoMass : Atom.H.AverageMass + Atom.O.AverageMass;

      if (this.term_diff_search_options.First != 0.0 || this.term_diff_search_options.Second != 0.0)
      {
        throw new Exception(
          "Term dynamic modification has not been implemented into this function, call author to fix it.");
      }

      IPeptideMassCalculator result;
      if (isMono)
      {
        result = new MonoisotopicPeptideMassCalculator(aas, nterm, cterm);
      }
      else
      {
        result = new AveragePeptideMassCalculator(aas, nterm, cterm);
      }

      return result;
    }
  }
}