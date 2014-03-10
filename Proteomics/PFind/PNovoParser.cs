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
  public class PNovoParser : ProgressClass
  {
    public const string MODIFICATION_CHAR = " *#@&^%$~1234567890";

    private readonly Regex keyValueRegex = new Regex(@"^(.+?)=(.*)");

    protected ITitleParser parser;

    public Dictionary<string, char> ModificationCharMap { get; private set; }

    public PNovoParser(ITitleParser parser)
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
    /// Get top ten peptide list from pNovo file
    /// 
    /// </summary>
    /// <param name="filename">pNovo file</param>
    /// <returns>List of IIdentifiedSpectrum</returns>
    public List<IIdentifiedSpectrum> ParsePeptides(string filename)
    {
      return ParsePeptides(filename, 10);
    }

    /// <summary>
    /// 
    /// Get the query/peptide map from pNovo file
    /// 
    /// </summary>
    /// <param name="filename">pFind proteins file</param>
    /// <param name="maxRank">Maximum rank of peptide identified in same spectrum</param>
    /// <returns>Query/peptide map</returns>
    public List<IIdentifiedSpectrum> ParsePeptides(string filename, int maxRank)
    {
      return ParsePeptides(filename, maxRank, 0.0);
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
    public List<IIdentifiedSpectrum> ParsePeptides(string filename, int maxRank, double minScore)
    {
      var result = new List<IIdentifiedSpectrum>();

      SequestFilename sf = null;

      int charge = 2;
      double expmh = 0;
      using (var sr = new StreamReader(filename))
      {
        string line;
        while ((line = sr.ReadLine()) != null)
        {
          var parts = line.Split('\t');
          if (parts.Length <= 5)
          { //spectrum information
            var seqcount = Convert.ToInt32(parts.Last());
            if (seqcount == 0)
            {
              continue;
            }

            sf = parser.GetValue(parts[0]);
            expmh = MyConvert.ToDouble(parts[1]);
            charge = Convert.ToInt32(parts[2]);
          }
          else
          {
            int curIndex = Convert.ToInt32(parts[0]);

            if (curIndex <= maxRank)
            {
              var score = MyConvert.ToDouble(parts[2]);
              if (score < minScore)
              {
                continue;
              }

              var curSpectrum = new IdentifiedSpectrum();
              curSpectrum.Query.FileScan = sf;
              curSpectrum.Query.Charge = charge;
              curSpectrum.ExperimentalMH = expmh;
              curSpectrum.Score = score;
              result.Add(curSpectrum);

              IdentifiedPeptide pep = new IdentifiedPeptide(curSpectrum);
              pep.Sequence = ModifySequence(parts[9]);
              pep.Spectrum.TheoreticalMH = MyConvert.ToDouble(parts[11]);
              pep.Spectrum.Rank = curIndex;
            }
          }
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
          var parts = line.Split('\t');
          if (parts.Length <= 5 && parts.Length >= 3)
          {
            parser.GetValue(parts[0]);
            result++;
          }
        }
      }
      return result;
    }
  }
}
