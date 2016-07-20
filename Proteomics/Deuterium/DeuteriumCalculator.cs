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
      var builder = new ChromatographProfileBuilder(options);

      if (!File.Exists(options.BoundaryOutputFile) || options.Overwrite)
      {
        Progress.SetMessage("Finding envelope ...");

        var outputFile = options.OutputFile;
        var drawImage = options.DrawImage;

        options.OutputFile = options.BoundaryOutputFile;
        options.DrawImage = false;

        builder.Progress = this.Progress;
        builder.Process();

        options.OutputFile = outputFile;
        options.DrawImage = drawImage;
      }

      if (!File.Exists(options.DeuteriumOutputFile) || options.Overwrite)
      {
        Progress.SetMessage("Calculating deuterium ...");
        var deuteriumOptions = new RTemplateProcessorOptions()
        {
          InputFile = options.BoundaryOutputFile,
          OutputFile = options.DeuteriumOutputFile,
          RTemplate = DeuteriumR,
          RExecute = SystemUtils.GetRExecuteLocation(),
          CreateNoWindow = true
        };

        deuteriumOptions.Parameters.Add("outputImage<-" + (options.DrawImage ? "1" : "0"));
        deuteriumOptions.Parameters.Add("excludeIsotopic0<-" + (options.ExcludeIsotopic0 ? "1" : "0"));

        new RTemplateProcessor(deuteriumOptions) { Progress = this.Progress }.Process();
      }

      var deuteriumMap = new MapReader("ChroFile", "Deuterium").ReadFromFile(options.DeuteriumOutputFile);

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
      format.WriteToFile(options.OutputFile, calcSpectra);

      Progress.SetMessage("Finished ...");

      return new string[] { options.OutputFile };
    }
  }
}
