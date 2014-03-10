using System.Collections.Generic;
using RCPA.Gui;
using RCPA.Proteomics.Summary;
using ICSharpCode.SharpZipLib.Zip;
using System.IO;
using System;

namespace RCPA.Proteomics.Sequest
{
  public class SequestOutZipParser : ProgressClass, ISequestOutParser
  {
    private readonly OutParser parser;

    public SequestOutZipParser(bool raiseDuplicatedReferenceAbsentException)
    {
      this.parser = new OutParser(raiseDuplicatedReferenceAbsentException);
    }

    public SequestOutZipParser(bool raiseDuplicatedReferenceAbsentException, double modificationDeltaScore)
    {
      this.parser = new ModificationOutParser(raiseDuplicatedReferenceAbsentException, modificationDeltaScore);
    }

    public SequestOutZipParser()
      : this(true)
    {
    }

    public SequestOutZipParser(double modificationDeltaScore)
      : this(true, modificationDeltaScore)
    {
    }

    public List<IIdentifiedSpectrum> ParsePeptides(string outZipFilename)
    {
      var result = new List<IIdentifiedSpectrum>();

      //检查dta文件与out文件数目是否匹配，以防止数据库搜索出错。
      using (var s = new ZipInputStream(new FileInfo(outZipFilename).OpenRead()))
      {
        int dtacount = 0;
        int outcount = 0;
        ZipEntry theEntry;
        while ((theEntry = s.GetNextEntry()) != null)
        {
          var name = theEntry.Name.ToLower();
          if (name.EndsWith(".out"))
          {
            outcount++;
          }
          else if (name.EndsWith(".dta"))
          {
            dtacount++;
          }
        }

        if (dtacount != 0 && outcount != dtacount)
        {
          throw new Exception(MyConvert.Format("Dta file count ({0}) in {1} is not equals to out file count ({2})",
                                            dtacount, outZipFilename, outcount));
        }
      }

      using (var s = new ZipInputStream(new FileInfo(outZipFilename).OpenRead()))
      {
        Progress.SetRange(1, new FileInfo(outZipFilename).Length);

        long length = 0;
        ZipEntry theEntry;
        while ((theEntry = s.GetNextEntry()) != null)
        {
          if (Progress.IsCancellationPending())
          {
            throw new UserTerminatedException();
          }

          if (theEntry.IsDirectory)
          {
            continue;
          }

          string entryFileName = Path.GetFileName(theEntry.Name);
          if (entryFileName != String.Empty && entryFileName.ToLower().EndsWith(".out"))
          {
            List<string> context = new List<string>();

            StreamReader sr = new StreamReader(s);
            while (!sr.EndOfStream)
            {
              context.Add(sr.ReadLine());
            }

            IIdentifiedSpectrum spectrum = this.parser.Parse(context);

            if (null != spectrum)
            {
              result.Add(spectrum);
            }
          }

          length += theEntry.Size;
          Progress.SetPosition(length);
        }
        Progress.End();
      }

      return result;
    }
  }
}