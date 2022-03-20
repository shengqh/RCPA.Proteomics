using RCPA.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace RCPA.Tools.Quantification
{
  public class ITraqResultImageBuilder : IFileProcessor
  {
    private string rLocation;
    public ITraqResultImageBuilder(string rLocation)
    {
      this.rLocation = rLocation;
    }

    #region IFileProcessor Members

    public IEnumerable<string> Process(string itraqFileName)
    {
      DirectoryInfo scriptsDir = new DirectoryInfo(new FileInfo(Application.ExecutablePath).DirectoryName + "/scripts");

      string rDistribution = scriptsDir + "/ITraqRatioDistribution.R";
      if (!File.Exists(rDistribution))
      {
        throw new FileNotFoundException(MyConvert.Format("File not found : {0}", rDistribution));
      }

      string title = new FileInfo(itraqFileName).Name;
      while (Path.HasExtension(title))
      {
        title = FileUtils.ChangeExtension(title, "");
      }

      string tmpFilename = scriptsDir + "/temp.R";
      try
      {
        string resultFileName = itraqFileName + ".jpg";
        using (StreamWriter sw = new StreamWriter(tmpFilename))
        {
          sw.WriteLine(MyConvert.Format("source(\"{0}\");", rDistribution).Replace('\\', '/'));
          sw.WriteLine(MyConvert.Format("ITraqDistribution(\"{0}\", \"{1}\", \"{2}\");", itraqFileName, title, resultFileName).Replace('\\', '/'));
        }

        try
        {
          SystemUtils.Execute(rLocation, "--no-save --quiet < " + tmpFilename);
        }
        catch (Exception ex)
        {
          throw new InvalidOperationException(MyConvert.Format("Exception thrown when generate jpg file from R : {0}", ex.Message), ex);
        }

        return new[] { resultFileName };
      }
      finally
      {
        File.Delete(tmpFilename);
      }
    }

    #endregion
  }
}
