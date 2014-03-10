using System.Collections.Generic;
using System.IO;
using System.Linq;
using RCPA.Proteomics.Summary;
using RCPA.Utils;

namespace RCPA.Proteomics.Mascot
{
  public class MascotPeptideTextFormat : AbstractPeptideTextFormat
  {
    public MascotPeptideTextFormat()
      : this(MascotHeader.MASCOT_PEPTIDE_HEADER)
    { }

    public MascotPeptideTextFormat(string header)
      : base(header)
    { }

    public MascotPeptideTextFormat(List<IIdentifiedSpectrum> spectra)
      : base(MascotHeader.MASCOT_PEPTIDE_HEADER, spectra)
    { }

    public MascotPeptideTextFormat(string header, List<IIdentifiedSpectrum> spectra)
      : base(header, spectra)
    { }

    /// <summary>
    /// 当从文件读取数据后，可进行相应的一些预处理
    /// </summary>
    /// <param name="spectra">读取的谱图</param>
    protected virtual void DoAfterRead(List<IIdentifiedSpectrum> spectra) { }

    #region IFileFormat<List<IIdentifiedSpectrum>> Members

    public override List<IIdentifiedSpectrum> ReadFromFile(string filename)
    {
      var result = new List<IIdentifiedSpectrum>();

      using (var sr = new StreamReader(filename))
      {
        string line = sr.ReadLine();

        this.PeptideFormat = new PeptideLineFormat(line);

        while ((line = sr.ReadLine()) != null)
        {
          if (line.Trim().Length == 0)
          {
            break;
          }


          result.Add(PeptideFormat.ParseString(line));
        }
      }

      string enzymeFile = filename + ".enzyme";
      if (File.Exists(enzymeFile))
      {
        new ProteaseFile().Fill(enzymeFile, result);
      }

      DoAfterRead(result);

      return result;
    }

    public override void WriteToFile(string filename, List<IIdentifiedSpectrum> t)
    {
      using (var sw = new StreamWriter(filename))
      {
        sw.WriteLine(PeptideFormat.GetHeader());
        foreach (IIdentifiedSpectrum mph in t)
        {
          sw.WriteLine(PeptideFormat.GetString(mph));
        }

        sw.WriteLine();
        sw.WriteLine("----- summary -----");
        var totalCount = IdentifiedSpectrumUtils.GetSpectrumCount(t);
        var totalUniqueCount = IdentifiedSpectrumUtils.GetUniquePeptideCount(t);
        sw.WriteLine("Total spectra: " + totalCount);
        sw.WriteLine("Total peptides: " + totalUniqueCount);

        var tags = (from s in t
                    select s.Tag).Distinct().ToList();
        if (tags.Count > 1)
        {
          tags.Sort();

          sw.WriteLine();
          sw.WriteLine("Tag\tSpectra\tPeptides");
          sw.WriteLine("All\t{0}\t{1}", totalCount, totalUniqueCount);

          foreach (var tag in tags)
          {
            var tagspectra = from s in t
                             where s.Tag == tag
                             select s;
            sw.WriteLine("{0}\t{1}\t{2}", tag, IdentifiedSpectrumUtils.GetSpectrumCount(tagspectra), IdentifiedSpectrumUtils.GetUniquePeptideCount(tagspectra));
          }
        }
      }

      string enzymeFile = filename + ".enzyme";
      new ProteaseFile().Write(enzymeFile, t);
    }

    #endregion

    protected override string GetDefaultPeptideHeader()
    {
      return MascotHeader.MASCOT_PEPTIDE_HEADER;
    }

    protected override string GetEngineName()
    {
      return "mascot";
    }
  }
}