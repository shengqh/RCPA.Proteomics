using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RCPA.Proteomics.Spectrum;

namespace RCPA.Proteomics.Mascot
{
  public static class MascotTitleFactory
  {
    private static List<ITitleFormat> _titles;

    public static ITitleFormat[] Titles
    {
      get
      {
        return GetTitles().ToArray();
      }
    }

    private static List<ITitleFormat> GetTitles()
    {
      if (_titles == null)
      {
        _titles = new List<ITitleFormat>();
        _titles.Add(new TitleFormatSequest());
        _titles.Add(new TitleFormatProteomeDiscoverer());
        _titles.Add(new TitleFormatRawCmpd());
        _titles.Add(new TitleFormatCmpd());
      }
      return _titles;
    }

    public static void RegisterTitle(ITitleFormat title)
    {
      GetTitles().Add(title);
    }

    public static ITitleFormat FindTitle(string titleName)
    {
      foreach (var title in GetTitles())
      {
        if (titleName.Equals(title.FormatName))
        {
          return title;
        }
      }
      return null;
    }


    public static ITitleFormat FindTitleOrDefault(string titleName)
    {
      var result = FindTitle(titleName);
      if (null == result)
      {
        result = GetTitles()[0];
      }
      return result;
    }
  }
}
