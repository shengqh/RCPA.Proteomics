namespace RCPA.Proteomics.Quantification.IsobaricLabelling
{
  public class IsobaricIndex
  {
    public IsobaricIndex(string name, int index)
    {
      this.Index = index;
      this.Name = name;
    }

    public int Index { get; set; }

    public double GetValue(IsobaricScan item)
    {
      return item[Index].Intensity;
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
