using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace RCPA.Proteomics.Quantification.Srm
{
  public class SrmPairedResultTextFormat : IFileWriter<SrmPairedResult>
  {
    private bool enabledOnly;

    public SrmPairedResultTextFormat(bool enabledOnly)
    {
      this.enabledOnly = enabledOnly;
    }

    //#region IFileReader<MRMPairedResult> Members

    //public MRMPairedResult ReadFromFile(string fileName)
    //{
    //  char[] splitChars = new char[] { '\t' };

    //  if (!File.Exists(fileName))
    //  {
    //    throw new FileNotFoundException(fileName);
    //  }

    //  Dictionary<int, MRMPairedPeptideItem> result = new Dictionary<int, MRMPairedPeptideItem>();
    //  using (StreamReader sr = new StreamReader(fileName))
    //  {
    //    sr.ReadLine();
    //    string line;
    //    while ((line = sr.ReadLine()) != null)
    //    {
    //      if (line.Trim().Length == 0)
    //      {
    //        break;
    //      }

    //      string[] parts = line.Split(splitChars);

    //      int peptideIndex = int.Parse(parts[0]);

    //      MRMPairedProductIon ion = new MRMPairedProductIon();
    //      ion.Light = new MRMTransaction(MyConvert.ToDouble(parts[1]), MyConvert.ToDouble(parts[2]));
    //      ion.Heavy = new MRMTransaction(MyConvert.ToDouble(parts[3]), MyConvert.ToDouble(parts[4]));
    //      ion.Ratio = MyConvert.ToDouble(parts[5]);
    //      ion.Distance = MyConvert.ToDouble(parts[6]);
    //      ion.RegressionCorrelation = MyConvert.ToDouble(parts[7]);

    //      if (parts.Length == 9)
    //      {
    //        ion.Enabled = bool.Parse(parts[8]);
    //      }
    //      else if (parts.Length == 12)
    //      {
    //        ion.LightArea = MyConvert.ToDouble(parts[8]);
    //        ion.HeavyArea = MyConvert.ToDouble(parts[9]);
    //        ion.EnabledScanCount = Convert.ToInt32(parts[10]);
    //        ion.Enabled = bool.Parse(parts[11]);
    //      }

    //      if (!result.ContainsKey(peptideIndex))
    //      {
    //        result[peptideIndex] = new MRMPairedPeptideItem();
    //      }

    //      result[peptideIndex].ProductIonPairs.Add(ion);
    //    }

    //    MRMPairedResult pr = new MRMPairedResult ();
    //    pr.AddRange(result.Values.ToList());

    //    XElement root = XElement.Parse(sr.ReadToEnd());
    //    pr.Options.FromXml(root);

    //    return pr;
    //  }
    //}

    //#endregion

    #region IFileWriter<MRMPairedResult> Members

    public void WriteToFile(string fileName, SrmPairedResult t)
    {
      using (StreamWriter sw = new StreamWriter(fileName))
      {
        sw.WriteLine("Index\tPrecursorPair\tEnabledProductPairCount\tRatio(L/H)\tSD(L/H)\tMaxSignalToNoise\tMaxRegressionCorrelation\tEnabled");
        var index = 1;
        foreach (var p in t)
        {
          if (enabledOnly && !p.Enabled)
          {
            continue;
          }

          var enabledCount = p.ProductIonPairs.Count(m => m.Enabled);
          sw.WriteLine("{0}\t{1}\t{2}\t{3:0.0000}\t{4}\t{5:0.0000}\t{6:0.0000}\t{7}",
            index,
            p.ToString(),
            enabledCount,
            enabledCount == 0 ? "-" : MyConvert.Format("{0:0.0000}", p.Ratio),
            double.IsInfinity(p.SD) || double.IsNaN(p.SD) ? "-" : MyConvert.Format("{0:0.0000}", p.SD),
            p.MaxSignalToNoise,
            p.MaxRegressionCorrelation,
            p.Enabled);
          index++;
        }

        sw.WriteLine();
        XElement config = new XElement("Root", t.Options.ToXml());
        sw.WriteLine(config.ToString(SaveOptions.None));
      }

      var productFile = FileUtils.ChangeExtension(fileName, "transRatio");
      using (StreamWriter sw = new StreamWriter(productFile))
      {
        sw.WriteLine("Index\tLightPrecursor\tLightProductIon\tLight(S/N)\tLightArea\tHeavyPrecursor\tHeavyProductIon\tHeavy(S/N)\tHeavyArea\tRatio\tDistance\tRegressionCorrelation\tEnabledScanCount\tEnabled");
        var index = 1;
        foreach (var p in t)
        {
          if (enabledOnly && !p.Enabled)
          {
            continue;
          }

          foreach (var m in p.ProductIonPairs)
          {
            if (enabledOnly && !m.Enabled)
            {
              continue;
            }

            if (m.Heavy != null)
            {
              sw.WriteLine("{0}\t{1:0.0000}\t{2:0.0000}\t{3:0.0000}\t{4:0.0000}\t{5:0.0000}\t{6:0.0000}\t{7:0.0000}\t{8:0.0000}\t{9:0.0000}\t{10:0.0}\t{11:0.0000}\t{12}\t{13}",
                index,
                m.Light.PrecursorMZ,
                m.Light.ProductIon,
                m.LightSignalToNoise,
                m.LightArea,
                m.Heavy.PrecursorMZ,
                m.Heavy.ProductIon,
                m.HeavySignalToNoise,
                m.HeavyArea,
                m.Ratio,
                m.Distance,
                m.RegressionCorrelation,
                m.EnabledScanCount,
                m.Enabled);
            }
            else
            {
              sw.WriteLine("{0}\t{1:0.0000}\t{2:0.0000}\t{3:0.0000}\t{4:0.0000}\t{5:0.0000}\t{6:0.0000}\t{7:0.0000}\t{8:0.0000}\t{9:0.0000}\t{10:0.0}\t{11:0.0000}\t{12}\t{13}",
                index,
                m.Light.PrecursorMZ,
                m.Light.ProductIon,
                m.LightSignalToNoise,
                m.LightArea,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                m.EnabledScanCount,
                m.Enabled);
            }
            index++;
          }
        }

        sw.WriteLine();
        XElement config = new XElement("Root", t.Options.ToXml());
        sw.WriteLine(config.ToString(SaveOptions.None));
      }
    }

    #endregion
  }
}
