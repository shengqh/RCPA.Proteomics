using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using RCPA.Proteomics.Summary;
using MathNet.Numerics.Statistics;

namespace RCPA.Proteomics.MaxQuant
{
  public class MaxQuantItemFunc
  {
    public Func<MaxQuantItem, string> ItemFunc { get; set; }

    public string Name { get; set; }

    public override string ToString()
    {
      return Name;
    }
  }

  public class MaxQuantPeptideRatioDistiller2 : AbstractThreadFileProcessor
  {
    private int minCount;
    private List<MaxQuantItemFunc> funcs;

    public MaxQuantPeptideRatioDistiller2(int minQuantifiedExperimentCount, IEnumerable<MaxQuantItemFunc> funcs)
    {
      this.minCount = minQuantifiedExperimentCount;
      this.funcs = funcs.ToList();
    }

    public override IEnumerable<string> Process(string fileName)
    {
      var ratioResult = fileName + ".dist";
      using (StreamReader sr = new StreamReader(fileName))
      using (StreamWriter sw = new StreamWriter(ratioResult))
      {
        var header = sr.ReadLine();
        var peptideFormat = new LineFormat<IIdentifiedSpectrum>(IdentifiedSpectrumPropertyConverterFactory.GetInstance(), header, "mascot");
        var spectrum = peptideFormat.ParseString(sr.ReadLine());
        var itemList = spectrum.GetMaxQuantItemList();
        sw.Write("Peptide");
        foreach (var func in funcs)
        {
          foreach (var item in itemList)
          {
            sw.Write("\t" + func.Name + ' ' + item.Name);
          }
        }
        sw.WriteLine();

        while (spectrum != null)
        {
          var curItem = spectrum.GetMaxQuantItemList();
          if (curItem.QuantifiedExperimentCount >= minCount)
          {

            StringBuilder sb = new StringBuilder(spectrum.Peptide.Sequence);

            bool bHasValue = false;
            foreach (var func in funcs)
            {
              foreach (var item in curItem)
              {
                var value = func.ItemFunc(item);

                if (value == string.Empty)
                {
                  sb.Append("\tNA");
                }
                else
                {
                  bHasValue = true;
                  sb.AppendFormat("\t{0}", value);
                }
              }
            }

            if (bHasValue)
            {
              sw.WriteLine(sb.ToString());
            }
          }

          string line = sr.ReadLine();
          if (line == null)
          {
            break;
          }

          spectrum = peptideFormat.ParseString(line);
        }

        return new[] { ratioResult };
      }
    }
  }
}
