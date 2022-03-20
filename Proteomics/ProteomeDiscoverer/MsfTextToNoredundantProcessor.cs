using RCPA.Proteomics.Summary;
using RCPA.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace RCPA.Proteomics.ProteomeDiscoverer
{
  public class MsfTextToNoredundantProcessor : AbstractMsfToNoredundantProcessor
  {
    public override List<IIdentifiedProtein> ParseProteins(string fileName)
    {
      Dictionary<string, IIdentifiedProtein> proteinMap = new Dictionary<string, IIdentifiedProtein>();

      using (StreamReader sr = new StreamReader(fileName))
      {
        string line = sr.ReadLine();
        string[] headerParts = line.Split('\t');

        int seqIndex = Array.FindIndex(headerParts, (m => m == "Sequence"));
        int proIndex = Array.FindIndex(headerParts, (m => m == "Protein Accessions"));
        int modIndex = Array.FindIndex(headerParts, (m => m == "Modifications"));
        int xcIndex = Array.FindIndex(headerParts, (m => m == "XCorr"));
        int deltaIndex = Array.FindIndex(headerParts, (m => m.EndsWith(" Score")));
        int chargeIndex = Array.FindIndex(headerParts, (m => m == "Charge"));
        int obsIndex = Array.FindIndex(headerParts, (m => m == "m/z [Da]"));
        int mhIndex = Array.FindIndex(headerParts, (m => m == "MH+ [Da]"));
        int fscanIndex = Array.FindIndex(headerParts, (m => m == "First Scan"));
        int lscanIndex = Array.FindIndex(headerParts, (m => m == "Last Scan"));
        int ionIndex = Array.FindIndex(headerParts, (m => m == "Ions Matched"));
        int fileIndex = Array.FindIndex(headerParts, (m => m == "Spectrum File"));

        Progress.SetRange(0, sr.BaseStream.Length);
        Progress.SetMessage("Parsing file ...");
        while ((line = sr.ReadLine()) != null)
        {
          if (line.Trim().Length == 0)
          {
            break;
          }
          string[] parts = line.Split('\t');
          if (parts[0].Length == 0)
          {
            continue;
          }

          Progress.SetPosition(sr.BaseStream.Position);

          string seq = parts[seqIndex];

          string deltaCn = parts[deltaIndex];
          if (deltaCn.Length == 0)//rank > 1
          {
            continue;
          }

          string protein = parts[proIndex];
          if (!proteinMap.ContainsKey(protein))
          {
            sr.ReadLine();
            string proLine = sr.ReadLine();
            string[] proParts = proLine.Split('\t');

            var p = new IdentifiedProtein(protein);

            p.Coverage = MyConvert.ToDouble(proParts[2]);
            p.MolecularWeight = MyConvert.ToDouble(proParts[5]) * 1000;
            p.IsoelectricPoint = MyConvert.ToDouble(proParts[6]);
            p.Score = MyConvert.ToDouble(proParts[7]);
            p.Description = proParts[8];

            proteinMap[protein] = p;
          }

          var pro = proteinMap[protein];

          IdentifiedSpectrum spectrum = new IdentifiedSpectrum();
          IdentifiedPeptide peptide = new IdentifiedPeptide(spectrum);
          peptide.Sequence = seq.ToUpper();
          peptide.AddProtein(protein);
          spectrum.Modifications = parts[modIndex];
          spectrum.DeltaScore = MyConvert.ToDouble(deltaCn);
          spectrum.Charge = Convert.ToInt32(parts[chargeIndex]);
          spectrum.ObservedMz = MyConvert.ToDouble(parts[obsIndex]);
          spectrum.TheoreticalMH = MyConvert.ToDouble(parts[mhIndex]);
          spectrum.Ions = parts[ionIndex];
          spectrum.Query.FileScan.FirstScan = Convert.ToInt32(parts[fscanIndex]);
          spectrum.Query.FileScan.LastScan = Convert.ToInt32(parts[lscanIndex]);
          spectrum.Query.FileScan.Experimental = FileUtils.RemoveAllExtension(parts[fileIndex]);

          pro.Peptides.Add(peptide);
        }
      }

      var proteins = proteinMap.Values.ToList();
      return proteins;
    }
  }
}
