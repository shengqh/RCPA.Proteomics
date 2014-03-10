namespace RCPA.Proteomics.Sequest
{
  public static class SequestHeader
  {
    public static readonly string SEQUEST_PEPTIDE_HEADER =
      "\t\"File, Scan(s)\"\tSequence\tMH+\tDiff(MH+)\tCharge\tRank\tXC\tDeltaCn\tSp\tRSp\tIons\tReference\tDIFF_MODIFIED_CANDIDATE\tPI\tMissCleavage\tMatchedTIC";

    public static readonly string SEQUEST_PROTEIN_HEADER = "\tReference\tPepCount\tUniquePepCount\tCoverPercent\tMW\tPI";
  }
}