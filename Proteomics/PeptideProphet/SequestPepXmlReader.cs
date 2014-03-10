using System.Collections.Generic;
using System.Xml;
using RCPA.Proteomics.Summary;
using RCPA.Utils;

namespace RCPA.Proteomics.PeptideProphet
{
  public class SequestPepXmlReader
  {
    public SequestPepXmlInfo Read(string filename)
    {
      var doc = new XmlDocument();
      var reader = new XmlTextReader(filename);
      reader.Read();
      doc.Load(reader);

      var xmlHelper = new XmlHelper(doc);

      var result = new SequestPepXmlInfo();

      List<XmlNode> msms_run_summaries = xmlHelper.GetChildren(doc.DocumentElement, "msms_run_summary");
      foreach (XmlNode msms_run_summary in msms_run_summaries)
      {
        XmlNode search_summary = xmlHelper.GetFirstChild(msms_run_summary, "search_summary");
        XmlNode search_database = xmlHelper.GetFirstChild(search_summary, "search_database");
        result.SearchDatabase = search_database.Attributes.GetNamedItem("local_path").Value;

        List<XmlNode> spectrumQueries = xmlHelper.GetChildren(msms_run_summary, "spectrum_query");
        List<SequestPeptideProphetItem> ppItems = result.PeptideProphetItems;
        foreach (XmlNode sp in spectrumQueries)
        {
          XmlAttributeCollection attributes = sp.Attributes;

          var ppi = new SequestPeptideProphetItem();
          IdentifiedSpectrum sph = ppi.SearchResult;

          sph.Query.FileScan.LongFileName = attributes.GetNamedItem("spectrum").Value + ".out";

          sph.ExperimentalMH = MyConvert.ToDouble(attributes.GetNamedItem("precursor_neutral_mass").Value) + Atom.H.MonoMass;
          sph.Query.Charge = int.Parse(attributes.GetNamedItem("assumed_charge").Value);

          XmlNode searchResult = xmlHelper.GetFirstChild(sp, "search_result");

          XmlNode searchHit = xmlHelper.GetFirstChild(searchResult, "search_hit");
          ParseSearchHit(ppi, searchHit, xmlHelper);

          ppItems.Add(ppi);
        }
      }

      return result;
    }

    private void ParseSearchHit(SequestPeptideProphetItem ppi, XmlNode searchHit, XmlHelper xmlHelper)
    {
      XmlAttributeCollection attributes = searchHit.Attributes;

      IdentifiedSpectrum sph = ppi.SearchResult;

      var sp = new IdentifiedPeptide(sph);

      sp.Sequence = attributes.GetNamedItem("peptide_prev_aa").Value + "." +
                    attributes.GetNamedItem("peptide").Value + "." +
                    attributes.GetNamedItem("peptide_next_aa").Value;

      sph.NumMissedCleavages = int.Parse(attributes.GetNamedItem("num_missed_cleavages").Value);

      sp.AddProtein(attributes.GetNamedItem("protein").Value);

      ppi.NumTotalProteins = int.Parse(attributes.GetNamedItem("num_tot_proteins").Value);
      if (ppi.NumTotalProteins > 1)
      {
        List<XmlNode> alternative_proteins = xmlHelper.GetChildren(searchHit, "alternative_protein");
        foreach (XmlNode alternative_protein in alternative_proteins)
        {
          sp.AddProtein(alternative_protein.Attributes.GetNamedItem("protein").Value);
        }
      }

      XmlNode analysis_result = xmlHelper.GetFirstChildByNameAndAttribute(searchHit, "analysis_result", "analysis",
                                                                         "peptideprophet");
      if (null != analysis_result)
      {
        XmlNode peptideprophet_result = xmlHelper.GetFirstChild(analysis_result, "peptideprophet_result");
        ppi.PeptideProphetProbability = MyConvert.ToDouble(peptideprophet_result.Attributes.GetNamedItem("probability").Value);
      }
    }
  }
}