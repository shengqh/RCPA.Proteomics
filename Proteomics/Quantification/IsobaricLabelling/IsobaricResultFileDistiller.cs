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

    public IsobaricResultFileDistiller(IsobaricResultFileDistillerOptions options)
    {
      this.options = options;
    }

    public override IEnumerable<string> Process()
    {
      IsobaricResult result = BuildIsobaricResult();

      Progress.SetMessage("Saving data to {0} ...", options.OutputFile);
      IsobaricResultFileFormatFactory.GetXmlFormat().WriteToFile(options.OutputFile, result);

      Progress.SetMessage("Done.");
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
        result.Comments["Mode"] = reader.ToString();
        result.PlexType = options.PlexType;
        result.ForEach(m => m.Experimental = experimental);

        format.WriteToFile(options.OriginalXmlFileName, result);
      }
      else
      {
        Progress.SetMessage("Reading xml information from " + options.OriginalXmlFileName + " ...");
        result = format.ReadFromFile(options.OriginalXmlFileName);
      }

      result.UsedChannels = (from ucha in options.UsedChannels
                             let cha = options.PlexType.Channels[ucha.Index]
                             select new UsedChannel() { Index = ucha.Index, Name = cha.Name, Mz = cha.Mz, MinMz = cha.Mz - IsobaricConsts.MAX_SHIFT, MaxMz = cha.Mz + IsobaricConsts.MAX_SHIFT }).ToList();

      if (options.PerformMassCalibration)
      {
        Progress.SetMessage("Performing mass calibration ...");
        var calPeaks = result.GetMassCalibrationPeaks();
        result.UsedChannels.CalibrateMass(calPeaks, options.OutputFile + ".calibration");
        result.Comments["PerformMassCalibration"] = true.ToString();
      }

      foreach (var channel in result.UsedChannels)
      {
        var mztolerance = PrecursorUtils.ppm2mz(channel.Mz, options.ProductPPMTolerance);
        channel.MinMz = channel.Mz - mztolerance;
        channel.MaxMz = channel.Mz + mztolerance;
      }

      Progress.SetMessage("Building isobaric result ...");
      result.ForEach(m => m.DetectReporter(result.UsedChannels));
      result.RemoveAll(m => m.PeakCount() < options.MinPeakCount);
      result.Comments["RequiredChannels"] = (from c in options.RequiredChannels select c.Name).Merge(",");

      if (options.RequiredChannels.Count > 0)
      {
        options.RequiredChannels.ForEach(m => m.Index = result.UsedChannels.FindIndex(l => l.Name.Equals(m.Name)));
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

      if (options.PerformPurityCorrection)
      {
        Progress.SetMessage("Performing purity correction ...");
        var tempFilename = options.OutputFile + ".csv";
        new IsobaricPurityCorrectionRCalculator(options.PlexType, result.UsedChannels, options.RExecute, options.PerformPurityCorrection, true).Calculate(result, tempFilename);
        result.ForEach(m => m.Reporters.ForEach(p =>
        {
          if (p.Intensity < IsobaricConsts.NULL_INTENSITY)
          {
            p.Intensity = IsobaricConsts.NULL_INTENSITY;
          }
        }));
        result.Comments["PerformPurityCorrection"] = true.ToString();
      }

      result.RemoveAll(m => m.PeakCount() < options.MinPeakCount);

      Progress.SetMessage("Calculating precursor percentage ...");
      result.ForEach(m => m.PrecursorPercentage = m.PeakInIsolationWindow.GetPrecursorPercentage(options.PrecursorPPMTolerance));

      return result;
    }

  }
}
