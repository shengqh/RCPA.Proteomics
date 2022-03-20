using RCPA.Proteomics.Raw;
using RCPA.Proteomics.Sequest;
using RCPA.Proteomics.Summary;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace RCPA.Tools.Raw
{
  /// <summary>
  /// 根据MS2/MS3信息筛选结果
  /// </summary>
  public class MsLevelCheckProcessor : AbstractThreadFileProcessor
  {
    private List<string> rawFiles;
    private Dictionary<string, string> expRawMap;
    private Regex ms2seqPattern;
    private Regex ms3seqPattern;

    public MsLevelCheckProcessor(string[] rawDirs, string ms2seqPattern, string ms3seqPattern)
    {
      this.rawFiles = FileUtils.GetFiles(rawDirs, "*.RAW", true);

      this.ms2seqPattern = new Regex(ms2seqPattern);

      this.ms3seqPattern = new Regex(ms3seqPattern);

      this.expRawMap = new Dictionary<string, string>();
      foreach (string fi in rawFiles)
      {
        this.expRawMap[FileUtils.ChangeExtension(new FileInfo(fi).Name, "")] = fi;
      }
    }

    public override IEnumerable<string> Process(string fileName)
    {
      SequestResultTextFormat format = new SequestResultTextFormat();

      IIdentifiedResult sr = format.ReadFromFile(fileName);

      Dictionary<string, HashSet<IIdentifiedSpectrum>> peptideMap = sr.GetExperimentalPeptideMap();

      Dictionary<IIdentifiedSpectrum, int> confused = new Dictionary<IIdentifiedSpectrum, int>();
      List<IIdentifiedSpectrum> wrongMs2 = new List<IIdentifiedSpectrum>();
      List<IIdentifiedSpectrum> wrongMs3 = new List<IIdentifiedSpectrum>();

      int pepcount = 0;

      List<string> exps = new List<string>(peptideMap.Keys);
      exps.Sort();
      foreach (string exp in exps)
      {
        Console.Out.WriteLine(exp);

        HashSet<IIdentifiedSpectrum> peps = peptideMap[exp];
        pepcount += peps.Count;
        using (IRawFile rawFile = new RawFileImpl(expRawMap[exp]))
        {
          foreach (IIdentifiedSpectrum pep in peps)
          {
            int msLevel = rawFile.GetMsLevel(pep.Query.FileScan.FirstScan);

            bool bMs2 = ms2seqPattern.Match(pep.Sequence).Success;
            bool bMs3 = ms3seqPattern.Match(pep.Sequence).Success;

            if (bMs2 && bMs3)
            {
              confused[pep] = msLevel;
              continue;
            }

            if (bMs3)
            {
              if (msLevel != 3)
              {
                wrongMs3.Add(pep);
                continue;
              }
            }
            else if (bMs2)
            {
              if (msLevel != 2)
              {
                wrongMs2.Add(pep);
                continue;
              }
            }
          }
        }
      }

      string incorrectFilename = FileUtils.ChangeExtension(fileName, ".incorrect.peptides.txt");
      using (StreamWriter sw = new StreamWriter(incorrectFilename))
      {
        sw.WriteLine("Type\tFilename\tSequence\tScore\tDeltaScore\tmsLevel");
        foreach (IIdentifiedSpectrum pep in confused.Keys)
        {
          sw.WriteLine(MyConvert.Format("Confused\t{0}\t{1}\t{2:0.00}\t{3:0.00}\t{4}",
            pep.Query.FileScan.LongFileName,
            pep.Sequence,
            pep.Score,
            pep.DeltaScore,
            confused[pep]));
        }

        foreach (IIdentifiedSpectrum pep in wrongMs2)
        {
          sw.WriteLine(MyConvert.Format("WrongMS2\t{0}\t{1}\t{2:0.00}\t{3:0.00}\t3",
            pep.Query.FileScan.LongFileName,
            pep.Sequence,
            pep.Score,
            pep.DeltaScore));
        }

        foreach (IIdentifiedSpectrum pep in wrongMs3)
        {
          sw.WriteLine(MyConvert.Format("WrongMS3\t{0}\t{1}\t{2:0.00}\t{3:0.00}\t2",
            pep.Query.FileScan.LongFileName,
            pep.Sequence,
            pep.Score,
            pep.DeltaScore));
        }

        sw.WriteLine();
        sw.WriteLine("Total\t" + pepcount);
        sw.WriteLine("Confused\t" + confused.Count);
        sw.WriteLine("WrongMs2\t" + wrongMs2.Count);
        sw.WriteLine("WrongMs3\t" + wrongMs3.Count);
      }

      List<string> incorrectDtafilenames = new List<string>();
      foreach (IIdentifiedSpectrum pep in confused.Keys)
      {
        incorrectDtafilenames.Add(pep.Query.FileScan.LongFileName);
      }
      foreach (IIdentifiedSpectrum pep in wrongMs2)
      {
        incorrectDtafilenames.Add(pep.Query.FileScan.LongFileName);
      }
      foreach (IIdentifiedSpectrum pep in wrongMs3)
      {
        incorrectDtafilenames.Add(pep.Query.FileScan.LongFileName);
      }

      for (int i = sr.Count - 1; i >= 0; i--)
      {
        foreach (IIdentifiedProtein sp in sr[i])
        {
          for (int j = sp.Peptides.Count - 1; j >= 0; j--)
          {
            if (incorrectDtafilenames.Contains(sp.Peptides[j].Spectrum.Query.FileScan.Experimental))
            {
              sp.Peptides.RemoveAt(j);
            }
          }
        }

        if (sr[i][0].Peptides.Count == 0)
        {
          sr.RemoveAt(i);
        }
      }

      string correctFilename = FileUtils.ChangeExtension(fileName, ".correct.txt");
      //BuildSummaryResultUtils.Write(correctFilename, cr);
      return new[] { correctFilename };
    }
  }
}
