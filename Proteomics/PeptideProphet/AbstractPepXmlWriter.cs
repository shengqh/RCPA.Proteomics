using RCPA.Proteomics.Summary;
using RCPA.Proteomics.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace RCPA.Proteomics.PeptideProphet
{
  public class AbstractPepXmlWriter : IFileWriter<List<IIdentifiedSpectrum>>
  {
    private readonly Aminoacids aas = new Aminoacids();

    private PepXmlWriterParameters parameters;

    public AbstractPepXmlWriter(PepXmlWriterParameters parameters)
    {
      this.parameters = parameters;
    }

    public void WriteToFile(string fileName, List<IIdentifiedSpectrum> t)
    {
      var resultFile = new FileInfo(fileName);

      using (var sw = new StreamWriter(fileName))
      {
        sw.NewLine = "\n";
        sw.WriteLine("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
        DateTime dt = DateTime.Now;
        sw.WriteLine("<msms_pipeline_analysis date=\"{0}\" xmlns=\"http://regis-web.systemsbiology.net/pepXML\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xsi:schemaLocation=\"http://sashimi.sourceforge.net/schema_revision/pepXML/pepXML_v117.xsd\" summary_xml=\"{1}\">",
          dt.ToString("yyyy-MM-dd\\THH:mm:ss"),
          Path.GetFileName(fileName));

        //Start of msms_run_summary
        sw.WriteLine("  <msms_run_summary base_name=\"{0}\" raw_data_type=\"raw\" raw_data=\"{1}\">",
          FileUtils.ChangeExtension(this.parameters.SourceFile, ""),
          Path.GetExtension(parameters.SourceFile));

        //Export protease information
        sw.WriteLine(@"  <sample_enzyme name=""{0}"">
    <specificity cut=""{1}"" no_cut=""{2}"" sense=""{3}""/>
  </sample_enzyme>",
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

        WriteModifications(sw, parameters.Modifications);

        var modChars = parameters.Modifications.Where(m => m.IsVariable).ToDictionary(m => m.Symbol[0], m => m.Mass);

        WriteParameters(sw, parameters.Parameters);

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

          if (!parameters.NotCombineRank1PSMs)
          {
            var pep = sph.Peptide;
            WritePeptide(sw, sph, pep);

            if (pep.Proteins.Count > 1)
            {
              for (int iProtein = 1; iProtein < pep.Proteins.Count; iProtein++)
              {
                WritePeptideProtein(sw, pep, iProtein);
              }
            }

            for (int i = 1; i < sph.Peptides.Count; i++)
            {
              for (int iProtein = 0; iProtein < sph.Peptides[i].Proteins.Count; iProtein++)
              {
                WritePeptideProtein(sw, sph.Peptides[i], iProtein);
              }
            }
            WriteModificationAndScore(sw, modChars, sph, pep);
            sw.WriteLine("        </search_hit>");
          }
          else
          {
            var peps = sph.Peptides.GroupBy(m => PeptideUtils.GetMatchedSequence(m.Sequence)).ToList().ConvertAll(m => m.ToArray()).ToList();

            foreach (var pepList in peps)
            {
              var pep = pepList.First();
              WritePeptide(sw, sph, pep);

              if (pep.Proteins.Count > 1)
              {
                for (int iProtein = 1; iProtein < pep.Proteins.Count; iProtein++)
                {
                  WritePeptideProtein(sw, pep, iProtein);
                }
              }

              for (int i = 1; i < pepList.Length; i++)
              {
                for (int iProtein = 0; iProtein < pepList[i].Proteins.Count; iProtein++)
                {
                  WritePeptideProtein(sw, pepList[i], iProtein);
                }
              }
              WriteModificationAndScore(sw, modChars, sph, pep);

              sw.WriteLine("        </search_hit>");
            }
          }
          sw.WriteLine("      </search_result>");
          sw.WriteLine("    </spectrum_query>");
        }

        //End of msms_run_summary
        sw.WriteLine("  </msms_run_summary>");
        sw.WriteLine("</msms_pipeline_analysis>");
      }
    }

    private void WriteModificationAndScore(StreamWriter sw, Dictionary<char, double> modChars, IIdentifiedSpectrum sph, IIdentifiedPeptide pep)
    {

      var matchSeq = PeptideUtils.GetMatchedSequence(pep.Sequence);

      if (matchSeq.Any(m => modChars.ContainsKey(m))) // modification
      {
        if (modChars.ContainsKey(matchSeq[0]))//Nterminal
        {
          sw.Write("          <modification_info mod_nterm_mass=\"{0:0.######}\"", modChars[matchSeq[0]]);
        }
        else
        {
          sw.Write("          <modification_info ");
        }
        sw.WriteLine(" modified_peptide=\"{0}\">", matchSeq);
        int pos = 1;
        double mass;
        for (int i = 0; i < matchSeq.Length; i++)
        {
          if (modChars.TryGetValue(matchSeq[i], out mass))
          {
            if (i == 0)
            {
              continue;
            }

            sw.WriteLine("            <mod_aminoacid_mass position=\"{0}\" mass=\"{1:0.######}\"/>", pos, mass);
          }
          else
          {
            pos++;
          }
        }
        sw.WriteLine("          </modification_info>");
      }

      WriteScore(sw, sph);
    }

    private static void WritePeptideProtein(StreamWriter sw, IIdentifiedPeptide pep, int iProtein)
    {
      sw.WriteLine("          <alternative_protein protein=\"{0}\" peptide_prev_aa=\"{1}\" peptide_next_aa=\"{2}\"/>",
        pep.Proteins[iProtein],
        pep.Sequence[1] == '.' ? pep.Sequence[0] : ' ',
        pep.Sequence[pep.Sequence.Length - 2] == '.' ? pep.Sequence[pep.Sequence.Length - 1] : ' ');
    }

    private static void WritePeptide(StreamWriter sw, IIdentifiedSpectrum sph, IIdentifiedPeptide pep)
    {
      sw.WriteLine(
        "        <search_hit hit_rank=\"1\" peptide=\"{0}\" peptide_prev_aa=\"{1}\" peptide_next_aa=\"{2}\" protein=\"{3}\" num_tot_proteins=\"{4}\" num_matched_ions=\"{5}\" tot_num_ions=\"{6}\" calc_neutral_pep_mass=\"{7:0.0000}\" massdiff=\"{8}{9:0.0000}\" num_tol_term=\"{10}\" num_missed_cleavages=\"{11}\" is_rejected=\"0\">",
        pep.PureSequence,
        pep.Sequence[1] == '.' ? pep.Sequence[0] : ' ',
        pep.Sequence[pep.Sequence.Length - 2] == '.' ? pep.Sequence[pep.Sequence.Length - 1] : ' ',
        pep.Proteins[0],
        pep.Proteins.Count,
        sph.MatchedIonCount,
        sph.TheoreticalIonCount,
        sph.TheoreticalMH - Atom.H.AverageMass,
        sph.TheoreticalMinusExperimentalMass <= 0 ? "+" : "",
        -sph.TheoreticalMinusExperimentalMass,
        2,
        sph.NumMissedCleavages);
    }

    private void WriteParameters(StreamWriter sw, Dictionary<string, string> parameters)
    {
      foreach (var param in parameters)
      {
        sw.WriteLine("      <parameter name=\"{0}\" value=\"{1}\"/>", param.Key, param.Value);
      }
    }

    private void WriteModifications(StreamWriter sw, PepXmlModifications sp)
    {
      var aas = new Aminoacids();
      foreach (var ssm in sp)
      {
        if (ssm.MassDiff != 0.0)
        {
          if (ssm.IsAminoacid)
          {
            sw.Write("      <aminoacid_modification aminoacid=\"{0}\" massdiff=\"{1:0.0000}\" mass=\"{2:0.0000}\" variable=\"{3}\"",
              ssm.Aminoacid,
              ssm.MassDiff,
              ssm.Mass,
              ssm.IsVariable ? "Y" : "N");
          }
          else
          {
            sw.Write("      <terminal_modification terminus=\"{0}\" massdiff=\"{1:0.0000}\" mass=\"{2:0.0000}\" variable=\"{3}\" protein_terminus=\"{4}\"",
              ssm.IsTerminalN ? "n" : "c",
              ssm.MassDiff,
              ssm.Mass,
              ssm.IsVariable ? "Y" : "N",
              ssm.IsProteinTerminal ? (ssm.IsTerminalN ? "n" : "c") : "");
          }

          if (string.IsNullOrEmpty(ssm.Symbol))
          {
            sw.WriteLine("/>");
          }
          else
          {
            sw.WriteLine(" symbol=\"{0}\"/>", ssm.Symbol);
          }
        }
      }
    }

    protected virtual void WriteScore(StreamWriter sw, IIdentifiedSpectrum sph)
    {
      sw.WriteLine("          <search_score name=\"Score\" value=\"{0:0.000}\"/>", sph.Score);
    }
  }
}