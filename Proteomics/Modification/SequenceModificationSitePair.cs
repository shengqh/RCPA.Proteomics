using RCPA.Proteomics.Summary;
using RCPA.Proteomics.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RCPA.Proteomics.Modification
{
  public class SequenceModificationSitePair : IComparable<SequenceModificationSitePair>
  {
    public static char MULTI_STATE_SITE = '&';

    public static char AMBIGUOUS_SITE = 'p';

    private List<Pair<char, char>> sites = new List<Pair<char, char>>();

    private int modifiedCount;

    private int trueModifiedCount;

    private int ambiguousModifiedCount;

    private int multistateModifiedCount;

    private String pureSequence;

    private String modifiedSequence;

    private bool isCandidateModifiedAminoacid(String modifiedAminoacids,
        char aminoacid)
    {
      return modifiedAminoacids.IndexOf(aminoacid) != -1;
    }

    private Pair<char, char> getLastPair()
    {
      return sites.Last();
    }

    public SequenceModificationSitePair(String modifiedAminoacids,
        IIdentifiedSpectrum hit, char modifiedSign)
    {
      initByPeptideHit(modifiedAminoacids, hit, modifiedSign);
    }

    private void initByPeptideHit(String modifiedAminoacids,
        IIdentifiedSpectrum hit, char modifiedSign)
    {
      List<String> peptideSequences = (from p in hit.Peptides
                                       select p.Sequence).ToList();

      this.init(modifiedAminoacids, peptideSequences[0], modifiedSign);

      for (int i = 1; i < peptideSequences.Count; i++)
      {
        SequenceModificationSitePair ambi = new SequenceModificationSitePair(
            modifiedAminoacids, peptideSequences[i]);
        this.mergeWithAmbiguous(ambi);
      }
    }

    public SequenceModificationSitePair(String modifiedAminoacids,
        IIdentifiedSpectrum hit)
    {
      initByPeptideHit(modifiedAminoacids, hit, ' ');
    }

    private void init(SequenceModificationSitePair source)
    {
      sites.Clear();
      for (int i = 0; i < source.sites.Count; i++)
      {
        sites.Add(new Pair<char, char>(source.sites[i]));
      }
      this.modifiedCount = source.modifiedCount;
      this.trueModifiedCount = source.trueModifiedCount;
      this.ambiguousModifiedCount = source.ambiguousModifiedCount;
      this.pureSequence = source.pureSequence;
      this.modifiedSequence = source.modifiedSequence;
      this.multistateModifiedCount = source.multistateModifiedCount;
    }

    public SequenceModificationSitePair(SequenceModificationSitePair source)
    {
      init(source);
    }

    public SequenceModificationSitePair(String modifiedAminoacids,
        String sequence, char modifiedSign)
    {
      init(modifiedAminoacids, sequence, modifiedSign);
    }

    public SequenceModificationSitePair(String modifiedAminoacids, String sequence)
    {
      init(modifiedAminoacids, sequence, ' ');
    }

    private void init(String modifiedAminoacids, String sequence,
        char modifiedSign)
    {
      String matchSequence = PeptideUtils.GetMatchedSequence(sequence);
      this.pureSequence = PeptideUtils.GetPureSequence(matchSequence);

      modifiedCount = 0;

      for (int i = 0; i < matchSequence.Length; i++)
      {
        char c = matchSequence[i];
        if (char.IsLetter(c) && char.IsUpper(c))
        {
          sites.Add(new Pair<char, char>(c, ' '));
        }
        else if (isCandidateModifiedAminoacid(modifiedAminoacids,
            getLastPair().First))
        {
          if (modifiedSign == ' ')
          {
            getLastPair().Second = c;
          }
          else
          {
            getLastPair().Second = modifiedSign;
          }
          modifiedCount++;
        }
      }
      calculate();
    }

    public void mergeWithAmbiguous(SequenceModificationSitePair another)
    {
      if (another.sites.Count != sites.Count)
      {
        throw new ArgumentException(
            "Maybe those pairs are not from same out file : this="
                + this.ToString() + " ; argument=" + another.ToString());
      }

      for (int i = 0; i < sites.Count; i++)
      {
        if (sites[i].Second == another.sites[i].Second)
        {
          continue;
        }
        else if (sites[i].Second == ' ' || another.sites[i].Second == ' ')
        {
          sites[i].Second = AMBIGUOUS_SITE;
        }
      }

      calculate();
    }

    private bool isModified(int index)
    {
      return this.sites[index].Second != ' ';
    }

    private bool isMultipleStateModified(int index)
    {
      return this.sites[index].Second == MULTI_STATE_SITE;
    }

    private bool isPositiveModified(int index)
    {
      return isModified(index) && !isAmbiguousModified(index);
    }

    private bool isAmbiguousModified(int index)
    {
      return this.sites[index].Second == AMBIGUOUS_SITE;
    }

    private void checkModificationSiteCount()
    {
      if (this.modifiedCount < this.trueModifiedCount)
      {
        this.modifiedCount = this.trueModifiedCount;
      }

      if (this.modifiedCount == this.trueModifiedCount
          && this.ambiguousModifiedCount != 0)
      {
        this.removeAmbiguousModifiedSite();
        calculate();
      }
    }

    public void mergeWithSurePeptide(SequenceModificationSitePair another)
    {
      // 首先将确定的位点全部赋值
      SequenceModificationSitePair tmp2 = new SequenceModificationSitePair(
          another);
      tmp2.mergeTrueModificationSite(this);
      this.mergeTrueModificationSite(another);

      if (tmp2.modifiedCount - tmp2.trueModifiedCount == this.modifiedCount
          - this.trueModifiedCount)
      {
        if (tmp2.ambiguousModifiedCount > 0 && this.containAllAmbiguousSite(tmp2))
        {
          this.replaceAmbiguousSite(tmp2);
        }
        else if (this.ambiguousModifiedCount > 0
            && this.allAmbiguousSiteContainedIn(tmp2))
        {
          return;
        }
      }
      this.mergeAmbiguousSites(tmp2);
      //this.modifiedCount = Math.max(this.modifiedCount, another.modifiedCount);
    }

    public void mergeAmbiguousSites(SequenceModificationSitePair shorter)
    {
      int ipos = this.pureSequence.IndexOf(shorter.pureSequence);
      if (ipos == -1)
      {
        throw new ArgumentException(this.pureSequence
            + " doesn't include " + shorter.pureSequence);
      }

      for (int i = 0; i < shorter.sites.Count; i++)
      {
        if (shorter.isAmbiguousModified(i) && !this.isModified(i + ipos))
        {
          this.sites[i + ipos].Second = shorter.sites[i].Second;
        }
      }

      if (this.modifiedCount < shorter.modifiedCount)
      {
        this.modifiedCount = shorter.modifiedCount;
      }
      calculate();
    }

    public void replaceAmbiguousSite(SequenceModificationSitePair shorter)
    {
      int ipos = this.pureSequence.IndexOf(shorter.pureSequence);
      if (ipos == -1)
      {
        throw new ArgumentException(this.pureSequence
            + " doesn't include " + shorter.pureSequence);
      }

      this.removeAmbiguousModifiedSite();

      for (int i = 0; i < shorter.sites.Count; i++)
      {
        if (shorter.isAmbiguousModified(i) && !this.isModified(i + ipos))
        {
          this.sites[i + ipos].Second = shorter.sites[i].Second;
        }
      }
      calculate();
    }

    public bool allModificationSiteContainedIn(
        SequenceModificationSitePair longer)
    {
      if (!this.pureSequence.Equals(longer.pureSequence))
      {
        throw new ArgumentException(
            "Argument of allModificationSiteContainedIn must has same peptide sequence!");
      }

      for (int i = 0; i < sites.Count; i++)
      {
        if (this.isModified(i) && !longer.isModified(i))
        {
          return false;
        }
      }
      return true;
    }

    public bool allAmbiguousSiteContainedIn(
        SequenceModificationSitePair shorter)
    {
      int ipos = this.pureSequence.IndexOf(shorter.pureSequence);
      if (ipos == -1)
      {
        throw new ArgumentException(this.pureSequence
            + " doesn't include " + shorter.pureSequence);
      }

      for (int i = 0; i < ipos; i++)
      {
        if (this.isAmbiguousModified(i))
        {
          return false;
        }
      }

      int iend = ipos + shorter.sites.Count;
      for (int i = ipos; i < iend; i++)
      {
        if (this.isAmbiguousModified(i) && !shorter.isModified(i - ipos))
        {
          return false;
        }
      }

      return true;
    }

    public bool containAllAmbiguousSite(SequenceModificationSitePair shorter)
    {
      int ipos = this.pureSequence.IndexOf(shorter.pureSequence);
      if (ipos == -1)
      {
        throw new ArgumentException(this.pureSequence
            + " doesn't include " + shorter.pureSequence);
      }

      for (int i = 0; i < shorter.sites.Count; i++)
      {
        if (shorter.isAmbiguousModified(i) && !this.isModified(i + ipos))
        {
          return false;
        }
      }

      return true;
    }

    public bool mergeOverlap(SequenceModificationSitePair another,
        String pureOverlapSequence)
    {
      SequenceModificationSitePair copy = new SequenceModificationSitePair(
          another);
      int copyPos = copy.pureSequence.IndexOf(pureOverlapSequence);
      int curPos = this.pureSequence.IndexOf(pureOverlapSequence);

      if (copyPos > curPos)
      {
        if (copy.mergeOverlap(this, pureOverlapSequence))
        {
          init(copy);
          return true;
        }

        return false;
      }

      String overlapSequence = this.pureSequence.Substring(curPos - copyPos);
      if (!copy.pureSequence.StartsWith(overlapSequence))
      {
        return false;
      }

      int startPos = curPos - copyPos;
      for (int i = 0; i < startPos; i++)
      {
        // if there is modification in the overlap-outside range, merge fail.
        if (!sites[i].Second.Equals(' '))
        {
          return false;
        }
      }
      int endPos = overlapSequence.Length;
      for (int i = endPos; i < copy.pureSequence.Length; i++)
      {
        // if there is modification in the overlap-outside range, merge fail.
        if (!copy.sites[i].Second.Equals(' '))
        {
          return false;
        }
      }

      for (int i = 0; i < endPos; i++)
      {
        if (copy.isPositiveModified(i))
        {
          if (this.isPositiveModified(i + startPos))
          {
            if (!this.sites[i + startPos].Second.Equals(copy.sites[i].Second))
            {
              this.sites[i + startPos].Second = MULTI_STATE_SITE;
            }
            continue;
          }

          if (!this.isModified(i + startPos))
          {
            this.modifiedCount++;
          }

          this.sites[i + startPos].Second = copy.sites[i].Second;
          continue;
        }
      }

      for (int i = endPos; i < copy.pureSequence.Length; i++)
      {
        this.sites.Add(new Pair<char, char>(copy.sites[i]));
      }

      calculate();

      return true;
    }

    public void mergeTrueModificationSite(SequenceModificationSitePair another)
    {
      if (pureSequence.Length < another.pureSequence.Length)
      {
        mergeTrueModificationSiteWithLongerSequence(another);
      }
      else
      {
        mergeTrueModificationSiteWithShorterSequence(another);
      }
    }

    public void mergeTrueModificationSiteWithShorterSequence(
        SequenceModificationSitePair shorter)
    {
      int ipos = this.pureSequence.IndexOf(shorter.pureSequence);
      if (ipos == -1)
      {
        throw new ArgumentException(this.pureSequence
            + " doesn't include " + shorter.pureSequence);
      }

      for (int i = 0; i < shorter.pureSequence.Length; i++)
      {
        if (shorter.isPositiveModified(i))
        {
          if (this.isPositiveModified(i + ipos))
          {
            if (!this.sites[i + ipos].Second.Equals(shorter.sites[i].Second))
            {
              this.sites[i + ipos].Second = MULTI_STATE_SITE;
            }
            continue;
          }

          if (!this.isModified(i + ipos))
          {
            this.modifiedCount++;
          }

          this.sites[i + ipos].Second = shorter.sites[i].Second;
          continue;
        }
      }
      calculate();
    }

    public void mergeTrueModificationSiteWithLongerSequence(
        SequenceModificationSitePair longer)
    {
      SequenceModificationSitePair copy = new SequenceModificationSitePair(longer);
      copy.mergeTrueModificationSiteWithShorterSequence(this);
      this.init(copy);
    }

    private void removeAmbiguousModifiedSite()
    {
      for (int i = 0; i < sites.Count; i++)
      {
        if (sites[i].Second.Equals(AMBIGUOUS_SITE))
        {
          sites[i].Second = ' ';
        }
      }
    }

    private void calculate()
    {
      trueModifiedCount = 0;
      ambiguousModifiedCount = 0;
      multistateModifiedCount = 0;
      for (int i = 0; i < sites.Count; i++)
      {
        if (sites[i].Second.Equals(AMBIGUOUS_SITE))
        {
          ambiguousModifiedCount++;
        }
        else if (!sites[i].Second.Equals(' '))
        {
          trueModifiedCount++;
          if (sites[i].Second.Equals(MULTI_STATE_SITE))
          {
            multistateModifiedCount++;
          }
        }
      }

      StringBuilder sbModifiedSequence = new StringBuilder();
      StringBuilder sbPureSequence = new StringBuilder();
      for (int i = 0; i < sites.Count; i++)
      {
        sbPureSequence.Append(sites[i].First);
        sbModifiedSequence.Append(sites[i].First);
        if (sites[i].Second != ' ')
        {
          sbModifiedSequence.Append(sites[i].Second);
        }
      }
      pureSequence = sbPureSequence.ToString();
      modifiedSequence = sbModifiedSequence.ToString();

      checkModificationSiteCount();
    }

    public override string ToString()
    {
      return modifiedSequence;
    }

    public int getModifiedCount()
    {
      return modifiedCount;
    }

    public int getTrueModifiedCount()
    {
      return trueModifiedCount;
    }

    public int getAmbiguousModifiedCount()
    {
      return ambiguousModifiedCount;
    }

    public int getMultistateModifiedCount()
    {
      return multistateModifiedCount;
    }

    public List<Pair<char, char>> getModificationSiteList()
    {
      List<Pair<char, char>> result = new List<Pair<char, char>>();

      for (int i = 0; i < sites.Count; i++)
      {
        if (this.isModified(i))
        {
          result.Add(new Pair<char, char>(sites[i]));
        }
      }
      return result;
    }

    public List<Pair<char, char>> getPositiveModificationSiteList()
    {
      List<Pair<char, char>> result = new List<Pair<char, char>>();

      for (int i = 0; i < sites.Count; i++)
      {
        if (this.isPositiveModified(i))
        {
          result.Add(new Pair<char, char>(sites[i]));
        }
      }
      return result;
    }

    public List<Pair<char, char>> getAmbiguousModificationSiteList()
    {
      List<Pair<char, char>> result = new List<Pair<char, char>>();

      for (int i = 0; i < sites.Count; i++)
      {
        if (this.isAmbiguousModified(i))
        {
          result.Add(new Pair<char, char>(sites[i]));
        }
      }
      return result;
    }

    public List<Pair<char, char>> getMultipleStateModificationSiteList()
    {
      List<Pair<char, char>> result = new List<Pair<char, char>>();

      for (int i = 0; i < sites.Count; i++)
      {
        if (isMultipleStateModified(i))
        {
          result.Add(new Pair<char, char>(sites[i]));
        }
      }
      return result;
    }

    public List<Pair<char, char>> getSiteList()
    {
      return sites;
    }

    public List<Pair<char, char>> getSiteListByFilter(
        IFilter<Pair<char, char>> filter)
    {
      List<Pair<char, char>> result = new List<Pair<char, char>>();

      for (int i = 0; i < sites.Count; i++)
      {
        if (filter.Accept(sites[i]))
        {
          result.Add(new Pair<char, char>(sites[i]));
        }
      }
      return result;
    }

    public String getModifiedSequence()
    {
      return modifiedSequence;
    }

    public void setModifiedCount(int modifiedCount)
    {
      this.modifiedCount = modifiedCount;
    }

    public String getPureSequence()
    {
      return pureSequence;
    }

    public override bool Equals(object obj)
    {
      if (obj == this)
      {
        return true;
      }
      if (!(obj is SequenceModificationSitePair))
      {
        return false;
      }

      SequenceModificationSitePair rhs = (SequenceModificationSitePair)obj;

      return this.modifiedSequence == rhs.modifiedSequence && this.modifiedCount == rhs.modifiedCount;
    }

    public int CompareTo(SequenceModificationSitePair other)
    {
      var result = this.modifiedSequence.CompareTo(other.modifiedSequence);
      if (result == 0)
      {
        result = this.modifiedCount.CompareTo(other.modifiedCount);
      }
      return result;
    }

    public override int GetHashCode()
    {
      return this.GetHashCodeFromFields(this.modifiedSequence, this.modifiedCount);
    }
  }
}
