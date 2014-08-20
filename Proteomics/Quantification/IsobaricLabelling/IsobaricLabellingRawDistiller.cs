using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RCPA.Proteomics.Raw;
using System.IO;
using RCPA.Proteomics.Quantification;
using RCPA.Proteomics.Quantification.ITraq;
using System.Xml;
using RCPA.Proteomics.Spectrum;

namespace RCPA.Proteomics.Quantification.IsobaricLabelling
{
  public class IsobaricLabellingRawDistiller : AbstractThreadProcessor
  {
    private IsobaricLabellingRawDistillerOptions _options;

    public IsobaricLabellingRawDistiller(IsobaricLabellingRawDistillerOptions options)
    {
      this._options = options;
    }

    public override IEnumerable<string> Process()
    {
      Progress.SetMessage(1, MyConvert.Format("Processing {0} ...", _options.InputFile));
      var items = _options.PlexType.Channels;

      var minMass = items.Min(m => m.Mz);
      var maxMass = items.Max(m => m.Mz);

      var delta = PrecursorUtils.ppm2mz(_options.PPMTolearnce, maxMass);
      minMass -= delta;
      maxMass += delta;

      using (var sw = new StreamWriter(_options.OutputFile))
      {
        sw.WriteLine("Scan\t{0}", (from item in items select item.Name + "\t" + item.Name + "_DeltaPPM").Merge("\t"));
        using (var reader = RawFileFactory.GetRawFileReader(_options.InputFile))
        {
          var first = reader.GetFirstSpectrumNumber();
          var last = reader.GetLastSpectrumNumber();

          Progress.SetRange(first, last);
          for (int scan = first; scan <= last; scan++)
          {
            var mslevel = reader.GetMsLevel(scan);
            if (mslevel != 2)
            {
              continue;
            }

            sw.Write(scan);

            var peaks = reader.GetPeakList(scan, minMass, maxMass);
            var ions = new List<Peak>();
            for (int i = 0; i < items.Count; i++)
            {
              var ion = peaks.FindPeak(items[i].Mz, delta).FindMaxIntensityPeak();
              if (ion == null)
              {
                sw.Write("\t0.0\t10");
              }
              else
              {
                sw.Write("\t{0:0.0}\t{1:0.0}", ion.Intensity, PrecursorUtils.mz2ppm(ion.Mz, ion.Mz - items[i].Mz));
              }
            }
            sw.WriteLine();

            Progress.SetPosition(scan);
          }
        }
      }
      Progress.SetMessage(1, "Finished!");

      return new string[] { _options.OutputFile };
    }
  }
}
