using System;
using System.Collections.Generic;
using System.Text;
using RCPA.Proteomics.Sequest;
using RCPA.Proteomics.Summary;

namespace RCPA.Tools.Distiller
{
  public class GetPeptideInUniqueProteinXProcessor : AbstractThreadFileProcessor
  {
    public int uniqueCount;

    private delegate bool Accept(IIdentifiedProteinGroup spg);

    private Accept acceptFunc;
    private string uniqueStr;

    public GetPeptideInUniqueProteinXProcessor(int uniqueCount, bool uniqueCountOnly)
    {
      this.uniqueCount = uniqueCount;

      if (uniqueCountOnly)
      {
        acceptFunc = this.AcceptUniqueCountOnly;
        uniqueStr = uniqueCount.ToString();
      }
      else
      {
        acceptFunc = this.AcceptUniqueCountUpper;
        uniqueStr = uniqueCount.ToString() + "+";
      }
    }

    private bool AcceptUniqueCountUpper(IIdentifiedProteinGroup spg)
    {
      return spg[0].UniquePeptideCount >= uniqueCount;
    }

    private bool AcceptUniqueCountOnly(IIdentifiedProteinGroup spg)
    {
      return spg[0].UniquePeptideCount == uniqueCount;
    }

    public override IEnumerable<string> Process(string filename)
    {
      SequestResultTextFormat format = new SequestResultTextFormat();

      Progress.SetMessage("Reading from " + filename + "...");
      IIdentifiedResult sr = format.ReadFromFile(filename);

      HashSet<IIdentifiedSpectrum> result = new HashSet<IIdentifiedSpectrum>();

      foreach (IIdentifiedProteinGroup spg in sr)
      {
        if (acceptFunc(spg))
        {
          result.UnionWith(spg[0].GetSpectra());
        }
      }

      List<IIdentifiedSpectrum> spectra = new List<IIdentifiedSpectrum>(result);
      spectra.Sort();

      string resultFilename = MyConvert.Format("{0}.{1}.peptides", filename, uniqueStr);
      Progress.SetMessage("Writing to " + resultFilename + "...");
      new SequestPeptideTextFormat(format.PeptideFormat.GetHeader()).WriteToFile(resultFilename, spectra);
      Progress.SetMessage("Finished");

      return new[] { resultFilename };
    }
  }
}
