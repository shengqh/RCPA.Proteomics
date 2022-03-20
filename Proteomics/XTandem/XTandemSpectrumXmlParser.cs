using RCPA.Gui;
using RCPA.Proteomics.Modification;
using RCPA.Proteomics.Summary;
using RCPA.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;

namespace RCPA.Proteomics.XTandem
{
  public class XTandemSpectrumXmlParser : ProgressClass, ISpectrumParser
  {
    private XmlHelper xmlHelper = null;

    private Protease protease;

    private Regex modificationRegex = new Regex(@"([+\-0-9\.]+)@(\S)");

    private Dictionary<char, double> staticModifications = new Dictionary<char, double>();

    private Dictionary<char, double> dynamicModifications = new Dictionary<char, double>();

    private Dictionary<string, char> dynamicModificationChars = new Dictionary<string, char>();

    public int MaxMissCleavageSites = 0;

    public XTandemSpectrumXmlParser(ITitleParser parser)
    {
      if (parser == null)
      {
        throw new ArgumentNullException("parser");
      }

      this.TitleParser = parser;
    }

    public XTandemSpectrumXmlParser() : this(new DefaultTitleParser()) { }

    class ModificationItem : IComparable<ModificationItem>
    {
      public string Type { get; set; }
      public int At { get; set; }
      public double Modified { get; set; }

      #region IComparable<ModificationItem> Members

      public int CompareTo(ModificationItem other)
      {
        return -this.At.CompareTo(other.At);
      }

      #endregion
    }

    public static string GetTitleSample(string xmlFilename)
    {
      XmlDocument doc = new XmlDocument();
      doc.Load(xmlFilename);

      var xmlHelper = new XmlHelper(doc);

      XmlNode root = doc.DocumentElement;

      List<XmlNode> groupNodes = xmlHelper.GetChildrenByNameAndAttribute(root, "group", "type", "model");
      if (groupNodes.Count > 0)
      {
        XmlNode spectrumNode = xmlHelper.GetFirstChildByNameAndAttribute(groupNodes[0], "group", "label", "fragment ion mass spectrum");
        XmlNode labelNode = xmlHelper.GetFirstChildByNameAndAttribute(spectrumNode, "note", "label", "Description");
        return labelNode.InnerText.Trim();
      }

      return string.Empty;
    }

