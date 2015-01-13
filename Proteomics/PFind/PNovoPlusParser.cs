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
  public class PNovoPlusParser : ProgressClass
  {
    public const string MODIFICATION_CHAR = " *#@&^%$~1234567890";

    private readonly Regex keyValueRegex = new Regex(@"^(.+?)=(.*)");

    protected ITitleParser parser;

    public Dictionary<string, char> ModificationCharMap { get; private set; }

    public PNovoPlusParser(ITitleParser parser)
    {
      this.parser = parser;

      this.ModificationCharMap = new Dictionary<string, char>();
    }

    private Regex modReg = new Regex(@"([+\.0-9]+)");
    protected string ModifySequence(string source)
    {
      var m = modReg.Match(source);
      var result = source;
      while (m.Success)
      {
        var mod = m.Groups[1].Value;
        if (!ModificationCharMap.ContainsKey(mod))
        {
          ModificationCharMap[mod] = MODIFICATION_CHAR[ModificationCharMap.Count + 1];
        }

        result = result.Replace(mod, ModificationCharMap[mod].ToString());
        m = m.NextMatch();
      }

      return result;
    }

    /// <summary>
    /// 
    /// Get the query/peptide map from pNovo result.
    /// 
    /// </summary>
    /// <param name="filename">pNovo proteins file</param>
    /// <param name="minRank">Minimum rank of peptide identified in same spectrum</param>
    /// <param name="minScore">Minimum score of peptide identified in same spectrum</param>
    /// <returns>Query/peptide map</returns>
    public List<IIdentifiedSpectrum> ParsePeptides(string filename, int maxRank = 10, double minScore = 0.0)
    {
      var result = new List<IIdentifiedSpectrum>();

      SequestFilename sf = null;

      int curIndex = 0;
      using (var sr = new StreamReader(filename))
      {
        string line;
        while ((line = sr.ReadLine()) != null)
        {
          if (string.IsNullOrWhiteSpace(line))
          {
            continue;
          }

          if (line.StartsWith("S"))
          {
            var title = line.StringAfter("\t");
            sf = this.parser.GetValue(title);
            curIndex = 0;
            continue;
          }

          var parts = line.Split('\t');
          var score = MyConvert.ToDouble(parts[2]);
          if (score < minScore)
          {
            continue;
          }

          curIndex++;

          IIdentifiedSpectrum curSpectrum;
          if (curIndex == 1)
          {
            curSpectrum = new IdentifiedSpectrum();
            curSpectrum.Query.FileScan = sf;
            curSpectrum.Query.Charge = sf.Charge;
            curSpectrum.Score = score;
            curSpectrum.Rank = curIndex;
            result.Add(curSpectrum);
          }
          else if(score == result.Last().Score)
          {
            curSpectrum = result.Last();
          }
          else if(curIndex > maxRank)
          {
            continue;
          }
          else
          {
            curSpectrum = new IdentifiedSpectrum();
            curSpectrum.Query.FileScan = sf;
            curSpectrum.Query.Charge = sf.Charge;
            curSpectrum.Score = score;
            curSpectrum.Rank = curIndex;
            result.Add(curSpectrum);
          }

          IdentifiedPeptide pep = new IdentifiedPeptide(curSpectrum);
          pep.Sequence = ModifySequence(parts[1]);
        }
      }
      return result;
    }
  
    /// <summary>
    /// 获取用于Denovo的谱图总数。
    /// </summary>
    /// <param name="filename">pNovo proteins file</param>
    /// <returns>用于Denovo的谱图总数</returns>
    public int GetSpectrumCount(string filename)
    {
      var result = 0;

      using (var sr = new StreamReader(filename))
      {
        string line;
        while ((line = sr.ReadLine()) != null)
        {
          if (line.StartsWith("S"))
          {
            result++;
          }
        }
      }
      return result;
    }
  }
}
