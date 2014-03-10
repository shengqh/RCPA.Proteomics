using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace RCPA.Proteomics.Statistic
{
  public class PrecursorOffsetEntry
  {
    public PrecursorOffsetEntry(string exp, double mean, double median)
    {
      this.Experimental = exp;
      this.Mean = mean;
      this.Median = median;
    }

    public string Experimental { get; set; }

    public double Mean { get; set; }

    public double Median { get; set; }

    public static List<PrecursorOffsetEntry> ReadFromFile(string fileName)
    {
      var lines = File.ReadAllLines(fileName).Skip(1).ToList();
      return (from line in lines
              let parts = line.Split('\t')
              let exp = parts[0]
              let mean = MyConvert.ToDouble(parts[1])
              let median = MyConvert.ToDouble(parts[2])
              select new PrecursorOffsetEntry(exp, mean, median)).ToList();
    }

    public static void WriteToFile(string fileName, List<PrecursorOffsetEntry> entries)
    {
      using (StreamWriter sw = new StreamWriter(fileName))
      {
        sw.WriteLine("File\tMean(Offset)\tMedian(Offset)\t###Offset=TheoreticalMass-ExperimentalMass");
        entries.ForEach(m => sw.WriteLine("{0}\t{1:0.00}\t{2:0.00}", m.Experimental, m.Mean, m.Median));
      }
    }
  }
}
