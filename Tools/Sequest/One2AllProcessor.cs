using System;
using System.Collections.Generic;
using System.Text;
using RCPA.Utils;
using System.IO;
using RCPA.Proteomics.Sequest;
using System.Collections.Specialized;
using RCPA.Proteomics.Summary;

namespace RCPA.Tools.Sequest
{
  public class One2AllProcessor : AbstractThreadFileProcessor
  {
    private Dictionary<string, List<string>> filePathMap;

    private string targetDirectory;

    private bool extractToSameDirectory;

    private OutParser parser = new OutParser(false);

    public One2AllProcessor(string dtasRoot, string targetDirectory, bool extractToSameDirectory)
      : this(FileUtils.GetFiles(dtasRoot, new string[]{"*.dtas","*.dtas.zip"}, true), targetDirectory, extractToSameDirectory)
    { }

    public One2AllProcessor(List<string> dtasFiles, string targetDirectory, bool extractToSameDirectory)
    {
      this.targetDirectory = targetDirectory;

      this.extractToSameDirectory = extractToSameDirectory;

      InitDtasFilePathMap(dtasFiles);
    }

    private void InitDtasFilePathMap(List<string> dtasFiles)
    {
      filePathMap = new Dictionary<string, List<string>>();
      foreach (string dtasFile in dtasFiles)
      {
        string name = new DtaOutFilenameConverter(new FileInfo(dtasFile).Name).PureName;

        if (!filePathMap.ContainsKey(name))
        {
          filePathMap[name] = new List<string>();
        }
        filePathMap[name].Add(dtasFile);
      }
    }

    public override IEnumerable<string> Process(string peptidesFilename)
    {
      List<IIdentifiedSpectrum> peptides = new SequestPeptideTextFormat().ReadFromFile(peptidesFilename);

      Dictionary<string, List<IIdentifiedSpectrum>> rawPeptideMap = IdentifiedSpectrumUtils.GetRawPeptideMap(peptides);

      Progress.SetRange(1, 1, rawPeptideMap.Count);

      List<string> raws = new List<string>(rawPeptideMap.Keys);
      raws.Sort();

      int position = 0;
      int totalRaws = rawPeptideMap.Count;
      foreach (string raw in raws)
      {
        if (Progress.IsCancellationPending())
        {
          throw new UserTerminatedException();
        }

        Progress.SetPosition(1, position++);
        Progress.SetMessage(1, MyConvert.Format("{0}/{1}, Extracting {2} for {3} peptides ...", position, totalRaws, raw, rawPeptideMap[raw].Count));

        if (!filePathMap.ContainsKey(raw))
        {
          throw new Exception("Cannot find raw dtas/outs file for " + raw);
        }

        if (filePathMap[raw].Count == 1)
        {
          ExtractSingleRaw(raw, filePathMap[raw][0], rawPeptideMap[raw]);
        }
        else
        {
          ExtractMultipleRaw(raw, filePathMap[raw], rawPeptideMap[raw]);
        }
      }

      Progress.SetPosition(1, position);

      return new List<string>();
    }

    private static List<string> GetSequestFilenames(List<IIdentifiedSpectrum> peptides, string extension)
    {
      List<string> result = new List<string>();
      foreach (IIdentifiedSpectrum peptide in peptides)
      {
        peptide.Query.FileScan.Extension = extension;
        result.Add(peptide.Query.FileScan.LongFileName);
      }
      return result;
    }

    private static List<string> GetDtaFilenames(List<IIdentifiedSpectrum> peptides)
    {
      return GetSequestFilenames(peptides, "dta");
    }

    private static List<string> GetOutFilenames(List<IIdentifiedSpectrum> peptides)
    {
      return GetSequestFilenames(peptides, "out");
    }

    private void ExtractMultipleRaw(string raw, List<string> rawFiles, List<IIdentifiedSpectrum> peptides)
    {
      string toDirectory = CreateToDirectory(raw);

      List<string> dtaFilenames = GetDtaFilenames(peptides);
      string dtasFile = new DtaOutFilenameConverter(rawFiles[0]).GetDtasFilename();
      using (DtasReader dtas = new DtasReader(dtasFile))
      {
        Progress.SetMessage(0, "Extracting from " + dtasFile + " ...");
        ExtractFile(dtas, dtaFilenames, toDirectory);
      }

      Dictionary<string, IIdentifiedSpectrum> outFilenamePeptideMap = GetOutFilenamePeptideMap(peptides);
      foreach (string rawFile in rawFiles)
      {
        string outsFile = new DtaOutFilenameConverter(rawFiles[0]).GetOutsFilename();
        Progress.SetMessage(0, "Extracting from " + outsFile + " ...");
        using (OutsReader outs = new OutsReader(outsFile))
        {
          ExtractFileByPeptide(outs, outFilenamePeptideMap, toDirectory);
        }
      }

      if (outFilenamePeptideMap.Count > 0)
      {
        StringBuilder sb;
        if (outFilenamePeptideMap.Count == 1)
        {
          sb = new StringBuilder("There is a file not found in dtas/outs file");
        }
        else
        {
          sb = new StringBuilder("There are a few files not found in dtas/outs file");
        }

        foreach (string key in outFilenamePeptideMap.Keys)
        {
          sb.Append("\n" + key);
        }

        throw new Exception(sb.ToString());
      }
    }

