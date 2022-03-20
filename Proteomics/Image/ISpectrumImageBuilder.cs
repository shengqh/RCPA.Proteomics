using System.Drawing;

namespace RCPA.Proteomics.Image
{
  public interface ISpectrumImageBuilder
  {
    /// <summary>
    /// 将鉴定结果画在图像的特定区域内。
    /// </summary>
    /// <param name="dc"></param>
    /// <param name="rec"></param>
    /// <param name="identifiedResult"></param>
    void Draw(Graphics dc, Rectangle rec, IIdentifiedPeptideResult identifiedResult);
  }
}
