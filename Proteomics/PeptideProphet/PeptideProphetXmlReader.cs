using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RCPA.Proteomics.Summary;
using System.Xml.Linq;
using RCPA.Gui;

namespace RCPA.Proteomics.PeptideProphet
{
  public class PeptideProphetModificationItem
  {
    public bool IsAminoacid { get; set; }
    public bool IsProteinTerminal { get; set; }
    public bool IsTerminalN { get; set; }

    public string Aminoacid { get; set; }
    public double Mass { get; set; }
    public double MassDiff { get; set; }
    public bool IsVariable { get; set; }
    public string ModificationChar { get; set; }

    public override string ToString()
    {
      if (IsAminoacid)
      {
        return MyConvert.Format("{0}({1:0.000000})", Aminoacid, MassDiff);
      }

      var ptype = IsProteinTerminal ? "Protein " : "Peptide ";
      var ttype = IsTerminalN ? "N-term" : "C-term";

      return MyConvert.Format("{0}{1}({2:0.000000})", ptype, ttype, MassDiff);
    }
  }

  public class PeptideProphetModifications : List<PeptideProphetModificationItem>
  {
    public const string MODIFICATION_CHAR = " *#@&^%$~1234567890";

    public void AssignModificationChar()
    {
      this.ForEach(m => m.ModificationChar = string.Empty);

      var variables = this.FindAll(m => m.IsVariable);

      int charIndex = 1;
      for (int i = 0; i < variables.Count; i++)
      {
        for (int j = 0; j < i; j++)
        {
          if (variables[i].MassDiff == variables[j].MassDiff)
          {
            variables[i].ModificationChar = variables[j].ModificationChar;
            break;
          }
        }

        if (variables[i].ModificationChar == string.Empty)
        {
          variables[i].ModificationChar = MODIFICATION_CHAR[charIndex++].ToString();
        }
      }
    }

    public bool HasModification(double mass)
    {
      return this.Find(m => m.Mass == mass) != null;
    }

    public string FindModificationChar(double mass)
    {
      var findMod = this.Find(m => m.Mass == mass);
      if (findMod == null)
      {
        throw new Exception(MyConvert.Format("Cannot find the modification corresponding to mass {0:0.000000}", mass));
      }
      return findMod.ModificationChar;
    }
  }

  public class ModificationAminoacidMass
  {
    public int Position { get; set; }
    public double Mass { get; set; }
  }

  public class PeptideProphetXmlReader : ProgressClass, IFileReader<List<IIdentifiedSpectrum>>
  {
    private ITitleParser parser;
    public PeptideProphetXmlReader(ITitleParser parser)
    {
      this.parser = parser;
    }

    public Protease ParseProtease(XElement enzyme)
    {
      var name = enzyme.Attribute("name").Value;
      var specificity = enzyme.FindFirstChild("specificity");
      var cut = specificity.Attribute("cut").Value;
      var notcut = specificity.Attribute("no_cut").Value;
      var sense = specificity.Attribute("sense").Value;

      return new Protease(name, sense == "C", cut, notcut);
    }

    public PeptideProphetModifications ParseModifications(XElement searchSummary)
    {
      PeptideProphetModifications result = new PeptideProphetModifications();
      var aminoacid_modifications = searchSummary.FindChildren("aminoacid_modification");
      foreach (var am in aminoacid_modifications)
      {
        PeptideProphetModificationItem item = new PeptideProphetModificationItem();
        item.IsVariable = am.Attribute("variable").Value.Equals("Y");
        item.IsAminoacid = true;
        item.IsProteinTerminal = false;
        item.IsTerminalN = false;
        item.Aminoacid = am.Attribute("aminoacid").Value;
        item.Mass = MyConvert.ToDouble(am.Attribute("mass").Value);
        item.MassDiff = MyConvert.ToDouble(am.Attribute("massdiff").Value);
        result.Add(item);
      }

      var terminal_modifications = searchSummary.FindChildren("terminal_modification");
      foreach (var am in terminal_modifications)
      {
        PeptideProphetModificationItem item = new PeptideProphetModificationItem();
        item.IsVariable = am.Attribute("variable").Value.Equals("Y");
        item.IsAminoacid = false;
        item.IsProteinTerminal = am.Attribute("protein_terminus").Value.Equals("Y");
        item.IsTerminalN = am.Attribute("terminus").Value.Equals("n"); ;
        item.Mass = MyConvert.ToDouble(am.Attribute("mass").Value);
        item.MassDiff = MyConvert.ToDouble(am.Attribute("massdiff").Value);
        result.Add(item);
      }

      result.AssignModificationChar();
      return result;
    }

    #region IFileReader<List<IIdentifiedSpectrum>> Members

