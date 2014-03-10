using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using RCPA.Proteomics.Mascot;
using RCPA.Utils;
using RCPA.Proteomics.Summary;
using RCPA.Proteomics.Spectrum;
using RCPA.Proteomics.IO;

namespace RCPA.Proteomics.Format
{
  public class Mgf2DtaProcessor : AbstractParallelTaskProcessor
  {
    private static readonly Regex pattern = new Regex(@"Cmpd\s+(\d+),");

    private ITitleParser parser;

    private DtaFormat<Peak> dtaFormat = new DtaFormat<Peak>();

    private DirectoryInfo targetDir;

    private bool extractToIndividualDir;

    private int scanIndex;

    public Mgf2DtaProcessor(String targetDirectory, ITitleParser parser, bool extractToIndividualDir)
    {
      this.targetDir = new DirectoryInfo(targetDirectory);

      if (!this.targetDir.Exists)
      {
        this.targetDir.Create();
      }

      this.parser = parser;
      this.extractToIndividualDir = extractToIndividualDir;
    }

    public override IEnumerable<string> Process(string mgfFilename)
    {
      var fi = new FileInfo(mgfFilename);
      String mgfName = FileUtils.ChangeExtension(fi.Name, "");

      using (var sr = new StreamReader(mgfFilename))
      {
        bool bProcessing = sr.BaseStream.Length > 0;

        var reader = new MascotGenericFormatIterator<Peak>(sr);

        this.scanIndex = 0;

        if (bProcessing)
        {
          Progress.SetMessage("Converting " + mgfFilename + " ...");
          Progress.SetRange(0, sr.BaseStream.Length);
        }

        Progress.SetPosition(0);
        while (reader.HasNext())
        {
          if (Progress.IsCancellationPending())
          {
            throw new UserTerminatedException();
          }

          PeakList<Peak> pkl = reader.Next();
          if (bProcessing)
          {
            Progress.SetPosition(sr.BaseStream.Position);
          }

          int[] charges;
          if (0 != pkl.PrecursorCharge)
          {
            charges = new[] { pkl.PrecursorCharge };
          }
          else
          {
            charges = new[] { 2, 3 };
          }

          foreach (int charge in charges)
          {
            pkl.PrecursorCharge = charge;
            String targetFilename = GetTargetFilename(pkl, mgfName);

            this.dtaFormat.WriteToFile(targetFilename, pkl);
          }
        }

        Progress.SetMessage("Converting " + mgfFilename + " finished.");
      }

      return new[] { this.targetDir.FullName };
    }

    public string GetTargetFilename(PeakList<Peak> pkl, String mgfName)
    {
      if (pkl.Annotations.ContainsKey(MascotGenericFormatConstants.TITLE_TAG))
      {
        var title = (String)pkl.Annotations[MascotGenericFormatConstants.TITLE_TAG];

        SequestFilename sf = parser.GetValue(title);
        sf.Extension = "dta";
        sf.Charge = pkl.PrecursorCharge;

        if (extractToIndividualDir)
        {
          DirectoryInfo dir = new DirectoryInfo(this.targetDir.FullName + "\\" + sf.Experimental);
          if (!dir.Exists)
          {
            dir.Create();
          }
          return new FileInfo(dir.FullName + "\\" + sf.LongFileName).FullName;
        }
        else
        {
          return new FileInfo(this.targetDir.FullName + "\\" + sf.LongFileName).FullName;
        }
      }

      this.scanIndex++;
      string filename = MyConvert.Format("{0}.{1}.{1}.{2}.dta", mgfName, this.scanIndex, pkl.PrecursorCharge);
      return new FileInfo(this.targetDir.FullName + "\\" + filename).FullName;
    }
  }
}