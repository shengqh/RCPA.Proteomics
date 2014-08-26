using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using RCPA.Gui;
using RCPA.Proteomics.Summary;
using RCPA.Utils;
using RCPA.Proteomics.Mascot;

namespace RCPA.Proteomics.PFind
{
  public class PFindSpectrumParser : ProgressClass
  {
    public const string MODIFICATION_CHAR = " *#@&^%$~1234567890";

    private readonly Regex keyValueRegex = new Regex(@"^(.+?)=(.*)");

    protected ITitleParser parser;

    private Dictionary<string, char> ModificationCharMap = new Dictionary<string, char>();

    public PFindSpectrumParser(ITitleParser parser)
    {
      this.parser = parser;
    }

    public PFindSpectrumParser()
      : this(new DefaultTitleParser())
    {
    }

    public Dictionary<string, string> ParseSection(StreamReader sr, string sectionName)
    {
      string secName = "[" + sectionName + "]";
      string line;
      while ((line = sr.ReadLine()) != null)
      {
        if (line.Trim().Equals(secName))
        {
          break;
        }
      }

      if (line == null)
      {
        throw new Exception("Cannot find " + sectionName + " information when parsing PFind result file.");
      }

      var result = new Dictionary<string, string>();

      while (!sr.EndOfStream && sr.Peek() != '[')
      {
        line = sr.ReadLine();

        if (line.Trim().Length == 0)
        {
          continue;
        }

        Match m = this.keyValueRegex.Match(line);
        if (m.Success)
        {
          result[m.Groups[1].Value] = m.Groups[2].Value.Trim();
        }
      }

      return result;
    }

    public PFindModification ParseModification(Dictionary<string, string> parameters)
    {
      var result = new PFindModification();

      result.StaticModification.Modification = parameters["Fixed_modifications"];
      result.DynamicModification.Modification = parameters["Variable_modifications"];

      return result;
    }

    protected void AssignModification(IIdentifiedSpectrum mph, Dictionary<int, string> modifications, PFindModification mm)
    {
      var mods = (from m in modifications
                  select m.Value).Distinct().ToArray();

      mph.Modifications = string.Join("; ", mods);
    }

    protected void ModifySequence(IdentifiedPeptide mp, Dictionary<int, string> modifications, PFindModification mm)
    {
      var positions = (from m in modifications
                       where !mm.StaticModification.ModificationMap.ContainsValue(m.Value)
                       orderby m.Key descending
                       select m.Key).ToList();

      foreach (var pos in positions)
      {
        string mod = modifications[pos];
        if (this.ModificationCharMap.ContainsKey(mod))
        {
          if (pos == mp.Sequence.Length)
          {
            mp.Sequence = mp.Sequence + this.ModificationCharMap[mod].ToString();
          }
          else
          {
            mp.Sequence = mp.Sequence.Insert(pos + 1, this.ModificationCharMap[mod].ToString());
          }
        }
        else
        {
          throw new Exception(MyConvert.Format("Cannot find dynamic modification {0} definition", mod));
        }
      }
    }

    public string GetSourceFile(string filename)
    {
      using (var sr = new StreamReader(filename))
      {
        string line;
        while ((line = sr.ReadLine()) != null)
        {
          if (line.StartsWith("InputPath="))
          {
            return new DirectoryInfo(line.Substring(10)).Name;
          }
        }
      }

      return "";
    }

