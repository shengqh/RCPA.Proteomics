using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RCPA.Seq;
using RCPA.Proteomics.Summary;
using RCPA.Proteomics.Sequest;
using System.IO;
using RCPA.Utils;
using RCPA.Proteomics.Utils;
using RCPA.Proteomics.Mascot;

namespace RCPA.Proteomics.Distribution
{
  public class PeptideDistributionCalculator : AbstractDistributionCalculator
  {
    protected IAccessNumberParser parser;

    public PeptideDistributionCalculator(bool exportIndividual)
      : base("Peptide", exportIndividual)
    { }

    private MascotPeptideTextFormat format = new MascotPeptideTextFormat();

    protected override void ParseToCalculationItems()
    {
      List<IIdentifiedSpectrum> spectra = format.ReadFromFile(option.SourceFileName);

      IEnumerable<IIdentifiedSpectrum> filteredSpectra = FilterSpectrum(spectra);

      calculationItems =
        (from entry in
           (from spectrum in filteredSpectra
            let pep = spectrum.Peptide
            group pep by pep.Sequence)
         select new CalculationItem()
         {
           Key = entry.Key,
           Peptides = entry
         }).ToList();
    }

    protected virtual IEnumerable<IIdentifiedSpectrum> FilterSpectrum(List<IIdentifiedSpectrum> spectra)
    {
      return spectra;
    }

    protected override void PrintHeader(StreamWriter pw)
    {
      pw.Write("Sequence");

      printClassifiedNames(pw, true);
    }

    protected override void PrintItem(StreamWriter pw, CalculationItem calculationItem)
    {
      pw.Write("{0}", calculationItem.Key);

      foreach (var s in option.GetClassifiedNames())
      {
        Count count = calculationItem.Classifications[s];
        pw.Write("\t" + count.PeptideCount);
        pw.Write("\t" + GetRank(peptideCounts[s], count.PeptideCount));
      }
    }

    /// <summary>
    /// 输出每次条件下，每个fraction的protein group文件
    /// </summary>
    protected override void ExportIndividualFractionFile()
    {
      DirectoryInfo individualDir = new DirectoryInfo(resultDir.FullName + "\\individual");
      FileInfo sourceFile = new FileInfo(option.SourceFileName);

      var writeFormat = GetWriteFormat();

      for (int iMinCount = option.FilterFrom; iMinCount <= option.FilterTo; iMinCount += option.FilterStep)
      {
        List<CalculationItem> currentItems = GetFilteredItems(iMinCount);

        if (!individualDir.Exists)
        {
          individualDir.Create();
        }

        foreach (string keptClassifiedName in option.GetClassifiedNames())
        {
          string result_file = MyConvert.Format(@"{0}\{1}.{2}.{3}{4}",
            individualDir.FullName,
            FileUtils.ChangeExtension(sourceFile.Name, ""),
            GetOptionCondition(iMinCount),
            keptClassifiedName,
            sourceFile.Extension);

          List<IIdentifiedSpectrum> spectra = new List<IIdentifiedSpectrum>();
          foreach (var item in currentItems)
          {
            if (item.GetClassifiedCount(keptClassifiedName) >= iMinCount)
            {
              var classifiedSpectra = (from p in item.Peptides
                                      let key = sphc.GetClassification(p)
                                      where key == keptClassifiedName
                                      select p.Spectrum).Distinct();
              spectra.AddRange(classifiedSpectra);
            }
          }

          writeFormat.WriteToFile(result_file, spectra);
        }
      }
    }

    private MascotPeptideTextFormat GetWriteFormat()
    {
      List<string> peptides = format.PeptideFormat.GetHeader().Split(new char[] { '\t' }).ToList();

      peptides.Remove("GroupCount");
      peptides.Remove("ProteinCount");

      return new MascotPeptideTextFormat(StringUtils.Merge(peptides, "\t"));
    }
  }
}