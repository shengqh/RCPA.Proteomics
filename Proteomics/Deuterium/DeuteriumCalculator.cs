using RCPA.Proteomics.Mascot;
using RCPA.Proteomics.Quantification;
using RCPA.Proteomics.Quantification.Labelfree;
using RCPA.Proteomics.Raw;
using RCPA.Proteomics.Spectrum;
using RCPA.Proteomics.Summary;
using RCPA.Proteomics.Utils;
using RCPA.R;
using RCPA.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RCPA.Proteomics.Deuterium
{
  public class DeuteriumCalculator : AbstractThreadProcessor
  {
    private static string DeuteriumR = Path.Combine(FileUtils.GetTemplateDir().FullName, "ProfileChroDeuterium.r");

    private DeuteriumCalculatorOptions options;
    public DeuteriumCalculator(DeuteriumCalculatorOptions options)
    {
      this.options = options;
    }

    public override IEnumerable<string> Process()
    {
      var outputFile = options.OutputFile;
      var boundaryFile = Path.ChangeExtension(options.OutputFile, ".boundary.csv");
      var deuteriumFile = Path.ChangeExtension(options.OutputFile, ".calc.tsv");

      options.OutputFile = boundaryFile;
      var builder = new ChromatographProfileBuilder(options);
      builder.Progress = this.Progress;
      builder.Process();

      Progress.SetMessage("Calculating deuterium ...");

      var deuteriumOptions = new RTemplateProcessorOptions()
      {
        InputFile = boundaryFile,
        OutputFile = deuteriumFile,
        RTemplate = DeuteriumR,
        RExecute = SystemUtils.GetRExecuteLocation(),
        CreateNoWindow = true
      };
      new RTemplateProcessor(deuteriumOptions) { Progress = this.Progress }.Process();

      var deuteriumMap = new MapReader("File", "Deuterium").ReadFromFile(deuteriumFile).ToDictionary(m => Path.GetFileNameWithoutExtension(m.Key), m => m.Value);

      var calcSpectra = new List<IIdentifiedSpectrum>();

      var format = new MascotPeptideTextFormat();
      var spectra = format.ReadFromFile(options.InputFile);
      foreach (var pep in spectra)
      {
        var filename = Path.GetFileNameWithoutExtension(builder.GetTargetFile(pep));
        if (deuteriumMap.ContainsKey(filename))
        {
          pep.Annotations["Deuterium"] = deuteriumMap[filename];
          calcSpectra.Add(pep);
        }
      }
      format.PeptideFormat.Headers = format.PeptideFormat.Headers + "\tDeuterium";
      format.WriteToFile(outputFile, calcSpectra);

      Progress.SetMessage("Finished ...");

      return new string[] { outputFile };
    }
  }
}
