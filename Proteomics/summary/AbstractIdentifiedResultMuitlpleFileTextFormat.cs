using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
using System.Linq;
using RCPA.Proteomics.Summary;
using RCPA.Utils;
using RCPA.Proteomics.Sequest;

namespace RCPA.Proteomics.Summary
{
  public abstract class AbstractIdentifiedResultMuitlpleFileTextFormat : AbstractIdentifiedResultTextFormat
  {
    public AbstractIdentifiedResultMuitlpleFileTextFormat()
      : base()
    {
    }

    public AbstractIdentifiedResultMuitlpleFileTextFormat(string proteinHeaders, string peptideHeaders)
      : base(proteinHeaders, peptideHeaders)
    { }

    #region IFileFormat<IIdentifiedResult> Members

    public override IIdentifiedResult ReadFromFile(string fileName)
    {
      if (!File.Exists(fileName))
      {
        throw new FileNotFoundException("Protein file not exist : " + fileName);
      }

      string peptideFilename = GetPeptideFileName(fileName);
      if (!File.Exists(peptideFilename))
      {
        throw new FileNotFoundException("Peptide file not exist : " + peptideFilename);
      }

      string linkFileName = GetLinkFileName(fileName);
      if (!File.Exists(linkFileName))
      {
        throw new FileNotFoundException("Peptide2group file not exist : " + linkFileName);
      }

      var pepFileReader = new PeptideTextReader(GetEngineName());
      List<IIdentifiedSpectrum> spectra = pepFileReader.ReadFromFile(peptideFilename);

      this.PeptideFormat = pepFileReader.PeptideFormat;

      var proFileReader = new ProteinTextReader(GetEngineName());
      List<IIdentifiedProtein> proteins = proFileReader.ReadFromFile(fileName);
      this.ProteinFormat = proFileReader.ProteinFormat;

      var peptideMap = spectra.ToDictionary(m => m.Id);
      var proteinMap = proteins.GroupBy(m => m.GroupIndex);

      IIdentifiedResult result = Allocate();
      foreach (var pros in proteinMap)
      {
        var group = new IdentifiedProteinGroup();
        pros.ToList().ForEach(m => group.Add(m));
        result.Add(group);
      }

      new Peptide2GroupTextReader().LinkPeptideToGroup(linkFileName, peptideMap, result.ToDictionary(m => m.Index));

      string fastaFile = fileName + ".fasta";
      if (File.Exists(fastaFile))
      {
        IdentifiedResultUtils.FillSequenceFromFasta(fastaFile, result, null);
      }

      return result;
    }

    protected virtual string GetLinkFileName(string fileName)
    {
      return FileUtils.ChangeExtension(fileName, ".peptide2group");
    }

    protected virtual string GetPeptideFileName(string fileName)
    {
      return FileUtils.ChangeExtension(fileName, ".peptides");
    }

    public override void WriteToFile(string fileName, IIdentifiedResult identifiedResult)
    {
      CheckFormat(identifiedResult);

      List<IIdentifiedSpectrum> allSpectra = identifiedResult.GetSpectra();
      for (int i = 0; i < allSpectra.Count; i++)
      {
        allSpectra[i].Id = i.ToString();
      }

      string linkFilename = GetLinkFileName(fileName);

      HashSet<IIdentifiedSpectrum> spectra = new HashSet<IIdentifiedSpectrum>();
      using (var linkWriter = new StreamWriter(linkFilename))
      {
        linkWriter.WriteLine("PeptideId\tGroupId");
        using (var sw = new StreamWriter(fileName))
        {
          sw.WriteLine(ProteinFormat.GetHeader());

          var groups = GetValidGroups(identifiedResult);

          foreach (IIdentifiedProteinGroup mpg in groups)
          {
            GroupWriter.WriteToStream(sw, mpg);

            var validSpectra = GetValidSpectra(mpg.GetSortedPeptides());

            foreach (var spectrum in validSpectra)
            {
              linkWriter.WriteLine(spectrum.Id + "\t" + mpg.Index);
            }

            spectra.UnionWith(validSpectra);
          }
        }
      }

      var finalSpectra = from s in spectra
                         orderby s.Id
                         select s;

      string peptideFile = GetPeptideFileName(fileName);

      if (!PeptideFormat.GetHeader().Contains("Id\t"))
      {
        PeptideFormat = new PeptideLineFormat("Id\t" + PeptideFormat.GetHeader());
      }

      using (var sw = new StreamWriter(peptideFile))
      {
        WritePeptideHeader(sw);

        foreach (IIdentifiedSpectrum mph in finalSpectra)
        {
          WritePeptide(sw, mph);
        }
      }

      if (identifiedResult.Count > 0 && identifiedResult[0][0].Sequence != null)
      {
        string fastaFilename = fileName + ".fasta";
        WriteFastaFile(fastaFilename, identifiedResult);
      }
    }

    #endregion
  }
}
