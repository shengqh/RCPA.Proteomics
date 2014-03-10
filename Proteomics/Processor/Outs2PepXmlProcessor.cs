using System.Collections.Generic;
using System.IO;
using RCPA.Proteomics.PeptideProphet;
using RCPA.Proteomics.Sequest;
using RCPA.Proteomics.Summary;
using RCPA.Utils;

namespace RCPA.Proteomics.Processor
{
  public class Outs2PepXmlProcessor : AbstractThreadFileProcessor
  {
    private readonly string targetDir;

    public Outs2PepXmlProcessor(string targetDir)
    {
      this.targetDir = targetDir;
    }

    public override IEnumerable<string> Process(string filename)
    {
      var parser = new OutParser();

      string resultFilename;
      if (null == this.targetDir)
      {
        resultFilename = FileUtils.ChangeExtension(filename, ".xml");
      }
      else
      {
        resultFilename = targetDir + "/" + FileUtils.ChangeExtension(new FileInfo(filename).Name, ".xml");
      }


      using (PepXmlWriter writer = new PepXmlWriter("out"))
      {
        writer.Open(resultFilename);

        writer.OpenMsmsRunSummary(filename);

        SequestParam sp = new SequestParamFile().ReadFromFile(filename);

        writer.WriteSequestParam(sp);

        using (var reader = new OutsReader(filename))
        {
          int totalCount = reader.FileCount;
          Progress.SetRange(0, totalCount);
          int currentCount = 0;

          while (reader.HasNext)
          {
            if (Progress.IsCancellationPending())
            {
              throw new UserTerminatedException();
            }

            List<string> contents = reader.NextContent();
            IIdentifiedSpectrum sph = parser.Parse(contents);
            if (sph == null)
            {
              continue;
            }

            writer.WriteSequestPeptideHit(sph);

            currentCount++;
            Progress.SetPosition(currentCount);
          }
          Progress.SetPosition(totalCount);
        }
        writer.CloseMsmsRunSummary();
      }

      return new[] { resultFilename };
    }
  }
}