using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using RCPA.Proteomics.Summary;
using RCPA.Utils;
using RCPA.Gui;
using RCPA.Proteomics.Mascot;
using RCPA.Proteomics.PeptideProphet;

namespace RCPA.Proteomics.Summary.Uniform
{
  public class PeptideProphetDatasetBuilder : AbstractDatasetBuilder<PeptideProphetDatasetOptions>
  {
    public PeptideProphetDatasetBuilder(PeptideProphetDatasetOptions options)
      : base(options)
    { }

    protected override List<IIdentifiedSpectrum> DoParse()
    {
      Progress.SetRange(0, options.PathNames.Count + 1);

      var peptideFormat = new MascotPeptideTextFormat(MascotHeader.PEPTIDEPROPHET_PEPTIDE_HEADER);

      var result = new List<IIdentifiedSpectrum>();

      IFilter<IIdentifiedSpectrum> spectrumFilter = options.GetFilter();

      var datParser = new PeptideProphetXmlReader(options.GetTitleParser());
      datParser.Progress = Progress;

      int stepCount = 0;
      foreach (string xmlFile in options.PathNames)
      {
        stepCount++;

        if (Progress.IsCancellationPending())
        {
          throw new UserTerminatedException();
        }

        List<IIdentifiedSpectrum> curPeptides;

        string peptideFilename = FileUtils.ChangeExtension(xmlFile, ".peptides");
        if (new FileInfo(peptideFilename).Exists)
        {
          Progress.SetMessage(MyConvert.Format("{0}/{1} : Reading peptides file {2}", stepCount, options.PathNames.Count,
                                            peptideFilename));
          curPeptides = peptideFormat.ReadFromFile(peptideFilename);
        }
        else
        {
          Progress.SetMessage(MyConvert.Format("{0}/{1} : Parsing xml file {2}", stepCount, options.PathNames.Count, xmlFile));
          curPeptides = datParser.ReadFromFile(xmlFile);
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
      return new PeptideProphetScoreFunction();
    }

    protected override List<IIdentifiedSpectrum> GetHighConfidentPeptides(List<IIdentifiedSpectrum> source)
    {
      return (from pep in source
              where pep.PValue >= 0.99
              select pep).ToList();
    }
  }
}