using RCPA.Parser;
using RCPA.Proteomics.Mascot;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace RCPA.Proteomics.Summary
{
  public class TitleParser : ITitleParser
  {
    private ParserFormat format;

    private Regex rawFileRegex = null;

    private Regex scanNumberRegex = null;

    private Regex mainRegex = null;

    private Regex rtRegex = null;

    public TitleParser(ParserFormat format)
    {
      this.format = format;

      if (!string.IsNullOrEmpty(format.IdentityRegex))
      {
        this.mainRegex = new Regex(format.IdentityRegex);
      }

      foreach (ParserItem item in format)
      {
        if (item.ItemName.Equals("rawFile") && item.RegularExpression.Length > 0)
        {
          rawFileRegex = new Regex(item.RegularExpression);
        }
        else if (item.ItemName.Equals("scanNumber") && item.RegularExpression.Length > 0)
        {
          scanNumberRegex = new Regex(item.RegularExpression);
        }
        else if (item.ItemName.Equals("retentionTime") && item.RegularExpression.Length > 0)
        {
          rtRegex = new Regex(item.RegularExpression);
        }
      }

      if (rawFileRegex == null && scanNumberRegex == null)
      {
        throw new ArgumentException("There is no rawFile and scanNumber parsing definition " + format.FormatName);
      }
    }

    #region IParser<string,SequestFilename> Members

    public SequestFilename GetValue(string obj)
    {
      SequestFilename result = new SequestFilename();

      if (rawFileRegex != null)
      {
        Match m = rawFileRegex.Match(obj);
        if (!m.Success)
        {
          throw new ArgumentException("Cannot parse raw file from title " + obj);
        }

        result.Experimental = m.Groups[1].Value;
      }

      if (scanNumberRegex != null)
      {
        Match m = scanNumberRegex.Match(obj);
        if (!m.Success)
        {
          throw new ArgumentException("Cannot parse scan number from title " + obj);
        }

        result.FirstScan = int.Parse(m.Groups[1].Value);
        if (m.Groups.Count > 2)
        {
          result.LastScan = int.Parse(m.Groups[2].Value);
        }
        else
        {
          result.LastScan = result.FirstScan;
        }
      }

      if (rtRegex != null)
      {
        Match m = rtRegex.Match(obj);
        if (!m.Success)
        {
          throw new ArgumentException("Cannot parse retention time from title " + obj);
        }

        result.RetentionTime = double.Parse(m.Groups[1].Value);
      }

      result.Extension = "dta";

      return result;
    }

    public string FormatName
    {
      get { return format.FormatName; }
    }

    public string Example
    {
      get { return format.Sample; }
    }

    #endregion

    public override string ToString()
    {
      return format.ToString();
    }

    #region ITitleParser Members

    public bool IsMatch(string title)
    {
      if (mainRegex == null)
      {
        return false;
      }
      return mainRegex.Match(title).Success;
    }

    #endregion
  }

  public class DefaultTitleParser : ITitleParser
  {
    public static readonly string FORMAT_NAME = "Default";

    private int moveCount = 0;

    private List<ITitleParser> parsers;
    public DefaultTitleParser(List<ITitleParser> parsers)
    {
      this.parsers = new List<ITitleParser>(parsers);
    }

    public DefaultTitleParser()
    {
      this.parsers = new List<ITitleParser>();
    }

    #region IParser<string,SequestFilename> Members

    public SequestFilename GetValue(string obj)
    {
      for (int i = 0; i < parsers.Count; i++)
      {
        ITitleParser parser = parsers[i];
        try
        {
          SequestFilename result = parser.GetValue(obj);

          if (i != 0 && moveCount < 10)
          {
            parsers.Remove(parser);
            parsers.Insert(0, parser);
            moveCount++;
          }

          return result;
        }
        catch (Exception)
        {
        }
      }

      return MascotUtils.ParseTitle(obj, 2);
    }

    public string FormatName
    {
      get
      {
        return FORMAT_NAME;
      }
    }

    #endregion

    public override string ToString()
    {
      return FORMAT_NAME;
    }

    #region ITitleParser Members


    public string Example
    {
      get { return string.Empty; }
    }

    public bool IsMatch(string title)
    {
      return false;
    }

    #endregion
  }
}
