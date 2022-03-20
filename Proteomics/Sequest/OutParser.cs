using RCPA.Proteomics.Summary;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace RCPA.Proteomics.Sequest
{
  public class DuplicatedReferenceAbsentException : Exception
  {
    public DuplicatedReferenceAbsentException(string filename)
      : base(filename)
    {
    }
  }

  public class OutParser
  {
    private bool raiseDuplicatedReferenceAbsentException = true;
    private Regex reg = new Regex(@"\s+");

    private Regex regexDuplicateProteinsWithId = new Regex(@"^\s+\d+\s+(\S+)");
    private Regex regexDuplicateProteinsWithoutId = new Regex(@"^\s+(\S+)");
    private Regex regexPrecursorMH = new Regex(@"=\s+([\d.]+)");

    private Regex version = new Regex(@"v.(\d+)\s\(rev\.\s*(\d+)");

    private OutItemIndex itemIndex;

    public OutParser()
    {
    }

    public OutParser(bool raiseDuplicatedReferenceAbsentException)
    {
      this.raiseDuplicatedReferenceAbsentException = raiseDuplicatedReferenceAbsentException;
    }

    protected bool ParseFromOutfileLineWithId(List<string> sLines, IdentifiedSpectrum entry)
    {
      if (sLines.Count < 12)
      {
        return false;
      }

      //entry.Index = int.Parse(sLines[0].Substring(0, sLines[0].Length - 1));
      entry.Rank = int.Parse(sLines[1]);
      entry.SpRank = int.Parse(sLines[2]);
      //entry.Id = int.Parse(sLines[3]);
      entry.TheoreticalMH = MyConvert.ToDouble(sLines[4]);
      entry.DeltaScore = MyConvert.ToDouble(sLines[5]);
      entry.Score = MyConvert.ToDouble(sLines[6]);
      entry.SpScore = MyConvert.ToDouble(sLines[7]);
      entry.MatchedIonCount = int.Parse(sLines[8]);
      entry.TheoreticalIonCount = int.Parse(sLines[9]);

      entry.ClearPeptides();
      string sequence;
      if ('+' != sLines[11][0])
      {
        entry.DuplicatedCount = 0;
        sequence = sLines[11];
      }
      else
      {
        entry.DuplicatedCount = int.Parse(sLines[11].Substring(1, sLines[11].Length - 1));
        sequence = sLines[12];
      }

      CheckSequenceValid(ref sequence);

      var sp = new IdentifiedPeptide(entry);
      sp.Sequence = sequence;
      sp.AddProtein(sLines[10]);

      return true;
    }

    protected bool ParseFromOutfileLineWithoutId(List<string> sLines, IdentifiedSpectrum entry)
    {
      if (sLines.Count < 11)
        return false;

      sLines.Insert(3, "0");

      return ParseFromOutfileLineWithId(sLines, entry);
    }

    //  1.   1 /  1          0 1964.9940  0.0000  5.6970  2133.9  21/30  sw|P02666|CASBBOVIN   +1  K.FQSEEQQQTEDELQDK.I
    protected bool ParseFromOutfileLine(string line, IdentifiedSpectrum entry)
    {
      //  Console.Out.WriteLine(line);
      //   dfadfas  entry.IsProteinFromOutFile = true;
      string sLine = line.Trim().Replace('/', ' ');

      string[] sLines = this.reg.Split(sLine);
      if (sLines.Length < itemIndex.MinCount)
      {
        return false;
      }

      entry.Rank = int.Parse(sLines[itemIndex.RankIndex]);
      entry.SpRank = int.Parse(sLines[itemIndex.SpRankIndex]);
      entry.TheoreticalMH = MyConvert.ToDouble(sLines[itemIndex.TheoreticalMHIndex]);
      entry.DeltaScore = MyConvert.ToDouble(sLines[itemIndex.DeltaScoreIndex]);
      entry.Score = MyConvert.ToDouble(sLines[itemIndex.ScoreIndex]);
      entry.SpScore = MyConvert.ToDouble(sLines[itemIndex.SpScoreIndex]);
      entry.MatchedIonCount = int.Parse(sLines[itemIndex.MatchedIonCountIndex]);
      entry.TheoreticalIonCount = int.Parse(sLines[itemIndex.TheoreticalIonCountIndex]);

      entry.ClearPeptides();
      string sequence;
      if ('+' != sLines[itemIndex.SequenceIndex][0])
      {
        entry.DuplicatedCount = 0;
        sequence = sLines[itemIndex.SequenceIndex];
      }
      else
      {
        entry.DuplicatedCount = int.Parse(sLines[itemIndex.SequenceIndex].Substring(1, sLines[itemIndex.SequenceIndex].Length - 1));
        sequence = sLines[itemIndex.SequenceIndex + 1];
      }

      CheckSequenceValid(ref sequence);

      var sp = new IdentifiedPeptide(entry);
      sp.Sequence = sequence;
      sp.AddProtein(sLines[itemIndex.ProteinIndex]);

      return true;
    }

    protected void CheckSequenceValid(ref string sequence)
    {
      if (sequence == null || sequence.Length == 0)
      {
        return;
      }

      if ('.' == sequence[0])
      {
        sequence.Insert(0, "X");
      }

      if ('.' == sequence[sequence.Length - 1])
      {
        sequence = sequence + "X";
      }
    }

    private bool ShortProteinExists(string protein, ICollection<string> proteins)
    {
      return proteins.Contains(protein);
    }

    private bool LongProteinExists(string protein, ICollection<string> proteins)
    {
      foreach (string p in proteins)
      {
        if (p.StartsWith(protein))
        {
          return true;
        }
      }
      return false;
    }

    protected bool ReadNextEntry(List<string> outContents, ref int index, IdentifiedSpectrum entry, bool firstentry,
                                 bool bHasId)
    {
      if (firstentry && index == outContents.Count)
      {
        return false;
      }

      if (outContents[index].Trim().Length == 0)
      {
        return false;
      }

      if (!ParseFromOutfileLine(outContents[index], entry))
      {
        return false;
      }

      Regex regex = bHasId ? this.regexDuplicateProteinsWithId : this.regexDuplicateProteinsWithoutId;
      index++;
      while (index < outContents.Count && outContents[index].Trim().Length != 0)
      {
        // duplicated entry
        if (outContents[index].StartsWith("    "))
        {
          Match m = regex.Match(outContents[index]);
          string protein;
          if (m.Success)
          {
            protein = m.Groups[1].Value;
          }
          else
          {
            //ignore current line and break
            index++;
            break;
          }

          ICollection<string> existsProteins = entry.Peptides[0].Proteins;
          ProteinExists pe;
          if (protein.Length >= 20)
          {
            pe = LongProteinExists;
          }
          else
          {
            pe = ShortProteinExists;
          }

          bool bExist = pe(protein, existsProteins);

          if (!bExist)
          {
            entry.Peptides[0].AddProtein(protein);
          }

          entry.DuplicatedCount = entry.Peptides[0].Proteins.Count - 1;
          index++;
        }
        else
        {
          break;
        }
      }

      return true;
    }

    public IIdentifiedSpectrum ParseFromFile(string filename)
    {
      List<string> outContents = FileUtils.ReadFile(filename);
      return Parse(outContents);
    }

    public IIdentifiedSpectrum Parse(List<string> outContents)
    {
      if (outContents.Count < 11)
      {
        return null;
      }

      int index = 0;
      while (index < outContents.Count && outContents[index].Trim().Length == 0)
      {
        index++;
      }

      string sequestOutFilename = outContents[index].Trim();
      index++;
      var m = version.Match(outContents[index]);
      var curVersion = Convert.ToInt32(m.Groups[1].Value);
      var revVersion = Convert.ToInt32(m.Groups[2].Value);
      //var isVersionHigherThan27 = curVersion > 27;

      index++;
      while (index < outContents.Count)
      {
        if (outContents[index].Contains("(M+H)+ mass ="))
        {
          break;
        }
        index++;
      }

      if (index == outContents.Count)
      {
        return null;
      }

      //get parent ion mass
      Match matchPrecursorMH = this.regexPrecursorMH.Match(outContents[index]);
      if (!matchPrecursorMH.Success)
      {
        return null;
      }
      double dParentMass = MyConvert.ToDouble(matchPrecursorMH.Groups[1].Value);
      bool isMono = outContents[index].IndexOf("MONO/") > 0;

      while (index < outContents.Count)
      {
        if (outContents[index].Contains("total inten ="))
        {
          break;
        }
        index++;
      }

      if (index == outContents.Count)
      {
        return null;
      }

      //get matched ion count
      Match matchTIC = this.regexPrecursorMH.Match(outContents[index]);
      if (!matchTIC.Success)
      {
        return null;
      }
      double dTotalInten = MyConvert.ToDouble(matchTIC.Groups[1].Value);

      //skip other lines
      while (index < outContents.Count)
      {
        if (outContents[index].Contains("Rank/Sp"))
        {
          break;
        }
        index++;
      }

      if (index >= outContents.Count)
      {
        return null;
      }

      var bHasId = revVersion > 11;
      if (revVersion <= 11)
      {
        itemIndex = OutItemIndex.Rev11;
      }
      else if (revVersion == 12)
      {
        itemIndex = OutItemIndex.Rev12;
      }
      else
      {
        itemIndex = OutItemIndex.Rev13;
      }

      //skip a line
      index++;
      if (index >= outContents.Count)
      {
        return null;
      }

      var result = new IdentifiedSpectrum();
      result.IsPrecursorMonoisotopic = isMono;

      //move to first entry
      index++;

      if (!ReadNextEntry(outContents, ref index, result, true, bHasId))
      {
        return null;
      }

      result.Query.FileScan.LongFileName = sequestOutFilename;

      if (this.raiseDuplicatedReferenceAbsentException && (0 < result.DuplicatedCount) &&
          (1 == result.Peptides[0].Proteins.Count))
      {
        throw new DuplicatedReferenceAbsentException(string.Format("File {0} error:\nmultiple proteins contains candidate peptide but only one protein name was recorded. You need to modify search parameter to let sequest output at least 50 proteins.", sequestOutFilename));
      }

      if (dParentMass > 0)
      {
        result.ExperimentalMH = dParentMass;
      }
      else
      {
        result.ExperimentalMH = result.TheoreticalMH;
      }

      result.MatchedTIC = dTotalInten;

      result.DeltaScore = 1.0;

      var otherEntry = new IdentifiedSpectrum();
      do
      {
        bool hasNext = false;
        try
        {
          hasNext = ReadNextEntry(outContents, ref index, otherEntry, false, bHasId);
        }
        catch (Exception)
        {
          break;
        }

        if (!hasNext)
        {
          break;
        }

        if (1 == otherEntry.Rank)
        {
          if (this.raiseDuplicatedReferenceAbsentException && (0 < otherEntry.DuplicatedCount) &&
              (1 == otherEntry.Peptides[0].Proteins.Count))
          {
            throw new DuplicatedReferenceAbsentException(sequestOutFilename);
          }
          result.AddPeptides(otherEntry.Peptides);
        }
        else if (SkipCurrentEntry(result, otherEntry))
        {
          continue;
        }
        else
        {
          result.DeltaScore = otherEntry.DeltaScore;
          break;
        }
      } while (true);

      return result;
    }

    protected virtual bool SkipCurrentEntry(IdentifiedSpectrum result, IdentifiedSpectrum otherEntry)
    {
      return false;
    }

    #region Nested type: ProteinExists

    private delegate bool ProteinExists(string protein, ICollection<string> proteins);

    #endregion
  }

  public class ModificationOutParser : OutParser
  {
    private readonly double maxDeltaScore;

    public ModificationOutParser(double maxDeltaScore)
    {
      this.maxDeltaScore = maxDeltaScore;
    }

    public ModificationOutParser(bool raiseDuplicatedReferenceAbsentException, double maxDeltaScore)
      :
        base(raiseDuplicatedReferenceAbsentException)
    {
      this.maxDeltaScore = maxDeltaScore;
    }

    protected override bool SkipCurrentEntry(IdentifiedSpectrum entry, IdentifiedSpectrum currEntry)
    {
      string sNextPureSequence = currEntry.Peptides[0].PureSequence;
      for (int i = 0; i < entry.Peptides.Count; i++)
      {
        IIdentifiedPeptide sp = entry.Peptides[i];
        if (sNextPureSequence.Equals(sp.PureSequence))
        {
          if (currEntry.DeltaScore <= this.maxDeltaScore)
          {
            entry.DiffModificationSiteCandidates.Add(new FollowCandidate(currEntry.GetSequences(" ! "), currEntry.Score,
                                                                         currEntry.DeltaScore));
          }
          return true;
        }
      }
      return false;
    }
  };
}