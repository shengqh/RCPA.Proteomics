using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using RCPA.Proteomics.Summary;
using System.Windows.Forms;

namespace RCPA.Proteomics.Summary.Uniform
{
  public abstract class AbstractTitleDatasetOptions : AbstractDatasetOptions
  {
    public string TitleParserName { get; set; }

    public override void Save(XElement parentNode)
    {
      base.Save(parentNode);

      parentNode.Add(new XElement("TitleFormat", TitleParserName));
    }

    public override void Load(XElement parentNode)
    {
      base.Load(parentNode);

      TitleParserName = parentNode.GetChildValue("TitleFormat", DefaultTitleParser.FORMAT_NAME);
    }

    public ITitleParser GetTitleParser()
    {
      List<ITitleParser> allParsers = TitleParserUtils.GetTitleParsers();
      foreach (ITitleParser parser in allParsers)
      {
        if (parser.FormatName.Equals(TitleParserName))
        {
          return parser;
        }
      }

      foreach (ITitleParser parser in allParsers)
      {
        if (parser.FormatName.Equals(DefaultTitleParser.FORMAT_NAME))
        {
          return parser;
        }
      }

      return new DefaultTitleParser();
    }
  }
}
