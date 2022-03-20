using System;

namespace RCPA.Proteomics.Summary
{
  public struct LoopDefinition
  {
    public LoopDefinition(LoopDefinition source)
    {
      this.from = source.from;
      this.to = source.to;
      this.step = source.step;
      CheckValue();
    }

    public LoopDefinition(double from, double to, double step)
    {
      this.from = from;
      this.to = to;
      this.step = step;
      CheckValue();
    }

    private void CheckValue()
    {
      if ((from < to && step <= 0) || (from > to && step >= 0))
      {
        throw new ArgumentException(MyConvert.Format("Error argument of LoopDefinition : From={0}, To={1}, Step={2}.", from, to, step));
      }
    }

    private double from;

    public double From
    {
      get { return from; }
      set { from = value; }
    }

    private double to;

    public double To
    {
      get { return to; }
      set { to = value; }
    }

    private double step;

    public double Step
    {
      get { return step; }
      set { step = value; }
    }
  }
}
