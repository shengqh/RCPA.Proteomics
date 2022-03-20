using RCPA.Proteomics.Mascot;
using System.Collections.Generic;

namespace RCPA.Proteomics.MaxQuant
{
  public class MaxQuantPeptidesMerger : AbstractThreadFileProcessor
  {
    private MascotPeptideTextFormat format;

    private Dictionary<string, List<string>> sourceFiles;

    public MaxQuantPeptidesMerger(Dictionary<string, List<string>> sourceFiles)
    {
      this.format = new MascotPeptideTextFormat();
      this.sourceFiles = sourceFiles;
    }

    public override IEnumerable<string> Process(string targetFilename)
    {
      foreach (var key in sourceFiles.Keys)
      {
        foreach (var file in sourceFiles[key])
        {
          Progress.SetMessage("Processing " + file + " ...");
          var spectra = format.ReadFromFile(file);
          spectra.RemoveAll(m => m.GetMaxQuantItemList()[0].Ratio.Length == 0);
          spectra.ForEach(m => m.Tag = key);

        }
      }

      //using (StreamWriter sw = new StreamWriter(targetFilename))
      //{
      //  HashSet<string> unique = new HashSet<string> ();
      //  int totalSpectrumCount = 0;

      //  Progress.SetRange(1, sourceFiles.Length);
      //  int count = 0;
      //  LineFormat<IIdentifiedSpectrum> pepFormat = null;
      //  foreach (string sourceFile in sourceFiles)
      //  {
      //    Progress.SetMessage("Processing " + sourceFile + " ...");

      //    var spectra = format.ReadFromFile(sourceFile);

      //    totalSpectrumCount += spectra.Count;
      //    unique.UnionWith(IdentifiedSpectrumUtils.GetUniquePeptide(spectra));

      //    if(count == 0){
      //      pepFormat = format.PeptideFormat;
      //      sw.WriteLine(pepFormat.GetHeader());
      //    }

      //    spectra.ForEach(m => sw.WriteLine(pepFormat.GetString(m)));

      //    count++;
      //    Progress.SetPosition(count);
      //  }

      //  format.WriteSummary(sw, totalSpectrumCount, unique.Count);

      //  Progress.End();
      //}

      return new[] { targetFilename };
    }
  }
}
