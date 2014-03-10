using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using RCPA.Proteomics;
using RCPA.Proteomics.Sequest;
using RCPA.Proteomics.Summary;
using RCPA.Proteomics.Utils;
using RCPA.Seq;

namespace RCPA.Tools.Isotopic
{
  public class SilacResultSplitter : IFileProcessor
  {
    private IPeptideMassCalculator lightCalc;
    private IPeptideMassCalculator heavyCalc;

    private FastaFormat ff = new FastaFormat();

    public SilacResultSplitter(string lightParamFile, string heavyParamFile)
    {
      SequestParamFile paramFile = new SequestParamFile();
      SequestParam light = paramFile.ReadFromFile(@"F:\sqh\Project\wuyibo\Light.params");
      SequestParam heavy = paramFile.ReadFromFile(@"F:\sqh\Project\wuyibo\Heavy.params");

      this.lightCalc = light.GetPeptideMassCalculator();
      this.heavyCalc = heavy.GetPeptideMassCalculator();
    }

    public IEnumerable<string> Process(string filename)
    {
      List<string> result = new List<string>();

      List<string> proteins = new List<string>();
      List<string> lightPeptides = new List<string>();
      List<string> heavyPeptides = new List<string>();

      Dictionary<string, Sequence> seqMap = new Dictionary<string, Sequence>();
      if (File.Exists(filename + ".fasta"))
      {
        List<Sequence> seqs = SequenceUtils.Read(ff, filename + ".fasta");
        foreach (Sequence seq in seqs)
        {
          seqMap[seq.Name] = seq;
        }
      }

      string lightResult = filename + ".light";
      string heavyResult = filename + ".heavy";

      StreamWriter swLightFasta = null;
      StreamWriter swHeavyFasta = null;
      if (seqMap.Count > 0)
      {
        swLightFasta = new StreamWriter(lightResult + ".fasta");
        swHeavyFasta = new StreamWriter(heavyResult + ".fasta");
      }

      try
      {
        using (StreamWriter swLight = new StreamWriter(lightResult))
        {
          using (StreamWriter swHeavy = new StreamWriter(heavyResult))
          {
            using (StreamReader sr = new StreamReader(filename))
            {
              string line = sr.ReadLine();
              swLight.WriteLine(line);
              swHeavy.WriteLine(line);

              line = sr.ReadLine();
              SequestPeptideTextFormat format = new SequestPeptideTextFormat(line);
              swLight.WriteLine(line);
              swHeavy.WriteLine(line);

              bool bIsProtein = true;
              while ((line = sr.ReadLine()) != null)
              {
                if (line.Trim().Length == 0)
                {
                  WriteGroup(proteins, lightPeptides, heavyPeptides, swLight, swHeavy, swLightFasta, swHeavyFasta, seqMap);
                  break;
                }

                if (line.StartsWith("$"))
                {
                  if (bIsProtein)
                  {
                    proteins.Add(line);
                    continue;
                  }

                  WriteGroup(proteins, lightPeptides, heavyPeptides, swLight, swHeavy, swLightFasta, swHeavyFasta, seqMap);

                  proteins.Clear();
                  lightPeptides.Clear();
                  heavyPeptides.Clear();

                  proteins.Add(line);
                  bIsProtein = true;
                  continue;
                }

                bIsProtein = false;

                IIdentifiedSpectrum sph = format.PeptideFormat.ParseString(line);
                string matchedSeq = PeptideUtils.GetMatchedSequence(sph.Sequence);
                double lightMass = lightCalc.GetMass(matchedSeq);
                double heavyMass = heavyCalc.GetMass(matchedSeq);

                if (Math.Abs(lightMass - sph.ExperimentalMass) < 0.1)
                {
                  lightPeptides.Add(line);
                  continue;
                }

                if (Math.Abs(heavyMass - sph.ExperimentalMass) < 0.1)
                {
                  heavyPeptides.Add(line);
                  continue;
                }

                throw new Exception(MyConvert.Format("Mass={0:0.0000}; {1:0.0000}; {2:0.0000}", sph.ExperimentalMass,
                  lightMass, heavyMass));
              }
            }
          }
        }
      }
      finally
      {
        if (seqMap.Count > 0)
        {
          swLightFasta.Close();
          swHeavyFasta.Close();
        }
      }

      result.Add(lightResult);
      result.Add(heavyResult);

      return result;
    }

    private void WriteGroup(List<string> proteins, List<string> lightPeptides, List<string> heavyPeptides, StreamWriter swLight, StreamWriter swHeavy, StreamWriter swLightFasta, StreamWriter swHeavyFasta, Dictionary<string, Sequence> seqMap)
    {
      if (lightPeptides.Count > 0)
      {
        foreach (string protein in proteins)
        {
          swLight.WriteLine(protein);
          WriteFasta(swLightFasta, seqMap, protein);
        }

        foreach (string peptide in lightPeptides)
        {
          swLight.WriteLine(peptide);
        }
      }

      if (heavyPeptides.Count > 0)
      {
        foreach (string protein in proteins)
        {
          swHeavy.WriteLine(protein);
          WriteFasta(swHeavyFasta, seqMap, protein);
        }

        foreach (string peptide in heavyPeptides)
        {
          swHeavy.WriteLine(peptide);
        }
      }
    }

    private void WriteFasta(StreamWriter swFasta, Dictionary<string, Sequence> seqMap, string protein)
    {
      if (swFasta != null)
      {
        string[] parts = Regex.Split( protein, @"\s+");
        ff.WriteSequence(swFasta, seqMap[parts[1].Trim()]);
      }
    }
  }
}