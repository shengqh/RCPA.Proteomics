using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace RCPA.Proteomics.Summary
{
  public abstract class AbstractIdentifiedResultSingleFileTextFormat : AbstractIdentifiedResultTextFormat
  {
    public AbstractIdentifiedResultSingleFileTextFormat()
      : base()
    { }

    public AbstractIdentifiedResultSingleFileTextFormat(string proteinHeaders, string peptideHeaders)
      : base(proteinHeaders, peptideHeaders)
    { }

    private IIdentifiedProteinGroup ReadNextProteinGroup(StreamReader filein, Dictionary<string, IIdentifiedSpectrum> peptideMap, ref string lastLine)
    {
      Progress.SetPosition(filein.BaseStream.Position);

      while (!IdentifiedResultUtils.IsProteinLine(lastLine) && (lastLine = filein.ReadLine()) != null)
      { }

      if (lastLine == null)
      {
        return null;
      }

      IIdentifiedProteinGroup result = new IdentifiedProteinGroup();

      while (IdentifiedResultUtils.IsProteinLine(lastLine))
      {
        IIdentifiedProtein protein = ProteinFormat.ParseString(lastLine);
        result.Add(protein);

        protein.GroupIndex = IdentifiedResultUtils.GetGroupIndex(lastLine);

        lastLine = filein.ReadLine();
      }

      List<IIdentifiedSpectrum> peptides = new List<IIdentifiedSpectrum>();
      while (!IdentifiedResultUtils.IsProteinLine(lastLine))
      {
        if (Progress.IsCancellationPending())
        {
          throw new UserTerminatedException();
        }

        IIdentifiedSpectrum mphit = PeptideFormat.ParseString(lastLine);

        string id = string.Format("{0}-{1}-{2}-{3}", mphit.Query.FileScan.LongFileName, mphit.Rank, mphit.Engine, mphit.Tag);

        if (!peptideMap.ContainsKey(id))
        {
          peptideMap[id] = mphit;
        }
        else
        {
          mphit = peptideMap[id];
        }

        peptides.Add(mphit);

        lastLine = filein.ReadLine();

        if (lastLine == null || lastLine.Trim().Length == 0)
        {
          break;
        }
      }

      foreach (IIdentifiedSpectrum hit in peptides)
      {
        result.AddIdentifiedSpectrum(hit);
      }

      return result;
    }

    #region IFileFormat<IIdentifiedResult> Members

    public override IIdentifiedResult ReadFromFile(string fileName)
    {
      if (!File.Exists(fileName))
      {
        throw new FileNotFoundException("File not exist : " + fileName);
      }

      IIdentifiedResult result = Allocate();

      Dictionary<string, IIdentifiedSpectrum> peptideMap = new Dictionary<string, IIdentifiedSpectrum>();

      using (StreamReader filein = new StreamReader(new FileStream(fileName, FileMode.Open, FileAccess.Read)))
      {
        Progress.SetRange(0, filein.BaseStream.Length);

        IIdentifiedProteinGroup group;

        string lastLine = filein.ReadLine();
        if (lastLine == null)
        {
          return result;
        }

        if (ProteinFormat != null)
        {
          ProteinFormat = new LineFormat<IIdentifiedProtein>(ProteinFormat.Factory, lastLine, GetEngineName());
        }
        else
        {
          ProteinFormat = new ProteinLineFormat(lastLine, GetEngineName());
        }

        lastLine = filein.ReadLine();
        if (lastLine == null)
        {
          return result;
        }

        if (PeptideFormat != null)
        {
          PeptideFormat = new LineFormat<IIdentifiedSpectrum>(PeptideFormat.Factory, lastLine, GetEngineName());
        }
        else
        {
          PeptideFormat = new PeptideLineFormat(lastLine, GetEngineName());
        }

        lastLine = null;
        while ((group = ReadNextProteinGroup(filein, peptideMap, ref lastLine)) != null)
        {
          result.Add(group);
        }
      }

      string fastaFile = fileName + ".fasta";
      if (File.Exists(fastaFile))
      {
        IdentifiedResultUtils.FillSequenceFromFasta(fastaFile, result, null);
      }

      return result;
    }

    public override void WriteToFile(string fileName, IIdentifiedResult identifiedResult)
    {
      CheckFormat(identifiedResult);

      var groups = GetValidGroups(identifiedResult);

      int totalSpectraCount = (from g in groups
                               select g[0].Peptides.Count).Sum();

      Progress.SetRange(0, totalSpectraCount);

      using (StreamWriter sw = new StreamWriter(fileName))
      {
        sw.WriteLine(ProteinFormat.Headers);

        WritePeptideHeader(sw);

        foreach (IIdentifiedProteinGroup mpg in groups)
        {
          GroupWriter.WriteToStream(sw, mpg);

          var spectra = GetValidSpectra(mpg.GetSortedPeptides());

          foreach (var p in spectra)
          {
            WritePeptide(sw, p);
          }

          Progress.Increment(mpg[0].Peptides.Count);
        }
      }

      using (StreamWriter sw = new StreamWriter(fileName + ".summary"))
      {
        WriteSummary(sw, identifiedResult);
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
