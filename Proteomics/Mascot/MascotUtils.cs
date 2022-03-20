using RCPA.Proteomics.Summary;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace RCPA.Proteomics.Mascot
{
  public sealed class MascotUtils
  {
    private static readonly Regex cmpdRegex = new Regex(@"Cmpd\s+(\d+)");

    //Elution from: 40.798 to 40.798 period: 0 experiment: 1 cycles: 1 precIntensity: 355220.0 FinneganScanNumber: 6070 MStype: enumIsNormalMS rawFile: KR_mix_lc_090115.raw
    private static readonly Regex dtasuperchargeRegex = new Regex(@"ScanNumber:\s+(\d+).+rawFile:\s+(.+)");
    private static readonly Regex qtofRegex = new Regex(@"spectrumId=(\d+)\s+");
    private static readonly Regex sqhCmpdRegex = new Regex(@"(.+),\s+Cmpd\s+(\d+)");

    private MascotUtils()
    {
    }

    public static SequestFilename ParseTitle(string title, int charge)
    {
      string trimTitle = title.Trim();

      if (trimTitle.StartsWith("Cmpd "))
      {
        Match match = cmpdRegex.Match(trimTitle);
        if (match.Success)
        {
          int scan = int.Parse(match.Groups[1].Value);

          var result = new SequestFilename();
          result.Experimental = "Cmpd";
          result.FirstScan = scan;
          result.LastScan = scan;
          result.Charge = charge;
          result.Extension = "dta";

          return result;
        }
        else
        {
          throw new ArgumentException("Unknow title fomat :" + title + " ; contact author please.");
        }
      }


      if (trimTitle.Contains("Cmpd "))
      {
        Match match = sqhCmpdRegex.Match(trimTitle);
        if (match.Success)
        {
          int scan = int.Parse(match.Groups[2].Value);

          var result = new SequestFilename();
          result.Experimental = match.Groups[1].Value.Trim();
          result.FirstScan = scan;
          result.LastScan = scan;
          result.Charge = charge;
          result.Extension = "dta";

          return result;
        }
        else
        {
          throw new ArgumentException("Unknow title fomat :" + title + " ; contact author please.");
        }
      }

      if (trimTitle.StartsWith("spectrumId="))
      {
        //spectrumId=6085 Polarity=Positive ScanMode=ProductIon TimeInMinutes=61.875 acqNumber=3712494
        Match match = qtofRegex.Match(trimTitle);
        if (match.Success)
        {
          int scan = int.Parse(match.Groups[1].Value);

          var result = new SequestFilename();
          result.Experimental = "";
          result.FirstScan = scan;
          result.LastScan = scan;
          result.Charge = charge;
          result.Extension = "dta";

          return result;
        }
        else
        {
          throw new ArgumentException("Unknow title fomat :" + title + " ; contact author please.");
        }
      }

      //Elution from: 40.798 to 40.798 period: 0 experiment: 1 cycles: 1 precIntensity: 355220.0 FinneganScanNumber: 6070 MStype: enumIsNormalMS rawFile: KR_mix_lc_090115.raw
      if (trimTitle.StartsWith("Elution from"))
      {
        Match match = dtasuperchargeRegex.Match(trimTitle);
        if (match.Success)
        {
          int scan = int.Parse(match.Groups[1].Value);
          var result = new SequestFilename();
          result.Experimental = FileUtils.ChangeExtension(match.Groups[2].Value, "");
          result.FirstScan = scan;
          result.LastScan = scan;
          result.Charge = charge;
          result.Extension = "dta";

          return result;
        }
        else
        {
          throw new ArgumentException("Unknow title fomat :" + title + " ; contact author please.");
        }
      }

      try
      {
        return SequestFilename.Parse(trimTitle);
      }
      catch (Exception)
      {
      }

      try
      {
        return SequestFilename.Parse(trimTitle + ".dta");
      }
      catch (Exception)
      {
      }

      try
      {
        return SequestFilename.Parse(trimTitle + "." + charge + ".dta");
      }
      catch (Exception)
      {
      }

      throw new ArgumentException("Unknow title format :" + title + " ; contact author please.");
    }

    public static List<IIdentifiedProtein> BuildProteins(IEnumerable<IIdentifiedSpectrum> peptides)
    {
      var proteinMap = new Dictionary<string, IIdentifiedProtein>();

      foreach (IIdentifiedSpectrum mph in peptides)
      {
        foreach (IIdentifiedPeptide pep in mph.Peptides)
        {
          foreach (string proteinName in pep.Proteins)
          {
            if (!proteinMap.ContainsKey(proteinName))
            {
              IIdentifiedProtein protein = new IdentifiedProtein();
              protein.Name = proteinName;
              proteinMap[proteinName] = protein;
              protein.Peptides.Add(pep);
            }
            else
            {
              proteinMap[proteinName].Peptides.Add(pep);
            }
          }
        }
      }

      return new List<IIdentifiedProtein>(proteinMap.Values);
    }

    public static List<IIdentifiedProteinGroup> BuildRedundantProteinGroups(List<IIdentifiedProtein> proteins)
    {
      foreach (IIdentifiedProtein mp in proteins)
      {
        mp.SortPeptides();
      }
      proteins.Sort();

      var result = new List<IIdentifiedProteinGroup>();

      //Merge the proteins with same peptides to same protein group
      for (int i = 0; i < proteins.Count; i++)
      {
        var mpg = new IdentifiedProteinGroup();
        result.Add(mpg);

        mpg.Add(proteins[i]);

        int j = i + 1;
        while (j < proteins.Count)
        {
          if (proteins[i].UniquePeptideCount != proteins[j].UniquePeptideCount ||
              proteins[i].Peptides.Count != proteins[j].Peptides.Count)
          {
            break;
          }

          if (CollectionUtils.ValueEquals(proteins[i].GetSpectra(), proteins[j].GetSpectra()))
          {
            mpg.Add(proteins[j]);
            proteins.RemoveAt(j);
          }
          else
          {
            j++;
          }
        }
      }
      return result;
    }

    public static List<IIdentifiedProteinGroup> BuildNonredundantProteinGroups(List<IIdentifiedProtein> proteins)
    {
      List<IIdentifiedProteinGroup> result = BuildRedundantProteinGroups(proteins);

      //Remove redundant protein group
      for (int i = 0; i < result.Count; i++)
      {
        List<IIdentifiedSpectrum> peptidesI = result[i][0].GetSpectra();
        for (int j = result.Count - 1; j > i; j--)
        {
          if (CollectionUtils.ContainsAll(peptidesI, result[j][0].GetSpectra()))
          {
            result.RemoveAt(j);
          }
        }
      }

      return result;
    }
  }
}