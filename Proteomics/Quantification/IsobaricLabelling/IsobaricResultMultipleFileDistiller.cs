using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RCPA.Proteomics.Raw;
using System.IO;
using RCPA.Proteomics.Quantification;
using System.Xml;

namespace RCPA.Proteomics.Quantification.IsobaricLabelling
{
  public class IsobaricResultMultipleFileDistiller : AbstractThreadFileProcessor
  {
    private IIsobaricRawReader reader;

    private string[] rawFiles;

    private bool individual;

    private int minPeakCount;

    private double precursorPPMTolerance;

    private double productPPMTolerance;

    private IsobaricType plexType;

    public IsobaricResultMultipleFileDistiller(IIsobaricRawReader reader, string[] rawFiles, bool individual, int minPeakCount, IsobaricType plexType, double precursorPPMTolerance, double productPPMTolerance)
    {
      this.reader = reader;
      this.rawFiles = rawFiles;
      this.individual = individual;
      this.minPeakCount = minPeakCount;
      this.plexType = plexType;
      this.precursorPPMTolerance = precursorPPMTolerance;
      this.productPPMTolerance = productPPMTolerance;
    }

    public override IEnumerable<string> Process(string targetFileName)
    {
      this.reader.Progress = this.Progress;

      List<string> resultFile = new List<string>();
      List<IsobaricItem> result = new List<IsobaricItem>();

      var format = new IsobaricResultXmlFormat();

      XmlTextWriter sw = null;
      if (!individual)
      {
        sw = new XmlTextWriter(targetFileName, Encoding.ASCII);
        format.StartWriteDocument(sw, reader.ToString());
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

          var options = new IsobaricResultFileDistillerOptions()
          {
            Reader = reader,
            InputFile = rawFiles[i],
            MinPeakCount = minPeakCount,
            PlexType = plexType,
            PrecursorPPMTolerance = precursorPPMTolerance,
            ProductPPMTolerance = productPPMTolerance
          };

          var distiller = new IsobaricResultFileDistiller(options)
          {
            Progress = this.Progress,
          };

          if (individual)
          {
            string itraqFile = distiller.Process().First();
            resultFile.Add(itraqFile);
          }
          else
          {
            var curResult = distiller.BuildIsobaricResult();

            foreach (var item in curResult)
            {
              format.WriteIsobaricItem(sw, item);
            }

            curResult = null;
          }

          GC.Collect();
          GC.WaitForFullGCComplete();
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
        var indexBuilder = new IsobaricResultXmlIndexBuilder(true)
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
