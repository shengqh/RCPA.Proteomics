using RCPA.Proteomics.Mascot;
using System.Collections.Generic;
using System.IO;

namespace RCPA.Proteomics.Summary
{
  public class DtaselectFileSplitter : AbstractThreadFileProcessor
  {
    private int splitCount;
    public DtaselectFileSplitter(int splitCount)
    {
      this.splitCount = splitCount;
    }

    public override IEnumerable<string> Process(string filename)
    {
      string[] files = new string[splitCount];
      MascotResultDtaselectFormat dtaSelectFormat = new MascotResultDtaselectFormat();

      Progress.SetMessage("Reading document ...");
      IIdentifiedResult mr = dtaSelectFormat.ReadFromFile(filename);

      Progress.SetRange(0, splitCount);
      for (int i = 0; i < splitCount; i++)
      {
        files[i] = FileUtils.ChangeExtension(filename, (i + 1).ToString() + new FileInfo(filename).Extension);
        MascotResult curMr = new MascotResult();

        for (int j = 0; j < mr.Count; j++)
        {
          if (j % splitCount == i)
          {
            curMr.Add(mr[j]);
          }
        }

        Progress.SetMessage("Writing document " + files[i] + " ...");
        dtaSelectFormat.WriteToFile(files[i], curMr);
        Progress.SetPosition(i + 1);
      }
      Progress.End();

      return new List<string>(files);
    }
  }
}
