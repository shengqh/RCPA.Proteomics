using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace RCPA.Proteomics.Quantification.IsobaricLabelling
{
  public class IsobaricResultFileDistillerOptions : AbstractIsobaricResultFileDistillerOptions
  {
    public IsobaricResultFileDistillerOptions()
    {
      this.PerformPurityCorrection = true;
    }

    public string InputFile { get; set; }

    private string _rExecute;
    public string RExecute
    {
      get
      {
        if (string.IsNullOrEmpty(_rExecute))
        {
          return ExternalProgramConfig.GetExternalProgram("R");
        }
        return _rExecute;
      }
      set
      {
        _rExecute = value;
      }
    }

    public string OriginalXmlFileName
    {
      get { return this.InputFile + "." + this.PlexType.Name + ".oisobaric.xml"; }
    }

    private string _outputFile;
    public string OutputFile
    {
      get
      {
        if (string.IsNullOrEmpty(_outputFile))
        {
          var cali = this.PerformMassCalibration ? ".cali" : "";
          var corr = this.PerformPurityCorrection ? ".corr" : "";
          return MyConvert.Format("{0}.min{1}{2}{3}.isobaric.xml", this.InputFile, this.MinPeakCount, cali, corr);
        }
        else
        {
          return _outputFile;
        }
      }
      set
      {
        _outputFile = value;
      }
    }
  }
}
