using RCPA.Proteomics.Mascot;
using RCPA.Proteomics.Spectrum;
using RCPA.Proteomics.Summary;
using System.Collections.Generic;
using System.Linq;

namespace RCPA.Proteomics.Format
{
  public class CombineMascotGenericFormatOption
  {
    public string HeaderFile { get; set; }

    public ITitleParser HeaderParser { get; set; }

    public string PeakFile { get; set; }

    public ITitleParser PeakParser { get; set; }

    public string TargetFile { get; set; }

    public MascotGenericFormatWriter<Peak> Writer { get; set; }
  }

  /// <summary>
  /// 用于将两个mgf整合到一起，但是用来自一个文件的peak list替换掉另一个文件的peak list。
  /// 比如ProteomeDiscovery产生的mgf文件，对母离子判定比较好，而用turboraw2mgf产生的peak list比较好，
  /// 就可以综合这两者的优势。
  /// </summary>
  public class ReplaceMascotGenericFormatProcessor : AbstractThreadFileProcessor
  {
    private CombineMascotGenericFormatOption option;

    public ReplaceMascotGenericFormatProcessor(CombineMascotGenericFormatOption option)
    {
      this.option = option;
    }

    public override IEnumerable<string> Process(string targetFile)
    {
      var hMap = ReadPeakMap(option.HeaderFile, option.HeaderParser);
      var pMap = ReadPeakMap(option.PeakFile, option.PeakParser);

      var mgfFormat = new MascotGenericFormatWriter<Peak>();
      foreach (var key in pMap.Keys)
      {
        if (hMap.ContainsKey(key))
        {
          pMap[key].PrecursorMZ = hMap[key].PrecursorMZ;
          pMap[key].PrecursorCharge = hMap[key].PrecursorCharge;
        }
      }

      var result = pMap.Values.ToList();

      //foreach (var key in hMap.Keys)
      //{
      //  if (!pMap.ContainsKey(key))
      //  {
      //    result.Add(pMap[key]);
      //  }
      //}

      foreach (var v in result)
      {
        v.Annotations.Remove(MascotGenericFormatConstants.TITLE_TAG);
      }

      result.Sort((m1, m2) => m1.FirstScan - m2.FirstScan);

      option.Writer.WriteToFile(targetFile, result);

      return new string[] { targetFile };
    }

    private static Dictionary<string, PeakList<Peak>> ReadPeakMap(string file, ITitleParser parser)
    {
      var reader = new MascotGenericFormatReader<Peak>();
      List<PeakList<Peak>> hList = reader.ReadFromFile(file);
      return hList.ToDictionary(m =>
      {
        var filename = parser.GetValue(m.Annotations[MascotGenericFormatConstants.TITLE_TAG] as string);
        if (string.IsNullOrEmpty(m.Experimental))
        {
          m.Experimental = filename.Experimental;
        }
        if (m.FirstScan == 0)
        {
          m.FirstScan = filename.FirstScan;
        }
        if (filename.Charge > 0)
        {
          m.PrecursorCharge = filename.Charge;
        }
        return filename.FirstScan.ToString();
      });
    }
  }
}
