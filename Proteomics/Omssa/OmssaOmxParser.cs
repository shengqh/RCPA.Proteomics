using RCPA.Gui;
using RCPA.Proteomics.Modification;
using RCPA.Proteomics.Summary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace RCPA.Proteomics.Omssa
{
  public class OmssaOmxParser : ProgressClass, ISpectrumParser
  {
    public SearchEngineType Engine { get { return SearchEngineType.OMSSA; } }

    public ITitleParser TitleParser { get; set; }

    public OmssaOmxParser(ITitleParser parser)
    {
      this.TitleParser = parser;
    }

    public OmssaOmxParser() : this(new DefaultTitleParser()) { }

    /// <summary>
    /// Parse search modification definition
    /// </summary>
    /// <param name="mods"></param>
    /// <returns></returns>
    public Dictionary<string, string> ParseSearchModificationMap(XElement mods)
    {
      var result = new Dictionary<string, string>();

      var lst = (from ele in mods.FindElements("MSMod")
                 select ele.Value).ToList();

      for (int i = 0; i < lst.Count; i++)
      {
        result[lst[i]] = ModificationConsts.MODIFICATION_CHAR[i + 1].ToString();
      }

      return result;
    }

    /// <summary>
    /// Parse enzyme used in database searching
    /// </summary>
    /// <param name="enzyme"></param>
    /// <returns></returns>
    public Protease ParseProtease(XElement enzyme)
    {
      var enzymeIndex = enzyme.FindElement("MSEnzymes").Value;
      if (OmssaConsts.EnzymeMap.ContainsKey(enzymeIndex))
      {
        var name = OmssaConsts.EnzymeMap[enzymeIndex];
        if (ProteaseManager.Registered(name))
        {
          return ProteaseManager.GetProteaseByName(name);
        }
      }

      return null;
    }

    public List<IIdentifiedSpectrum> ReadFromFile(string fileName)
    {
      XElement root = XElement.Load(fileName);

      XElement request = root.FindElement("MSSearch_request");

      //parsing identification protocol first
      var modMap = ParseSearchModificationMap(request.FindFirstDescendant("MSSearchSettings_variable"));
      var protease = ParseProtease(request.FindFirstDescendant("MSSearchSettings_enzyme"));

      Func<string, int> missCalc;
      if (protease == null)
      {
        missCalc = m => 0;
      }
      else
      {
        missCalc = m => protease.GetMissCleavageSiteCount(m);
      }

      //parsing sequence collection, including protein<->peptide map
      var result = new List<IIdentifiedSpectrum>();
      var response = root.FindElement("MSSearch_response");
      var scale = double.Parse(response.FindFirstDescendant("MSResponse_scale").Value);

      var idList = response.FindFirstDescendant("MSResponse_hitsets");
      foreach (var sir in idList.FindElements("MSHitSet"))
      {
        var hits = sir.FindElement("MSHitSet_hits");
        if (hits == null)
        {
          continue;
        }

        var spectrum = new IdentifiedSpectrum();
        result.Add(spectrum);

        var title = sir.FindElement("MSHitSet_ids").FindElement("MSHitSet_ids_E").Value;
        spectrum.Query.FileScan = this.TitleParser.GetValue(title);

        foreach (var hit in hits.FindElements("MSHits"))
        {
          var evalue = double.Parse(hit.FindElement("MSHits_evalue").Value);
          if (spectrum.Peptides.Count > 0)
          {
            if (evalue > spectrum.ExpectValue)
            {
              continue;
            }
            if (evalue < spectrum.ExpectValue)
            {
              spectrum.ClearPeptides();
            }
          }
          spectrum.ExpectValue = evalue;
          spectrum.Score = -Math.Log(spectrum.ExpectValue);
          if (spectrum.Query.Charge == 0) // trust the charge from title
          {
            spectrum.Query.Charge = int.Parse(hit.FindElement("MSHits_charge").Value);
          }
          spectrum.ExperimentalMass = double.Parse(hit.FindElement("MSHits_mass").Value) / scale;
          spectrum.TheoreticalMass = double.Parse(hit.FindElement("MSHits_theomass").Value) / scale;

          var peptide = new IdentifiedPeptide(spectrum);
          var seq = hit.FindElement("MSHits_pepstring").Value;
          spectrum.NumMissedCleavages = missCalc(seq);

          var mods = hit.FindElement("MSHits_mods");
          if (mods != null)
          {
            var modsloc = (from ele in mods.FindElements("MSModHit")
                           let loc = int.Parse(ele.FindElement("MSModHit_site").Value)
                           let modtype = ele.FindElement("MSModHit_modtype").FindElement("MSMod").Value
                           orderby loc descending
                           select new { Location = loc, ModType = modtype }).ToList();
            foreach (var modloc in modsloc)
            {
              seq = seq.Insert(modloc.Location + 1, modMap[modloc.ModType]);
            }
          }

          peptide.Sequence = hit.FindElement("MSHits_pepstart").Value + "." + seq + "." + hit.FindElement("MSHits_pepstop").Value;

          foreach (var pep in hit.FindElement("MSHits_pephits").FindElements("MSPepHit"))
          {
            var proteinName = pep.FindElement("MSPepHit_defline").Value.StringBefore(" ").StringBefore("\t");
            peptide.AddProtein(proteinName);
          }
        }
      }

      return result;
    }
  }
}