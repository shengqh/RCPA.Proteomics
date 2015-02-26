using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RCPA.Proteomics.Summary;
using System.Xml.Linq;
using RCPA.Gui;
using RCPA.Proteomics.PeptideProphet;
using System.Text.RegularExpressions;
using RCPA.Proteomics.Modification;

namespace RCPA.Proteomics.MzIdent
{
  public abstract class AbstractMzIdentParser : ProgressClass, ISpectrumParser
  {
    public abstract SearchEngineType Engine { get; }

    public ITitleParser TitleParser { get; set; }

    public AbstractMzIdentParser(ITitleParser parser)
    {
      this.TitleParser = parser;
    }

    public AbstractMzIdentParser()
    {
      this.TitleParser = new DefaultTitleParser();
    }

    public Dictionary<string, MzIdentModificationDefinitionItem> ParseSearchModificationMap(XElement mods)
    {
      var lst = (from ele in mods.FindElements("SearchModification")
                 let isFixed = bool.Parse(ele.Attribute("fixedMod").Value)
                 where !isFixed
                 let cvparam = ele.FindElement("cvParam")
                 select new MzIdentModificationDefinitionItem()
                 {
                   IsFixed = isFixed,
                   MassDelta = double.Parse(ele.Attribute("massDelta").Value),
                   CvRef = cvparam.Attribute("cvRef").Value,
                   Accession = cvparam.Attribute("accession").Value,
                   Name = cvparam.Attribute("name").Value
                 }).GroupBy(m => m.Accession).ToList().ConvertAll(m => m.First()).OrderBy(m => m.Accession).ToList();

      for (int i = 0; i < lst.Count; i++)
      {
        lst[i].ModificationChar = ModificationConsts.MODIFICATION_CHAR[i + 1].ToString();
      }

      return lst.ToDictionary(m => m.Accession);
    }

    public MzIdentModificationItem ParseModification(XElement mod, Dictionary<string, MzIdentModificationDefinitionItem> map)
    {
      var cvparam = mod.FindElement("cvParam");
      var accession = cvparam.Attribute("accession").Value;
      if (map.ContainsKey(accession))
      {
        var result = new MzIdentModificationItem();
        result.Location = int.Parse(mod.Attribute("location").Value);
        result.Item = map[accession];
        return result;
      }
      return null;
    }

    private static Regex cutReg = new Regex(@"\(\?<=\[(.+?)\]\)");
    private static Regex notCutReg = new Regex(@"\(\?\!(.+?)\)");
    /// <summary>
    ///<Enzymes independent="false">
    ///  <Enzyme id="ENZ_1" cTermGain="OH" nTermGain="H" missedCleavages="1" minDistance="1" semiSpecific="false">
    ///    <SiteRegexp>(?&lt;=[KR])</SiteRegexp>
    ///    <EnzymeName><cvParam cvRef="MS" accession="MS:1001313" name="Trypsin/P" value=""/></EnzymeName>
    ///  </Enzyme>
    ///</Enzymes>
    /// </summary>
    /// <param name="enzyme"></param>
    /// <returns></returns>
    public Protease ParseProtease(XElement enzyme)
    {
      var id = enzyme.Attribute("id").Value;
      var name = enzyme.FindElement("EnzymeName").FindElement("cvParam").Attribute("name").Value;
      if (ProteaseManager.Registered(name))
      {
        return ProteaseManager.GetProteaseByName(name);
      }

      var siteEle = enzyme.FindElement("SiteRegexp");
      if (siteEle != null)
      {
        string cut = string.Empty;
        string notCut = string.Empty;
        var sitereg = siteEle.Value;
        var cutM = cutReg.Match(sitereg);
        if (cutM.Success)
        {
          cut = cutM.Groups[1].Value;
        }
        var notCutM = notCutReg.Match(sitereg);
        if (notCutM.Success)
        {
          notCut = notCutM.Groups[1].Value;
        }
        return new Protease(name, true, cut, notCut) { Id = id };
      }

      return null;
    }

    public List<Protease> ParseEnzymes(XElement enzyme)
    {
      return (from enz in enzyme.FindElements("Enzyme")
              let protease = ParseProtease(enz)
              where protease != null
              select protease).ToList();
    }

    #region IFileReader<List<IIdentifiedSpectrum>> Members

