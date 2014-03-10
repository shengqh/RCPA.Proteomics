using System;
using System.Collections.Generic;
using System.Text;
using RCPA.Proteomics.Summary;
using System.IO;
using RCPA.Proteomics.Sequest;
using RCPA.Proteomics;

namespace RCPA.Tools.Summary
{
  public class IdentifiedPeptidesMerger : AbstractThreadFileProcessor
  {
    private SequestPeptideTextFormat format;

    private string[] sourceFiles;

    public IdentifiedPeptidesMerger(string[] sourceFiles)
    {
      this.format = new SequestPeptideTextFormat();
      this.sourceFiles = sourceFiles;
    }

    public override IEnumerable<string> Process(string targetFilename)
    {
      using (StreamWriter sw = new StreamWriter(targetFilename))
      {
        HashSet<string> unique = new HashSet<string> ();
        int totalSpectrumCount = 0;

        Progress.SetRange(1, sourceFiles.Length);
        int count = 0;
        LineFormat<IIdentifiedSpectrum> pepFormat = null;
        foreach (string sourceFile in sourceFiles)
        {
          Progress.SetMessage("Processing " + sourceFile + " ...");
          
          var spectra = format.ReadFromFile(sourceFile);

          totalSpectrumCount += spectra.Count;
          unique.UnionWith(IdentifiedSpectrumUtils.GetUniquePeptide(spectra));
          
          if(count == 0){
            pepFormat = format.PeptideFormat;
            sw.WriteLine(pepFormat.GetHeader());
          }

          spectra.ForEach(m => sw.WriteLine(pepFormat.GetString(m)));

          count++;
          Progress.SetPosition(count);
        }

        format.WriteSummary(sw, totalSpectrumCount, unique.Count);

        Progress.End();
      }

      return new[] { targetFilename };
    }
  }
}
