using System.Collections.Generic;

namespace RCPA.Tools.Quantification
{
  public class MultipleFileQuantificationDefinition
  {
    /// <summary>
    /// 哪些raw文件名是属于同一个dataset的。只有同一个dataset的肽段定量结果用于相应的蛋白质定量。key是dataset的名字。
    /// </summary>
    public Dictionary<string, List<string>> Datasets { get; set; }

    /// <summary>
    /// 哪些raw文件名是属于扩展定量的，例如第一个dataset中的第一个raw文件跟第二个dataset中的第一个raw文件，就归属于同一个PairedRawFile列表。
    /// </summary>
    public List<List<string>> PairedRawFiles { get; set; }

    public MultipleFileQuantificationDefinition()
    {
      this.Datasets = new Dictionary<string, List<string>>();
      this.PairedRawFiles = new List<List<string>>();
    }
  }
}
