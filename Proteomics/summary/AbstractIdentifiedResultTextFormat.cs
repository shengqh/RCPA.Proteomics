using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
using System.Linq;
using RCPA.Proteomics.Summary;
using RCPA.Utils;
using RCPA.Gui;
using RCPA.Proteomics.PropertyConverter;

namespace RCPA.Proteomics.Summary
{
  public abstract class AbstractIdentifiedResultTextFormat : ProgressClass, IIdentifiedResultTextFormat
  {
    /// <summary>
    /// Gets or Sets the format used to read peptide information
    /// </summary>
    public LineFormat<IIdentifiedSpectrum> PeptideFormat { get; set; }

    /// <summary>
    /// Gets or Sets the format used to read protein information
    /// </summary>
    public LineFormat<IIdentifiedProtein> ProteinFormat { get; set; }

    private IIdentifiedProteinGroupWriter groupWriter;
    public IIdentifiedProteinGroupWriter GroupWriter
    {
      get
      {
        if (groupWriter == null)
        {
          groupWriter = new IdentifiedProteinGroupMultipleLineWriter();
        }
        groupWriter.ProteinFormat = this.ProteinFormat;

        return groupWriter;
      }
      set
      {
        this.groupWriter = value;
      }
    }

    public Func<IIdentifiedProteinGroup, bool> ValidGroup { get; set; }

    public Func<IIdentifiedSpectrum, bool> ValidSpectrum { get; set; }

    protected virtual bool IsValidSpectrum(IIdentifiedSpectrum spectrum)
    {
      return ValidSpectrum == null ? true : ValidSpectrum(spectrum);
    }

    public AbstractIdentifiedResultTextFormat()
    {
      this.GroupWriter = new IdentifiedProteinGroupMultipleLineWriter();
      this.ValidGroup = (m => true);
      this.ValidSpectrum = (m => true);
    }

    public AbstractIdentifiedResultTextFormat(string proteinHeaders, string peptideHeaders)
      : this()
    {
      if (string.IsNullOrEmpty(proteinHeaders))
      {
        this.ProteinFormat = new ProteinLineFormat(GetDefaultProteinHeader(), GetEngineName());
      }
      else
      {
        this.ProteinFormat = new ProteinLineFormat(proteinHeaders, GetEngineName());
      }

      if (string.IsNullOrEmpty(peptideHeaders))
      {
        this.PeptideFormat = new PeptideLineFormat(GetDefaultPeptideHeader(), GetEngineName());
      }
      else
      {
        this.PeptideFormat = new PeptideLineFormat(peptideHeaders, GetEngineName());
      }
    }

    protected abstract IIdentifiedResult Allocate();

    protected abstract string GetDefaultProteinHeader();

    protected abstract string GetDefaultPeptideHeader();

    protected abstract string GetEngineName();

    #region IFileFormat<IIdentifiedResult> Members

    public abstract IIdentifiedResult ReadFromFile(string fileName);

    public abstract void WriteToFile(string fileName, IIdentifiedResult identifiedResult);

    public void InitializeByResult(IIdentifiedResult identifiedResult)
    {
      string oldProteinHeader = ProteinFormat == null ? GetDefaultProteinHeader() : ProteinFormat.GetHeader();

      InitializeProteinFormat(identifiedResult, oldProteinHeader);

      string oldPeptideHeader = PeptideFormat == null ? GetDefaultPeptideHeader() : PeptideFormat.GetHeader();

      InitializePeptideFormat(identifiedResult, oldPeptideHeader);
    }

    private void InitializePeptideFormat(IIdentifiedResult identifiedResult, string oldPeptideHeader)
    {
      var spectra = identifiedResult.GetSpectra();
      List<string> pepAnnotations = AnnotationUtils.GetAnnotationKeys(spectra);
      string newPeptideHeader = StringUtils.GetMergedHeader(oldPeptideHeader, pepAnnotations, '\t');
      PeptideFormat = new LineFormat<IIdentifiedSpectrum>(IdentifiedSpectrumPropertyConverterFactory.GetInstance(), newPeptideHeader, GetEngineName(), spectra);
    }

