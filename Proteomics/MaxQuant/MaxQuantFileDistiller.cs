using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RCPA.Proteomics.Summary;
using RCPA.Utils;
using System.IO;

namespace RCPA.Proteomics.MaxQuant
{
  public class MaxQuantFileDistiller : AbstractThreadFileProcessor
  {
    private string targetFile;

    private MaxQuantTagMatchRoles roles;

    public MaxQuantFileDistiller(string targetFile, MaxQuantTagMatchRoles roles)
    {
      this.targetFile = targetFile;
      this.roles = roles;
    }

    public override IEnumerable<string> Process(string fileName)
    {
      var targetFormat = new AnnotationTextFormat(roles.GetTargetHeaders());
      targetFormat.Progress = this.Progress;

      Progress.SetMessage("reading target file " + targetFile + " ...");
      var targets = targetFormat.ReadFromFile(targetFile);

      Progress.SetMessage("initializing target roles ...");
      MaxQuantTagMatcher matcher = new MaxQuantTagMatcher(roles, targets);

      var resultFile = this.targetFile + ".target";
      using (StreamWriter sw = new StreamWriter(resultFile))
      {
        using (StreamReader sr = new StreamReader(fileName))
        {
          string line = sr.ReadLine();
          sw.WriteLine(line);
          var sourceLineFormat = new LineFormat<IAnnotation>(new AnnotationPropertyConverterFactory(), line);

          Progress.SetRange(0, sr.BaseStream.Length);
          Progress.SetMessage("processing source file " + fileName + " ...");
          int lineCount = 0;
          while ((line = sr.ReadLine()) != null)
          {
            lineCount++;
            if (lineCount == 100)
            {
              lineCount = 0;
              Progress.SetPosition(sr.GetCharpos());
              if (Progress.IsCancellationPending())
              {
                throw new UserTerminatedException();
              }
            }

            var source = sourceLineFormat.ParseString(line);

            if (!roles.IsValidSource(source))
            {
              continue;
            }

            if(matcher.Match(source))
            {
              sw.WriteLine(line);
              continue;
            }
          }

          var missedtargets = matcher.GetMissedTargets();
          var missedFile = this.targetFile + ".missed";

          var missedFileFormat = new AnnotationTextFormat();
          missedFileFormat.WriteToFile(missedFile, missedtargets);
          return new string[] { resultFile, missedFile };
        }
      }
    }
  }
}
