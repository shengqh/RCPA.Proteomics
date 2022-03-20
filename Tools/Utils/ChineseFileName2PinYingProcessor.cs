using RCPA.Utils;
using System;
using System.Collections.Generic;
using System.IO;

namespace RCPA.Tools.Utils
{
  public class ChineseFileName2PinYingProcessor : IFileProcessor
  {
    public static string version = "1.0.0";

    #region IFileProcessor Members

    public IEnumerable<string> Process(string mp3dir)
    {
      FileInfo[] files = new DirectoryInfo(mp3dir).GetFiles("*.mp3");
      foreach (FileInfo fi in files)
      {
        string oldFilename = fi.Name.Replace("£¨", "(").Replace("£©", ")").Replace("£¬", "").Replace(" ", "");
        string newFilename = Chinese.GetSpells(oldFilename);

        if (!fi.Name.Equals(newFilename))
        {
          Console.Out.WriteLine(fi.Name + "-->" + newFilename);
          fi.MoveTo(fi.DirectoryName + "\\" + newFilename);
        }
      }

      return new List<string>();
    }

    #endregion
  }
}
