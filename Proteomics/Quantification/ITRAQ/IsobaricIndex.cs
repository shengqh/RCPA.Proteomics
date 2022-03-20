namespace RCPA.Proteomics.Quantification.ITraq
{
  public class IsobaricIndex
  {
    public IsobaricIndex(int index)
    {
      this.Index = index;
      this.Name = "I" + index.ToString();
    }

    public int Index { get; private set; }

    public double GetValue(IsobaricItem item)
    {
      return item[Index];
    }

    public string Name { get; set; }

    public override string ToString()
    {
      return Name;
    }

    public string ChannelRatioName
    {
      get { return this.Name + "/REF"; }
    }
  }
}
