using System;
using System.Collections.Generic;
using System.Text;
using RCPA.Proteomics.Mascot;
using RCPA.Proteomics.Summary;

namespace RCPA.Tools.Format
{
  public class IdentifiedResult2DtaSelectProcessor : AbstractThreadFileProcessor
  {
    public override IEnumerable<string> Process(string filename)
    {
      IIdentifiedResult mr = new MascotResultTextFormat().ReadFromFile(filename);

      string resultFilename = filename + ".dtaselect.txt";
      new MascotResultDtaselectFormat().WriteToFile(resultFilename, mr);

      return new [] { resultFilename };
    }
  }
}
