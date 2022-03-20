﻿using System.Collections.Generic;

namespace RCPA.Proteomics.PeptideProphet
{
  public class PepXmlWriterParameters
  {
    public PepXmlWriterParameters()
    {
      RawDataType = "raw";
      Protease = ProteaseManager.GetProteaseByName("Trypsin");
      SearchEngine = "unknown";
      MassTypeParentAnnotation = "monoisotopic";
      MassTypeFragmentAnnotation = "monoisotopic";
    }

    public string RawDataType { get; set; }

    public Protease Protease { get; set; }

    private string _sourceFile;
    public string SourceFile
    {
      get
      {
        return _sourceFile;
      }
      set
      {
        _sourceFile = value.Replace("\\", "/");
      }
    }

    private string _searchDatabase;
    public string SearchDatabase
    {
      get
      {
        return _searchDatabase;
      }
      set
      {
        _searchDatabase = value.Replace("\\", "/");
      }
    }

    public string SearchEngine { get; set; }

    public string MassTypeParentAnnotation { get; set; }

    public string MassTypeFragmentAnnotation { get; set; }

    public PepXmlModifications Modifications { get; set; }

    public Dictionary<string, string> Parameters { get; set; }

    public bool NotCombineRank1PSMs { get; set; }
  }
}
