using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace RCPA.Proteomics.Quantification
{
  public class CensusMs1IndexFileBuilder : AbstractThreadFileProcessor
  {
    public string GetResultFilename(string ms1Filename)
    {
      return ms1Filename + ".index";
    }

    public override IEnumerable<string> Process(string filename)
    {
      string resultFilename = GetResultFilename(filename);
      using (var sr = new StreamReader(filename))
      {
        long filesize = sr.BaseStream.Length;
        Progress.SetRange(0, filesize);

        var buffer = new char[1000000];
        using (var sw = new StreamWriter(resultFilename))
        {
          var sb = new StringBuilder();
          var r = new Regex(@"\nS\s+(\d+)\s+\d+");
          long lastPosition = 0;

          while (!sr.EndOfStream)
          {
            if (Progress.IsCancellationPending())
            {
              sw.Close();

              var fi = new FileInfo(resultFilename);
              if (fi.Exists)
              {
                fi.Delete();
              }

              throw new UserTerminatedException();
            }

            Progress.SetPosition(lastPosition);

            int actualLength = sr.ReadBlock(buffer, 0, buffer.Length);
            sb.Append(buffer, 0, actualLength);

            Match m = r.Match(sb.ToString());
            int lastMatchPosition = -1;
            while (m.Success)
            {
              int scan = int.Parse(m.Groups[1].Value);
              lastMatchPosition = m.Index + 1;
              sw.Write("{0}\t{1}\n", scan, lastPosition + lastMatchPosition);
              m = m.NextMatch();
            }

            if (lastMatchPosition != -1)
            {
              sb.Remove(0, lastMatchPosition);
              lastPosition += lastMatchPosition;
            }
          }
        }
      }
      return new[] { resultFilename };
    }
  }
}