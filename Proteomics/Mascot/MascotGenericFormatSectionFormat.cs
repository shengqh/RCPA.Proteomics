using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;
using RCPA.Proteomics.Spectrum;
using RCPA.Utils;

namespace RCPA.Proteomics.Mascot
{
  public class MascotGenericFormatSectionReader
  {
    private readonly StreamReader reader;

    public MascotGenericFormatSectionReader(StreamReader reader)
    {
      this.reader = reader;
    }

    public List<string> Next()
    {
      var result = new List<string>();

      string lastLine;
      while ((lastLine = this.reader.ReadLine()) != null)
      {
        if (lastLine.Length == 0)
        {
          continue;
        }

        result.Add(lastLine);

        if (lastLine.StartsWith(MascotGenericFormatConstants.END_PEAK_LIST_TAG))
        {
          break;
        }
      }

      return result;
    }

    public bool HasNext()
    {
      return MascotGenericFormatIterator<Peak>.CheckHasNext(this.reader);
    }

    public string GetNextTitle()
    {
      string result = string.Empty;

      long position = StreamUtils.GetCharpos(this.reader);
      try
      {
        string line;
        while ((line = this.reader.ReadLine()) != null)
        {
          if (line.StartsWith(MascotGenericFormatConstants.TITLE_TAG))
          {
            result = line.Substring(line.IndexOf('=') + 1);
            break;
          }
        }
      }
      finally
      {
        this.reader.SetCharpos(position);
      }

      return result;
    }

    public void SkipNext()
    {
      string lastLine;

      while ((lastLine = this.reader.ReadLine()) != null)
      {
        if (lastLine.Length == 0)
        {
          continue;
        }

        if (lastLine.StartsWith(MascotGenericFormatConstants.END_PEAK_LIST_TAG))
        {
          break;
        }
      }
    }
  }
}
