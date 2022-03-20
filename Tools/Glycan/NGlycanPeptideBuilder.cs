using RCPA.Proteomics;
using RCPA.Seq;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace RCPA.Tools.Glycan
{
  public class NGlycanPeptideBuilder : AbstractThreadFileProcessor
  {
    private IAccessNumberParser parser;

    public NGlycanPeptideBuilder(IAccessNumberParser acParser)
    {
      this.parser = acParser;
    }

    class NGlycanValue
    {
      private string nGlycanSites;

      public string NGlycanSites
      {
        get { return nGlycanSites; }
        set { nGlycanSites = value; }
      }

      private List<string> proteins;

      public List<string> Proteins
      {
        get
        {
          if (proteins == null)
          {
            proteins = new List<string>();
          }
          return proteins;
        }
      }
    }

    public override IEnumerable<string> Process(string filename)
    {
      FastaFormat ff = new FastaFormat();
      Digest digest = new Digest();
      digest.DigestProtease = ProteaseManager.FindOrCreateProtease("Trypsin", true, "RK", "P");
      digest.MaxMissedCleavages = 1;

      NGlycanFilter filter = new NGlycanFilter();
      digest.Filter = filter;

      string resultFile = filename + ".nglycan";
      Dictionary<string, NGlycanValue> peptideProteinMap = new Dictionary<string, NGlycanValue>();
      using (StreamReader sr = new StreamReader(filename))
      {
        Sequence seq;
        while ((seq = ff.ReadSequence(sr)) != null)
        {
          digest.ProteinSequence = seq;
          digest.AddDigestFeatures();

          if (seq.Annotation.ContainsKey(Digest.PEPTIDE_FEATURE_TYPE))
          {
            bool[] isGlycans = filter.IsNglycan;

            List<DigestPeptideInfo> nglycanPeptides = (List<DigestPeptideInfo>)seq.Annotation[Digest.PEPTIDE_FEATURE_TYPE];
            foreach (DigestPeptideInfo dpi in nglycanPeptides)
            {
              if (!peptideProteinMap.ContainsKey(dpi.PeptideSeq))
              {
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < dpi.PeptideSeq.Length; i++)
                {
                  if (isGlycans[dpi.PeptideLoc.Min - 1 + i])
                  {
                    sb.Append(1);
                  }
                  else
                  {
                    sb.Append(0);
                  }
                }

                NGlycanValue value = new NGlycanValue();
                value.NGlycanSites = sb.ToString();

                peptideProteinMap[dpi.PeptideSeq] = value;
              }

              peptideProteinMap[dpi.PeptideSeq].Proteins.Add(parser.GetValue(dpi.ProteinName));
            }
          }
        }
      }

      List<string> peptides = new List<string>(peptideProteinMap.Keys);
      peptides.Sort();

      using (StreamWriter sw = new StreamWriter(resultFile))
      {
        foreach (string pep in peptides)
        {
          NGlycanValue value = peptideProteinMap[pep];
          sw.Write(pep + "\t" + value.NGlycanSites + "\t");
          bool bFirst = true;
          foreach (string protein in value.Proteins)
          {
            if (bFirst)
            {
              bFirst = false;
              sw.Write(protein);
            }
            else
            {
              sw.Write(" ! " + protein);
            }
          }
          sw.WriteLine();
        }
      }

      return new[] { resultFile };
    }
  }

}
