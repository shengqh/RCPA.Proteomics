using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RCPA.Proteomics.Spectrum;

namespace RCPA.Proteomics.Mascot
{
  public class MascotTitle
  {
    public MascotTitle(string name, string title, Func<MascotGenericFormatWriter<Peak>> allocateWriter)
    {
      this.Name = name;
      this.Title = title;
      this.allocateWriter = allocateWriter;
    }

    public string Name { get; private set; }

    public string Title { get; private set; }

    private Func<MascotGenericFormatWriter<Peak>> allocateWriter;

    public MascotGenericFormatWriter<Peak> CreateWriter()
    {
      return allocateWriter();
    }

    public override string ToString()
    {
      return Title;
    }
  }

  public static class MascotTitleFactory
  {
    private static List<MascotTitle> _titles;

    public static MascotTitle[] Titles
    {
      get
      {
        return GetTitles().ToArray();
      }
    }

    private static List<MascotTitle> GetTitles()
    {
      if (_titles == null)
      {
        _titles = new List<MascotTitle>();
        _titles.Add(new MascotTitle("SEQUEST", "{Raw File Name}.{First Scan Number}.{Last Scan Number}.{Precursor Charge}.dta -- recommended", () => new MascotGenericFormatSequestWriter<Peak>()));
        _titles.Add(new MascotTitle("RAWCMPD", "{Raw File Name}, Cmpd {Scan Number}, +MSn({Precursor Mz}), {Retention Time} min", () => new MascotGenericFormatSqhWriter<Peak>()));
        _titles.Add(new MascotTitle("CMPD", "Cmpd {Scan Number}, +MSn({Precursor Mz}), {Retention Time} min", () => new MascotGenericFormatYehiaWriter<Peak>()));
      }
      return _titles;
    }

    public static void RegisterTitle(MascotTitle title)
    {
      GetTitles().Add(title);
    }

    public static MascotTitle FindTitle(string titleName)
    {
      foreach (var title in GetTitles())
      {
        if (titleName.Equals(title.Name))
        {
          return title;
        }
      }
      return null;
    }


    public static MascotTitle FindTitleOrDefault(string titleName)
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