    /// <summary>
    /// 
    /// Get top one peptide list from PFind proteins file
    /// 
    /// </summary>
    /// <param name="filename">pFind proteins file</param>
    /// <returns>List of IIdentifiedSpectrum</returns>
    public List<IIdentifiedSpectrum> ParsePeptides(string filename)
    {
      Dictionary<int, List<IIdentifiedSpectrum>> queryPepMap = ParsePeptides(filename, 1);

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
    /// Get the query/peptide map from pFind proteins file<.
    /// 
    /// </summary>
    /// <param name="filename">pFind proteins file</param>
    /// <param name="minRank">Minimum rank of peptide identified in same spectrum</param>
    /// <returns>Query/peptide map</returns>
    public Dictionary<int, List<IIdentifiedSpectrum>> ParsePeptides(string filename, int minRank)
    {
      return ParsePeptides(filename, minRank, 0.0);
    }

    /// <summary>
    /// 
    /// Get the query/peptide map from mascot dat file.
    /// 
    /// </summary>
    /// <param name="filename">pFind proteins file</param>
    /// <param name="minRank">Minimum rank of peptide identified in same spectrum</param>
    /// <param name="minScore">Minimum score of peptide identified in same spectrum</param>
    /// <returns>Query/peptide map</returns>
    public Dictionary<int, List<IIdentifiedSpectrum>> ParsePeptides(string filename, int minRank, double minScore)
    {
      var result = new Dictionary<int, List<IIdentifiedSpectrum>>();

      var sourceDir = GetSourceFile(filename);

      using (var sr = new StreamReader(filename))
      {
        var parameters = ParseSection(sr, "Search");

        var mm = ParseModification(parameters);

        foreach (var mod in mm.DynamicModification)
        {
          if (!this.ModificationCharMap.ContainsKey(mod.Modification))
          {
            this.ModificationCharMap[mod.Modification] = MODIFICATION_CHAR[this.ModificationCharMap.Count + 1];
          }
        }

        var headers = ParseSection(sr, "Total");

        var queryCount = int.Parse(headers["Spectra"]);

        Progress.SetRange(1, queryCount);
        for (int queryId = 1; queryId <= queryCount; queryId++)
        {
          if (Progress.IsCancellationPending())
          {
            throw new UserTerminatedException();
          }

          Progress.SetPosition(queryId);

          var speName = MyConvert.Format("Spectrum{0}", queryId);
          var peptideSection = ParseSection(sr, speName);

          int candidateCount = int.Parse(peptideSection["ValidCandidate"]);
          if (candidateCount == 0)
          {
            continue;
          }

          var expMH = MyConvert.ToDouble(peptideSection["MH"]);
          var expMz = MyConvert.ToDouble(peptideSection["MZ"]);
          var charge = int.Parse(peptideSection["Charge"]);

          var iPeps = new List<IIdentifiedSpectrum>();
          result[queryId] = iPeps;

          IIdentifiedSpectrum lastHit = null;
          int rank = 0;
          for (int k = 1; k <= candidateCount; k++)
          {
            string key = "NO" + k.ToString();
            var scoreKey = key + "_Score";
            if (!peptideSection.ContainsKey(scoreKey))
            {
              if (null != lastHit)
              {
                lastHit.DeltaScore = 1.0;
              }
              break;
            }

            double score = MyConvert.ToDouble(peptideSection[scoreKey]);
            if (score < minScore || score == 0.0)
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

              mphit.Rank = rank;
              mphit.Score = score;
              mphit.ExpectValue = MyConvert.ToDouble(peptideSection[key + "_EValue"]);

              var mhkey = key + "_MH";
              if (peptideSection.ContainsKey(mhkey))
              {
                mphit.TheoreticalMH = MyConvert.ToDouble(peptideSection[mhkey]);
              }
              else
              {
                mphit.TheoreticalMH = MyConvert.ToDouble(peptideSection[key + "_Mass"]);
              }

              var micKey = key + "_Matched_Peaks";
              if (peptideSection.ContainsKey(micKey))
              {
                mphit.MatchedIonCount = int.Parse(peptideSection[micKey]);
                mphit.MatchedTIC = MyConvert.ToDouble(peptideSection[key + "_Matched_Intensity"]);
              }

              var misKey = key + "_MissCleave";
              if (peptideSection.ContainsKey(misKey))
              {
                mphit.NumMissedCleavages = int.Parse(peptideSection[misKey]);
              }
              mphit.ExperimentalMH = expMH;
              mphit.DeltaScore = 1.0;

              mphit.Query.QueryId = queryId;
              mphit.Query.ObservedMz = expMz;
              mphit.Query.Charge = charge;
              //mphit.Query.MatchCount = queryItem.MatchCount;

              lastHit = mphit;
            }

            var mp = new IdentifiedPeptide(mphit);
            mp.Sequence = peptideSection[key + "_SQ"];

            string modificationPos = peptideSection[key + "_Modify_Pos"];
            string modificationName = peptideSection[key + "_Modify_Name"];

            Dictionary<int, string> modifications = GetModifications(modificationPos, modificationName);

            ModifySequence(mp, modifications, mm);
            AssignModification(mphit, modifications, mm);

            string proteins = peptideSection[key + "_Proteins"];
            var parts = proteins.Split(',');
            for (int i = 1; i < parts.Count(); i++)
            {
              mp.AddProtein(parts[i]);
            }

            if (!bSameRank)
            {
              iPeps.Add(mphit);
            }
          }

          var title = new FileInfo(peptideSection["Input"]).Name;

          SequestFilename sf = this.parser.GetValue(title);
          sf.Charge = charge;

          if (sf.Experimental == null || sf.Experimental.Length == 0)
          {
            sf.Experimental = sourceDir;
          }

          foreach (IIdentifiedSpectrum mp in iPeps)
          {
            mp.Query.Title = title;
            mp.Query.FileScan.LongFileName = sf.LongFileName;
          }
        }
      }

      return result;
    }

    private Dictionary<int, string> GetModifications(string modificationPos, string modificationName)
    {
      Dictionary<int, string> result = new Dictionary<int, string>();

      var posParts = modificationPos.Split(',');
      var nameParts = modificationName.Split(',');
      for (int i = 1; i < posParts.Count() && i < nameParts.Count(); i++)
      {
        result[int.Parse(posParts[i])] = nameParts[i];
      }

      return result;
    }

    public static string GetTitleSample(string fileName)
    {
      using (StreamReader sr = new StreamReader(fileName))
      {
        string line;
        while ((line = sr.ReadLine()) != null)
        {
          if (line.StartsWith("Input="))
          {
            return line.Substring(6);
          }
        }
      }
      return string.Empty;
    }
  }
}