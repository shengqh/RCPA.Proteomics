using System;
using System.Collections.Generic;
using System.IO;
using RCPA.Gui;
using RCPA.Proteomics.Summary;
using RCPA.Utils;

namespace RCPA.Proteomics.Sequest
{
  public class SequestOutDirectoryParser : AbstractSequestSpectrumParser
  {
    private OutParser parser;

    public SequestOutDirectoryParser(bool raiseDuplicatedReferenceAbsentException)
    {
      this.parser = new OutParser(raiseDuplicatedReferenceAbsentException);
    }

    public SequestOutDirectoryParser(bool raiseDuplicatedReferenceAbsentException, double modificationDeltaScore)
    {
      this.parser = new ModificationOutParser(raiseDuplicatedReferenceAbsentException, modificationDeltaScore);
    }

    public SequestOutDirectoryParser()
      : this(true)
    {
    }

    public SequestOutDirectoryParser(double modificationDeltaScore)
      : this(true, modificationDeltaScore)
    {
    }

    public override List<IIdentifiedSpectrum> ReadFromFile(string outDirectory)
    {
      var result = new List<IIdentifiedSpectrum>();

      FileInfo[] outFiles = new DirectoryInfo(outDirectory).GetFiles("*.out");
      FileInfo[] dtaFiles = new DirectoryInfo(outDirectory).GetFiles("*.dta");

      if (dtaFiles.Length != 0 && outFiles.Length != dtaFiles.Length)
      {
        throw new Exception(MyConvert.Format("Dta file count ({0}) in {1} is not equals to out file count ({2})",
                                          dtaFiles.Length, outDirectory, outFiles.Length));
      }

      Progress.SetRange(1, outFiles.Length);
      for (int i = 0; i < outFiles.Length; i++)
      {
        List<string> context = FileUtils.ReadFile(outFiles[i].FullName, false);

        IIdentifiedSpectrum spectrum = this.parser.Parse(context);

        if (null != spectrum)
        {
          result.Add(spectrum);
        }

        Progress.SetPosition(i + 1);
      }

      Progress.End();

      return result;
    }
  }
}