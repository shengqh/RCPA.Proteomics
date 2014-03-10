using System;
using System.Collections.Generic;
using RCPA.Seq;

/*
 *                    BioJava development code
 *
 * This code may be freely distributed and modified under the
 * terms of the GNU Lesser General Public Licence.  This should
 * be distributed with the code.  If you do not have a copy,
 * see:
 *
 *      http://www.gnu.org/copyleft/lesser.html
 *
 * Copyright for this code is held jointly by the individual
 * authors.  These should be listed in @author doc comments.
 *
 * For more information on the BioJava project and its aims,
 * or to join the biojava-l mailing list, visit the home page
 * at:
 *
 *      http://www.biojava.org/
 *
 */

namespace RCPA.Proteomics
{
  /**
   * This class contains methods for calculating the results of proteolytic digestion
   * of a protein sequence
   *
   * <b> this class is not designed to be thread safe </b>
   *
   * @author Michael Jones
   * @author Mark Schreiber (refactoring, some documentation)
   */

  public class Digest
  {
    public class DigestRangeLocation : RangeLocation
    {
      public int MissCleavage { get; set; }

      public DigestRangeLocation(int min, int max, int miss)
        : base(min, max)
      {
        this.MissCleavage = miss;
      }
    }

    public static String PEPTIDE_FEATURE_TYPE = "Peptide";
    private IRangeLocationFilter filter;
    private int maxMissedCleavages;
    private List<DigestRangeLocation> peptideQue = new List<DigestRangeLocation>();
    private Protease protease;
    private Sequence sequence;

    /** Creates a new Digest Bean*/

    public IRangeLocationFilter Filter
    {
      set { this.filter = value; }
    }

    public Protease DigestProtease
    {
      get { return this.protease; }
      set { this.protease = value; }
    }

    public Sequence ProteinSequence
    {
      get { return this.sequence; }
      set { this.sequence = value; }
    }

    /**
     * Sets the maximum number of partial digest products to be annotated.
     * @param maxMissedCleavages the max number of partial digest products
     */

    public int MaxMissedCleavages
    {
      set { this.maxMissedCleavages = value; }
    }


    /** Adds peptides as features to the Sequence in this class. The feature will
     * contain a small annotation specifying the protease with the key "protease".
     * For Example:
     * <PRE>
     *
     *         Sequence sequence = ...
     *         Digest bioJavaDigest = new Digest();
     *
     *         bioJavaDigest.setMaxMissedCleavages(2);
     *         bioJavaDigest.setProtease(ProteaseManager.getProteaseByName(Protease.ASP_N));
     *         bioJavaDigest.setSequence(sequence);
     *         bioJavaDigest.addDigestFeatures();
     * </PRE>
     */

    public void AddDigestFeatures()
    {
      if (null == this.protease)
      {
        throw new InvalidOperationException("Protease is null");
      }

      if (null == this.sequence)
      {
        throw new InvalidOperationException("Sequence is null");
      }

      this.sequence.Annotation.Remove(PEPTIDE_FEATURE_TYPE);

      string cleaveSites = this.protease.CleaveageResidues;
      string notCleave = this.protease.NotCleaveResidues;

      if (0 == cleaveSites.Length && 0 == notCleave.Length)
      {
        throw new InvalidOperationException("Protease contains no cleave information");
      }

      this.peptideQue = new List<DigestRangeLocation>();

      bool endoProtease = this.protease.IsEndoProtease;
      int nTerm = 1;

      string seq = this.sequence.SeqString;
      for (int j = 1; j <= seq.Length; j++)
      {
        char aa = seq[j - 1];

        if (-1 != cleaveSites.IndexOf(aa))
        {
          if (endoProtease)
          {
            bool cleave = true;
            if (j < seq.Length)
            {
              char nextAA = seq[j];
              if (-1 != notCleave.IndexOf(nextAA))
              {
                cleave = false;
              }
            }

            if (cleave)
            {
              this.peptideQue.Add(new DigestRangeLocation(nTerm, j, 0));
              nTerm = j + 1;
            }
          }
          else
          {
            if (j > 1)
            {
              this.peptideQue.Add(new DigestRangeLocation(nTerm, j - 1, 0));
              nTerm = j;
            }
          }
        }
      }

      if (nTerm <= seq.Length)
      {
        this.peptideQue.Add(new DigestRangeLocation(nTerm, seq.Length, 0));
      }

      AddMissedCleavages();

      //Now add the locations as Peptide freatures to the Sequence
      if (this.filter != null)
      {
        this.filter.SetSequence(this.sequence);
        foreach (var loc in this.peptideQue)
        {
          if (this.filter.Accept(loc))
          {
            CreatePeptideFeature(loc);
          }
        }
      }
      else
      {
        foreach (var loc in this.peptideQue)
        {
          CreatePeptideFeature(loc);
        }
      }
    }

    public void AddMissedCleavages()
    {
      if (this.maxMissedCleavages > 0)
      {
        var missedList = new List<DigestRangeLocation>();
        for (int i = 0; i < this.peptideQue.Count; i++)
        {
          DigestRangeLocation loc = this.peptideQue[i];

          for (int iMiss = 0; iMiss < this.maxMissedCleavages; iMiss++)
          {
            int j = i + 1 + iMiss;
            if (j == this.peptideQue.Count)
            {
              break;
            }

            RangeLocation loc2 = this.peptideQue[j];
            missedList.Add(new DigestRangeLocation(loc.Min, loc2.Max, iMiss + 1));
          }
        }

        this.peptideQue.AddRange(missedList);
      }
    }

    public void CreatePeptideFeature(DigestRangeLocation loc)
    {
      if (!this.sequence.Annotation.ContainsKey(PEPTIDE_FEATURE_TYPE))
      {
        this.sequence.Annotation[PEPTIDE_FEATURE_TYPE] = new List<DigestPeptideInfo>();
      }
      var peptides = (List<DigestPeptideInfo>)this.sequence.Annotation[PEPTIDE_FEATURE_TYPE];

      var dpi = new DigestPeptideInfo();
      dpi.ProteinName = this.sequence.Name;
      dpi.PeptideSeq = this.sequence.SeqString.Substring(loc.Min - 1, loc.Max - loc.Min + 1);
      dpi.PeptideLoc = new RangeLocation(loc);
      dpi.MissCleavage = loc.MissCleavage;
      peptides.Add(dpi);
    }
  }

  public static class DigestHelper
  {
    public static List<DigestPeptideInfo> GetDigestPeptideInfo(this Sequence protein)
    {
      return (List<DigestPeptideInfo>)protein.Annotation[Digest.PEPTIDE_FEATURE_TYPE];
    }
  }
}