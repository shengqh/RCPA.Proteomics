using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using RCPA.Seq;
using RCPA.Utils;
using System.Text.RegularExpressions;
using System.Collections;

namespace RCPA.Proteomics.Summary
{
  public static class IdentifiedResultUtils
  {
    private static readonly Regex proteinReg = new Regex(@"^\$(\d+)-\d+\s");

    public static bool IsProteinLine(string line)
    {
      if (null == line)
      {
        return false;
      }

      return proteinReg.Match(line).Success;
    }

    public static int GetGroupIndex(string line)
    {
      return Convert.ToInt32(proteinReg.Match(line).Groups[1].Value);
    }

    public static void FillSequenceFromFasta(string fastaFilename, IIdentifiedResult t, IProgressCallback progress)
    {
      IAccessNumberParser acParser = AccessNumberParserFactory.GuessParser(fastaFilename);
      FillSequenceFromFasta(acParser, fastaFilename, t, progress);
    }

    public static HashSet<string> GetContaminationAccessNumbers(IStringParser<string> acParser, string fastaFilename, string contaminationDescriptionPattern,
                                             IProgressCallback progress)
    {
      HashSet<string> result = new HashSet<string>();

      if (progress == null)
      {
        progress = new EmptyProgressCallback();
      }

      Regex reg = new Regex(contaminationDescriptionPattern, RegexOptions.IgnoreCase);

      progress.SetMessage("Get contamination map from database ...");
      var ff = new FastaFormat();
      using (var sr = new StreamReader(fastaFilename))
      {
        progress.SetRange(1, sr.BaseStream.Length);

        Sequence seq;
        while ((seq = ff.ReadSequence(sr)) != null)
        {
          if (progress.IsCancellationPending())
          {
            throw new UserTerminatedException();
          }

          progress.SetPosition(sr.GetCharpos());

          string ac = acParser.GetValue(seq.Name);

          if (reg.Match(seq.Reference).Success)
          {
            result.Add(ac);
          }
        }
      }

      progress.SetMessage("Get contamination map from database finished.");

      return result;
    }


    public static void FillSequenceFromFasta(IStringParser<string> acParser, string fastaFilename, IIdentifiedResult t,
                                             IProgressCallback progress)
    {
      if (progress == null)
      {
        progress = new EmptyProgressCallback();
      }

      progress.SetMessage("Initializing accessNumber/protein map ...");

      var acMap = new Dictionary<string, IIdentifiedProtein>();
      foreach (IIdentifiedProteinGroup group in t)
      {
        foreach (IIdentifiedProtein protein in group)
        {
          string ac = acParser.GetValue(protein.Name);
          if (acMap.ContainsKey(ac))
          {
            throw new Exception("Duplicate access number " + ac);
          }
          acMap[ac] = protein;

          if (ac != protein.Name)
          {
            if (acMap.ContainsKey(protein.Name))
            {
              throw new Exception("Duplicate access number " + protein.Name);
            }
            acMap[protein.Name] = protein;
          }
        }
      }

      progress.SetMessage("Filling sequence from database ...");
      var ff = new FastaFormat();
      using (var sr = new StreamReader(fastaFilename))
      {
        progress.SetRange(1, sr.BaseStream.Length);

        Sequence seq;
        while ((seq = ff.ReadSequence(sr)) != null)
        {
          if (progress.IsCancellationPending())
          {
            throw new UserTerminatedException();
          }

          progress.SetPosition(sr.BaseStream.Position);

          string ac = acParser.GetValue(seq.Name);
          if (acMap.ContainsKey(ac))
          {
            IIdentifiedProtein protein = acMap[ac];
            protein.Name = seq.Name.Replace("/", " ");
            protein.Description = seq.Description.Replace("\t", " ").Replace("/", " ");
            protein.Sequence = seq.SeqString;
          }
        }
      }

      var failed = acMap.Values.Where(l => l.Sequence == null).ToList();
      if (failed.Count > 0)
      {
        var proteinNames = failed.ConvertAll(l => l.Name).ToArray();
        if (!proteinNames.All(l => l.StartsWith("XXX_")))
        {
          throw new Exception(string.Format("Couldn't find sequence of following protein(s), change access number pattern or select another database\n{0}", proteinNames.Merge("/")));
        }
      }

      progress.SetMessage("Fill sequence from database finished.");
    }

    public static void WriteFastaFile(string fastaFilename, IList<IIdentifiedProteinGroup> t, Func<IIdentifiedProteinGroup, bool> validateGroup)
    {
      foreach (var g in t)
      {
        if (validateGroup(g) && g.Count > 0 && (g[0].Sequence == null || g[0].Sequence.Length == 0))
        {
          return;
        }
      }

      var ff = new FastaFormat();

      using (var sw = new StreamWriter(fastaFilename))
      {
        foreach (IIdentifiedProteinGroup mpg in t)
        {
          if (validateGroup(mpg))
          {
            foreach (IIdentifiedProtein protein in mpg)
            {
              ff.WriteSequence(sw, protein.Reference, protein.Sequence);
            }
          }
        }
      }
    }

    public static void WriteFastaFile(string fastaFilename, IList<IIdentifiedProteinGroup> t)
    {
      WriteFastaFile(fastaFilename, t, (m => true));
    }

    public static void InitializeGroupCount(this IList<IIdentifiedProteinGroup> ir)
    {
      foreach (var group in ir)
      {
        foreach (var pep in group[0].Peptides)
        {
          pep.Spectrum.GroupCount = 0;
        }
      }

      foreach (IIdentifiedProteinGroup group in ir)
      {
        foreach (var pep in group[0].Peptides)
        {
          pep.Spectrum.GroupCount++;
        }
      }
    }
  }
}