    private string CreateToDirectory(string raw)
    {
      string toDirectory = targetDirectory;
      if (!extractToSameDirectory)
      {
        toDirectory = toDirectory + "/" + raw;
      }

      DirectoryInfo toDir = new DirectoryInfo(toDirectory);
      if (!toDir.Exists)
      {
        toDir.Create();
      }

      return toDirectory;
    }

    private Dictionary<string, IIdentifiedSpectrum> GetOutFilenamePeptideMap(List<IIdentifiedSpectrum> peptides)
    {
      Dictionary<string, IIdentifiedSpectrum> result = new Dictionary<string, IIdentifiedSpectrum>();
      foreach (IIdentifiedSpectrum peptide in peptides)
      {
        peptide.Query.FileScan.Extension = "out";
        string file = peptide.Query.FileScan.LongFileName;
        result[file] = peptide;
      }
      return result;
    }

    private void ExtractFileByPeptide(OutsReader reader, Dictionary<string, IIdentifiedSpectrum> outFilenamePeptideMap, string toDirectory)
    {
      Progress.SetRange(0, 1, reader.FileCount);
      int count = 0;
      while (outFilenamePeptideMap.Count > 0 && reader.HasNext)
      {
        if (Progress.IsCancellationPending())
        {
          throw new UserTerminatedException();
        }

        string filename = reader.NextFilename;
        if (outFilenamePeptideMap.ContainsKey(filename))
        {
          List<string> content = reader.NextContent();

          IIdentifiedSpectrum sph = outFilenamePeptideMap[filename];

          IIdentifiedSpectrum parsed = parser.Parse(content);

          if (sph.Sequence.Equals(parsed.Sequence))
          {
            string targetFilename = toDirectory + "/" + filename;
            using (StreamWriter sw = new StreamWriter(targetFilename))
            {
              foreach (string line in content)
              {
                sw.WriteLine(line);
              }
            }

            outFilenamePeptideMap.Remove(filename);
          }
        }
        else
        {
          reader.SkipNextContent();
        }

        Progress.SetPosition(0, count++);
      }

      Progress.SetPosition(0, reader.FileCount);
    }

    private void ExtractSingleRaw(string raw, string rawFile, List<IIdentifiedSpectrum> peptides)
    {
      string toDirectory = CreateToDirectory(raw);

      List<string> dtaFilenames = GetDtaFilenames(peptides);
      string dtasFile = new DtaOutFilenameConverter(rawFile).GetDtasFilename();
      using (DtasReader dtas = new DtasReader(dtasFile))
      {
        Progress.SetMessage(0, MyConvert.Format("Extracting dta from {0} ...", dtasFile));
        ExtractFile(dtas, dtaFilenames, toDirectory);
      }

      List<string> outFilenames = GetOutFilenames(peptides);
      string outsFile = new DtaOutFilenameConverter(rawFile).GetOutsFilename();
      using (OutsReader outs = new OutsReader(outsFile))
      {
        Progress.SetMessage(0, MyConvert.Format("Extracting out from {0} ...", outsFile));
        ExtractFile(outs, outFilenames, toDirectory);
      }
    }

    private void ExtractFile(IMergedFileReader reader, List<string> filenames, string toDirectory)
    {
      Progress.SetRange(0, 1, reader.FileCount);

      int count = 0;
      while (filenames.Count > 0 && reader.HasNext)
      {
        if (Progress.IsCancellationPending())
        {
          throw new UserTerminatedException();
        }

        string filename = reader.NextFilename;
        if (filenames.Contains(filename))
        {
          filenames.Remove(filename);
          List<string> content = reader.NextContent();

          string targetFilename = toDirectory + "/" + filename;
          using (StreamWriter sw = new StreamWriter(targetFilename))
          {
            foreach (string line in content)
            {
              sw.WriteLine(line);
            }
          }
        }
        else
        {
          reader.SkipNextContent();
        }

        Progress.SetPosition(0, count++);
      }
      Progress.SetPosition(0, reader.FileCount);
    }
  }
}
