using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RCPA.Proteomics.Raw;
using System.IO;
using RCPA.Proteomics.Quantification;
using RCPA.Proteomics.Quantification.ITraq;
using System.Xml;

namespace RCPA.Proteomics.Quantification.ITraq
{
  public class ITraqResultMultipleFileDistiller : AbstractThreadFileProcessor
  {
    private IITraqRawReader reader;

    private string[] rawFiles;

    private bool individual;

    private int minPeakCount;

    private string isotopeImpurityCorrectionTableFileName;

    private IITraqNormalizationBuilder builder;

    private double precursorPPMTolearnce;

    private IsobaricType plexType;

    public ITraqResultMultipleFileDistiller(IITraqRawReader reader, string[] rawFiles, bool individual, int minPeakCount, IsobaricType plexType, string isotopeImpurityCorrectionTableFileName, IITraqNormalizationBuilder builder, double precursorPPMTolearnce)
    {
      this.reader = reader;
      this.rawFiles = rawFiles;
      this.individual = individual;
      this.minPeakCount = minPeakCount;
      this.plexType = plexType;
      this.isotopeImpurityCorrectionTableFileName = isotopeImpurityCorrectionTableFileName;
      this.builder = builder;
      this.precursorPPMTolearnce = precursorPPMTolearnce;
    }

    public override IEnumerable<string> Process(string targetFileName)
    {
      this.reader.Progress = this.Progress;

      List<string> resultFile = new List<string>();
      List<IsobaricItem> result = new List<IsobaricItem>();

      ITraqResultXmlFormatFast format = new ITraqResultXmlFormatFast();

      XmlTextWriter sw = null;
      if (!individual)
      {
        sw = new XmlTextWriter(targetFileName, Encoding.ASCII);
        ITraqResultXmlFormatFast.StartWriteDocument(sw, reader.ToString());
      }

      try
      {
        for (int i = 0; i < rawFiles.Count(); i++)
        {
          if (Progress.IsCancellationPending())
          {
            throw new UserTerminatedException();
          }

          Progress.SetMessage(1, MyConvert.Format("Processing {0}/{1} ...", i + 1, rawFiles.Count()));
          using (var rawreader = RawFileFactory.GetRawFileReader(rawFiles[i]))
          {
            reader.RawReader = rawreader;

            ITraqResultFileDistiller distiller = new ITraqResultFileDistiller(reader, minPeakCount, plexType, isotopeImpurityCorrectionTableFileName, precursorPPMTolearnce)
            {
              Progress = this.Progress,
              NormalizationBuilder = this.builder
            };

            if (individual)
            {
              string itraqFile = distiller.Process(rawFiles[i]).First();
              resultFile.Add(itraqFile);
            }
            else
            {
              var curResult = distiller.GetTraqResult(rawFiles[i]);

              foreach (var item in curResult)
              {
                ITraqResultXmlFormatFast.WriteIsobaricItem(sw, item);
              }

              curResult = null;
            }

            GC.Collect();
            GC.WaitForFullGCComplete();
          }
        }

        Progress.SetMessage(0, "");
      }
      finally
      {
        if (!individual)
        {
          sw.Close();
        }
      }

      if (!individual)
      {
        var indexBuilder = new ITraqResultXmlIndexBuilder(true)
        {
          Progress = this.Progress
        };
        indexBuilder.Process(targetFileName);
        resultFile.Add(targetFileName);
      }

      Progress.SetMessage(1, "Finished!");

      return resultFile;
    }
  }
}
