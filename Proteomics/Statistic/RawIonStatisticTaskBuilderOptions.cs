using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace RCPA.Proteomics.Statistic
{
  public class RawIonStatisticTaskBuilderOptions
  {
    public RawIonStatisticTaskBuilderOptions()
    {
      this.SourceRFile = FileUtils.GetTemplateDir() + "/ion.r";
    }

    public double ProductIonPPM { get; set; }

    public double MinRelativeIntensity { get; set; }

    public string SourceRFile { get; private set; }

    public double PValue { get; set; }

    public string TargetDirectory { get; set; }

    public string InputFile { get; set; }

    public double MinFrequency { get; set; }

    public double MinMedianRelativeIntensity { get; set; }

    public string GetRCommand()
    {
      var result = ExternalProgramConfig.GetExternalProgram("R");

      if (result == null)
      {
        throw new Exception("Define R location first!");
      }

      return result;
    }

    public string PrepareRFile(string datafile, out string outputfile)
    {
      if (!File.Exists(SourceRFile))
      {
        throw new ArgumentException(string.Format("file not exists : {0}", SourceRFile));
      }

      if (!File.Exists(datafile))
      {
        throw new ArgumentException(string.Format("file not exists : {0}", datafile));
      }

      var result = new FileInfo(datafile + ".r").FullName.Replace("\\", "/");
      outputfile = datafile + ".sig";

      try
      {
        var lines = File.ReadAllLines(SourceRFile);
        using (var sw = new StreamWriter(result))
        {
          sw.WriteLine("setwd(\"{0}\")", this.TargetDirectory.Replace("\\", "/"));
          if (Path.GetDirectoryName(Path.GetFullPath(datafile)).Equals(Path.GetFullPath(this.TargetDirectory)))
          {
            sw.WriteLine("inputfile<-\"{0}\"", Path.GetFileName(datafile));
          }
          else
          {
            sw.WriteLine("inputfile<-\"{0}\"", Path.GetFullPath(datafile).Replace("\\", "/"));
          }
          sw.WriteLine("outputfile<-\"{0}\"", Path.GetFileName(outputfile));
          sw.WriteLine("pvalue<-{0}", PValue);

          var inpredefined = true;
          foreach (var line in lines)
          {
            if (line.StartsWith("##predefine_end"))
            {
              inpredefined = false;
              continue;
            }

            if (!inpredefined && !line.Trim().StartsWith("#"))
            {
              sw.WriteLine(line);
            }
          }
        }
      }
      catch (Exception ex)
      {
        throw new Exception(string.Format("create R file {0} failed: {1}", outputfile, ex.Message));
      }

      return result;
    }
  }
}
