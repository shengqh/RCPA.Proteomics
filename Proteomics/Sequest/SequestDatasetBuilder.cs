using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using RCPA.Proteomics.Summary;
using RCPA.Utils;
using System.Diagnostics;
using RCPA.Seq;
using RCPA.Gui;
using RCPA.Proteomics.Sequest;
using RCPA.Proteomics.Comet;
using RCPA.Proteomics.ProteomeDiscoverer;
using RCPA.Proteomics.Summary.Uniform;

namespace RCPA.Proteomics.Sequest
{
  public class SequestDatasetBuilder : AbstractDatasetBuilder<SequestDatasetOptions>
  {
    public SequestDatasetBuilder(SequestDatasetOptions options) :
      base(options)
    { }

    protected override List<IIdentifiedSpectrum> DoParse()
    {
      IAccessNumberParser parser = options.Parent.Database.GetAccessNumberParser();

      var peptideFormat = new SequestPeptideTextFormat()
      {
        Progress = this.Progress
      };

      Progress.SetRange(0, options.PathNames.Count + 1);

      var result = new List<IIdentifiedSpectrum>();

      IFilter<IIdentifiedSpectrum> spectrumFilter = options.GetFilter();

      SequestOutDirectoryParser outDirParser;
      SequestOutsParser outsParser;
      SequestOutZipParser outZipParser;
      string modStr = "";
      if (options.SkipSamePeptideButDifferentModificationSite)
      {
        modStr = MyConvert.Format(".M{0:0.00}", options.MaxModificationDeltaCn);

        outsParser = new SequestOutsParser(true, options.MaxModificationDeltaCn);

        outDirParser = new SequestOutDirectoryParser(true, options.MaxModificationDeltaCn);

        outZipParser = new SequestOutZipParser(true, options.MaxModificationDeltaCn);
      }
      else
      {
        outsParser = new SequestOutsParser(true);

        outDirParser = new SequestOutDirectoryParser(true);

        outZipParser = new SequestOutZipParser(true);
      }
      outsParser.Progress = Progress;
      outDirParser.Progress = Progress;
      outZipParser.Progress = Progress;

      long afterFirstMemory = 0;
      DateTime afterFirstTime = DateTime.Now;

      int stepCount = 0;
      foreach (string pathName in options.PathNames)
      {
        stepCount++;

        if (Progress.IsCancellationPending())
        {
          throw new UserTerminatedException();
        }

        AbstractSequestSpectraDistiller distiller;
        string engine;
        if (Directory.Exists(pathName))
        {
          var dir = new DirectoryInfo(pathName);

          if (dir.GetFiles("*.outs").Length > 0 || dir.GetFiles("*.outs.zip").Length > 0)
          {
            distiller = new SequestOutsDistiller(outsParser, peptideFormat);
          }
          else
          {
            distiller = new SequestOutDirectoryDistiller(outDirParser, peptideFormat);
          }
          engine = "SEQUEST";
        }
        else if (pathName.ToLower().EndsWith(".xml"))
        {
          distiller = new CometSpectraDistiller(peptideFormat);
          engine = "COMET";
        }
        else if (pathName.ToLower().EndsWith(".msf"))
        {
          distiller = new MsfSpectraDistiller(peptideFormat);
          engine = "PD";
        }
        else //zipfile
        {
          ISpectrumParser zipParser;
          if (ZipUtils.HasFile(pathName, m => m.ToLower().EndsWith(".out")))
          {
            zipParser = outZipParser;
          }
          else
          {
            zipParser = outsParser;
          }
          distiller = new SequestOutZipDistiller(zipParser, peptideFormat);
          engine = "SEQUEST";
        }

        distiller.Progress = this.Progress;

        List<IIdentifiedSpectrum> curPeptides = distiller.ParseSpectra(pathName, modStr, stepCount, options.PathNames.Count);
        int curPeptideCount = curPeptides.Count;

        if (null != spectrumFilter)
        {
          curPeptides.RemoveAll(m => !spectrumFilter.Accept(m));
        }

        curPeptides.ForEach(m =>
        {
          m.Tag = options.Name;
          m.Engine = engine;
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
  }
}