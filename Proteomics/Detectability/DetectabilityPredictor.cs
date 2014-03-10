using System;
using System.Collections.Generic;
using System.IO;
using RCPA.Utils;

namespace RCPA.Proteomics.Detectability
{
  public class DetectabilityPredictor : IFileProcessor
  {
    private readonly string predictorExeFilePath;

    public DetectabilityPredictor(string predictorExeFilePath)
    {
      if (!File.Exists(predictorExeFilePath))
      {
        throw new Exception("Cannot find predictor : " + predictorExeFilePath);
      }

      this.predictorExeFilePath = new FileInfo(predictorExeFilePath).FullName;
    }

    #region IFileProcessor Members

    public IEnumerable<string> Process(string fastaFilename)
    {
      var fi = new FileInfo(fastaFilename).FullName;
      SystemUtils.Execute(this.predictorExeFilePath, "-F \"" + fi + "\"");

      DirectoryInfo di = new FileInfo(fastaFilename).Directory;
      List<DetectabilityEntry> deList = DetectabilityEntry.ReadFromDirectory(di.FullName);

      string detectabilityFilename = FileUtils.ChangeExtension(fastaFilename, "detectability");
      DetectabilityEntry.WriteToFile(detectabilityFilename, deList);
      DetectabilityEntry.DeleteFromDirectory(di.FullName);

      return new[] { detectabilityFilename };
    }

    #endregion
  }
}