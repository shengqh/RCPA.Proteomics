using RCPA.Gui;
using RCPA.Proteomics.Modification;
using RCPA.Proteomics.Summary;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace RCPA.Proteomics.MaxQuant
{
  public class MaxQuantPeptideTextReader : ProgressClass, IFileReader<List<IIdentifiedSpectrum>>
  {
    private char[] splits = { '\t' };

    public MaxQuantPeptideTextReader()
    { }

    private int bestLocalizationRawFileIndex;
    private int bestLocalizationScanNumberIndex;
    private int sequenceWindowIndex;
    private int modificationWindowIndex;
    private int chargeIndex;
    private int localizationProbIndex;
    private int scoreDiffIndex;
    private int pepIndex;
    private int scoreIndex;

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

        bestLocalizationRawFileIndex = FindIndex(parts, "Best localization raw file");
        bestLocalizationScanNumberIndex = FindIndex(parts, "Best localization scan number");
        sequenceWindowIndex = FindIndex(parts, "Sequence window");
        modificationWindowIndex = FindIndex(parts, "Modification window");
        chargeIndex = FindIndex(parts, "Charge");
        localizationProbIndex = FindIndex(parts, "Localization prob");
        scoreDiffIndex = FindIndex(parts, "Score diff");
        scoreIndex = FindIndex(parts, "Score");
        pepIndex = FindIndex(parts, "PEP");
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
      result.Score = MyConvert.ToDouble(parts[scoreIndex]);
      result.DeltaScore = MyConvert.ToDouble(parts[scoreDiffIndex]);
      result.ExpectValue = MyConvert.ToDouble(parts[pepIndex]);
      result.Probability = MyConvert.ToDouble(parts[localizationProbIndex]);

      string seq = parts[sequenceWindowIndex];
      string modifications = parts[modificationWindowIndex];

      seq = GetModifiedSequence(seq, modifications);
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

    public static string GetModifiedSequence(string seq, string modifications)
    {
      seq = seq.StringBefore(";");
      var mods = modifications.Split(';');
      StringBuilder result = new StringBuilder();
      for (int i = 0; i < seq.Length; i++)
      {
        result.Append(seq[i]);
        if (mods[i] != "X")
        {
          var newmod = mods[i].StringBefore("(").Trim();
          result.Append("(" + newmod + ")");
        }
      }
      return result.ToString();
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