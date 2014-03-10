using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RCPA.Proteomics.Summary;
using Microsoft.Office.Interop.Excel;
using RCPA.Utils;
using RCPA.Gui;
using System.Reflection;
using System.Text.RegularExpressions;

namespace RCPA.Proteomics.Sequest
{
  public class MsfExcelToNoredundantProcessor : AbstractMsfToNoredundantProcessor
  {
    public override List<IIdentifiedProtein> ParseProteins(string fileName)
    {
      Dictionary<string, IIdentifiedProtein> proteinMap = new Dictionary<string, IIdentifiedProtein>();

      Application xApp = new Application();

      //得到WorkBook对象, 可以用两种方式之一: 下面的是打开已有的文件
      Workbook xBook = xApp.Workbooks._Open(fileName,
        Missing.Value, Missing.Value, Missing.Value, Missing.Value
        , Missing.Value, Missing.Value, Missing.Value, Missing.Value
        , Missing.Value, Missing.Value, Missing.Value, Missing.Value);

      try
      {
        Worksheet xSheet = (Worksheet)xBook.Sheets[1];

        int fromRow = 2;
        int endRow = fromRow;

        for (; endRow <= xSheet.Rows.Count; endRow++)
        {
          string b = xSheet.Value('B', endRow);
          if (null == b)
          {
            break;
          }
        }
        endRow--;

        Progress.SetRange(fromRow, endRow);
        Progress.SetMessage("Parsing file ...");
        for (int i = fromRow; i <= endRow; i++)
        {
          Progress.SetPosition(i);

          string seq = xSheet.Value('A', i);
          if (null == seq)//蛋白质信息
          {
            continue;
          }

          string deltaCn = xSheet.Value('I', i);
          if (null == deltaCn)//rank > 1
          {
            continue;
          }

          string protein = xSheet.Value('B', i);
          if (!proteinMap.ContainsKey(protein))
          {
            var p = new IdentifiedProtein(protein);

            p.Coverage = MyConvert.ToDouble(xSheet.Value('C', i + 2));
            p.MolecularWeight = MyConvert.ToDouble(xSheet.Value('F', i + 2)) * 1000;
            p.IsoelectricPoint = MyConvert.ToDouble(xSheet.Value('G', i + 2));
            p.Score = MyConvert.ToDouble(xSheet.Value('H', i + 2));
            p.Description = xSheet.Value('I', i + 2);

            proteinMap[protein] = p;
          }

          var pro = proteinMap[protein];

          IdentifiedSpectrum spectrum = new IdentifiedSpectrum();
          IdentifiedPeptide peptide = new IdentifiedPeptide(spectrum);
          peptide.Sequence = seq.ToUpper();
          peptide.AddProtein(protein);
          spectrum.Modifications = xSheet.Value('F', i);
          spectrum.DeltaScore = MyConvert.ToDouble(deltaCn);
          spectrum.Charge = Convert.ToInt32(xSheet.Value('K', i));
          spectrum.ObservedMz = MyConvert.ToDouble(xSheet.Value('L', i));
          spectrum.TheoreticalMH = MyConvert.ToDouble(xSheet.Value('M', i));
          spectrum.Ions = xSheet.Value('S', i);
          spectrum.Query.FileScan.FirstScan = Convert.ToInt32(xSheet.Value('P', i));
          spectrum.Query.FileScan.LastScan = Convert.ToInt32(xSheet.Value('Q', i));
          spectrum.Query.FileScan.Experimental = FileUtils.RemoveAllExtension(xSheet.Value('T', i));

          pro.Peptides.Add(peptide);
        }
      }
      finally
      {
        xBook.Close(false, Type.Missing, Type.Missing);
      }

      var proteins = proteinMap.Values.ToList();
      return proteins;
    }
  }
}
