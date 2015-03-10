using RCPA.Commandline;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RCPA.Proteomics.MaxQuant
{
  public class MaxQuantEvidenceToPeptideProcessorOptions : AbstractOptions
  {
    public string InputFile { get; set; }

    private string _outputFile;
    public string OutputFile
    {
      get
      {
        if (string.IsNullOrEmpty(_outputFile))
        {
          return Path.ChangeExtension(InputFile, ".peptides");
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

    public bool RemoveContanimant
    {
      get
      {
        return true;
      }
    }

    public bool RemoveDecoy
    {
      get
      {
        return true;
      }
    }

    public override bool PrepareOptions()
    {
      CheckFile("MaxQuant Envidence", InputFile);

      return ParsingErrors.Count == 0;
    }
  }
}