    public List<IIdentifiedSpectrum> ReadFromFile(string fileName)
    {
      List<IIdentifiedSpectrum> result = new List<IIdentifiedSpectrum>();

      XElement root = XElement.Load(fileName);

      var msms_run_summaries = root.FindChildren("msms_run_summary");
      foreach (var msms_run_summary in msms_run_summaries)
      {
        var search_summary = msms_run_summary.FindFirstChild("search_summary");
        var engine = search_summary.Attribute("search_engine").Value;
        var ismono = search_summary.Attribute("precursor_mass_type").Value.Equals("monoisotopic");

        var enzyme = ParseProtease(msms_run_summary.FindFirstChild("sample_enzyme"));
        var modifications = ParseModifications(msms_run_summary);

        var spectrumQueries = msms_run_summary.FindChildren("spectrum_query");

        foreach (var sp in spectrumQueries)
        {
          IdentifiedSpectrum sph = new IdentifiedSpectrum();

          sph.IsPrecursorMonoisotopic = ismono;

          var sf = parser.GetValue(sp.Attribute("spectrum").Value);
          sph.Query.FileScan.LongFileName = sf.LongFileName;

          sph.ExperimentalMass = MyConvert.ToDouble(sp.Attribute("precursor_neutral_mass").Value);
          sph.Query.Charge = int.Parse(sp.Attribute("assumed_charge").Value);

          var searchResult = sp.FindFirstChild("search_result");
          var searchHit = searchResult.FindFirstChild("search_hit");

          sph.TheoreticalMass = MyConvert.ToDouble(searchHit.Attribute("calc_neutral_pep_mass").Value);
          sph.MatchedIonCount = int.Parse(searchHit.Attribute("num_matched_ions").Value);
          sph.TheoreticalIonCount = int.Parse(searchHit.Attribute("tot_num_ions").Value);

          ParseSearchHit(sph, searchHit, modifications);

          sph.Engine = engine;
          sph.DigestProtease = enzyme;

          result.Add(sph);
        }
      }

      return result;
    }

    private void ParseSearchHit(IIdentifiedSpectrum sph, XElement searchHit, PeptideProphetModifications ppmods)
    {
      var sp = new IdentifiedPeptide(sph);

      var mod_info = searchHit.FindFirstChild("modification_info");

      string seq = searchHit.Attribute("peptide").Value;
      if (mod_info != null)
      {
        var modaas = PeptideProphetUtils.ParseModificationAminoacidMass(mod_info);
        if (modaas != null && modaas.Count > 0)
        {
          modaas.Reverse();
          foreach (var modaa in modaas)
          {
            var modchar = ppmods.FindModificationChar(modaa.Mass);
            seq = seq.Insert(modaa.Position, modchar);
          }
        }
      }
      sp.Sequence = searchHit.Attribute("peptide_prev_aa").Value + "." +
                    seq + "." +
                    searchHit.Attribute("peptide_next_aa").Value;

      sph.NumMissedCleavages = int.Parse(searchHit.Attribute("num_missed_cleavages").Value);
      sph.NumProteaseTermini = int.Parse(searchHit.Attribute("num_tol_term").Value);

      sp.AddProtein(searchHit.Attribute("protein").Value);

      var NumTotalProteins = int.Parse(searchHit.Attribute("num_tot_proteins").Value);
      if (NumTotalProteins > 1)
      {
        var alternative_proteins = searchHit.FindChildren("alternative_protein");
        foreach (var alternative_protein in alternative_proteins)
        {
          sp.AddProtein(alternative_protein.Attribute("protein").Value);
        }
      }

      var analysis_results = searchHit.FindChildren("analysis_result");

      var ppresult = analysis_results.Find(m => m.Attribute("analysis").Value.Equals("peptideprophet"));

      if (null != ppresult)
      {
        sph.PValue = MyConvert.ToDouble(ppresult.FindFirstChild("peptideprophet_result").Attribute("probability").Value);
      }

      var ipresult = analysis_results.Find(m => m.Attribute("analysis").Value.Equals("interprophet"));

      if (null != ipresult)
      {
        sph.PValue = MyConvert.ToDouble(ipresult.FindFirstChild("interprophet_result").Attribute("probability").Value);
      }

      ParseScore(sph, searchHit);
    }

    protected virtual void ParseScore(IIdentifiedSpectrum sph, XElement searchHit)
    {
      var scores = searchHit.FindChildren("search_score");

      foreach (var item in scores)
      {
        var name = item.Attribute("name").Value;
        if (name.Equals("ionscore"))
        {
          sph.Score = MyConvert.ToDouble(item.Attribute("value").Value);
        }
        else if (name.Equals("expect"))
        {
          sph.ExpectValue = MyConvert.ToDouble(item.Attribute("value").Value);
        }
      }
    }

    #endregion
  }
}
