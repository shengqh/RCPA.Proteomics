using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace RCPA.Proteomics.MzIdent
{
  public class MzIdentSlimProcessor : AbstractThreadProcessor
  {
    private MzIdentSlimProcessorOptions options;

    public MzIdentSlimProcessor(MzIdentSlimProcessorOptions options)
    {
      this.options = options;
    }

    public override IEnumerable<string> Process()
    {
      var result = new List<string>();
      foreach (var file in options.SourceFiles)
      {
        if (file.EndsWith(".slim.mzid"))
        {
          continue;
        }

        var targetFile = Path.Combine(options.TargetDirectory, Path.GetFileNameWithoutExtension(file) + ".slim.mzid");
        result.Add(targetFile);

        if (File.Exists(targetFile) && !options.Overwrite)
        {
          continue;
        }

        var tmpFile = targetFile + ".tmp";

        Progress.SetMessage("Processing " + file + " ...");

        bool export = true;
        using (var sr = new StreamReader(file))
        using (var sw = new StreamWriter(tmpFile))
        {
          string line;
          while ((line = sr.ReadLine()) != null)
          {
            if (line.Contains("<SpectrumIdentificationItem"))
            {
              export = line.Contains("rank=\"1\"");
            }
            else if (line.Contains("</SpectrumIdentificationItem>"))
            {
              if (export)
              {
                sw.WriteLine(line);
              }
              else
              {
                export = true;
              }
              continue;
            }

            if (export)
            {
              sw.WriteLine(line);
            }
          }
        }

        File.Move(tmpFile, targetFile);
        //break;
      }

      return result;
    }
  }
}
