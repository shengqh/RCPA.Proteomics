using System;
using System.Collections.Generic;
using System.IO;
using RCPA.Proteomics.Summary;
using RCPA.Utils;
using RCPA.Gui;
using RCPA.Proteomics.Mascot;
using System.Diagnostics;

namespace RCPA.Proteomics.Summary.Uniform
{
  public abstract class AbstractOneParserDatasetBuilder<T> : AbstractDatasetBuilder<T> where T : IDatasetOptions
  {
    public AbstractOneParserDatasetBuilder(T options) : base(options) { }

    #region IDatasetBuilder Members

    protected override List<IIdentifiedSpectrum> DoParse()
    {
      var peptideFormat = new MascotPeptideTextFormat();

      Progress.SetRange(0, options.PathNames.Count + 1);

      var result = new List<IIdentifiedSpectrum>();

      var spectrumFilter = options.GetFilter();

      long afterFirstMemory = 0;
      DateTime afterFirstTime = DateTime.Now;

      int stepCount = 0;
      foreach (string dataFile in options.PathNames)
      {
        stepCount++;

        if (Progress.IsCancellationPending())
        {
          throw new UserTerminatedException();
        }

        var dataParser = Options.SearchEngine.GetFactory().GetParser(dataFile);

        dataParser.Progress = this.Progress;

        if (options is AbstractTitleDatasetOptions)
        {
          dataParser.TitleParser = (options as AbstractTitleDatasetOptions).GetTitleParser();
        }

        List<IIdentifiedSpectrum> curPeptides;

        string peptideFilename = dataFile + ".peptides";
        if (new FileInfo(peptideFilename).Exists)
        {
          Progress.SetMessage(MyConvert.Format("{0}/{1} : Reading peptides file {2}", stepCount, options.PathNames.Count, peptideFilename));
          curPeptides = peptideFormat.ReadFromFile(peptideFilename);
        }
        else
        {
          Progress.SetMessage(MyConvert.Format("{0}/{1} : Parsing {2} file {3}", stepCount, options.PathNames.Count, dataParser.Engine, dataFile));
          curPeptides = dataParser.ReadFromFile(dataFile);
          peptideFormat.WriteToFile(peptideFilename, curPeptides);
        }

        int curPeptideCount = curPeptides.Count;

        if (null != spectrumFilter)
        {
          curPeptides.RemoveAll(m => !spectrumFilter.Accept(m));
        }

        curPeptides.ForEach(m =>
        {
          m.Tag = options.Name;
          m.Engine = options.SearchEngine.ToString();
        });

        result.AddRange(curPeptides);
        curPeptides = null;
        GC.Collect();
        GC.WaitForPendingFinalizers();

        if (stepCount == 1)
        {
          afterFirstMemory = Process.GetCurrentProcess().WorkingSet64 / (1024 * 1024);
          afterFirstTime = DateTime.Now;
        }
        else
        {
          long currMemory = Process.GetCurrentProcess().WorkingSet64 / (1024 * 1024);
          double averageCost = (double)(currMemory - afterFirstMemory) / (stepCount - 1);
          double estimatedCost = afterFirstMemory + averageCost * options.PathNames.Count;

          DateTime currTime = DateTime.Now;
          var averageTime = currTime.Subtract(afterFirstTime).TotalMinutes / (stepCount - 1);
          var finishTime = afterFirstTime.AddMinutes(averageTime * (options.PathNames.Count - 1));
          Console.WriteLine("{0}/{1}, cost {2}M, avg {3:0.0}M, need {4:0.0}M, will finish at {5:MM-dd HH:mm:ss}", stepCount, options.PathNames.Count, currMemory, averageCost, estimatedCost, finishTime.ToString());
        }
      }
      return result;
    }

    #endregion
  }
}