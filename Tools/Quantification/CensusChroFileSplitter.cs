using System.Collections.Generic;
using System.IO;
using System.Xml;
using RCPA.Utils;
using System;

namespace RCPA.Tools.Quantification
{
  public class CensusChroFileSplitter : AbstractThreadFileProcessor
  {
    private readonly int splitCount;

    public CensusChroFileSplitter(int splitCount)
    {
      this.splitCount = splitCount;
    }

    public override IEnumerable<string> Process(string filename)
    {
      var result = new string[this.splitCount];
      var xml = new XmlDocument();

      Progress.SetMessage("Reading document ...");
      xml.Load(filename);
      var xmlHelper = new XmlHelper(xml);
      Progress.SetMessage("Reading document finished.");

      Progress.SetMessage("Sliming document ...");
      CensusChroSlimBuilder.SlimDocument(xml, Progress);
      Progress.SetMessage("Sliming document finished.");

      List<XmlNode> nodes = xmlHelper.GetChildren(xml.DocumentElement, "protein");

      Progress.SetRange(0, this.splitCount);
      for (int i = 0; i < this.splitCount; i++)
      {
        result[i] = FileUtils.ChangeExtension(filename, (i + 1) + new FileInfo(filename).Extension);

        List<XmlNode> curNodes = xmlHelper.GetChildren(xml.DocumentElement, "protein");
        foreach (XmlNode node in curNodes)
        {
          xml.DocumentElement.RemoveChild(node);
        }

        for (int j = 0; j < nodes.Count; j++)
        {
          if (j % this.splitCount == i)
          {
            xml.DocumentElement.AppendChild(nodes[j]);
          }
        }

        Progress.SetMessage("Writing document " + result[i] + " ...");
        xml.Save(result[i]);
        Progress.SetMessage("Writing document finished.");
        Progress.SetPosition(i + 1);
      }

      return result;
    }
  }
}