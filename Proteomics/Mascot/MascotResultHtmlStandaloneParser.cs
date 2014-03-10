using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using RCPA.Proteomics.Summary;
using RCPA.Utils;

namespace RCPA.Proteomics.Mascot
{
  public class MascotResultHtmlStandaloneParser :
    AbstractMascotResultHtmlParser
  {
    private static readonly Regex peptideInfoRegex = new Regex
      ("<tr>.+?value=\"([\\d.]+)\\sfrom\\(([\\d.]+),(\\d+)\\+\\).+?title\\((.+?)\\)\\s+query\\((\\d+)\\).+?<b>\\s*([\\d.]+).+?</b>.+?<b>\\s*([\\d.]+).+?</b>.+?<b>\\s*([\\d.]+).+?</b>.+?<b>\\s*([-\\d.]+).+?</b>.+?<b>\\s*([\\d.]+).+?</b>.+?<b>[\\s(]*([\\d.]+).+?</b>.+?<b>\\s*([\\d.e+-]+).+?</b>.+?<b>\\s*([\\d]+).+?</b>.+<b>\\s*(.+?)</b></font>",
       RegexOptions.IgnoreCase);

    private static readonly Regex proteinNameMassRefRegex = new Regex(
      @"<a\s+href.+?>(.+?)</a>.+?Mass:.+?(\d+).+?matched.+?<tt>(.*?)</tt>",
      RegexOptions.IgnoreCase);

    private static readonly Regex proteinNameMassRegex = new Regex(
      @"<a\s+href.+?>(.+?)</a>.+?Mass:.+?(\d+)", RegexOptions.IgnoreCase);

    private static readonly Regex proteinNameRegex = new Regex(
      @"<a\s+href.+?>(.+?)</a>", RegexOptions.IgnoreCase);

    private static readonly Regex tableRegex = new Regex(
      "([(?:<table)|^].+?</table>)", RegexOptions.IgnoreCase);

    private static readonly Regex trRegex = new Regex("(<tr.+?</tr>)",
                                                      RegexOptions.IgnoreCase);

    private static Regex proteinRegex;

    public MascotResultHtmlStandaloneParser(bool filterByDefaultScoreAndPvalue)
      : base(filterByDefaultScoreAndPvalue)
    {
    }

    public MascotResultHtmlStandaloneParser(
      IFilter<IIdentifiedSpectrum> defaultPeptideFilter,
      bool filterByDefaultScoreAndPvalue)
      : base(defaultPeptideFilter, filterByDefaultScoreAndPvalue)
    {
    }

    protected override Regex GetProteinRegex()
    {
      if (null == proteinRegex)
      {
        proteinRegex = new Regex(
          "(<a\\sname=(?:\"){0,1}Hit.+?<table.+?Query.+?</table>)",
          RegexOptions.IgnoreCase);
      }
      return proteinRegex;
    }


    protected override List<String> GetProteinInfo(String proteinContent)
    {
      Match tableMatch = tableRegex.Match(proteinContent);
      if (!tableMatch.Success)
      {
        throw new ArgumentException(
          "Cannot find protein information table in : " + proteinContent);
      }

      var result = new List<String>();
      Match proteinInfoMatch = proteinNameMassRefRegex.Match(tableMatch.Groups[1].Value);
      if (proteinInfoMatch.Success)
      {
        result.Add(proteinInfoMatch.Groups[1].Value);
        result.Add(proteinInfoMatch.Groups[2].Value);
        result.Add(proteinInfoMatch.Groups[3].Value.Trim());
      }
      else
      {
        proteinInfoMatch = proteinNameMassRegex.Match(tableMatch
                                                        .Groups[1].Value);
        if (proteinInfoMatch.Success)
        {
          result.Add(proteinInfoMatch.Groups[1].Value);
          result.Add(proteinInfoMatch.Groups[2].Value);
          result.Add("");
        }
        else
        {
          proteinInfoMatch = proteinNameRegex.Match(tableMatch
                                                      .Groups[1].Value);
          if (!proteinInfoMatch.Success)
          {
            throw new ArgumentException("Cannot find protein name in : "
                                        + tableMatch.Groups[1]);
          }
          result.Add(proteinInfoMatch.Groups[1].Value);
          result.Add("0");
          result.Add("");
        }
      }

      return result;
    }


    protected override List<String> GetPeptideInfoContentList(String proteinContent)
    {
      var result = new List<String>();
      Match tableMatch = tableRegex.Match(proteinContent);
      // Find the table contains "Mr(expt)"
      while (tableMatch.Success)
      {
        if (-1 != tableMatch.Groups[1].Value.IndexOf("Mr(expt)"))
        {
          break;
        }

        tableMatch = tableMatch.NextMatch();
      }

      if (tableMatch.Success)
      {
        Match trMatch = trRegex.Match(tableMatch.Groups[1].Value);
        trMatch = trMatch.NextMatch(); // excluding first Match, it's header
        while (trMatch.Success)
        {
          result.Add(trMatch.Groups[1].Value);
          trMatch = trMatch.NextMatch();
        }
      }
      return result;
    }

    protected override List<String> GetPeptideInfo(String peptideInfoContent)
    {
      Match Match = peptideInfoRegex.Match(peptideInfoContent);
      if (!Match.Success)
      {
        return new List<String>();
      }

      return Match.ToList();
    }

    protected override Regex GetTableRegex()
    {
      return tableRegex;
    }
  }
}