using Microsoft.Office.Interop.Excel;

namespace RCPA
{
  public static class ExcelExtensions
  {
    public static string Value(this Worksheet xSheet, int row, int col)
    {
      Range obj = (Range)xSheet.Cells[row, col];
      if (null == obj || null == obj.Value2 || 0 == obj.Value2.ToString().Length)
      {
        return null;
      }

      return obj.Value2.ToString();
    }
  }
}
