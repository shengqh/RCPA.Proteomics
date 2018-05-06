using RCPA.Gui;
using RCPA.Proteomics.Modification;
using RCPA.Proteomics.Summary;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace RCPA.Proteomics.Mascot
{
  public class MascotDatSpectrumParser : ProgressClass, ISpectrumParser
  {
    public static readonly string DECOY_PREFIX = "REVERSED_";
    private string delimiter = "--gc0p4Jq0M2Yt08jU534c0p";
    private readonly Regex keyValueRegex = new Regex(@"^(.+?)=(.*)");

    private Regex queryIdRegex = new Regex(@"qmass(\d+)=(.+)");

    private readonly Regex peptideRegex =
      new Regex(
        @"^(?<MissCleavage>\d),(?<TheoreticalMass>[\d.]+),[\d.-]+,(?<MatchedIon>\d+),(?<Sequence>\S+?),\d+,(?<Modification>\d+),(?<Score>[\d.]+),\d+,\d,\d;(?<ProteinNames>.+)$");

    private readonly Regex precursorRegex = new Regex(@"([\d.]+),(\d)+");
    private readonly Regex precursorMrRegex = new Regex(@"([\d.]+),Mr");
    private readonly Regex proteinNameRegex = new Regex("\"(.+?)\":\\d+:(\\d+):");

    private readonly Regex termsRegex = new Regex(@"([A-Z\-]),([A-Z\-])");

    public Protease CurrentProtease { get; private set; }

    public MascotModification CurrentModifications { get; private set; }

    public Dictionary<string, string> CurrentParameters { get; private set; }

    public static string HomologyScoreKey = "HomologyScore";

    public MascotDatSpectrumParser(ITitleParser parser)
    {
      this.TitleParser = parser;
    }

    public MascotDatSpectrumParser() : this(new DefaultTitleParser()) { }

    protected Dictionary<string, string> ParseSection(StreamReader sr, string sectionName, bool required = true)
    {
      return ParseSection(sr, sectionName, null, required);
    }

    protected Dictionary<string, string> ParseSection(StreamReader sr, string sectionName, string lineRegexStr, bool required = true)
    {
      string line;

      SectionClass sc = new SectionClass(sectionName);

      while ((line = sr.ReadLine()) != null)
      {
        if (sc.IsStartLine(line))
        {
          break;
        }
      }

      if (line == null)
      {
        if (required)
        {
          throw new Exception("Cannot find " + sectionName + " information when parsing dat file.");
        }
        else
        {
          return new Dictionary<string, string>();
        }
      }

      Regex lineRegex = null;
      if (lineRegexStr != null && lineRegexStr.Length > 0)
      {
        lineRegex = new Regex(lineRegexStr);
      }

      var result = new Dictionary<string, string>();
      while ((line = sr.ReadLine()) != null)
      {
        if (IsSectionEnd(line))
        {
          break;
        }

        if (line.Trim().Length == 0)
        {
          continue;
        }

        if (lineRegex == null || lineRegex.Match(line).Success)
        {
          Match m = this.keyValueRegex.Match(line);
          if (m.Success)
          {
            result[m.Groups[1].Value] = m.Groups[2].Value.Trim();
          }
        }
      }
      return result;
    }

    private bool IsSectionEnd(string line)
    {
      return line.StartsWith(delimiter);
    }

    protected Protease ParseEnzyme(StreamReader sr)
    {
      SectionClass sc = new SectionClass("enzyme");
      string line;

      //find enzyme section
      while ((line = sr.ReadLine()) != null)
      {
        if (sc.IsStartLine(line))
        {
          break;
        }
      }

      string name = "";
      string cleavage = "";
      string uncleavage = "";
      bool cterm = false;
      bool semiSpecific = false;
      while ((line = sr.ReadLine()) != null)
      {
        if (IsSectionEnd(line))
        {
          break;
        }

        if (line.StartsWith("Title"))
        {
          name = line.Substring(line.IndexOf(':') + 1);
        }
        else if (line.StartsWith("Cleavage"))
        {
          cleavage = line.Substring(line.IndexOf(':') + 1);
        }
        else if (line.StartsWith("Restrict"))
        {
          uncleavage = line.Substring(line.IndexOf(':') + 1);
        }
        else if (line.StartsWith("Cterm"))
        {
          cterm = true;
        }
        else if (line.StartsWith("SemiSpecific"))
        {
          semiSpecific = true;
        }
      }

      return new Protease(name, cterm, cleavage, uncleavage) { IsSemiSpecific = semiSpecific };
    }

    //-102.091507,Acetyl (K)
    protected MascotModification ParseModification(Dictionary<string, string> parameters)
    {
      var result = new MascotModification();

      result.Parse(parameters);

      return result;
    }

    protected Dictionary<int, MascotQueryItem> ParseQueryItems(StreamReader sr, int queryCount, string prefix = "")
    {
      string line;

      SectionClass sc = new SectionClass(prefix + "summary");
      while ((line = sr.ReadLine()) != null)
      {
        if (sc.IsStartLine(line))
        {
          break;
        }
      }

      var result = new Dictionary<int, MascotQueryItem>();
      while ((line = sr.ReadLine()) != null)
      {
        if (IsSectionEnd(line))
        {
          break;
        }

        if (line.StartsWith("qmass"))
        {
          Match m = queryIdRegex.Match(line);

          var item = new MascotQueryItem();

          item.QueryId = Convert.ToInt32(m.Groups[1].Value);
          item.ExperimentalMass = MyConvert.ToDouble(m.Groups[2].Value);

          line = sr.ReadLine();
          Match precursor = this.precursorRegex.Match(line);
          if (precursor.Success)
          {
            item.Observed = MyConvert.ToDouble(precursor.Groups[1].Value);
            item.Charge = int.Parse(precursor.Groups[2].Value);
          }
          else
          {
            var pmr = this.precursorMrRegex.Match(line);
            item.Observed = MyConvert.ToDouble(pmr.Groups[1].Value);
            item.Charge = 0;
          }

          line = sr.ReadLine();
          if (line.StartsWith("qintensity"))
          {
            item.Intensity = MyConvert.ToDouble(GetValue(line));
            line = sr.ReadLine();
          }

          if (line.StartsWith("qmatch"))
          {
            item.MatchCount = Convert.ToInt32(GetValue(line));
            line = sr.ReadLine();
          }

          if (line.StartsWith("qplughole"))
          {
            item.HomologyScore = Convert.ToDouble(GetValue(line));
          }

          result[item.QueryId] = item;
        }
      }

      return result;
    }

    private static string GetValue(string line)
    {
      return line.Substring(line.IndexOf('=') + 1);
    }

    protected void AssignModification(IIdentifiedSpectrum mph, string modification, MascotModification mm)
    {
      foreach (char c in modification)
      {
        if (c != '0')
        {
          int ci = int.Parse(c.ToString()) - 1;
          string mod = mm.DynamicModification[ci].ToString();
          if (mph.Modifications == null || mph.Modifications.Length == 0)
          {
            mph.Modifications = mod;
          }
          else if (!mph.Modifications.Contains(mod))
          {
            mph.Modifications += "; " + mod;
          }
        }
      }
    }

    protected string ModifySequence(string seq, string modification)
    {
      for (int j = modification.Length - 2; j >= 0; j--)
      {
        if (modification[j] != '0')
        {
          seq = seq.Insert(j, ModificationConsts.MODIFICATION_CHAR.Substring(int.Parse(modification.Substring(j, 1)), 1));
        }
      }

      return seq;
    }

    public static string GetSourceFile(string filename)
    {
      using (var sr = new StreamReader(filename))
      {
        string line;
        while ((line = sr.ReadLine()) != null)
        {
          if (line.StartsWith("FILE="))
          {
            return line.Substring(5);
          }
        }
      }

      return "";
    }

    public static string GetTitleSample(string filename)
    {
      using (StreamReader sr = new StreamReader(filename))
      {
        string line;
        while ((line = sr.ReadLine()) != null)
        {
          if (line.StartsWith("title="))
          {
            return Uri.UnescapeDataString(line.Substring(6));
          }
        }
      }

      return string.Empty;
    }

    public Dictionary<string, string> ParseSection(string fileName, string sectionName, bool required = true)
    {
      using (StreamReader sr = new StreamReader(fileName))
      {
        return ParseSection(sr, sectionName, null, true);
      }
    }


    /// <summary>
    /// 
    /// Get top one peptide list from mascot dat file
    /// 
    /// </summary>
    /// <param name="fileName">Mascot dat filename</param>
    /// <returns>List of MascotPeptideHit</returns>
    public List<IIdentifiedSpectrum> ReadFromFile(string fileName)
    {
      Dictionary<int, List<IIdentifiedSpectrum>> queryPepMap = ParsePeptides(fileName, 1);

      var result = new List<IIdentifiedSpectrum>();

      foreach (var peps in queryPepMap.Values)
      {
        if (peps.Count > 0)
        {
          result.Add(peps[0]);
        }
      }

      return result;
    }

    /// <summary>
    /// 
    /// Get the query/peptide map from mascot dat file.
    /// 
    /// </summary>
    /// <param name="datFilename">Mascot dat filename</param>
    /// <param name="minRank">Minimum rank of peptide identified in same spectrum</param>
    /// <returns>Query/peptide map</returns>
    public Dictionary<int, List<IIdentifiedSpectrum>> ParsePeptides(string datFilename, int minRank)
    {
      return ParsePeptides(datFilename, minRank, 0.0);
    }

    /// <summary>
    /// 
    /// Get the query/peptide map from mascot dat file.
    /// 
    /// </summary>
    /// <param name="datFilename">Mascot dat filename</param>
    /// <param name="minRank">Minimum rank of peptide identified in same spectrum</param>
    /// <param name="minScore">Minimum score of peptide identified in same spectrum</param>
    /// <returns>Query/peptide map</returns>
    public Dictionary<int, List<IIdentifiedSpectrum>> ParsePeptides(string datFilename, int minRank, double minScore)
    {
      var result = DoParsePeptides(datFilename, minRank, minScore, false);
      var decoy = DoParsePeptides(datFilename, minRank, minScore, true);

      if (decoy.Count > 0)
      {
        foreach (var d in decoy)
        {
          if (d.Value.Count == 0)
          {
            continue;
          }

          List<IIdentifiedSpectrum> target;
          if (result.TryGetValue(d.Key, out target))
          {
            if (target.Count == 0 || target.First().Score < d.Value.First().Score)
            {
              result[d.Key] = d.Value;
            }
            else if (target.First().Score == d.Value.First().Score)
            {
              result[d.Key].AddRange(d.Value);
            }
          }
          else
          {
            result[d.Key] = d.Value;
          }
        }
      }

      return result;
    }

    public Dictionary<int, List<IIdentifiedSpectrum>> DoParsePeptides(string datFilename, int minRank, double minScore, bool isDecoy)
    {
      var result = new Dictionary<int, List<IIdentifiedSpectrum>>();

      Dictionary<string, string> headers;
      int queryCount;
      Dictionary<int, MascotQueryItem> queryItems;
      Dictionary<string, string> peptideSection;

      var prefix = isDecoy ? "decoy_" : "";

      using (var sr = new StreamReader(datFilename))
      {
        InitializeBoundary(sr);

        CurrentParameters = ParseSection(sr, "parameters");

        var hasDecoy = CurrentParameters.ContainsKey("DECOY") && CurrentParameters["DECOY"].Equals("1");

        if (!hasDecoy && isDecoy)
        {
          return result;
        }

        var masses = ParseSection(sr, "masses");

        CurrentModifications = ParseModification(masses);

        long curPos = sr.GetCharpos();

        CurrentProtease = ParseEnzyme(sr);

        sr.SetCharpos(curPos);

        headers = ParseSection(sr, "header");
        queryCount = int.Parse(headers["queries"]);

        queryItems = ParseQueryItems(sr, queryCount, prefix);
        peptideSection = ParseSection(sr, prefix + "peptides", !isDecoy);
      }

      string file = CurrentParameters["FILE"];
      if (file.StartsWith("File Name: "))
      {
        file = file.Substring(10).Trim();
      }
      string defaultExperimental = FileUtils.ChangeExtension(new FileInfo(file).Name, "");
      bool isPrecursorMonoisotopic = true;
      if (CurrentParameters.ContainsKey("MASS"))
      {
        isPrecursorMonoisotopic = CurrentParameters["MASS"].Equals("Monoisotopic");
      }

      using (var sr = new StreamReader(datFilename))
      {
        //Progress.SetRange(1, queryCount);
        for (int queryId = 1; queryId <= queryCount; queryId++)
        {
          if (Progress.IsCancellationPending())
          {
            throw new UserTerminatedException();
          }

          //Progress.SetPosition(queryId);

          MascotQueryItem queryItem = queryItems[queryId];

          var iPeps = new List<IIdentifiedSpectrum>();
          result[queryId] = iPeps;

          IIdentifiedSpectrum lastHit = null;
          int rank = 0;
          for (int k = 1; k <= 10; k++)
          {
            string key = "q" + queryId + "_p" + k;
            if (!peptideSection.ContainsKey(key))
            {
              if (null != lastHit)
              {
                lastHit.DeltaScore = 1.0;
              }
              break;
            }

            string line = peptideSection[key];
            if (line == null || line.Equals("-1"))
            {
              if (null != lastHit)
              {
                lastHit.DeltaScore = 1.0;
              }
              break;
            }

            Match mDetail = this.peptideRegex.Match(line);
            if (!mDetail.Success)
            {
              throw new Exception("Wrong format of peptides : " + line);
            }

            double score = MyConvert.ToDouble(mDetail.Groups["Score"].Value);
            if (score < minScore)
            {
              if (null != lastHit)
              {
                lastHit.DeltaScore = 1.0 - score / lastHit.Score;
              }
              break;
            }

            bool bSameRank = null != lastHit && score == lastHit.Score;
            if (!bSameRank)
            {
              if (null != lastHit)
              {
                lastHit.DeltaScore = 1.0 - score / lastHit.Score;
              }

              rank++;
              if (rank > minRank)
              {
                break;
              }
            }

            IIdentifiedSpectrum mphit;
            if (bSameRank)
            {
              mphit = lastHit;
            }
            else
            {
              mphit = new IdentifiedSpectrum();
              mphit.IsPrecursorMonoisotopic = isPrecursorMonoisotopic;

              mphit.Rank = rank;
              mphit.NumMissedCleavages = int.Parse(mDetail.Groups["MissCleavage"].Value);
              mphit.TheoreticalMass = MyConvert.ToDouble(mDetail.Groups["TheoreticalMass"].Value);
              mphit.ExperimentalMass = queryItem.ExperimentalMass;
              mphit.Score = score;
              mphit.ExpectValue = ExpectValueCalculator.Calc(mphit.Score, queryItem.MatchCount, 0.05);

              mphit.Query.QueryId = queryId;
              mphit.Query.ObservedMz = queryItem.Observed;
              mphit.Query.Charge = queryItem.Charge;
              mphit.Query.MatchCount = queryItem.MatchCount;
              if (queryItem.HomologyScore != 0)
              {
                mphit.Annotations[HomologyScoreKey] = queryItem.HomologyScore;
              }

              if (CurrentProtease.IsSemiSpecific)
              {
                mphit.NumProteaseTermini = 1;
              }

              lastHit = mphit;
            }

            var pureSeq = mDetail.Groups["Sequence"].Value;
            string modification = mDetail.Groups["Modification"].Value;
            var seq = ModifySequence(pureSeq, modification);
            AssignModification(mphit, modification, CurrentModifications);

            string proteins = mDetail.Groups["ProteinNames"].Value;
            Match proteinNameMatch = this.proteinNameRegex.Match(proteins);

            string key_terms = key + "_terms";
            if (!peptideSection.ContainsKey(key_terms))
            {
              throw new Exception("Mascot version is too old. It's not supported.");
            }

            string value_terms = peptideSection[key_terms];
            Match termsMatch = this.termsRegex.Match(value_terms);

            int numProteaseTermini = 0;
            while (proteinNameMatch.Success && termsMatch.Success)
            {
              var fullSeq = MyConvert.Format("{0}.{1}.{2}",
                                        termsMatch.Groups[1].Value,
                                        seq,
                                        termsMatch.Groups[2].Value);

              var name = proteinNameMatch.Groups[1].Value.Replace("/", "_");
              if (isDecoy)
              {
                name = DECOY_PREFIX + name;
              }

              bool findPeptide = false;
              for (int i = 0; i < mphit.Peptides.Count; i++)
              {
                if (mphit.Peptides[i].Sequence == fullSeq)
                {
                  mphit.Peptides[i].AddProtein(name);
                  findPeptide = true;
                  break;
                }
              }

              if (!findPeptide)
              {
                var mp = new IdentifiedPeptide(mphit);
                mp.Sequence = fullSeq;
                mp.AddProtein(name);

                if (CurrentProtease.IsSemiSpecific)
                {
                  int position = Convert.ToInt32(proteinNameMatch.Groups[2].Value);
                  int count = CurrentProtease.GetNumProteaseTermini(termsMatch.Groups[1].Value[0], pureSeq, termsMatch.Groups[2].Value[0], '-', position);
                  numProteaseTermini = Math.Max(numProteaseTermini, count);
                }
              }

              proteinNameMatch = proteinNameMatch.NextMatch();
              termsMatch = termsMatch.NextMatch();
            }

            if (CurrentProtease.IsSemiSpecific)
            {
              mphit.NumProteaseTermini = Math.Max(mphit.NumProteaseTermini, numProteaseTermini);
            }

            if (!bSameRank)
            {
              iPeps.Add(mphit);
            }
          }

          string query = "query" + queryId;

          Dictionary<string, string> querySection = ParseSection(sr, query);
          string title = Uri.UnescapeDataString(querySection["title"]);

          SequestFilename sf = this.TitleParser.GetValue(title);
          sf.Charge = queryItem.Charge;

          if (sf.Experimental == null || sf.Experimental.Length == 0)
          {
            sf.Experimental = defaultExperimental;
          }

          foreach (IIdentifiedSpectrum mp in iPeps)
          {
            mp.Query.Title = title;
            mp.Query.FileScan = sf;
          }
        }
      }

      return result;
    }

    private void InitializeBoundary(StreamReader sr)
    {
      string bkey = "boundary=";

      string line;
      while ((line = sr.ReadLine()) != null)
      {
        int pos = line.IndexOf(bkey);
        if (pos == -1)
        {
          continue;
        }

        delimiter = "--" + line.Substring(pos + bkey.Length);
        break;
      }
    }

    public SearchEngineType Engine
    {
      get { return SearchEngineType.MASCOT; }
    }

    public ITitleParser TitleParser { get; set; }
  }
}