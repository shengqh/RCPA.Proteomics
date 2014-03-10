using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RCPA.Proteomics.PeptideProphet;
using RCPA.Proteomics.Mascot;
using RCPA.Utils;
using RCPA.Proteomics.Summary;

namespace RCPA.Proteomics.Format
{
  public class ProteinProphetToSummaryProcessor:AbstractThreadFileProcessor
  {
    private double min_probability;

    public ProteinProphetToSummaryProcessor(double min_probability)
    {
      this.min_probability = min_probability;
    }

    public override IEnumerable<string> Process(string fileName)
    {
      var result = new ProteinProphetXmlReader().ReadFromFile(fileName);

      for (int i = result.Count - 1; i >= 0; i--)
      {
        if (result[i].Probability < min_probability)
        {
          result.RemoveAt(i);
        }
      }

      result.Sort();
      result.BuildGroupIndex();

      var resultFile = FileUtils.ChangeExtension(fileName,"noredundant");
      var format = new MascotResultTextFormat(MascotHeader.PROTEINPROPHET_PROTEIN_HEADER, MascotHeader.PROTEINPROPHET_PEPTIDE_HEADER);
      format.WriteToFile(resultFile, result);

      return new string[] { resultFile };
    }
  }
}
