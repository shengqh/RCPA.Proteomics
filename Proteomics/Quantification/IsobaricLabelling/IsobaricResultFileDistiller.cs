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

      var allpkls = (from r in result select r.RawPeaks).ToList();
      var validpkls = allpkls.Where(r => r.Count >= result.PlexType.Channels.Count).ToList();
      if (validpkls.Count >= result.Count / 2)
      {
        //use confident peak lists for calibration
        result.PlexType.CalibrateMass(validpkls);
      }
      else
      {
        //use all peak lists for calibration
        result.PlexType.CalibrateMass(allpkls);
      }

      var builder = new IsobaricResultBuilder(result.PlexType, options.ProductPPMTolerance);
      result = builder.BuildIsobaricResult(result, 1);

      result.RemoveAll(m => m.PeakCount() < options.MinPeakCount);

      if (options.RequiredChannels.Count > 0)
      {
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
      new IsobaricPurityCorrectionRCalculator(options.PlexType, options.RExecute, options.PerformPurityCorrection, true).Calculate(result, tempFilename);

      result.RemoveAll(m => m.PeakCount() < options.MinPeakCount);
      result.ForEach(m => m.PrecursorPercentage = m.PeakInIsolationWindow.GetPrecursorPercentage(options.PrecursorPPMTolerance));

      return result;
    }
  }
}
