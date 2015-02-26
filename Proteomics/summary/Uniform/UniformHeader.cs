namespace RCPA.Proteomics.Sequest
{
  public static class UniformHeader
  {
    public static readonly string PEPTIDE_HEADER = "\tFileScan\tSequence\tMH+\tDiff(MH+)\tDiffPPM\tCharge\tRank\tScore\tDeltaScore\tExpectValue\tIons\tReference\tDIFF_MODIFIED_CANDIDATE\tPI\tMissCleavage\tModification\tMatchedTIC\tNumProteaseTermini\tEngine\tTag\tDecoy\tSiteProbability";

    public static readonly string PROTEIN_HEADER = "\tReference\tPepCount\tUniquePepCount\tCoverPercent\tMW\tPI";
  }
}