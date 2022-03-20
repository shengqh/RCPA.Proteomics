using RCPA.Gui;
using RCPA.Proteomics.Summary;
using System;
using System.Collections.Generic;
using System.IO;

namespace RCPA.Proteomics.Sequest
{
  public class SequestPeptideTextFormat : ProgressClass, IFileFormat<List<IIdentifiedSpectrum>>
  {
    public SequestPeptideTextFormat()
    {
      this.PeptideFormat = new LineFormat<IIdentifiedSpectrum>(
        IdentifiedSpectrumPropertyConverterFactory.GetInstance(), SequestHeader.SEQUEST_PEPTIDE_HEADER, "sequest");
    }

    public SequestPeptideTextFormat(string header)
    {
      this.PeptideFormat = new LineFormat<IIdentifiedSpectrum>(
        IdentifiedSpectrumPropertyConverterFactory.GetInstance(), header, "sequest");
    }

    public LineFormat<IIdentifiedSpectrum> PeptideFormat { get; set; }

    #region IFileFormat<List<IIdentifiedSpectrum>> Members

    public List<IIdentifiedSpectrum> ReadFromFile(string filename)
    {
      if (!File.Exists(filename))
      {
        throw new FileNotFoundException("File not exist : " + filename);
      }

      var result = new List<IIdentifiedSpectrum>();

      long fileSize = new FileInfo(filename).Length;

      Progress.SetRange(0, fileSize);
      try
      {
        using (var br = new StreamReader(filename))
        {
          String line = br.ReadLine();

          PeptideFormat = new LineFormat<IIdentifiedSpectrum>(IdentifiedSpectrumPropertyConverterFactory.GetInstance(),
                                                              line, "sequest");

          while ((line = br.ReadLine()) != null)
          {
            if (0 == line.Trim().Length)
            {
              break;
            }

            Progress.SetPosition(br.BaseStream.Position);

            result.Add(PeptideFormat.ParseString(line));
          }
        }
      }
      catch (Exception ex)
      {
        throw new Exception(MyConvert.Format("Reading from file {0} error :\n" + ex.Message, filename), ex);
      }

      string enzymeFile = filename + ".enzyme";
      if (File.Exists(enzymeFile))
      {
        new ProteaseFile().Fill(enzymeFile, result);
      }

      return result;
    }

    public void WriteToFile(string filename, List<IIdentifiedSpectrum> spectra)
    {
      using (var sw = new StreamWriter(filename))
      {
        sw.WriteLine(PeptideFormat.GetHeader());
        foreach (IIdentifiedSpectrum pep in spectra)
        {
          sw.WriteLine(PeptideFormat.GetString(pep));
        }
      }

      using (var sw = new StreamWriter(filename + ".summary"))
      {
        WriteSummary(sw, IdentifiedSpectrumUtils.GetSpectrumCount(spectra), IdentifiedSpectrumUtils.GetUniquePeptideCount(spectra));
      }

      string enzymeFile = filename + ".enzyme";
      new ProteaseFile().Write(enzymeFile, spectra);
    }

    public void WriteSummary(StreamWriter sw, int spectrumCount, int uniquePeptideCount)
    {
      sw.WriteLine("Category\tValue");
      sw.WriteLine("Total peptides\t{0}", spectrumCount);
      sw.WriteLine("Unique peptides\t{0}", uniquePeptideCount);
    }

    #endregion
  }
}