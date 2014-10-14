using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace RCPA.Proteomics.Omssa
{
  public class OmssaOmxSlimProcessor : AbstractThreadProcessor
  {
    private OmssaOmxSlimProcessorOptions options;

    public OmssaOmxSlimProcessor(OmssaOmxSlimProcessorOptions options)
    {
      this.options = options;
    }

    public override IEnumerable<string> Process()
    {
      var result = new List<string>();
      foreach (var file in options.SourceFiles)
      {
        if (file.EndsWith(".slim.omx"))
        {
          continue;
        }

        var targetFile = Path.Combine(options.TargetDirectory, Path.GetFileNameWithoutExtension(file) + ".slim.omx");
        result.Add(targetFile);

        if (File.Exists(targetFile))
        {
          if (options.Overwrite)
          {
            File.Delete(targetFile);
          }
          else
          {
            continue;
          }
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
            if (line.Contains("<MSRequest_spectra") || line.Contains("<MSHits_mzhits>"))
            {
              export = false;
              continue;
            }
            else if (line.Contains("</MSRequest_spectra>") || line.Contains("</MSHits_mzhits>"))
            {
              export = true;
              continue;
            }

            if (export)
            {
              sw.WriteLine(line);
            }

            if (line.Contains("<MSResponse_hitsets>"))
            {
              break;
            }
          }

          //remove empty identification
          var mshit = new List<string>();
          while ((line = sr.ReadLine()) != null)
          {
            if (line.Contains("</MSResponse_hitsets>"))
            {
              sw.WriteLine(line);
              break;
            }

            if (line.Contains("</MSHitSet>"))
            {
              mshit.Add(line);
              WriteAndClearMsHit(sw, mshit);
              continue;
            }

            mshit.Add(line);
          }

          export = true;
          while ((line = sr.ReadLine()) != null)
          {
            if (line.Contains("<MSResponse_bioseqs>"))
            {
              export = false;
              continue;
            }
            else if (line.Contains("</MSResponse_bioseqs>"))
            {
              export = true;
              continue;
            }

            if (export)
            {
              sw.WriteLine(line);
            }

            if (line.Contains("<MSResponse_hitsets>"))
            {
              break;
            }
          }
        }

        File.Move(tmpFile, targetFile);
        //break;
      }

      return result;
    }

    private static void WriteAndClearMsHit(StreamWriter sw, List<string> mshit)
    {
      if (mshit.Count > 0)
      {
        if (mshit.Any(m => m.Contains("MSHits_evalue")))
        {
          bool export = true;
          foreach (var line in mshit)
          {
            if (line.Contains("<MSHits_mzhits>"))
            {
              export = false;
              continue;
            }

            if (line.Contains("</MSHits_mzhits>"))
            {
              export = true;
              continue;
            }

            if (export)
            {
              sw.WriteLine(line);
            }
          }
        }
        mshit.Clear();
      }
    }
  }
}