    public List<IIdentifiedSpectrum> ReadFromFile(string fileName)
    {
      XElement root = XElement.Load(fileName);
      var name = root.FindElement("AnalysisSoftwareList").
        FindElement("AnalysisSoftware").
        FindElement("SoftwareName").
        FindElement("cvParam").Attribute("name").Value;

      //parsing identification protocol first
      var protocols = root.FindElement("AnalysisProtocolCollection");
      var sip = protocols.FindElement("SpectrumIdentificationProtocol");
      var modMap = ParseSearchModificationMap(sip.FindElement("ModificationParams"));
      var proteases = ParseEnzymes(sip.FindElement("Enzymes"));
      var protease = proteases.FirstOrDefault();

      //parsing sequence collection, including protein<->peptide map
      var seqs = root.FindElement("SequenceCollection");
      var proteinMap = (from ele in seqs.FindElements("DBSequence")
                        let id = ele.Attribute("id").Value
                        let accession = ele.Attribute("accession").Value
                        let db = ele.Attribute("searchDatabase_ref").Value
                        select new { Id = id, Accession = accession, DB = db }).ToDictionary(m => m.Id);

      var peptideMap = (from ele in seqs.FindElements("Peptide")
                        let id = ele.Attribute("id").Value
                        let seq = ele.FindElement("PeptideSequence").Value
                        let mods = (from modEle in ele.FindElements("Modification")
                                    let mod = ParseModification(modEle, modMap)
                                    where mod != null
                                    orderby mod.Location descending
                                    select mod).ToArray()
                        let numMiss = protease == null?0:protease.GetMissCleavageSiteCount(seq)
                        select new { Id = id, PureSequence = seq, Modifications = mods, Sequence = GetModifiedSequence(seq, mods) , NumMissCleavage = numMiss}).ToDictionary(m => m.Id);

      var peptideEvidenceMap = (from g in
                                  (from ele in seqs.FindElements("PeptideEvidence")
                                   select new
                                   {
                                     Id = ele.Attribute("id").Value,
                                     PeptideRef = ele.Attribute("peptide_ref").Value,
                                     DbRef = ele.Attribute("dBSequence_ref").Value,
                                     Pre = ele.Attribute("pre").Value,
                                     Post = ele.Attribute("post").Value
                                   }).GroupBy(m => m.Id)
                                select g.First()).ToDictionary(m => m.Id);

      //now parsing data
      var data = root.FindElement("DataCollection");

      var result = new List<IIdentifiedSpectrum>();
      var analysisData = data.FindElement("AnalysisData");
      var idList = analysisData.FindElement("SpectrumIdentificationList");
      foreach (var sir in idList.FindElements("SpectrumIdentificationResult"))
      {
        var spectrum = new IdentifiedSpectrum();
        result.Add(spectrum);

        var spectrumId = sir.Attribute("spectrumID").Value;

        var sirCvParams = GetCvParams(sir);
        string value;
        if (sirCvParams.TryGetValue("MS:1000796", out value))
        {
          spectrum.Query.FileScan = TitleParser.GetValue(value);
        }
        else
        {
          spectrum.Query.FileScan.Experimental = spectrumId;
        }

        if (sirCvParams.TryGetValue("MS:1001115", out value))
        {
          spectrum.Query.FileScan.FirstScan = int.Parse(value);
        }

        bool bFirst = true;
        foreach (var sit in sir.FindElements("SpectrumIdentificationItem"))
        {
          if (!sit.Attribute("rank").Value.Equals("1"))
          {
            break;
          }

          if (bFirst) //only parse score once
          {
            spectrum.Charge = int.Parse(sit.Attribute("chargeState").Value);
            spectrum.TheoreticalMH = PrecursorUtils.MzToMH(double.Parse(sit.Attribute("calculatedMassToCharge").Value), spectrum.Charge, true);
            spectrum.ExperimentalMH = PrecursorUtils.MzToMH(double.Parse(sit.Attribute("experimentalMassToCharge").Value), spectrum.Charge, true);

            var cvParams = GetCvParams(sit);
            if (cvParams.TryGetValue("MS:1001121", out value))
            {
              spectrum.MatchedIonCount = int.Parse(value);
            }

            if (cvParams.TryGetValue("MS:1001362", out value))
            {
              spectrum.TheoreticalIonCount = int.Parse(value) + spectrum.MatchedIonCount;
            }

            ParseScore(spectrum, cvParams);

            bFirst = false;
          }

          var peptide = new IdentifiedPeptide(spectrum);
          var pep_ref = sit.Attribute("peptide_ref").Value;
          var pep = peptideMap[pep_ref];
          spectrum.Modifications = (from m in pep.Modifications
                                    select string.Format("{0}:{1}", m.Location, m.Item.Name)).Reverse().Merge(",");
          spectrum.NumMissedCleavages = pep.NumMissCleavage;

          foreach (var per in sit.FindElements("PeptideEvidenceRef"))
          {
            var pe_ref = per.Attribute("peptideEvidence_ref").Value;
            var pe = peptideEvidenceMap[pe_ref];
            peptide.Sequence = pe.Pre + "." + pep.Sequence + "." + pe.Post;

            var protein = proteinMap[pe.DbRef];
            peptide.AddProtein(protein.Accession);
          }
        }
      }

      return result;
    }

    private string GetModifiedSequence(string seq, MzIdentModificationItem[] mods)
    {
      foreach (var mod in mods)
      {
        if (!mod.Item.IsFixed)
        {
          seq = seq.Insert(mod.Location, mod.Item.ModificationChar);
        }
      }

      return seq;
    }

    private static Dictionary<string, string> GetCvParams(XElement sit)
    {
      var cvParams = (from cvParam in sit.FindElements("cvParam")
                      select new
                      {
                        Accession = cvParam.Attribute("accession").Value,
                        Value = cvParam.Attribute("value").Value
                      }).ToDictionary(m => m.Accession, m => m.Value);
      return cvParams;
    }

    protected abstract void ParseScore(IdentifiedSpectrum spectrum, Dictionary<string, string> cvParams);

    #endregion
  }
}
