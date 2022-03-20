using RCPA.Proteomics.Isotopic;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace RCPA.Proteomics.Quantification.Labelfree
{
  public class IsotopimerAbundancePredictor : AbstractThreadProcessor
  {
    private string boundaryFile;
    private double minPearsonCorrelation;

    public IsotopimerAbundancePredictor(string boundaryFile, double minPearsonCorrelation)
    {
      this.boundaryFile = boundaryFile;
      this.minPearsonCorrelation = minPearsonCorrelation;
    }

    public override IEnumerable<string> Process()
    {
      var annos = new AnnotationFormat().ReadFromFile(boundaryFile);
      var aas = new Aminoacids();
      var pbuilder = new EmassProfileBuilder();
      var chroFormat = new ChromatographProfileTextWriter();

      foreach (var ann in annos)
      {
        var chroFile = Path.Combine(ann.Annotations["ChroDirectory"] as string, ann.Annotations["ChroFile"] as string) + ".tsv";
        var peptide = (ann.Annotations["PeptideId"] as string).StringBefore("_");
        var start = double.Parse(ann.Annotations["ChroLeft"] as string);
        var end = double.Parse(ann.Annotations["ChroRight"] as string);

        var chro = chroFormat.ReadFromFile(chroFile);
        var chroFiltered = chro.Profiles.Where(l => l.RetentionTime >= start && l.RetentionTime <= end).ToArray();

        //all observed isotopimers, start from 1, transfer to zero-based
        var allIsotopics = (from f in chroFiltered
                            from s in f
                            select s.Isotopic - 1).Distinct().OrderBy(l => l).ToArray();

        //get minimum intensity of each isotopimer
        var minIntensities = (from iso in allIsotopics
                              select (from f in chroFiltered
                                      where f.Count > iso
                                      select f[iso].Intensity).Min()).ToArray();

        //how many isotopic should I trust?
        var iso0 = (from f in chroFiltered select f.First().Intensity).ToArray();
        int maxIso = 1;
        for (int i = 2; i < allIsotopics.Length; i++)
        {
          var isoi = (from f in chroFiltered select f.Count > i ? f[i].Intensity : minIntensities[i]).ToArray();
          var corr = MathNet.Numerics.Statistics.Correlation.Pearson(iso0, isoi);
          if (corr < minPearsonCorrelation)
          {
            maxIso = i;
            break;
          }
        }
        var maxIsoArray = allIsotopics.Where(l => l <= maxIso).ToArray();

        //get observed profile in each scan
        var observedIons = (from f in chroFiltered
                            select (from iso in maxIsoArray select f.Count > iso ? f[iso].Intensity : minIntensities[iso]).ToArray()).ToArray();

        var atomComposition = aas.GetPeptideAtomComposition(peptide);
        var hatom = (int)(Math.Round(aas.ExchangableHAtom(peptide)));
        for (int h2 = 0; h2 < hatom; h2++)
        {

        }


        var profiles = pbuilder.GetProfile(atomComposition, 1, 3);
        var isotopicIons = (from peak in profiles
                            select new IsotopicIon()
                            {
                              Mz = peak.Mz,
                              Intensity = peak.Intensity,
                            }).ToArray();


      }

      return null;
    }
  }
}
