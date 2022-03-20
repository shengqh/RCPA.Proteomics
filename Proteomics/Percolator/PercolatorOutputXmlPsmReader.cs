﻿using RCPA.Proteomics.Summary;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;

namespace RCPA.Proteomics.Percolator
{
  public class PercolatorOutputXmlPsmReader : IFileReader<List<IIdentifiedSpectrum>>
  {
    public List<IIdentifiedSpectrum> ReadFromFile(string fileName)
    {
      var result = new List<IIdentifiedSpectrum>();
      XElement root = XElement.Load(fileName);
      var psms = root.FindElement("psms").FindElements("psm");
      foreach (var psm in psms)
      {
        IIdentifiedSpectrum spec = new IdentifiedSpectrum();
        spec.Id = psm.FindAttribute("psm_id").Value.StringAfter("decoy_");
        spec.FromDecoy = psm.FindAttribute("decoy").Value.Equals("true");
        spec.SpScore = double.Parse(psm.FindElement("svm_score").Value);
        spec.QValue = double.Parse(psm.FindElement("q_value").Value);
        spec.Score = double.Parse(psm.FindElement("pep").Value);
        spec.Probability = double.Parse(psm.FindElement("p_value").Value);
        spec.TheoreticalMH = double.Parse(psm.FindElement("calc_mass").Value);
        spec.Query.FileScan.Experimental = Path.GetFileName(fileName).StringBefore(".");
        var pep = new IdentifiedPeptide(spec);
        var pepseq = psm.FindElement("peptide_seq");
        pep.Sequence = pepseq.FindAttribute("seq").Value;
        pep.AddProtein(psm.FindElement("protein_id").Value);
        result.Add(spec);
      }

      return result;
    }
  }
}
