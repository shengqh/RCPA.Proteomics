using System;
using System.Collections.Generic;
using System.Text;
using RCPA.Parser;

namespace RCPA.Proteomics.Summary
{
  public class TitleParserUtils
  {
    public static List<ITitleParser> GetTitleParsers()
    {
      List<ITitleParser> result = new List<ITitleParser>();

      ParserFormatList lstFormat = ParserFormatList.ReadFromOptionFile("TITLElineParseDefinitions");
      if (null != lstFormat)
      {
        List<ITitleParser> parsers = lstFormat.ConvertAll(format => (ITitleParser)(new TitleParser(format)));

        DefaultTitleParser auto = new DefaultTitleParser(parsers);

        result = new List<ITitleParser>(parsers);

        result.Add(auto);
      }
      else
      {
        result.Add(new DefaultTitleParser());
      }

      return result;
    }

    public static ITitleParser GuessTitleParser(string title, IEnumerable<ITitleParser> parsers)
    {
      foreach (var parser in parsers)
      {
        if (parser.IsMatch(title))
        {
          return parser;
        }
      }

      return null;
    }

    public static ITitleParser GuessTitleParser(string title)
    {
      var parsers = GetTitleParsers();
      return GuessTitleParser(title, parsers);
    }
  }
}
