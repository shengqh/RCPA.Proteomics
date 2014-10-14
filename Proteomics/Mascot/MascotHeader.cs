namespace RCPA.Proteomics.Mascot
{
  public static class MascotHeader
  {
    public static readonly string MASCOT_PEPTIDE_HEADER =
      "\tFileScan\tQuery\tSequence\tObs\tMH+\tDiff(MH+)\tDiffPPM\tCharge\tRank\tScore\tDeltaScore\tExpectValue\tReference\tMissCleavage\tModification\tMatchCount\tNumProteaseTermini";

    public static readonly string MASCOT_PROTEIN_HEADER =
      "\tName\tDescription\tUniquePeptideCount\tPepCount\tMass\tTotalScore";

    public static readonly string SEQUEST_PEPTIDE_HEADER =
      "\tFileScan\tSequence\tObs\tMH+\tDiff(MH+)\tDiffPPM\tCharge\tRank\tScore\tDeltaScore\tExpectValue\tQuery\tIons\tReference\tDIFF_MODIFIED_CANDIDATE\tPI\tMissCleavage\tModification\tMatchCount\tNumProteaseTermini";

    public static readonly string SEQUEST_PROTEIN_HEADER = "\tName\tDescription\tUniquePepCount\tPepCount\tCoverPercent\tMW\tPI";

    public static readonly string PEPTIDEPROPHET_PEPTIDE_HEADER = MASCOT_PEPTIDE_HEADER + "\tPValue";

    public static readonly string PROTEINPROPHET_PROTEIN_HEADER = "\tName\tDescription\tUniquePeptideCount\tPepCount\tPValue";

    public static readonly string PROTEINPROPHET_PEPTIDE_HEADER = "\tSequence\tMH+\tCharge\tPValue\tNumProteaseTermini";
  }
}