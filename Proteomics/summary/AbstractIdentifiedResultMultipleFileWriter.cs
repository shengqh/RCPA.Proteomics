using RCPA.Proteomics.Mascot;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace RCPA.Proteomics.Summary
{
  public abstract class AbstractIdentifiedResultMultipleFileWriter : IFileWriter<IIdentifiedResult>
  {
    public delegate void WriteProteinLine(StreamWriter sw, IIdentifiedProteinGroup mpg, IdentifiedProteinTextWriter proteinWriter);

    private readonly Regex[] perferAccessNumberRegexs;

    private readonly WriteProteinLine WriteFunction;

    public AbstractIdentifiedResultMultipleFileWriter()
    {
      this.WriteFunction = WriteProteinMultipleLines;
    }

    public AbstractIdentifiedResultMultipleFileWriter(string[] perferAccessNumberRegexString)
    {
      this.WriteFunction = WriteProteinOneLine;

      this.perferAccessNumberRegexs = new Regex[perferAccessNumberRegexString.Length];
      for (int i = 0; i < perferAccessNumberRegexString.Length; i++)
      {
        this.perferAccessNumberRegexs[i] = new Regex(perferAccessNumberRegexString[i]);
      }
    }

    private void WriteProteinOneLine(StreamWriter sw, IIdentifiedProteinGroup mpg, IdentifiedProteinTextWriter proteinWriter)
    {
      //find user-defined protein
      IIdentifiedProtein filtered = null;
      foreach (Regex reg in this.perferAccessNumberRegexs)
      {
        foreach (IIdentifiedProtein mp in mpg)
        {
          if (reg.Match(mp.Name).Success)
          {
            filtered = mp;
            break;
          }
        }
        if (filtered != null)
        {
          break;
        }
      }

      string name;
      string reference;
      if (filtered != null)
      {
        name = filtered.Name;
        reference = filtered.Description;
        filtered.UniquePeptideCount = mpg[0].UniquePeptideCount;
      }
      else
      {
        var names = new StringBuilder();
        var refs = new StringBuilder();
        for (int proteinIndex = 0; proteinIndex < mpg.Count; proteinIndex++)
        {
          names.Append(" ! ").Append(mpg[proteinIndex].Name);
          refs.Append(" ! ").Append(mpg[proteinIndex].Description);
          IIdentifiedProtein mpro = mpg[proteinIndex];
          mpro.UniquePeptideCount = mpg[0].UniquePeptideCount;
        }

        filtered = mpg[0];
        name = names.ToString().Substring(3);
        reference = refs.ToString().Substring(3);
      }

      sw.WriteLine("${0}-1\t{1}\t{2}{3}",
                   mpg.Index,
                   name,
                   reference,
                   proteinWriter.GetString(filtered));
    }

    private void WriteProteinMultipleLines(StreamWriter sw, IIdentifiedProteinGroup mpg, IdentifiedProteinTextWriter proteinWriter)
    {
      for (int proteinIndex = 0; proteinIndex < mpg.Count; proteinIndex++)
      {
        IIdentifiedProtein mpro = mpg[proteinIndex];
        mpro.UniquePeptideCount = mpg[0].UniquePeptideCount;
        sw.WriteLine("${0}-{1}\t{2}\t{3}\t{4}",
                     mpg.Index,
                     proteinIndex + 1,
                     mpro.Name,
                     mpro.Description,
                     proteinWriter.GetString(mpro));
      }
    }

    protected abstract string GetProteinHeader();

    protected abstract string GetPeptideHeader();

    protected abstract string GetPeptideFileName(string proteinFileName);

    #region IFileWriter<IIdentifiedResult> Members

    public void WriteToFile(string proteinFile, IIdentifiedResult mr)
    {
      var proteinWriter =
        new IdentifiedProteinTextWriter(GetProteinHeader());

      using (var sw = new StreamWriter(proteinFile))
      {
        sw.WriteLine("\tName\tDescription" + proteinWriter.GetHeader());

        foreach (IIdentifiedProteinGroup mpg in mr)
        {
          if (mpg[0].IsEnabled(true))
          {
            mpg[0].InitUniquePeptideCount(mph => mph.Spectrum.IsEnabled(true));

            this.WriteFunction(sw, mpg, proteinWriter);
          }
        }
      }

      string peptideFile = GetPeptideFileName(proteinFile);

      var peptideWriter = new MascotPeptideTextFormat(GetPeptideHeader());

      using (var sw = new StreamWriter(peptideFile))
      {
        sw.WriteLine(peptideWriter.PeptideFormat.GetHeader());

        foreach (IIdentifiedProteinGroup mpg in mr)
        {
          if (mpg[0].IsEnabled(true))
          {
            foreach (IIdentifiedSpectrum mph in mpg[0].GetSpectra())
            {
              if (mph.IsEnabled(false))
              {
                sw.WriteLine(peptideWriter.PeptideFormat.GetString(mph));
              }
            }
          }
        }
      }
    }

    #endregion
  }
}