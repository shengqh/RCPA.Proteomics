using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using RCPA.Proteomics.Summary;
using RCPA.Utils;
using RCPA.Gui;
using RCPA.Proteomics.Mascot;
using System.Threading.Tasks;
using System.Threading;
using System.Collections.Concurrent;
using System.Text;

namespace RCPA.Proteomics.Summary.Uniform
{
  public class MascotDatasetBuilder : AbstractDatasetBuilder<MascotDatasetOptions>
  {
    private readonly ReaderWriterLockSlim _readerWriterLockSlim = new ReaderWriterLockSlim();

    private int _finishedCount = 0;

    private int FinishedCount
    {
      get
      {
        _readerWriterLockSlim.EnterReadLock();
        try
        {
          return _finishedCount;
        }
        finally
        {
          _readerWriterLockSlim.ExitReadLock();
        }
      }
      set
      {
        _readerWriterLockSlim.EnterWriteLock();
        try
        {
          _finishedCount = value;
        }
        finally
        {
          _readerWriterLockSlim.ExitWriteLock();
        }
      }
    }

    public MascotDatasetBuilder(MascotDatasetOptions options)
      : base(options)
    { }

    protected override List<IIdentifiedSpectrum> DoParse()
    {
      Progress.SetMessage("Parsing dat files ...");
      Progress.SetRange(0, options.PathNames.Count + 1);
      FinishedCount = 0;

      if (options.PathNames.Count > 1)
      {
        var exceptions = new ConcurrentQueue<Exception>();

        Parallel.ForEach(options.PathNames, new ParallelOptions { MaxDegreeOfParallelism = Environment.ProcessorCount }, (datFile, loopState) =>
        {
          var datParser = new MascotDatSpectrumParser(options.GetTitleParser())
          {
            Progress = this.Progress
          };

          var peptideFormat = new MascotPeptideTextFormat();

          string peptideFilename = FileUtils.ChangeExtension(datFile, ".peptides");
          if (!new FileInfo(peptideFilename).Exists)
          {
            try
            {
              var curPeptides = datParser.ParsePeptides(datFile);
              peptideFormat.WriteToFile(peptideFilename, curPeptides);
              FinishedCount++;
              Progress.SetPosition(FinishedCount);
            }
            catch (UserTerminatedException)
            {
              loopState.Stop();
            }
            catch (Exception e)
            {
              exceptions.Enqueue(e);
              loopState.Stop();
            }
          }

          GC.Collect();
          GC.WaitForPendingFinalizers();
        });

        if (Progress.IsCancellationPending())
        {
          throw new UserTerminatedException();
        }

        if (exceptions.Count > 0)
        {
          if (exceptions.Count == 1)
          {
            throw new Exception(exceptions.First().Message, exceptions.First());
          }
          else
          {
            StringBuilder sb = new StringBuilder();
            foreach (var ex in exceptions)
            {
              sb.AppendLine(ex.Message);
            }
            throw new Exception(sb.ToString());
          }
        }
      }
      else
      {
        var datParser = new MascotDatSpectrumParser(options.GetTitleParser())
        {
          Progress = this.Progress
        };

        var peptideFormat = new MascotPeptideTextFormat();

        var datFile = options.PathNames[0];
        string peptideFilename = FileUtils.ChangeExtension(datFile, ".peptides");
        if (!new FileInfo(peptideFilename).Exists)
        {
          var curPeptides = datParser.ParsePeptides(datFile);
          peptideFormat.WriteToFile(peptideFilename, curPeptides);
          FinishedCount++;
          Progress.SetPosition(FinishedCount);
        }
      }

      var result = new List<IIdentifiedSpectrum>();

      Progress.SetMessage("Reading peptide files ...");
      Progress.SetRange(0, options.PathNames.Count + 1);
      FinishedCount = 0;
      Parallel.ForEach(options.PathNames, new ParallelOptions { MaxDegreeOfParallelism = Environment.ProcessorCount }, (datFile, loopState) =>
      {
        if (Progress.IsCancellationPending())
        {
          loopState.Stop();
          return;
        }

        IFilter<IIdentifiedSpectrum> spectrumFilter = options.GetFilter();

        var peptideFormat = new MascotPeptideTextFormat();

        List<IIdentifiedSpectrum> curPeptides;

        string peptideFilename = FileUtils.ChangeExtension(datFile, ".peptides");
        curPeptides = peptideFormat.ReadFromFile(peptideFilename);

        if (null != spectrumFilter)
        {
          curPeptides.RemoveAll(m => !spectrumFilter.Accept(m));
        }

        curPeptides.ForEach(m =>
        {
          m.Tag = options.Name;
          m.Engine = "MASCOT";
        });

        _readerWriterLockSlim.EnterWriteLock();
        try
        {
          result.AddRange(curPeptides);
          _finishedCount++;
        }
        finally
        {
          _readerWriterLockSlim.ExitWriteLock();
        }

        Progress.SetPosition(FinishedCount);

        curPeptides = null;
        GC.Collect();
        GC.WaitForPendingFinalizers();
      });

      return result;
    }

    public override IScoreFunctions GetScoreFunctions()
    {
      return new MascotScoreFunctions();
    }

    protected override List<IIdentifiedSpectrum> GetHighConfidentPeptides(List<IIdentifiedSpectrum> source)
    {
      return (from pep in source
              where pep.ExpectValue < 0.001
              select pep).ToList();
    }
  }
}