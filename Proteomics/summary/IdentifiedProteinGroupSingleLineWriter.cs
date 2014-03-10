using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;

namespace RCPA.Proteomics.Summary
{
  public class IdentifiedProteinGroupSingleLineWriter : IIdentifiedProteinGroupWriter
  {
    public IdentifiedProteinGroupSingleLineWriter(string[] perferAccessNumberRegexString)
    {
      SetProteinNameSelectionPatterns(perferAccessNumberRegexString);
    }

    private Regex[] perferAccessNumberRegexs;

    public void SetProteinNameSelectionPatterns(string[] perferAccessNumberRegexString)
    {
      this.perferAccessNumberRegexs = new Regex[perferAccessNumberRegexString.Length];
      for (int i = 0; i < perferAccessNumberRegexString.Length; i++)
      {
        this.perferAccessNumberRegexs[i] = new Regex(perferAccessNumberRegexString[i]);
      }
    }

    #region IIdentifiedProteinGroupWriter Members

    public LineFormat<IIdentifiedProtein> ProteinFormat { get; set; }

    public void WriteToStream(StreamWriter sw, IIdentifiedProteinGroup mpg)
    {
      //find user-defined protein
      foreach (Regex reg in perferAccessNumberRegexs)
      {
        foreach (IIdentifiedProtein mp in mpg)
        {
          if (reg.Match(mp.Name).Success)
          {
            sw.WriteLine("${0}-1{1}", mpg.Index, ProteinFormat.GetString(mp));
            return;
          }
        }
      }

      StringBuilder names = new StringBuilder();
      StringBuilder refs = new StringBuilder();
      for (int proteinIndex = 0; proteinIndex < mpg.Count; proteinIndex++)
      {
        if (proteinIndex != 0)
        {
          names.Append(" ! ");
          refs.Append(" ! ");
        }

        names.Append(mpg[proteinIndex].Name);
        refs.Append(mpg[proteinIndex].Description);
      }

      string oldName = mpg[0].Name;
      string oldReference = mpg[0].Description;

      mpg[0].Name = names.ToString();
      mpg[0].Description = refs.ToString();
      try
      {
        sw.WriteLine("${0}-1{1}", mpg.Index, ProteinFormat.GetString(mpg[0]));
      }
      finally
      {
        mpg[0].Name = oldName;
        mpg[0].Description = oldReference;
      }
    }

    #endregion
  }
}
