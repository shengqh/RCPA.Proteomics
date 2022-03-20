using RCPA.Gui;
using System.Xml.Linq;

namespace RCPA.Proteomics.Quantification.Reproducibility
{
  public class PrecursorReproducibitityConfiguration
  {
    [RcpaOption("FileNames", RcpaOptionType.StringArray)]
    public string[] FileNames { get; set; }

    [RcpaOption("PPMTolerance", RcpaOptionType.Double)]
    public double PPMTolerance { get; set; }

    public void LoadFromFile(string fileName)
    {
      XElement option = XElement.Load(fileName, LoadOptions.SetBaseUri);
      RcpaOptionUtils.LoadFromXml(this, option);
    }

    public void SaveToFile(string fileName)
    {
      XElement option = new XElement("configuration");
      RcpaOptionUtils.SaveToXml(this, option);
      option.Save(fileName);
    }
  }
}
