using System.Collections.Generic;
using System.IO;
using System.Linq;
using RCPA.Proteomics.Utils;
using RCPA.Utils;
using RCPA.Gui;
using RCPA.Proteomics.Mascot;
using System.Text.RegularExpressions;
using System;

namespace RCPA.Proteomics.Summary.Uniform
{
  public class UniformIdentifiedResultProteinFdrBuilder : AbstractThreadFileProcessor
  {
    private string peptideFile;

    public UniformIdentifiedResultProteinFdrBuilder(string peptideFile)
    {
      this.peptideFile = peptideFile;
    }

    public override IEnumerable<string> Process(string parameterFile)
    {
      var result = new List<string>();

      BuildSummaryOptions conf = BuildSummaryOptionsUtils.LoadFromFile(parameterFile);

      IStringParser<string> acParser = conf.Database.GetAccessNumberParser();

      IIdentifiedProteinBuilder proteinBuilder = new IdentifiedProteinBuilder();
      IIdentifiedProteinGroupBuilder groupBuilder = new IdentifiedProteinGroupBuilder()
      {
        Progress = this.Progress
      };

      //保存非冗余蛋白质列表文件
      IFileFormat<IIdentifiedResult> resultFormat = conf.GetIdetifiedResultFormat();

      if (resultFormat is IProgress)
      {
        (resultFormat as IProgress).Progress = this.Progress;
      }

      var resultBuilder = new IdentifiedResultBuilder(acParser, conf.Database.Location);
      resultBuilder.Progress = Progress;

      List<IIdentifiedSpectrum> finalPeptides = new MascotPeptideTextFormat().ReadFromFile(this.peptideFile);

      CalculateIsoelectricPoint(finalPeptides);

      Progress.SetMessage("Building protein...");
      //构建蛋白质列表
      List<IIdentifiedProtein> finalProteins = proteinBuilder.Build(finalPeptides);

      Progress.SetMessage("Building protein group...");
      //构建蛋白质群列表
      List<IIdentifiedProteinGroup> finalGroups = groupBuilder.Build(finalProteins);

      //构建鉴定结果
      IdentifiedResult finalResult = resultBuilder.Build(finalGroups) as IdentifiedResult;

      resultFormat.WriteToFile(FileUtils.ChangeExtension(parameterFile, ".noredundant"), finalResult);

      CalculateGroupTIC(finalGroups);

      var calc = conf.FalseDiscoveryRate.GetFalseDiscoveryRateCalculator();

      CalculateGroupQValue(finalGroups, calc, new Regex(conf.Database.DecoyPattern));

      finalResult.Sort((m1, m2) => m2.TotalIonCount.CompareTo(m1.TotalIonCount));

      var noredundantFile = FileUtils.ChangeExtension(parameterFile, ".sorted.arff");
      using (StreamWriter sw = new StreamWriter(noredundantFile))
      {
        sw.WriteLine("@relation proteinfdr");
        //sw.WriteLine("@attribute name string");
        sw.WriteLine("@attribute uniquecount numeric");
        sw.WriteLine("@attribute tic numeric");
        sw.WriteLine("@attribute qvalue numeric");
        sw.WriteLine("@attribute fromdecoy {True, False}");
        sw.WriteLine("@data");
        foreach (var group in finalResult)
        {
          sw.WriteLine("{0},{1:0.00},{2:0.00},{3}",
            Math.Log(group[0].UniquePeptideCount),
            Math.Log(group.TotalIonCount),
            group.QValue,
            group.FromDecoy);
        }
      }

      result.Add(noredundantFile);

      Progress.SetMessage("Finished!");

      return result;
    }

    private void CalculateGroupQValue(List<IIdentifiedProteinGroup> groups, IFalseDiscoveryRateCalculator fdrCalc, Regex decoyReg)
    {
      //根据TotalIonCount进行从高到低排序
      groups.Sort((m1, m2) => m2.TotalIonCount.CompareTo(m1.TotalIonCount));
      groups.ForEach(m => m.FromDecoy = m.Any(n => decoyReg.Match(n.Name).Success));

      int totalTarget = 0;
      int totalDecoy = 0;
      foreach (var group in groups)
      {
        group.QValue = 0.0;

        if (group.FromDecoy)
        {
          totalDecoy++;
        }
        else
        {
          totalTarget++;
        }
      }

      double lastTIC = groups.Last().TotalIonCount;
      double lastQvalue = fdrCalc.Calculate(totalDecoy, totalTarget);
      for (int i = groups.Count - 1; i >= 0; i--)
      {
        double score = groups[i].TotalIonCount;
        if (score != lastTIC)
        {
          lastTIC = score;
          lastQvalue = fdrCalc.Calculate(totalDecoy, totalTarget);
          if (lastQvalue == 0.0)
          {
            break;
          }
          groups[i].QValue = lastQvalue;
        }
        else
        {
          groups[i].QValue = lastQvalue;
        }

        if (groups[i].FromDecoy)
        {
          totalDecoy--;
        }
        else
        {
          totalTarget--;
        }
      }

    }

    private void CalculateGroupTIC(List<IIdentifiedProteinGroup> finalGroups)
    {
      //计算每个肽段的共享数
      finalGroups.InitializeGroupCount();

      foreach (var group in finalGroups)
      {
        group.TotalIonCount = 0;
        var spectra = group.GetPeptides();
        foreach (var pep in spectra)
        {
          group.TotalIonCount += pep.MatchedTIC / pep.TheoreticalIonCount / pep.GroupCount;
        }
      }
    }

    private void CalculateIsoelectricPoint(List<IIdentifiedProtein> proteins)
    {
      foreach (IIdentifiedProtein protein in proteins)
      {
        if (string.IsNullOrEmpty(protein.Sequence))
        {
          continue;
        }

        protein.IsoelectricPoint = IsoelectricPointCalculator.GetIsoelectricPoint(protein.Sequence);
      }
    }

    private void CalculateIsoelectricPoint(List<IIdentifiedSpectrum> finalPeptides)
    {
      foreach (IIdentifiedSpectrum spectrum in finalPeptides)
      {
        spectrum.IsoelectricPoint = IsoelectricPointCalculator.GetIsoelectricPoint(spectrum.Peptide.PureSequence);
      }
    }

    protected void WriteFdrFile(string parameterFile, BuildSummaryOptions conf, List<IIdentifiedSpectrum> result)
    {
      Progress.SetMessage("Calculating identified peptide false discovery rate ...");

      IFalseDiscoveryRateCalculator calc = conf.FalseDiscoveryRate.GetFalseDiscoveryRateCalculator();

      DecoyPeptideBuilder.AssignDecoy(result, conf.GetDecoySpectrumFilter());

      int decoyCount = 0;
      int targetCount = 0;

      foreach (IIdentifiedSpectrum mph in result)
      {
        if (mph.FromDecoy)
        {
          decoyCount++;
        }
        else
        {
          targetCount++;
        }
      }

      double fdr = calc.Calculate(decoyCount, targetCount);

      string optimalResultFile = FileUtils.ChangeExtension(parameterFile, ".optimal");

      using (var sw = new StreamWriter(optimalResultFile))
      {
        List<string> filters = conf.GetFilterString();
        foreach (string filter in filters)
        {
          sw.WriteLine(filter);
        }

        sw.WriteLine("DecoyCount\t{0}", decoyCount);
        sw.WriteLine("TargetCount\t{0}", targetCount);
        sw.WriteLine("FDR\t{0:0.######}", fdr);
      }
    }
  }
}