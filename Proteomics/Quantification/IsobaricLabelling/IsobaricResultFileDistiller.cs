using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RCPA.Gui;
using System.IO;
using RCPA.Proteomics.Raw;
using MathNet.Numerics.Statistics;
using RCPA.Utils;
using RCPA.Proteomics.Quantification;
using RCPA.Proteomics;
using RCPA.Proteomics.Spectrum;
using MathNet.Numerics.Distributions;

namespace RCPA.Proteomics.Quantification.IsobaricLabelling
{
  /// <summary>
  /// 从原始Raw文件中读取IsobaricItem信息，保存到后缀为.isobaric.xml的文件中。
  /// </summary>
  public class IsobaricResultFileDistiller : AbstractThreadProcessor
  {
    private IsobaricResultFileDistillerOptions options;

    private static readonly double MAX_SHIFT = 0.2;

    public IsobaricResultFileDistiller(IsobaricResultFileDistillerOptions options)
    {
      this.options = options;
    }

    public override IEnumerable<string> Process()
    {
      IsobaricResult result = BuildIsobaricResult();

      IsobaricResultFileFormatFactory.GetXmlFormat().WriteToFile(options.OutputFile, result);

      return new[] { options.OutputFile };
    }

    public virtual IsobaricResult BuildIsobaricResult()
    {
      Progress.SetMessage("Processing " + options.InputFile + " ...");

      string experimental = Path.GetFileNameWithoutExtension(options.InputFile);

      var format = IsobaricResultFileFormatFactory.GetXmlFormat();
      format.Progress = this.Progress;
      format.HasReporters = false;

      IsobaricResult result = null;

      var reader = options.Reader;

      if (!File.Exists(options.OriginalXmlFileName))
      {
        reader.Progress = this.Progress;

        Progress.SetMessage("Reading isobaric tag channels from " + new FileInfo(options.InputFile).Name + "...");
        var pkls = reader.ReadFromFile(options.InputFile);
        Progress.SetMessage("Reading isobaric tag channels finished.");

        if (pkls.Count == 0)
        {
          throw new Exception(MyConvert.Format("No isobaric tag information readed from file {0}, contact with author.", options.InputFile));
        }

        result = new IsobaricResult(pkls);
        result.Mode = reader.ToString();
        result.PlexType = options.PlexType;
        result.ForEach(m => m.Experimental = experimental);

        format.WriteToFile(options.OriginalXmlFileName, result);
      }
      else
      {
        Progress.SetMessage("Read xml information from " + options.OriginalXmlFileName + " ...");
        result = format.ReadFromFile(options.OriginalXmlFileName);
      }

      var usedChannels = (from ucha in options.UsedChannels
                          let cha = options.PlexType.Channels[ucha.Index]
                          select new UsedChannel() { Index = ucha.Index, Name = cha.Name, Mz = cha.Mz, MinMz = cha.Mz - MAX_SHIFT, MaxMz = cha.Mz + MAX_SHIFT }).ToList();

      var allpkls = (from r in result select r.RawPeaks).ToList();

      //Get all peak list with all used channel information
      var validpkls = allpkls.Where(r =>
      {
        foreach (var channel in usedChannels)
        {
          if (!r.HasPeak(channel.Mz, channel.MinMz, channel.MaxMz))
          {
            return false;
          }
        }

        return true;
      }).ToList();

      var calPeaks = validpkls.Count >= result.Count / 2 ? validpkls : allpkls;

      usedChannels.CalibrateMass(calPeaks, MAX_SHIFT);

      var builder = new IsobaricResultBuilder(usedChannels, options.ProductPPMTolerance);
      result = builder.BuildIsobaricResult(result, 1);

      result.RemoveAll(m => m.PeakCount() < options.MinPeakCount);

      if (options.RequiredChannels.Count > 0)
      {
        options.RequiredChannels.ForEach(m => m.Index = usedChannels.FindIndex(l => l.Name.Equals(m.Name)));
        result.RemoveAll(m =>
        {
          foreach (var channel in options.RequiredChannels)
          {
            if (channel.GetValue(m) <= IsobaricConsts.NULL_INTENSITY)
            {
              return true;
            }
          }

          return false;
        });
      }

      var tempFilename = options.OutputFile + ".tsv";
      new IsobaricPurityCorrectionRCalculator(options.PlexType, result.UsedChannels, options.RExecute, options.PerformPurityCorrection, true).Calculate(result, tempFilename);

      result.RemoveAll(m => m.PeakCount() < options.MinPeakCount);
      result.ForEach(m => m.PrecursorPercentage = m.PeakInIsolationWindow.GetPrecursorPercentage(options.PrecursorPPMTolerance));

      return result;
    }
  }
}
