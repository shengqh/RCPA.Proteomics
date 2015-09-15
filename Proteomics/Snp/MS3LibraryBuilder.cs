using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using RCPA.Proteomics.Mascot;
using RCPA.Proteomics.Raw;
using RCPA.Proteomics.Spectrum;

namespace RCPA.Proteomics.Snp
{
  public class MS3LibraryBuilder : AbstractThreadProcessor
  {
    private MS3LibraryBuilderOptions options;

    public MS3LibraryBuilder(MS3LibraryBuilderOptions options)
    {
      this.options = options;
    }

    public override IEnumerable<string> Process()
    {
      var format = new MascotPeptideTextFormat();
      var expPeptidesMap = format.ReadFromFile(options.PeptideFile).GroupBy(m => m.Query.FileScan.Experimental).ToDictionary(m => m.Key, m => m.ToList());
      var expRawfileMap = options.RawFiles.ToDictionary(m => Path.GetFileNameWithoutExtension(m));

      foreach (var exp in expPeptidesMap.Keys)
      {
        if (!expRawfileMap.ContainsKey(exp))
        {
          throw new Exception(string.Format("Raw file of {0} is not assigned in RawFiles.", exp));
        }
      }

      var ms2list = new List<MS2Item>();
      foreach (var exp in expPeptidesMap.Keys)
      {
        var rawfile = expRawfileMap[exp];
        var peptides = expPeptidesMap[exp];

        using (var reader = RawFileFactory.GetRawFileReader(rawfile, false))
        {
          var firstScan = reader.GetFirstSpectrumNumber();
          var lastScan = reader.GetLastSpectrumNumber();

          Progress.SetRange(0, peptides.Count);
          Progress.SetMessage("Extracting MS2/MS3 information ...");
          int count = 0;
          foreach (var peptide in peptides)
          {
            count++;
            Progress.SetPosition(count);

            var ms2 = new MS2Item()
            {
              Peptide = peptide.Peptide.Sequence,
              Precursor = peptide.GetPrecursorMz(),
              Charge = peptide.Query.Charge,
              Modification = peptide.Modifications,
              FileScan = peptide.Query.FileScan.LongFileName
            };

            for (int ms3scan = peptide.Query.FileScan.FirstScan + 1; ms3scan < lastScan; ms3scan++)
            {
              var mslevel = reader.GetMsLevel(ms3scan);
              if (mslevel != 3)
              {
                break;
              }
              var pkl = reader.GetPeakList(ms3scan);
              if (pkl.Count == 0)
              {
                continue;
              }
              var precursor = reader.GetPrecursorPeak(ms3scan);
              pkl.PrecursorMZ = precursor.Mz;
              ms2.MS3Spectra.Add(pkl);
            }

            if (ms2.MS3Spectra.Count > 0)
            {
              ms2list.Add(ms2);
            }
          }
        }
      }

      Progress.SetMessage("Merging MS2 by peptide and charge ...");

      var ms2group = ms2list.GroupBy(m => string.Format("{0}:{1}", m.Peptide, m.Charge)).ToList();
      var ms2library = new List<MS2Item>();
      foreach (var g in ms2group)
      {
        if (g.Count() < options.MinIdentifiedSpectraPerPeptide)
        {
          continue;
        }

        var gitem = g.First();
        gitem.CombinedCount = g.Count();
        gitem.FileScan = (from gg in g select gg.FileScan).Merge(";");

        gitem.Precursor = g.Average(m => m.Precursor);
        foreach (var ms2 in g.Skip(1))
        {
          gitem.MS3Spectra.AddRange(ms2.MS3Spectra);
        }

        ms2library.Add(gitem);
      }

      ms2library.Sort((m1, m2) =>
      {
        var res = m1.Peptide.CompareTo(m2.Peptide);
        if (res == 0)
        {
          res = m1.Charge.CompareTo(m2.Charge);
        }
        return res;
      });

      new MS2ItemXmlFormat().WriteToFile(options.OutputUncombinedFile, ms2library);

      Progress.SetMessage("Combing MS3 by precursor ...");

      var builder = new BestSpectrumTopSharedPeaksBuilder(options.FragmentPPMTolerance, options.MaxFragmentPeakCount);
      ms2library.ForEach(m => m.CombineMS3Spectra(builder, options.PrecursorPPMTolerance));
      new MS2ItemXmlFormat().WriteToFile(options.OutputFile, ms2library);

      Progress.End();

      return new[] { options.OutputFile, options.OutputUncombinedFile };
    }
  }
}
