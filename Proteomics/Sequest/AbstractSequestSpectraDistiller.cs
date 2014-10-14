using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RCPA.Gui;
using RCPA.Utils;
using System.IO;
using RCPA.Proteomics.Summary;

namespace RCPA.Proteomics.Sequest
{
  public abstract class AbstractSequestSpectraDistiller : ProgressClass
  {
    protected ISpectrumParser parser;

    protected IFileFormat<List<IIdentifiedSpectrum>> peptideFormat;

    public AbstractSequestSpectraDistiller(ISpectrumParser parser, IFileFormat<List<IIdentifiedSpectrum>> peptideFormat)
    {
      this.parser = parser;
      this.peptideFormat = peptideFormat;
    }

    public virtual string GetPeptideFilename(string outsFile, string modStr)
    {
      return new DtaOutFilenameConverter(outsFile).PureName + modStr + ".peptides";
    }

    protected bool MatchedIntensityExist(string peptideFilename)
    {
      using (StreamReader sr = new StreamReader(peptideFilename))
      {
        string line = sr.ReadLine();
        return line.IndexOf("MatchedTIC") != -1;
      }
    }

    public abstract List<IIdentifiedSpectrum> ParseSpectra(string dir, string modStr, int stepCount, int totalCount);
  }
}