    /// <summary>
    /// 
    /// Get top one peptide list from xtandem xml file
    /// 
    /// </summary>
    /// <param name="fileName">xtandem xml filename</param>
    /// <returns>List of IIdentifiedSpectrum</returns>
    public List<IIdentifiedSpectrum> ReadFromFile(string fileName)
    {
      string sourceFilename = GetSourceFile(fileName);

      List<IIdentifiedSpectrum> result = new List<IIdentifiedSpectrum>();

      XmlDocument doc = new XmlDocument();
      doc.Load(fileName);

      this.xmlHelper = new XmlHelper(doc);

      XmlNode root = doc.DocumentElement;

      Match mSource = Regex.Match(sourceFilename, @"(.+)\.(?:RAW)", RegexOptions.IgnoreCase);
      if (mSource.Success)
      {
        sourceFilename = mSource.Groups[1].Value;
      }
      else
      {
        mSource = Regex.Match(sourceFilename, @"(.+?)\.");
        if (mSource.Success)
        {
          sourceFilename = mSource.Groups[1].Value;
        }
      }

      XmlNode parameters = xmlHelper.GetFirstChildByNameAndAttribute(root, "group", "label", "input parameters");
      ParseParameters(parameters);

      int pos = sourceFilename.LastIndexOfAny(new char[] { '/', '\\' });

      string rawFileName;
      if (pos > 0)
      {
        rawFileName = sourceFilename.Substring(pos + 1);
      }
      else
      {
        rawFileName = sourceFilename;
      }
      rawFileName = FileUtils.ChangeExtension(rawFileName, "");

      List<XmlNode> groupNodes = xmlHelper.GetChildrenByNameAndAttribute(root, "group", "type", "model");
      foreach (XmlNode groupNode in groupNodes)
      {
        Dictionary<string, IIdentifiedPeptide> pepmap = new Dictionary<string, IIdentifiedPeptide>();

        IIdentifiedSpectrum spectrum = new IdentifiedSpectrum();

        List<XmlNode> proteins = xmlHelper.GetChildren(groupNode, "protein");

        foreach (XmlNode proteinNode in proteins)
        {
          XmlNode domainNode = xmlHelper.GetValidChild(xmlHelper.GetValidChild(proteinNode, "peptide"), "domain");

          int numMissedCleavages = int.Parse(domainNode.Attributes["missed_cleavages"].Value);

          string preSeq = domainNode.Attributes["pre"].Value;
          if (preSeq.Equals("["))
          {
            preSeq = "-";
          }

          string postSeq = domainNode.Attributes["post"].Value;
          if (postSeq.Equals("]"))
          {
            postSeq = "-";
          }

          StringBuilder pepSeqSB = new StringBuilder(domainNode.Attributes["seq"].Value);

          int start = int.Parse(domainNode.Attributes["start"].Value);
          int end = int.Parse(domainNode.Attributes["end"].Value);

          List<XmlNode> modifications = xmlHelper.GetChildren(domainNode, "aa");
          if (modifications.Count > 0)
          {
            List<ModificationItem> items = new List<ModificationItem>();
            foreach (XmlNode modification in modifications)
            {
              int at = int.Parse(modification.Attributes["at"].Value);
              if (at < start || at > end)
              {
                continue;
              }

              ModificationItem item = new ModificationItem();
              item.Type = modification.Attributes["type"].Value;
              item.At = at;
              item.Modified = MyConvert.ToDouble(modification.Attributes["modified"].Value);
              if (!staticModifications.ContainsKey(item.Type[0]))
              {
                items.Add(item);
              }
            }

            spectrum.Modifications = "";
            if (items.Count > 0)
            {
              items.Sort((m1, m2) => m1.At - m2.At);

              var mod = "";
              foreach (ModificationItem item in items)
              {
                mod = mod + MyConvert.Format(",{0}({1:0.0000})", item.Type, item.Modified);
              }
              spectrum.Modifications = mod.Substring(1);

              items.Sort((m1, m2) => m2.At - m1.At);
              foreach (ModificationItem item in items)
              {
                var key = GetModifiedKey(item.Modified);
                if (!dynamicModificationChars.ContainsKey(key))
                {
                  AddDynamicModificationChar(key);
                }
                char modificationChar = dynamicModificationChars[key];
                pepSeqSB.Insert(item.At - start + 1, modificationChar.ToString());
              }

              spectrum.Modifications = mod.Substring(1);
            }
          }

          StringBuilder sb = new StringBuilder();
          sb.Append(preSeq.Substring(preSeq.Length - 1));
          sb.Append(".");
          sb.Append(pepSeqSB.ToString());
          sb.Append(".");
          sb.Append(postSeq[0]);

          string pepSeq = sb.ToString();

          if (!pepmap.ContainsKey(pepSeq))
          {
            IdentifiedPeptide pep = new IdentifiedPeptide(spectrum);
            pep.Sequence = pepSeq;
            pepmap[pepSeq] = pep;
            spectrum.TheoreticalMH = MyConvert.ToDouble(domainNode.Attributes["mh"].Value);
            spectrum.Score = MyConvert.ToDouble(domainNode.Attributes["hyperscore"].Value);

            double nextScore = MyConvert.ToDouble(domainNode.Attributes["nextscore"].Value);
            spectrum.DeltaScore = (spectrum.Score - nextScore) / spectrum.Score;
            spectrum.NumMissedCleavages = int.Parse(domainNode.Attributes["missed_cleavages"].Value);
          }

          var noteNode = xmlHelper.GetValidChild(proteinNode, "note");
          string proteinName = noteNode.InnerText.StringBefore(" ").StringBefore("\t");
          pepmap[pepSeq].AddProtein(proteinName);
        }

        if (spectrum.Peptides.Count > 0)
        {
          spectrum.DigestProtease = protease;
          result.Add(spectrum);

          spectrum.Query.QueryId = int.Parse(groupNode.Attributes["id"].Value);
          spectrum.ExperimentalMH = MyConvert.ToDouble(groupNode.Attributes["mh"].Value);
          spectrum.ExpectValue = MyConvert.ToDouble(groupNode.Attributes["expect"].Value);

          XmlNode spectrumNode = xmlHelper.GetFirstChildByNameAndAttribute(groupNode, "group", "label", "fragment ion mass spectrum");
          XmlNode labelNode = xmlHelper.GetFirstChildByNameAndAttribute(spectrumNode, "note", "label", "Description");
          string title = labelNode.InnerText.Trim();
          if (title.StartsWith("RTINSECONDS"))
          {
            var rtvalue = title.StringAfter("=").StringBefore(" ").StringBefore("-");
            spectrum.Query.FileScan.RetentionTime = double.Parse(rtvalue);
            title = title.StringAfter(" ").Trim();
          }

          SequestFilename sf = this.TitleParser.GetValue(title);
          if (sf.Experimental == null || sf.Experimental.Length == 0)
          {
            sf.Experimental = sourceFilename;
          }
          spectrum.Query.FileScan.LongFileName = sf.LongFileName;
          if (sf.RetentionTime > 0 && spectrum.Query.FileScan.RetentionTime == 0)
          {
            spectrum.Query.FileScan.RetentionTime = sf.RetentionTime;
          }

          spectrum.Query.Charge = int.Parse(groupNode.Attributes["z"].Value);
          spectrum.Query.Title = title;
        }
      }
      return result;
    }

