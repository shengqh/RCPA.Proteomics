using RCPA.Proteomics.Summary;
using RCPA.Proteomics.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace RCPA.Proteomics.Mascot
{
  public class MascotPeptideHitTextWriter
  {
    private readonly List<string> _annotationKeys;

    private readonly IEnumerable<IIdentifiedSpectrum> _peptides;

    public MascotPeptideHitTextWriter(IEnumerable<IIdentifiedSpectrum> peptides)
    {
      this._peptides = peptides;
      this._annotationKeys = AnnotationUtils.GetAnnotationKeys(this._peptides);
    }

    public String GetHeader()
    {
      var sb = new StringBuilder();
      sb.Append(
        "\t\"File, Scan(s)\"\tSequence\tMH+\tDiff(MH+)\tCharge\tRank\tScore\tDeltaScore\tExpectValue\tQuery\tIons\tReference\tDIFF_MODIFIED_CANDIDATE\tPI\tMissCleavage\tModification");
      foreach (string key in this._annotationKeys)
      {
        sb.Append("\t" + key);
      }
      return sb.ToString();
    }

    public static string GetSequenceString(IIdentifiedSpectrum mphit)
    {
      var sb = new StringBuilder();
      bool bfirst = true;
      foreach (IIdentifiedPeptide pep in mphit.Peptides)
      {
        if (bfirst)
        {
          sb.Append(pep.Sequence);
          bfirst = false;
        }
        else
        {
          sb.Append(" ! ");
          sb.Append(pep.Sequence);
        }
      }

      return sb.ToString();
    }

    public static string GetProteinString(IIdentifiedSpectrum mphit)
    {
      var sb = new StringBuilder();
      bool bfirst = true;
      foreach (IIdentifiedPeptide pep in mphit.Peptides)
      {
        if (bfirst)
        {
          sb.Append(StringUtils.Merge(pep.Proteins, "/"));
          bfirst = false;
        }
        else
        {
          sb.Append(" ! ");
          sb.Append(StringUtils.Merge(pep.Proteins, "/"));
        }
      }

      return sb.ToString();
    }

    public string GetString(IIdentifiedSpectrum mphit)
    {
      var sb = new StringBuilder();

      sb.Append(
        MyConvert.Format(
          "\t{0}\t{1}\t{2:0.0000}\t{3:0.0000}\t{4}\t{5}\t{6:0.00}\t{7:0.00}\t{8:E2}\t{9}\t{10}\t{11}\t{12}\t{13}\t{14}\t{15}",
          mphit.Query.FileScan.ShortFileName,
          GetSequenceString(mphit),
          mphit.TheoreticalMass + Atom.H.MonoMass,
          mphit.TheoreticalMinusExperimentalMass,
          mphit.Query.Charge,
          mphit.Rank,
          mphit.Score,
          mphit.DeltaScore,
          mphit.ExpectValue,
          mphit.Query.QueryId,
          "0|0",
          GetProteinString(mphit),
          "",
          "0.00",
          mphit.NumMissedCleavages,
          mphit.Modifications));

      foreach (string key in this._annotationKeys)
      {
        if (mphit.Annotations.ContainsKey(key))
        {
          sb.Append("\t" + mphit.Annotations[key]);
        }
        else
        {
          sb.Append("\t");
        }
      }
      return sb.ToString();
    }

    public void WriteToFile(string peptideFile)
    {
      using (var sw = new StreamWriter(peptideFile))
      {
        sw.WriteLine(GetHeader());

        int count = 0;
        var unique = new HashSet<string>();
        foreach (IIdentifiedSpectrum mphit in this._peptides)
        {
          sw.WriteLine(GetString(mphit));

          count++;
          unique.Add(PeptideUtils.GetPureSequence(mphit.Sequence));
        }

        sw.WriteLine();
        sw.WriteLine("----- summary -----");
        sw.WriteLine("Total peptides: " + count);
        sw.WriteLine("Unique peptides: " + unique.Count);
      }
    }
  }
}