using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RCPA.Seq;
using System.IO;
using RCPA.Utils;

namespace RCPA.Proteomics.Database
{
  public class ExtractFastaFromDatFileProcessor : AbstractThreadFileProcessor
  {
    public override IEnumerable<string> Process(string fileName)
    {
      DatFormat reader = new DatFormat();
      FastaFormat writer = new FastaFormat();

      string result = FileUtils.ChangeExtension(fileName, ".fasta");

      long fileLength = new FileInfo(fileName).Length;
      using (StreamReader sr = new StreamReader(fileName))
      using (StreamWriter sw = new StreamWriter(result))
      {
        Progress.SetRange(0, fileLength);

        Sequence seq;
        while ((seq = reader.ReadSequence(sr)) != null)
        {
          if (Progress.IsCancellationPending())
          {
            throw new UserTerminatedException();
          }

          Progress.SetPosition(sr.GetCharpos());

          writer.WriteSequence(sw, seq);
        }
      }

      return new string[] { result };
    }
  }
}
