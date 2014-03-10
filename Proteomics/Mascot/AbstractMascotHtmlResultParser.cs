using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using RCPA.Proteomics.Isotopic;
using RCPA.Proteomics.Summary;
using RCPA.Utils;

namespace RCPA.Proteomics.Mascot
{
  public interface IMascotResultParser
  {
    MascotResult ParseContent(String filecontent);

    MascotResult ParseFile(FileInfo file);
  }

  public abstract class AbstractMascotResultHtmlParser : IMascotResultParser
  {
    private static Regex peakIsotopicTypeRegex;
    protected static Regex peakToleranceRegex;
    private static Regex pValueRegex;
    private readonly IFilter<IIdentifiedSpectrum> defaultPeptideFilter;
    private readonly Regex modificationReg = new Regex(@"(?:\d\s){0,1}(.+)\s+\((\S+)\)");

    private IFilter<IIdentifiedSpectrum> currentPeptideFilter = new FilterTrue<IIdentifiedSpectrum>();
    private bool filterByDefaultScoreAndPvalue = true;
    protected Dictionary<string, char> modifications = new Dictionary<string, char>();

    public AbstractMascotResultHtmlParser(bool filterByDefaultScoreAndPvalue)
    {
      this.filterByDefaultScoreAndPvalue = filterByDefaultScoreAndPvalue;
    }

    public AbstractMascotResultHtmlParser(IFilter<IIdentifiedSpectrum> defaultPeptideFilter,
                                          bool filterByDefaultScoreAndPvalue)
    {
      this.defaultPeptideFilter = defaultPeptideFilter;
      this.filterByDefaultScoreAndPvalue = filterByDefaultScoreAndPvalue;
    }

    #region IMascotResultParser Members

    public MascotResult ParseFile(FileInfo file)
    {
      String fileContent = FileUtils.ReadFileWithoutLineBreak(file.FullName, true);

      return ParseContent(fileContent);
    }

    public MascotResult ParseContent(String fileContent)
    {
      var result = new MascotResult();

      this.modifications = new Dictionary<string, char>();

      Pair<int, double> pValueScore = ParsePValueScore(fileContent);
      result.PValueScore = pValueScore.First;
      result.PValue = pValueScore.Second;

      var offsets = new List<Offset>();
      try
      {
        result.PeakIsotopicType = ParsePeakIsotopicType(fileContent);
      }
      catch (ArgumentException)
      {
      }

      try
      {
        result.PeakTolerance = ParsePeakTolerance(fileContent);
      }
      catch (ArgumentException)
      {
      }

      var filters = new List<IFilter<IIdentifiedSpectrum>>();
      if (this.filterByDefaultScoreAndPvalue)
      {
        filters.Add(new IdentifiedSpectrumScoreFilter(pValueScore.First));
        filters.Add(new IdentifiedSpectrumExpectValueFilter(pValueScore.Second));
      }

      filters.Add(new IdentifiedSpectrumRankFilter(1));
      if (null != this.defaultPeptideFilter)
      {
        filters.Add(this.defaultPeptideFilter);
      }

      this.currentPeptideFilter = new AndFilter<IIdentifiedSpectrum>(filters);

      Match proteinMatch = GetProteinRegex().Match(fileContent);
      while (proteinMatch.Success)
      {
        IdentifiedProtein protein = ParseProtein(proteinMatch.Groups[1].Value);
        var group = new IdentifiedProteinGroup();
        group.Add(protein);
        result.Add(group);
        offsets.Add(new Offset(proteinMatch.Index, proteinMatch.Index + proteinMatch.Length, group));
        proteinMatch = proteinMatch.NextMatch();
      }

      int endIndex = fileContent.IndexOf("Peptide matches not assigned to protein hits");
      if (-1 == endIndex)
      {
        endIndex = fileContent.Length - 1;
      }

      for (int i = 0; i < offsets.Count; i++)
      {
        int start = offsets[i].End;
        int end = i == offsets.Count - 1 ? endIndex : offsets[i + 1].Start;
        String redundant = fileContent.Substring(start, end - start + 1);
        if (!redundant.Contains("Proteins matching the same set"))
        {
          continue;
        }

        List<IdentifiedProtein> sameMatchProteins = ParseSameMatchProteins(redundant);

        foreach (IdentifiedProtein mp in sameMatchProteins)
        {
          mp.Peptides.AddRange(offsets[i].Mpg[0].Peptides);
          offsets[i].Mpg.Add(mp);
        }
      }

      for (int i = result.Count - 1; i >= 0; i--)
      {
        if (0 == result[i][0].Peptides.Count)
        {
          result.RemoveAt(i);
        }
      }

      RefineModification(result);

      MergePeptides(result);

      result.InitUniquePeptideCount();

      return result;
    }

