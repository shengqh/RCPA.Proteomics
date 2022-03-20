using System.Collections.Generic;
using System.Xml.Linq;

namespace RCPA.Proteomics.Summary.Uniform
{
  public abstract class AbstractTitleDatasetOptions : AbstractDatasetOptions, ITitleDatasetOptions
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

    public virtual ITitleParser GetTitleParser()
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
