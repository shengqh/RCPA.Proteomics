using System.Collections.Generic;
using System.IO;
using System.Linq;
using RCPA.Proteomics.Utils;
using RCPA.Utils;
using RCPA.Gui;
using RCPA.Proteomics.Mascot;
using RCPA.Proteomics.Statistic;

namespace RCPA.Proteomics.Summary.Uniform
{
  public class UniformIdentifiedResultBuilder : AbstractThreadFileProcessor
  {
    private string peptideFile;

    public UniformIdentifiedResultBuilder()
    {
      this.peptideFile = null;
    }

    public UniformIdentifiedResultBuilder(string peptideFile)
    {
      this.peptideFile = peptideFile;
    }

    public override IEnumerable<string> Process(string parameterFile)
    {
      var result = new List<string>();

      BuildSummaryOptions conf = BuildSummaryOptionsUtils.LoadFromFile(parameterFile);

      if (!conf.MergeResult)
      {
        for (int i = 0; i < conf.DatasetList.Count; i++)
        {
          for (int j = 0; j < conf.DatasetList[i].PathNames.Count; j++)
          {
            var curConf = BuildSummaryOptionsUtils.LoadFromFile(parameterFile);
            for (int k = curConf.DatasetList.Count - 1; k >= 0; k--)
            {
              if (k != i)
              {
                curConf.DatasetList.RemoveAt(k);
              }
            }

            for (int k = curConf.DatasetList[0].PathNames.Count - 1; k >= 0; k--)
            {
              if (k != j)
              {
                curConf.DatasetList[0].PathNames.RemoveAt(k);
              }
            }

            curConf.MergeResult = false;
            var curFile = curConf.DatasetList[0].PathNames[0];

            var curParamFile = Path.GetDirectoryName(parameterFile) + "\\" + Path.ChangeExtension(new FileInfo(curFile).Name, ".param");
            curConf.SaveToFile(curParamFile);

            RunCurrentParameter(curParamFile, result, curConf);
          }
        }
      }
      else
      {
        RunCurrentParameter(parameterFile, result, conf);
      }

      return result;
    }

    private void RunCurrentParameter(string parameterFile, List<string> result, BuildSummaryOptions conf)
    {
      IStringParser<string> acParser = conf.Database.GetAccessNumberParser();

      IIdentifiedProteinBuilder proteinBuilder = new IdentifiedProteinBuilder();
      IIdentifiedProteinGroupBuilder groupBuilder = new IdentifiedProteinGroupBuilder()
      {
        Progress = this.Progress
      };

      var resultBuilder = new IdentifiedResultBuilder(acParser, conf.Database.Location);
      resultBuilder.Progress = Progress;

      List<IIdentifiedSpectrum> finalPeptides;

      if (this.peptideFile == null)
      { //parse from configuration
        //build spectrum list
        IIdentifiedSpectrumBuilder spectrumBuilder = conf.GetSpectrumBuilder();
        if (spectrumBuilder is IProgress)
        {
          (spectrumBuilder as IProgress).Progress = this.Progress;
        }

        finalPeptides = spectrumBuilder.Build(parameterFile);
      }
      else
      {
        finalPeptides = new MascotPeptideTextFormat().ReadFromFile(this.peptideFile);
        conf.SavePeptidesFile = false;
      }

      CalculateIsoelectricPoint(finalPeptides);

      //如果需要通过蛋白质注释去除contamination，首先需要在肽段水平删除
      if (conf.Database.HasContaminationDescriptionFilter() && (conf.FalseDiscoveryRate.FdrLevel != FalseDiscoveryRateLevel.Protein))
      {
        Progress.SetMessage("Removing contamination by description ...");
        var notConGroupFilter = conf.Database.GetNotContaminationDescriptionFilter(Progress);

        while (true)
        {
          List<IIdentifiedProtein> proteins = proteinBuilder.Build(finalPeptides);
          List<IIdentifiedProteinGroup> groups = groupBuilder.Build(proteins);
          IIdentifiedResult tmpResult = resultBuilder.Build(groups);

          HashSet<IIdentifiedSpectrum> notConSpectra = new HashSet<IIdentifiedSpectrum>();
          foreach (var group in tmpResult)
          {
            if (notConGroupFilter.Accept(group))
            {
              notConSpectra.UnionWith(group[0].GetSpectra());
            }
          }

          if (notConSpectra.Count == finalPeptides.Count)
          {
            break;
          }
          finalPeptides = notConSpectra.ToList();
        }
      }

      if (conf.SavePeptidesFile)
      {
        if (conf.Database.RemovePeptideFromDecoyDB)
        {
          DecoyPeptideBuilder.AssignDecoy(finalPeptides, conf.GetDecoySpectrumFilter());
          for (int i = finalPeptides.Count - 1; i >= 0; i--)
          {
            if (finalPeptides[i].FromDecoy)
            {
              finalPeptides.RemoveAt(i);
            }
          }
        }

        finalPeptides.Sort();

        //保存肽段文件
        IFileFormat<List<IIdentifiedSpectrum>> peptideFormat = conf.GetIdentifiedSpectrumFormat();
        string peptideFile = FileUtils.ChangeExtension(parameterFile, ".peptides");
        Progress.SetMessage("Writing peptides file...");
        peptideFormat.WriteToFile(peptideFile, finalPeptides);
        result.Add(peptideFile);

        if (!conf.FalseDiscoveryRate.FilterByFdr && conf.Database.DecoyPatternDefined)
        {
          WriteFdrFile(parameterFile, conf, finalPeptides);
        }

        Progress.SetMessage("Calculating precursor offset...");
        new PrecursorOffsetCalculator().Process(peptideFile);
        result.AddRange(new PrecursorOffsetCalculator().Process(peptideFile));
      }

      Progress.SetMessage("Building protein...");
      //构建蛋白质列表
      List<IIdentifiedProtein> finalProteins = proteinBuilder.Build(finalPeptides);

      Progress.SetMessage("Building protein group...");
      //构建蛋白质群列表
      List<IIdentifiedProteinGroup> finalGroups = groupBuilder.Build(finalProteins);

      //构建最终鉴定结果
      IIdentifiedResult finalResult = resultBuilder.Build(finalGroups);

      if (conf.Database.HasContaminationDescriptionFilter())
      {
        var notConGroupFilter = conf.Database.GetNotContaminationDescriptionFilter(Progress);

        for (int i = finalResult.Count - 1; i >= 0; i--)
        {
          if (!notConGroupFilter.Accept(finalResult[i]))
          {
            finalResult.RemoveAt(i);
          }
        }

        finalResult.BuildGroupIndex();
      }

      CalculateIsoelectricPoint(finalResult.GetProteins());

      //保存非冗余蛋白质列表文件
      IFileFormat<IIdentifiedResult> resultFormat = conf.GetIdetifiedResultFormat();

      if (resultFormat is ProgressClass)
      {
        (resultFormat as ProgressClass).Progress = this.Progress;
      }

      string noredundantFile = FileUtils.ChangeExtension(parameterFile, ".noredundant");
      Progress.SetMessage("Writing noredundant file...");
      resultFormat.WriteToFile(noredundantFile, finalResult);
      result.Add(noredundantFile);

      Progress.SetMessage("Finished!");
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