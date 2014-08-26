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
    private IsobaricResultMultipleFileDistillerOptions options;

    public IsobaricResultMultipleFileDistiller(IsobaricResultMultipleFileDistillerOptions options)
    {
      this.options = options;
    }

    public override IEnumerable<string> Process(string targetFileName)
    {
      this.options.Reader.Progress = this.Progress;

      List<string> resultFile = new List<string>();
      List<IsobaricScan> result = new List<IsobaricScan>();

      var format = new IsobaricResultXmlFormat();

      XmlTextWriter sw = null;
      if (!options.Individual)
      {
        sw = new XmlTextWriter(targetFileName, Encoding.ASCII);
        format.StartWriteDocument(sw, options.Reader.ToString(), options.PlexType.Name);
      }

      try
      {
        for (int i = 0; i < options.RawFiles.Count(); i++)
        {
          if (Progress.IsCancellationPending())
          {
            throw new UserTerminatedException();
          }

          Progress.SetMessage(1, MyConvert.Format("Processing {0}/{1} ...", i + 1, options.RawFiles.Count()));

          var fileoptions = new IsobaricResultFileDistillerOptions()
          {
            Reader = options.Reader,
            InputFile = options.RawFiles[i],
            MinPeakCount = options.MinPeakCount,
            PrecursorPPMTolerance = options.PrecursorPPMTolerance,
            ProductPPMTolerance = options.ProductPPMTolerance,
            RequiredChannels = options.RequiredChannels
          };

          var distiller = new IsobaricResultFileDistiller(fileoptions)
          {
            Progress = this.Progress,
          };

          if (options.Individual)
          {
            string itraqFile = distiller.Process().First();
            resultFile.Add(itraqFile);
          }
          else
          {
            var curResult = distiller.BuildIsobaricResult();

            foreach (var item in curResult)
            {
              format.WriteIsobaricItem(sw, options.PlexType, item);
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
        if (!options.Individual)
        {
          sw.Close();
        }
      }

      if (!options.Individual)
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
