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
using RCPA.Proteomics.Quantification.ITraq;
using MathNet.Numerics.Distributions;

namespace RCPA.Proteomics.Quantification.ITraq
{
  /// <summary>
  /// 从原始Raw文件中读取ITraq信息，保存到后缀为.itraq的文件中。
  /// </summary>
  public class ITraqResultFileDistiller : AbstractThreadFileProcessor
  {
    public IITraqRawReader itraqReader { get; set; }

    private IsobaricImpurityCorrectionCalculator calc;

    private int minPeakCount;

    public IITraqNormalizationBuilder NormalizationBuilder { get; set; }

    private double precursorPPM;

    public ITraqResultFileDistiller(IITraqRawReader itraqReader, int minPeakCount, IsobaricType plexType, string isotopeImpurityFile, double precursorPPM)
    {
      this.itraqReader = itraqReader;
      this.minPeakCount = minPeakCount;
      this.calc = new IsobaricImpurityCorrectionCalculator(plexType, isotopeImpurityFile);
      this.precursorPPM = precursorPPM;
    }

    public string GetOriginalITraqFileName(string rawFileName)
    {
      return MyConvert.Format("{0}.oitraq.xml", rawFileName);
    }

    public string GetITraqFileName(string rawFileName)
    {
      return MyConvert.Format("{0}.{1}.itraq.xml", rawFileName, minPeakCount);
    }

    private bool CheckOriginalFile(string originalFileName)
    {
      return File.Exists(originalFileName);
    }

    public override IEnumerable<string> Process(string rawFileName)
    {
      IsobaricResult result = GetTraqResult(rawFileName);

      string resultFileName = GetITraqFileName(rawFileName);

      ITraqResultFileFormatFactory.GetXmlFormat().WriteToFile(resultFileName, result);

      return new[] { resultFileName };
    }

    private static readonly double SigmaFoldTolerance = 3;

    public IsobaricResult GetTraqResult(string rawFileName)
    {
      Progress.SetMessage("Processing " + rawFileName + " ...");

      string experimental = FileUtils.ChangeExtension(new FileInfo(rawFileName).Name, "");

      string originalFileName = GetOriginalITraqFileName(rawFileName);

      string paramFileName = originalFileName + ".param";

      IsobaricResult result= null;

      ITraqFileBuilder builder = new ITraqFileBuilder(itraqReader.PlexType.GetDefinition());

      if (!CheckOriginalFile(originalFileName) || !File.Exists(paramFileName))
      {
        itraqReader.Progress = this.Progress;

        Progress.SetMessage("Reading isobaric tag channels from " + new FileInfo(rawFileName).Name + "...");
        List<IsobaricItem> pkls = itraqReader.ReadFromFile(rawFileName);
        Progress.SetMessage("Reading isobaric tag channels finished.");

        if (pkls.Count == 0)
        {
          throw new Exception(MyConvert.Format("No isobaric tag information readed from file {0}, contact with author.", rawFileName));
        }

        var accs = builder.GetDistances(from pkl in pkls select pkl.RawPeaks);

        if (accs.Count == 0)
        {
          throw new Exception(MyConvert.Format("No isobaric tag information readed from file {0}, contact with author.", rawFileName));
        }

        result = builder.GetITraqResult(pkls, accs, SigmaFoldTolerance, 1);
        result.Mode = itraqReader.ToString();

        result.ForEach(m => m.Experimental = experimental);

        ITraqResultFileFormatFactory.GetXmlFormat().WriteToFile(originalFileName, result);

        using (StreamWriter sw = new StreamWriter(paramFileName))
        {
          sw.WriteLine("Ion\tMean\tSigma");
          var ions = builder.Definition.Items;
          for (int i = 0; i < ions.Length; i++)
          {
            sw.WriteLine("{0:0.0}\t{1:0.0000}\t{2:0.0000}", ions[i].Index, accs[i].Mean, accs[i].StdDev);
          }
        }
      }
      else
      {
        Progress.SetMessage("Read xml information from " + originalFileName + " ...");

        var format = ITraqResultFileFormatFactory.GetXmlFormat();
        format.Progress = this.Progress;
        
        result = format.ReadFromFile(originalFileName);
      }

      result.RemoveAll(m => m.PeakCount() < minPeakCount);

      result.ForEach(m => calc.Correct(m));

      result.RemoveAll(m => m.PeakCount() < minPeakCount);

      result.ForEach(m => m.PrecursorPercentage = m.PeakInIsolationWindow.GetPrecursorPercentage(this.precursorPPM));

      if (NormalizationBuilder != null)
      {
        NormalizationBuilder.Normalize(result);
      }
      return result;
    }
  }
}
