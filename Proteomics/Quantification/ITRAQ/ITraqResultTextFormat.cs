//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.IO;
//using RCPA.Gui;

//namespace RCPA.Proteomics.Quantification.ITraq
//{
//  public class ITraqResultTextFormat2 : ProgressClass, IFileFormat<ITraqResult>
//  {
//    #region IFileFormat<ITraqResult> Members

//    public ITraqResult ReadFromFile(string fileName)
//    {
//      ITraqResult result = new ITraqResult();
//      using (StreamReader sr = new StreamReader(fileName))
//      {
//        Progress.SetRange(0, sr.BaseStream.Length);

//        var chars = new char[] { '\t' };

//        string header = sr.ReadLine();

//        bool hasIonInjectionTime = header.Contains("IonInjectionTime");
//        bool hasScanMode = header.Contains("ScanMode");

//        var ionInjectionTimeIndex = 6;
//        var scanModeIndex = 6;
//        if (hasScanMode)
//        {
//          ionInjectionTimeIndex++;
//        }

//        string line;
//        while ((line = sr.ReadLine()) != null)
//        {
//          Progress.SetPosition(sr.BaseStream.Position);

//          if (line.Trim().Length == 0)
//          {
//            break;
//          }

//          string[] parts = line.Split(chars);

//          string scanMode = string.Empty;
//          if (hasScanMode)
//          {
//            scanMode = parts[scanModeIndex];
//          }

//          ScanTime st = new ScanTime(Convert.ToInt32(parts[1]), 0.0);
//          if (hasIonInjectionTime)
//          {
//            st.IonInjectionTime = MyConvert.ToDouble(parts[ionInjectionTimeIndex]);
//            st.RetentionTime = MyConvert.ToDouble(parts[ionInjectionTimeIndex+1]);
//          }

//          ITraqItem item = new ITraqItem
//          {
//            Scan = st,
//            Experimental = parts[0],
//            ScanMode = scanMode,
//            I114 = MyConvert.ToDouble(parts[2]),
//            I115 = MyConvert.ToDouble(parts[3]),
//            I116 = MyConvert.ToDouble(parts[4]),
//            I117 = MyConvert.ToDouble(parts[5])
//          };

//          result.Add(item);
//        }
//      }

//      return result;
//    }

//    public void WriteToFile(string fileName, ITraqResult t)
//    {
//      using (StreamWriter sw = new StreamWriter(fileName))
//      {
//        sw.WriteLine("Experimental\tScan\tI114\tI115\tI116\tI117\tScanMode\tIonInjectionTime\tRetentionTime");
//        t.ForEach(item =>
//        {
//          sw.WriteLine(MyConvert.Format("{0}\t{1}\t{2:0.0}\t{3:0.0}\t{4:0.0}\t{5:0.0}\t{6}\t{7:0.0}\t{8:0.00}", item.Experimental, item.Scan.Scan, item.I114, item.I115, item.I116, item.I117, item.ScanMode, item.Scan.IonInjectionTime, item.Scan.RetentionTime));
//        });
//      }
//    }

//    #endregion
//  }
//}


