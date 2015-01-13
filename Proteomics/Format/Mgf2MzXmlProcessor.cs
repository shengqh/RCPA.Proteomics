using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using RCPA.Proteomics.Mascot;
using RCPA.Utils;
using RCPA.Proteomics.Summary;
using RCPA.Proteomics.Spectrum;
using RCPA.Proteomics.IO;

namespace RCPA.Proteomics.Format
{
  public class Mgf2MzXmlProcessor : AbstractParallelTaskFileProcessor
  {
    private static readonly Regex pattern = new Regex(@"Cmpd\s+(\d+),");

    private ITitleParser parser;

    private string targetFile;

    private int scanIndex;

    public Mgf2MzXmlProcessor(String targetFile, ITitleParser parser)
    {
      this.targetFile = targetFile;
      this.parser = parser;
    }

    public override IEnumerable<string> Process(string mgfFilename)
    {
      String mgfName = Path.GetFileNameWithoutExtension(mgfFilename);

      using (var sw = new StreamWriter(targetFile))
      {
        sw.WriteLine("H\tCreationDate\t{0:yyyyMMdd}", DateTime.Now);
        sw.WriteLine("H\tExtractor\tProteomicsTools");

        using (var sr = new StreamReader(mgfFilename))
        {
          bool bProcessing = sr.BaseStream.Length > 0;

          var reader = new MascotGenericFormatIterator<Peak>(sr);

          this.scanIndex = 0;

          if (bProcessing)
          {
            Progress.SetMessage("Converting " + mgfFilename + " ...");
            Progress.SetRange(0, sr.BaseStream.Length);
          }

          Progress.SetPosition(0);
          while (reader.HasNext())
          {
            if (Progress.IsCancellationPending())
            {
              throw new UserTerminatedException();
            }

            PeakList<Peak> pkl = reader.Next();
            if (bProcessing)
            {
              Progress.SetPosition(sr.BaseStream.Position);
            }

            int[] charges;
            if (0 != pkl.PrecursorCharge)
            {
              charges = new[] { pkl.PrecursorCharge };
            }
            else if (IsWiff(pkl))
            {
              charges = new[] { 1 };
            }
            else
            {
              charges = new[] { 2, 3 };
            }

            var sf = GetFilename(pkl, mgfName);
            sw.WriteLine("S\t{0}\t{1}\t{2}", sf.FirstScan, sf.LastScan, pkl.PrecursorMZ);
            foreach (var charge in charges)
            {
              sw.WriteLine("Z\t{0}\t{1:0.#####}", charge, PrecursorUtils.MzToMH(pkl.PrecursorMZ, charge, true));
            }
            foreach (var peak in pkl)
            {
              sw.WriteLine("{0:0.#####}\t{1:0.#}", peak.Mz, peak.Intensity);
            }
            sw.WriteLine();
          }

          Progress.SetMessage("Converting " + mgfFilename + " finished.");
        }
      }
      return new[] { this.targetFile };
    }

    private bool IsWiff(PeakList<Peak> pkl)
    {
      if (pkl.Annotations.ContainsKey(MascotGenericFormatConstants.TITLE_TAG))
      {
        var title = (String)pkl.Annotations[MascotGenericFormatConstants.TITLE_TAG];
        return title.ToLower().Contains(".wiff");
      }
      return false;
    }

    public SequestFilename GetFilename(PeakList<Peak> pkl, String mgfName)
    {
      if (pkl.Annotations.ContainsKey(MascotGenericFormatConstants.TITLE_TAG))
      {
        var title = (String)pkl.Annotations[MascotGenericFormatConstants.TITLE_TAG];

        SequestFilename sf = parser.GetValue(title);
        sf.Extension = "dta";
        sf.Charge = pkl.PrecursorCharge;

        return sf;
      }

      this.scanIndex++;
      return new SequestFilename(mgfName, this.scanIndex, this.scanIndex, pkl.PrecursorCharge, "dta");
    }
  }
}