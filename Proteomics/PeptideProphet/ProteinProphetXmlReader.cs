using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RCPA.Proteomics.Summary;
using RCPA.Gui;
using System.Xml.Linq;

namespace RCPA.Proteomics.PeptideProphet
{
  public class ProteinProphetXmlReader : ProgressClass, IFileReader<IIdentifiedResult>
  {
    #region IFileReader<IIdentifiedResult> Members

    public IIdentifiedResult ReadFromFile(string fileName)
    {
      PeptideProphetModifications ppmods = new PeptideProphetModifications();

      XElement root = XElement.Load(fileName);

      var header = root.FindFirstChild("protein_summary_header");
      var program = header.FindFirstChild("program_details");
      var details = program.FindFirstChild("proteinprophet_details");

      var filters = ParseFilters(details);

      IdentifiedResult result = new IdentifiedResult();
      result.SetProteinSummaryDataFilterList(filters);

      var groupEles = root.FindChildren("protein_group");
      foreach (var groupEle in groupEles)
      {
        IdentifiedProteinGroup group = new IdentifiedProteinGroup();
        result.Add(group);

        group.Index = Convert.ToInt32(groupEle.Attribute("group_number").Value);
        group.Probability = MyConvert.ToDouble(groupEle.Attribute("probability").Value);

        var proteinEles = groupEle.FindChildren("protein");
        foreach (var proteinEle in proteinEles)
        {
          IdentifiedProtein protein = new IdentifiedProtein();
          group.Add(protein);

          protein.Name = proteinEle.Attribute("protein_name").Value;
          protein.Score = MyConvert.ToDouble(proteinEle.Attribute("probability").Value);
          var cov = proteinEle.Attribute("percent_coverage");
          if (cov != null)
          {
            protein.Coverage = MyConvert.ToDouble(cov.Value);
          }
          protein.PeptideCount = Convert.ToInt32(proteinEle.Attribute("total_number_peptides").Value);

          var annEle = proteinEle.FindFirstChild("annotation");
          if (annEle != null)
          {
            protein.Description = annEle.Attribute("protein_description").Value;
          }

          var peptideEles = proteinEle.FindChildren("peptide");
          foreach (var peptideEle in peptideEles)
          {
            var spectrum = new IdentifiedSpectrum();
            var peptide = new IdentifiedPeptide(spectrum);
            protein.Peptides.Add(peptide);

            peptide.Sequence = peptideEle.Attribute("peptide_sequence").Value;
            spectrum.Query.FileScan.Charge = Convert.ToInt32(peptideEle.Attribute("charge").Value);
            spectrum.Query.FileScan.Experimental = string.Empty;
            spectrum.PValue = MyConvert.ToDouble(peptideEle.Attribute("initial_probability").Value);
            spectrum.NumProteaseTermini = Convert.ToInt32(peptideEle.Attribute("n_enzymatic_termini").Value);
            spectrum.TheoreticalMass = MyConvert.ToDouble(peptideEle.Attribute("calc_neutral_pep_mass").Value);

            var modEle = peptideEle.FindFirstChild("modification_info");
            if (modEle != null)
            {
              var modaas = PeptideProphetUtils.ParseModificationAminoacidMass(modEle);
              if (modaas != null && modaas.Count > 0)
              {
                var seq = peptide.Sequence;
                modaas.Reverse();
                foreach (var modaa in modaas)
                {
                  if (!ppmods.HasModification(modaa.Mass))
                  {
                    ppmods.Add(new PeptideProphetModificationItem()
                    {
                      Mass = modaa.Mass,
                      IsAminoacid = true,
                      IsVariable = true
                    });
                    ppmods.AssignModificationChar();
                  }
                  var modchar = ppmods.FindModificationChar(modaa.Mass);
                  seq = seq.Insert(modaa.Position, modchar);
                }
                peptide.Sequence = seq;
              }
            }

            protein.InitUniquePeptideCount();
          }
        }
      }

      return result;
    }

    public ProteinSummaryDataFilterList ParseFilters(XElement details)
    {
      ProteinSummaryDataFilterList result = new ProteinSummaryDataFilterList();

      var filters = details.FindChildren("protein_summary_data_filter");
      foreach (var filter in filters)
      {
        ProteinSummaryDataFilterItem item = new ProteinSummaryDataFilterItem();

        item.MinProbability = MyConvert.ToDouble(filter.Attribute("min_probability").Value);
        item.Sensitivity = MyConvert.ToDouble(filter.Attribute("sensitivity").Value);
        item.FalsePositiveErrorRate = MyConvert.ToDouble(filter.Attribute("false_positive_error_rate").Value);
        item.PredictedNumCorrect = MyConvert.ToDouble(filter.Attribute("predicted_num_correct").Value);
        item.PredictedNumIncorrect = MyConvert.ToDouble(filter.Attribute("predicted_num_incorrect").Value);

        result.Add(item);
      }

      return result;
    }

    #endregion
  }
}
