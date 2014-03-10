using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RCPA.Proteomics.Summary;
using RCPA.Proteomics.Sequest;
using MathNet.Numerics.Statistics;
using System.IO;
using RCPA.Utils;
using RCPA.Proteomics.Utils;
using RCPA.Proteomics.Modification;
using RCPA.Proteomics;
using RCPA.Proteomics.Mascot;

namespace RCPA.Proteomics.Quantification.ITraq
{
  public class ITraqModifiedPeptideRsdFilter : AbstractITraqPeptideStatisticBuilder
  {
    private IITraqRatioCalculator calc;

    private string modifiedAminoacids;

    private double fold;
    private double logFold;

    public ITraqModifiedPeptideRsdFilter(string rawFileName, string modifiedAminoacids, IITraqRatioCalculator calc, double fold)
      : base(rawFileName)
    {
      this.calc = calc;
      this.modifiedAminoacids = modifiedAminoacids;
      this.fold = fold;
      this.logFold = Math.Log(fold);
    }

    public override IEnumerable<string> Process(string fileName)
    {
      //Progress.SetMessage("Reading peptides file...");
      //List<IIdentifiedSpectrum> spectra = new MascotPeptideTextFormat().ReadFromFile(fileName);

      //if (modifiedAminoacids != null && modifiedAminoacids.Length > 0)
      //{
      //  ModificationSpectrumFilter filter = new ModificationSpectrumFilter(modifiedAminoacids);
      //  spectra.RemoveAll(m => !filter.Accept(m));
      //}

      //Progress.SetMessage("Matching spectrum with itraq...");
      //var find = GetSpectrumITraqPairList(spectra);

      //find.RemoveAll(m => !calc.Valid(m.Value));
      //find.ForEach(m =>
      //{
      //  m.Key.Annotations[calc.GetRatioHeader()] = calc.GetRatioValue(m.Value);
      //  m.Key.SetITraqItem(m.Value);
      //});

      //List<IIdentifiedSpectrum> final =
      //  (from s in find
      //   orderby s.Key.Peptide.Sequence
      //   select s.Key).ToList();

      //MascotPeptideTextFormat format = new MascotPeptideTextFormat();
      //string peptideHeader = format.PeptideFormat.GetHeader() + "\t" + AnnotationUtils.GetAnnotationHeader(final);
      //format.PeptideFormat = new LineFormat<IIdentifiedSpectrum>(IdentifiedSpectrumPropertyConverterFactory.GetInstance(), peptideHeader, "");

      //Progress.SetMessage("Writing final spectra file...");
      //string resultFileName = FileUtils.ChangeExtension(fileName, MyConvert.Format("{0}.itraq{1}", modifiedAminoacids, new FileInfo(fileName).Extension));
      //format.WriteToFile(resultFileName, final);

      //final.RemoveAll(m => Math.Abs(Math.Log(MyConvert.ToDouble((string)m.Annotations[calc.GetRatioHeader()]))) < logFold);

      //string resultFileName2 = FileUtils.ChangeExtension(fileName, MyConvert.Format("{0}.itraq.fold{1}{2}", modifiedAminoacids, fold, new FileInfo(fileName).Extension));
      //format.WriteToFile(resultFileName2, final);

      //Progress.SetMessage("Finished.");

      //return new string[] { resultFileName, resultFileName2 };

      return null;
    }
  }
}
