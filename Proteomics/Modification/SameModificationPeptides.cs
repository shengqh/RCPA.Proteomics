//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using RCPA.Proteomics.Summary;

//namespace RCPA.Proteomics.Modification
//{
//public class SameModificationPeptides {
//  private String transMatchSequence;

//  private String matchSequence;

//  private String pureSequence;

//  private List<IIdentifiedSpectrum> peptideHits;

//  private SequenceModificationSitePair sequence;

//  public SameModificationPeptides(String modifiedAminoacids,
//      List<BuildSummaryPeptideHit> pephits) {
//    this.peptideHits = new ArrayList<BuildSummaryPeptideHit>(pephits);

//    IdentifiedPeptideHitUtils.sortByCandidatePeptideCountAscending(peptideHits);

//    BuildSummaryPeptide peptide = peptideHits.get(0).getPeptide(0);

//    this.pureSequence = PeptideUtils.getPurePeptideSequence(peptide
//        .getSequence());

//    this.sequence = getSequenceModificationSitePair(modifiedAminoacids);

//    this.matchSequence = sequence.toString();

//    this.transMatchSequence = new SequenceModificationSitePair(
//        modifiedAminoacids, peptide.getSequence(), '*').toString();
//  }

//  private SequenceModificationSitePair getSequenceModificationSitePair(
//      String modifiedAminoacids) {
//    SequenceModificationSitePair result = new SequenceModificationSitePair(
//        modifiedAminoacids, peptideHits.get(0));

//    for (int i = 1; i < peptideHits.size(); i++) {
//      BuildSummaryPeptideHit phit = peptideHits.get(i);
//      boolean bFound = false;
//      for(BuildSummaryPeptide pep : phit.getPeptides()){
//        if(pureSequence.equals(PeptideUtils.getPurePeptideSequence(pep.getSequence()))){
//          SequenceModificationSitePair current = new SequenceModificationSitePair(
//              modifiedAminoacids, pep.getSequence(), '*');
//          result.mergeWithSurePeptide(current);
//          bFound = true;
//          break;
//        }
//      }
			
//      if(!bFound){
//        for (int k = 0; k < phit.getFollowCandidates().size(); k++) {
//          String seq =PeptideUtils.getPurePeptideSequence(phit.getFollowCandidates().get(k).getSequence());
//          if(pureSequence.equals(seq)){
//            SequenceModificationSitePair current = new SequenceModificationSitePair(
//                modifiedAminoacids, phit.getFollowCandidates().get(k).getSequence(), '*');
//            result.mergeWithSurePeptide(current);
//            bFound = true;
//            break;
//          }
//        }
//      }
			
//      if(!bFound){
//        throw new IllegalArgumentException("Cannot find sequence "
//            + pureSequence + " in peptide "
//            + phit.getPeakListInfo().getLongFilename());
//      }
//    }

//    return result;
//  }

//  public SequenceModificationSitePair getSequence() {
//    return sequence;
//  }

//  public List<BuildSummaryPeptideHit> getPeptideHits() {
//    return Collections.unmodifiableList(peptideHits);
//  }

//  public void addPeptideHits(List<BuildSummaryPeptideHit> phits) {
//    this.peptideHits.addAll(phits);
//  }

//  public String getPureSequence() {
//    return pureSequence;
//  }

//  public String getMatchSequence() {
//    return matchSequence;
//  }

//  public String getTransMatchSequence() {
//    return transMatchSequence;
//  }

//  public int getPeptideHitCount() {
//    return peptideHits.size();
//  }

//  public static SameModificationPeptides parseFromBuildSummaryPeptideHit(
//      String modifiedAminoacid, String line) throws SequestParseException {
//    ArrayList<BuildSummaryPeptideHit> peps = new ArrayList<BuildSummaryPeptideHit>();

//    peps.add(BuildSummaryPeptideHitReader.parse(line));

//    return new SameModificationPeptides(modifiedAminoacid, peps);
//  }

//}

//}