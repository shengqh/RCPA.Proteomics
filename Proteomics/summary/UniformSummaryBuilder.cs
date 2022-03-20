using RCPA.Gui;
using RCPA.Proteomics.Mascot;
using RCPA.Proteomics.Statistic;
using RCPA.Proteomics.Summary.Uniform;
using RCPA.Proteomics.Utils;
using RCPA.Utils;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace RCPA.Proteomics.Summary
{
  public class UniformSummaryBuilder : AbstractThreadProcessor
  {
    private UniformSummaryBuilderOptions options;

    public UniformSummaryBuilder(UniformSummaryBuilderOptions options)
    {
      this.options = options;
    }

    public override IEnumerable<string> Process()
    {
      var result = new List<string>();

      BuildSummaryOptions conf = BuildSummaryOptionsUtils.LoadFromFile(options.InputFile);

      if (!conf.MergeResult)
      {
        for (int i = 0; i < conf.DatasetList.Count; i++)
        {
          for (int j = 0; j < conf.DatasetList[i].PathNames.Count; j++)
          {
            var curConf = BuildSummaryOptionsUtils.LoadFromFile(options.InputFile);
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

            var curParamFile = Path.GetDirectoryName(options.InputFile) + "\\" + Path.ChangeExtension(new FileInfo(curFile).Name, ".param");
            curConf.SaveToFile(curParamFile);

            RunCurrentParameter(curParamFile, result, curConf);
          }
        }
      }
      else
      {
        RunCurrentParameter(options.InputFile, result, conf);
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

      IdentifiedSpectrumBuilderResult isbr;

      List<IIdentifiedSpectrum> finalPeptides;

      if (string.IsNullOrEmpty(options.PeptideFile))
      { //parse from configuration
        //build spectrum list
        IIdentifiedSpectrumBuilder spectrumBuilder = conf.GetSpectrumBuilder();
        if (spectrumBuilder is IProgress)
        {
          (spectrumBuilder as IProgress).Progress = this.Progress;
        }

        isbr = spectrumBuilder.Build(parameterFile);
        finalPeptides = isbr.Spectra;
      }
      else
      {
        Progress.SetMessage("Reading peptides from {0} ...", options.PeptideFile);
        finalPeptides = new MascotPeptideTextFormat().ReadFromFile(options.PeptideFile);
        conf.SavePeptidesFile = false;
        isbr = null;
      }

      CalculateIsoelectricPoint(finalPeptides);

      //如果需要通过蛋白质注释去除contamination，首先需要在肽段水平删除
      if (conf.Database.HasContaminationDescriptionFilter() && (conf.FalseDiscoveryRate.FdrLevel != FalseDiscoveryRateLevel.Protein))
      {
        Progress.SetMessage("Removing contamination by description ...");
        var notConGroupFilter = conf.Database.GetNotContaminationDescriptionFilter(Progress);

        var tempResultBuilder = new IdentifiedResultBuilder(null, null);
        while (true)
        {
          List<IIdentifiedProtein> proteins = proteinBuilder.Build(finalPeptides);
          List<IIdentifiedProteinGroup> groups = groupBuilder.Build(proteins);
          IIdentifiedResult tmpResult = tempResultBuilder.Build(groups);

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

      if (conf.FalseDiscoveryRate.FilterOneHitWonder && conf.FalseDiscoveryRate.MinOneHitWonderPeptideCount > 1)
      {
        Progress.SetMessage("Filtering single wonders ...");
        var proteinFilter = new IdentifiedProteinSingleWonderPeptideCountFilter(conf.FalseDiscoveryRate.MinOneHitWonderPeptideCount);
        List<IIdentifiedProtein> proteins = proteinBuilder.Build(finalPeptides);
        int oldProteinCount = proteins.Count;
        proteins.RemoveAll(l => !proteinFilter.Accept(l));
        if (oldProteinCount != proteins.Count)
        {
          HashSet<IIdentifiedSpectrum> newspectra = new HashSet<IIdentifiedSpectrum>();
          foreach (var protein in proteins)
          {
            newspectra.UnionWith(protein.GetSpectra());
          }
          finalPeptides = newspectra.ToList();
        }
      }

      //if (conf.SavePeptidesFile && !(conf.FalseDiscoveryRate.FilterOneHitWonder && conf.FalseDiscoveryRate.MinOneHitWonderPeptideCount > 1))
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
        result.AddRange(new PrecursorOffsetCalculator(finalPeptides).Process(peptideFile));
      }

      Progress.SetMessage("Building protein...");
      //构建蛋白质列表
      List<IIdentifiedProtein> finalProteins = proteinBuilder.Build(finalPeptides);

      Progress.SetMessage("Building protein group...");
      //构建蛋白质群列表
      List<IIdentifiedProteinGroup> finalGroups = groupBuilder.Build(finalProteins);
      if (conf.Database.HasContaminationDescriptionFilter())
      {
        var notConGroupFilter = conf.Database.GetNotContaminationDescriptionFilter(Progress);

        for (int i = finalGroups.Count - 1; i >= 0; i--)
        {
          if (!notConGroupFilter.Accept(finalGroups[i]))
          {
            finalGroups.RemoveAt(i);
          }
        }
      }

      //构建最终鉴定结果
      var resultBuilder = conf.GetIdentifiedResultBuilder();
      resultBuilder.Progress = Progress;
      IIdentifiedResult finalResult = resultBuilder.Build(finalGroups);
      finalResult.BuildGroupIndex();

      if (conf.FalseDiscoveryRate.FilterByFdr)
      {
        var decoyGroupFilter = conf.GetDecoyGroupFilter();
        foreach (var group in finalResult)
        {
          group.FromDecoy = decoyGroupFilter.Accept(group);
          foreach (var protein in group)
          {
            protein.FromDecoy = group.FromDecoy;
          }
        }

        finalResult.ProteinFDR = conf.FalseDiscoveryRate.GetFalseDiscoveryRateCalculator().Calculate(finalResult.Count(l => l[0].FromDecoy), finalResult.Count(l => !l[0].FromDecoy));
      }

      CalculateIsoelectricPoint(finalResult.GetProteins());
      if (isbr != null)
      {
        finalResult.PeptideFDR = isbr.PeptideFDR;
      }

      //保存非冗余蛋白质列表文件

      var resultFormat = conf.GetIdetifiedResultFormat(finalResult, this.Progress);

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