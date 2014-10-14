using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RCPA.Proteomics.Summary;
using System.Xml.Linq;
using RCPA.Gui;

namespace RCPA.Proteomics.PeptideProphet
{
  public class PeptideProphetXmlParser : ProgressClass, ISpectrumParser
  {
    public PeptideProphetXmlParser()
    { }

    public Protease ParseProtease(XElement enzyme)
    {
      var name = enzyme.Attribute("name").Value;
      if (ProteaseManager.Registered(name))
      {
        return ProteaseManager.GetProteaseByName(name);
      }

      var specificity = enzyme.FindFirstDescendant("specificity");
      if (specificity != null)
      {
        var cut = specificity.Attribute("cut").Value;
        var notcut = specificity.Attribute("no_cut").Value;
        var sense = specificity.Attribute("sense").Value;
        return new Protease(name, sense == "C", cut, notcut);
      }

      return new Protease(name, true, string.Empty, string.Empty);
    }

    public PeptideProphetModifications ParseModifications(XElement searchSummary)
    {
      PeptideProphetModifications result = new PeptideProphetModifications();
      var aminoacid_modifications = searchSummary.FindDescendants("aminoacid_modification");
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

      var terminal_modifications = searchSummary.FindDescendants("terminal_modification");
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

      var msms_run_summaries = root.FindDescendants("msms_run_summary");
      foreach (var msms_run_summary in msms_run_summaries)
      {
        var search_summary = msms_run_summary.FindFirstDescendant("search_summary");
        var engine = search_summary.Attribute("search_engine").Value;
        var ismono = search_summary.Attribute("precursor_mass_type").Value.Equals("monoisotopic");

        var enzyme = ParseProtease(msms_run_summary.FindFirstDescendant("sample_enzyme"));
        var modifications = ParseModifications(msms_run_summary);

        var spectrumQueries = msms_run_summary.FindDescendants("spectrum_query");

        foreach (var sp in spectrumQueries)
        {
          IdentifiedSpectrum sph = new IdentifiedSpectrum();

          sph.IsPrecursorMonoisotopic = ismono;

          var sf = TitleParser.GetValue(sp.Attribute("spectrum").Value);
          sph.Query.FileScan.LongFileName = sf.LongFileName;

          sph.ExperimentalMass = MyConvert.ToDouble(sp.Attribute("precursor_neutral_mass").Value);
          sph.Query.Charge = int.Parse(sp.Attribute("assumed_charge").Value);

          var searchResult = sp.FindFirstDescendant("search_result");
          var searchHit = searchResult.FindFirstDescendant("search_hit");

          if (searchHit == null)
          {
            continue;
          }

          sph.TheoreticalMass = MyConvert.ToDouble(searchHit.Attribute("calc_neutral_pep_mass").Value);
          sph.MatchedIonCount = int.Parse(searchHit.Attribute("num_matched_ions").Value);
          var ticAtt = searchHit.Attribute("tot_num_ions");
          sph.TheoreticalIonCount = ticAtt == null ? 0 : int.Parse(ticAtt.Value);

          ParseSearchHit(sph, searchHit, modifications);

          sph.Engine = engine;
          sph.DigestProtease = enzyme;

          result.Add(sph);
        }
      }

      return result;
    }

    private static int GetAttributeValue(XElement ele, string attrName, int defaultValue)
    {
      var attr = ele.Attribute(attrName);
      if (attr != null)
      {
        return int.Parse(attr.Value);
      }
      return defaultValue;
    }

    private void ParseSearchHit(IIdentifiedSpectrum sph, XElement searchHit, PeptideProphetModifications ppmods)
    {
      var sp = new IdentifiedPeptide(sph);

      var mod_info = searchHit.FindFirstDescendant("modification_info");

      string seq = searchHit.Attribute("peptide").Value;
      if (mod_info != null)
      {
        var modified_peptide = mod_info.Attribute("modified_peptide");
        if (modified_peptide != null)
        {
          seq = modified_peptide.Value;
        }
        else
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
      }

      if (searchHit.Attribute("peptide_prev_aa") != null)
      {
        sp.Sequence = searchHit.Attribute("peptide_prev_aa").Value + "." +
                      seq + "." +
                      searchHit.Attribute("peptide_next_aa").Value;
      }
      else
      {
        sp.Sequence = seq;
      }

      sph.NumMissedCleavages = GetAttributeValue(searchHit, "num_missed_cleavages", 0);
      sph.NumProteaseTermini = GetAttributeValue(searchHit, "num_tol_term", 2);

      sp.AddProtein(searchHit.Attribute("protein").Value);

      var NumTotalProteins = int.Parse(searchHit.Attribute("num_tot_proteins").Value);
      if (NumTotalProteins > 1)
      {
        var alternative_proteins = searchHit.FindDescendants("alternative_protein");
        foreach (var alternative_protein in alternative_proteins)
        {
          sp.AddProtein(alternative_protein.Attribute("protein").Value);
        }
      }

      var analysis_results = searchHit.FindDescendants("analysis_result");

      var ppresult = analysis_results.Find(m => m.Attribute("analysis").Value.Equals("peptideprophet"));

      if (null != ppresult)
      {
        sph.PValue = MyConvert.ToDouble(ppresult.FindFirstDescendant("peptideprophet_result").Attribute("probability").Value);
      }

      var ipresult = analysis_results.Find(m => m.Attribute("analysis").Value.Equals("interprophet"));

      if (null != ipresult)
      {
        sph.PValue = MyConvert.ToDouble(ipresult.FindFirstDescendant("interprophet_result").Attribute("probability").Value);
      }

      ParseScore(sph, searchHit);
    }

    protected virtual void ParseScore(IIdentifiedSpectrum sph, XElement searchHit)
    {
      var scores = searchHit.FindDescendants("search_score");

      foreach (var item in scores)
      {
        var name = item.Attribute("name").Value;
        if (name.Equals("ionscore") || name.Equals("IonScore"))
        {
          sph.Score = MyConvert.ToDouble(item.Attribute("value").Value);
        }
        else if (name.Equals("expect") || name.Equals("Exp Value"))
        {
          sph.ExpectValue = MyConvert.ToDouble(item.Attribute("value").Value);
        }
      }
    }

    #endregion

    public virtual SearchEngineType Engine
    {
      get { return SearchEngineType.PeptidePhophet; }
    }

    public ITitleParser TitleParser { get; set; }
  }
}
