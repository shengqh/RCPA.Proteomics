using System;
using System.Collections.Generic;
using System.IO;
using RCPA.Proteomics.Summary;

namespace RCPA.Proteomics.Mascot
{
  public class MascotResultDtaselectFormat : IFileFormat<IIdentifiedResult>
  {
    private IPropertyConverter<IIdentifiedSpectrum> peptideConverter;
    private IPropertyConverter<IIdentifiedProtein> proteinConverter;

    #region IFileFormat<IIdentifiedResult> Members

    public IIdentifiedResult ReadFromFile(string proteinFile)
    {
      if (!File.Exists(proteinFile))
      {
        throw new FileNotFoundException("File not exist : " + proteinFile);
      }

      var result = new MascotResult();

      var peptideMap = new Dictionary<string, IIdentifiedSpectrum>();

      using (var filein = new StreamReader(proteinFile))
      {
        string lastLine;
        while ((lastLine = filein.ReadLine()) != null)
        {
          if (lastLine.StartsWith("Locus"))
          {
            this.proteinConverter = IdentifiedProteinPropertyConverterFactory.GetInstance().GetConverters(lastLine, '\t',
                                                                                                          "Dtaselect");
          }

          if (lastLine.StartsWith("Unique"))
          {
            this.peptideConverter = IdentifiedSpectrumPropertyConverterFactory.GetInstance().GetConverters(lastLine,
                                                                                                           '\t',
                                                                                                           "Dtaselect");
            break;
          }
        }

        IIdentifiedProteinGroup group;
        lastLine = null;
        while ((group = ReadNextProteinGroup(filein, peptideMap, ref lastLine)) != null)
        {
          result.Add(group);
        }
      }

      return result;
    }

    public void WriteToFile(string proteinFile, IIdentifiedResult mr)
    {
      if (this.proteinConverter == null)
      {
        this.proteinConverter =
          IdentifiedProteinPropertyConverterFactory.GetInstance().GetConverters(
            "Locus\tSequence Count\tSpectrum Count\tSequence Coverage\tLength\tMolWt\tpI\tValidation Status\tDescriptive Name",
            '\t', "Dtaselect");
        this.peptideConverter =
          IdentifiedSpectrumPropertyConverterFactory.GetInstance().GetConverters(
            "Unique\tFileName\tScore\tDeltCN\tM+H+\tCalcM+H+\tTotalIntensity\tSpRank\tSpScore\tIonProportion\tRedundancy\tSequence",
            '\t', "Dtaselect");
      }

      using (var sw = new StreamWriter(proteinFile))
      {
        sw.WriteLine("DTASelect v1.9");
        sw.WriteLine();
        sw.WriteLine("true	Use criteria");
        sw.WriteLine();
        sw.WriteLine(this.proteinConverter.Name);
        sw.WriteLine(this.peptideConverter.Name);

        foreach (IdentifiedProteinGroup mpg in mr)
        {
          for (int proteinIndex = 0; proteinIndex < mpg.Count; proteinIndex++)
          {
            IIdentifiedProtein mpro = mpg[proteinIndex];
            sw.WriteLine(this.proteinConverter.GetProperty(mpro));
          }

          mpg.GetSortedPeptides().ForEach(m => sw.WriteLine(this.peptideConverter.GetProperty(m)));
        }
      }
    }

    #endregion

    public bool IsProteinLine(string line)
    {
      if (null == line)
      {
        return false;
      }

      if (line.StartsWith("\t"))
      {
        return false;
      }

      try
      {
        var mp = new IdentifiedProtein();
        this.proteinConverter.SetProperty(mp, line);
      }
      catch (Exception)
      {
        return false;
      }

      return true;
    }

    private IIdentifiedProteinGroup ReadNextProteinGroup(StreamReader filein,
                                                         Dictionary<string, IIdentifiedSpectrum> peptideMap,
                                                         ref string lastLine)
    {
      while (!IsProteinLine(lastLine) && (lastLine = filein.ReadLine()) != null)
      {
      }

      if (lastLine == null)
      {
        return null;
      }

      IIdentifiedProteinGroup result = new IdentifiedProteinGroup();

      while (IsProteinLine(lastLine))
      {
        IIdentifiedProtein protein = new IdentifiedProtein();
        this.proteinConverter.SetProperty(protein, lastLine);
        result.Add(protein);

        lastLine = filein.ReadLine();
      }

      var peptides = new List<IIdentifiedSpectrum>();
      while (!IsProteinLine(lastLine))
      {
        IIdentifiedSpectrum mphit = new IdentifiedSpectrum();
        this.peptideConverter.SetProperty(mphit, lastLine);
        string id = mphit.Query.FileScan.LongFileName + "-" + mphit.Rank;
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

      peptides.Sort();
      result.AddIdentifiedSpectra(peptides);

      return result;
    }
  }
}