using System.Linq;
using System.Xml.Linq;

namespace RCPA.Proteomics.Format
{
  public class MultipleRaw2MSnProcessorOptions : AbstractRawConverterOptions, IXml
  {
    public MultipleRaw2MSnProcessorOptions()
    {
      this.TargetDirectory = string.Empty;
      this.RawFiles = new string[] { };
      this.GroupByMsLevel = true;
    }

    public string[] RawFiles { get; set; }

    public bool ParallelMode { get; set; }

    public void Save(XElement parentNode)
    {
      parentNode.Add(
        new XElement("TargetDirectory", TargetDirectory),
        new XElement("RawFiles", from rf in RawFiles
                                 select new XElement("File", rf)),
        new XElement("GroupByMode", GroupByMode),
        new XElement("GroupByMsLevel", GroupByMsLevel),
        new XElement("ParallelMode", ParallelMode),
        new XElement("ExtractRawMS3", ExtractRawMS3),
        new XElement("Overwrite", Overwrite));
    }

    public void Load(XElement parentNode)
    {
      TargetDirectory = parentNode.Element("TargetDirectory").Value;
      RawFiles = (from file in parentNode.Element("RawFiles").Elements("File")
                  select file.Value).ToArray();
      GroupByMode = bool.Parse(parentNode.Element("GroupByMode").Value);
      GroupByMsLevel = bool.Parse(parentNode.Element("GroupByMsLevel").Value);
      ParallelMode = bool.Parse(parentNode.Element("ParallelMode").Value);
      ExtractRawMS3 = bool.Parse(parentNode.Element("ExtractRawMS3").Value);
      Overwrite = bool.Parse(parentNode.Element("Overwrite").Value);
    }

    public override string Extension { get { return "ms"; } }
  }
}
