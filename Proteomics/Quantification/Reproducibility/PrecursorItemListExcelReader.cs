using Microsoft.Office.Interop.Excel;
using System;
using System.Reflection;

namespace RCPA.Proteomics.Quantification.Reproducibility
{
  public class PrecursorItemListExcelReader : IFileReader<PrecursorItemList>
  {
    #region IFileReader<PrecursorItemList> Members


    public PrecursorItemList ReadFromFile(string fileName)
    {
      PrecursorItemList result = new PrecursorItemList();

      Application xApp = new Application();

      //得到WorkBook对象, 可以用两种方式之一: 下面的是打开已有的文件
      Workbook xBook = xApp.Workbooks._Open(fileName,
        Missing.Value, Missing.Value, Missing.Value, Missing.Value
        , Missing.Value, Missing.Value, Missing.Value, Missing.Value
        , Missing.Value, Missing.Value, Missing.Value, Missing.Value);
      try
      {
        Worksheet xSheet = (Worksheet)xBook.Sheets[1];

        for (int i = 12; i <= xSheet.Rows.Count; i++)
        {
          Range obj = xSheet.get_Range("B" + i.ToString(), Missing.Value);
          if (null == obj || null == obj.Value2 || 0 == obj.Value2.ToString().Length)
          {
            break;
          }

          PrecursorItem item = new PrecursorItem();

          item.RetentionTime = MyConvert.ToDouble(xSheet.get_Range("B" + i.ToString(), Missing.Value).Value2);
          item.Mz = MyConvert.ToDouble(xSheet.get_Range("D" + i.ToString(), Missing.Value).Value2);
          item.Abundance = MyConvert.ToDouble(xSheet.get_Range("F" + i.ToString(), Missing.Value).Value2);

          result.Add(item);
        }
      }
      finally
      {
        xBook.Close(false, Type.Missing, Type.Missing);
      }

      return result;
    }

    #endregion
  }
}
