using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using RCPA.Proteomics.Summary;
using RCPA.Utils;
using RCPA.Gui;
using RCPA.Proteomics.PFind;
using RCPA.Proteomics.Mascot;

namespace RCPA.Proteomics.Summary.Uniform
{
  public class PFindDatasetBuilder : AbstractDatasetBuilder<PFindDatasetOptions>
  {
    public PFindDatasetBuilder(PFindDatasetOptions options)
      : base(options)
    { }

    protected override List<IIdentifiedSpectrum> DoParse()
    {
      var peptideFormat = new MascotPeptideTextFormat();

      Progress.SetRange(0, options.PathNames.Count + 1);

      var result = new List<IIdentifiedSpectrum>();

      IFilter<IIdentifiedSpectrum> spectrumFilter = options.GetFilter();

      var datParser = new PFindSpectrumParser(options.GetTitleParser());
      datParser.Progress = Progress;

      int stepCount = 0;
      foreach (string datFile in options.PathNames)
      {
        stepCount++;

        if (Progress.IsCancellationPending())
        {
          throw new UserTerminatedException();
        }

        List<IIdentifiedSpectrum> curPeptides;

        string peptideFilename = FileUtils.ChangeExtension(datFile, ".peptides");
        if (new FileInfo(peptideFilename).Exists)
        {
          Progress.SetMessage(MyConvert.Format("{0}/{1} : Reading peptides file {2}", stepCount, options.PathNames.Count,
                                            peptideFilename));
          curPeptides = peptideFormat.ReadFromFile(peptideFilename);
        }
        else
        {
          Progress.SetMessage(MyConvert.Format("{0}/{1} : Parsing pFind file {2}", stepCount, options.PathNames.Count, datFile));
          curPeptides = datParser.ParsePeptides(datFile);
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
          m.Engine = "PFIND";
        });

        result.AddRange(curPeptides);
        curPeptides = null;
        GC.Collect();
        GC.WaitForPendingFinalizers();
      }
      return result;
    }

    public override IScoreFunctions GetScoreFunctions()
    {
      return new PFindExpectValueFunctions();
    }

    protected override List<IIdentifiedSpectrum> GetHighConfidentPeptides(List<IIdentifiedSpectrum> source)
    {
      return (from pep in source
              where pep.ExpectValue < 0.001
              select pep).ToList();
    }
  }
}