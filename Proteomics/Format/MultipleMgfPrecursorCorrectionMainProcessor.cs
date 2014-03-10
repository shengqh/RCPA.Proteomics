using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RCPA.Proteomics.Spectrum;
using RCPA.Proteomics.Mascot;
using System.IO;
using MathNet.Numerics.Statistics;

namespace RCPA.Proteomics.Format
{
  public class MultipleMgfPrecursorCorrectionMainProcessor : AbstractParallelMainProcessor
  {
    private string targetDir;

    private List<Pair<string, double>> expPPMMap;

    public MultipleMgfPrecursorCorrectionMainProcessor(string targetDir, string[] mgfFiles)
      : base(mgfFiles)
    {
      this.targetDir = targetDir;
      //ParallelMode = false;
    }

    protected override void PrepareBeforeProcessing(string peptideFile)
    {
      Progress.SetMessage("Reading peptides from " + peptideFile);

      var peptides = new MascotPeptideTextFormat().ReadFromFile(peptideFile);

      var expMap = peptides.GroupBy(m => m.Query.FileScan.Experimental).ToDictionary(m => m.Key);

      expPPMMap = (from exp in expMap
                   let mean = Statistics.Mean(from pep in exp.Value select PrecursorUtils.mz2ppm(pep.TheoreticalMass, pep.TheoreticalMinusExperimentalMass))
                   orderby exp.Key descending
                   select new Pair<string, double>(exp.Key, mean)).ToList();
    }

    protected override IParallelTaskFileProcessor GetTaskProcessor(string peptideFile, string fileName)
    {
      return new MultipleMgfPrecursorCorrectionTaskProcessor(targetDir, expPPMMap);
    }
  }
}
