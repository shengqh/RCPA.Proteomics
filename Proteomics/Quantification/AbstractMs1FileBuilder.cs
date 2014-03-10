using System;
using System.Collections.Generic;
using System.IO;
using RCPA.Proteomics.Raw;
using RCPA.Utils;
using RCPA.Proteomics.Spectrum;

namespace RCPA.Proteomics.Quantification
{
  public abstract class AbstractMs1FileBuilder : AbstractThreadFileProcessor
  {
    private readonly string targetDir;

    protected IRawFile rawFile;

    public AbstractMs1FileBuilder(IRawFile rawFile, string targetDir)
    {
      this.rawFile = rawFile;
      this.targetDir = targetDir;
    }

    public string GetResultFilename(string rawFilename)
    {
      string result = FileUtils.ChangeExtension(rawFilename, ".ms1");
      if (this.targetDir != null)
      {
        result = this.targetDir + "/" + (new FileInfo(result).Name);
      }
      return result;
    }

    public override IEnumerable<string> Process(string rawFilename)
    {
      string ms1Filename = GetResultFilename(rawFilename);
      string ms1IndexFilename = ms1Filename + ".index";

      rawFile.Open(rawFilename);
      try
      {
        int firstScan = rawFile.GetFirstSpectrumNumber();
        int lastScan = rawFile.GetLastSpectrumNumber();

        int firstFullScan = 0;
        for (firstFullScan = firstScan; firstFullScan < lastScan; firstFullScan++)
        {
          if (rawFile.GetMsLevel(firstFullScan) == 1)
          {
            break;
          }
        }

        if (firstFullScan >= lastScan)
        {
          return new List<string>();
        }

        Progress.SetRange(firstScan, lastScan);

        using (var sw = new StreamWriter(ms1Filename))
        {
          //write header
          sw.Write("H\tCreationDate\t{0:m/dd/yyyy HH:mm:ss}\n", DateTime.Now);
          sw.Write("H\tExtractor\t{0}\n", GetProgramName());
          sw.Write("H\tExtractorVersion\t{0}\n", GetProgramVersion());
          sw.Write("H\tComments\t{0} written by Quanhu Sheng, 2008-2009\n", GetProgramName());
          sw.Write("H\tExtractorOptions\tMS1\n");
          sw.Write("H\tAcquisitionMethod\tData-Dependent\n");
          sw.Write("H\tDataType\tCentriod\n");
          sw.Write("H\tScanType\tMS1\n");
          sw.Write("H\tFirstScan\t{0}\n", firstScan);
          sw.Write("H\tLastScan\t{0}\n", lastScan);

          using (var swIndex = new StreamWriter(ms1IndexFilename))
          {
            for (int scan = firstScan; scan <= lastScan; scan++)
            {
              if (Progress.IsCancellationPending())
              {
                throw new UserTerminatedException();
              }

              Progress.SetPosition(scan);

              if (rawFile.GetMsLevel(scan) == 1)
              {
                sw.Flush();

                swIndex.Write("{0}\t{1}\n", scan, sw.BaseStream.Position);

                sw.Write("S\t{0}\t{0}\n", scan);
                sw.Write("I\tRetTime\t{0:0.00}\n", rawFile.ScanToRetentionTime(scan));

                PeakList<Peak> pkl = rawFile.GetPeakList(scan);
                foreach (Peak p in pkl)
                {
                  sw.Write("{0:0.0000} {1:0.0} {2}\n", p.Mz, p.Intensity, p.Charge);
                }
              }
            }
          }
        }
      }
      finally
      {
        rawFile.Close();
      }

      Progress.End();

      return new[] { ms1Filename };
    }

    protected abstract string GetProgramName();
    protected abstract string GetProgramVersion();
  }
}