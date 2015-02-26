using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using RCPA.Proteomics.Summary;
using RCPA.Gui;
using System.Text.RegularExpressions;
using RCPA.Proteomics.Mascot;
using System.Text;
using RCPA.Proteomics.Modification;

namespace RCPA.Proteomics.MaxQuant
{
  public class MaxQuantPeptideTextReader : ProgressClass, IFileReader<List<IIdentifiedSpectrum>>
  {
    private char[] splits = { '\t' };

    public MaxQuantPeptideTextReader()
    { }

    private int bestLocalizationRawFileIndex;
    private int bestLocalizationScanNumberIndex;
    private int sequenceIndex;
    private int chargeIndex;
    //private int mzIndex;
    //private int mzDiffPPMIndex;
    private int localizationProbIndex;
    private int scoreDiffIndex;
    private int pepIndex;
    //private int ptmScoreIndex;
    private int mascotScoreIndex;
    //private int proteinsIndex;

    private Regex modReg = new Regex(@"(\(.+?\))");
    private Dictionary<string, string> modCharMap = new Dictionary<string, string>();

    public Dictionary<string, string> ModificationMap { get { return modCharMap; } }

    private int FindIndex(string[] parts, string name)
    {
      var result = Array.IndexOf(parts, name);

      if (result == -1)
      {
        throw new Exception(MyConvert.Format("Cannot find name {0} in MaxQuant peptide file header", name));
      }

      return result;
    }

    #region IFileReader<List<IIdentifiedSpectrum>> Members

    public List<IIdentifiedSpectrum> ReadFromFile(string filename)
    {
      if (!File.Exists(filename))
      {
        throw new FileNotFoundException("File not exist : " + filename);
      }

      var result = new List<IIdentifiedSpectrum>();

      var fi = new FileInfo(filename);

      long fileSize = fi.Length;

      Progress.SetRange(0, fileSize);
      using (var br = new StreamReader(fi.OpenRead()))
      {
        String line = br.ReadLine();
        string[] parts = line.Split(splits);

        bestLocalizationRawFileIndex = FindIndex(parts, "Best Localization Raw File");
        bestLocalizationScanNumberIndex = FindIndex(parts, "Best Localization Scan Number");
        sequenceIndex = FindIndex(parts, "Modified Sequence");
        //mzIndex = Array.IndexOf(parts, "m/z");
        //mzDiffPPMIndex = Array.IndexOf(parts, "Mass Error [ppm]");
        chargeIndex = FindIndex(parts, "Charge");
        localizationProbIndex = FindIndex(parts, "Localization Prob");
        scoreDiffIndex = FindIndex(parts, "Score Diff");
        //ptmScoreIndex = Array.IndexOf(parts, "PTM Score");
        mascotScoreIndex = FindIndex(parts, "Mascot Score");
        pepIndex = FindIndex(parts, "PEP");
        //proteinsIndex = Array.IndexOf(parts, "Proteins");
        while ((line = br.ReadLine()) != null)
        {
          if (0 == line.Trim().Length)
          {
            break;
          }

          Progress.SetPosition(br.BaseStream.Position);

          result.Add(ParsePeptide(line));
        }
      }

      return result;
    }

    private IIdentifiedSpectrum ParsePeptide(string line)
    {
      IIdentifiedSpectrum result = new IdentifiedSpectrum();

      string[] parts = line.Split(splits);
      result.Query.FileScan.Experimental = parts[bestLocalizationRawFileIndex];
      result.Query.FileScan.FirstScan = int.Parse(parts[bestLocalizationScanNumberIndex]);
      result.Query.FileScan.LastScan = result.Query.FileScan.FirstScan;
      result.Query.FileScan.Charge = int.Parse(parts[chargeIndex]);
      result.Score = MyConvert.ToDouble(parts[mascotScoreIndex]);
      result.DeltaScore = MyConvert.ToDouble(parts[scoreDiffIndex]);
      result.ExpectValue = MyConvert.ToDouble(parts[pepIndex]);
      result.PValue = MyConvert.ToDouble(parts[localizationProbIndex]);

      string seq = parts[sequenceIndex];

      seq = ReplaceModificationStringToChar(seq);
      seq = seq.Replace("_", "");

      Match m = modReg.Match(seq);
      if (m.Success)
      {
        while (m.Success)
        {
          string mod = m.Groups[1].Value;
          if (!modCharMap.ContainsKey(mod))
          {
            modCharMap[mod] = ModificationConsts.MODIFICATION_CHAR.Substring(modCharMap.Count + 1, 1);
          }
          m = m.NextMatch();
        }

        seq = ReplaceModificationStringToChar(seq);
      }

      result.NewPeptide().Sequence = seq;

      StringBuilder sb = new StringBuilder();
      foreach (var mod in modCharMap)
      {
        if (seq.Contains(mod.Value))
        {
          sb.Append(MyConvert.Format("{0}={1};", mod.Value, mod.Key));
        }
      }
      result.Modifications = sb.ToString();

      return result;
    }

    private string ReplaceModificationStringToChar(string seq)
    {
      foreach (var key in modCharMap.Keys)
      {
        seq = seq.Replace(key, modCharMap[key]);
      }
      return seq;
    }

    #endregion
  }
}