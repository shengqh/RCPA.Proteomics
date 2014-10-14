using System;
using System.Collections.Generic;
using System.Text;
using RCPA.Proteomics.Summary;
using System.IO;
using RCPA.Seq;

namespace RCPA.Tools.Summary
{
  public class IdentifiedResultSeparatorBySpectrumFilter : AbstractSeparatorBySpectrumFilter
  {
    public IdentifiedResultSeparatorBySpectrumFilter(Dictionary<IFilter<IIdentifiedSpectrum>, string> filterMap)
      : base(filterMap, true)
    { }

    protected override IEnumerable<string> DoProcess(string filename, List<string> result, Dictionary<IFilter<IIdentifiedSpectrum>, SpectrumEntry> map)
    {
      string database = filename + ".fasta";
      if (!File.Exists(database))
      {
        throw new Exception("Fasta file not exists : " + database);
      }

      IAccessNumberParser acParser = AccessNumberParserFactory.GuessParser(database);
      Dictionary<string, Sequence> seqMap = DatabaseUtils.GetAccessNumberMap(database, acParser);

      try
      {
        using (IdentifiedProteinGroupEnumerator iter = new IdentifiedProteinGroupEnumerator(filename))
        {
          foreach (IFilter<IIdentifiedSpectrum> filter in map.Keys)
          {
            SpectrumEntry entry = map[filter];
            entry.ResultWriter.WriteLine(iter.ProteinFormat.GetHeader());
            entry.ResultWriter.WriteLine(iter.PeptideFormat.GetHeader());
          }

          while (iter.MoveNext())
          {
            IIdentifiedProteinGroup group = iter.Current;

            List<IIdentifiedSpectrum> spectra = group[0].GetSpectra();

            foreach (IFilter<IIdentifiedSpectrum> filter in map.Keys)
            {
              SpectrumEntry entry = map[filter];
              entry.Spectra.Clear();

              foreach (IIdentifiedSpectrum spectrum in spectra)
              {
                if (filter.Accept(spectrum))
                {
                  entry.Spectra.Add(spectrum);
                }
              }

              if (entry.Spectra.Count > 0)
              {
                for (int i = 0; i < group.Count; i++)
                {
                  entry.ResultWriter.WriteLine("${0}-{1}{2}", group.Index, i + 1, iter.ProteinFormat.GetString(group[i]));

                  string ac = acParser.GetValue(group[i].Name);
                  Sequence seq = seqMap[ac];
                  entry.FastaWriter.WriteLine(">" + seq.Reference);
                  entry.FastaWriter.WriteLine(seq.SeqString);
                }

                foreach (IIdentifiedSpectrum spectrum in entry.Spectra)
                {
                  entry.ResultWriter.WriteLine(iter.PeptideFormat.GetString(spectrum));
                }
              }
            }
          }

          return result;
        }
      }
      finally
      {
        foreach (SpectrumEntry entry in map.Values)
        {
          entry.Dispose();
        }
      }
    }
  }
}
