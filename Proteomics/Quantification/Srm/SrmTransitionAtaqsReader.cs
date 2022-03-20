using System;
using System.Collections.Generic;
using System.IO;

namespace RCPA.Proteomics.Quantification.Srm
{
  public class SrmTransitionAtaqsReader : IFileReader<List<SrmTransition>>
  {
    #region IFileReader<List<SrmTransition>> Members

    public List<SrmTransition> ReadFromFile(string fileName)
    {
      List<SrmTransition> result = new List<SrmTransition>();
      using (StreamReader sr = new StreamReader(fileName))
      {
        string line;
        bool bFirst = true;
        while ((line = sr.ReadLine()) != null)
        {
          var parts = line.Split(',');
          if (parts.Length < 10)
          {
            break;
          }

          double value;
          if (bFirst && !MyConvert.TryParse(parts[3], out value))
          {
            continue;
          }

          SrmTransition st = new SrmTransition();
          result.Add(st);
          st.ObjectName = parts[0];
          st.PrecursorFormula = parts[1];
          st.PrecursorCharge = Convert.ToInt32(parts[2]);
          st.PrecursorMZ = MyConvert.ToDouble(parts[3]);
          st.ProductIonCharge = Convert.ToInt32(parts[4]);
          st.ProductIon = MyConvert.ToDouble(parts[5]);
          st.IonType = parts[6];
          st.IonIndex = Convert.ToInt32(parts[7]);
          st.ExpectCenterRetentionTime = MyConvert.ToDouble(parts[8]);
          st.CollisionEnergy = MyConvert.ToDouble(parts[9]);

          bFirst = false;
        }
      }
      return result;
    }

    #endregion

    public override string ToString()
    {
      return "ATAQS CSV Format";
    }
  }
}
