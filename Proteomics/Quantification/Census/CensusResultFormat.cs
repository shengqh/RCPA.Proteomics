using RCPA.Converter;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace RCPA.Proteomics.Quantification.Census
{
  public class CensusResultFormat : IFileFormat<CensusResult>
  {
    private readonly bool readSingleton;
    private readonly bool labelFree;

    private LineFormat<CensusPeptideItem> peptideFormat;

    public LineFormat<CensusPeptideItem> PeptideFormat
    {
      get
      {
        if (peptideFormat == null)
        {
          InitDefaultFormat();
        }
        return peptideFormat;
      }
    }

    private LineFormat<CensusProteinItem> proteinFormat;

    public CensusResultFormat(bool readSingleton, bool labelFree)
    {
      this.readSingleton = readSingleton;
      this.labelFree = labelFree;
    }

    public CensusResultFormat(bool readSingleton) : this(readSingleton, false) { }

    #region IFileFormat<CensusResult> Members

    public CensusResult ReadFromFile(string fileName)
    {
      var result = new CensusResult();
      result.Headers = CensusUtils.ReadHeaders(fileName);

      InitFormat(result.Headers);

      result.Proteins = ReadProteins(fileName);

      return result;
    }

    public void WriteToFile(string fileName, CensusResult t)
    {
      if (null == this.proteinFormat)
      {
        InitFormat(t.Headers);
        if (null == this.proteinFormat)
        {
          InitDefaultFormat();
        }
      }

      using (var sw = new StreamWriter(fileName))
      {
        foreach (string header in t.Headers)
        {
          sw.Write(header + "\n");
        }

        foreach (CensusProteinItem cpi in t.Proteins)
        {
          sw.Write(this.proteinFormat.GetString(cpi) + "\n");
          foreach (CensusPeptideItem peptide in cpi.Peptides)
          {
            sw.Write(this.peptideFormat.GetString(peptide) + "\n");
          }
        }
      }
    }

    private void InitDefaultFormat()
    {
      this.proteinFormat = new LineFormat<CensusProteinItem>(
        CensusProteinItemPropertyConverterFactory.GetInstance(),
        "PLINE	LOCUS	AVERAGE_RATIO	STANDARD_DEVIATION	WEIGHTED_AVERAGE	PEPTIDE_NUM	SPEC_COUNT	DESCRIPTION");
      this.peptideFormat = new LineFormat<CensusPeptideItem>(
        CensusPeptideItemPropertyConverterFactory.GetInstance(),
        "SLINE	UNIQUE	SEQUENCE	RATIO	REGRESSION_FACTOR	DETERMINANT_FACTOR	XCorr	deltaCN	SAM_INT	REF_INT	AREA_RATIO	SINGLETON_SCORE	FILE_NAME");
    }

    #endregion

    private List<CensusProteinItem> ReadProteins(string filename)
    {
      var result = new List<CensusProteinItem>();
      var peptideMap = new Dictionary<string, CensusPeptideItem>();

      var lastItem = new CensusProteinItem();
      using (var sr = new StreamReader(filename))
      {
        string lastLine = sr.ReadLine();
        while (lastLine != null)
        {
          lastLine = lastLine.Trim();

          if (lastLine.StartsWith("S") || (lastLine.StartsWith("&S") && this.readSingleton))
          {
            CensusPeptideItem pepItem = this.peptideFormat.ParseString(lastLine);
            if (pepItem.Ratio != 0.0)
            {
              string longFilename = pepItem.Filename.LongFileName;
              if (peptideMap.ContainsKey(longFilename))
              {
                lastItem.Peptides.Add(peptideMap[longFilename]);
              }
              else
              {
                peptideMap[longFilename] = pepItem;
                lastItem.Peptides.Add(pepItem);
              }
            }
          }
          else if (lastLine.StartsWith("P"))
          {
            lastItem = this.proteinFormat.ParseString(lastLine);
            result.Add(lastItem);
          }

          lastLine = sr.ReadLine();
        }
      }

      result.ForEach(m => m.ValidatePeptides());

      result.RemoveAll(m => m.Peptides.Count == 0);

      return result;
    }

    public List<CensusPeptideItem> ReadPeptides(string filename)
    {
      var peptideMap = new Dictionary<string, CensusPeptideItem>();

      List<string> headers = CensusUtils.ReadHeaders(filename);

      InitFormat(headers);

      using (var sr = new StreamReader(filename))
      {
        string lastLine;
        while ((lastLine = sr.ReadLine()) != null)
        {
          lastLine = lastLine.Trim();

          if (lastLine.StartsWith("S") || lastLine.StartsWith("&S"))
          {
            CensusPeptideItem pepItem = this.peptideFormat.ParseString(lastLine);
            if (pepItem.Ratio != 0.0)
            {
              string longFilename = pepItem.Filename.LongFileName;
              if (peptideMap.ContainsKey(longFilename))
              {
                continue;
              }
              else
              {
                peptideMap[longFilename] = pepItem;
              }
            }
          }
        }
      }

      return new List<CensusPeptideItem>(peptideMap.Values);
    }

    private void InitFormat(List<string> headers)
    {
      if (labelFree)
      {
        InitLabelFreeFormat(headers);
      }
      else
      {
        InitLabelFormat(headers);
      }
    }

    private void InitLabelFormat(List<string> headers)
    {
      string sline = string.Empty;

      foreach (string header in headers)
      {
        if (header.StartsWith("H	PLINE"))
        {
          this.proteinFormat = new LineFormat<CensusProteinItem>(CensusProteinItemPropertyConverterFactory.GetInstance(), header.Substring(2));
        }
        else if (header.StartsWith("H	SLINE"))
        {
          sline = header.Substring(2).Trim();
          this.peptideFormat = new LineFormat<CensusPeptideItem>(CensusPeptideItemPropertyConverterFactory.GetInstance(), sline);
        }
        else if (header.StartsWith("H	&SLINE"))
        {
          string line = header.Substring(3).Trim();
          if (line.Equals(sline))
          {
            continue;
          }

          if (line.Length > sline.Length)
          {
            this.peptideFormat = new LineFormat<CensusPeptideItem>(CensusPeptideItemPropertyConverterFactory.GetInstance(), line);
          }
        }
      }
    }

    private void InitLabelFreeFormat(List<string> headers)
    {
      Regex proteinReg = new Regex(@"AVG\((\S+)_INT/(\S+)_INT\)");
      string sampleName = "";
      string referenceName = "";

      foreach (string header in headers)
      {
        if (header.StartsWith("H	PLINE"))
        {
          Match m = proteinReg.Match(header);
          sampleName = m.Groups[1].Value;
          referenceName = m.Groups[2].Value;

          CensusProteinItemPropertyConverterFactory factory = CensusProteinItemPropertyConverterFactory.GetInstance();

          IPropertyConverter<CensusProteinItem> averageRatioConverter = new CensusProteinItem_AVERAGE_RATIO_Converter();
          factory.RegisterConverter(new PropertyAliasConverter<CensusProteinItem>(averageRatioConverter, MyConvert.Format("AVG({0}_INT/{1}_INT)", sampleName, referenceName)));

          IPropertyConverter<CensusProteinItem> sdConverter = new CensusProteinItem_STANDARD_DEVIATION_Converter();
          factory.RegisterConverter(new PropertyAliasConverter<CensusProteinItem>(sdConverter, MyConvert.Format("STDEV({0}_INT/{1}_INT)", sampleName, referenceName)));

          this.proteinFormat = new LineFormat<CensusProteinItem>(factory, header.Substring(2));
        }
        else if (header.StartsWith("H	SLINE"))
        {
          string peptideHeader = header.Substring(2);
          List<string> parts = new List<string>(peptideHeader.Split(new char[] { '\t' }));
          parts.Remove(MyConvert.Format("SPEC_COUNT({0})", sampleName));
          parts.Remove(MyConvert.Format("SPEC_COUNT({0})", referenceName));
          peptideHeader = StringUtils.Merge(parts, "\t");

          CensusPeptideItemPropertyConverterFactory factory = CensusPeptideItemPropertyConverterFactory.GetInstance();

          IPropertyConverter<CensusPeptideItem> ratioConverter = new CensusPeptideItem_RATIO_Converter();
          factory.RegisterConverter(new PropertyAliasConverter<CensusPeptideItem>(ratioConverter, MyConvert.Format("{0}_INT/{1}_INT", sampleName, referenceName)));

          IPropertyConverter<CensusPeptideItem> samIntConverter = new CensusPeptideItem_SAM_INT_Converter();
          factory.RegisterConverter(new PropertyAliasConverter<CensusPeptideItem>(samIntConverter, MyConvert.Format("INT({0})", sampleName)));

          IPropertyConverter<CensusPeptideItem> refIntConverter = new CensusPeptideItem_REF_INT_Converter();
          factory.RegisterConverter(new PropertyAliasConverter<CensusPeptideItem>(refIntConverter, MyConvert.Format("INT({0})", referenceName)));

          this.peptideFormat = new LineFormat<CensusPeptideItem>(factory, peptideHeader);
        }
      }
    }

  }
}