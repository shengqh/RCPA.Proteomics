using System;
using System.Collections.Generic;
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
      get { return MyConvert.Format("{0}.{1}.oisobaric.xml", this.InputFile, this.Reader.Name); }
    }

    private string _outputFile;
    public string OutputFile
    {
      get
      {
        if (string.IsNullOrEmpty(_outputFile))
        {
          var corr = this.PerformPurityCorrection ? ".corr" : "";
          return MyConvert.Format("{0}.{1}.isobaric.min{2}{3}.xml", this.InputFile, this.Reader.Name, this.MinPeakCount, corr);
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
