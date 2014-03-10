using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace RCPA.Proteomics.Snp
{
  public class SapPtmTable
  {
    public SapPtmTable()
      : this(new FileInfo(Application.ExecutablePath).DirectoryName + "\\SAP_PTM.txt")
    { }

    public Dictionary<Pair<char, char>, string> SapPtmMap { get; private set; }
    public SapPtmTable(string fileName)
    {
      SapPtmMap = new Dictionary<Pair<char, char>, string>();

      var lines = File.ReadAllLines(fileName);
      for (int i = 1; i < lines.Length; i++)
      {
        var parts = lines[i].Split(new string[] { "->", "\t" }, StringSplitOptions.RemoveEmptyEntries);
        if (parts.Length >= 4)
        {
          SapPtmMap[new Pair<char, char>(parts[0][0], parts[1][0])] = parts[2];
        }
      }
    }

    public string GetModification(char fromAminoacid, char toAminoacid)
    {
      var pair = new Pair<char, char>(fromAminoacid, toAminoacid);
      if (SapPtmMap.ContainsKey(pair))
      {
        return SapPtmMap[pair];
      }
      else
      {
        return string.Empty;
      }
    }
  }
}
