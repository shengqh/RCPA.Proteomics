using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace RCPA.Proteomics.Image
{
  public class RectangleTransform
  {
    private ScaleTransform widthST;

    private ScaleTransform heightST;

    private Rectangle rec;

    public RectangleTransform(Rectangle rec, double maxWidth, double maxHeight)
    {
      this.rec = new Rectangle(rec.Location, rec.Size);
      this.widthST = new ScaleTransform(maxWidth, rec.Width);
      this.heightST = new ScaleTransform(maxHeight, rec.Height);
    }

    public int GetTransformX(double width)
    {
      return (int)widthST.AtoB(width) + rec.Left;
    }

    public int GetTransformY(double height)
    {
      return rec.Bottom - (int)heightST.AtoB(height);
    }
  }
}