    #endregion

    protected static Regex GetPValueRegex()
    {
      if (pValueRegex == null)
      {
        pValueRegex = new Regex(@"(\d+)\sindicate\sidentity\sor\sextensive\shomology\s\(p&lt;([\d.]+)\)");
      }
      return pValueRegex;
    }

    protected static Regex GetPeakIsotopicTypeRegex()
    {
      if (peakIsotopicTypeRegex == null)
      {
        peakIsotopicTypeRegex = new Regex(@"Mass\svalues\s+:\s+(Monoisotopic|Average)");
      }
      return peakIsotopicTypeRegex;
    }

    protected static Regex GetPakToleranceRegex()
    {
      if (peakToleranceRegex == null)
      {
        peakToleranceRegex = new Regex(@"Fragment Mass Tolerance.*?:.+?([\d.]+)\s");
      }
      return peakToleranceRegex;
    }

    public void SetFilterByDefaultScoreAndPvalue(bool value)
    {
      this.filterByDefaultScoreAndPvalue = value;
    }

    public Pair<int, double> ParsePValueScore(String content)
    {
      Match Match = GetPValueRegex().Match(content);
      if (!Match.Success)
      {
        throw new ArgumentException("No pvalue Regex found in " + content);
      }

      return new Pair<int, double>(int.Parse(Match.Groups[1].Value), MyConvert.ToDouble(Match.Groups[2].Value));
    }

    public IsotopicType ParsePeakIsotopicType(String content)
    {
      Match Match = GetPeakIsotopicTypeRegex().Match(content);
      if (!Match.Success)
      {
        throw new ArgumentException("No peak isotopic type Regex found.");
      }

      if (Match.Groups[1].Value.Equals("Monoisotopic"))
      {
        return IsotopicType.Monoisotopic;
      }
      else
      {
        return IsotopicType.Average;
      }
    }

    public double ParsePeakTolerance(String content)
    {
      Match Match = GetPakToleranceRegex().Match(content);
      if (!Match.Success)
      {
        throw new ArgumentException("No peak tolerance Regex found.");
      }

      return MyConvert.ToDouble(Match.Groups[1].Value);
    }

    private void RefineModification(MascotResult result)
    {
      var mods = new List<string>(this.modifications.Keys);
      mods.Sort();

      for (int i = 0; i < mods.Count; i++)
      {
        this.modifications[mods[i]] = MascotDatSpectrumParser.MODIFICATION_CHAR[i + 1];
      }

      List<IIdentifiedSpectrum> spectra = result.GetSpectra();
      foreach (IIdentifiedSpectrum spectrum in spectra)
      {
        foreach (IIdentifiedPeptide pep in spectrum.Peptides)
        {
          if (null == spectrum.Modifications)
          {
            continue;
          }

          string seq = pep.Sequence;
          string mod = spectrum.Modifications;

          string[] modStrs = mod.Split(new[] {';'}, StringSplitOptions.RemoveEmptyEntries);
          foreach (string modStr in modStrs)
          {
            Match m = this.modificationReg.Match(modStr.Trim());
            char modChar = this.modifications[m.Groups[1].Value];
            seq = seq.Replace("<u>" + m.Groups[2].Value + "</u>", m.Groups[2].Value + modChar);
            seq = seq.Replace("<U>" + m.Groups[2].Value + "</U>", m.Groups[2].Value + modChar);
          }

          seq = seq.Replace("<u>", "");
          seq = seq.Replace("</u>", "*");
          seq = seq.Replace("<U>", "");
          seq = seq.Replace("</U>", "*");
          seq = seq.Replace(".*", ".");

          pep.Sequence = seq;
        }
      }
    }

    protected abstract Regex GetTableRegex();

    protected List<IdentifiedProtein> ParseSameMatchProteins(String redundant)
    {
      var result = new List<IdentifiedProtein>();

      Match tableMatch = GetTableRegex().Match(redundant);
      while (tableMatch.Success)
      {
        try
        {
          IdentifiedProtein mp = GetProtein(tableMatch.Groups[1].Value);
          result.Add(mp);
        }
        catch (ArgumentException)
        {
        }
        tableMatch = tableMatch.NextMatch();
      }

      return result;
    }

    private void MergePeptides(MascotResult result)
    {
      var peptideMap = new Dictionary<String, IIdentifiedPeptide>();
      foreach (IIdentifiedProteinGroup group in result)
      {
        foreach (IIdentifiedProtein protein in group)
        {
          for (int i = 0; i < protein.Peptides.Count; i++)
          {
            String pepid = protein.Peptides[i].Spectrum.Query.QueryId + "_" + protein.Peptides[i].Sequence;
            if (peptideMap.ContainsKey(pepid))
            {
              IIdentifiedPeptide old = peptideMap[pepid];

              old.AddProtein(protein.Name);
              protein.Peptides[i] = old;
            }
            else
            {
              peptideMap[pepid] = protein.Peptides[i];
            }
          }
        }
      }
    }

