using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using RCPA.Proteomics.Mascot;

namespace RCPA.Proteomics.Quantification.Srm
{
  public class SrmTransitionAgilentFormat:IFileFormat<List<AgilentSrmTransition>>
  {
    #region IFileReader<List<AgilentMRMTransaction>> Members

    public List<AgilentSrmTransition> ReadFromFile(string fileName)
    {
      var result = new List<AgilentSrmTransition>();
      using (StreamReader sr = new StreamReader(fileName))
      {
        sr.ReadLine();
        string line;
        while ((line = sr.ReadLine()) != null)
        {
          if (line.Trim().Length == 0)
          {
            break;
          }

          var parts = line.Split('\t');
          var mrm = new AgilentSrmTransition();
          result.Add(mrm);

          mrm.PrecursorFormula = parts[0];
          mrm.IsHeavy = Convert.ToBoolean(parts[1]);
          mrm.IsQual = Convert.ToBoolean(parts[2]);
          mrm.PrecursorMZ = MyConvert.ToDouble(parts[3]);
          mrm.Q1Resolution = parts[4];
          mrm.ProductIon = MyConvert.ToDouble(parts[5]);
          mrm.Q3Resolution = parts[6];
          mrm.Fragmentor = MyConvert.ToDouble(parts[7]);
          mrm.CollisionEnergy = MyConvert.ToDouble(parts[8]);
          mrm.ExpectCenterRetentionTime = MyConvert.ToDouble(parts[9]);
          mrm.DeltaRT = MyConvert.ToDouble(parts[10]);
          mrm.MSpolarity = parts[11];
        }
      }

      return result;
    }

    #endregion

    #region IFileWriter<List<AgilentMRMTransaction>> Members

    public void WriteToFile(string fileName, List<AgilentSrmTransition> t)
    {
      using (StreamWriter sw = new StreamWriter(fileName))
      {
        sw.WriteLine("Peptide Name\tHeavy Labeled\tUnknown\tPrecursor m/z\tQ1 Resolution\tProduct ion m/z\tQ3 Resolution\tFragmentor\tCollision energy\tRT\tDelta RT\tMS polarity");
        foreach (var m in t)
        {
          sw.WriteLine("{0}\t{1}\t{2}\t{3:0.0000}\t{4}\t{5:0.0000}\t{6}\t{7:0.#}\t{8:0.#}\t{9:0.#}\t{10:0.#}\t{11}",
            m.PrecursorFormula,
            m.IsHeavy,
            m.IsQual,
            m.PrecursorMZ,
            m.Q1Resolution,
            m.ProductIon,
            m.Q3Resolution,
            m.Fragmentor,
            m.CollisionEnergy,
            m.ExpectCenterRetentionTime,
            m.DeltaRT,
            m.MSpolarity);
        }
      }
    }

    #endregion
  }
}
