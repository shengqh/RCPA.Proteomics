using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RCPA.Proteomics.Mascot;
using RCPA.Proteomics.Summary;
using System.IO;
using RCPA.Proteomics.Spectrum;
using RCPA.Proteomics;
using System.Text.RegularExpressions;
using RCPA.Utils;

namespace RCPA.Tools.Distiller
{
  public class MaxQuantMgfDistiller : AbstractThreadFileProcessor
  {
    private string[] sourceFiles;

    private ITitleParser parser;

    private bool singleResult;

    private string singleResultFilename;

    public MaxQuantMgfDistiller(string[] sourceFiles, ITitleParser parser, bool singleResult, string singleResultFilename)
    {
      this.sourceFiles = sourceFiles;
      this.parser = parser;
      this.singleResult = singleResult;
      this.singleResultFilename = singleResultFilename;
    }

    public override IEnumerable<string> Process(string peptideFile)
    {
      Progress.SetMessage("Loading peptide file {0}...", peptideFile);

      var format = new MascotPeptideTextFormat();
      var peptides = format.ReadFromFile(peptideFile);

      var map = peptides.ToDictionary(p => GetScan(p.Query.FileScan));

      var pepMap = new Dictionary<string, List<IIdentifiedSpectrum>>();

      Regex silac = new Regex(@"\.((?:iso|sil\d))_\d+.msm");

      Dictionary<string, StreamWriter> swmap = new Dictionary<string, StreamWriter>();
      try
      {

        int count = 0;
        foreach (var msmFile in sourceFiles)
        {
          string resultFileName = GetResultFilename(silac, msmFile, peptideFile);
          if (!swmap.ContainsKey(resultFileName))
          {
            swmap[resultFileName] = null;
          }

          count++;

          Progress.SetMessage("Parsing {0}/{1} : {2} ...", count, sourceFiles.Length, msmFile);
          using (var sr = new StreamReader(msmFile))
          {
            Progress.SetRange(0, sr.BaseStream.Length);
            MascotGenericFormatSectionReader reader = new MascotGenericFormatSectionReader(sr);
            while (reader.HasNext() && map.Count > 0)
            {
              if (Progress.IsCancellationPending())
              {
                throw new UserTerminatedException();
              }

              string title = reader.GetNextTitle();
              var scan = GetScan(parser.GetValue(title));
              if (map.ContainsKey(scan))
              {
                var spectrum = map[scan];
                var section = reader.Next();

                var sw = swmap[resultFileName];
                if (sw == null)
                {
                  sw = new StreamWriter(resultFileName);
                  swmap[resultFileName] = sw;

                  pepMap[resultFileName] = new List<IIdentifiedSpectrum>();
                }

                section.ForEach(m => sw.WriteLine(m));
                pepMap[resultFileName].Add(spectrum);

                map.Remove(scan);
              }
              else
              {
                reader.SkipNext();
              }

              Progress.SetPosition(sr.BaseStream.Position);
            }
          }
        }
      }
      finally
      {
        foreach (var sw in swmap.Values)
        {
          if (sw != null)
          {
            sw.Close();
          }
        }
      }

      var result = new List<string>(from k in swmap
                                    where k.Value != null
                                    select k.Key);

      foreach (var pep in pepMap)
      {
        var pepFilename = FileUtils.ChangeExtension(pep.Key, ".peptides");
        format.WriteToFile(pepFilename, pep.Value);
      }

      if (map.Count > 0)
      {
        var missed = peptideFile + ".missed";
        result.Add(missed);
        format.WriteToFile(missed, map.Values.ToList());
      }

      return result;
    }

    private string GetResultFilename(Regex silac, string msmFile, string peptideFile)
    {
      if (singleResult)
      {
        return singleResultFilename;
      }

      Match m = silac.Match(msmFile);
      string name;
      if (m.Success)
      {
        name = "." + m.Groups[1].Value;
      }
      else
      {
        name = ".iso";
      }

      return peptideFile + name + ".mgf";
    }

    private static string GetScan(SequestFilename p)
    {
      return MyConvert.Format("{0}.{1}", p.Experimental, p.FirstScan);
    }
  }
}