    public IFilter<IIdentifiedSpectrum> GetPeptideFilter()
    {
      return this.currentPeptideFilter;
    }

    protected IdentifiedProtein ParseProtein(String proteinContent)
    {
      IdentifiedProtein result = GetProtein(proteinContent);

      List<String> peptideInfoContentList = GetPeptideInfoContentList(proteinContent);
      foreach (String peptideInfoContent in peptideInfoContentList)
      {
        List<String> peptideInfo = GetPeptideInfo(peptideInfoContent);
        if (0 == peptideInfo.Count)
        {
          continue;
        }

        IIdentifiedSpectrum mphit = new IdentifiedSpectrum();

        // Group 0 : peptide mass from observed m/z
        double experimentalPeptideMass = MyConvert.ToDouble(peptideInfo[0]);
        mphit.ExperimentalMass = experimentalPeptideMass;

        // Group 1 : observed m/z
        double observed = MyConvert.ToDouble(peptideInfo[1]);
        mphit.Query.ObservedMz = observed;

        // Group 2 : charge
        int charge = int.Parse(peptideInfo[2]);
        mphit.Query.Charge = charge;

        // Group 3 : title
        String title = Uri.UnescapeDataString(peptideInfo[3]).Trim();
        mphit.Query.Title = title;

        SequestFilename sf = MascotUtils.ParseTitle(title, charge);
        if (sf != null)
        {
          mphit.Query.FileScan.LongFileName = sf.LongFileName;
        }

        // Group 4 : query
        mphit.Query.QueryId = int.Parse(peptideInfo[4]);

        // Group 5 equals Group 1

        // Group 6 equals Group 0

        // Group 7 : calculated peptide mass
        mphit.TheoreticalMass = MyConvert.ToDouble(peptideInfo[7]);

        // Group 8 : different between observed peptide mass and calculated
        // peptide mass

        // Group 9 : miss cleavage
        mphit.NumMissedCleavages = int.Parse(peptideInfo[9]);

        // Group 10: score
        mphit.Score = int.Parse(peptideInfo[10]);

        // Group 11: expect p value
        mphit.ExpectValue = MyConvert.ToDouble(peptideInfo[11]);

        // Group 12: rank
        mphit.Rank = int.Parse(peptideInfo[12]);

        // Group 13: peptide sequence
        // &nbsp;K.YEINVLR<u>.</u>N + Label:18O(2) (C-term)
        String seq = peptideInfo[13].Replace("&nbsp;", "");

        var mpep = new IdentifiedPeptide(mphit);

        string[] parts = Regex.Split(seq, "\\+");
        if (parts.Length > 1)
        {
          seq = parts[0].Trim();
          mphit.Modifications = parts[1].Trim();
          string[] mods = parts[1].Trim().Split(new[] {';'}, StringSplitOptions.RemoveEmptyEntries);
          foreach (string mod in mods)
          {
            Match m = this.modificationReg.Match(mod.Trim());
            if (!this.modifications.ContainsKey(m.Groups[1].Value))
            {
              this.modifications[m.Groups[1].Value] = ' ';
            }
          }
        }

        mpep.Sequence = seq;

        if (GetPeptideFilter().Accept(mphit))
        {
          mpep.AddProtein(result.Name);
          result.Peptides.Add(mpep);
        }
      }

      return result;
    }

    private IdentifiedProtein GetProtein(String proteinContent)
    {
      List<String> proteinInfo = GetProteinInfo(proteinContent);

      var result = new IdentifiedProtein();
      result.Name = proteinInfo[0];
      result.MolecularWeight = MyConvert.ToDouble(proteinInfo[1]);
      result.Description = proteinInfo[2];

      return result;
    }

    // Get a Regex which can match protein content one by one
    protected abstract Regex GetProteinRegex();

    // Parse protein information from protein content
    // First item should be protein name
    // Second item should be protein reference
    protected abstract List<String> GetProteinInfo(String proteinContent);

    // Parse and get peptide info content list from protein content
    protected abstract List<String> GetPeptideInfoContentList(
      String proteinContent);

    // Parse peptide information from peptide info content
    protected abstract List<String> GetPeptideInfo(String peptideInfoContent);

    #region Nested type: Offset

    private class Offset
    {
      public Offset(int start, int end, IdentifiedProteinGroup mpg)
      {
        Start = start;
        End = end;
        Mpg = mpg;
      }

      public int Start { get; set; }

      public int End { get; set; }

      public IdentifiedProteinGroup Mpg { get; set; }
    }

    #endregion
  }
}