    private string GetModifiedKey(double mass)
    {
      return MyConvert.Format("{0:0.0000}", mass);
    }

    private void AddDynamicModificationChar(string key)
    {
      var curChar = ModificationConsts.MODIFICATION_CHAR[dynamicModificationChars.Count + 1];
      dynamicModificationChars[key] = curChar;
    }

    private void ParseParameters(XmlNode parameters)
    {
      staticModifications.Clear();
      ParseModifications(parameters, "residue, modification mass", staticModifications);
      ParseModifications(parameters, "refine, modification mass", staticModifications);

      dynamicModifications.Clear();
      ParseModifications(parameters, "residue, potential modification mass", dynamicModifications);
      ParseModifications(parameters, "refine, potential modification mass", dynamicModifications);

      List<char> aminoacids = new List<char>(dynamicModifications.Keys);
      aminoacids.Sort();

      foreach (var aa in aminoacids)
      {
        string key = GetModifiedKey(dynamicModifications[aa]);
        if (!dynamicModificationChars.ContainsKey(key))
        {
          AddDynamicModificationChar(key);
        }
      }

      ParseProtease(parameters);

      ParseMaxMissCleavageSites(parameters);
    }

    private void ParseMaxMissCleavageSites(XmlNode parameters)
    {
      XmlNode node = xmlHelper.GetFirstChildByNameAndAttribute(parameters, "note", "label", "scoring, maximum missed cleavage sites");
      MaxMissCleavageSites = int.Parse(node.InnerText);
    }

    private void ParseProtease(XmlNode parameters)
    {
      XmlNode node = xmlHelper.GetFirstChildByNameAndAttribute(parameters, "note", "label", "protein, cleavage site");

      Match m = Regex.Match(node.InnerText, @"\[(.+)\]\|\{(.+)\}");

      protease = ProteaseManager.FindOrCreateProtease("XTANDEM_PROTEASE", true, m.Groups[1].Value, m.Groups[2].Value);
    }

    private void ParseModifications(XmlNode parameters, string attrValue, Dictionary<char, double> map)
    {
      XmlNode node = xmlHelper.GetFirstChildByNameAndAttribute(parameters, "note", "label", attrValue);

      if (node == null)
      {
        return;
      }

      Match m = modificationRegex.Match(node.InnerText);
      while (m.Success)
      {
        map[m.Groups[2].Value[0]] = MyConvert.ToDouble(m.Groups[1].Value);
        m = m.NextMatch();
      }
    }

    public static string GetSourceFile(string xmlFilename)
    {
      using (StreamReader sr = new StreamReader(xmlFilename))
      {
        int count = 0;
        string line;
        Regex reg = new Regex("Models from '(.+)'", RegexOptions.IgnoreCase);
        while ((line = sr.ReadLine()) != null && count < 10)
        {
          Match m = reg.Match(line);
          if (m.Success)
          {
            return m.Groups[1].Value;
          }
          count++;
        }

        return null;
      }
    }

    public SearchEngineType Engine
    {
      get { return SearchEngineType.XTandem; }
    }

    public ITitleParser TitleParser { get; set; }
  }
}
