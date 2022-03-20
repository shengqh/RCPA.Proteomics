namespace RCPA.Proteomics.Sequest
{
  public class OutItemIndex
  {
    public int RankIndex { get; set; }
    public int SpRankIndex { get; set; }
    public int TheoreticalMHIndex { get; set; }
    public int DeltaScoreIndex { get; set; }
    public int ScoreIndex { get; set; }
    public int SpScoreIndex { get; set; }
    public int MatchedIonCountIndex { get; set; }
    public int TheoreticalIonCountIndex { get; set; }
    public int ProteinIndex { get; set; }
    public int SequenceIndex { get; set; }
    public int MinCount { get; set; }

    // TurboSEQUEST v.27 (rev. 11), (c) 1999-2000
    // #   Rank/Sp    (M+H)+   deltCn   XCorr    Sp     Ions  Reference                  Peptide
    // 1.   1 /226  1702.0120  0.0000  1.4213   152.7  10/26  IPI:IPI00329593.1|RE +1  -.IVLNPNKPVVEWHR.-
    public static OutItemIndex Rev11 = new OutItemIndex()
    {
      RankIndex = 1,
      SpRankIndex = 2,
      TheoreticalMHIndex = 3,
      DeltaScoreIndex = 4,
      ScoreIndex = 5,
      SpScoreIndex = 6,
      MatchedIonCountIndex = 7,
      TheoreticalIonCountIndex = 8,
      ProteinIndex = 9,
      SequenceIndex = 10,
      MinCount = 11
    };

    // TurboSEQUEST - PVM Slave v.27 (rev. 12), (c) 1998-2005
    // #   Rank/Sp      Id#     (M+H)+    deltCn   XCorr    Sp    Ions   Reference                            Peptide
    // 7.   7 /225          0 2752.29524  0.1643  1.0906   183.2  34/210 gi|47522678|ref|NP_999068.1|     +2  K.KHPINVDFLGIYIPPT*T*KFS*PK.A
    public static OutItemIndex Rev12 = new OutItemIndex()
    {
      RankIndex = 1,
      SpRankIndex = 2,
      TheoreticalMHIndex = 4,
      DeltaScoreIndex = 5,
      ScoreIndex = 6,
      SpScoreIndex = 7,
      MatchedIonCountIndex = 8,
      TheoreticalIonCountIndex = 9,
      ProteinIndex = 10,
      SequenceIndex = 11,
      MinCount = 12
    };

    // SEQUEST v.28 (rev. 13), (c) 1998-2007
    // #   Rank/Sp      Id#     (M+H)+    deltCn   XCorr    Sp     Sf   Ions  Reference                                                                                                                                                                              Peptide
    // 1.   1 /  1      15563 1344.73694  0.0000  2.6095   215.7  0.33  11/24  IPI:IPI00029778.3  +3  R.VPETVSAATQTIK.N
    public static OutItemIndex Rev13 = new OutItemIndex()
    {
      RankIndex = 1,
      SpRankIndex = 2,
      TheoreticalMHIndex = 4,
      DeltaScoreIndex = 5,
      ScoreIndex = 6,
      SpScoreIndex = 7,
      MatchedIonCountIndex = 9,
      TheoreticalIonCountIndex = 10,
      ProteinIndex = 11,
      SequenceIndex = 12,
      MinCount = 13
    };
  }
}
