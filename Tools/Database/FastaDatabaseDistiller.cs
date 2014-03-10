using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using RCPA.Seq;
using System.IO;
using RCPA.Utils;

namespace RCPA.Tools.Database
{
  public class FastaDatabaseDistiller : AbstractThreadFileProcessor
  {
    private string name;
    private Regex nameRegex;
    public FastaDatabaseDistiller(string name, string namePattern)
    {
      this.name = name;
      this.nameRegex = new Regex(namePattern);
    }

    public override IEnumerable<string> Process(string fileName)
    {
      string result = FileUtils.ChangeExtension(fileName, "") + "_" + name + new FileInfo(fileName).Extension;
      FastaFormat format = new FastaFormat();

      Progress.SetMessage("Processing " + fileName);
      using (StreamReader sr = new StreamReader(fileName))
      {
        Progress.SetRange(0, sr.BaseStream.Length);
        using (StreamWriter sw = new StreamWriter(result))
        {
          Sequence seq;
          while ((seq = format.ReadSequence(sr)) != null)
          {
            if (Progress.IsCancellationPending())
            {
              throw new UserTerminatedException();
            }

            Progress.SetPosition(sr.BaseStream.Position);
            if (nameRegex.Match(seq.Name).Success)
            {
              format.WriteSequence(sw, seq);
            }
          }
        }
      }

      return new string[] { result };
    }
  }
}