    private void InitializeProteinFormat(IIdentifiedResult identifiedResult, string oldProteinHeader)
    {
      var proteins = identifiedResult.GetProteins();
      List<string> proAnnotations = AnnotationUtils.GetAnnotationKeys(proteins);
      string newProteinHeader = StringUtils.GetMergedHeader(oldProteinHeader, proAnnotations, '\t');
      ProteinFormat = new LineFormat<IIdentifiedProtein>(IdentifiedProteinPropertyConverterFactory.GetInstance(), newProteinHeader, GetEngineName(), proteins);
    }

    #endregion

    protected void CheckFormat(IIdentifiedResult identifiedResult)
    {
      if (ProteinFormat == null)
      {
        InitializeProteinFormat(identifiedResult, GetDefaultProteinHeader());
      }

      if (PeptideFormat == null)
      {
        InitializePeptideFormat(identifiedResult, GetDefaultPeptideHeader());
      }
    }

    protected IEnumerable<IIdentifiedProteinGroup> GetValidGroups(IIdentifiedResult identifiedResult)
    {
      if (ValidGroup == null)
      {
        return identifiedResult;
      }

      return from g in identifiedResult
             where ValidGroup(g)
             select g;
    }

    protected IEnumerable<IIdentifiedSpectrum> GetValidSpectra(IEnumerable<IIdentifiedSpectrum> spectra)
    {
      if (ValidSpectrum == null)
      {
        return spectra;
      }

      return from s in spectra
             where ValidSpectrum(s)
             select s;
    }

    public void WriteSummary(StreamWriter sw, IIdentifiedResult mr)
    {
      sw.WriteLine();
      sw.WriteLine("----- summary -----");

      var groups = GetValidGroups(mr);

      int totalProteinCount = (from g in groups
                               select g.Count).Sum();

      int totalGroupCount = groups.Count();

      sw.WriteLine("Total protein\t: " + totalProteinCount);

      sw.WriteLine("Total protein group\t: " + totalGroupCount);

      if (totalGroupCount > 0)
      {
        sw.WriteLine("UniPepCount\tProteinGroupCount\tPercent\tProteinCount\tPercent");

        var bin = new Dictionary<int, Pair<int, int>>();
        foreach (IIdentifiedProteinGroup group in groups)
        {
          var spectra = GetValidSpectra(group.GetPeptides());

          var unique = group[0].Peptides.GetUniquePeptideCount(m => IsValidSpectrum(m.Spectrum));

          if (!bin.ContainsKey(unique))
          {
            bin[unique] = new Pair<int, int>(0, 0);
          }

          Pair<int, int> counts = bin[unique];
          counts.First = counts.First + 1;
          counts.Second = counts.Second + group.Count;
        }

        var uniques = new List<int>(bin.Keys);
        uniques.Sort();
        foreach (int unique in uniques)
        {
          Pair<int, int> counts = bin[unique];
          sw.WriteLine("{0}\t{1}\t{2:0.00}%\t{3}\t{4:0.00}%", unique, counts.First, (counts.First * 100.0) / totalGroupCount,
                       counts.Second, (counts.Second * 100.0) / totalProteinCount);
        }
      }
    }

    public void WriteFastaFile(string fastaFilename, IIdentifiedResult mr)
    {
      IdentifiedResultUtils.WriteFastaFile(fastaFilename, mr, ValidGroup);
    }

    protected virtual void WritePeptide(StreamWriter sw, IIdentifiedSpectrum mph)
    {
      sw.WriteLine(PeptideFormat.GetString(mph));
    }

    protected virtual void WritePeptideHeader(StreamWriter sw)
    {
      sw.WriteLine(PeptideFormat.GetHeader());
    }
  }
}
